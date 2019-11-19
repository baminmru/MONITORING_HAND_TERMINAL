Imports System.Data

Imports System.IO
Imports System.Reflection
Imports System.Xml
Imports System.Net
Imports System.Collections
Imports System.Diagnostics
Imports ProcessCE



Module Moduletdsupdate

    Public svcUVar As datalogicTSD2.TSDUpdater3.TSDUpdater



    Public Function svcU() As datalogicTSD2.TSDUpdater3.TSDUpdater
        If Not svcUVar Is Nothing Then
            Return svcUVar
        Else
            svcUVar = New datalogicTSD2.TSDUpdater3.TSDUpdater
            svcUVar.Timeout = 5000
            Return svcUVar
        End If
    End Function

    Declare Function setsystemtime Lib "kernel32.dll" ()
   

    Sub UpdateLibs()
        Dim fileBytes() As Byte = Nothing

        Dim alllibs As String = ""
        Dim libs() As String
        Try
            alllibs = svcU.GetLibs()
        Catch ex As Exception
            alllibs = ""
        End Try

        If alllibs <> "" Then

            libs = alllibs.Split(";")


            Dim i As Integer
            For i = LBound(libs) To UBound(libs)
                If libs(i) <> "" Then
                    Try
                        fileBytes = svcU.GetLib(libs(i))
                    Catch ex As Exception
                        fileBytes = Nothing
                    End Try

                    If Not fileBytes Is Nothing Then
                        Dim sw As FileStream
                        Try
                            sw = File.Create("\Platform\monitoring\" + libs(i), fileBytes.Length)
                            sw.Write(fileBytes, 0, fileBytes.Length)
                            sw.Flush()
                            sw.Close()
                        Catch ex As Exception
                        End Try
                    End If



                End If


            Next
        End If
    End Sub
End Module
