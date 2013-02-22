Imports horse.utilidades
Imports horse.BD
Imports Npgsql
Imports NpgsqlTypes
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Public Class FormAgregar
    Public ObUtilidades As New utilidades
    Public instruccion As String
    Public conexion As New BD
    Public registro As Integer
    Public nombrePropietario As String
    Public identificacion As Integer
    Public apellidosPropietario As String
    Public fechaRegistro As Date
    Public resultadoSQL As NpgsqlDataReader
    Public cadenaDatosFunc() As String
    Public propitarioExiste As String 'Bandera para saber si ya existe ese propietario que esta tratando de ingresar ocmo nuevo
    Public nombre As String 'Nombre para poner en caso de radio boton "¿es nuevo? ingreso rapido"

    'constructor 
    Public Sub New(ByVal cadeDatosInfo As String, ByVal ins As String)
        instruccion = ins
        Me.InitializeComponent()

        ObUtilidades.Cargar_Combo(CBraza, "select descripcion from raza order by descripcion desc;")
        ObUtilidades.Cargar_Combo(CBandar, "select descripcion from andares order by descripcion desc;")
        ObUtilidades.Cargar_Combo(CbcolorPelaje, "select descripcion from colores order by descripcion desc;")
        ObUtilidades.Cargar_Combo(CBpersonas, "select nombres|| ' ' ||apellidos as propietario  from personas;")
        ObUtilidades.Cargar_Combo(CBasociacion, "select descripcion from asociaciones order by descripcion desc;")
        ObUtilidades.Cargar_Combo(CBservicios, "select descripcion from servicios order by descripcion desc;")

        If instruccion.Equals("a") Then '  si vamos a actualizar o editar un caballo
            GroupBox1.Size = New Size(441, 117) 'group Box CABEZA
            GroupBox2.Size = New Size(441, 117) 'GB tronco
            GroupBox3.Size = New Size(441, 117) 'gb patas
            GroupBox5.Size = New Size(453, 424) 'gb señas particulares
            GroupBox4.Size = New Size(355, 422) 'gb imagen
            PictureBox1.Size = New Size(318, 314)
            GroupBox4.Left = 465
            Call procesaDatosCaballo(cadeDatosInfo)
            DataGridView1.Visible = True 'gv cabeza
            DataGridView2.Visible = True 'gv tronco
            DataGridView3.Visible = True 'gv patas

            'desbloqueamos las tabpages
            bloqueo_tabs.Enabled = False
        End If
        If instruccion.Equals("i") Then ' si vamos a insertar un nuevo caballoo
            GroupBox1.Size = New Size(308, 117)
            GroupBox2.Size = New Size(308, 117)
            GroupBox3.Size = New Size(308, 117)
            GroupBox5.Size = New Size(320, 424)
            GroupBox4.Size = New Size(474, 422)
            GroupBox4.Left = 349
            PictureBox1.Size = New Size(453, 314) '453
            CBcabeza.Visible = True
            CBtronco.Visible = True
            CBpatas.Visible = True
            DataGridView1.Visible = False
            DataGridView2.Visible = False
            DataGridView3.Visible = False
            'conexion.Conectar()
            ' Dim idCuenta As String
            TBid.Text = ObUtilidades.IncrementaCodigo("select max(idcaballos) as id from caballos") + 1
            TBid.Text.ToString()
            'Dim cadenaSQL = "select nombre from caballos where idcaballos='" + idCuenta + "';"
            'Dim resultadoSQL As NpgsqlDataReader = conexion.consultar(cadenaSQL, conexion.conexionBD)
            'If resultadoSQL.Read Then

            '    Dim resultado = resultadoSQL.GetString(0)
            'End If
            'conexion.desconectar()

        End If
        If instruccion.Equals("g") Then ' si vamos a editar un caballo nuevo osea acabado de crear en genialogia......
            Call cab_de_genealogia(cadeDatosInfo)
        End If

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName

            ' TODO: agregue código aquí para abrir el archivo.
        End If
    End Sub

    Public Function cab_de_genealogia(ByVal id_caba_genealo) 'esta funcion lo unico que hace es sacar el nombre asociado al caballo de genealogia que se piensa editar o completar la demas informacion, y tambien pinta el id del caballo en el campo que se debe pintar

        ObUtilidades.Cargar_Combo(CBraza, "select descripcion from raza;")
        TBid.Text = id_caba_genealo
        LBidcaballos.Text = id_caba_genealo 'para saber en señas particulares si existe o no un caballo....
        LB_id_caballo.Text = id_caba_genealo 'para la parte de genealogia
        conexion.Conectar()
        Dim nombre_caba = ""
        Dim cadenaSQL = "select nombre from caballos where idcaballos='" + id_caba_genealo + "';"
        Dim resultadoSQLservi As NpgsqlDataReader = conexion.consultar(cadenaSQL, conexion.conexionBD)

        If resultadoSQLservi.Read Then
            If Not (resultadoSQLservi.IsDBNull(0)) Then 'lo que sucede es q cuando el registro viene vacio.. pues daba error tocaba validarlo de esta manera gx gx gx
                nombre_caba = resultadoSQLservi.GetString(0)
            End If
        End If
        conexion.desconectar()

        TBnombre.Text = nombre_caba
        LB_nombre_caba.Text = nombre_caba 'para la parte de genealogia
        LBnombreCaballo.Text = nombre_caba
    End Function
    'Funcion que pinta los datos que tiene el caballo seleccionado, en el formulario EDITAR pestaña 1
    Public Function procesaDatosCaballo(ByVal cadenaDatos)
        cadenaDatosFunc = Split(cadenaDatos, "*")


        TBid.Text = cadenaDatosFunc(0)
        TBregistro.Text = cadenaDatosFunc(1)

        CBraza.Text = cadenaDatosFunc(2)
        TBnombre.Text = cadenaDatosFunc(3)

        CBandar.Text = cadenaDatosFunc(5)
        'sexo-4
        CBsexos.Text = cadenaDatosFunc(4)

        CbcolorPelaje.Text = cadenaDatosFunc(6)

        'pone la fecha de registro
        DateTimePicker2.Text = cadenaDatosFunc(7)
        DateTimePicker1.Text = cadenaDatosFunc(8)

        CBestado.Text = cadenaDatosFunc(9)

        CBpersonas.Text = cadenaDatosFunc(10)

        CBasociacion.Text = cadenaDatosFunc(11)

        CBservicios.Text = cadenaDatosFunc(13)
        'CBraza.Selection = cadenaDatosFunc(2)

        LBidcaballos.Text = cadenaDatosFunc(0) 'para saber en señas particulares si existe o no un caballo....
        LB_id_caballo.Text = cadenaDatosFunc(0) 'para la parte de genealogia
        LB_nombre_caba.Text = cadenaDatosFunc(3) 'para la parte de genealogia
        LBnombreCaballo.Text = cadenaDatosFunc(3)
        'Me.PictureBox1.Image = Image.FromFile(cadenaDatosFunc(10))
        'Me.PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        'Me.PictureBox1.BorderStyle = BorderStyle.Fixed3D
        Call ClearMemory()
        Call llenarListView(cadenaDatosFunc(0))

    End Function
    'LIMPIAR MEMORIA PARA 
    Private Declare Auto Function SetProcessWorkingSetSize Lib "kernel32.dll" (ByVal procHandle As IntPtr, ByVal min As Int32, ByVal max As Int32) As Boolean
    Public Sub ClearMemory()

        Try
            Dim Mem As Process
            Mem = Process.GetCurrentProcess()
            SetProcessWorkingSetSize(Mem.Handle, -1, -1)
        Catch ex As Exception
            'Control de errores
        End Try

    End Sub

    'BOTON PARA INSERTAR O ACTUALIZAR CABALLOS O DATOS DE CABALLOS
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles BtnAgregarEditar.Click

        'si checkbox esta seleccionado ingresa el numero de registro
        If CBsiPoseeNumRegi.Checked = True Then
            registro = TBregistro.Text
        Else 'si no, pues envia un cero.
            registro = 0
        End If   'fin de instruccion para validacion de numero de registro

        If instruccion.Equals("a") Then
            Call insertar("a") '"a" de actualizar
        End If

        If instruccion.Equals("i") Then
            Call insertar("i") ' "i" de insertar
        End If

        If instruccion.Equals("g") Then
            Call insertar("a") ' "g" de editar un caballo de genealogia
        End If
        BtnExaminar.Enabled = True


    End Sub
    'FUNCION INSERTAR: INSERTA CABALLOS, Y ACTUALIZAR
    Public Function insertar(ByVal comando As String)
        Dim com = comando
        Dim cadenaSQLorden
        conexion.Conectar()
        Dim resuland As Integer
        Dim resulcolor As Integer
        Dim resulraza As Integer
        Dim resulasoci As Integer
        Dim resulservici As Integer
        Dim resulsEstado As String
        Dim resulPropietario As Integer
        Dim TBnombre As String = ""

        'DATOS DE ANDARES
        Dim cadenaSQLandar = "select idandares from andares where descripcion='" + CBandar.Text + "';"
        Dim resultadoSQLandar As NpgsqlDataReader = conexion.consultar(cadenaSQLandar, conexion.conexionBD)
        If resultadoSQLandar.Read Then
            If resultadoSQLandar.GetInt32(0) = 0 Then
                MsgBox("fue cero")
            End If
            resuland = resultadoSQLandar.GetInt32(0)
        End If

        conexion.desconectar()

        'DATOS DE COLORES
        conexion.Conectar()
        Dim cadenaSQLcolor = "select idcolores from colores where descripcion='" + CbcolorPelaje.Text + "';"
        Dim resultadoSQLcolor As NpgsqlDataReader = conexion.consultar(cadenaSQLcolor, conexion.conexionBD)
        If resultadoSQLcolor.Read Then
            resulcolor = resultadoSQLcolor.GetInt32(0)
        End If
        conexion.desconectar()

        'DATOS DE RAZA
        conexion.Conectar()
        Dim cadenaSQLraza = "select idraza from raza where descripcion='" + CBraza.Text + "';"
        Dim resultadoSQLraza As NpgsqlDataReader = conexion.consultar(cadenaSQLraza, conexion.conexionBD)
        If resultadoSQLraza.Read Then
            resulraza = resultadoSQLraza.GetInt32(0)
        End If
        conexion.desconectar()

        'DATOS DE ASOCIACIONES
        conexion.Conectar()
        Dim cadenaSQLasocia = "select idasociaciones from asociaciones where descripcion='" + CBasociacion.Text + "';"
        Dim resultadoSQLasocia As NpgsqlDataReader = conexion.consultar(cadenaSQLasocia, conexion.conexionBD)
        If resultadoSQLasocia.Read Then
            resulasoci = resultadoSQLasocia.GetInt32(0)
        End If
        conexion.desconectar()

        'DATOS DE SERVICIOS
        conexion.Conectar()

        conexion.desconectar()

        'DATOS DE ESTADO
        conexion.Conectar()

        Dim cadenaSQLestado = "select idestado_caballo from estado_caballos where descripcion='" + CBestado.Text + "';"
        Dim resultadoSQLestado As NpgsqlDataReader = conexion.consultar(cadenaSQLestado, conexion.conexionBD)
        If resultadoSQLestado.Read Then
            resulsEstado = resultadoSQLestado.GetString(0)
        End If
        conexion.desconectar()

        'DATOS DE PROPIETARIO
        conexion.Conectar()

        If TBnombreProp.Text = "" And TBapellidosProp.Text = "" And TBidentifi.Text = "" Then
            Dim cadenaSQLpropietario = "select identificacion from personas where nombres='" + CBpersonas.Text + "';"
            Dim resultadoSQLpropietario As NpgsqlDataReader = conexion.consultar(cadenaSQLpropietario, conexion.conexionBD)
            If resultadoSQLpropietario.Read Then
                resulPropietario = resultadoSQLpropietario.GetInt32(0)
            End If
        End If
        If TBnombreProp.Text <> "" And TBapellidosProp.Text <> "" And TBidentifi.Text <> "" Then
            nombrePropietario = TBnombreProp.Text
            apellidosPropietario = TBapellidosProp.Text
            If IsNumeric(TBidentifi.Text) = True Then
                identificacion = CInt(TBidentifi.Text)
                'identificacion = Integer.Parse(TBidentifi.Text)
                Try
                    cadenaSQLorden = "Insert into personas(identificacion,nombres,apellidos) values (@identificacion, @nombres,@apellidos) "
                    Dim cmd As New NpgsqlCommand(cadenaSQLorden, conexion.conexionBD)
                    With cmd
                        .Parameters.Add("@identificacion", NpgsqlDbType.Integer).NpgsqlValue = identificacion
                        .Parameters.Add("@nombres", NpgsqlDbType.Varchar, 30).NpgsqlValue = nombrePropietario
                        .Parameters.Add("@apellidos", NpgsqlDbType.Varchar, 30).NpgsqlValue = apellidosPropietario
                    End With
                    cmd.ExecuteNonQuery()
                    resulPropietario = identificacion
                Catch sqlExc As NpgsqlException
                    ' MsgBox("esta persona ya existe")
                    resulPropietario = 1
                    'MessageBox.Show(sqlExc.ToString, "SQL Exception Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            End If
        End If

        conexion.desconectar()


        If CBsiPoseeNumRegi.Checked = True Then
            fechaRegistro = Me.DateTimePicker1.Value.Date
        Else
            fechaRegistro = "01/01/1753"
        End If

        'Este primer IF, no permite insertar en la base de datos si los campos de Estado y Nombre estan vacios
        If Me.TBnombre.Text = "" Then
            MsgBox("Los datos con (*) son obligatorio, ingrese el Nombre", MsgBoxStyle.Exclamation)
            Label35.ForeColor = Color.Red

        ElseIf resulsEstado = "" Then
            MsgBox("Los datos con (*) son obligatorio, ingrese el Estado", MsgBoxStyle.Exclamation)
            Label34.ForeColor = Color.Red

        ElseIf TBidentifi.Text <> "" And IsNumeric(TBidentifi.Text) = False Then
            MsgBox("La Identificacion debe de ser un Numero")

        ElseIf resulPropietario.Equals(1) Then
            MessageBox.Show("Persona existe en la base de datos. Por favor, seleccionela en la opcion *ingresar*", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ElseIf CBsexos.Text = "" Then
            MsgBox("Los datos con (*) son obligatorio, ingrese el Sexo", MsgBoxStyle.Exclamation)
            Label14.ForeColor = Color.Red
        Else
            Try
                If conexion.Conectar() = False Then
                    MsgBox("Problemas en la Conexión", MsgBoxStyle.Exclamation)
                    conexion.desconectar()
                End If
                Dim cadenaSQLservi = "select idservicios from servicios where descripcion='" + CBservicios.Text + "';"
                Dim resultadoSQLservi As NpgsqlDataReader = conexion.consultar(cadenaSQLservi, conexion.conexionBD)
                If resultadoSQLservi.Read Then
                    resulservici = resultadoSQLservi.GetInt32(0)

                End If
                If com.Equals("i") Then
                    cadenaSQLorden = "Insert Into caballos(idcaballos,nombre,estado,numero_registro,raza,sexo,andar,color_pelaje,fecha_nacimiento,fecha_registro,propietario,asociacion,servicios)Values(@id,@nombre,@estado,@nregistro,@raza,@sexo,@andar,@colorPel,@fnacimi,@fregis,@propie,@asoci,@servici)"

                Else
                    cadenaSQLorden = "Update caballos SET nombre=@nombre,numero_registro=@nregistro,raza=@raza,estado=@estado,sexo=@sexo,andar=@andar,color_pelaje=@colorPel,fecha_nacimiento=@fnacimi,fecha_registro=@fregis,propietario=@propie,asociacion=@asoci,servicios=@servici where idcaballos=@id"
                End If

                Dim cmd As New NpgsqlCommand(cadenaSQLorden, conexion.conexionBD)
                With cmd
                    .Parameters.Add("@id", NpgsqlDbType.Integer).NpgsqlValue = Me.TBid.Text
                    .Parameters.Add("@nregistro", NpgsqlDbType.Integer, 50).NpgsqlValue = registro
                    .Parameters.Add("@raza", NpgsqlDbType.Integer).NpgsqlValue = resulraza
                    .Parameters.Add("@nombre", NpgsqlDbType.Varchar).NpgsqlValue = Me.TBnombre.Text
                    .Parameters.Add("@sexo", NpgsqlDbType.Varchar, 50).NpgsqlValue = Me.CBsexos.Text
                    .Parameters.Add("@andar", NpgsqlDbType.Integer).NpgsqlValue = resuland
                    .Parameters.Add("@colorPel", NpgsqlDbType.Integer).NpgsqlValue = resulcolor
                    .Parameters.Add("@fnacimi", NpgsqlDbType.Date, 100).NpgsqlValue = Me.DateTimePicker2.Value
                    .Parameters.Add("@fregis", NpgsqlDbType.Date, 100).NpgsqlValue = fechaRegistro
                    .Parameters.Add("@estado", NpgsqlDbType.Varchar).NpgsqlValue = resulsEstado
                    .Parameters.Add("@propie", NpgsqlDbType.Integer).NpgsqlValue = resulPropietario
                    .Parameters.Add("@asoci", NpgsqlDbType.Integer).NpgsqlValue = resulasoci
                    '.Parameters.Add("@genea", NpgsqlDbType.Varchar, 100).NpgsqlValue = "null"
                    .Parameters.Add("@servici", NpgsqlDbType.Integer).NpgsqlValue = resulservici
                    ''.Parameters.Add("@separtic", NpgsqlDbType.Varchar, 50).NpgsqlValue = "null"
                    ''.Parameters.Add(New NpgsqlParameter("@imagen", NpgsqlDbType.Bytea)).NpgsqlValue = arrImage
                End With
                cmd.ExecuteNonQuery()
                If com.Equals("i") Then
                    'cuando se INSERTE un caballo se actualice automaticamente en la pestaña señas y genealogia, los datos de ese caballo para no tener que cerrar y abrir la ventana de nuevo.
                    LBidcaballos.Text = TBid.Text
                    LBnombreCaballo.Text = Me.TBnombre.Text
                    LB_id_caballo.Text = TBid.Text
                    LB_nombre_caba.Text = Me.TBnombre.Text

                    MessageBox.Show("Se ha Guardado el registro correctamente", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    bloqueo_tabs.Enabled = False 'desbloqueo de taps
                    If MsgBox("¿Desea ingresar ahora los Datos Señas Particulares?", MsgBoxStyle.YesNo, "Desbloqueado Señas Particulares") = MsgBoxResult.Yes Then
                        TabControl1.SelectedTab = TabPage2
                    End If
                Else
                    'aqui lo mismo pero para cuando se ACTUALICE.
                    LBidcaballos.Text = TBid.Text
                    LBnombreCaballo.Text = Me.TBnombre.Text
                    LB_id_caballo.Text = TBid.Text
                    LB_nombre_caba.Text = Me.TBnombre.Text

                    MessageBox.Show("Se ha actualizado el registro correctamente", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    llenarListView(TBid.Text)
                    If MsgBox("¿Desea actualizar ahora los Datos Señas Particulares?", MsgBoxStyle.YesNo, "Desbloqueado Señas Particulares") = MsgBoxResult.Yes Then

                        TabControl1.SelectedTab = TabPage2
                    End If
                End If

                'Call Cargar_Combo(ComboBox1, "select codigo from logos order by codigo;")
            Catch sqlExc As NpgsqlException

                MessageBox.Show(sqlExc.ToString, "SQL Exception Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                If ConnectionState.Open Then
                    conexion.desconectar()
                End If
            End Try

        End If

    End Function
    'LOAD DEL FORMULARIO
    'CONTROLES U OBJETOS QUE VENDRAR PRECARGADOS CUANDO EL FORMULARIO SE ABRA
    Private Sub FormAgregar_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'TabPage2.Parent = Nothing
        'TabPage2.Parent=TabControl1
        'incrementa codigo funcion. Solo se utiliza aca en el load. 

        TBregistro.Enabled = False
        TBcodigo.Text = ObUtilidades.IncrementaCodigo("select max(codigo)as id from logos") + 1 'para la imagen del caballo se usa
        DateTimePicker1.Enabled = False
        'CBsiPoseeNumRegi.Checked = False
        Me.BtnInsertar.Enabled = False
        Call ObUtilidades.Cargar_Combo(CBcabeza, "select distinct descripcion from senales_particulares where ubicacion='1'  order by descripcion desc;")
        Call ObUtilidades.Cargar_Combo(CBtronco, "select distinct descripcion from senales_particulares  where ubicacion='2' order by descripcion desc;")
        Call ObUtilidades.Cargar_Combo(CBpatas, "select distinct descripcion from senales_particulares  where ubicacion='3'  order by descripcion desc;")

        TBnombreProp.Visible = False
        TBapellidosProp.Visible = False
        TBidentifi.Visible = False
        RadioButton4.Checked = True 'seleccionamos el radio button de ingresar


        If instruccion.Equals("a") Then '  si vamos a actualizar o editar un caballo
            If cadenaDatosFunc(1).Equals("0") Then
                CBsiPoseeNumRegi.Checked = False
            Else
                TBregistro.Enabled = True
                CBsiPoseeNumRegi.CheckState = CheckState.Checked
                DateTimePicker1.Enabled = True
                CBsiPoseeNumRegi.Checked = True
            End If
        End If
        If LBidcaballos.Text = "" Then
            BtnExaminar.Enabled = False
        End If


    End Sub
    Public Function llenarListView(caballo As String) 'LLENA DATAGRID VIEW.
        'Dim column1 As New System.Windows.Forms.ColumnHeader
        'Dim column2 As New System.Windows.Forms.ColumnHeader
        'column1.Text = "Ubicacion"

        'column2.Text = "Descripcion Seña Particular"

        'column1.Width = 100
        'column2.Width = 100

        'ListView1.Columns.Clear() 'siempre es importante asegurarse que no existan columnas y para eso es mejor borrarlas antes de agregar las nuevas columnas

        ''Ahora agregamos las columnas nuevas
        'ListView1.Columns.Add(column1)
        'ListView1.Columns.Add(column2)
        'ListView1.View = View.Details

        'ahora llenamos el listview

        Dim cadenaSQL As String = "select s.descripcion from senales_particulares s,caballos c,tipo_ubicacion_senas_particulares tu where s.ubicacion=id_tipo_ubicacion and id_tipo_ubicacion=1 and id_caballo=c.idcaballos and c.idcaballos='" + caballo + "';"
        conexion.Conectar()
        Dim ds As New DataSet
        Dim DA As New NpgsqlDataAdapter(cadenaSQL, conexion.conexionBD)
        DA.Fill(ds)
        DataGridView1.DataSource = ds.Tables(0)

        Dim consultaTronco As String = "select s.descripcion from senales_particulares s,caballos c,tipo_ubicacion_senas_particulares tu where s.ubicacion=id_tipo_ubicacion and id_tipo_ubicacion=2 and id_caballo=c.idcaballos and c.idcaballos='" + caballo + "';"
        Dim dsTronco As New DataSet
        Dim DATronco As New NpgsqlDataAdapter(consultaTronco, conexion.conexionBD)
        DATronco.Fill(dsTronco)
        DataGridView2.DataSource = dsTronco.Tables(0)

        Dim consultaPatas As String = "select s.descripcion from senales_particulares s,caballos c,tipo_ubicacion_senas_particulares tu where s.ubicacion=id_tipo_ubicacion and id_tipo_ubicacion=3 and id_caballo=c.idcaballos and c.idcaballos='" + caballo + "';"
        Dim dsPatas As New DataSet
        Dim DAPatas As New NpgsqlDataAdapter(consultaPatas, conexion.conexionBD)
        DAPatas.Fill(dsPatas)
        DataGridView3.DataSource = dsPatas.Tables(0)

        'ListView1.Items.Clear()
        'Dim i As Integer
        'For i = 0 To ds.Tables(0).Rows.Count - 1
        '    ListView1.Items.Add(ds.Tables(0).Rows(i)("descripcion"))
        '    ListView1.Items(i).SubItems.Add(ds.Tables(0).Rows(i)("ubicacion"))
        'Next
        'ds.Tables("senales_particulares").Clear()
        conexion.desconectar()

    End Function
    'BOTON PARA EXAMINAR ENTRE LOS DOCUMENTOS LA IMAGEN A INSERTAR 
    Private Sub Button6_Click(ByVal sender As Object, e As EventArgs) Handles BtnExaminar.Click
        Dim OpenFD As New OpenFileDialog
        With OpenFD
            .InitialDirectory = ""
            .FileName = "Todos los Archivos"
            .Filter = "Todos los Archivos|*.*|JPEGs|*.jpg|GIFs|*.gif|Bitmaps|*.bmp"
            .FilterIndex = 2
        End With
        If OpenFD.ShowDialog() = Windows.Forms.DialogResult.OK Then
            With PictureBox1
                .Image = Image.FromFile(OpenFD.FileName)
                .SizeMode = PictureBoxSizeMode.StretchImage
                .BorderStyle = BorderStyle.Fixed3D
                Me.BtnInsertar.Enabled = True
            End With
        End If
    End Sub
    'BOTON PARA INSERTAR LA IMAGEN
    Private Sub BtnInsertar_Click(sender As Object, e As EventArgs) Handles BtnInsertar.Click
        insertar()

    End Sub
    'Insertar IMAGEN
    Public Function insertar()
        Try
            If conexion.Conectar() = False Then
                MsgBox("Problemas en la Conexion", MsgBoxStyle.Exclamation)
                Me.Close()
            End If
            Dim arrFilename() As String = Split(Text, "\")
            Array.Reverse(arrFilename)

            Dim ms As New MemoryStream
            PictureBox1.Image.Save(ms, PictureBox1.Image.RawFormat)
            Dim arrImage() As Byte = ms.GetBuffer
            'guarda la imagen en la tabla "logos"
            Dim cmd As New NpgsqlCommand("Insert Into logos(codigo,Imagen)Values(@cod,@imagen)", conexion.conexionBD)
            With cmd
                .Parameters.Add("@cod", NpgsqlDbType.Integer).NpgsqlValue = Val(Me.TBcodigo.Text)
                .Parameters.Add(New NpgsqlParameter("@imagen", NpgsqlDbType.Bytea)).NpgsqlValue = arrImage
            End With
            cmd.ExecuteNonQuery() 'ejecuta la instruccion SQl
            'Guarda y relaciona la imagen recien guardada en "logos", en el campo "foto" de la tabla "caballos" para el caballo que se este editando
            Dim cmd2 As New NpgsqlCommand("Update caballos SET foto=@cod where idcaballos='" + Me.LBidcaballos.Text + "';", conexion.conexionBD)
            With cmd2
                .Parameters.Add("@cod", NpgsqlDbType.Integer).NpgsqlValue = Me.TBcodigo.Text
            End With
            cmd2.ExecuteNonQuery()
            MessageBox.Show("Se ha guardado el registro correctamente", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch sqlExc As NpgsqlException
            MessageBox.Show(sqlExc.ToString, "SQL Exception Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If ConnectionState.Open Then
                conexion.desconectar()
            End If
        End Try
    End Function
    'BOTON PARA AGREGAR SEÑA PARTICULAR DE LA CABEZA
    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        If LBidcaballos.Text <> "" Then
            Dim señas As New FormSenasParticulares(LBidcaballos.Text, "c", Me)
            señas.ShowDialog()
        Else
            MessageBox.Show("Por favor, primero ingrese los Datos Generales de un caballo", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

    End Sub
    'BOTON PARA AGREGAR SEÑA PARTICULAR DEL TRONCO
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If LBidcaballos.Text <> "" Then
            Dim señas As New FormSenasParticulares(LBidcaballos.Text, "t", Me)
            señas.ShowDialog()
        Else
            MessageBox.Show("Por favor, primero ingrese los Datos Generales de un caballo", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

    End Sub
    'BOTON PARA AGREGAR SEÑA PARTICULAR DE LAS PATAS
    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        If LBidcaballos.Text <> "" Then
            Dim señas As New FormSenasParticulares(LBidcaballos.Text, "p", Me)
            señas.ShowDialog()
        Else
            MessageBox.Show("Por favor, primero ingrese los Datos Generales de un caballo", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

    End Sub
    'BOTON PARA GUARDAR LOS CAMBIOS EN LA PESTAÑA SEÑAS PARTICULARES, BOTON GUARDAR.
    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click
        Dim codigoIncrementado As String
        Dim descripcion As String
        Dim uicacion As String
        Dim patas As String
        Dim idcaba As String 'ID de caballo
        Dim idubicacion As String 'ID de cabeza
        Dim bandera As Integer
        idcaba = LBidcaballos.Text

        'toca validar que exista el id de ese caballo primero 
        If idcaba <> "" Then

            If Not (CBcabeza.Text.Equals("Seleccione Una Opcion") Or CBcabeza.Text.Equals("")) Then
                idubicacion = "1" ' es una seña en la cabeza
                ' primero debemos comprobar que la seña q estan seleccionando el caballo no la tenga porke si la tiene no la vamos a guardar en la base de datos y debmos avisar al usuario
                Dim sena_Seleccionada = CBcabeza.Text
                conexion.Conectar()
                Dim cadenaSQLpatas = "select idsenales_particulares from senales_particulares where descripcion='" + sena_Seleccionada + "' and ubicacion='1' and id_caballo='" + idcaba + "';"
                Dim resultadoSQLpatas As NpgsqlDataReader = conexion.consultar(cadenaSQLpatas, conexion.conexionBD)
                If resultadoSQLpatas.Read Then
                    MsgBox("lo sentimos el caballo ya tiene esta sena")
                Else
                    'es porke este caballini no tiene esa seña y debemos guardarlaaaaaaaaaa
                    Try
                        If conexion.Conectar() = False Then
                            MsgBox("Problemas en la Conexión", MsgBoxStyle.Exclamation)
                            Me.Close()
                        End If

                        Dim cmd1 As New NpgsqlCommand("Insert into senales_particulares (descripcion,ubicacion,id_caballo) VALUES (@descripcion,@ubicacion,@id_caballo) ;", conexion.conexionBD)
                        With cmd1
                            .Parameters.Add("@descripcion", NpgsqlDbType.Varchar).NpgsqlValue = CBcabeza.Text
                            .Parameters.Add("@ubicacion", NpgsqlDbType.Integer).NpgsqlValue = 1
                            .Parameters.Add("@id_caballo", NpgsqlDbType.Integer).NpgsqlValue = idcaba
                        End With
                        cmd1.ExecuteNonQuery()
                        MessageBox.Show("Se ha guardado el registro Señal Particular Cabeza correctamente", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        llenarListView(idcaba)
                    Catch sqlExc As NpgsqlException
                        MessageBox.Show(sqlExc.ToString, "SQL Exception Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    Finally
                        If ConnectionState.Open Then
                            conexion.desconectar()
                        End If
                    End Try
                End If
                conexion.desconectar()
            Else
                bandera = 1 ' si la bandera esta en 3 es porke no selecciono ningun seña y toca avisarle al usuario
            End If

            If Not (CBtronco.Text.Equals("Seleccione Una Opcion") Or CBtronco.Text.Equals("")) Then
                MsgBox("hola")
                idubicacion = "1" ' es una seña en la cabeza
                ' primero debemos comprobar que la seña q estan seleccionando el caballo no la tenga porke si la tiene no la vamos a guardar en la base de datos y debmos avisar al usuario
                Dim sena_Seleccionada = CBtronco.Text
                conexion.Conectar()
                Dim cadenaSQLpatas = "select idsenales_particulares from senales_particulares where descripcion='" + sena_Seleccionada + "' and ubicacion='2' and id_caballo='" + idcaba + "';"
                Dim resultadoSQLpatas As NpgsqlDataReader = conexion.consultar(cadenaSQLpatas, conexion.conexionBD)
                If resultadoSQLpatas.Read Then
                    MsgBox("lo sentimos el caballo ya tiene esta sena")
                Else
                    'es porke este caballini no tiene esa seña y debemos guardarlaaaaaaaaaa
                    Try
                        If conexion.Conectar() = False Then
                            MsgBox("Problemas en la Conexión", MsgBoxStyle.Exclamation)
                            Me.Close()
                        End If

                        Dim cmd1 As New NpgsqlCommand("Insert into senales_particulares (descripcion,ubicacion,id_caballo) VALUES (@descripcion,@ubicacion,@id_caballo) ;", conexion.conexionBD)
                        With cmd1
                            .Parameters.Add("@descripcion", NpgsqlDbType.Varchar).NpgsqlValue = CBtronco.Text
                            .Parameters.Add("@ubicacion", NpgsqlDbType.Integer).NpgsqlValue = 2
                            .Parameters.Add("@id_caballo", NpgsqlDbType.Integer).NpgsqlValue = idcaba
                        End With
                        cmd1.ExecuteNonQuery()
                        MessageBox.Show("Se ha guardado el registro Señal Particular Tronco correctamente", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        llenarListView(idcaba)
                    Catch sqlExc As NpgsqlException
                        MessageBox.Show(sqlExc.ToString, "SQL Exception Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    Finally
                        If ConnectionState.Open Then
                            conexion.desconectar()
                        End If
                    End Try
                End If
                conexion.desconectar()
            Else
                bandera += 1 ' si la bandera esta en 3 es porke no selecciono ningun seña y toca avisarle al usuario
            End If


            If Not (CBpatas.Text.Equals("Seleccione Una Opcion") Or CBpatas.Text.Equals("")) Then
                MsgBox("opcion patas")
                idubicacion = "1" ' es una seña en la cabeza
                ' primero debemos comprobar que la seña q estan seleccionando el caballo no la tenga porke si la tiene no la vamos a guardar en la base de datos y debmos avisar al usuario
                Dim sena_Seleccionada = CBpatas.Text
                conexion.Conectar()
                Dim cadenaSQLpatas = "select idsenales_particulares from senales_particulares where descripcion='" + sena_Seleccionada + "' and ubicacion='3' and id_caballo='" + idcaba + "';"
                Dim resultadoSQLpatas As NpgsqlDataReader = conexion.consultar(cadenaSQLpatas, conexion.conexionBD)
                If resultadoSQLpatas.Read Then
                    MsgBox("lo sentimos el caballo ya tiene esta sena")
                Else
                    'es porke este caballini no tiene esa seña y debemos guardarlaaaaaaaaaa
                    Try
                        If conexion.Conectar() = False Then
                            MsgBox("Problemas en la Conexión", MsgBoxStyle.Exclamation)
                            Me.Close()
                        End If

                        Dim cmd1 As New NpgsqlCommand("Insert into senales_particulares (descripcion,ubicacion,id_caballo) VALUES (@descripcion,@ubicacion,@id_caballo) ;", conexion.conexionBD)
                        With cmd1
                            .Parameters.Add("@descripcion", NpgsqlDbType.Varchar).NpgsqlValue = CBpatas.Text
                            .Parameters.Add("@ubicacion", NpgsqlDbType.Integer).NpgsqlValue = 3
                            .Parameters.Add("@id_caballo", NpgsqlDbType.Integer).NpgsqlValue = idcaba
                        End With
                        cmd1.ExecuteNonQuery()
                        MessageBox.Show("Se ha guardado el registro Señal Particular Patas correctamente", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        llenarListView(idcaba)
                    Catch sqlExc As NpgsqlException
                        MessageBox.Show(sqlExc.ToString, "SQL Exception Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    Finally
                        If ConnectionState.Open Then
                            conexion.desconectar()
                        End If
                    End Try
                End If
                conexion.desconectar()
            Else
                bandera += 1 ' si la bandera esta en 3 es porke no selecciono ningun seña y toca avisarle al usuario
            End If

            If bandera = 3 Then
                MsgBox("No seleccionó ninguna seña")
            Else
                'finalmente preguntamos si desea ingresar los datos de genealogia, si es asi, lo lleva automaticamente a la pestaña Genealogia. De lo contrario still here.
                If MsgBox("¿Desea ingresar ahora los Datos de Genalogia?", MsgBoxStyle.YesNo, "Desbloqueado Genealogia") = MsgBoxResult.Yes Then
                    TabControl1.SelectedTab = TabPage3
                End If
            End If

        Else
            MsgBox("Por favor, Primero diligencie los Datos Generales del caballo y haga click en guardar", MsgBoxStyle.Information)
        End If

    End Sub

    'funcion que recarga o trae el pariente asociado a un caballo
    Public Function traer_pariente(ByVal id_caballo, ByVal pariente) As String
        Dim retornito As String = "Sin Asignar"
        'sacamos el id de genealogia asociado al caballo
        conexion.Conectar()
        Dim id_genealo As String = ""
        Dim cadenaSQL As String = "select genealogia from caballos where idcaballos='" + id_caballo + "';"
        Dim resultadoSQLservi As NpgsqlDataReader = conexion.consultar(cadenaSQL, conexion.conexionBD)

        If resultadoSQLservi.Read Then
            If Not (resultadoSQLservi.IsDBNull(0)) Then 'lo que sucede es q cuando el registro viene vacio.. pues daba error tocaba validarlo de esta manera gx gx gx
                id_genealo = resultadoSQLservi.GetInt32(0)
            End If
        End If
        conexion.desconectar()

        If Not (id_genealo.Equals("")) Then ' si existe un id_genealogia de ese caballo
            conexion.Conectar()
            Dim id_pariente As String = ""
            cadenaSQL = "select " + pariente + " from genealogia where idgenealogia='" + id_genealo + "';"
            Dim resultadoSQLservi1 As NpgsqlDataReader = conexion.consultar(cadenaSQL, conexion.conexionBD)
            If resultadoSQLservi1.Read Then
                If Not (resultadoSQLservi1.IsDBNull(0)) Then 'lo que sucede es q cuando el registro viene vacio.. pues daba error tocaba validarlo de esta manera gx gx gx
                    id_pariente = resultadoSQLservi1.GetInt32(0)
                End If
            End If
            conexion.desconectar()

            If Not (id_pariente.Equals("")) Then ' si tenemos este pariente
                Dim nombre_pariente As String = ""
                conexion.Conectar()
                cadenaSQL = "select nombre from caballos where idcaballos='" + id_pariente + "';"
                Dim resultadoSQLservi3 As NpgsqlDataReader = conexion.consultar(cadenaSQL, conexion.conexionBD)

                If resultadoSQLservi3.Read Then
                    If Not (resultadoSQLservi3.IsDBNull(0)) Then 'lo que sucede es q cuando el registro viene vacio.. pues daba error tocaba validarlo de esta manera gx gx gx
                        nombre_pariente = resultadoSQLservi3.GetString(0)
                    End If
                End If
                conexion.desconectar()
                If Not (nombre_pariente.Equals("")) Then 'si tenemos nombre
                    retornito = nombre_pariente
                End If
            End If
        Else
            retornito = "Sin Asignar"
        End If

        Return retornito
    End Function



    'BOTON CANCELAR.
    Private Sub Button3_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    'radio boton de "POR ahora no ingresar"
    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        CBpersonas.Enabled = False
        CBpersonas.Text = ""
    End Sub

    'Radio Boton para Nueva Persona
    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton5.CheckedChanged
        If RadioButton5.Checked = True Then 'SI SÍ esta habilitado el radio buton de "nuevo", los controles quedan ubicados asi:
            TBnombreProp.Text = "nombre..."
            TBnombreProp.ForeColor = Color.DarkGray
            TBapellidosProp.Text = "apellidos..."
            TBapellidosProp.ForeColor = Color.DarkGray
            TBidentifi.Text = "identificación..."
            TBidentifi.ForeColor = Color.DarkGray
            Me.Size = New Size(849, 586)
            TabControl1.Size = New Size(826, 544)
            CBpersonas.Visible = False
            TBnombreProp.Visible = True
            TBapellidosProp.Visible = True
            TBidentifi.Visible = True
            BtnAgregarEditar.Location = New Point(286, 430)
            btonCancelar.Location = New Point(388, 430)

        Else 'SI NO esta habilitado el radio buton de "nuevo", los controles quedan ubicados asi:
            TBnombreProp.Text = ""
            TBapellidosProp.Text = ""
            TBidentifi.Text = ""
            Me.Size = New Size(849, 534)
            TabControl1.Size = New Size(826, 494)
            CBpersonas.Visible = True
            TBnombreProp.Visible = False
            TBapellidosProp.Visible = False
            TBidentifi.Visible = False
            BtnAgregarEditar.Location = New Point(286, 385)
            btonCancelar.Location = New Point(388, 385)
        End If

    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        If RadioButton4.Checked = True Then
            CBpersonas.Visible = True
            CBpersonas.Enabled = True
        Else
            CBpersonas.Enabled = False
        End If
    End Sub

    'Funciones para cuando el foco ESTA en el TBnombreProp
    Private Sub TBnombreProp_Enter(sender As Object, e As EventArgs) Handles TBnombreProp.Enter
        If TBnombreProp.Text.Equals("nombre...") Then
            TBnombreProp.Text = ""
            TBnombreProp.ForeColor = Color.Black
            Dim font As New Font(Me.TBnombreProp.Font.FontFamily, 10, FontStyle.Regular)
            Me.TBnombreProp.Font = font
        End If
    End Sub
    'Funciones para cuando el foco NO esta en el TBnombreProp
    Private Sub TBnombreProp_Leave(sender As Object, e As EventArgs) Handles TBnombreProp.Leave
        If TBnombreProp.Text = "" Then
            TBnombreProp.Text = "nombre..."
            TBnombreProp.ForeColor = Color.DarkGray
            Dim font As New Font(Me.TBnombreProp.Font.FontFamily, 10, FontStyle.Italic)
            Me.TBnombreProp.Font = font
        End If
    End Sub

    'Funciones para cuando el foco ESTA en el TBapellidosProp
    Private Sub TBapellidosProp_Enter(sender As Object, e As EventArgs) Handles TBapellidosProp.Enter
        If TBapellidosProp.Text.Equals("apellidos...") Then
            TBapellidosProp.Text = ""
            TBapellidosProp.ForeColor = Color.Black
            Dim font As New Font(Me.TBapellidosProp.Font.FontFamily, 10, FontStyle.Regular)
            Me.TBapellidosProp.Font = font
        End If

    End Sub

    'Funciones para cuando el foco NO esta en el TBapellidosProp
    Private Sub TBapellidosProp_Leave(sender As Object, e As EventArgs) Handles TBapellidosProp.Leave
        If TBapellidosProp.Text = "" Then
            TBapellidosProp.Text = "apellidos..."
            TBapellidosProp.ForeColor = Color.DarkGray
            Dim font As New Font(Me.TBapellidosProp.Font.FontFamily, 10, FontStyle.Italic)
            Me.TBapellidosProp.Font = font
        End If
    End Sub

    'Funciones para cuando el foco ESTA en el TBidentifi

    Private Sub TBidentifi_Enter(sender As Object, e As EventArgs) Handles TBidentifi.Enter
        If TBidentifi.Text.Equals("identificación...") Then
            TBidentifi.Text = ""
            TBidentifi.ForeColor = Color.Black
            Dim font As New Font(Me.TBidentifi.Font.FontFamily, 10, FontStyle.Regular)
            Me.TBidentifi.Font = font
        End If

    End Sub
    'Funciones para cuando el foco no esta en el TBidentifi
    Private Sub TBidentifi_Leave(sender As Object, e As EventArgs) Handles TBidentifi.Leave
        If TBidentifi.Text = "" Then
            TBidentifi.Text = "identificación..."
            TBidentifi.ForeColor = Color.DarkGray
            Dim font As New Font(Me.TBidentifi.Font.FontFamily, 10, FontStyle.Italic)
            Me.TBidentifi.Font = font
        End If
    End Sub

    'BOTON CANCELAR
    Private Sub btonCancelar_Click(sender As Object, e As EventArgs) Handles btonCancelar.Click
        Me.Close()
    End Sub


    Private Sub CBsiPoseeNumRegi_CheckedChanged(sender As Object, e As EventArgs) Handles CBsiPoseeNumRegi.CheckedChanged
        If CBsiPoseeNumRegi.Checked = True Then
            TBregistro.Enabled = True
            Me.DateTimePicker1.Enabled = True
        Else
            TBregistro.Enabled = False
            Me.DateTimePicker1.Enabled = False
        End If

    End Sub

    Public Function buscar_nom_genealogia(texto As String) As String
        conexion.Conectar()
        Dim id_genealo_caba As String = "" 'este es el id de la genealogia del caballo q estamos digitando en caso de q lo tengamos
        Dim retorno As String = ""
        Dim cadenaSQL = "select genealogia from caballos where nombre='" + texto.ToLower + "';"
        Dim resultadoSQLservi As NpgsqlDataReader = conexion.consultar(cadenaSQL, conexion.conexionBD)

        If resultadoSQLservi.Read Then
            If Not (resultadoSQLservi.IsDBNull(0)) Then 'lo que sucede es q cuando el registro viene vacio.. pues daba error tocaba validarlo de esta manera gx gx gx
                id_genealo_caba = resultadoSQLservi.GetInt32(0)
                retorno = id_genealo_caba
            End If
        End If
        conexion.desconectar()
        Return retorno
    End Function

    Public Function traer_pariente(genealo As String, pariente As String) As String
        conexion.Conectar()
        Dim retorno As String = ""
        Dim id_caballo_pariente As String
        Dim cadenaSQL = "select" + Chr(34) + pariente + Chr(34) + " from genealogia where idgenealogia='" + genealo + "';"
        Dim resultadoSQLservi As NpgsqlDataReader = conexion.consultar(cadenaSQL, conexion.conexionBD)

        If resultadoSQLservi.Read Then
            If Not (resultadoSQLservi.IsDBNull(0)) Then 'lo que sucede es q cuando el registro viene vacio.. pues daba error tocaba validarlo de esta manera gx gx gx
                id_caballo_pariente = resultadoSQLservi.GetInt32(0)
                conexion.desconectar()

                Dim nomb_caballo_pari As String
                conexion.Conectar()
                Dim cadenaSQL1 = "select nombre from caballos where idcaballos='" + id_caballo_pariente + "';"
                Dim resultadoSQLservi1 As NpgsqlDataReader = conexion.consultar(cadenaSQL1, conexion.conexionBD)

                If resultadoSQLservi1.Read Then
                    If Not (resultadoSQLservi1.IsDBNull(0)) Then 'lo que sucede es q cuando el registro viene vacio.. pues daba error tocaba validarlo de esta manera gx gx gx
                        nomb_caballo_pari = resultadoSQLservi1.GetString(0) 'aca tenemos el nombre del caballo pariente
                        retorno = nomb_caballo_pari
                    End If
                End If
            End If
        End If
        conexion.desconectar()
        Return retorno
    End Function

    Public Function carga_genealogia(id_gene As String, nivel As Integer, pariente As String)
        'los niveles indican q  voy a cargar ejemplo padre es nivel 1, abuelo 2,bisabuelo 3
        If nivel.Equals(1) Then
            If pariente.Equals("papa") Then 'es el padre entons debe cargar por el lado del papa
                TB_abuelop.Text = traer_pariente(id_gene, "papa")
                TB_abuelap.Text = traer_pariente(id_gene, "mama")
                TB_bis_pat_p.Text = traer_pariente(id_gene, "abueloP")
                TB_bisa_pat_p.Text = traer_pariente(id_gene, "abuelaP")
                TB_bis_m_p.Text = traer_pariente(id_gene, "abueloM")
                TB_bisa_mat_p.Text = traer_pariente(id_gene, "abuelaM")
            End If

            If pariente.Equals("mama") Then 'es el madre entons debe cargar por el lado del mama
                TB_abuelop.Text = traer_pariente(id_gene, "papa")
                TB_abuelap.Text = traer_pariente(id_gene, "mama")
                TB_bis_pat_p.Text = traer_pariente(id_gene, "abueloP")
                TB_bisa_pat_p.Text = traer_pariente(id_gene, "abuelaP")
                TB_bis_m_p.Text = traer_pariente(id_gene, "abueloM")
                TB_bisa_mat_p.Text = traer_pariente(id_gene, "abuelaM")
            End If
        End If

    End Function


    Private Sub TB_padre_caballo_TextChanged(sender As Object, e As EventArgs) Handles TB_padre_caballo.TextChanged
        'evento para la genealogia del padre
        Dim text As String = TB_padre_caballo.Text
        Dim genealo As String = buscar_nom_genealogia(text)
        If Not (genealo.Equals("")) Then

            If MsgBox("Existe una Genealogia asociada a este Caballo, ¿ desea cargarla ?", MsgBoxStyle.YesNo, "Cargar Genealogia") = MsgBoxResult.Yes Then
                carga_genealogia(genealo, 1, "papa")
            End If
            'MsgBox("existe una genealogia asociada a este caballo  desea cagarla jajajjaja buenisimo" + genealo)
        End If
    End Sub
    'Para que se puecda cerrar con la tecla ESC
    Private Sub Form1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If (e.KeyCode = Keys.Escape) Then
            Me.Close()
        End If
    End Sub


    Private Sub TabControl1_Selecting(sender As Object, e As TabControlCancelEventArgs) Handles TabControl1.Selecting
        If bloqueo_tabs.Enabled = True Then 'evento q no permite que el usuario se cambie de tap
            e.Cancel = True
            MsgBox("Por favor Guarde los datos de un Caballo Nuevo, antes de cambiar de pestaña", MsgBoxStyle.Critical, "Pestaña Bloqueada")

        End If
    End Sub

    Private Sub TB_madre_caballo_TextChanged(sender As Object, e As EventArgs) Handles TB_madre_caballo.TextChanged
        'evento para la genealogia de la madre
        Dim text As String = TB_padre_caballo.Text
        Dim genealo As String = buscar_nom_genealogia(text)
        If Not (genealo.Equals("")) Then

            If MsgBox("Existe una Genealogia asociada a este Caballo, ¿ desea cargarla ?", MsgBoxStyle.YesNo, "Cargar Genealogia") = MsgBoxResult.Yes Then
                carga_genealogia(genealo, 1, "mama")
            End If
            'MsgBox("existe una genealogia asociada a este caballo  desea cagarla jajajjaja buenisimo" + genealo)
        End If
    End Sub
End Class