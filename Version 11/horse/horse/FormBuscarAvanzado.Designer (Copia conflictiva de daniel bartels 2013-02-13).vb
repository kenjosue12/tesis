<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormBuscarAvanzado
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CBcabeza = New System.Windows.Forms.ComboBox()
        Me.CBpatas = New System.Windows.Forms.ComboBox()
        Me.CBtronco = New System.Windows.Forms.ComboBox()
        Me.Cbraza = New System.Windows.Forms.ComboBox()
        Me.CBservicios = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ChecBpatas = New System.Windows.Forms.CheckBox()
        Me.ChecBtronco = New System.Windows.Forms.CheckBox()
        Me.ChecBcabeza = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.CBsexo = New System.Windows.Forms.ComboBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.CBandares = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Maroon
        Me.Label1.Location = New System.Drawing.Point(195, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(184, 33)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Busqueda Avanzada"
        '
        'CBcabeza
        '
        Me.CBcabeza.FormattingEnabled = True
        Me.CBcabeza.Location = New System.Drawing.Point(104, 48)
        Me.CBcabeza.Name = "CBcabeza"
        Me.CBcabeza.Size = New System.Drawing.Size(121, 21)
        Me.CBcabeza.TabIndex = 12
        '
        'CBpatas
        '
        Me.CBpatas.FormattingEnabled = True
        Me.CBpatas.Location = New System.Drawing.Point(104, 113)
        Me.CBpatas.Name = "CBpatas"
        Me.CBpatas.Size = New System.Drawing.Size(121, 21)
        Me.CBpatas.TabIndex = 13
        '
        'CBtronco
        '
        Me.CBtronco.FormattingEnabled = True
        Me.CBtronco.Location = New System.Drawing.Point(104, 84)
        Me.CBtronco.Name = "CBtronco"
        Me.CBtronco.Size = New System.Drawing.Size(121, 21)
        Me.CBtronco.TabIndex = 14
        '
        'Cbraza
        '
        Me.Cbraza.FormattingEnabled = True
        Me.Cbraza.Location = New System.Drawing.Point(18, 33)
        Me.Cbraza.Name = "Cbraza"
        Me.Cbraza.Size = New System.Drawing.Size(228, 21)
        Me.Cbraza.TabIndex = 15
        '
        'CBservicios
        '
        Me.CBservicios.FormattingEnabled = True
        Me.CBservicios.Location = New System.Drawing.Point(18, 33)
        Me.CBservicios.Name = "CBservicios"
        Me.CBservicios.Size = New System.Drawing.Size(222, 21)
        Me.CBservicios.TabIndex = 16
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ChecBpatas)
        Me.GroupBox1.Controls.Add(Me.ChecBtronco)
        Me.GroupBox1.Controls.Add(Me.ChecBcabeza)
        Me.GroupBox1.Controls.Add(Me.CBtronco)
        Me.GroupBox1.Controls.Add(Me.CBpatas)
        Me.GroupBox1.Controls.Add(Me.CBcabeza)
        Me.GroupBox1.Location = New System.Drawing.Point(23, 66)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(264, 169)
        Me.GroupBox1.TabIndex = 17
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Señas Particulares"
        '
        'ChecBpatas
        '
        Me.ChecBpatas.AutoSize = True
        Me.ChecBpatas.Location = New System.Drawing.Point(13, 117)
        Me.ChecBpatas.Name = "ChecBpatas"
        Me.ChecBpatas.Size = New System.Drawing.Size(53, 17)
        Me.ChecBpatas.TabIndex = 17
        Me.ChecBpatas.Text = "Patas"
        Me.ChecBpatas.UseVisualStyleBackColor = True
        '
        'ChecBtronco
        '
        Me.ChecBtronco.AutoSize = True
        Me.ChecBtronco.Location = New System.Drawing.Point(13, 82)
        Me.ChecBtronco.Name = "ChecBtronco"
        Me.ChecBtronco.Size = New System.Drawing.Size(60, 17)
        Me.ChecBtronco.TabIndex = 16
        Me.ChecBtronco.Text = "Tronco"
        Me.ChecBtronco.UseVisualStyleBackColor = True
        '
        'ChecBcabeza
        '
        Me.ChecBcabeza.AutoSize = True
        Me.ChecBcabeza.Location = New System.Drawing.Point(13, 48)
        Me.ChecBcabeza.Name = "ChecBcabeza"
        Me.ChecBcabeza.Size = New System.Drawing.Size(62, 17)
        Me.ChecBcabeza.TabIndex = 15
        Me.ChecBcabeza.Text = "Cabeza"
        Me.ChecBcabeza.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Cbraza)
        Me.GroupBox2.Location = New System.Drawing.Point(23, 241)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(264, 72)
        Me.GroupBox2.TabIndex = 18
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Raza"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.CBservicios)
        Me.GroupBox3.Location = New System.Drawing.Point(293, 66)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(262, 73)
        Me.GroupBox3.TabIndex = 19
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Servicios"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(189, 336)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(80, 39)
        Me.Button1.TabIndex = 20
        Me.Button1.Text = "BUSCAR"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(299, 336)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(80, 39)
        Me.Button2.TabIndex = 21
        Me.Button2.Text = "CANCELAR"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.CBsexo)
        Me.GroupBox4.Location = New System.Drawing.Point(293, 150)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(262, 73)
        Me.GroupBox4.TabIndex = 20
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Sexo"
        '
        'CBsexo
        '
        Me.CBsexo.FormattingEnabled = True
        Me.CBsexo.Items.AddRange(New Object() {"macho", "hembra"})
        Me.CBsexo.Location = New System.Drawing.Point(18, 33)
        Me.CBsexo.Name = "CBsexo"
        Me.CBsexo.Size = New System.Drawing.Size(222, 21)
        Me.CBsexo.TabIndex = 16
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.CBandares)
        Me.GroupBox5.Location = New System.Drawing.Point(299, 241)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(262, 73)
        Me.GroupBox5.TabIndex = 21
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Andares"
        '
        'CBandares
        '
        Me.CBandares.FormattingEnabled = True
        Me.CBandares.Location = New System.Drawing.Point(18, 33)
        Me.CBandares.Name = "CBandares"
        Me.CBandares.Size = New System.Drawing.Size(222, 21)
        Me.CBandares.TabIndex = 16
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(29, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(259, 13)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "Escoga una de las siguientes opciones de busqueda:"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(405, 336)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(106, 39)
        Me.Button3.TabIndex = 24
        Me.Button3.Text = "ESCOGER ESTE"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(583, 99)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(320, 150)
        Me.DataGridView1.TabIndex = 23
        '
        'FormBuscarAvanzado
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(915, 397)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormBuscarAvanzado"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Busqueda Avanzada"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CBcabeza As System.Windows.Forms.ComboBox
    Friend WithEvents CBpatas As System.Windows.Forms.ComboBox
    Friend WithEvents CBtronco As System.Windows.Forms.ComboBox
    Friend WithEvents Cbraza As System.Windows.Forms.ComboBox
    Friend WithEvents CBservicios As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents CBsexo As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents CBandares As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ChecBpatas As System.Windows.Forms.CheckBox
    Friend WithEvents ChecBtronco As System.Windows.Forms.CheckBox
    Friend WithEvents ChecBcabeza As System.Windows.Forms.CheckBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
End Class
