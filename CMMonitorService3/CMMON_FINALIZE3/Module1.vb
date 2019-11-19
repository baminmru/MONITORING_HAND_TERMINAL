Imports System.Xml
Imports System.Net
Imports System.IO
Imports System.Text
Module Module1
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
            'Console.WriteLine(s)
            Dim d As DateTime
            d = Date.Now
            File.AppendAllText(LogPath & d.Year.ToString() & "_" & d.Month.ToString & "_" & d.Day.ToString & ".txt", Date.Now.ToString("dd MMM HH:mm:ss") & "> CMMON_FINALIZE3:" & s & vbCrLf)
        Catch ex As Exception

        End Try

    End Sub

    Private Function fdate(ByVal d As Date) As String
        Dim s As String
        s = d.ToString("yyyy-MM-dd")
        Return s
    End Function

    Private Function ftime(ByVal d As Date) As String
        Dim s As String
        s = d.ToString("HH:mm:ss")
        Return s
    End Function

    Sub Main(ByVal args() As String)
        Dim termid As String = ""
        Dim pod As String = ""
        Dim ftype As String = ""
        Dim prc As String = "100"
        Dim brig As String = ""
        Dim i As Integer
        Dim sapurl As String
        Dim sapuser As String
        Dim sappassword As String
        Dim subrc As String
        Dim ocher As String = ""
        Dim prich As String
        Dim tn As String = ""
        Dim tt As Long
        For i = args.GetLowerBound(0) To args.GetUpperBound(0)
            If args(i).StartsWith("T=") Then
                termid = args(i).Substring(2)
            End If
            If args(i).StartsWith("P=") Then
                pod = args(i).Substring(2)
            End If

            If args(i).StartsWith("F=") Then
                ftype = args(i).Substring(2)
            End If
            If args(i).StartsWith("D=") Then
                prc = args(i).Substring(2)
            End If
            If args(i).StartsWith("B=") Then
                brig = args(i).Substring(2)
            End If

            If args(i).StartsWith("C=") Then
                tt = Long.Parse(args(i).Substring(2))
            End If
        Next
        If termid <> "" And pod <> "" And ftype <> "" Then

            Dim xml As XmlDocument
            xml = New XmlDocument
            xml.Load(GetMyDir() + "\Config.xml")
            Dim node As XmlElement
            node = xml.LastChild()


            sapurl = node.Attributes.GetNamedItem("sapurl").Value
            sapuser = node.Attributes.GetNamedItem("sapuser").Value
            sappassword = node.Attributes.GetNamedItem("sappassword").Value
            LogPath = node.Attributes.GetNamedItem("logpath").Value
            tablename = node.Attributes.GetNamedItem("tablename").Value


            Dim cmc As CMConnector
            cmc = New CMConnector
            Dim dt As DataTable
            Dim sd As Date
            Dim cd As Date
            ''dt = cmc.QuerySelect("select sysdate cd from dual")
            ''d = dt.Rows(0)("cd")
            ''cd = cd.AddMinutes()
            cd = New Date(tt)

            dt = cmc.QuerySelect("select max(PODEVENTDATE) podeventdate from CMMON2_PODVRF  where TERMINALID=" + termid + " and POD='" + pod + "' and BRIG='" + brig + "'")
            If dt.Rows.Count > 0 Then
                Try
                    sd = dt.Rows(0)("podeventdate")
                Catch ex As Exception
                    sd = DateTime.Now().AddDays(-1)
                End Try

            Else
                sd = DateTime.Now().AddDays(-1)
            End If


            'dt = cmc.QuerySelect("select * from CMMON2_INFO  where TERMINALID=" + termid + " and eventdate in (select max(eventdate) from CMMON2_INFO where TERMINALID=" + termid + ")")
            'If dt.Rows.Count > 0 Then
            'tn = dt.Rows(0)("TN")
            'End If

            tn = brig

            Try
                dt = cmc.QuerySelect("select * from  CMMON2_PODINFO   where TERMINALID=" + termid + " and POD='" + pod + "' and BRIG='" + brig + "' and podeventdate in (select max(podeventdate) from CMMON2_PODINFO where TERMINALID=" + termid + " and POD='" + pod + "' and BRIG='" + brig + "')")
            Catch ex As Exception
                LogString(ex.Message)
            End Try

            Try
                cmc.QueryExec("update cmmon2_podvrf set OCHER=0,closemsg='???'  where TERMINALID=" + termid + " and POD='" + pod + "' and BRIG='" + brig + "' and podeventdate in (select max(podeventdate) from CMMON2_PODVRF where TERMINALID=" + termid + " and POD='" + pod + "' and BRIG='" + brig + "')")
            Catch ex As Exception
                LogString(ex.Message)
            End Try


            Dim addr As String
            addr = sapurl
            addr = addr + "&OutputParameter=Result"
            addr = addr + "&RUECK=" + pod
            addr = addr + "&INVNUM=" + termid
            If dt.Rows.Count > 0 Then
                addr = addr + "&KAPNAME=" + dt.Rows(0)("WP").ToString
            Else
                addr = addr + "&KAPNAME=" + "0"
            End If
            addr = addr + "&BRIGADE=" + tn
            If dt.Rows.Count > 0 Then
                addr = addr + "&ANZMA=" + dt.Rows(0)("QWORKERS").ToString
            Else
                addr = addr + "&ANZMA=" + "0"
            End If


            If ftype = "99" Then
                addr = addr + "&IPRZ1=0"
            ElseIf ftype = "98" Then
                If prc = "0" Then
                    addr = addr + "&IPRZ1="
                Else
                    addr = addr + "&IPRZ1=" + prc
                End If
            End If

                If dt.Rows.Count > 0 Then
                    addr = addr + "&XMNGA=" + dt.Rows(0)("BRK").ToString
                Else
                    addr = addr + "&XMNGA=0"
                End If

                If dt.Rows.Count > 0 Then
                    If dt.Rows(0)("LMNGA") <= 0 Then
                        addr = addr + "&LMNGA="
                    Else
                        addr = addr + "&LMNGA=" + dt.Rows(0)("LMNGA").ToString
                    End If

                Else
                    addr = addr + "&LMNGA="
                End If


                addr = addr + "&ISDD=" + fdate(sd)
                addr = addr + "&ISDZ=" + ftime(sd)
                addr = addr + "&IEDD=" + fdate(cd)
                addr = addr + "&IEDZ=" + ftime(cd)
                If ftype = "99" Then
                    addr = addr + "&PDTV=1"
                ElseIf ftype = "98" Then
                    addr = addr + "&PDTV=2"
                End If



                Dim wc As System.Net.HttpWebRequest
                Dim sout As String = ""
                wc = WebRequest.Create(addr)
                Dim authInfo As String = sapuser + ":" + sappassword
                authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo))
                wc.Headers("Authorization") = "Basic " + authInfo

                LogString("SAP send Call:" + vbCrLf + " Query:" + addr + vbCrLf)

                Dim myCred As New NetworkCredential(sapuser, sappassword)
                wc.UseDefaultCredentials = False
                wc.Credentials = myCred
                Dim wr As WebResponse

                Try
                    wr = wc.GetResponse()
                Catch ex As Exception
                    wr = Nothing
                    LogString("SAP Call error." + vbCrLf + " Query:" + addr + vbCrLf + " Err:" + ex.Message)
                End Try

                If Not wr Is Nothing Then
                    Dim receiveStream As Stream = Nothing
                    Try
                        receiveStream = wr.GetResponseStream()
                    Catch ex As Exception
                        receiveStream = Nothing
                        LogString("SAP Call error. Bad stream." + vbCrLf + " Query:" + addr + vbCrLf + " Err:" + ex.Message)
                    End Try
                    sout = ""
                    If Not receiveStream Is Nothing Then
                        Try
                            Dim readStream As New StreamReader(receiveStream, Encoding.UTF8)

                            sout = readStream.ReadToEnd()
                            wr.Close()
                            readStream.Close()
                        Catch ex As Exception
                            LogString("SAP Call error. Error while reading stream." + vbCrLf + " Query:" + addr + vbCrLf + " Err:" + ex.Message)
                        End Try

                        If sout <> "" Then
                            Dim xmlOut As XmlDocument
                            xmlOut = New XmlDocument
                            Dim nodesOut As XmlNodeList = Nothing
                            Try
                                xmlOut.LoadXml(sout)

                                nodesOut = xmlOut.GetElementsByTagName("Result")
                            Catch ex As Exception
                                LogString("SAP Call error. Error while parsing XML." + vbCrLf + " Query:" + addr + " XML:" + sout + vbCrLf + " Err:" + ex.Message)
                                nodesOut = Nothing
                            End Try

                            If Not nodesOut Is Nothing Then
                                If nodesOut.Count > 0 Then
                                    Dim xmlRes As XmlDocument
                                    xmlRes = New XmlDocument
                                    Dim nodesRes As XmlNodeList = Nothing
                                    Try
                                    xmlRes.LoadXml(nodesOut.Item(0).InnerText)
                                    nodesRes = xmlRes.GetElementsByTagName("PRICH")
                                    prich = nodesRes.Item(0).InnerText
                                    nodesRes = xmlRes.GetElementsByTagName("SUBRC")
                                    subrc = nodesRes.Item(0).InnerText
                                    nodesRes = xmlRes.GetElementsByTagName("OCHER")
                                    ocher = nodesRes.Item(0).InnerText

                                    If subrc = "000000" Then
                                        Try
                                            cmc.QueryExec("update cmmon2_podvrf set OCHER=0,closemsg=''  where TERMINALID=" + termid + " and POD='" + pod + "' and BRIG='" + brig + "' and podeventdate in (select max(podeventdate) from CMMON2_PODVRF where TERMINALID=" + termid + " and POD='" + pod + "' and BRIG='" + brig + "')")
                                        Catch ex As Exception
                                            LogString(ex.Message)
                                        End Try
                                    Else
                                        If ocher = "1" Then

                                            Try
                                                cmc.QueryExec("update cmmon2_podvrf set OCHER=1,closemsg='" + prich + "'  where TERMINALID=" + termid + " and POD='" + pod + "' and BRIG='" + brig + "' and podeventdate in (select max(podeventdate) from CMMON2_PODVRF where TERMINALID=" + termid + " and POD='" + pod + "' and BRIG='" + brig + "')")
                                            Catch ex As Exception
                                                LogString(ex.Message)
                                            End Try
                                        End If

                                        If subrc = "000001" Then

                                            Try
                                                cmc.QueryExec("update cmmon2_podvrf set OCHER=0,closemsg='" + prich + "'  where TERMINALID=" + termid + " and POD='" + pod + "' and BRIG='" + brig + "' and podeventdate in (select max(podeventdate) from CMMON2_PODVRF where TERMINALID=" + termid + " and POD='" + pod + "' and BRIG='" + brig + "')")
                                            Catch ex As Exception
                                                LogString(ex.Message)
                                            End Try
                                        End If
                                    End If


                                    

                                        ' nodesRes = xmlRes.GetElementsByTagName("OCHER")
                                        'ocher = nodesRes.Item(0).InnerText
                                    Catch ex As Exception
                                        LogString("SAP Call error. Error while parsing Result XML." + vbCrLf + " XML:" + nodesOut.Item(0).InnerText + vbCrLf + " Err:" + ex.Message)

                                        subrc = ""
                                        prich = ""
                                    End Try
                                    If subrc <> "" Then
                                        If Integer.Parse(subrc) = 0 Then
                                            If ftype = "99" Then
                                                SaveToSAP(cmc, Integer.Parse(termid), pod, brig, "Izd", "99999999", cd, 0)
                                            End If
                                            If ftype = "98" Then
                                                SaveToSAP(cmc, Integer.Parse(termid), pod, brig, "Izd", "99999998", cd, 0)
                                            End If
                                            '    cmc.QueryExec("update CMMON2_PODVRF set PROCESSSTATUS =1 where PROCESSSTATUS=0 and terminalid=" + termid + " and POD='" + pod + "'")

                                            '    SaveToSAP(cmc, Integer.Parse(termid), pod, "TabNo", Integer.Parse(tn), cd, 0)
                                            LogString("SAP Finalize OK. Query:" + addr)

                                        Else
                                            '    cmc.QueryExec("update CMMON2_PODVRF set PROCESSSTATUS =2, PROCESSMSG='" + prich + "' where PROCESSSTATUS=0 and terminalid=" + termid + " and POD='" + pod + "'")
                                            '    SaveToSAP(cmc, Integer.Parse(termid), pod, "Izd", 1, cd, 1)
                                        LogString("SAP Finalize REJECT. Query:" + addr + "SUBRC:" + subrc + " OCHER:" + ocher + " PRICH:" + prich)
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
        Else
            LogString("Wrong arguments")
        End If

    End Sub

    Private Function SaveToSAP(ByVal cmc As CMConnector, ByVal terminalid As Integer, ByVal RUECK As String, ByVal BRIG As String, ByVal VAR_NAME As String, ByVal VAR_VAL As Integer, ByVal VAR_DATE As Date, ByVal PR As Integer) As Boolean
        Try
            cmc.QueryExec("INSERT into " + tablename + " (INVN,RUECK,VAR_NAME,VAR_VAL,var_DATE,PR,BRIG) values(" + terminalid.ToString() + "," + RUECK + ",'" + VAR_NAME + "'," + VAR_VAL.ToString + "," + cmc.OracleDate(VAR_DATE) + ",null,'" + BRIG + "')")
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

End Module
