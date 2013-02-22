Imports horse.utilidades
Imports horse.BD
Imports Npgsql
Imports NpgsqlTypes
Imports System.IO
Imports System.Data


Public Class formBuscarHistoria
    Public Sub New()
        InitializeComponent()

    End Sub

    Private Sub VButton1_Click(sender As Object, e As EventArgs) Handles VButton1.Click
        FormHistoriaClinica.ShowDialog()
    End Sub
End Class