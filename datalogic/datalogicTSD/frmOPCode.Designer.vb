<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmOPCode
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
        Me.txtOPCode = New System.Windows.Forms.TextBox
        Me.txtInfo = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'txtOPCode
        '
        Me.txtOPCode.BackColor = System.Drawing.Color.White
        Me.txtOPCode.Location = New System.Drawing.Point(13, 56)
        Me.txtOPCode.Name = "txtOPCode"
        Me.txtOPCode.ReadOnly = True
        Me.txtOPCode.Size = New System.Drawing.Size(202, 23)
        Me.txtOPCode.TabIndex = 0
        '
        'txtInfo
        '
        Me.txtInfo.BackColor = System.Drawing.Color.White
        Me.txtInfo.Location = New System.Drawing.Point(13, 94)
        Me.txtInfo.Multiline = True
        Me.txtInfo.Name = "txtInfo"
        Me.txtInfo.ReadOnly = True
        Me.txtInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtInfo.Size = New System.Drawing.Size(202, 76)
        Me.txtInfo.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(10, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(204, 25)
        Me.Label1.Text = "отсканируйте"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(10, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(204, 25)
        Me.Label2.Text = "код операции"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmOPCode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(225, 193)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtInfo)
        Me.Controls.Add(Me.txtOPCode)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(5, 50)
        Me.Name = "frmOPCode"
        Me.Text = "Мониторинг. Операция"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtOPCode As System.Windows.Forms.TextBox
    Friend WithEvents txtInfo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
