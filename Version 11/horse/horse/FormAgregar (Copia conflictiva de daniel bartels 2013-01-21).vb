Imports horse.utilidades
Imports horse.BD
Imports Npgsql
Imports NpgsqlTypes
Imports System.IO
Imports System.Data

Public Class FormAgregar
    Public ObUtilidades As New utilidades
    Public instruccion As String
    Public conexion As New BD
    Public genea As Agre_cab_genealogia
    Public registro As Integer
    Public nombrePropietario As String
    Public identificacion As Integer
    Public apellidosPropietario As String
    Public fechaRegistro As Date
    Public resultadoSQL As NpgsqlDataReader

    'constructor para editar, con parametros
    Public Sub New(ByVal cadeDatosInfo As String, ByVal ins As String)
        instruccion = ins
        Me.InitializeComponent()

        If instruccion.Equals("a") Then '  si vamos a actualizar o editar un caballo
            Call procesaDatosCaballo(cadeDatosInfo)
        End If
        If instruccion.Equals("i") Then ' si vamos a insertar un nuevo caballoo
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
    'Funcion que pinta los datos que tiene el caballo seleccionado, en el formulario EDITAR
    Public Function procesaDatosCaballo(ByVal cadenaDatos)
        Dim cadenaDatosFunc() As String = Split(cadenaDatos, "*")

        ObUtilidades.Cargar_Combo(CBraza, "select descripcion from raza;")

        TBid.Text = cadenaDatosFunc(0)
        TBregistro.Text = cadenaDatosFunc(1)
        CBraza.Text = cadenaDatosFunc(2)
        TBnombre.Text = cadenaDatosFunc(3)
        CBandar.Text = cadenaDatosFunc(5)
        'sexo-4
        CbcolorPelaje.Text = cadenaDatosFunc(6)
        'fecha nacimiento7
        'fecha registro 8
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
    End Sub
    'FUNCION INSERTAR: INSERTA CABALLOS
    Public Function insertar(ByVal comando As String)
        Dim com = comando
        Dim cadenaSQLorden
        conexion.Conectar()
        Dim resuland As Integer
        Dim resulcolor As Integer
        Dim resulraza As Integer
        Dim resulasoci As Integer
        Dim resulservici As String
        Dim resulsEstado As String
        Dim resulPropietario As Integer
        Dim TBnombre As String = ""

        'DATOS DE ANDARES
        Dim cadenaSQLandar = "select idandares from andares where descripcion='" + CBandar.Text + "';"
        Dim resultadoSQLandar As NpgsqlDataReader = conexion.consultar(cadenaSQLandar, conexion.conexionBD)
        If resultadoSQLandar.Read Then
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
        Dim cadenaSQLservi = "select idservicios from servicios where descripcion='" + CBservicios.Text + "';"
        Dim resultadoSQLservi As NpgsqlDataReader = conexion.consultar(cadenaSQLservi, conexion.conexionBD)
        If resultadoSQLservi.Read Then
            resulservici = resultadoSQLservi.GetString(0)
        End If
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
        If TBnombreProp.Text <> "" And TBapellidosProp.Text <> "" And TBidentifi.Text <> "" Then
            nombrePropietario = TBnombreProp.Text
            apellidosPropietario = TBapellidosProp.Text
            'identificacion = Integer.Parse(TBidentifi.Text)
            If IsNumeric(TBidentifi.Text) = True Then
                identificacion = CInt(TBidentifi.Text)
            Else
                MsgBox("La Identificacion debe de ser un Numero")
            End If
            cadenaSQLorden = "Insert into personas(identificacion,nombres,apellidos) values (@identificacion, @nombres,@apellidos) "
            Dim cmd As New NpgsqlCommand(cadenaSQLorden, conexion.conexionBD)
            With cmd
                .Parameters.Add("@identificacion", NpgsqlDbType.Integer).NpgsqlValue = identificacion
                .Parameters.Add("@nombres", NpgsqlDbType.Varchar, 30).NpgsqlValue = nombrePropietario
                .Parameters.Add("@apellidos", NpgsqlDbType.Varchar, 30).NpgsqlValue = apellidosPropietario
            End With
            cmd.ExecuteNonQuery()
            resulPropietario = identificacion
        Else
            Dim cadenaSQLpropietario = "select identificacion from personas where nombres='" + CBpersonas.Text + "';"
            Dim resultadoSQLpropietario As NpgsqlDataReader = conexion.consultar(cadenaSQLpropietario, conexion.conexionBD)
            If resultadoSQLpropietario.Read Then
                resulPropietario = resultadoSQLpropietario.GetInt32(0)
            End If
        End If
        conexion.desconectar()

        'Este primer IF, no permite insertar en la base de datos si los campos de Estado y Nombre estan vacios
        If Me.TBnombre.Text = "" Then
            MsgBox("Los datos con (*) son obligatorio, ingrese el Nombre", MsgBoxStyle.Exclamation)
            Label35.ForeColor = Color.Red
        ElseIf resulsEstado = "" Then
            MsgBox("Los datos con (*) son obligatorio, ingrese el Estado", MsgBoxStyle.Exclamation)
            Label34.ForeColor = Color.Red
        Else
            Try
                If conexion.Conectar() = False Then
                    MsgBox("Problemas en la Conexión", MsgBoxStyle.Exclamation)
                    conexion.desconectar()
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
                    .Parameters.Add("@fnacimi", NpgsqlDbType.Date, 100).NpgsqlValue = Me.DateTimePicker2.Value.Date
                    .Parameters.Add("@fregis", NpgsqlDbType.Date, 100).NpgsqlValue = fechaRegistro
                    .Parameters.Add("@estado", NpgsqlDbType.Varchar).NpgsqlValue = resulsEstado
                    .Parameters.Add("@propie", NpgsqlDbType.Integer).NpgsqlValue = resulPropietario
                    .Parameters.Add("@asoci", NpgsqlDbType.Integer).NpgsqlValue = resulasoci
                    '.Parameters.Add("@genea", NpgsqlDbType.Varchar, 100).NpgsqlValue = "null"
                    .Parameters.Add("@servici", NpgsqlDbType.Varchar, 10).NpgsqlValue = resulservici
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
                    MessageBox.Show("A continuacion puede ingresar los datos de Señas Particulares y Genealogia", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    'aqui lo mismo pero para cuando se ACTUALICE.
                    LBidcaballos.Text = TBid.Text
                    LBnombreCaballo.Text = Me.TBnombre.Text
                    LB_id_caballo.Text = TBid.Text
                    LB_nombre_caba.Text = Me.TBnombre.Text
                    MessageBox.Show("Se ha actualizado el registro correctamente", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

        'incrementa codigo funcion. Solo se utiliza aca en el load. 
        TBregistro.Enabled = False
        TBcodigo.Text = ObUtilidades.IncrementaCodigo("select max(codigo)as id from logos") + 1
        Me.DateTimePicker1.Enabled = False
        Me.BtnInsertar.Enabled = False
        Call ObUtilidades.Cargar_Combo(CBcabeza, "select distinct descripcion from senales_particulares where ubicacion='1';")
        Call ObUtilidades.Cargar_Combo(CBtronco, "select distinct descripcion from senales_particulares  where ubicacion='2';")
        Call ObUtilidades.Cargar_Combo(CBpatas, "select distinct descripcion from senales_particulares  where ubicacion='3';")
        Call ObUtilidades.Cargar_Combo(CBraza, "select descripcion from raza;")
        Call ObUtilidades.Cargar_Combo(CBandar, "select descripcion from andares;")
        Call ObUtilidades.Cargar_Combo(CbcolorPelaje, "select descripcion from colores;")
        Call ObUtilidades.Cargar_Combo(CBasociacion, "select descripcion from asociaciones;")
        Call ObUtilidades.Cargar_Combo(CBservicios, "select descripcion from servicios;")
        ObUtilidades.Cargar_Combo(CBpersonas, "select  nombres from personas;")
        TBnombreProp.Visible = False
        TBapellidosProp.Visible = False
        TBidentifi.Visible = False
        RadioButton4.Checked = True
        If RadioButton5.Checked = True Then
            MsgBox("holaa")
        End If

    End Sub
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
            Dim cmd As New NpgsqlCommand("Insert Into logos(codigo,Nombre,Descripcion,Imagen)Values(@cod,@nombre,@Descriccion,@imagen)", conexion.conexionBD)
            With cmd
                .Parameters.Add("@cod", NpgsqlDbType.Integer).NpgsqlValue = Val(Me.TBcodigo.Text)
                .Parameters.Add("@nombre", NpgsqlDbType.Varchar, 50).NpgsqlValue = Me.TBnombreIma.Text
                .Parameters.Add("@Descriccion", NpgsqlDbType.Varchar, 100).NpgsqlValue = Me.TBdescripcion.Text
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

    End Sub
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
                        MessageBox.Show("Se ha guardado el registro correctamente", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Close()
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
                        MessageBox.Show("Se ha guardado el registro correctamente", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Close()
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
                        MessageBox.Show("Se ha guardado el registro correctamente", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Close()
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
                MsgBox("No ha seleccionado ninguna seña")
            End If
        Else
            MsgBox("Por favor, Primero diligencie los Datos Generales del caballo y haga click en guardar", MsgBoxStyle.Information)
        End If
    End Sub

    'funcion que recarga o trae el pariente asociado a un caballo
    Public Function traer_pariente(ByVal id_caballo, ByVal pariente) As String
        Dim retornito As String = "null"
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
                Else
                    MsgBox("no existe nombre asociado a este caballo errorrrrrrrrrrrrr")
                End If

            Else
                MsgBox("no tenemos el id del caballo pariente ups")
            End If
        Else
            MsgBox("Error no existe una genealogia asociada a este caballo") 'ojooooooooooooooooooooooo BORRAR ESTO
        End If

        Return retornito
    End Function

    Private Sub TB_padre_caballo_Click(sender As Object, e As EventArgs) Handles TB_padre_caballo.Click 'asignamos el papa del caballo
        Dim caba As String = LB_id_caballo.Text

        If caba.Equals("") Then
            MsgBox("NO SE HA SELECCIONADO NINGUN CABABALLO,PRIMERO DEBE CREAR UN NUEVO REGISTRO")
            TabControl1.SelectTab(0)
        Else
            genea = New Agre_cab_genealogia(LB_id_caballo.Text, "papa")
            genea.ShowDialog()
            TB_padre_caballo.Text = traer_pariente(LB_id_caballo.Text, "papa")
        End If
    End Sub

    'Si el checkbox de "si tiene numero de registro" esta seleccionado, habilita el texbox, si no, lo inhabilita.
    Private Sub CBsiPoseeNumRegi_CheckedChanged(sender As Object, e As EventArgs) Handles CBsiPoseeNumRegi.CheckedChanged
        If CBsiPoseeNumRegi.Checked = True Then
            TBregistro.Enabled = True
            Me.DateTimePicker1.Enabled = True
            fechaRegistro = Me.DateTimePicker1.Value.Date
        Else
            TBregistro.Enabled = False
            Me.DateTimePicker1.Enabled = False
            fechaRegistro = "0000/00/00"
        End If
    End Sub

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
            TBnombreProp.Text = "nombre.."
            TBnombreProp.ForeColor = Color.DarkGray
            TBapellidosProp.Text = "apellidos..."
            TBapellidosProp.ForeColor = Color.DarkGray
            TBidentifi.Text = "identificacion..."
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


    Private Sub TB_madre_caballo_Click(sender As Object, e As EventArgs) Handles TB_madre_caballo.Click 'para asignar la mama del caballo
        Dim caball As String = LB_id_caballo.Text

        If caball.Equals("") Then
            MsgBox("NO SE HA SELECCIONADO NINGUN CABABALLO,PRIMERO DEBE CREAR UN NUEVO REGISTRO")
            TabControl1.SelectTab(0)
        Else
            genea = New Agre_cab_genealogia(LB_id_caballo.Text, "mama")
            genea.ShowDialog()
            TB_madre_caballo.Text = traer_pariente(LB_id_caballo.Text, "mama")
        End If
    End Sub


    Private Sub TextBox12_Click(sender As Object, e As EventArgs) Handles TB_abuelop.Click 'para asignar el abuelo paterno
        Dim caball As String = LB_id_caballo.Text

        If caball.Equals("") Then
            MsgBox("NO SE HA SELECCIONADO NINGUN CABABALLO,PRIMERO DEBE CREAR UN NUEVO REGISTRO")
            TabControl1.SelectTab(0)
        Else
            genea = New Agre_cab_genealogia(LB_id_caballo.Text, "abuelop")
            genea.ShowDialog()
            TB_abuelop.Text = traer_pariente(LB_id_caballo.Text, "abuelop")
        End If

    End Sub

    Private Sub TextBox6_Click(sender As Object, e As EventArgs) Handles TB_abuelap.Click ' para asignar la abuela paterna
        Dim caball As String = LB_id_caballo.Text

        If caball.Equals("") Then
            MsgBox("NO SE HA SELECCIONADO NINGUN CABABALLO,PRIMERO DEBE CREAR UN NUEVO REGISTRO")
            TabControl1.SelectTab(0)
        Else
            genea = New Agre_cab_genealogia(LB_id_caballo.Text, "abuelap")
            genea.ShowDialog()
            TB_abuelap.Text = traer_pariente(LB_id_caballo.Text, "abuelap")
        End If


    End Sub

    Private Sub TextBox13_Click(sender As Object, e As EventArgs) Handles TB_abuelom.Click ' para asignar el abuelo materno
        Dim caball As String = LB_id_caballo.Text

        If caball.Equals("") Then
            MsgBox("NO SE HA SELECCIONADO NINGUN CABABALLO,PRIMERO DEBE CREAR UN NUEVO REGISTRO")
            TabControl1.SelectTab(0)
        Else
            genea = New Agre_cab_genealogia(LB_id_caballo.Text, "abuelom")
            genea.ShowDialog()
            TB_abuelom.Text = traer_pariente(LB_id_caballo.Text, "abuelom")
        End If

    End Sub

    Private Sub TextBox16_Click(sender As Object, e As EventArgs) Handles TB_abualam.Click ' para asignar la abuela materna
        Dim caball As String = LB_id_caballo.Text

        If caball.Equals("") Then
            MsgBox("NO SE HA SELECCIONADO NINGUN CABABALLO,PRIMERO DEBE CREAR UN NUEVO REGISTRO")
            TabControl1.SelectTab(0)
        Else
            genea = New Agre_cab_genealogia(LB_id_caballo.Text, "abuelam")
            genea.ShowDialog()
            TB_abualam.Text = traer_pariente(LB_id_caballo.Text, "abuelam")
        End If

    End Sub

    Private Sub TextBox17_Click(sender As Object, e As EventArgs) Handles TB_bis_pat_p.Click ' para asignar el bisabuelo paterno paterno
        Dim caball As String = LB_id_caballo.Text

        If caball.Equals("") Then
            MsgBox("NO SE HA SELECCIONADO NINGUN CABABALLO,PRIMERO DEBE CREAR UN NUEVO REGISTRO")
            TabControl1.SelectTab(0)
        Else
            genea = New Agre_cab_genealogia(LB_id_caballo.Text, "bisabuelopp")
            genea.ShowDialog()
            TB_bis_pat_p.Text = traer_pariente(LB_id_caballo.Text, "bisabuelopp")
        End If
    End Sub

    Private Sub TextBox18_Click(sender As Object, e As EventArgs) Handles TB_bisa_pat_p.Click ' para asginar la bisabuela paterna paterna

        Dim caball As String = LB_id_caballo.Text

        If caball.Equals("") Then
            MsgBox("NO SE HA SELECCIONADO NINGUN CABABALLO,PRIMERO DEBE CREAR UN NUEVO REGISTRO")
            TabControl1.SelectTab(0)
        Else
            genea = New Agre_cab_genealogia(LB_id_caballo.Text, "bisabulapp")
            genea.ShowDialog()
            TB_bisa_pat_p.Text = traer_pariente(LB_id_caballo.Text, "bisabuelapp")
        End If
    End Sub

    Private Sub TextBox20_Click(sender As Object, e As EventArgs) Handles TB_bis_m_p.Click 'paraa asignar el bisabuelo materna paterna
        Dim caball As String = LB_id_caballo.Text

        If caball.Equals("") Then
            MsgBox("NO SE HA SELECCIONADO NINGUN CABABALLO,PRIMERO DEBE CREAR UN NUEVO REGISTRO")
            TabControl1.SelectTab(0)
        Else
            genea = New Agre_cab_genealogia(LB_id_caballo.Text, "bisabuelomp")
            genea.ShowDialog()
            TB_bis_m_p.Text = traer_pariente(LB_id_caballo.Text, "bisabuelomp")
        End If
    End Sub

    Private Sub TextBox19_Click(sender As Object, e As EventArgs) Handles TB_bisa_mat_p.Click 'paraa asignar la bisabuela materna paterna
        Dim caball As String = LB_id_caballo.Text

        If caball.Equals("") Then
            MsgBox("NO SE HA SELECCIONADO NINGUN CABABALLO,PRIMERO DEBE CREAR UN NUEVO REGISTRO")
            TabControl1.SelectTab(0)
        Else
            genea = New Agre_cab_genealogia(LB_id_caballo.Text, "bisabuelamp")
            genea.ShowDialog()
            TB_bisa_mat_p.Text = traer_pariente(LB_id_caballo.Text, "bisabuelamp")
        End If
    End Sub

    Private Sub TextBox22_Click(sender As Object, e As EventArgs) Handles TB_bis_pat_m.Click ' para asignar el bisabuelo paterno materno
        Dim caball As String = LB_id_caballo.Text

        If caball.Equals("") Then
            MsgBox("NO SE HA SELECCIONADO NINGUN CABABALLO,PRIMERO DEBE CREAR UN NUEVO REGISTRO")
            TabControl1.SelectTab(0)
        Else
            genea = New Agre_cab_genealogia(LB_id_caballo.Text, "bisabuelopm")
            genea.ShowDialog()
            TB_bis_pat_m.Text = traer_pariente(LB_id_caballo.Text, "bisabuelopm")
        End If
    End Sub

    Private Sub TextBox21_Click(sender As Object, e As EventArgs) Handles TB_bisa_pat_m.Click ' para asignar la bisabuela paterno materno
        Dim caball As String = LB_id_caballo.Text

        If caball.Equals("") Then
            MsgBox("NO SE HA SELECCIONADO NINGUN CABABALLO,PRIMERO DEBE CREAR UN NUEVO REGISTRO")
            TabControl1.SelectTab(0)
        Else
            genea = New Agre_cab_genealogia(LB_id_caballo.Text, "bisabuelapm")
            genea.ShowDialog()
            TB_bisa_pat_m.Text = traer_pariente(LB_id_caballo.Text, "bisabuelapm")
        End If
    End Sub

    Private Sub TextBox24_Click(sender As Object, e As EventArgs) Handles TB_bis_mat_m.Click ' para asignar el bisabuelo materno materno
        Dim caball As String = LB_id_caballo.Text

        If caball.Equals("") Then
            MsgBox("NO SE HA SELECCIONADO NINGUN CABABALLO,PRIMERO DEBE CREAR UN NUEVO REGISTRO")
            TabControl1.SelectTab(0)
        Else
            genea = New Agre_cab_genealogia(LB_id_caballo.Text, "bisabuelomm")
            genea.ShowDialog()
            TB_bis_mat_m.Text = traer_pariente(LB_id_caballo.Text, "bisabuelomm")
        End If
    End Sub

    Private Sub TextBox23_Click(sender As Object, e As EventArgs) Handles TB_bisa_mat_m.Click ' para asignar la bisabuela materno materno
        Dim caball As String = LB_id_caballo.Text

        If caball.Equals("") Then
            MsgBox("NO SE HA SELECCIONADO NINGUN CABABALLO,PRIMERO DEBE CREAR UN NUEVO REGISTRO")
            TabControl1.SelectTab(0)
        Else
            genea = New Agre_cab_genealogia(LB_id_caballo.Text, "bisabuelamm")
            genea.ShowDialog()
            TB_bisa_mat_m.Text = traer_pariente(LB_id_caballo.Text, "bisabuelamm")
        End If
    End Sub
    'Funciones para cuando el foco ESTA en el TBnombreProp
    Private Sub TBnombreProp_Enter(sender As Object, e As EventArgs) Handles TBnombreProp.Enter
        TBnombreProp.Text = ""
        TBnombreProp.ForeColor = Color.Black
    End Sub
    'Funciones para cuando el foco NO esta en el TBnombreProp
    Private Sub TBnombreProp_Leave(sender As Object, e As EventArgs) Handles TBnombreProp.Leave
        TBnombreProp.Text = "nombre..."
        TBnombreProp.ForeColor = Color.DarkGray
    End Sub
    'Funciones para cuando el foco ESTA en el TBapellidosProp
    Private Sub TBapellidosProp_Enter(sender As Object, e As EventArgs) Handles TBapellidosProp.Enter
        TBapellidosProp.Text = ""
        TBapellidosProp.ForeColor = Color.Black
    End Sub
    'Funciones para cuando el foco no esta en el TBapellidosProp
    Private Sub TBapellidosProp_Leave(sender As Object, e As EventArgs) Handles TBapellidosProp.Leave
        TBapellidosProp.Text = "apellidos..."
        TBapellidosProp.ForeColor = Color.DarkGray
    End Sub
    'Funciones para cuando el foco ESTA en el TBidentifi
    Private Sub TBidentifi_Enter(sender As Object, e As EventArgs) Handles TBidentifi.Enter
        TBidentifi.Text = ""
        TBidentifi.ForeColor = Color.Black
    End Sub
    'Funciones para cuando el foco no esta en el TBidentifi
    Private Sub TBidentifi_Leave(sender As Object, e As EventArgs) Handles TBidentifi.Leave
        TBidentifi.Text = "identificación..."
        TBidentifi.ForeColor = Color.DarkGray
    End Sub
    'BOTON CANCELAR
    Private Sub btonCancelar_Click(sender As Object, e As EventArgs) Handles btonCancelar.Click
        Me.Close()
    End Sub


End Class