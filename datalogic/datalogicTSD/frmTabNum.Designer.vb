<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmTabNum
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Private mainMenu1 As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.mainMenu1 = New System.Windows.Forms.MainMenu
        Me.txtNum = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'txtNum
        '
        Me.txtNum.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Regular)
        Me.txtNum.Location = New System.Drawing.Point(13, 119)
        Me.txtNum.MaxLength = 7
        Me.txtNum.Name = "txtNum"
        Me.txtNum.Size = New System.Drawing.Size(220, 35)
        Me.txtNum.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(13, 59)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(212, 31)
        Me.Label1.Text = "введите"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(13, 90)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(212, 26)
        Me.Label2.Text = "номер бригады"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmTabNum
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(243, 290)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtNum)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTabNum"
        Me.Text = "Мониторинг.  № бригады"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtNum As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
