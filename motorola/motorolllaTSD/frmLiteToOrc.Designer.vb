<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmLiteToOrc
    Inherits System.Windows.Forms.Form

    'Форма переопределяет метод Dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Требуется для конструктора Windows Forms
    Private components As System.ComponentModel.IContainer
    Private mainMenu1 As System.Windows.Forms.MainMenu

    'Примечание: следующая процедура является обязательной для конструктора Windows Forms
    'Для ее изменения используйте конструктор Windows Forms.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.mainMenu1 = New System.Windows.Forms.MainMenu
        Me.txtprogress = New System.Windows.Forms.Label
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.Timer1 = New System.Windows.Forms.Timer
        Me.SuspendLayout()
        '
        'txtprogress
        '
        Me.txtprogress.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtprogress.Location = New System.Drawing.Point(39, 50)
        Me.txtprogress.Name = "txtprogress"
        Me.txtprogress.Size = New System.Drawing.Size(164, 63)
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(39, 153)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(164, 20)
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'frmLiteToOrc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(243, 290)
        Me.ControlBox = False
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.txtprogress)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLiteToOrc"
        Me.Text = "Прогресс передачи данных"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtprogress As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
