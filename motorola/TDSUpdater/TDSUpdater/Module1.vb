Imports System.Data

Imports System.IO
Imports System.Reflection
Imports System.Xml
Imports System.Net
Imports System.Collections
Imports System.Diagnostics
Imports ProcessCE



Module Module1

Public sVersion As String
Public sSubversion As String
Public sModel As String
Public ServiceURL As String
Public svcVar As TDSUpdater.TDSUpdaterService.TSDUpdater
Public srvVersion As Integer
Public srvSubversion As Integer
Public myVersion As Integer
Public mySubversion As Integer
    Public needUpdate As Boolean = False
    Public Terminalid As String = "unknown"
    Public strAppDir As String
    Private Sub CheckWLAN()

        Dim myWlan As Symbol.Fusion.WLAN.WLAN
        Dim myAdapter As Symbol.Fusion.WLAN.Adapter
        Try
            myWlan = New Symbol.Fusion.WLAN.WLAN()
            Dim i As Integer
            Dim WLAN_ok As Boolean
            WLAN_ok = False
            For i = 0 To myWlan.Adapters.Length - 1
                myAdapter = myWlan.Adapters(i)
                If myAdapter.PowerState = Symbol.Fusion.WLAN.Adapter.PowerStates.ON Then
                    WLAN_ok = True
                End If
            Next

            If Not WLAN_ok Then
                For i = 0 To myWlan.Adapters.Length - 1
                    myAdapter = myWlan.Adapters(i)
                    Try
                        myAdapter.PowerState = Symbol.Fusion.WLAN.Adapter.PowerStates.ON
                    Catch ex As Exception

                    End Try
                Next

                Dim cnt As Integer
                cnt = 0
                While cnt < 50
                  System.Threading.Thread.Sleep(100)
                  Application.DoEvents()
                  cnt = cnt + 1
                End While
            End If
            myWlan.Dispose()
        Catch ex As Exception

        End Try

    End Sub
