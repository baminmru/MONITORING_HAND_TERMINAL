Imports System.Data.Common
Imports System.Data
Imports System.Data.SQLite




Public Class SqLite

    ''''''''''''''''''''''''''''''''''''''''''запись данных об операциях в БД SQLite''''''''''''''''''''''''''''''''''

    Public Function RegisterInfo(ByVal TerminalID As String, ByVal opdate As Date) As Boolean
        Dim cmc As ConnectorLite
        cmc = New ConnectorLite

        Try
            'cmc.QueryExecLite("insert into CMMON2_INFO (TERMINAlID,EVENTDATE) values('" + TerminalID.ToString + "','" + Date.Now.ToString + "')")
            'cmc.QueryExecLite("insert into CMMON2_INFO (TERMINAlID,EVENTDATE) values('" + TerminalID.ToString + "','" + Date.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "')")
            cmc.QueryExecLite("insert into CMMON2_INFO (TERMINAlID,EVENTDATE) values('" + TerminalID.ToString + "',?)", opdate)
        Catch ex As Exception
            MsgBox("Информация об операции не записана", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Ошибка")
            Return False
        End Try

        Return True
    End Function
    Public Function NewPod(ByVal TerminalID As String, ByVal POD As String, ByVal brig As String, ByVal opdate As Date) As Boolean
        Dim cmc As ConnectorLite
        cmc = New ConnectorLite
        Try
            'cmc.QueryExecLite("insert into CMMON2_PODVRF (TERMINALID,PODEVENTDATE,BRIG,POD,PROCESSSTATUS,PROCESSMSG) values(" + TerminalID.ToString + ",'" + opdate.ToString() + "','" + brig + "','" + POD + "','','')")
            cmc.QueryExecLite("insert into CMMON2_PODVRF (TERMINALID,PODEVENTDATE,BRIG,POD,PROCESSSTATUS,PROCESSMSG) values(" + TerminalID.ToString + ",?,'" + brig + "','" + POD + "','','')", opdate)
        Catch ex As Exception
            MsgBox("Информация об операции не записана", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Ошибка")
            Return False
        End Try

        Return True
    End Function
    Public Function RegisterPodInfo(ByVal TerminalID As String, ByVal BRIG As String, ByVal POD As String, ByVal OPdate As Date, ByVal WP As String, ByVal QWORKERS As Integer, ByVal STATUSID As Integer, ByVal opPRC As Integer, ByVal BRK As Integer, ByVal LMNGA As Integer, ByVal WPChangedL As Integer, ByVal QWORKERSChangedL As Integer, ByVal STATUSChangedL As Integer, ByVal opPRCChangedL As Integer, ByVal BRKChangedL As Integer, ByVal LMNGAChangedL As Integer) As Boolean
        Dim cmc As ConnectorLite
        cmc = New ConnectorLite
        Try
            'cmc.QueryExecLite("insert into  CMMON2_PODINFO(  TERMINALID ,BRIG, POD,  PODEVENTDATE,  WP ,  QWORKERS ,  STATUSID,  OPPRC,BRK, LMNGA,SAVED2SAP,WPChanged,QWORKERSChanged,STATUSChanged,opPRCChanged,BRKChanged,LMNGAChanged) values( " + TerminalID.ToString + " ,'" + BRIG + "','" + POD + "','" + OPdate.ToString + "','" + WP + "' ," + QWORKERS.ToString + " ,  " + STATUSID.ToString + ", " + opPRC.ToString + "," + BRK.ToString + "," + LMNGA.ToString + ",0," + WPChangedL.ToString + "," + QWORKERSChangedL.ToString + "," + STATUSChangedL.ToString + "," + opPRCChangedL.ToString + "," + BRKChangedL.ToString + "," + LMNGAChangedL.ToString + ")")
            cmc.QueryExecLite("insert into  CMMON2_PODINFO(  TERMINALID ,BRIG, POD,  PODEVENTDATE,  WP ,  QWORKERS ,  STATUSID,  OPPRC,BRK, LMNGA,SAVED2SAP,WPChanged,QWORKERSChanged,STATUSChanged,opPRCChanged,BRKChanged,LMNGAChanged) values( " + TerminalID.ToString + " ,'" + BRIG + "','" + POD + "',?,'" + WP + "' ," + QWORKERS.ToString + " ,  " + STATUSID.ToString + ", " + opPRC.ToString + "," + BRK.ToString + "," + LMNGA.ToString + ",0," + WPChangedL.ToString + "," + QWORKERSChangedL.ToString + "," + STATUSChangedL.ToString + "," + opPRCChangedL.ToString + "," + BRKChangedL.ToString + "," + LMNGAChangedL.ToString + ")", OPdate)
        Catch ex As Exception
            MsgBox("Информация об операции не записана", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Ошибка")
            Return False
        End Try
        Return True
    End Function
    ''''''''''''''''''''''''''''''''запись данных об операций из БД SQLite в Oracle и SAP ERP''''''''''''''''''''''''''''''''''''''''''''''''''

    Public Function LiteToOracl(ByVal terminalid As String) As Boolean
        Dim cmc As ConnectorLite
        cmc = New ConnectorLite
        Dim sout As String = ""
        Try
            Dim dt2 As DataTable
            Dim dt1 As DataTable
            Dim dt3 As DataTable
            dt1 = cmc.QuerySelectLite("SELECT rowid,* FROM CMMON2_INFO WHERE terminalid=" + terminalid.ToString + " ")
            dt2 = cmc.QuerySelectLite("SELECT rowid,* FROM CMMON2_PODINFO WHERE terminalid=" + terminalid.ToString + " ")
            dt3 = cmc.QuerySelectLite("SELECT rowid,* FROM CMMON2_PODVRF WHERE terminalid=" + terminalid.ToString + " ")
            Dim adt1 As Date
            Dim adt2 As Date
            Dim adt3 As Date
            Dim num1 As Integer
            Dim num2 As Integer
            Dim num3 As Integer
            Dim num As Integer
            Dim ok As Boolean
            num1 = dt1.Rows.Count
            num2 = dt2.Rows.Count
            num3 = dt3.Rows.Count
            num = num1 + num2 + num3
            ok = True
            ShowWindow(FindWindow("HHTaskBar", Nothing), 0)
            'Dim f As frmLiteToOrc
            'f = New frmLiteToOrc
            'f.WindowState = FormWindowState.Maximized
            'f.ShowDialog()
            If num1 > 0 Then
                
                For i = 0 To num1 - 1
                    If ok Then
                        'f.gettext(i + 1, num)
                        
                        adt1 = dt1.Rows(i)("EVENTDATE")
                        Try
                            If svc.RegisterInfo3(terminalid, adt1) Then
                                cmc.QueryDeLite("DELETE from CMMON2_INFO  WHERE rowid=" + dt1.Rows(i)("rowid").ToString + "")
                            Else
                                MsgBox("Данные записать не удалось", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "Передача данных на сервер")
                                ok = False
                            End If

                        Catch ex As Exception
                            MsgBox("данные начиная с " + (i + 1).ToString + " строки передать не удалось " + ex.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "Передача данных на сервер")
                            ok = False
                        End Try
                        'If i = num1 - 1 Or ok = False Then f.closeform()
                    End If
                Next
            End If

            If num3 > 0 Then
                
                For i = 0 To num3 - 1
                    If ok Then
                        'f.gettext(i + 1 + num1, num)
                        adt3 = dt3.Rows(i)("PODEVENTDATE")
                        Try
                            If svc.NewPod3(terminalid, dt3.Rows(i)("POD"), dt3.Rows(i)("BRIG"), adt3) Then
                                cmc.QueryDeLite("DELETE from CMMON2_PODVRF WHERE rowid=" + dt3.Rows(i)("rowid").ToString + "")
                            Else
                                MsgBox("Данные записать не удалось", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "Передача данных на сервер")
                                ok = False
                            End If

                        Catch ex As Exception
                            MsgBox("данные начиная с " + (i + 1 + num2 + num3).ToString + " строки передать не удалось " + ex.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "Передача данных на сервер")
                            ok = False
                        End Try
                        'If i = num3 - 1 Or ok = False Then f.closeform()
                    End If
                Next

            End If
            If num2 > 0 Then
                
                For i = 0 To num2 - 1
                    If ok Then
                        'f.gettext(i + 1 + num1 + num3, num)
                        adt2 = dt2.Rows(i)("PODEVENTDATE")
                        Try
                            If svc.RegisterPodInfo3(terminalid, dt2.Rows(i)("BRIG"), dt2.Rows(i)("POD"), adt2, dt2.Rows(i)("WP"), _
                                dt2.Rows(i)("QWORKERS"), dt2.Rows(i)("STATUSID"), dt2.Rows(i)("OPPRC"), dt2.Rows(i)("BRK"), dt2.Rows(i)("LMNGA"), dt2.Rows(i)("WPChanged"), dt2.Rows(i)("QWORKERSChanged"), dt2.Rows(i)("STATUSChanged"), dt2.Rows(i)("opPRCChanged"), dt2.Rows(i)("BRKChanged"), dt2.Rows(i)("LMNGAChanged")) Then
                                cmc.QueryDeLite("DELETE from CMMON2_PODINFO  WHERE rowid=" + dt2.Rows(i)("rowid").ToString + "")
                            Else
                                MsgBox("Данные записать не удалось", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "Передача данных на сервер")
                                ok = False
                            End If

                        Catch ex As Exception
                            MsgBox("данные начиная с " + (i + 1 + num1).ToString + " строки передать не удалось " + ex.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "Передача данных на сервер")
                            ok = False
                        End Try
                        'If i = num2 - 1 Or ok = False Then 'f.closeform()
                    End If
                Next

            End If

            'f.Close()

            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function


    Public Function GetTerminalOperations(ByVal terminalid As String) As String
        Dim cmc As ConnectorLite
        cmc = New ConnectorLite
        Dim sout As String = "<data>"
        Try

            Dim dt1 As DataTable
            Dim dt2 As DataTable
            ' dtcount = cmc.QuerySelectLite("SELECT * FROM CMMON2_INFO WHERE terminalid=" + terminalid.ToString + " ")
            'dt = cmc.QuerySelectLite("SELECT  max(eventdate) adt FROM CMMON2_INFO   WHERE terminalid=" + terminalid.ToString())
            Dim atn As String
            'Dim adt As String


            '''''!!!!! если нет максимального веремени, то adt NULL. одна строка в dt!!!!!!
            'If dt.Rows.Count > 0 Then
            '''''''
            'If dtcount.Rows.Count > 0 Then
            'adt = dt.Rows(0)("adt")
            Dim apod As String
            Dim apoddt As String

            Dim i As Integer

            'удалил and saved2sap=1



            'dt1 = cmc.QuerySelectLite("SELECT BRIG, pod, max(podeventdate) podeventdate FROM CMMON2_INFO JOIN CMMON2_PODINFO " & _
            '                      " ON   cmmon2_info.terminalid = CMMON2_PODINFO.terminalid    " & _
            '                      " AND STATUSID <98 WHERE  CMMON2_INFO.eventdate >'" + adt + "' " & _
            '                      " GROUP BY  pod ORDER BY pod ")
            '
            '???? а почему знак > перед adt???


            'WHERE  CMMON2_INFO.eventdate >'" + adt + "' " & 


            dt1 = cmc.QuerySelectLite("SELECT BRIG, pod, max(podeventdate) podeventdate FROM  CMMON2_PODINFO " & _
                                  " where    " & _
                                  " STATUSID <98 " & _
                                  " GROUP BY BRIG, pod ORDER BY pod ")

            For i = 0 To dt1.Rows.Count - 1

                apod = dt1.Rows(i)("pod")
                apoddt = dt1.Rows(i)("podeventdate")
                atn = dt1.Rows(i)("BRIG")


                'dt2 = cmc.QuerySelectLite("SELECT WP,QWORKERS,STATUSID,OPPRC,BRK,LMNGA  FROM CMMON2_INFO JOIN CMMON2_PODINFO " & _
                '                 " ON   cmmon2_info.terminalid = CMMON2_PODINFO.terminalid    " & _
                '                 " AND STATUSID <98 WHERE CMMON2_INFO.tn = '" + atn + "' And CMMON2_INFO.eventdate = '" + adt + "' " & _
                '                 " AND CMMON2_PODINFO.BRIG = '" + atn + "' And CMMON2_PODINFO.POD = '" + apod + "' And CMMON2_PODINFO.podeventdate = '" + apoddt + "' ")
                dt2 = cmc.QuerySelectLite("SELECT WP,QWORKERS,STATUSID,OPPRC,BRK,LMNGA  FROM CMMON2_PODINFO " & _
                                 " where  " & _
                                 " STATUSID <98 and " & _
                                 "  CMMON2_PODINFO.BRIG = '" + atn + "' And CMMON2_PODINFO.POD = '" + apod + "' And CMMON2_PODINFO.podeventdate = '" + apoddt + "' ")
                If dt2.Rows.Count > 0 Then
                    If sout = "" Then sout = sout & vbCrLf
                    sout = sout & "<RECORD BRIG=""" & atn & """ POD=""" & apod & """ WP=""" & dt2.Rows(0)("WP") & """ QWORKERS=""" & dt2.Rows(0)("QWORKERS").ToString & """ STATUSID=""" & dt2.Rows(0)("STATUSID").ToString & """ OPPRC=""" & dt2.Rows(0)("OPPRC").ToString & """ BRK=""" & dt2.Rows(0)("BRK").ToString & """ LMNGA=""" & dt2.Rows(0)("LMNGA").ToString & """ />"
                End If

            Next
            'End If
            Return sout & "</data>"
        Catch ex As Exception
            MsgBox(ex.Message)
            Return sout & "</data>"
        End Try
    End Function
End Class
