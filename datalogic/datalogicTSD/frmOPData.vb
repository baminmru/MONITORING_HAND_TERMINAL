
Imports System.IO
Imports System.Reflection
Imports System.Xml
Imports System.Data
Imports System.Net
Imports System.Diagnostics
Imports datalogic.datacapture
Imports datalogic.pdc

Public Class frmOPData


    Public ps As PODStatus
    Private WithEvents m_Laser As Laser
    Private IgnoreLaser As Boolean

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If txtWP.Text.Length = 0 Then
              MsgBox("Не задан Номер рабочего места")
                txtWP.Focus()
                Return
        End If
        If Not IsNumeric(txtWP.Text) Then
              MsgBox("Номер рабочего места - число")
              txtWP.Text = ""
              txtWP.Focus()
              Return
        End If
        If txtWP.Text.Length <> 6 Then
            MsgBox("Номер рабочего места - 6 символов")

            txtWP.Focus()
            Return
        End If

        If txtQ.Text.Length = 0 Then
                MsgBox("Не задано кол-во работников")
                txtQ.Focus()
                Return
        End If
        If Not IsNumeric(txtQ.Text) Then
            MsgBox("Количество сотрудников - число")
            txtQ.Text = ""
            txtQ.Focus()
            Return
        End If

        If Integer.Parse(txtQ.Text) <= 0 Then
                MsgBox("Количество сотрудников >0")
                txtQ.Focus()
                Return
        End If

        If txtBRK.Text.Length = 0 Then
              MsgBox("Не задано количество брака")
                txtBRK.Focus()
                Return
        End If

        If Not IsNumeric(txtBRK.Text) Then
            MsgBox("Количество брака - число")
            txtBRK.Text = ""
            txtBRK.Focus()
            Return
        End If

          If Integer.Parse(txtBRK.Text) < 0 Then
                MsgBox("Количество брака >=0")
                txtBRK.Focus()
                Return
            End If

        If lstST.SelectedIndex < 0 Then
                 MsgBox("Не задан статус")
                lstST.Focus()
                Return
        End If

        If lstST.Text.StartsWith("98") Then
            If txtPRC.Text.Length = 0 And txtLMNGA.Text = 0 Then
                MsgBox("Надо ввести результат исполнения ")
                panel98.Visible = True
                txtPRC.Focus()
                Return
            End If
            If Not IsNumeric(txtPRC.Text) And Not IsNumeric(txtLMNGA.Text) Then
                MsgBox("% завершения - число")
                txtPRC.Text = ""
                panel98.Visible = True
                txtPRC.Focus()
                Return
            End If


            If Not IsNumeric(txtLMNGA.Text) Then
                MsgBox("завершено деталей - число")
                panel98.Visible = True
                txtLMNGA.Text = ""
                txtLMNGA.Focus()
                Return
            End If

            If Integer.Parse(txtPRC.Text) <= 0 And Integer.Parse(txtLMNGA.Text) <= 0 Then
                MsgBox("% завершения >0 либо завершено деталей >0 ")
                panel98.Visible = True
                txtPRC.Focus()
                Return
            End If
            If Integer.Parse(txtPRC.Text) > 0 And Integer.Parse(txtLMNGA.Text) > 0 Then
                MsgBox("% завершения >0 либо завершено деталей >0 ")
                panel98.Visible = True
                txtPRC.Focus()
                Return
            End If

            If Integer.Parse(txtPRC.Text) >= 100 Then
                MsgBox("% завершения <100")
                panel98.Visible = True
                txtPRC.Focus()
                Return
            End If

        End If

        Try

            ps.BRIG = txtBRIG.Text
            ps.OPdate = Now
            If ps.WP <> txtWP.Text Then
                ps.WP = txtWP.Text
                ps.WPChanged = True
                ps.WPChangedL = 1
            End If
            If ps.Q <> Integer.Parse("0" + txtQ.Text) Then
                ps.Q = Integer.Parse("0" + txtQ.Text)
                ps.QChanged = True
                ps.QChangedL = 1
            End If
            If ps.PRC <> Integer.Parse("0" + txtPRC.Text) Then
                ps.PRC = Integer.Parse("0" + txtPRC.Text)
                ps.PRChanged = True
                ps.PRChangedL = 1
            End If
            If ps.LMNGA <> Integer.Parse("0" + txtLMNGA.Text) Then
                ps.LMNGA = Integer.Parse("0" + txtLMNGA.Text)
                ps.LMNGAChanged = True
                ps.LMNGAChangedL = 1
            End If
            If ps.BRK <> Integer.Parse("0" + txtBRK.Text) Then
                ps.BRK = Integer.Parse("0" + txtBRK.Text)
                ps.BRKChanged = True
                ps.BRKChangedL = 1
            End If
            If ps.Status <> Integer.Parse("0" + lstST.Text.Substring(0, 2)) Then
                ps.Status = Integer.Parse("0" + lstST.Text.Substring(0, 2))
                ps.StatusChanged = True
                ps.StatusChangedL = 1
            End If
            If ps.WPChanged Or ps.QChanged Or ps.PRChanged Or ps.BRKChanged Or ps.StatusChanged Then
                Try
                    If Not NoNetworkMode Then
                        Try
                            svc.RegisterPodInfo3(TerminalID, ps.BRIG, ps.POD, ps.OPdate, ps.WP, ps.Q, ps.Status, ps.PRC, ps.BRK, ps.LMNGA, ps.WPChanged, ps.QChanged, ps.StatusChanged, ps.PRChanged, ps.BRKChanged, ps.LMNGAChanged)
                        Catch ex As Exception
                            NoNetworkMode = True
                            If Not svcVar Is Nothing Then svcVar.Dispose()
                            svcVar = Nothing
                            Try
                                svcLite.RegisterPodInfo(TerminalID, ps.BRIG, ps.POD, ps.OPdate, ps.WP, ps.Q, ps.Status, ps.PRC, ps.BRK, ps.LMNGA, ps.WPChangedL, ps.QChangedL, ps.StatusChangedL, ps.PRChangedL, ps.BRKChangedL, ps.LMNGAChangedL)
                            Catch ex1 As Exception
                                MsgBox("Ошибка связи с обоими бд")
                            End Try
                        End Try

                    Else
                        Try
                            svcLite.RegisterPodInfo(TerminalID, ps.BRIG, ps.POD, ps.OPdate, ps.WP, ps.Q, ps.Status, ps.PRC, ps.BRK, ps.LMNGA, ps.WPChangedL, ps.QChangedL, ps.StatusChangedL, ps.PRChangedL, ps.BRKChangedL, ps.LMNGAChangedL)
                        Catch ex As Exception
                            'MsgBox(ex.Message)
                        End Try
                    End If

                    ps.WPChanged = False
                    ps.QChanged = False
                    ps.PRChanged = False
                    ps.BRKChanged = False
                    ps.StatusChanged = False

                    ps.WPChangedL = 0
                    ps.QChangedL = 0
                    ps.PRChangedL = 0
                    ps.BRKChangedL = 0
                    ps.StatusChangedL = 0

                    If lstST.Text.StartsWith("98") Or lstST.Text.StartsWith("99") Then
                        'ps.Deleted = True
                        ps.Closing = True
                        ps.CloseDate = Now
                        ps.ErrMsg = ""
                    End If

                Catch ex As Exception
                    

                End Try

            End If




        Catch ex As Exception

        End Try

        Me.Close()
       

    End Sub

    Private Sub frmOPData_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Deactivate

    End Sub

 
    Private Sub frmOp_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        If e.KeyChar = vbCr Then
            cmdClose_Click(sender, e)
        End If
        If e.KeyChar = Chr(System.Windows.Forms.Keys.Escape) Then
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End If
    End Sub

  Private Sub LoadList()



        lstST.DataSource = StateDT
        lstST.DisplayMember = "Name"
        lstST.ValueMember = "ID"

        If ps.Status > 0 Then
            Dim drv As DataRowView
            Dim i As Integer
            For i = 0 To lstST.Items.Count - 1
                drv = lstST.Items(i)
                If ps.Status = Integer.Parse(drv.Row("ID")) Then
                    lstST.SelectedIndex = i
                    lstST.Focus()
                    Exit For
                End If
            Next
        End If


    End Sub


    Private Sub frmopdata_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Timer1.Enabled = False
        IgnoreLaser = True
        m_Laser = Nothing
    End Sub
 


  Private Sub frmOPData_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    m_Laser = myLaser
    IgnoreLaser = False
        LoadList()
        Timer1.Enabled = True
  End Sub

     Private Sub HandleData(ByVal sender As datalogic.datacapture.ScannerEngine) Handles m_Laser.GoodReadEvent

        If Not IgnoreLaser Then
            Dim s As String
            s = sender.BarcodeDataAsText
            'MsgBox("*" + s + "*" + s.Length.ToString)
            Dim i As Integer
            Dim ok As Boolean = False
            Dim drv As DataRowView
            Dim test As String

            If s.Length = 4 Then

                For i = 0 To lstST.Items.Count - 1
                    drv = lstST.Items(i)
                    test = drv.Row("ID")
                    If "00" + test = s Then
                        lstST.SelectedIndex = i
                        Exit For
                    End If
                Next

            End If

            If s.Length = 6 Then
                txtWP.Text = s
            End If

            If s.Length = 7 Then
                txtBRIG.Text = s
            End If

            If s.Length >= 8 Then

                If s.StartsWith("999999") Then
                    Dim s1 As String
                    s1 = s.Substring(6)

                    'MsgBox(s1)

                    For i = 0 To lstST.Items.Count - 1
                        drv = lstST.Items(i)
                        test = drv.Row("ID")
                        'MsgBox(test + " " + s1)
                        If test = s1 Then

                            lstST.SelectedIndex = i
                            Exit For
                        End If
                    Next
                    If s1 = "98" Then
                        Dim f As frmGetPrc
                        f = New frmGetPrc
                        If f.ShowDialog = Windows.Forms.DialogResult.OK Then
                            txtPRC.Text = f.txtCode.Text
                        End If
                        f = Nothing
                        If txtPRC.Text = "" Then
                            txtPRC.Text = "0"
                            txtPRC.Focus()
                        End If
                    End If
                End If

            End If

        End If


    End Sub

    Private Sub lstST_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstST.SelectedIndexChanged
        If lstST.Text.StartsWith("98") Then
            panel98.Visible = True
        Else
            txtPRC.Text = 0
            txtLMNGA.Text = 0
            panel98.Visible = False
        End If
    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.Location = New Point(0, 0)

    End Sub

    Private Sub frmOPData_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Dim SZ As Size
        SZ = New Size
        SZ.Height = (Screen.PrimaryScreen.WorkingArea.Height)
        SZ.Width = (Screen.PrimaryScreen.WorkingArea.Width)
        Me.Size = Size
    End Sub

    Private Sub closePanel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        panel98.Visible = False
    End Sub
End Class