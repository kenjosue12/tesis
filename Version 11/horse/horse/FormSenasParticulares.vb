Imports horse.utilidades
Imports horse.BD
Imports Npgsql
Imports NpgsqlTypes
Imports System.IO
Imports System.Data
Imports horse.FormAgregar
Public Class FormSenasParticulares
    Public conexion As New BD
    Public objUtilidades As New utilidades
    Public idcaba As String
    Public idsenales As String
    Public parteCuerpo As String
    Public cadenaSQLparte As String
    Public ObUtilidades As New utilidades
    Private formu_agregar As FormAgregar

    Public Sub New(ByVal idcaballo As String, ByVal parte As String, ByVal formagre As FormAgregar)
        idcaba = idcaballo
        parteCuerpo = parte
        formu_agregar = formagre
        ' Llamada necesaria para el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BTguardarnuevasena.Click

        If TBsenas.Text <> "" Then
            If conexion.Conectar() = False Then
                MsgBox("Problemas en la Conexión", MsgBoxStyle.Exclamation)
                Me.Close()
            End If

            If parteCuerpo.Equals("c") Then
                Dim cadenaSQLpatas = "select idsenales_particulares from senales_particulares where descripcion='" + TBsenas.Text + "' and ubicacion='1' and id_caballo='" + idcaba + "';"
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
                            .Parameters.Add("@descripcion", NpgsqlDbType.Varchar).NpgsqlValue = TBsenas.Text
                            .Parameters.Add("@ubicacion", NpgsqlDbType.Integer).NpgsqlValue = 1
                            .Parameters.Add("@id_caballo", NpgsqlDbType.Integer).NpgsqlValue = idcaba
                        End With
                        cmd1.ExecuteNonQuery()
                        MessageBox.Show("Se ha guardado el registro correctamente", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        formu_agregar.llenarListView(idcaba)
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


            End If
            If parteCuerpo.Equals("t") Then
                Dim cadenaSQLpatas = "select idsenales_particulares from senales_particulares where descripcion='" + TBsenas.Text + "' and ubicacion='2' and id_caballo='" + idcaba + "';"
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
                            .Parameters.Add("@descripcion", NpgsqlDbType.Varchar).NpgsqlValue = TBsenas.Text
                            .Parameters.Add("@ubicacion", NpgsqlDbType.Integer).NpgsqlValue = 2
                            .Parameters.Add("@id_caballo", NpgsqlDbType.Integer).NpgsqlValue = idcaba
                        End With
                        cmd1.ExecuteNonQuery()
                        MessageBox.Show("Se ha guardado el registro correctamente", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        formu_agregar.llenarListView(idcaba)
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


            End If
            If parteCuerpo.Equals("p") Then

                Dim cadenaSQLpatas = "select idsenales_particulares from senales_particulares where descripcion='" + TBsenas.Text + "' and ubicacion='3' and id_caballo='" + idcaba + "';"
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
                            .Parameters.Add("@descripcion", NpgsqlDbType.Varchar).NpgsqlValue = TBsenas.Text
                            .Parameters.Add("@ubicacion", NpgsqlDbType.Integer).NpgsqlValue = 3
                            .Parameters.Add("@id_caballo", NpgsqlDbType.Integer).NpgsqlValue = idcaba
                        End With
                        cmd1.ExecuteNonQuery()
                        MessageBox.Show("Se ha guardado el registro correctamente", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        formu_agregar.llenarListView(idcaba)
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

            End If
            Call ObUtilidades.Cargar_Combo(formu_agregar.CBcabeza, "select distinct descripcion from senales_particulares where ubicacion='1';")
            Call ObUtilidades.Cargar_Combo(formu_agregar.CBtronco, "select distinct descripcion from senales_particulares  where ubicacion='2';")
            Call ObUtilidades.Cargar_Combo(formu_agregar.CBpatas, "select distinct descripcion from senales_particulares  where ubicacion='3';")

        Else
            MsgBox("no ha escrito ninguna seña ")
        End If
    End Sub

    Private Sub FormSenasParticulares_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conexion.Conectar()
        Label1.Text = objUtilidades.IncrementaCodigo("select max(idsenales_particulares)as id from senales_particulares") + 1

        conexion.desconectar()
    End Sub

End Class