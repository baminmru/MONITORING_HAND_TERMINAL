<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmOPS
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
        Me.lstOP = New System.Windows.Forms.ListBox
        Me.txtOPCode = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtInfo = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtBRIG = New System.Windows.Forms.TextBox
        Me.lblMode = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lstOP
        '
        Me.lstOP.Location = New System.Drawing.Point(5, 3)
        Me.lstOP.Name = "lstOP"
        Me.lstOP.Size = New System.Drawing.Size(230, 130)
        Me.lstOP.TabIndex = 0
        '
        'txtOPCode
        '
        Me.txtOPCode.Location = New System.Drawing.Point(6, 163)
        Me.txtOPCode.MaxLength = 8
        Me.txtOPCode.Name = "txtOPCode"
        Me.txtOPCode.Size = New System.Drawing.Size(102, 23)
        Me.txtOPCode.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(6, 136)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(230, 24)
        Me.Label1.Text = "Операция"
        '
        'txtInfo
        '
        Me.txtInfo.Location = New System.Drawing.Point(6, 189)
        Me.txtInfo.Name = "txtInfo"
        Me.txtInfo.Size = New System.Drawing.Size(226, 64)
        '
        'Timer1
        '
        Me.Timer1.Interval = 3000
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(114, 136)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(91, 24)
        Me.Label2.Text = "Бригада"
        '
        'txtBRIG
        '
        Me.txtBRIG.Location = New System.Drawing.Point(114, 163)
        Me.txtBRIG.Name = "txtBRIG"
        Me.txtBRIG.Size = New System.Drawing.Size(78, 23)
        Me.txtBRIG.TabIndex = 3
        '
        'lblMode
        '
        Me.lblMode.Location = New System.Drawing.Point(212, 166)
        Me.lblMode.Name = "lblMode"
        Me.lblMode.Size = New System.Drawing.Size(20, 20)
        Me.lblMode.Text = "A"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(196, 136)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 19)
        Me.Label3.Text = "Сеть"
        '
        'frmOPS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(243, 290)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblMode)
        Me.Controls.Add(Me.txtBRIG)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtInfo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtOPCode)
        Me.Controls.Add(Me.lstOP)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOPS"
        Me.Text = "Текущие операции"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstOP As System.Windows.Forms.ListBox
    Friend WithEvents txtOPCode As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtInfo As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtBRIG As System.Windows.Forms.TextBox
    Friend WithEvents lblMode As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
