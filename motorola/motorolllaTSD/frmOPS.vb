Imports Symbol
Imports Symbol.ResourceCoordination
Imports System.Threading
Imports System.IO
Imports System.Reflection
Imports System.Xml
Imports System.Data


Public Class frmOPS

   Private WithEvents m_Laser As Monitoring.BarcodeAPI
    Private IgnoreLaser As Boolean

 
 Private Sub ReloadPOD()
        lstOP.Items.Clear()
        txtInfo.Text = ""
        CleanPOD()
        Dim i As Integer
        Try
            For i = 0 To PODList.Count

                If Not PODList.Item(i).Deleted Then
                    lstOP.Items.Add(PODList.Item(i).POD + "(" + PODList.Item(i).BRIG + ")")
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CheckClosed()
        Dim i As Integer
        Dim cc As Boolean = False
        Try
            For i = 0 To PODList.Count
                If PODList.Item(i).Closing Then
                    If Math.Abs(DateDiff(DateInterval.Minute, PODList.Item(i).CloseDate, Now)) > 10 Then
                        PODList.Item(i).Closing = False
                        PODList.Item(i).Deleted = True
                        cc = True
                    End If
                End If
            Next
            If cc Then
                ReloadPOD()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub frmOPS_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.F1 Then
            CleanPOD()
            'If PODList.Count = 0 Then
            '    Dim f As frmTabNum
            '    f = New frmTabNum

            '    If f.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtOPCode.Text = ""
            txtBRIG.Text = ""
            ReloadPOD()
            '    End If
            '    f.Close()
            '    f = Nothing
            'End If
            
        End If
        If e.KeyCode = Keys.F2 Then
            If lstOP.SelectedIndex >= 0 Then
                Dim ps As PODStatus
                Dim pi As Integer

                pi = FindPOD(lstOP.Text)
                ps = Nothing
                If pi >= 0 Then
                    Try
                        ps = PODList.Item(pi)
                    Catch ex As Exception

                    End Try
                End If
                If Not ps Is Nothing Then
                    If MsgBox("Удалить код подтверждения: " + ps.POD + "(" + ps.BRIG + ") ?", MsgBoxStyle.YesNo, "Удаление") = MsgBoxResult.Yes Then
                        PODList.Remove(ps)
                        If Not NoNetworkMode Then
                            svc.NoloadPodInfo(TerminalID.ToString, ps.BRIG, ps.POD)
                        End If
                        ReloadPOD()
                        txtOPCode.Text = ""
                        txtBRIG.Text = ""
                    End If
                End If
            End If


        End If
    End Sub

  Private Sub frmOPS_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        m_Laser = myLaser
        IgnoreLaser = False
        ReloadPOD()
        txtOPCode.Focus()
        Me.Text = "Операции. T№" + TerminalID.ToString
        Me.txtBRIG.Text = ""
        Timer1.Enabled = True
  End Sub

    Private Sub HandleData(ByVal TheReaderData As Symbol.Barcode.ReaderData) Handles m_Laser.HandleData

        Dim s As String
        If Not IgnoreLaser Then

            s = TheReaderData.Text
            If s.Length = 8 Then
                txtBRIG.Text = ""
                txtOPCode.Text = s
            End If

            If s.Length = 7 Then
                txtBRIG.Text = s
            End If

        End If

    End Sub

    Private Sub frmOPS_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        If e.KeyChar = vbCr Then
            If lstOP.SelectedIndex >= 0 Then
                Dim ps As PODStatus
                Dim pi As Integer
                Dim msg As String
                pi = FindPOD(lstOP.Text)
                ps = Nothing
                If pi >= 0 Then
                    Try
                        ps = PODList.Item(pi)
                    Catch ex As Exception

                    End Try
                End If

                If Not ps Is Nothing Then

                    If Not NoNetworkMode Then
                        If ps.VerifyOK = False And (ps.ErrMsg = "" Or ps.ErrMsg = "Идет проверка") Then
                            Try
                                msg = svc.VerifyPod3(TerminalID, ps.POD, ps.BRIG)
                                If msg.StartsWith("OK") Then
                                    ps.VerifyOK = True
                                    ps.ErrMsg = msg.Replace("OK", "")
                                Else
                                    ps.VerifyOK = False
                                    ps.ErrMsg = msg
                                    If ps.ErrMsg <> "Идет проверка" Then
                                        ps.Deleted = True
                                    End If
                                End If
                            Catch ex As Exception
                                MsgBox("Ошибка при проверке кода операции")
                                If Not svcVar Is Nothing Then svcVar.Dispose()
                                svcVar = Nothing
                            End Try
                        End If
                    Else
                        '''''''''!!!!!!!!!!!!!!!!!!!
                        ps.VerifyOK = True
                    End If

                    If ps.Closing = False Then

                        If ps.VerifyOK = True Then
                            Dim f As frmOPData
                            f = New frmOPData
                            f.ps = ps
                            f.lblOP.Text = ps.POD
                            f.txtWP.Text = ps.WP
                            f.txtQ.Text = ps.Q.ToString
                            f.txtBRK.Text = ps.BRK.ToString
                            f.txtPRC.Text = ps.PRC.ToString
                            f.txtBRIG.Text = ps.BRIG
                            f.txtLMNGA.Text = ps.LMNGA.ToString

                            IgnoreLaser = True
                            f.ShowDialog()
                            f.Close()
                            f = Nothing
                            IgnoreLaser = False
                            ReloadPOD()
                            If lstOP.Items.Count > 0 Then
                                lstOP.Focus()
                            Else
                                txtOPCode.Focus()
                            End If

                        Else
                            If ps.ErrMsg <> "Идет проверка" Then
                                MsgBox(ps.ErrMsg, MsgBoxStyle.OkOnly, "Операция не подтверждена")
                                ps.Deleted = True
                            Else
                                MsgBox(ps.ErrMsg, MsgBoxStyle.OkOnly, "Ожидается ответ")
                            End If

                            ReloadPOD()
                        End If
                    Else
                        If ps.ErrMsg <> "" Then
                            MsgBox(ps.ErrMsg, MsgBoxStyle.OkOnly, "Завершение")
                        Else
                            MsgBox("Завершение", MsgBoxStyle.OkOnly, "Ожидается ответ")
                        End If
                    End If
                End If
            End If
        End If


        If e.KeyChar = Chr(System.Windows.Forms.Keys.Escape) Then
            Dim f As frmPass
            f = New frmPass
            If f.ShowDialog = Windows.Forms.DialogResult.OK Then
                IgnoreLaser = True
                m_Laser = Nothing
                FileUnLock()
                Me.Close()
            End If
        End If


    End Sub

    Private Sub frmOPS_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Timer1.Enabled = False


    End Sub

    Private pingCount As Integer = 0
    Private UpdaterCall As Boolean = False
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.Location = New Point(0, 0)



        Try

            Me.Text = "Операции. T№" + TerminalID.ToString + " (" + BatteryStrength().ToString + "%)"

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

   

        If NoNetworkMode Then
            lblMode.Text = "A"
        Else
            lblMode.Text = "C"
        End If

        pingCount += 1

        If pingCount = 10 Then
            CheckWLAN()
        End If
        If pingCount = 15 Then
            Try
                If Not Ping() Then
                    If Not svcVar Is Nothing Then svcVar.Dispose()
                    svcVar = Nothing
                End If
            Catch ex As Exception
                txtInfo.Text = "Сбой связи"
                If Not svcVar Is Nothing Then svcVar.Dispose()
                svcVar = Nothing
            End Try

            pingCount = 0
        End If

        CheckClosed()

        Dim mydate As Date
        mydate = Date.Now
        If UpdaterCall = False And mydate.Hour = 23 And mydate.Minute > 15 And mydate.Minute < 20 Then
            UpdaterCall = True
            Dim strAppDir As String = Path.GetDirectoryName( _
          Assembly.GetExecutingAssembly().GetName().CodeBase)
            Dim sPath As String = Path.Combine(strAppDir, "TDSUpdater.exe")


            Try
                System.Diagnostics.Process.Start(sPath, "")
            Catch ex As Exception
                'MsgBox(ex.Message)
            End Try
        End If

        If UpdaterCall And mydate.Hour = 23 And mydate.Minute > 20 Then
            UpdaterCall = False
        End If

    End Sub

    Private Sub RegOP()
        Dim i As Integer
        Dim s As String
        s = txtOPCode.Text + "(" + txtBRIG.Text + ")"

        If txtOPCode.Text.Length = 8 And txtBRIG.Text.Length = 7 Then
            Dim ok As Boolean = False
            For i = 0 To lstOP.Items.Count - 1
                If lstOP.Items(i) = s Then
                    lstOP.SelectedIndex = i
                    ok = True
                    Exit For
                End If
            Next
            If Not ok Then
                i = lstOP.Items.Add(s)

                Dim ps As PODStatus = Nothing
                Try



                    ps = New PODStatus
                    ps.BRIG = txtBRIG.Text
                    ps.POD = txtOPCode.Text
                    ps.VerifyOK = False
                    ps.ErrMsg = ""
                    ps.Q = 1
                    ps.WP = ""
                    ps.PRC = 0
                    ps.BRK = 0
                    ps.LMNGA = 0
                    ps.OPdate = Date.Now
                    PODList.Add(ps)
                    lstOP.SelectedIndex = i
                    If Not NoNetworkMode Then
                        Try
                            svc.NewPod3(TerminalID, ps.POD, ps.BRIG, ps.OPdate)
                        Catch ex As Exception
                            NoNetworkMode = True
                            GoTo trysave
                        End Try

                        Try
                            txtInfo.Text = svc.GetOPInfo(ps.POD)
                            ps.PODName = txtInfo.Text
                            txtOPCode.Text = ""
                        Catch ex As Exception

                            txtInfo.Text = "Ошибка расшифровки кода"
                            ps.PODName = ""
                            If Not svcVar Is Nothing Then svcVar.Dispose()
                            svcVar = Nothing
                            
                        End Try
                    Else
trysave:

                        svcLite.NewPod(TerminalID, ps.POD, ps.BRIG, ps.OPdate)
                        Try
                            txtOPCode.Text = ""
                            txtInfo.Text = "нет расшифорвки, т.к. нет связи"
                            ps.PODName = ""
                        Catch ex As Exception
                            'If Not svcVarLite Is Nothing Then svcVarLite.Dispose()
                            svcVarLite = Nothing
                        End Try

                    End If
                Catch ex As Exception
                    txtInfo.Text = "Ошибка регистрации кода операции"
                    If Not ps Is Nothing Then
                        ps.Deleted = True
                        ps = Nothing
                    End If

                    If Not svcVar Is Nothing Then svcVar.Dispose()
                    svcVar = Nothing
                End Try

            End If


        End If
    End Sub

    Private Sub txtOPCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOPCode.TextChanged
        If Not inselChange Then
            RegOP()
        End If
    End Sub

    Dim inselChange As Boolean = False

    Private Sub lstOP_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstOP.SelectedIndexChanged
        If lstOP.SelectedIndex >= 0 Then
            inselChange = True
            Dim ps As PODStatus
            Dim pi As Integer

            pi = FindPOD(lstOP.Text)
            If pi >= 0 Then
                ps = Nothing
                Try
                    ps = PODList.Item(pi)
                Catch ex As Exception

                End Try

                If Not ps Is Nothing Then
                    txtBRIG.Text = ps.BRIG
                    txtOPCode.Text = ps.POD
                    If Not NoNetworkMode Then
                        If ps.VerifyOK = False And (ps.ErrMsg = "" Or ps.ErrMsg = "Идет проверка") Then
                            Dim msg As String
                            Try
                                msg = svc.VerifyPod3(TerminalID, ps.POD, ps.BRIG)
                                If msg.StartsWith("OK") Then
                                    ps.VerifyOK = True
                                    ps.ErrMsg = msg.Replace("OK", "")
                                Else
                                    ps.VerifyOK = False
                                    If ps.ErrMsg <> "Идет проверка" And ps.ErrMsg <> "" Then
                                        ps.Deleted = True
                                    End If
                                    ps.ErrMsg = msg

                                End If
                            Catch ex As Exception
                                MsgBox("Ошибка при проверке кода операции")
                                If Not svcVar Is Nothing Then svcVar.Dispose()
                                svcVar = Nothing
                            End Try
                        End If

                        If ps.Closing Then
                            Dim msg As String
                            Try
                                msg = svc.VerifyClose(TerminalID, ps.POD, ps.BRIG)
                                If msg = "OK" Then
                                    ps.Deleted = True
                                    ps.Closing = False
                                Else

                                    ps.ErrMsg = msg
                                End If
                            Catch ex As Exception
                                MsgBox("Ошибка при проверке  статуса закрытия")
                                If Not svcVar Is Nothing Then svcVar.Dispose()
                                svcVar = Nothing
                            End Try
                        End If

                    Else
                        ps.VerifyOK = True
                        'MsgBox("не проверено тк нет связи")
                    End If



                    If ps.VerifyOK Then
                        Dim si As Integer
                        Dim st As String = ""
                        For si = 0 To StateDT.Rows.Count - 1
                            If Integer.Parse(StateDT.Rows(si)("ID")) = ps.Status Then
                                st = StateDT.Rows(si)("Name")
                            End If
                        Next
                        If st = "" Then
                            st = ps.Status.ToString
                        End If

                        If ps.PODName = "" Then
                            Try
                                ps.PODName = svc.GetOPInfo(txtOPCode.Text)
                            Catch ex As Exception
                                ps.PODName = ""
                                If Not svcVar Is Nothing Then svcVar.Dispose()
                                svcVar = Nothing
                            End Try
                        End If
                        Dim sname As String
                        If ps.PODName = "" Then
                            If NoNetworkMode Then
                                sname = "нет расшифровки, так как нет связи"
                            Else
                                sname = "ошибка расшифровки"
                            End If
                        Else
                            sname = ps.PODName

                        End If
                        If ps.ErrMsg <> "Идет проверка" And ps.ErrMsg <> "" Then
                            txtInfo.Text = ps.ErrMsg + " С:" + st + " Kол:" + ps.Q.ToString + vbCrLf + sname
                            txtInfo.ForeColor = Color.Yellow
                        Else
                            txtInfo.Text = "С:" + st + " Kол:" + ps.Q.ToString + vbCrLf + sname
                            txtInfo.ForeColor = Color.Black
                        End If

                    Else


                        txtInfo.Text = ps.ErrMsg + vbCrLf + ps.PODName
                        If ps.ErrMsg = "Идет проверка" Or ps.ErrMsg = "" Then
                            txtInfo.ForeColor = Color.Yellow
                        Else
                            txtInfo.ForeColor = Color.Red
                        End If

                        End If
                    If ps.Closing And ps.ErrMsg <> "" Then
                        txtInfo.ForeColor = Color.Yellow

                        If ps.ErrMsg.StartsWith("ERR") Then
                            ps.CloseDate = Date.Now.AddMinutes(-9)
                        End If
                        txtInfo.Text = ps.ErrMsg.Replace("ERR", "") + vbCrLf + ps.PODName

                    End If


                End If
            End If
            inselChange = False
            End If
    End Sub

 

    Private Sub frmOPS_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Dim SZ As Size
        SZ = New Size
        SZ.Height = (Screen.PrimaryScreen.WorkingArea.Height)
        SZ.Width = (Screen.PrimaryScreen.WorkingArea.Width)
        Me.Size = Size
    End Sub

    Private Sub txtBRIG_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBRIG.TextChanged
        If Not inselChange Then
            RegOP()
        End If

    End Sub

   
    Private Sub txtInfo_ParentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInfo.ParentChanged

    End Sub
End Class