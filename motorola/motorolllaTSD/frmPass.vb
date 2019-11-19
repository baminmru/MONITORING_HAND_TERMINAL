Public Class frmPass

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = Windows.Forms.DialogResult.OK
     End Sub

  

    Private Sub frmPass_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        If e.KeyChar = vbCr Then
          If txtCode.Text = ClosePassword Then
            cmdClose_Click(sender, e)
          End If
        End If
        If e.KeyChar = Chr(System.Windows.Forms.Keys.Escape) Then
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End If
    End Sub
End Class