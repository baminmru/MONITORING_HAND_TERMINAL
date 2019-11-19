Public Class frmLiteToOrc
    Private number As Integer = 0

    Public Sub gettext(ByVal i As Integer, ByVal quality As Integer)
        txtprogress.Text = "выполняется передача данных " + i.ToString + "из " + quality.ToString
        Timer1.Enabled = True
        ProgressBar1.Value = 0
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.Location = New Point(0, 0)
        ProgressBar1.Value += 1
        Me.Close()
    End Sub
    Public Sub closeform()
        ProgressBar1.Value = 0
        Me.Close()
        Timer1.Enabled = False
    End Sub

    Private Sub ProgressBar1_ParentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProgressBar1.ParentChanged

    End Sub
End Class