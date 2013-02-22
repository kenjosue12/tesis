Imports horse.BD
Imports Npgsql
Imports System.Data
Public Class InicioSesion

    Public cadenaSQL As String
    'Public resultadoSQL As NpgsqlDataReader


    Public Sub New()
        ' The Me.InitializeComponent call is required for Windows Forms designer support.
        Me.InitializeComponent()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Public Function ingresar()
        Dim usuario As String = TBusuario.Text
        Dim clave As String = TBcontrase.Text
        Dim conexion As New BD
        Dim sesion = usuario + clave
        conexion.Conectar()


        If conexion.activa Then
            cadenaSQL = "select identificacion from personas where nombres= '" + usuario + "' and contrasena='" + clave + "';"
            Dim resultadoSQL As NpgsqlDataReader = conexion.consultar(cadenaSQL, conexion.conexionBD)
            If resultadoSQL.Read Then
                Dim log As New FormPrincipal(Me, conexion)
                resultadoSQL.Close()
                log.Show()
                Me.Hide()
            Else
                MsgBox("Datos Incompletos O Erroneos", MsgBoxStyle.Information)
            End If
        Else
            MsgBox("Sin conexion", MsgBoxStyle.Critical)
        End If
        Return True
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Call ingresar()
    End Sub

 
End Class