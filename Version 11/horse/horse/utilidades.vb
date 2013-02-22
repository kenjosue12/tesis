Imports System.Data
Imports Npgsql
Imports System.IO
Imports NpgsqlTypes
Imports horse.BD
Public Class utilidades
    Public conn As NpgsqlConnection = Nothing
    Public myGlobalDataSet As DataSet
    Public myGlobalCommand As NpgsqlCommand
    Public consultaSQL As String
    ' Public conexion As New BD

    Function IncrementaCodigo(ByVal consulta As String) As Integer
        Dim conexion As New BD
        Dim consu = consulta
        Dim Leer As NpgsqlDataReader
        Dim nextResult As Boolean = True
        conexion.Conectar()
        'consulta = "select max(idcaballos)as id from caballos;"
        myGlobalCommand = New NpgsqlCommand(consu, conexion.conexionBD)
        ' myGlobalCommand.CommandType = CommandType.t
        Try
            Leer = myGlobalCommand.ExecuteReader
            If (Leer.Read()) Then
                If IsDBNull(Leer.Item("id")) = False Then
                    IncrementaCodigo = Leer.Item("id")
                Else
                    IncrementaCodigo = 0
                End If
            Else
                MsgBox("Problemas con los Datos", MsgBoxStyle.Information, "Imagen")
                Leer.Close()

            End If

            'IncrementaCodigo = Leer.GetValue(1)

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Exclamation, "sistema")
        End Try
        conexion.desconectar() 'lo agregue porke es necesario 
        Return IncrementaCodigo 'tambien se agrego porke no estabaaaa
    End Function
    'FUNCION PARA CARGAR COMBO BOX 
    Public Sub Cargar_Combo(ByVal ComboBox As ComboBox, ByVal sql As String)
        Dim conexion As New BD
        conexion.Conectar()
        Try
            Dim cmd As New NpgsqlCommand(sql, conexion.conexionBD)
            Dim da As New NpgsqlDataAdapter(cmd)
            Dim ds As New DataSet
            da.Fill(ds)
            ComboBox.DataSource = ds.Tables(0)
            ComboBox.DisplayMember = ds.Tables(0).Columns(0).Caption
            conexion.desconectar()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, _
                            "Error", MessageBoxButtons.OK, _
                            MessageBoxIcon.Error)
        End Try
    End Sub

End Class
