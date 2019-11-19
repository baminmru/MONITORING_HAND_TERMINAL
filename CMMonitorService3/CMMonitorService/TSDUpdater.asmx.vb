Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.IO

<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class TSDUpdater
    Inherits System.Web.Services.WebService

    Private LogPath As String = "E:\Projects\CM\log\CMMLOG"



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

    <WebMethod()> _
    Public Function IsLastVersion(ByVal Version As Integer, ByVal subversion As Integer, ByVal TSDModel As String) As Boolean
        Dim cmc As CMConnector
        cmc = New CMConnector
        Dim dt As DataTable
        Try
            dt = cmc.QuerySelect("select max(APPVERSION * 100000 + APPSUBVERSION) topver from CMMON2_APPVERSIONS where TSDMODEL='" + TSDModel + "'")
        Catch ex As Exception
            dt = Nothing
        End Try
        If Not dt Is Nothing Then
            If dt.Rows.Count > 0 Then
                If TypeName(dt.Rows(0)("topver")) <> "DBNull" Then
                    If dt.Rows(0)("topver") > Version * 100000 + subversion Then
                        Return False
                    End If
                End If
            End If
        End If

        Return True
    End Function

    <WebMethod()> _
    Public Function GetCurrentTime() As DateTime
        Dim cmc As CMConnector
        cmc = New CMConnector
        Dim curdate As DateTime
        curdate = DateTime.Now
        Dim dt As DataTable
        Try
            dt = cmc.QuerySelect("select sysdate curdate  from dual")
        Catch ex As Exception
            dt = Nothing
        End Try
        If Not dt Is Nothing Then
            If dt.Rows.Count > 0 Then
                If TypeName(dt.Rows(0)("curdate")) <> "DBNull" Then
                    curdate = dt.Rows(0)("curdate")

                End If
            End If
        End If
        Return curdate
    End Function


    <WebMethod()> _
    Public Function GetLastVersion(ByVal TSDModel As String) As String
        Dim sOut As String
        Dim tv As Long
        Dim cmc As CMConnector
        cmc = New CMConnector
        Dim dt As DataTable
        Try
            dt = cmc.QuerySelect("select max(APPVERSION * 100000 + APPSUBVERSION) topver from CMMON2_APPVERSIONS where TSDMODEL='" + TSDModel + "'")

        Catch ex As Exception
            dt = Nothing
        End Try
        If Not dt Is Nothing Then
            If dt.Rows.Count > 0 Then
                If TypeName(dt.Rows(0)("topver")) <> "DBNull" Then
                    tv = dt.Rows(0)("topver")
                    Try
                        dt = cmc.QuerySelect("select APPVERSION,APPSUBVERSION from CMMON2_APPVERSIONS where APPVERSION * 100000 + APPSUBVERSION =" + tv.ToString + " and TSDMODEL='" + TSDModel + "'")
                    Catch ex As Exception
                        dt = Nothing
                    End Try
                    If Not dt Is Nothing Then
                        If dt.Rows.Count > 0 Then
                            sOut = dt.Rows(0)("APPVERSION").ToString + "." + dt.Rows(0)("APPSUBVERSION").ToString
                            Return sOut
                        End If
                    End If
                End If
            End If
        End If

        Return "2.0"
    End Function

    ' Метод возвращает файл с последней версией софта для терминала этой модели
    <WebMethod()> _
    Public Function GetCab(ByVal TSDModel As String, ByVal Terminalid As String) As Byte()

        Dim fi As FileInfo
        Dim zarr(0 To 0) As Byte
        Dim tv As Long
        Dim cmc As CMConnector
        cmc = New CMConnector
        LogPath = cmc.LogPath
        Dim dt As DataTable
        Try
            dt = cmc.QuerySelect("select max(APPVERSION * 100000 + APPSUBVERSION) topver from CMMON2_APPVERSIONS where TSDMODEL='" + TSDModel + "'")

        Catch ex As Exception
            dt = Nothing
        End Try
        If Not dt Is Nothing Then
            If dt.Rows.Count > 0 Then
                If TypeName(dt.Rows(0)("topver")) <> "DBNull" Then
                    tv = dt.Rows(0)("topver")
                    Try
                        dt = cmc.QuerySelect("select FILEPATH from CMMON2_APPVERSIONS where APPVERSION * 100000 + APPSUBVERSION =" + tv.ToString + " and TSDMODEL='" + TSDModel + "'")
                    Catch ex As Exception
                        dt = Nothing
                    End Try
                    If Not dt Is Nothing Then
                        If dt.Rows.Count > 0 Then
                            fi = New FileInfo(dt.Rows(0)("FILEPATH"))
                            If fi.Exists Then
                                Try
                                    Dim oFileStream As System.IO.FileStream = fi.OpenRead()
                                    Dim lBytes As Long = oFileStream.Length

                                    If (lBytes > 0) Then
                                        Dim fileData(lBytes - 1) As Byte
                                        oFileStream.Read(fileData, 0, lBytes)
                                        oFileStream.Close()
                                        LogString("Terminal:" + Terminalid + " (" + TSDModel + "). Get last version " + tv.ToString)
                                        Return fileData
                                    End If
                                Catch ex As Exception
                                    Return zarr
                                End Try
                            End If
                        End If
                    End If
                End If
            End If
        End If

        Return zarr
    End Function





    ' Метод возвращает файл с последней версией софта для терминала этой модели
    <WebMethod()> _
    Public Function GetLib(ByVal LibName As String) As Byte()
        Dim fi As FileInfo
        Dim zarr(0 To 0) As Byte
        Dim tv As Long
        Dim cmc As CMConnector
        cmc = New CMConnector
        LogPath = cmc.LogPath

     
        fi = New FileInfo(cmc.LibPath & LibName)
        If fi.Exists Then
            Try
                Dim oFileStream As System.IO.FileStream = fi.OpenRead()
                Dim lBytes As Long = oFileStream.Length

                If (lBytes > 0) Then
                    Dim fileData(lBytes - 1) As Byte
                    oFileStream.Read(fileData, 0, lBytes)
                    oFileStream.Close()
                    Return fileData
                End If
            Catch ex As Exception
                Return zarr
            End Try
        End If
        Return zarr
    End Function


    ' Метод возвращает файл с последней версией софта для терминала этой модели
    <WebMethod()> _
    Public Function GetLibs() As String
        Dim fi As FileInfo
        Dim cmc As CMConnector
        cmc = New CMConnector
        Dim di As DirectoryInfo
        Dim sout As String
        di = New DirectoryInfo(cmc.LibPath)
        sout = ""
        For Each fi In di.GetFiles()
            sout = fi.Name & ";" & sout
        Next
        Return sout
    End Function



End Class