Public Function ReadCFG() As Boolean

        strAppDir = Path.GetDirectoryName( _
         Assembly.GetExecutingAssembly().GetName().CodeBase)
        Dim strFullPathToMyFile As String = Path.Combine(strAppDir, "UpdaterCFG.xml")

        Dim xml As XmlDocument
        xml = New XmlDocument
        Try
            xml.Load(strFullPathToMyFile)
            Dim node As XmlElement
            node = xml.LastChild()
            sVersion = Integer.Parse(node.Attributes.GetNamedItem("Version").Value.ToString)
            sSubversion = (node.Attributes.GetNamedItem("Subversion").Value)
            sModel = (node.Attributes.GetNamedItem("Model").Value)
            ServiceURL = (node.Attributes.GetNamedItem("ServiceURL").Value)

        Catch
            Return False
        End Try

        
     
        Return True
    End Function

 Public Function svc() As TDSUpdater.TDSUpdaterService.TSDUpdater
        If Not svcVar Is Nothing Then
            Return svcVar
        Else
            svcVar = New TDSUpdater.TDSUpdaterService.TSDUpdater(ServiceURL)
            svcVar.Timeout = 5000
            Return svcVar
        End If
    End Function

    Declare Function setsystemtime Lib "kernel32.dll" ()
  Sub Main()
      If Not ReadCFG() Then
        MsgBox("Не найден файл конфигурации")
        Return
        End If
        CheckWLAN()
      Dim sVer As String
        Dim sverArr() As String
        Dim srvdate As DateTime
        Try
            srvdate = svc.GetCurrentTime()
            STime.SetMyTime(srvdate)

        Catch ex As Exception

        End Try
    
      Try
        sVer = svc.GetLastVersion(sModel)
        sverArr = Split(sVer, ".")
        If sverArr.Length > 1 Then
          srvVersion = Integer.Parse(sverArr(0))
          srvSubversion = Integer.Parse(sverArr(1))
          myVersion = Integer.Parse(sVersion)
          mySubversion = Integer.Parse(sSubversion)
          If myVersion < srvVersion Or (myVersion = srvVersion And mySubversion < srvSubversion) Then
            needUpdate = True
          End If
        End If

      Catch ex As Exception
            MsgBox("GetLastVersion ->" + ex.Message)

          needUpdate = False
      End Try

      If needUpdate Then
         UpdateTDS()
      Else
          RestartProcess()
      End If
  End Sub

    Sub RestartProcess()

          Dim processname As String


        If sModel = "motorolla" Then
            processname = "motorollaTSD2.exe"
        Else
            processname = "datalogicTSD2.exe"
        End If

        Dim list() As ProcessInfo = ProcessCE.GetProcesses()
        'Dim prc As Process
        Dim processStarted As Boolean = False

            ' kill working process
            For Each pinfo As ProcessInfo In list
            If pinfo.FullPath.EndsWith(processname) Then
                processStarted = True
                'prc = System.Diagnostics.Process.GetProcessById(pinfo.Pid)

                'prc.Refresh()
                'If Not prc.HasExited Then
                '    Try
                '        prc.CloseMainWindow()
                '        prc.WaitForExit(5000)
                '    Catch ex As Exception
                '        'MsgBox("Не удалось закрыть приложение")
                '        Return
                '    End Try

                'End If

            End If
            Next
        If Not processStarted Then
            Dim strAppDir As String = Path.GetDirectoryName( _
            Assembly.GetExecutingAssembly().GetName().CodeBase)
            Dim sPath As String = Path.Combine(strAppDir, processname)


            Try
                System.Diagnostics.Process.Start(sPath, "")
            Catch ex As Exception
                'MsgBox(ex.Message)
            End Try
        End If



    End Sub
      Sub UpdateTDS()
          Dim fileBytes() As Byte = Nothing
          Dim processname As String

          

        If sModel = "motorolla" Then
            processname = "motorollaTSD2.exe"
        Else
            processname = "datalogicTSD2.exe"
        End If

        Dim prc As Process = System.Diagnostics.Process.GetCurrentProcess

        Dim list() As ProcessInfo = ProcessCE.GetProcesses()

        ' kill working process
        For Each pinfo As ProcessInfo In list
            prc = System.Diagnostics.Process.GetProcessById(pinfo.Pid)
            If pinfo.FullPath.EndsWith(processname) Then
                prc.Refresh()
                If Not prc.HasExited Then
                    Try
                        prc.CloseMainWindow()
                        prc.WaitForExit(5000)
                    Catch ex As Exception
                        MsgBox("Не удалось закрыть приложение")
                        Return
                    End Try

                End If

            End If
        Next

        Dim strFullPathToMyFile As String = Path.Combine(strAppDir, "CmConfig.xml")


        Dim xml As XmlDocument = New XmlDocument
        Try
            'Dim sxml As String
            xml.Load(strFullPathToMyFile)
            Dim node As XmlElement
            node = xml.LastChild()
            Terminalid = Integer.Parse(node.Attributes.GetNamedItem("TerminalID").Value.ToString)
        Catch

        End Try


        Try
            fileBytes = svc.GetCab(sModel, Terminalid)
        Catch ex As Exception
            MsgBox("GetCab ->" + ex.Message)
            Return
        End Try
        If Not fileBytes Is Nothing Then
            Dim strAppDir As String = Path.GetDirectoryName( _
            Assembly.GetExecutingAssembly().GetName().CodeBase)
            Dim sPath As String = Path.Combine(strAppDir, processname)
            Dim sPathnew As String = Path.Combine(strAppDir, "new_" + processname)

            Dim sw As FileStream

            Try
                System.IO.File.Delete(sPathnew)
            Catch ex As Exception
                MsgBox("Не удалось удалить файл:" + sPathnew)
            End Try


            Try
                sw = File.Create(sPathnew, fileBytes.Length)
                sw.Write(fileBytes, 0, fileBytes.Length)
                sw.Flush()
                sw.Close()
            Catch ex As Exception
                MsgBox("Не удалось создать файл:" + sPathnew)
                Return
            End Try


            'Try
            '    System.IO.File.Delete(sPath)
            'Catch ex As Exception
            '    MsgBox("Не удалось удалить файл:" + sPath)
            'End Try


            Try
                System.IO.File.Copy(sPathnew, sPath, True)
            Catch ex As Exception
                MsgBox("Не удалось скопировать файл:" + sPathnew + " в " + sPath)
                Return
            End Try




            strFullPathToMyFile = Path.Combine(strAppDir, "UpdaterCFG.xml")


            xml = New XmlDocument
            Try
                xml.Load(strFullPathToMyFile)
                Dim node As XmlElement
                node = xml.LastChild()
                node.Attributes.GetNamedItem("Version").Value = srvVersion.ToString
                node.Attributes.GetNamedItem("Subversion").Value = srvSubversion.ToString
                xml.Save(strFullPathToMyFile)

            Catch
                MsgBox("Не удалось сохранить номер новой версии")
            End Try

            Try
                System.Diagnostics.Process.Start(sPath, "")
            Catch ex As Exception
                MsgBox("Не удалось запустить программу мониторинг")
            End Try

        End If

      End Sub
End Module
