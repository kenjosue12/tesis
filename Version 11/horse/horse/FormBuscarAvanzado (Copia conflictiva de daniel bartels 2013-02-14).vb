Imports System.Data.SqlClient
Imports horse.utilidades
Imports horse.BD
Imports Npgsql
Imports NpgsqlTypes
Imports System.IO
Imports System.Data
Imports horse.FormInfoGeneral
Public Class FormBuscarAvanzado
    Public obUtilidades As New utilidades
    Public cadenaSQl As String
    Public conexion As BD
    Public infoGeneral As New FormInfoGeneral
    Public resultadoSQL As NpgsqlDataReader
    Public raza As String
    Public resul As String
    Public cadenaId As String
    Public resultadoId As String
    Public dato As String

    Public Sub New(ByVal conec As BD)

        Me.InitializeComponent()
        conexion = conec

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub
    Private Sub FormBuscarAvanzado_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        
       
        

    End Sub


    '***********BOTON QUE TRAE LA CONSULTA EN EL DATAGRID***************
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        conexion.Conectar()


        Dim consultaSQLAvanzada = "select idandares from andares where descripcion='" + TextBox1.Text + "';"
        Dim resultadoSQL As NpgsqlDataReader = conexion.consultar(consultaSQLAvanzada, conexion.conexionBD)
        If resultadoSQL.Read Then
            Dim idandar = resultadoSQL.GetInt32(0)
            Dim consultaSQLandar = "select *  from caballos c, andares a where c.andar=idandares and idandares='" + idandar + "';"
            Dim resultadoSQLandar As NpgsqlDataReader = conexion.consultar(consultaSQLandar, conexion.conexionBD)
            If resultadoSQLandar.Read Then
                Dim ds As New DataSet
                Dim DA As New NpgsqlDataAdapter(consultaSQLandar, conexion.conexionBD)
                DA.Fill(ds)
                DataGridView1.DataSource = ds.Tables(0)
            Else
                MsgBox("No se ha encontrado registros")
            End If
        Else
            MsgBox("No se ha encontrado registros")
            'If then
            'End If

        End If
        conexion.desconectar()


    End Sub

    




End Class