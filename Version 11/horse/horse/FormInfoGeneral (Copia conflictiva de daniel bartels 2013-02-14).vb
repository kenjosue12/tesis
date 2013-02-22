Imports System.Data.SqlClient
Imports System.Data
Imports horse.BD
Imports Npgsql
Imports System.IO
Imports horse.FormAgregar
Imports NpgsqlTypes

Public Class FormInfoGeneral

    Public cadenaSQL As String
    Public resultadoSQL As NpgsqlDataReader
    Private conexion As BD
    Public obFormAgregar As New FormAgregar

    Public Sub New(ByVal conec As BD)

        Me.InitializeComponent()
        conexion = conec
    End Sub
    Public Sub New()

        ' Llamada necesaria para el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub

    'BOTON BUSCAR'
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim nombre As String = TBnombre.Text
        Dim nregistro As String = TBregistro.Text

        If String.IsNullOrEmpty(TBnombre.Text) And String.IsNullOrEmpty(TBregistro.Text) Then
            MsgBox("Ingrese Datos a Buscar")
        Else
            If TBnombre.Text <> "" Then

                cadenaSQL = "select idcaballos, numero_registro,r.descripcion as raza,nombre, sexo,a.descripcion as andar,co.descripcion as color,fecha_nacimiento,fecha_registro,e.descripcion as estado,propietario,aso.descripcion as asociacion,s.descripcion as servicio from caballos c, raza r , andares a,colores co, estado_caballos e, asociaciones aso,servicios s where nombre='" + nombre + "' and r.idraza=c.raza and a.idandares=c.andar and co.idcolores=c.color_pelaje and e.idestado_caballo=c.estado and aso.idasociaciones=c.asociacion and s.idservicios=c.servicios;"
            Else

                cadenaSQL = "select idcaballos, numero_registro,r.descripcion as raza,nombre, sexo,a.descripcion as andar,co.descripcion as color,fecha_nacimiento,fecha_registro,e.descripcion as estado,propietario,aso.descripcion as asociacion,s.descripcion as servicio from caballos c, raza r , andares a,colores co, estado_caballos e, asociaciones aso,servicios s where numero_registro='" + nregistro + "' and r.idraza=c.raza and a.idandares=c.andar and co.idcolores=c.color_pelaje and e.idestado_caballo=c.estado and aso.idasociaciones=c.asociacion and s.idservicios=c.servicios;"
            End If
            Dim ds As New DataSet
            Dim DA As New NpgsqlDataAdapter(cadenaSQL, conexion.conexionBD)
            DA.Fill(ds)
            DataGridView1.DataSource = ds.Tables(0)
        End If


        'cadenaSQL = "select idcaballos,numero_registro,nombre,raza,sexo,andar,color_pelaje,fecha_nacimiento,fecha_registro,estado,senas_particulares,propietario,asociacion, servicios from caballos where nombre ='" + nombre + "'and registro=" + nregistro + " ;"


    End Sub
    'Boton para abrir el form BUSCAR.

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Dim buscarAvanzado As New FormBuscarAvanzado(conexion)
        buscarAvanzado.ShowDialog()

    End Sub
    'BOTON AGREGAR 
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles BTN_agregar.Click

        Dim agregar As New FormAgregar("dafds", "i")
        agregar.Text = "Agregar Caballo"
        agregar.ShowDialog()
    End Sub
    'Radio Boton de NOMBRE
    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        TBregistro.Enabled = False
        TBnombre.Enabled = True
    End Sub
    'Radio Boton de REGISTRO
    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        TBnombre.Enabled = False
        TBregistro.Enabled = True
    End Sub
    'Check box que trae todos los registros
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        BTN_editar.Enabled = True
        BTN_eliminar.Enabled = True
        Button3.Enabled = True
        traerTodosCaballos()
    End Sub

    'Funcion para traer todos caballos
    Public Function traerTodosCaballos()
        cadenaSQL = "select idcaballos,numero_registro,r.descripcion as raza,nombre,sexo,a.descripcion as andar,co.descripcion as color_pelaje,fecha_nacimiento,fecha_registro,e.descripcion as estado,p.nombres|| ' ' ||p.apellidos as propietario,aso.descripcion as asociacion, s.descripcion as servicios from caballos c, raza r ,andares a, colores co,estado_caballos e,asociaciones aso,servicios s, personas p where  r.idraza=c.raza and a.idandares=c.andar and co.idcolores=c.color_pelaje and e.idestado_caballo=c.estado and aso.idasociaciones=c.asociacion and s.idservicios=c.servicios and p.identificacion=c.propietario order by idcaballos asc;"
        'select idcaballos,numero_registro,nombre,r.descripcion as raza,sexo,andar,color_pelaje,fecha_nacimiento,fecha_registro,estado,senas_particulares,propietario,asociacion, servicios from caballos c, raza r where  r.idraza=c.raza ;"
        Dim ds As New DataSet
        Dim DA As New NpgsqlDataAdapter(cadenaSQL, conexion.conexionBD)
        DA.Fill(ds)
        DataGridView1.DataSource = ds.Tables(0)
    End Function
    Function MostrarImagen(ByVal consulta As String) As Byte()
        conexion.Conectar()
        Dim SqlSelect As String = consulta
        Dim Command As New NpgsqlCommand(SqlSelect, conexion.conexionBD)
        'conn.Open()
        Dim MyPhoto() As Byte = CType(Command.ExecuteScalar(), Byte())
        conexion.conexionBD.Close()
        Return MyPhoto
    End Function

    'BOTON MOSTRAR FOTO 
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim caballoID As Integer = DataGridView1("idcaballos", DataGridView1.CurrentRow.Index).Value
        Try
            If conexion.Conectar() = False Then
                MsgBox("Problemas en la Conexion", MsgBoxStyle.Exclamation)
                Me.Close()
            End If
            'El MemoryStream nos permite crear un almacen de memoria
            Dim ms As New MemoryStream(MostrarImagen("SELECT  l.imagen  FROM logos l, caballos c where l.codigo=c.foto and idcaballos='" & caballoID & "';"))
            FormDeFotoMostrada.PictureBox1.Image = Image.FromStream(ms)
            'Me.PictureBox1.Image.s
            FormDeFotoMostrada.ShowDialog()
        Catch ex As Exception
            MsgBox("Este caballo aun no tiene una imagen asignada", MsgBoxStyle.Critical)
        End Try
    End Sub
    'LOAD DEL FORMULARIO
    'PONE BOTONES INACTIVOS HASTA QUE ALLA DATOS EN LA GRILLA
    Private Sub FormInfoGeneral_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Autocompleta el nombre del caballo que se esta buscando.
        Try
            conexion.Conectar()
            Dim dt As New DataTable
            Dim ds As New DataSet
            ds.Tables.Add(dt)

            Dim DA As New NpgsqlDataAdapter("select nombre from caballos;", conexion.conexionBD)
            DA.Fill(dt)

            Dim r As DataRow

            TBnombre.AutoCompleteCustomSource.Clear()
            For Each r In dt.Rows
                TBnombre.AutoCompleteCustomSource.Add(r.Item(0).ToString)
            Next
            conexion.desconectar()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

        'Autocompletar para Numero Registro
        Try
            conexion.Conectar()
            Dim dt As New DataTable
            Dim ds As New DataSet
            ds.Tables.Add(dt)

            Dim DA As New NpgsqlDataAdapter("select numero_registro from caballos;", conexion.conexionBD)
            DA.Fill(dt)

            Dim r As DataRow

            TBregistro.AutoCompleteCustomSource.Clear()
            For Each r In dt.Rows
                TBregistro.AutoCompleteCustomSource.Add(r.Item(0).ToString)
            Next
            conexion.desconectar()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

        'ACTIVA BOTONES
        RadioButton1.Checked = True
        Button3.Enabled = False
        BTN_editar.Enabled = False
        BTN_eliminar.Enabled = False

    End Sub
    'BOTON LLAMAR A FORM ACTUALIZAR o EDITAR CON LA CADENA DE DATOS DEL CABALLO
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles BTN_editar.Click
        EditarCaballo()
    End Sub
    Public Function EditarCaballo()
        Dim datosCaballo As String
        Dim aux As String

        For i As Integer = 0 To DataGridView1.SelectedCells.Count - 1
            aux = DataGridView1(i, DataGridView1.CurrentRow.Index).Value.ToString
            If aux = "" Then
                aux = "null"
            End If
            datosCaballo += aux + "*"
        Next

        Dim editar As New FormAgregar(datosCaballo, "a") 'a de actualizar
        editar.Text = "Editar Caballo"
        'codigo para mostrar la foto en el formulario Editar
        Dim caballoID As Integer = DataGridView1("idcaballos", DataGridView1.CurrentRow.Index).Value
        Try
            If conexion.Conectar() = False Then
                MsgBox("Problemas en la Conexion", MsgBoxStyle.Exclamation)
                Me.Close()
            End If
            'El MemoryStream nos permite crear un almacen de memoria
            Dim ms As New MemoryStream(MostrarImagen("SELECT  l.imagen  FROM logos l, caballos c where l.codigo=c.foto and idcaballos='" & caballoID & "';"))
            editar.PictureBox1.Image = Image.FromStream(ms)
            editar.PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
            editar.PictureBox1.BorderStyle = BorderStyle.Fixed3D
        Catch ex As Exception ' esta parte muestra una foto "sin imagen" en caso de que el caballo no tenga imagen
            Dim ms As New MemoryStream(MostrarImagen("SELECT  imagen  FROM logos  where codigo=9;"))
            editar.PictureBox1.Image = Image.FromStream(ms)
            editar.PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
            editar.PictureBox1.BorderStyle = BorderStyle.Fixed3D
            conexion.desconectar()
        End Try
        editar.ShowDialog() 'muestra el Formulario Editar.
    End Function
    'Se abre el form EDITAR con doble click en una celda del datagrid

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Button3.Enabled = True
        BTN_editar.Enabled = True
        BTN_eliminar.Enabled = True
        EditarCaballo()
    End Sub
    'Funcion para cerrar el form con la tecla ESC
    Private Sub Form1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If (e.KeyCode = Keys.Escape) Then
            Me.Close()
        End If
    End Sub

    'Boton para ELIMINAR    
    Private Sub BTN_eliminar_Click(sender As Object, e As EventArgs) Handles BTN_eliminar.Click
        If MsgBox("¿Esta seguro que desea borrar este registro?", MsgBoxStyle.YesNo, "Atención") = MsgBoxResult.Yes Then
            Dim cadenaSQLorden
            Dim datosCaballo
            Dim aux

            For i As Integer = 0 To DataGridView1.SelectedCells.Count - 1
                aux = DataGridView1(i, DataGridView1.CurrentRow.Index).Value.ToString
                If aux = "" Then
                    aux = "null"
                End If
                datosCaballo += aux + "*"
            Next
            Dim idcaballo = obFormAgregar.procesaDatosCaballo(datosCaballo) 'Guardo en idcaballo lo que me retorna la funcion, en este caso el idcaballo.. -_-
            If MsgBox("El registro seleccionado se borrará permanentemente, ¿desea continuar?", MsgBoxStyle.YesNo, "Atención") = MsgBoxResult.Yes Then

                conexion.Conectar()
                Dim cadenaSQL = "select idsenales_particulares from senales_particulares s, caballos c where  s.id_caballo=c.idcaballos and idcaballos='" + idcaballo + "';"

                Dim resultadoSQLSena As NpgsqlDataReader = conexion.consultar(cadenaSQL, conexion.conexionBD)
                If resultadoSQLSena.Read Then 'Se pregunta si el caballo a borrar tiene señas asociaciadas, si es  asi, se procede a borrarlas.
                    Try
                        If conexion.Conectar() = False Then
                            MsgBox("Problemas en la Conexion", MsgBoxStyle.Exclamation)
                            Me.Close()
                        End If

                        Dim cmd As New NpgsqlCommand("delete from senales_particulares where id_caballo=@idcaba;", conexion.conexionBD) 'borra la señas asociadas al caballo
                        With cmd
                            .Parameters.Add("@idcaba", NpgsqlDbType.Integer).NpgsqlValue = idcaballo
                        End With
                        cmd.ExecuteNonQuery() 'ejecuta la instruccion SQl
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

                Try 'curso normal para borrar el caballo y sus datos de la base de datos.
                    conexion.Conectar()
                    cadenaSQLorden = "delete from caballos where idcaballos=@idcaballo; "
                    Dim cmd As New NpgsqlCommand(cadenaSQLorden, conexion.conexionBD)
                    cmd.Parameters.Add("@idcaballo", NpgsqlDbType.Integer).NpgsqlValue = idcaballo
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("¡El registro se ha borrado exitosamente!", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    traerTodosCaballos()
                Catch sqlExc As NpgsqlException
                    MsgBox("abajo")
                    MessageBox.Show(sqlExc.ToString, "SQL Exception Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
                conexion.desconectar()
            End If

        End If
    End Sub
End Class