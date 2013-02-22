<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormAgregar
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.CBasociacion = New System.Windows.Forms.ComboBox()
        Me.CBservicios = New System.Windows.Forms.ComboBox()
        Me.CbcolorPelaje = New System.Windows.Forms.ComboBox()
        Me.CBandar = New System.Windows.Forms.ComboBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.BtnAgregarEditar = New System.Windows.Forms.Button()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.CBraza = New System.Windows.Forms.ComboBox()
        Me.TBpropietario = New System.Windows.Forms.TextBox()
        Me.TBestado = New System.Windows.Forms.TextBox()
        Me.TBregistro = New System.Windows.Forms.TextBox()
        Me.TBnombre = New System.Windows.Forms.TextBox()
        Me.TBid = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(1, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(787, 481)
        Me.TabControl1.TabIndex = 40
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.CBasociacion)
        Me.TabPage1.Controls.Add(Me.CBservicios)
        Me.TabPage1.Controls.Add(Me.CbcolorPelaje)
        Me.TabPage1.Controls.Add(Me.CBandar)
        Me.TabPage1.Controls.Add(Me.Button3)
        Me.TabPage1.Controls.Add(Me.BtnAgregarEditar)
        Me.TabPage1.Controls.Add(Me.DateTimePicker2)
        Me.TabPage1.Controls.Add(Me.DateTimePicker1)
        Me.TabPage1.Controls.Add(Me.RadioButton2)
        Me.TabPage1.Controls.Add(Me.RadioButton1)
        Me.TabPage1.Controls.Add(Me.CBraza)
        Me.TabPage1.Controls.Add(Me.TBpropietario)
        Me.TabPage1.Controls.Add(Me.TBestado)
        Me.TabPage1.Controls.Add(Me.TBregistro)
        Me.TabPage1.Controls.Add(Me.TBnombre)
        Me.TabPage1.Controls.Add(Me.TBid)
        Me.TabPage1.Controls.Add(Me.Label15)
        Me.TabPage1.Controls.Add(Me.Label13)
        Me.TabPage1.Controls.Add(Me.Label12)
        Me.TabPage1.Controls.Add(Me.Label11)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(779, 452)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'CBasociacion
        '
        Me.CBasociacion.FormattingEnabled = True
        Me.CBasociacion.Location = New System.Drawing.Point(470, 231)
        Me.CBasociacion.Margin = New System.Windows.Forms.Padding(4)
        Me.CBasociacion.Name = "CBasociacion"
        Me.CBasociacion.Size = New System.Drawing.Size(179, 24)
        Me.CBasociacion.TabIndex = 77
        '
        'CBservicios
        '
        Me.CBservicios.FormattingEnabled = True
        Me.CBservicios.Location = New System.Drawing.Point(138, 338)
        Me.CBservicios.Margin = New System.Windows.Forms.Padding(4)
        Me.CBservicios.Name = "CBservicios"
        Me.CBservicios.Size = New System.Drawing.Size(179, 24)
        Me.CBservicios.TabIndex = 76
        '
        'CbcolorPelaje
        '
        Me.CbcolorPelaje.FormattingEnabled = True
        Me.CbcolorPelaje.Location = New System.Drawing.Point(140, 288)
        Me.CbcolorPelaje.Margin = New System.Windows.Forms.Padding(4)
        Me.CbcolorPelaje.Name = "CbcolorPelaje"
        Me.CbcolorPelaje.Size = New System.Drawing.Size(179, 24)
        Me.CbcolorPelaje.TabIndex = 75
        '
        'CBandar
        '
        Me.CBandar.FormattingEnabled = True
        Me.CBandar.Location = New System.Drawing.Point(140, 238)
        Me.CBandar.Margin = New System.Windows.Forms.Padding(4)
        Me.CBandar.Name = "CBandar"
        Me.CBandar.Size = New System.Drawing.Size(179, 24)
        Me.CBandar.TabIndex = 74
        '
        'Button3
        '
        Me.Button3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.Location = New System.Drawing.Point(410, 406)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(93, 40)
        Me.Button3.TabIndex = 73
        Me.Button3.Text = "Cancelar"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'BtnAgregarEditar
        '
        Me.BtnAgregarEditar.Location = New System.Drawing.Point(296, 391)
        Me.BtnAgregarEditar.Name = "BtnAgregarEditar"
        Me.BtnAgregarEditar.Size = New System.Drawing.Size(87, 55)
        Me.BtnAgregarEditar.TabIndex = 72
        Me.BtnAgregarEditar.Text = "Guardar"
        Me.BtnAgregarEditar.UseVisualStyleBackColor = True
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Location = New System.Drawing.Point(499, 128)
        Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(265, 22)
        Me.DateTimePicker2.TabIndex = 70
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(470, 340)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(265, 22)
        Me.DateTimePicker1.TabIndex = 69
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(243, 191)
        Me.RadioButton2.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(86, 20)
        Me.RadioButton2.TabIndex = 68
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "Femenino"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(138, 191)
        Me.RadioButton1.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(87, 20)
        Me.RadioButton1.TabIndex = 67
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "Masculino"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'CBraza
        '
        Me.CBraza.FormattingEnabled = True
        Me.CBraza.Location = New System.Drawing.Point(139, 137)
        Me.CBraza.Margin = New System.Windows.Forms.Padding(4)
        Me.CBraza.Name = "CBraza"
        Me.CBraza.Size = New System.Drawing.Size(179, 24)
        Me.CBraza.TabIndex = 66
        '
        'TBpropietario
        '
        Me.TBpropietario.Location = New System.Drawing.Point(470, 290)
        Me.TBpropietario.Margin = New System.Windows.Forms.Padding(4)
        Me.TBpropietario.Name = "TBpropietario"
        Me.TBpropietario.Size = New System.Drawing.Size(180, 22)
        Me.TBpropietario.TabIndex = 65
        '
        'TBestado
        '
        Me.TBestado.Location = New System.Drawing.Point(470, 183)
        Me.TBestado.Margin = New System.Windows.Forms.Padding(4)
        Me.TBestado.Name = "TBestado"
        Me.TBestado.Size = New System.Drawing.Size(180, 22)
        Me.TBestado.TabIndex = 63
        '
        'TBregistro
        '
        Me.TBregistro.Location = New System.Drawing.Point(470, 77)
        Me.TBregistro.Margin = New System.Windows.Forms.Padding(4)
        Me.TBregistro.Name = "TBregistro"
        Me.TBregistro.Size = New System.Drawing.Size(180, 22)
        Me.TBregistro.TabIndex = 57
        '
        'TBnombre
        '
        Me.TBnombre.Location = New System.Drawing.Point(138, 86)
        Me.TBnombre.Margin = New System.Windows.Forms.Padding(4)
        Me.TBnombre.Name = "TBnombre"
        Me.TBnombre.Size = New System.Drawing.Size(180, 22)
        Me.TBnombre.TabIndex = 56
        '
        'TBid
        '
        Me.TBid.Location = New System.Drawing.Point(139, 34)
        Me.TBid.Margin = New System.Windows.Forms.Padding(4)
        Me.TBid.Name = "TBid"
        Me.TBid.Size = New System.Drawing.Size(180, 22)
        Me.TBid.TabIndex = 55
        Me.TBid.Tag = "Defina algo"
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(359, 234)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(133, 28)
        Me.Label15.TabIndex = 54
        Me.Label15.Text = "Asociacion:   "
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(26, 344)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(133, 28)
        Me.Label13.TabIndex = 52
        Me.Label13.Text = "Servicios: "
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(359, 133)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(133, 28)
        Me.Label12.TabIndex = 51
        Me.Label12.Text = "Fecha Nacimiento: "
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(359, 340)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(133, 28)
        Me.Label11.TabIndex = 50
        Me.Label11.Text = "Fecha Registro:  "
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(359, 183)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(133, 28)
        Me.Label10.TabIndex = 49
        Me.Label10.Text = "Estado: "
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(359, 287)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(133, 28)
        Me.Label8.TabIndex = 47
        Me.Label8.Text = "Propietario:  "
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(359, 71)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(100, 28)
        Me.Label7.TabIndex = 46
        Me.Label7.Text = "N° Registro: "
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(26, 90)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(133, 28)
        Me.Label6.TabIndex = 45
        Me.Label6.Text = "Nombre: "
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(26, 141)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(133, 28)
        Me.Label5.TabIndex = 44
        Me.Label5.Text = "Raza: "
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(26, 192)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(133, 28)
        Me.Label4.TabIndex = 43
        Me.Label4.Text = "Sexo: "
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(26, 240)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(133, 28)
        Me.Label3.TabIndex = 42
        Me.Label3.Text = "Andar: "
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(26, 293)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 28)
        Me.Label2.TabIndex = 41
        Me.Label2.Text = "Color Pelaje: "
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(26, 34)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 28)
        Me.Label1.TabIndex = 40
        Me.Label1.Text = "ID: "
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox4)
        Me.TabPage2.Controls.Add(Me.GroupBox3)
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 25)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(779, 452)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Señas Particulares"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(301, 25)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(180, 22)
        Me.TextBox2.TabIndex = 75
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Label14)
        Me.TabPage3.Location = New System.Drawing.Point(4, 25)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(779, 452)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "TabPage3"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(329, 18)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(100, 28)
        Me.Label14.TabIndex = 59
        Me.Label14.Text = "Genealogia: "
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Location = New System.Drawing.Point(29, 15)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(706, 67)
        Me.GroupBox1.TabIndex = 82
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Cabeza"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TextBox2)
        Me.GroupBox2.Location = New System.Drawing.Point(29, 94)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(706, 69)
        Me.GroupBox2.TabIndex = 83
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Tronco"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(301, 30)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(180, 22)
        Me.TextBox1.TabIndex = 74
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.TextBox4)
        Me.GroupBox3.Location = New System.Drawing.Point(29, 169)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(706, 71)
        Me.GroupBox3.TabIndex = 84
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Patas"
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(301, 28)
        Me.TextBox4.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(180, 22)
        Me.TextBox4.TabIndex = 75
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label16)
        Me.GroupBox4.Controls.Add(Me.TextBox3)
        Me.GroupBox4.Location = New System.Drawing.Point(7, 246)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(765, 194)
        Me.GroupBox4.TabIndex = 85
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Imagen"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(171, 94)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(83, 16)
        Me.Label16.TabIndex = 77
        Me.Label16.Text = "BLABLABLA"
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(352, 94)
        Me.TextBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(180, 22)
        Me.TextBox3.TabIndex = 75
        '
        'FormAgregar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(789, 489)
        Me.Controls.Add(Me.TabControl1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MinimizeBox = False
        Me.Name = "FormAgregar"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Agregar Caballo"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents BtnAgregarEditar As System.Windows.Forms.Button
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents CBraza As System.Windows.Forms.ComboBox
    Friend WithEvents TBpropietario As System.Windows.Forms.TextBox
    Friend WithEvents TBestado As System.Windows.Forms.TextBox
    Friend WithEvents TBregistro As System.Windows.Forms.TextBox
    Friend WithEvents TBnombre As System.Windows.Forms.TextBox
    Friend WithEvents TBid As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents CBandar As System.Windows.Forms.ComboBox
    Friend WithEvents CbcolorPelaje As System.Windows.Forms.ComboBox
    Friend WithEvents CBservicios As System.Windows.Forms.ComboBox
    Friend WithEvents CBasociacion As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
End Class
