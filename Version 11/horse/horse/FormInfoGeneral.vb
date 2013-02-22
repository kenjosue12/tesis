Imports System.Data.SqlClient
Imports System.Data
Imports horse.BD
Imports Npgsql
Imports System.IO
Imports horse.FormAgregar
Imports NpgsqlTypes
Imports horse.FormBuscarAvanzado

Public Class FormInfoGeneral

    Public cadenaSQL As String
    Public resultadoSQL As NpgsqlDataReader
    Private conexion As BD
    Public obFormAgregar As New FormAgregar
    Public dt As New DataTable



    Public Sub New(ByVal conec As BD)

        Me.InitializeComponent()
        conexion = conec
    End Sub
    Public Sub New()

        ' Llamada necesaria para el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub
    Public Sub New(ByVal dataTable As DataTable)
        ' Llamada necesaria para el diseñador.
        InitializeComponent()
        Dim dt As DataTable = dataTable
        MsgBox("hola")
        If Not RellenarListview(dt) Then Throw New Exception("Sucedio un error al rellenar el listado.")
        FormBuscarAvanzado.Close()
        Me.Show()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub
    'Funcio para limpiar el listview
    Public Function limpiarListView()
        'Hay que borrar los datos guardados en el dt, porque cada vez que se llena el listview no se borra automaticamente los datos ya guardados en el dt.
        If lvwDatos.Items.Count > 0 Then
            dt.Clear()
        End If
    End Function
    'FUncion que busca datos para para llenar el DataTable 
    Function BuscarDatos(ByRef dt As DataTable, ByVal consultaSQL As String) As Boolean
        Dim consulSQL = consultaSQL
        Dim bolResultado As Boolean = True
        Try
            If dt Is Nothing Then dt = New DataTable

            Dim cmd As New NpgsqlCommand(consulSQL, conexion.conexionBD)

            Using DA As New NpgsqlDataAdapter(cmd)
                DA.Fill(dt)
            End Using

        Catch ex As Exception
            bolResultado = False
        End Try
        Return bolResultado
    End Function
    Function RellenarListview(ByVal dt As DataTable) As Boolean
        Dim bolResultado As Boolean = True
        Dim lstElemento As ListViewItem
        Try

            Me.lvwDatos.Items.Clear()
            Me.lvwDatos.Columns.Clear()
            For Each col As DataColumn In dt.Columns
                lvwDatos.Columns.Add(col.ColumnName, col.ColumnName)
            Next
            For Each row As DataRow In dt.Rows
                lstElemento = New ListViewItem
                lstElemento.Text = row(0).ToString()
                For intcontador As Integer = 1 To dt.Columns.Count - 1
                    lstElemento.SubItems.Add(row(intcontador).ToString())
                Next
                lvwDatos.Items.Add(lstElemento)
            Next
            Me.lvwDatos.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
        Catch ex As Exception
            bolResultado = False
        End Try
        Return bolResultado
    End Function
    'BOTON BUSCAR'
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        limpiarListView()
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
           
        End If
        Try
            If Not BuscarDatos(dt, cadenaSQL) Then Throw New Exception("Sucedio un error al consultar los datos.") 'llama a la funcion BuscarDatos.
            If Not RellenarListview(dt) Then Throw New Exception("Sucedio un error al rellenar el listado.") 'Llama a la funcion RellenarListview
            BTN_editar.Enabled = True
            BTN_eliminar.Enabled = True
            Button3.Enabled = True
        Catch ex As Exception
            MsgBox(ex.Message, , "Error")
        End Try

        'cadenaSQL = "select idcaballos,numero_registro,nombre,raza,sexo,andar,color_pelaje,fecha_nacimiento,fecha_registro,estado,senas_particulares,propietario,asociacion, servicios from caballos where nombre ='" + nombre + "'and registro=" + nregistro + " ;"


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
        limpiarListView()
        BTN_editar.Enabled = True
        BTN_eliminar.Enabled = True
        Button3.Enabled = True
        traerTodosCaballos()
    End Sub

    'Funcion para traer todos caballos
    Public Function traerTodosCaballos()
        'Hay que borrar los datos guardados en el dt, porque cada vez que se llena el listview no se borra automaticamente los datos ya guardados en el dt.
        Dim dat As New DataTable
        Dim cadena = "select idcaballos,numero_registro,r.descripcion as raza,nombre,sexo,a.descripcion as andar,co.descripcion as color_pelaje,fecha_nacimiento,fecha_registro,e.descripcion as estado,p.nombres|| ' ' ||p.apellidos as propietario,aso.descripcion as asociacion, s.descripcion as servicios from caballos c, raza r ,andares a, colores co,estado_caballos e,asociaciones aso,servicios s, personas p where  r.idraza=c.raza and a.idandares=c.andar and co.idcolores=c.color_pelaje and e.idestado_caballo=c.estado and aso.idasociaciones=c.asociacion and s.idservicios=c.servicios and p.identificacion=c.propietario order by idcaballos asc;"
        'select idcaballos,numero_registro,nombre,r.descripcion as raza,sexo,andar,color_pelaje,fecha_nacimiento,fecha_registro,estado,senas_particulares,propietario,asociacion, servicios from caballos c, raza r where  r.idraza=c.raza ;"
        Try
            If lvwDatos.Items.Count > 0 Then
                dat.Clear()
            End If
            If Not BuscarDatos(dat, cadena) Then Throw New Exception("Sucedio un error al consultar los datos.") 'llama a la funcion BuscarDatos.
            If Not RellenarListview(dat) Then Throw New Exception("Sucedio un error al rellenar el listado.") 'Llama a la funcion RellenarListview

        Catch ex As Exception
            MsgBox(ex.Message, , "Error")
        End Try
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
        Dim datosCaballo
        Dim aux
        If lvwDatos.SelectedItems.Count > 0 Then 'Pregunto si hay alguna fila seleccionada.
            For i = 0 To 12 'Se pone hasta 12 porque: El numero de subitems siempre es el numero de columnas -1. Y entonces ya serian 13, pero como empieza en 0, por eso termina la cuenta en 12, aunque las columnas sean 15.
                aux = lvwDatos.SelectedItems(0).SubItems(i).Text 'guardo cada dato de la fila
                If aux = "" Then
                    aux = "null"
                End If
                datosCaballo += aux + "*"
                ' MsgBox(datos)
            Next
        End If
        Dim caballoID = obFormAgregar.procesaDatosCaballo(datosCaballo) 'Guardo en idcaballo lo que me retorna la funcion, en este caso el idcaballo.. -_-
        'Dim caballoID As Integer = DataGridView1("idcaballos", DataGridView1.CurrentRow.Index).Value
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
       
        If lvwDatos.SelectedItems.Count > 0 Then 'Pregunto si hay alguna fila seleccionada.
            For i = 0 To 12 'Se pone hasta 12 porque: El numero de subitems siempre es el numero de columnas -1. Y entonces ya serian 13, pero como empieza en 0, por eso termina la cuenta en 12, aunque las columnas sean 15.
                aux = lvwDatos.SelectedItems(0).SubItems(i).Text 'guardo cada dato de la fila
                If aux = "" Then
                    aux = "null"
                End If
                datosCaballo += aux + "*"
                ' MsgBox(datos)
            Next
        End If

            Dim editar As New FormAgregar(datosCaballo, "a") 'a de actualizar
            editar.Text = "Editar Caballo"
            'codigo para mostrar la foto en el formulario Editar
        Dim caballoID = obFormAgregar.procesaDatosCaballo(datosCaballo) 'Guardo en idcaballo lo que me retorna la funcion, en este caso el idcaballo.. -_-
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
            If lvwDatos.SelectedItems.Count > 0 Then 'Pregunto si hay alguna fila seleccionada.
                For i = 0 To 12 'Se pone hasta 12 porque: El numero de subitems siempre es el numero de columnas -1. Y entonces ya serian 13, pero como empieza en 0, por eso termina la cuenta en 12, aunque las columnas sean 15.
                    aux = lvwDatos.SelectedItems(0).SubItems(i).Text 'guardo cada dato de la fila
                    If aux = "" Then
                        aux = "null"
                    End If
                    datosCaballo += aux + "*"
                    ' MsgBox(datos)
                Next
            End If
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
                    'Hay que borrar los datos guardados en el dt, porque cada vez que se llena el listview no se borra automaticamente los datos ya guardados en el dt.
                    limpiarListView()
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

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        'guarda en resultado , lo que devuelve la funcion getValores, es decir una estructura, en este caso la estructura contiene el dt, el datatable 
        'que esta lleno con la consulta que se hizo el formulario Buscar Avanzado.
        Dim resultado As FormBuscarAvanzado.ParametrosRetorno = FormBuscarAvanzado.GetValores()

        'Se pregunta si ese resultado viene vacio o no. Para saber si viene vacio se pregunta si ese resultado venia con la estructura .bandera
        If resultado.bandera <> "NULL" Then
            'Si no viene con .bandera, entonces debe venir con la estructura .datatable, es decir con el dt ya lleno. y 
            'se procede a llenar el listview con ese dt.
            If resultado.cancelar <> "no" Then
                If Not RellenarListview(resultado.datatable) Then Throw New Exception("Sucedio un error al rellenar el listado.") 'Llama a la funcion RellenarListview
            End If

        End If
    End Sub
    'Se abre el form EDITAR con doble click en una celda del datagrid
    Private Sub lvwDatos_DoubleClick(sender As Object, e As EventArgs) Handles lvwDatos.DoubleClick
        Button3.Enabled = True
        BTN_editar.Enabled = True
        BTN_eliminar.Enabled = True
        EditarCaballo()
    End Sub

    'NO BORRAR.
    'Funcion que recorre todoos los datos del listview
    '    'For i = 0 To lvwDatos.Items.Count - 1 'El item es la primera celda de la fila
    '    '    'El item es la primera columna de cada renglon
    '    '    MsgBox(lvwDatos.Items(i).Text)
    '    '    'Con el for interno se cuentan los subitems, y se imprimen.
    '    '    For j = 0 To 12 'Se pone hasta 12 porque: El numero de subitems siempre es el numero de columnas -1. Y entonces ya serian 13, pero como empieza en 0, por eso termina la cuenta en 12, aunque las columnas sean 15.
    '    '        MsgBox(lvwDatos.Items(i).SubItems(j).Text) 'los subitem son las celdas que le siguen al Item de ahi en adelante.
    '    '    Next j
    '    'Next i

End Class