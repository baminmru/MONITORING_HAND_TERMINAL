Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Xml
Imports System.IO
Imports System.Text
Imports System.Diagnostics

<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class CMMon2Service
    Inherits System.Web.Services.WebService

    Private LogPath As String = "E:\Projects\CM\log\CMMLOG"
    Private tablename As String = "opc_lmz_indx_test"


    Private Function GetMyDir() As String
        Dim s As String
        s = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)
        s = s.Substring(6)
        Return s
    End Function
    Public Sub LogString(ByVal s As String)
        Try
            Dim d As DateTime
            d = Date.Now
            File.AppendAllText(LogPath & d.Year.ToString() & "_" & d.Month.ToString & "_" & d.Day.ToString & ".txt", Date.Now.ToString("dd MMM HH:mm:ss") & ">CMMon2Service: " & s & vbCrLf)
        Catch ex As Exception

        End Try

    End Sub
    Private Sub StartProcessVerify(ByVal Terminalid As Integer, ByVal POD As String)
        LogString("Start verify process ")
        Try
            Dim ProcessVerify As Process = New Process()
            Dim FileName As String
            Dim DirName As String
            FileName = GetMyDir() + "\CMMON_VERIFY.exe"
            DirName = GetMyDir()
            ProcessVerify.StartInfo.FileName = FileName
            ProcessVerify.StartInfo.Arguments = "T=" + Terminalid.ToString + " P=" + POD
            ProcessVerify.StartInfo.WorkingDirectory = DirName
            ProcessVerify.Start()
        Catch ex As Exception
            LogString(ex.Message)
        End Try
        
    End Sub


    Private Sub StartProcessFinalize(ByVal Terminalid As Integer, ByVal POD As String, ByVal ftype As String, ByVal prc As String)
        LogString("Start finalize process ")
        Try
            Dim ProcessVerify As Process = New Process()
            Dim FileName As String
            Dim DirName As String
            FileName = GetMyDir() + "\CMMON_FINALIZE.exe"
            DirName = GetMyDir()
            ProcessVerify.StartInfo.FileName = FileName
            ProcessVerify.StartInfo.Arguments = "T=" + Terminalid.ToString + " P=" + POD + " F=" + ftype + " D=" + prc
            ProcessVerify.StartInfo.WorkingDirectory = DirName
            ProcessVerify.Start()
        Catch ex As Exception
            LogString(ex.Message)
        End Try

    End Sub


    <WebMethod()> _
    Public Function RegisterInfo(ByVal TerminalID As String, ByVal Brigada As String, ByVal Tnum As String) As Boolean
        Dim cmc As CMConnector
        cmc = New CMConnector
        LogPath = cmc.LogPath
        tablename = cmc.TableName
        LogString("RegisterInfo " + TerminalID.ToString + " B=" + Brigada + " TNum=" + Tnum)
        Try
            cmc.QueryExec("insert into CMMON2_INFO (TERMINALID,EVENTDATE,TN,TNUM,LASTPING) values(" + TerminalID.ToString + ",sysdate,'" + Brigada + "','" + Tnum + "',sysdate)")
        Catch ex As Exception
            LogString(ex.Message)
            Return False
        End Try

        Return True
    End Function


    <WebMethod()> _
    Public Function RegisterPodInfo(ByVal TerminalID As String, ByVal POD As String, ByVal WP As String, ByVal QWORKERS As Integer, ByVal STATUSID As Integer, ByVal opPRC As Integer, ByVal BRK As Integer, ByVal WPChanged As Boolean, ByVal QWORKERSChanged As Boolean, ByVal STATUSChanged As Boolean, ByVal opPRCChanged As Boolean, ByVal BRKChanged As Boolean) As Boolean
        Dim cmc As CMConnector
        cmc = New CMConnector
        LogPath = cmc.LogPath
        tablename = cmc.TableName
        LogString("RegisterPodInfo " + TerminalID.ToString + " P=" + POD + " STATUSID=" + STATUSID.ToString)
        Dim dt As DataTable
        Dim ok As Boolean = False

        '  проверить прошел ли код проверку
        Try
            dt = cmc.QuerySelect("select * from CMMON2_PODVRF where TERMINALID=" + TerminalID.ToString + " and POD='" + POD + "' order by PODEVENTDATE desc")
            If dt.Rows.Count = 0 Then
                ok = False
            End If
            If dt.Rows(0)("PROCESSSTATUS") = 0 Then
                ok = False
            End If
            If dt.Rows(0)("PROCESSSTATUS") = 1 Then
                ok = True
            End If

            If dt.Rows(0)("PROCESSSTATUS") > 1 Then
                ok = False
            End If
        Catch ex As Exception
            LogString(ex.Message)
        End Try


        If ok Then
            Dim cd As Date
            dt = cmc.QuerySelect("select sysdate cd from dual")
            cd = dt.Rows(0)("cd")
            Try
                cmc.QueryExec("insert into  CMMON2_PODINFO(  TERMINALID ,POD,  PODEVENTDATE,  WP ,  QWORKERS ,  STATUSID,  OPPRC,BRK,SAVED2SAP) values( " + TerminalID.ToString + " ,'" + POD + "', " + cmc.OracleDate(cd) + ",'" + WP + "' ," + QWORKERS.ToString + " ,  " + STATUSID.ToString + ", " + opPRC.ToString + "," + BRK.ToString + ",0)")
            Catch ex As Exception
                LogString(ex.Message)
                Return False
            End Try

            '' получить  общую инфу по статусу терминала



            dt = cmc.QuerySelect("select * from CMMON2_INFO where TERMINALID=" + TerminalID.ToString + " and EVENTDATE  in (select max(EVENTDATE) from CMMON2_INFO where TERMINALID=" + TerminalID.ToString + "  )")


            '  Переслать все в таблицу ....

            'ok = SaveToSAP(cmc, TerminalID, POD, "TabNo", Integer.Parse(dt.Rows(0)("TN")), cd, 0)
            ok = True

            


            If QWORKERSChanged Then
                ok = ok And SaveToSAP(cmc, TerminalID, POD, "KOL", QWORKERS, cd, 0)
            End If
            If BRKChanged Then
                ok = ok And SaveToSAP(cmc, TerminalID, POD, "XMNGA", BRK, cd, 0)
            End If

            If WPChanged Then
                ok = ok And SaveToSAP(cmc, TerminalID, POD, "RMT", Integer.Parse(WP), cd, 0)
            End If


            If ok Then
                Try
                    cmc.QueryExec("update  CMMON2_PODINFO set SAVED2SAP=1 where TERMINALID=" + TerminalID.ToString + " and POD='" + POD + "' and PODEVENTDATE= " + cmc.OracleDate(cd))
                Catch ex As Exception
                    LogString(ex.Message)
                End Try
            End If

            If STATUSChanged Then
                If STATUSID < 90 Then
                    ok = ok And SaveToSAP(cmc, TerminalID, POD, "SO", STATUSID, cd, 0)
                Else
                    ok = ok And SaveToSAP(cmc, TerminalID, POD, "Izd", 99999900 + STATUSID, cd, 0)
                End If
                If STATUSID = 98 Then
                    If opPRCChanged Then
                        ok = ok And SaveToSAP(cmc, TerminalID, POD, "PRC", opPRC, cd, 0)
                    End If
                    StartProcessFinalize(TerminalID, POD, "98", opPRC.ToString)
                End If

                If STATUSID = 99 Then
                    StartProcessFinalize(TerminalID, POD, "99", "100")
                End If
            End If

            Return True
        Else
            Return False
        End If

    End Function

    Private Function SaveToSAP(ByVal cmc As CMConnector, ByVal terminalid As Integer, ByVal RUECK As String, ByVal VAR_NAME As String, ByVal VAR_VAL As Integer, ByVal VAR_DATE As Date, ByVal PR As Integer) As Boolean
        Try
            cmc.QueryExec("INSERT into " + cmc.TableName + " (INVN,RUECK,VAR_NAME,VAR_VAL,var_DATE,PR) values(" + terminalid.ToString() + "," + RUECK + ",'" + VAR_NAME + "'," + VAR_VAL.ToString + "," + cmc.OracleDate(VAR_DATE) + ",null)")
        Catch ex As Exception
            LogString(ex.Message)
            Return False
        End Try
        Return True
    End Function

    <WebMethod()> _
    Public Function NewPod(ByVal TerminalID As String, ByVal POD As String) As Boolean
        Dim cmc As CMConnector
        cmc = New CMConnector
        LogPath = cmc.LogPath
        LogString("NewPod T=" + TerminalID.ToString + " pod=" + POD)

        Try
            cmc.QueryExec("insert into CMMON2_PODVRF (TERMINALID,PODEVENTDATE,POD,PROCESSSTATUS,PROCESSMSG) values(" + TerminalID.ToString + ",sysdate,'" + POD + "',0,'')")
        Catch ex As Exception
            LogString(ex.Message)
            Return False
        End Try

        StartProcessVerify(TerminalID, POD)

        Return True
    End Function

    <WebMethod()> _
    Public Function VerifyPod(ByVal TerminalID As String, ByVal POD As String) As String
        Dim cmc As CMConnector
        cmc = New CMConnector
        LogPath = cmc.LogPath
        Dim dt As DataTable

        LogString("VerifyPod T=" + TerminalID.ToString + " pod=" + POD)

        Try
            dt = cmc.QuerySelect("select * from CMMON2_PODVRF where TERMINALID=" + TerminalID.ToString + " and POD='" + POD + "' order by PODEVENTDATE desc")
            If dt.Rows.Count = 0 Then
                LogString("VerifyPod->Подтверждение ранее не вводилось")
                Return "Идет проверка"
            End If
            If dt.Rows(0)("PROCESSSTATUS") = 0 Then
                LogString("VerifyPod->Идет проверка")
                Return "Идет проверка"
            End If
            If dt.Rows(0)("PROCESSSTATUS") = 1 Then
                LogString("VerifyPod->OK")
                Return "OK"
            End If
            LogString("VerifyPod->" + dt.Rows(0)("PROCESSMSG"))
            Return dt.Rows(0)("PROCESSMSG")


        Catch ex As Exception
            LogString(ex.Message)
            Return ex.Message
        End Try


    End Function


    <WebMethod()> _
    Public Sub Ping(ByVal TerminalID As Integer, ByVal Address As String)
        Dim cmc As CMConnector
        cmc = New CMConnector
        LogPath = cmc.LogPath
        Try
            
            cmc.QueryExec("update CMMON2_INFO  set LASTPING=sysdate, IPaddr='" + Address + "' where TERMINALID=" + TerminalID.ToString + " and eventdate in (select max(eventdate) from CMMON2_INFO where TERMINALID=" + TerminalID.ToString + ")")
        Catch ex As Exception
            LogString(ex.Message)
        End Try
      
    End Sub

    <WebMethod()> _
    Public Function GetMaxStatus() As Integer
        Dim cmc As CMConnector
        cmc = New CMConnector
        LogPath = cmc.LogPath
        Try
          
            Dim dt As DataTable
            dt = cmc.QuerySelect("select max(STATUSID) CNT from CMMON_STATUSNAME ")
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0)("CNT")
            Else
                Return 0
            End If
        Catch ex As Exception
            LogString(ex.Message)
            Return 0
        End Try
       
    End Function

    <WebMethod()> _
    Public Function GetStatusName(ByVal id As Integer) As String
        Dim cmc As CMConnector
        cmc = New CMConnector
        LogPath = cmc.LogPath
        Try
            
            Dim dt As DataTable
            dt = cmc.QuerySelect("select NAME from CMMON_STATUSNAME where STATUSID=" + id.ToString)
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0)("NAME")
            Else
                Return "UNKNOWN"
            End If
        Catch ex As Exception
            LogString(ex.Message)
            Return "UNKNOWN"
        End Try
       
    End Function

    <WebMethod()> _
    Public Function AddStatus(ByVal id As Integer, ByVal name As String) As Boolean
        Dim cmc As CMConnector
        cmc = New CMConnector
        LogPath = cmc.LogPath
        Try

            cmc.QueryExec("insert into CMMON_STATUSNAME ( NAME,STATUSID) values('" + name + "'," + id.ToString + ")")
        Catch ex As Exception
            LogString(ex.Message)
            Return False
        End Try
        Return True

    End Function


    <WebMethod()> _
    Public Function GetOPInfo(ByVal OpCode As String) As String
        Dim cmc As CMConnector
        cmc = New CMConnector
        LogPath = cmc.LogPath
        Try
           
            Dim dt As DataTable
            Dim op As String
            op = OpCode
            While op.Length < 10
                op = "0" + op
            End While
            dt = cmc.QuerySelect("select VORNR||' '||LTXA1||' " & vbCrLf & "'||MATNR||' '||MAKTX txt from opc_mk_sap_all where RUECK = '" + op + "'")
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0)("txt")
            Else
                Return "UNKNOWN"
            End If
        Catch ex As Exception
            LogString(ex.Message)
            Return "UNKNOWN"
        End Try
        
    End Function

    <WebMethod()> _
    Public Function GetTerminalOperations(ByVal terminalid As String) As String
        Dim cmc As CMConnector
        cmc = New CMConnector
        LogPath = cmc.LogPath
        Dim sout As String = "<data>"
        Try

            Dim dt As DataTable
            Dim dt1 As DataTable
            Dim dt2 As DataTable

            dt = cmc.QuerySelect("SELECT tn, max(eventdate) adt   FROM CMMON2_INFO   WHERE terminalid=" + terminalid.ToString() + "  GROUP BY  tn  ORDER BY max(eventdate) DESC")
            Dim atn As String
            Dim adt As DateTime
            If dt.Rows.Count > 0 Then
                atn = dt.Rows(0)("tn")
                adt = dt.Rows(0)("adt")
                Dim apod As String
                Dim apoddt As DateTime
                Dim i As Integer
                Dim j As Integer
                dt1 = cmc.QuerySelect("SELECT pod, max(podeventdate) podeventdate FROM CMMON2_INFO JOIN CMMON2_PODINFO " & _
                                      " ON   cmmon2_info.terminalid = CMMON2_PODINFO.terminalid    " & _
                                      " AND  saved2sap=1 WHERE CMMON2_INFO.tn = '" + atn + "' And CMMON2_PODINFO.podeventdate >CMMON2_INFO.eventdate and CMMON2_INFO.eventdate = " + cmc.OracleDate(adt) + " " & _
                                      " GROUP BY  pod ORDER BY pod ")
                For i = 0 To dt1.Rows.Count - 1

                    apod = dt1.Rows(i)("pod")
                    apoddt = dt1.Rows(i)("podeventdate")

                    dt2 = cmc.QuerySelect("SELECT WP,QWORKERS,STATUSID,OPPRC,BRK  FROM CMMON2_INFO JOIN CMMON2_PODINFO " & _
                                     " ON   cmmon2_info.terminalid = CMMON2_PODINFO.terminalid    " & _
                                     " AND STATUSID < 98 AND saved2sap=1 WHERE CMMON2_INFO.tn = '" + atn + "' And CMMON2_INFO.eventdate = " + cmc.OracleDate(adt) + " " & _
                                     " AND CMMON2_PODINFO.POD = '" + apod + "' And CMMON2_PODINFO.podeventdate = " + cmc.OracleDate(apoddt))
                    If dt2.Rows.Count > 0 Then
                        If sout = "" Then sout = sout & vbCrLf
                        sout = sout & "<RECORD BRIG=""" & atn & """ POD=""" & apod & """ WP=""" & dt2.Rows(0)("WP") & """ QWORKERS=""" & dt2.Rows(0)("QWORKERS").ToString & """ STATUSID=""" & dt2.Rows(0)("STATUSID").ToString & """ OPPRC=""" & dt2.Rows(0)("OPPRC").ToString & """ BRK=""" & dt2.Rows(0)("BRK").ToString & """ />"
                    End If

                Next
            End If
            Return sout & "</data>"
        Catch ex As Exception
            LogString(ex.Message)
            Return sout & "</data>"
        End Try
    End Function

End Class