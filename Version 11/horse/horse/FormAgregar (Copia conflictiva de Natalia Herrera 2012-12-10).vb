Imports horse.utilidades
Imports horse.BD
Imports Npgsql
Imports NpgsqlTypes
Imports System.IO

Public Class FormAgregar
    Public ObUtilidades As New utilidades
    Public instruccion As String
    Public conexion As New BD

    'constructor para editar, con parametros
    Public Sub New(ByVal cadeDatosInfo As String, ByVal ins As String)
        instruccion = ins
        Me.InitializeComponent()

        If instruccion.Equals("e") Then
            Call procesaDatosCaballo(cadeDatosInfo)
        End If

        If instruccion.Equals("i") Then

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

    Public Function procesaDatosCaballo(ByVal cadenaDatos) As String
        Dim cadenaDatosFunc() As String = Split(cadenaDatos, "*")

        ObUtilidades.Cargar_Combo(CBraza, "select descripcion from raza;")
        TBid.Text = cadenaDatosFunc(0)
        TBregistro.Text = cadenaDatosFunc(1)

        TBnombre.Text = cadenaDatosFunc(2)
        CBandar.Text = cadenaDatosFunc(5)

        CbcolorPelaje.Text = cadenaDatosFunc(6)
        TBpropietario.Text = cadenaDatosFunc(10)
        TBestado.Text = cadenaDatosFunc(9)
        CBasociacion.Text = cadenaDatosFunc(11)
        CBservicios.Text = cadenaDatosFunc(13)
        'ObUtilidades.IncrementaCodigo()
        CBraza.Text = cadenaDatosFunc(3)
        'CBraza.SelectedValue = cadenaDatosFunc(3)
    End Function

    'BOTON PARA INSERTAR
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles BtnAgregarEditar.Click
        conexion.Conectar()
        Dim resuland As String
        Dim resulcolor As String
        Dim resulraza As String
        Dim resulasoci As String
        Dim resulservici As String

        Dim cadenaSQLandar = "select idandares from andares where descripcion='" + CBandar.Text + "';"
        Dim resultadoSQLandar As NpgsqlDataReader = conexion.consultar(cadenaSQLandar, conexion.conexionBD)
        If resultadoSQLandar.Read Then
            resuland = resultadoSQLandar.GetString(0)
        End If
        conexion.desconectar()

        conexion.Conectar()
        Dim cadenaSQLcolor = "select idcolores from colores where descripcion='" + CbcolorPelaje.Text + "';"
        Dim resultadoSQLcolor As NpgsqlDataReader = conexion.consultar(cadenaSQLcolor, conexion.conexionBD)
        If resultadoSQLcolor.Read Then
            resulcolor = resultadoSQLcolor.GetString(0)
        End If
        conexion.desconectar()

        conexion.Conectar()
        Dim cadenaSQLraza = "select idraza from raza where descripcion='" + CBraza.Text + "';"
        Dim resultadoSQLraza As NpgsqlDataReader = conexion.consultar(cadenaSQLraza, conexion.conexionBD)
        If resultadoSQLraza.Read Then
            resulraza = resultadoSQLraza.GetString(0)
        End If
        conexion.desconectar()

        conexion.Conectar()
        Dim cadenaSQLasocia = "select idasociaciones from asociaciones where descripcion='" + CBasociacion.Text + "';"
        Dim resultadoSQLasocia As NpgsqlDataReader = conexion.consultar(cadenaSQLasocia, conexion.conexionBD)
        If resultadoSQLasocia.Read Then
            resulasoci = resultadoSQLasocia.GetString(0)
        End If
        conexion.desconectar()

        conexion.Conectar()
        Dim cadenaSQLservi = "select idservicios from servicios where descripcion='" + CBservicios.Text + "';"
        Dim resultadoSQLservi As NpgsqlDataReader = conexion.consultar(cadenaSQLservi, conexion.conexionBD)
        If resultadoSQLservi.Read Then
            resulservici = resultadoSQLservi.GetString(0)
        End If
        conexion.desconectar()
        Try
            If conexion.Conectar() = False Then
                MsgBox("Problemas en la Coneccion", MsgBoxStyle.Exclamation)
                conexion.desconectar()
            End If
            'Dim arrFilename() As String = Split(Text, "\")
            'Array.Reverse(arrFilename)

            'Dim ms As New MemoryStream
            'PictureBox1.Image.Save(ms, PictureBox1.Image.RawFormat)
            'Dim arrImage() As Byte = ms.GetBuffer

            Dim cmd As New NpgsqlCommand("Insert Into caballos(idcaballos,nombre,estado)Values(@id,@nombre,'a')", conexion.conexionBD)
            With cmd

                .Parameters.Add("@id", NpgsqlDbType.Varchar).NpgsqlValue = Val(Me.TBid.Text)
                '.Parameters.Add("@nregistro", NpgsqlDbType.Integer, 50).NpgsqlValue = Me.TBregistro.Text
                '.Parameters.Add("@raza", NpgsqlDbType.Varchar, 100).NpgsqlValue = resulraza
                .Parameters.Add("@nombre", NpgsqlDbType.Varchar).NpgsqlValue = Val(Me.TBnombre.Text)
                ' .Parameters.Add("@sexo", NpgsqlDbType.Varchar, 50).NpgsqlValue = Me.RadioButton1.Text
                '.Parameters.Add("@andar", NpgsqlDbType.Varchar, 100).NpgsqlValue = resuland
                '.Parameters.Add("@colorPel", NpgsqlDbType.Varchar).NpgsqlValue = resulcolor
                ''    .Parameters.Add("@fnacimi", NpgsqlDbType.Date, 100).NpgsqlValue = Me.DateTimePicker2.Text
                ''  .Parameters.Add("@fregis", NpgsqlDbType.Date, 100).NpgsqlValue = Me.DateTimePicker1.Text
                ' .Parameters.Add("@estado", NpgsqlDbType.Varchar).NpgsqlValue = "'" + Me.TBestado.Text + "'"
                '.Parameters.Add("@propie", NpgsqlDbType.Varchar, 50).NpgsqlValue = Me.TBpropietario.Text
                '.Parameters.Add("@asoci", NpgsqlDbType.Varchar, 100).NpgsqlValue = resulasoci
                ''   .Parameters.Add("@genea", NpgsqlDbType.Varchar, 100).NpgsqlValue = "null"
                '.Parameters.Add("@servici", NpgsqlDbType.Varchar).NpgsqlValue = resultadoSQLservi
                ''    .Parameters.Add("@separtic", NpgsqlDbType.Varchar, 50).NpgsqlValue = "null"
                ''.Parameters.Add(New NpgsqlParameter("@imagen", NpgsqlDbType.Bytea)).NpgsqlValue = arrImage
            End With
            MsgBox("el supuesto comando=" + cmd.CommandText)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Se ha Guardado el registro correctamente", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'Call Cargar_Combo(ComboBox1, "select codigo from logos order by codigo;")
        Catch sqlExc As NpgsqlException
            MessageBox.Show(sqlExc.ToString, "SQL Exception Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MsgBox("entra aca")
            MsgBox(ex.Message)
        Finally
            If ConnectionState.Open Then
                conexion.desconectar()
            End If
        End Try
    End Sub

    Private Sub FormAgregar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call ObUtilidades.Cargar_Combo(CBraza, "select descripcion from raza;")
        Call ObUtilidades.Cargar_Combo(CBandar, "select descripcion from andares;")
        Call ObUtilidades.Cargar_Combo(CbcolorPelaje, "select descripcion from colores;")
        Call ObUtilidades.Cargar_Combo(CBasociacion, "select descripcion from asociaciones;")
        Call ObUtilidades.Cargar_Combo(CBservicios, "select descripcion from servicios;")
    End Sub
End Class