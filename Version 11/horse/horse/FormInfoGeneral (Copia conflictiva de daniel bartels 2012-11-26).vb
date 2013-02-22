Imports System.Data.SqlClient
Imports horse.BD
Imports Npgsql

Public Class FormInfoGeneral

    Public cadenaSQL As String
    Public resultadoSQL As NpgsqlDataReader
    Private conexion As BD

    Public Sub New(ByVal conec As BD)

        Me.InitializeComponent()
        conexion = conec
    End Sub

    'BOTON BUSCAR'
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim nombre As String = TBnombre.Text
        Dim nregistro As String = TBregistro.Text

        If String.IsNullOrEmpty(TBnombre.Text) Then
            If String.IsNullOrEmpty(TBregistro.Text) Then
                MsgBox("Ingrese Datos a Buscar")
            End If
        Else
            cadenaSQL = "select idcaballos from caballos where nombre ='" + nombre + "' ;"
            resultadoSQL = conexion.consultar(cadenaSQL, conexion.conexionBD)
            If resultadoSQL.Read Then

                Dim colmmnas As Integer = 0
                Try
                    colmmnas = Convert.ToInt16(resultadoSQL.FieldCount.ToString)
                Catch
                    Return
                End Try
                Dim PosicionCol As Integer = 0
                Dim PosicionFil As Integer = 0

                Try
                    DataGridView1.Columns.Clear()
                    DataGridView1.Rows.Clear()
                Catch
                End Try

                While PosicionCol < colmmnas
                    DataGridView1.Columns.Add(" ", resultadoSQL.GetName(PosicionCol).ToString) 'nombre de las columnas
                    PosicionCol += 1
                End While

                While resultadoSQL.Read
                    DataGridView1.Rows.Add()
                    PosicionCol = 0
                    While PosicionCol < colmmnas
                        DataGridView1.Item(PosicionCol, PosicionFil).Value = resultadoSQL.GetString(0)
                        PosicionCol += 1
                        MsgBox("contenido=" + resultadoSQL.GetString(0))
                    End While
                    PosicionFil += 1
                End While

                'conexion.desconectar()
            End If

            'select idcaballos,numero_registro,nombre,raza,sexo,andar,color_pelaje,fecha_nacimiento,fecha_registro,estado,senas_particulares,propietario,asociacion, servicios from caballos where nombre ='
            '

        End If
        'DataGridView1.ColumnCount = 3

        'Dim chk As New DataGridViewCheckBoxColumn()
        'DataGridView1.Columns.Add(chk)
        'chk.HeaderText = "Check Data"
        'chk.Name = "chk"

    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        FormBuscarAvanzado.ShowDialog()

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        FormAgregar.ShowDialog()


    End Sub
End Class