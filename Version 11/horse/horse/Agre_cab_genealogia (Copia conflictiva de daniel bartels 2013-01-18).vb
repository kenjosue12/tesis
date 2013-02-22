Imports Npgsql
Imports NpgsqlTypes
Imports System.IO
Imports System.Data

Public Class Agre_cab_genealogia

    Dim id_caba As String
    Dim parentesco As String
    Dim id_nuevo_cab_parient As String
    Public conexion As New BD
    Public ObUtilidades As New utilidades

    'constructor para recibir los parametros, el id del caballo y el nombre
    Public Sub New(ByVal id As String, ByVal pariente As String)
        id_caba = id
        parentesco = pariente
        Me.InitializeComponent()
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RB_propiedad.CheckedChanged

        Me.Size = New Size(375, 154) 'cambio el tamaño de la ventana para que no muestre los demas labels
        'cuando es un caballo existente

        ObUtilidades.Cargar_Combo(todos_caballos_gen, "select idcaballos || '-> ' ||nombre  from caballos;")
        todos_caballos_gen.Visible = True
        lab_caball_gene.Visible = True
        label_nom_cab_gene.Visible = False
        nomb_caba_nue_genealo.Visible = False
        
        LB_raza.Visible = False
        
        CB_raza.Visible = False
        BT_aceptar.Location = New Point(156, 86)
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RB_nuevo.CheckedChanged
        'cuando es un caballo  nuevo...................
        Me.Size = New Size(375, 204)
        todos_caballos_gen.Visible = False
        lab_caball_gene.Visible = False
        label_nom_cab_gene.Visible = True
        nomb_caba_nue_genealo.Visible = True
        
        LB_raza.Visible = True
        
        ObUtilidades.Cargar_Combo(CB_raza, "select descripcion from raza;")
        CB_raza.Visible = True
        BT_aceptar.Location = New Point(59, 131) 'movemos el boton para que quede en la posicion adecuada
    End Sub

    'Boton ACEPTAR  para ingresar un nuevo caballo, o escoger uno de los que hay ya, segun escoja. 
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BT_aceptar.Click

        If RB_nuevo.Checked = True Then
            'tenemos q insertar este caballo como uno nuevo y tambn actualizar la genealogia del caballo actual
            If nomb_caba_nue_genealo.Text.Equals("") Then ' && CB_raza.selec OJOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
                MsgBox("EL nombre del caballo y la Raza son Obligatorios")
            Else
                'primero debemos saber si este caballo ya tiene algun pariente..... si lo tiene pues usamos ese id de genealogia si no creamos uno
                conexion.Conectar()
                Dim resul_genealog = ""
                Dim cadenaSQL = "select genealogia from caballos where idcaballos='" + id_caba + "';"
                Dim resultadoSQLservi As NpgsqlDataReader = conexion.consultar(cadenaSQL, conexion.conexionBD)
                Dim id_genealogi As String 'id de genealogia que generamos

                If resultadoSQLservi.Read Then
                    If Not (resultadoSQLservi.IsDBNull(0)) Then 'lo que sucede es q cuando el registro viene vacio.. pues daba error tocaba validarlo de esta manera gx gx gx
                        resul_genealog = resultadoSQLservi.GetString(0)
                    End If
                End If
                conexion.desconectar()
                If resul_genealog.Equals("") Then
                    Try
                        If conexion.Conectar() = False Then
                            MsgBox("Problemas en la Conexión", MsgBoxStyle.Exclamation)
                            Me.Close()
                        End If

                        id_nuevo_cab_parient = ObUtilidades.IncrementaCodigo("select max(idcaballos)as id from caballos") + 1
                        cadenaSQL = "Insert into caballos(idcaballos,nombre,estado) values (@id_genea,@nom_caball_pariente,'a');"
                        Dim cmd1 As New NpgsqlCommand(cadenaSQL, conexion.conexionBD)
                        With cmd1
                            .Parameters.Add("@id_genea", NpgsqlDbType.Varchar, 10).NpgsqlValue = id_nuevo_cab_parient
                            .Parameters.Add("@nom_caball_pariente", NpgsqlDbType.Varchar, 10).NpgsqlValue = nomb_caba_nue_genealo.Text
                        End With
                        cmd1.ExecuteNonQuery()

                        id_genealogi = ObUtilidades.IncrementaCodigo("select max(idgenealogia)as id from genealogia") + 1
                        cadenaSQL = "Insert into genealogia(idgenealogia," + parentesco + ") values (@id_genea,@id_caball_pariente);"
                        Dim cmd2 As New NpgsqlCommand(cadenaSQL, conexion.conexionBD)
                        With cmd2
                            .Parameters.Add("@id_genea", NpgsqlDbType.Varchar, 10).NpgsqlValue = id_genealogi
                            .Parameters.Add("@id_caball_pariente", NpgsqlDbType.Varchar, 10).NpgsqlValue = id_nuevo_cab_parient
                        End With
                        cmd2.ExecuteNonQuery()
                        Dim cmd3 As New NpgsqlCommand("Update caballos SET genealogia=@id_genealo where idcaballos=@id", conexion.conexionBD)
                        With cmd3
                            .Parameters.Add("@id", NpgsqlDbType.Varchar).NpgsqlValue = id_caba
                            .Parameters.Add("@id_genealo", NpgsqlDbType.Varchar, 30).NpgsqlValue = id_genealogi
                        End With
                        cmd3.ExecuteNonQuery()
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

                Else
                    'el caballo ya tiene algun familiar
                    Try
                        If conexion.Conectar() = False Then
                            MsgBox("Problemas en la Conexión", MsgBoxStyle.Exclamation)
                            Me.Close()
                        End If

                        id_nuevo_cab_parient = ObUtilidades.IncrementaCodigo("select max(idcaballos)as id from caballos") + 1
                        cadenaSQL = "Insert into caballos(idcaballos,nombre,estado) values (@id_genea,@nom_caball_pariente,'a');"
                        Dim cmd1 As New NpgsqlCommand(cadenaSQL, conexion.conexionBD)
                        With cmd1
                            .Parameters.Add("@id_genea", NpgsqlDbType.Varchar, 10).NpgsqlValue = id_nuevo_cab_parient
                            .Parameters.Add("@nom_caball_pariente", NpgsqlDbType.Varchar, 10).NpgsqlValue = nomb_caba_nue_genealo.Text
                        End With
                        cmd1.ExecuteNonQuery()

                        cadenaSQL = "update genealogia set " + parentesco + "=@id_caball_pariente where idgenealogia=@id_genea"
                        Dim cmd As New NpgsqlCommand(cadenaSQL, conexion.conexionBD)
                        With cmd
                            .Parameters.Add("@id_genea", NpgsqlDbType.Varchar, 10).NpgsqlValue = resul_genealog
                            .Parameters.Add("@id_caball_pariente", NpgsqlDbType.Varchar, 10).NpgsqlValue = id_nuevo_cab_parient
                        End With
                        cmd.ExecuteNonQuery()
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
            End If
        End If 'if del checked


        If RB_propiedad.Checked = True Then
            'es un caballo que ya es existente debemos de insertarlo pero antes tenemos que sacar del combobox el id del caballo seleccionado

            Dim caballo_selecc = todos_caballos_gen.Text
            Dim arrFilename() As String = Split(caballo_selecc, " ")

            Dim aux As String = arrFilename(0)
            Dim id() As String = Split(aux, "->") 'por fin tenemos el id limpio 

            Dim nombre_caballo_pariente As String = arrFilename(1) 'tenemos el nombre del caballo pariente 
            Dim id_caba_pariente As String = id(0) 'tenemos el id del caballo que es el pariente

            ' primero comprobar si este caballo tiene un pariente, si es asi solo actualizamos, si no pues tenemos q hacer un insert....



            conexion.Conectar()
            Dim resul_genealog = ""
            Dim cadenaSQL = "select genealogia from caballos where idcaballos='" + id_caba + "';"
            Dim resultadoSQLservi As NpgsqlDataReader = conexion.consultar(cadenaSQL, conexion.conexionBD)
            Dim id_genealogi As String 'id de genealogia que generamos

            If resultadoSQLservi.Read Then
                If Not (resultadoSQLservi.IsDBNull(0)) Then 'lo que sucede es q cuando el registro viene vacio.. pues daba error tocaba validarlo de esta manera gx gx gx
                    resul_genealog = resultadoSQLservi.GetString(0)
                End If
            End If
            conexion.desconectar()

            If resul_genealog.Equals("") Then 'comprobamos q el caballo no tenga una genealogia si la tiene pues toca es actualizar si no insertar
                'el caballo no tiene ningun familiar

                Try
                    If conexion.Conectar() = False Then
                        MsgBox("Problemas en la Conexión", MsgBoxStyle.Exclamation)
                        Me.Close()
                    End If

                    id_genealogi = ObUtilidades.IncrementaCodigo("select max(idgenealogia)as id from genealogia") + 1
                    cadenaSQL = "Insert into genealogia(idgenealogia," + parentesco + ") values (@id_genea,@id_caball_pariente);"
                    Dim cmd As New NpgsqlCommand(cadenaSQL, conexion.conexionBD)
                    With cmd
                        .Parameters.Add("@id_genea", NpgsqlDbType.Varchar, 10).NpgsqlValue = id_genealogi
                        .Parameters.Add("@id_caball_pariente", NpgsqlDbType.Varchar, 10).NpgsqlValue = id_caba_pariente
                    End With
                    cmd.ExecuteNonQuery()
                    Dim cmd1 As New NpgsqlCommand("Update caballos SET genealogia=@id_genealo where idcaballos=@id", conexion.conexionBD)
                    With cmd1
                        .Parameters.Add("@id", NpgsqlDbType.Varchar).NpgsqlValue = id_caba
                        .Parameters.Add("@id_genealo", NpgsqlDbType.Varchar, 30).NpgsqlValue = id_genealogi
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


            Else
                'el caballo ya tiene algun familiar
                Try
                    If conexion.Conectar() = False Then
                        MsgBox("Problemas en la Conexión", MsgBoxStyle.Exclamation)
                        Me.Close()
                    End If
                    cadenaSQL = "update genealogia set " + parentesco + "=@id_caball_pariente where idgenealogia=@id_genea"
                    Dim cmd As New NpgsqlCommand(cadenaSQL, conexion.conexionBD)
                    With cmd
                        .Parameters.Add("@id_genea", NpgsqlDbType.Varchar, 10).NpgsqlValue = resul_genealog
                        .Parameters.Add("@id_caball_pariente", NpgsqlDbType.Varchar, 10).NpgsqlValue = id_caba_pariente
                    End With
                    cmd.ExecuteNonQuery()
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

        End If
    End Sub


    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles agre_mas_info.Click
        If nomb_caba_nue_genealo.Text.Equals("") Then ' && CB_raza.selec OJOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
            MsgBox("EL nombre del caballo y la Raza son Obligatorios")
        Else
            'primero debemos saber si este caballo ya tiene algun pariente..... si lo tiene pues usamos ese id de genealogia si no creamos uno
            conexion.Conectar()
            Dim resul_genealog = ""
            Dim cadenaSQL = "select genealogia from caballos where idcaballos='" + id_caba + "';"
            Dim resultadoSQLservi As NpgsqlDataReader = conexion.consultar(cadenaSQL, conexion.conexionBD)
            Dim id_genealogi As String 'id de genealogia que generamos

            If resultadoSQLservi.Read Then
                If Not (resultadoSQLservi.IsDBNull(0)) Then 'lo que sucede es q cuando el registro viene vacio.. pues daba error tocaba validarlo de esta manera gx gx gx
                    resul_genealog = resultadoSQLservi.GetString(0)
                End If
            End If
            conexion.desconectar()
            If resul_genealog.Equals("") Then
                Try
                    If conexion.Conectar() = False Then
                        MsgBox("Problemas en la Conexión", MsgBoxStyle.Exclamation)
                        Me.Close()
                    End If

                    id_nuevo_cab_parient = ObUtilidades.IncrementaCodigo("select max(idcaballos)as id from caballos") + 1
                    cadenaSQL = "Insert into caballos(idcaballos,nombre,estado) values (@id_genea,@nom_caball_pariente,'a');"
                    Dim cmd1 As New NpgsqlCommand(cadenaSQL, conexion.conexionBD)
                    With cmd1
                        .Parameters.Add("@id_genea", NpgsqlDbType.Varchar, 10).NpgsqlValue = id_nuevo_cab_parient
                        .Parameters.Add("@nom_caball_pariente", NpgsqlDbType.Varchar, 10).NpgsqlValue = nomb_caba_nue_genealo.Text
                    End With
                    cmd1.ExecuteNonQuery()

                    id_genealogi = ObUtilidades.IncrementaCodigo("select max(idgenealogia)as id from genealogia") + 1
                    cadenaSQL = "Insert into genealogia(idgenealogia," + parentesco + ") values (@id_genea,@id_caball_pariente);"
                    Dim cmd2 As New NpgsqlCommand(cadenaSQL, conexion.conexionBD)
                    With cmd2
                        .Parameters.Add("@id_genea", NpgsqlDbType.Varchar, 10).NpgsqlValue = id_genealogi
                        .Parameters.Add("@id_caball_pariente", NpgsqlDbType.Varchar, 10).NpgsqlValue = id_nuevo_cab_parient
                    End With
                    cmd2.ExecuteNonQuery()
                    Dim cmd3 As New NpgsqlCommand("Update caballos SET genealogia=@id_genealo where idcaballos=@id", conexion.conexionBD)
                    With cmd3
                        .Parameters.Add("@id", NpgsqlDbType.Varchar).NpgsqlValue = id_caba
                        .Parameters.Add("@id_genealo", NpgsqlDbType.Varchar, 30).NpgsqlValue = id_genealogi
                    End With
                    cmd3.ExecuteNonQuery()
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

            Else
                'el caballo ya tiene algun familiar
                Try
                    If conexion.Conectar() = False Then
                        MsgBox("Problemas en la Conexión", MsgBoxStyle.Exclamation)
                        Me.Close()
                    End If

                    id_nuevo_cab_parient = ObUtilidades.IncrementaCodigo("select max(idcaballos)as id from caballos") + 1
                    cadenaSQL = "Insert into caballos(idcaballos,nombre,estado) values (@id_genea,@nom_caball_pariente,'a');"
                    Dim cmd1 As New NpgsqlCommand(cadenaSQL, conexion.conexionBD)
                    With cmd1
                        .Parameters.Add("@id_genea", NpgsqlDbType.Varchar, 10).NpgsqlValue = id_nuevo_cab_parient
                        .Parameters.Add("@nom_caball_pariente", NpgsqlDbType.Varchar, 10).NpgsqlValue = nomb_caba_nue_genealo.Text
                    End With
                    cmd1.ExecuteNonQuery()

                    cadenaSQL = "update genealogia set " + parentesco + "=@id_caball_pariente where idgenealogia=@id_genea"
                    Dim cmd As New NpgsqlCommand(cadenaSQL, conexion.conexionBD)
                    With cmd
                        .Parameters.Add("@id_genea", NpgsqlDbType.Varchar, 10).NpgsqlValue = resul_genealog
                        .Parameters.Add("@id_caball_pariente", NpgsqlDbType.Varchar, 10).NpgsqlValue = id_nuevo_cab_parient
                    End With
                    cmd.ExecuteNonQuery()
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
        End If

        Dim agregar As New FormAgregar(id_nuevo_cab_parient, "g")
        agregar.Text = "Editar Caballo"
        agregar.ShowDialog()
    End Sub
End Class