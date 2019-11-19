
Imports System.IO
Imports System.Reflection
Imports System.Xml
Imports System.Data

Public Class frmTabNum
    Dim WithEvents m_laser As Monitoring.BarcodeAPI

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If txtNum.Text <> "" Then

          If Not IsNumeric(txtNum.Text) Then
             MsgBox("Номер бригады - число")
             txtNum.Focus()
             Return
          End If
          If txtNum.Text.Length = 7 Then
              If TNum <> txtNum.Text Then
                    TNum = txtNum.Text
                    If Not NoNetworkMode Then
                        Try
                            svc.RegisterInfo3(TerminalID, Date.Now)
                        Catch ex As Exception
                            If Not svcVar Is Nothing Then svcVar.Dispose()
                            svcVar = Nothing
                        End Try
                    Else
                        Try
                            svcLite.RegisterInfo(TerminalID, Date.Now)
                        Catch ex As Exception
                            'If Not svcVar Is Nothing Then svcVar.Dispose()
                            svcVarLite = Nothing
                        End Try
                    End If
                    PODList.Clear()
                End If
                    m_laser = Nothing
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                Else
                    MsgBox("Номер бригады - 7 символов")
                    txtNum.Focus()
                End If
            Else
                m_laser = Nothing
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
            End If
    End Sub

    Private Sub frmTabNum_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        If e.KeyChar = Chr(Keys.Enter) Then
            Button1_Click(sender, e)
        End If
        If e.KeyChar = Chr(Keys.Escape) Then
            m_laser = Nothing
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End If
    End Sub

    Private Sub frmTabNum_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        m_laser = myLaser
    End Sub

    'Private Sub m_laser_GoodReadEvent(ByVal sender As datalogic.datacapture.ScannerEngine) Handles m_laser.GoodReadEvent
    '    txtNum.Text = sender.BarcodeDataAsText
    'End Sub

  Private Sub HandleData(ByVal TheReaderData As Symbol.Barcode.ReaderData) Handles m_laser.HandleData
          txtNum.Text = TheReaderData.Text
  End Sub
End Class