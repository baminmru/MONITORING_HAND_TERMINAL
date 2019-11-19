Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Configuration

<System.Web.Services.WebService(Namespace:="http://www.microsoft.com/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class Service1
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function GetCurrentStatus(ByVal TerminalID As Integer) As String
        Dim cmc As CMConnector
        cmc = New CMConnector
        Dim dt As DataTable
        dt = cmc.QuerySelect("select CMMON_STATUSNAME.STATUSID ||' ' || CMMON_STATUSNAME.NAME SNAME from CMMON_STATUS  join CMMON_STATUSNAME on CMMON_STATUS.STATUS=CMMON_STATUSNAME.STATUSID where TERMINALID=" + TerminalID.ToString)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)("SNAME")
        Else
            cmc.QueryExec("insert into CMMON_STATUS(STATUS,TERMINALID,REGDATE,STATUSDATE,SHCODEDATE)values(0," + TerminalID.ToString + ",SYSDATE,SYSDATE,SYSDATE)")
            Return "0"
        End If

    End Function


    <WebMethod()> _
    Public Function GetCurrentOP(ByVal TerminalID As Integer) As String
        Dim cmc As CMConnector
        cmc = New CMConnector
        Dim dt As DataTable
        dt = cmc.QuerySelect("select SHCODE from CMMON_STATUS where TERMINALID=" + TerminalID.ToString)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)("SHCODE") & ""
        Else
            cmc.QueryExec("insert into CMMON_STATUS(STATUS,TERMINALID,REGDATE,STATUSDATE,SHCODEDATE)values(0," + TerminalID.ToString + ",SYSDATE,SYSDATE,SYSDATE)")
            Return ""
        End If

    End Function

    <WebMethod()> _
    Public Sub Ping(ByVal TerminalID As Integer, ByVal Address As String)
        Dim cmc As CMConnector
        cmc = New CMConnector
        cmc.QueryExec("update CMMON_STATUS  set LASTPING=sysdate, IPaddr='" + Address + "' where TERMINALID=" + TerminalID.ToString)
     

    End Sub

    <WebMethod()> _
    Public Function GetMaxStatus() As Integer
        Dim cmc As CMConnector
        cmc = New CMConnector
        Dim dt As DataTable
        dt = cmc.QuerySelect("select max(STATUSID) CNT from CMMON_STATUSNAME ")
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)("CNT")
        Else
            Return 0
        End If
    End Function

  

    <WebMethod()> _
    Public Function GetStatusName(ByVal id As Integer) As String
        Dim cmc As CMConnector
        cmc = New CMConnector
        Dim dt As DataTable
        dt = cmc.QuerySelect("select NAME from CMMON_STATUSNAME where STATUSID=" + id.ToString)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)("NAME")
        Else
            Return "UNKNOWN"
        End If
    End Function

    <WebMethod()> _
    Public Function AddStatus(ByVal id As Integer, ByVal name As String) As Boolean
        Dim cmc As CMConnector
        cmc = New CMConnector
        Try
            cmc.QueryExec("insert into CMMON_STATUSNAME ( NAME,STATUSID) values('" + name + "'," + id.ToString + ")")
        Catch ex As Exception
            Return False
        End Try
        Return True

    End Function


    <WebMethod()> _
    Public Function GetOPInfo(ByVal OpCode As String) As String
        Dim cmc As CMConnector
        cmc = New CMConnector
        Dim dt As DataTable
        dt = cmc.QuerySelect("select LTXA1 from opc_mk_sap_all where RUECK='" + OpCode + "'")
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)("LTXA1")
        Else
            Return "UNKNOWN"
        End If
    End Function

    <WebMethod()> _
    Public Function RegisterOP(ByVal TerminalID As String, ByVal NewOperation As String, ByVal Tnum As String) As Boolean
        Dim cmc As CMConnector
        cmc = New CMConnector



        Try
            cmc.QueryExec("insert into CMMON_INFO (TERMINALID,EVENTDATE,SHCODE,TNUM) values(" + TerminalID.ToString + ",sysdate,'" + NewOperation + "','" + Tnum + "')")

        Catch ex As Exception
            Return False
        End Try

        Try
            cmc.QueryExec("update CMMON_STATUS set SHCODEDATE=SYSDATE, LASTPING=sysdate, SHCODE='" + NewOperation + "' where TERMINALID=" + TerminalID.ToString)

        Catch ex As Exception
            Return False
        End Try
        Dim dt As DataTable
        dt = cmc.QuerySelect("select * from CMMON_STATUS where TERMINALID=" + TerminalID.ToString)
        If dt.Rows.Count > 0 Then

            Dim s As String = ""

            s = s + "insert into OPC_LMZ("

            s = s + "STANOK       "
            s = s + ",DATE_SO      "
            s = s + ",SO           "
            s = s + ",DATE_SA      "
            s = s + ",KOD          "
            s = s + ",DATE_KOD     "
            s = s + ",QO           "
            s = s + ",SA           "
            s = s + ",KOL_SIM"
            s = s + ") values("
            s = s + "'<" + dt.Rows(0)("IPADDR") + ">.2'"
            s = s + "," + cmc.OracleDate(dt.Rows(0)("STATUSDATE"))
            s = s + "," + dt.Rows(0)("STATUS").ToString
            s = s + "," + cmc.OracleDate(dt.Rows(0)("REGDATE"))
            s = s + ",'" + dt.Rows(0)("SHCODE") + "'"
            s = s + "," + cmc.OracleDate(dt.Rows(0)("SHCODEDATE"))
            s = s + ",192"
            s = s + ",0"
            s = s + ",1"
            s = s + ")"
            Try
                cmc.QueryExec(s)
            Catch ex As Exception

            End Try


        End If

        Return True
    End Function


    <WebMethod()> _
    Public Function RegisterState(ByVal TerminalID As String, ByVal NewStatus As Integer, ByVal Tnum As String) As Boolean
        Dim cmc As CMConnector
        cmc = New CMConnector


       

        Try
            cmc.QueryExec("insert into CMMON_INFO (TERMINALID,STATUSID,EVENTDATE,TNUM) values(" + TerminalID.ToString + "," + NewStatus.ToString + ",sysdate,'" + Tnum + "')")

        Catch ex As Exception
            Return False
        End Try

        Try
            cmc.QueryExec("update CMMON_STATUS set LASTPING=sysdate, STATUSDATE=SYSDATE, STATUS =" + NewStatus.ToString + " where TERMINALID=" + TerminalID.ToString)

        Catch ex As Exception
            Return False
        End Try
        Dim dt As DataTable
        dt = cmc.QuerySelect("select * from CMMON_STATUS where TERMINALID=" + TerminalID.ToString)
        If dt.Rows.Count > 0 Then

            Dim s As String = ""

            s = s + "insert into OPC_LMZ("

            s = s + "STANOK       "
            s = s + ",DATE_SO      "
            s = s + ",SO           "
            s = s + ",DATE_SA      "
            s = s + ",KOD          "
            s = s + ",DATE_KOD     "
            s = s + ",QO           "
            s = s + ",SA           "
            s = s + ",KOL_SIM"
            s = s + ") values("
            s = s + "'<" + dt.Rows(0)("IPADDR") + ">.2'"
            s = s + "," + cmc.OracleDate(dt.Rows(0)("STATUSDATE"))
            s = s + "," + dt.Rows(0)("STATUS").ToString
            s = s + "," + cmc.OracleDate(dt.Rows(0)("REGDATE"))
            s = s + ",'" + dt.Rows(0)("SHCODE") + "'"
            s = s + "," + cmc.OracleDate(dt.Rows(0)("SHCODEDATE"))
            s = s + ",192"
            s = s + ",0"
            s = s + ",1"
            s = s + ")"
            Try
                cmc.QueryExec(s)
            Catch ex As Exception

            End Try


        End If

        Return True
    End Function


End Class