Imports Symbol
Imports Symbol.ResourceCoordination
Imports System.IO
Imports System.Reflection
Imports System.Xml
Imports System.Data

Public Class frmStart


    Private WithEvents m_Laser As Monitoring.BarcodeAPI
    Private IgnoreLaser As Boolean = False

    Public Sub New()
        InitializeComponent()
    End Sub


 Private Sub frmStart_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
    m_Laser = Nothing
    IgnoreLaser = False
  End Sub
  Private Sub frmStart_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        m_Laser = myLaser
    End Sub

   Private Sub HandleData(ByVal TheReaderData As Symbol.Barcode.ReaderData) Handles m_Laser.HandleData
        If Not IgnoreLaser Then
          txtNum.Text = TheReaderData.Text
          If IsNumeric(txtNum.Text) And txtNum.Text.Length = 7 Then
            NextForm()
          End If
        End If

    End Sub 'HandleData


    Private Sub txtNum_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNum.TextChanged
        If IsNumeric(txtNum.Text) And txtNum.Text.Length = 7 Then
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
                Catch
                    ' If Not svcVarLite Is Nothing Then svcVarLite.Dispose()
                    svcVarLite = Nothing
                End Try
            End If
        NextForm()
        End If
    End Sub

    Private Sub NextForm()
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If IsNumeric(txtNum.Text) And txtNum.Text.Length = 7 Then
            Try
                svc.RegisterInfo3(TerminalID, Date.Now)
            Catch ex As Exception
                If Not svcVar Is Nothing Then svcVar.Dispose()
                svcVar = Nothing
            End Try
            NextForm()
        End If
    End Sub
End Class