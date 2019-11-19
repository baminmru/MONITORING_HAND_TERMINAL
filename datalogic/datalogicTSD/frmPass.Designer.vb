<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmPass
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
        Me.txtCode = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.ToolBar1 = New System.Windows.Forms.ToolBar
        Me.SuspendLayout()
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(33, 136)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtCode.Size = New System.Drawing.Size(177, 23)
        Me.txtCode.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular)
        Me.Label1.Location = New System.Drawing.Point(33, 59)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(182, 19)
        Me.Label1.Text = "введите"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(33, 99)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(172, 19)
        Me.Label2.Text = "код для выхода"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'ToolBar1
        '
        Me.ToolBar1.Name = "ToolBar1"
        '
        'frmPass
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(243, 290)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtCode)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPass"
        Me.Text = "код закрытия"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ToolBar1 As System.Windows.Forms.ToolBar
End Class
