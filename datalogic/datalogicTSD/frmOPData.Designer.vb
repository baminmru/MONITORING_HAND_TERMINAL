<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmOPData
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
Me.components = New System.ComponentModel.Container
Me.mainMenu1 = New System.Windows.Forms.MainMenu()
Me.Label1 = New System.Windows.Forms.Label
Me.Label2 = New System.Windows.Forms.Label
Me.txtWP = New System.Windows.Forms.TextBox
Me.txtQ = New System.Windows.Forms.TextBox
Me.lstST = New System.Windows.Forms.ListBox
Me.lblOP = New System.Windows.Forms.Label
Me.Timer1 = New System.Windows.Forms.Timer()
Me.Label4 = New System.Windows.Forms.Label
Me.txtBRK = New System.Windows.Forms.TextBox
Me.Label5 = New System.Windows.Forms.Label
Me.panel98 = New System.Windows.Forms.Panel
Me.txtLMNGA = New System.Windows.Forms.TextBox
Me.l6 = New System.Windows.Forms.Label
Me.txtPRC = New System.Windows.Forms.TextBox
Me.Label3 = New System.Windows.Forms.Label
Me.Label6 = New System.Windows.Forms.Label
Me.txtBRIG = New System.Windows.Forms.TextBox
Me.panel98.SuspendLayout()
Me.SuspendLayout()
'
'Label1
'
Me.Label1.Font = New System.Drawing.Font("Arial", 12.0!, FontStyle.Regular)
Me.Label1.Location = New System.Drawing.Point(4, 54)
Me.Label1.Name = "Label1"
Me.Label1.Size = New System.Drawing.Size(100, 30)
Me.Label1.TabIndex = 7
Me.Label1.Text = "Раб. место"
'
'Label2
'
Me.Label2.Font = New System.Drawing.Font("Arial", 12.0!, FontStyle.Regular)
Me.Label2.Location = New System.Drawing.Point(4, 85)
Me.Label2.Name = "Label2"
Me.Label2.Size = New System.Drawing.Size(100, 25)
Me.Label2.TabIndex = 6
Me.Label2.Text = "Кол-во чел."
'
'txtWP
'
Me.txtWP.Font = New System.Drawing.Font("Arial", 12.0!, FontStyle.Regular)
Me.txtWP.Location = New System.Drawing.Point(123, 59)
Me.txtWP.MaxLength = 6
Me.txtWP.Name = "txtWP"
Me.txtWP.Size = New System.Drawing.Size(115, 26)
Me.txtWP.TabIndex = 1
'
'txtQ
'
Me.txtQ.Font = New System.Drawing.Font("Arial", 12.0!, FontStyle.Regular)
Me.txtQ.Location = New System.Drawing.Point(123, 85)
Me.txtQ.MaxLength = 5
Me.txtQ.Name = "txtQ"
Me.txtQ.Size = New System.Drawing.Size(115, 26)
Me.txtQ.TabIndex = 2
'
'lstST
'
Me.lstST.Font = New System.Drawing.Font("Tahoma", 16.0!, FontStyle.Regular)
'Me.lstST.ItemHeight = 25
Me.lstST.Location = New System.Drawing.Point(4, 180)
Me.lstST.Name = "lstST"
Me.lstST.Size = New System.Drawing.Size(234, 104)
Me.lstST.TabIndex = 4
'
'lblOP
'
Me.lblOP.Font = New System.Drawing.Font("Arial", 14.0!, FontStyle.Regular)
Me.lblOP.Location = New System.Drawing.Point(125, 32)
Me.lblOP.Name = "lblOP"
Me.lblOP.Size = New System.Drawing.Size(116, 24)
Me.lblOP.TabIndex = 5
Me.lblOP.Text = "00000000"
Me.lblOP.TextAlign = System.Drawing.ContentAlignment.TopCenter
'
'Timer1
'
Me.Timer1.Interval = 1000
'
'Label4
'
Me.Label4.Font = New System.Drawing.Font("Arial", 12.0!, FontStyle.Regular)
Me.Label4.Location = New System.Drawing.Point(4, 116)
Me.Label4.Name = "Label4"
Me.Label4.Size = New System.Drawing.Size(100, 20)
Me.Label4.TabIndex = 4
Me.Label4.Text = "Брак"
'
'txtBRK
'
Me.txtBRK.Font = New System.Drawing.Font("Arial", 12.0!, FontStyle.Regular)
Me.txtBRK.Location = New System.Drawing.Point(123, 111)
Me.txtBRK.MaxLength = 8
Me.txtBRK.Name = "txtBRK"
Me.txtBRK.Size = New System.Drawing.Size(115, 26)
Me.txtBRK.TabIndex = 3
'
'Label5
'
Me.Label5.Location = New System.Drawing.Point(0, 32)
Me.Label5.Name = "Label5"
Me.Label5.Size = New System.Drawing.Size(131, 25)
Me.Label5.TabIndex = 1
Me.Label5.Text = "Текущая операция"
'
'panel98
'
Me.panel98.Controls.Add(Me.txtLMNGA)
Me.panel98.Controls.Add(Me.l6)
Me.panel98.Controls.Add(Me.txtPRC)
Me.panel98.Controls.Add(Me.Label3)
Me.panel98.Location = New System.Drawing.Point(244, 46)
Me.panel98.Name = "panel98"
Me.panel98.Size = New System.Drawing.Size(236, 126)
Me.panel98.TabIndex = 0
Me.panel98.Visible = False
'
'txtLMNGA
'
Me.txtLMNGA.Font = New System.Drawing.Font("Arial", 11.0!, FontStyle.Regular)
Me.txtLMNGA.Location = New System.Drawing.Point(59, 86)
Me.txtLMNGA.Name = "txtLMNGA"
Me.txtLMNGA.Size = New System.Drawing.Size(116, 24)
Me.txtLMNGA.TabIndex = 14
'
'l6
'
Me.l6.Font = New System.Drawing.Font("Arial", 12.0!, FontStyle.Regular)
Me.l6.Location = New System.Drawing.Point(37, 64)
Me.l6.Name = "l6"
Me.l6.Size = New System.Drawing.Size(165, 20)
Me.l6.TabIndex = 15
Me.l6.Text = "завершено  деталей"
'
'txtPRC
'
Me.txtPRC.Font = New System.Drawing.Font("Arial", 11.0!, FontStyle.Regular)
Me.txtPRC.HideSelection = False
Me.txtPRC.Location = New System.Drawing.Point(59, 27)
Me.txtPRC.MaxLength = 2
Me.txtPRC.Name = "txtPRC"
Me.txtPRC.Size = New System.Drawing.Size(112, 24)
Me.txtPRC.TabIndex = 13
'
'Label3
'
Me.Label3.Enabled = False
Me.Label3.Font = New System.Drawing.Font("Arial", 12.0!, FontStyle.Regular)
Me.Label3.Location = New System.Drawing.Point(48, 8)
Me.Label3.Name = "Label3"
Me.Label3.Size = New System.Drawing.Size(127, 25)
Me.Label3.TabIndex = 16
Me.Label3.Text = "% завершения"
'
'Label6
'
Me.Label6.Font = New System.Drawing.Font("Arial", 12.0!, FontStyle.Regular)
Me.Label6.Location = New System.Drawing.Point(3, 145)
Me.Label6.Name = "Label6"
Me.Label6.Size = New System.Drawing.Size(103, 32)
Me.Label6.TabIndex = 12
Me.Label6.Text = "Бригада"
'
'txtBRIG
'
Me.txtBRIG.Font = New System.Drawing.Font("Arial", 11.0!, FontStyle.Regular)
Me.txtBRIG.Location = New System.Drawing.Point(123, 148)
Me.txtBRIG.Name = "txtBRIG"
Me.txtBRIG.ReadOnly = True
Me.txtBRIG.Size = New System.Drawing.Size(115, 24)
Me.txtBRIG.TabIndex = 11
'
'frmOPData
'
Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
Me.AutoScroll = True
Me.ClientSize = New System.Drawing.Size(245, 315)
Me.ControlBox = False
Me.Controls.Add(Me.panel98)
Me.Controls.Add(Me.Label5)
Me.Controls.Add(Me.txtBRK)
Me.Controls.Add(Me.Label4)
Me.Controls.Add(Me.lblOP)
Me.Controls.Add(Me.lstST)
Me.Controls.Add(Me.txtQ)
Me.Controls.Add(Me.txtWP)
Me.Controls.Add(Me.Label2)
Me.Controls.Add(Me.Label1)
Me.Controls.Add(Me.txtBRIG)
Me.Controls.Add(Me.Label6)
Me.KeyPreview = True
Me.Name = "frmOPData"
Me.Text = "Статус операции"
Me.TopMost = True
Me.panel98.ResumeLayout(False)
'Me.panel98.PerformLayout()
Me.ResumeLayout(False)
'Me.PerformLayout()

End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtWP As System.Windows.Forms.TextBox
    Friend WithEvents txtQ As System.Windows.Forms.TextBox
    Friend WithEvents lstST As System.Windows.Forms.ListBox
    Friend WithEvents lblOP As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtBRK As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents panel98 As System.Windows.Forms.Panel
    Friend WithEvents txtLMNGA As System.Windows.Forms.TextBox
    Friend WithEvents l6 As System.Windows.Forms.Label
    Friend WithEvents txtPRC As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtBRIG As System.Windows.Forms.TextBox
End Class
