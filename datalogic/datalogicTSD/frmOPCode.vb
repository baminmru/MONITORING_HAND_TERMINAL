Imports datalogic.datacapture
Imports datalogic.pdc
Imports System.IO
Imports System.Reflection
Imports System.Xml
Imports System.Data

Public Class frmOPCode
    Dim WithEvents m_laser As Laser
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If txtOPCode.Text <> "" Then
            Me.DialogResult = Windows.Forms.DialogResult.OK

        Else
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End If
    End Sub
    Private Sub frmOPCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        If e.KeyChar = Chr(Keys.Enter) Then
            Button1_Click(sender, e)
        End If
        If e.KeyChar = Chr(Keys.Escape) Then
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End If
    End Sub

    Private Sub frmOPCode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        m_laser = myLaser
    End Sub

    Private Sub m_laser_GoodReadEvent(ByVal sender As datalogic.datacapture.ScannerEngine) Handles m_laser.GoodReadEvent
        txtOPCode.Text = sender.BarcodeDataAsText
        Try
            txtInfo.Text = svc.GetOPInfo(txtOPCode.Text)
        Catch ex As Exception
            txtInfo.Text = "Ошибка расшифровки кода"
        End Try

    End Sub

    Private Sub txtOPCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOPCode.TextChanged

    End Sub
End Class