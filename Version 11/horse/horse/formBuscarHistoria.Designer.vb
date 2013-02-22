<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formBuscarHistoria
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.VButton2 = New VIBlend.WinForms.Controls.vButton()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.VButton1 = New VIBlend.WinForms.Controls.vButton()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'VButton2
        '
        Me.VButton2.AllowAnimations = True
        Me.VButton2.BackColor = System.Drawing.Color.Transparent
        Me.VButton2.Location = New System.Drawing.Point(76, 120)
        Me.VButton2.Name = "VButton2"
        Me.VButton2.RoundedCornersMask = CType(15, Byte)
        Me.VButton2.Size = New System.Drawing.Size(163, 30)
        Me.VButton2.TabIndex = 25
        Me.VButton2.Text = "Ingresar Nueva Historia"
        Me.VButton2.UseVisualStyleBackColor = False
        Me.VButton2.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.EXPRESSIONDARK
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(95, 67)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(100, 20)
        Me.TextBox2.TabIndex = 29
        '
        'VButton1
        '
        Me.VButton1.AllowAnimations = True
        Me.VButton1.BackColor = System.Drawing.Color.Transparent
        Me.VButton1.Location = New System.Drawing.Point(220, 44)
        Me.VButton1.Name = "VButton1"
        Me.VButton1.RoundedCornersMask = CType(15, Byte)
        Me.VButton1.Size = New System.Drawing.Size(100, 30)
        Me.VButton1.TabIndex = 24
        Me.VButton1.Text = "Buscar"
        Me.VButton1.UseVisualStyleBackColor = False
        Me.VButton1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.EXPRESSIONDARK
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(95, 31)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 20)
        Me.TextBox1.TabIndex = 28
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(42, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "Nombre:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(28, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 13)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "N° Registro"
        '
        'formBuscarHistoria
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(363, 172)
        Me.Controls.Add(Me.VButton2)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.VButton1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "formBuscarHistoria"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "formBuscarHistoria"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents VButton2 As VIBlend.WinForms.Controls.vButton
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents VButton1 As VIBlend.WinForms.Controls.vButton
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
