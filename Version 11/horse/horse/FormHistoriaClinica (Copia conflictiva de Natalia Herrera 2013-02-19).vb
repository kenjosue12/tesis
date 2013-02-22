Imports horse.utilidades
Imports horse.BD
Imports Npgsql
Imports NpgsqlTypes
Imports System.IO
Imports System.Data


Public Class FormHistoriaClinica

    Public estiloForm As String = "none"
    Public Sub New()

        ' Llamada necesaria para el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub

    'controlar el cambio de color cuando pase el mouse por encima'''''''''''''''''''''''''''''''''''''''''

    Private Sub Panel1_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel1.MouseMove
        Panel1.BackColor = Color.LightGray
    End Sub

    Private Sub Panel1_MouseLeave(sender As Object, e As EventArgs) Handles Panel1.MouseLeave
        Panel1.BackColor = Color.WhiteSmoke
    End Sub
    Private Sub Panel2_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel2.MouseMove
        Panel2.BackColor = Color.LightGray
    End Sub
    Private Sub Panel2_MouseLeave(sender As Object, e As EventArgs) Handles Panel2.MouseLeave
        Panel2.BackColor = Color.WhiteSmoke
    End Sub
    Private Sub Panel3_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel3.MouseMove
        Panel3.BackColor = Color.LightGray
    End Sub
    Private Sub Panel3_MouseLeave(sender As Object, e As EventArgs) Handles Panel3.MouseLeave
        Panel3.BackColor = Color.WhiteSmoke
    End Sub
    Private Sub Panel4_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel4.MouseMove
        Panel4.BackColor = Color.LightGray
    End Sub
    Private Sub Panel4_MouseLeave(sender As Object, e As EventArgs) Handles Panel4.MouseLeave
        Panel4.BackColor = Color.WhiteSmoke
    End Sub

    Private Sub Panel5_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel5.MouseMove
        Panel5.BackColor = Color.LightGray
    End Sub
    Private Sub Panel5_MouseLeave(sender As Object, e As EventArgs) Handles Panel5.MouseLeave
        Panel5.BackColor = Color.WhiteSmoke
    End Sub
    Private Sub Panel6_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel6.MouseMove
        Panel6.BackColor = Color.LightGray
    End Sub
    Private Sub Panel6_MouseLeave(sender As Object, e As EventArgs) Handles Panel6.MouseLeave
        Panel6.BackColor = Color.WhiteSmoke
    End Sub
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'Funcion para verificar que panel esta activado, y poner su borde en "none"
    Public Function verificarPanelActivado()
        If estiloForm = "panel1" Then
            Panel1.BorderStyle = BorderStyle.None
        End If
        If estiloForm = "panel2" Then
            Panel2.BorderStyle = BorderStyle.None
        End If

        If estiloForm = "panel3" Then
            Panel3.BorderStyle = BorderStyle.None
        End If

        If estiloForm = "panel4" Then
            Panel4.BorderStyle = BorderStyle.None
        End If

        If estiloForm = "panel5" Then
            Panel5.BorderStyle = BorderStyle.None
        End If
        If estiloForm = "panel6" Then
            Panel6.BorderStyle = BorderStyle.None
        End If

    End Function

    'Grupo de funcion para poner borde 3D.***************************************************************
    'Solo uno de los paneles se pone con borde 3D, por medio de la funcion verificar panel.
    Private Sub Panel1_MouseClick(sender As Object, e As MouseEventArgs) Handles Panel1.MouseClick
        If estiloForm.Equals("none") Then
            Panel1.BorderStyle = BorderStyle.Fixed3D
            estiloForm = "panel1"
        Else
            If estiloForm <> "panel1" Then
                verificarPanelActivado()
                Panel1.BorderStyle = BorderStyle.Fixed3D
                estiloForm = "panel1"
            End If
        End If

    End Sub

    Private Sub Panel2_MouseClick(sender As Object, e As MouseEventArgs) Handles Panel2.MouseClick
        If estiloForm.Equals("none") Then
            Panel2.BorderStyle = BorderStyle.Fixed3D
            estiloForm = "panel2"
        Else
            If estiloForm <> "panel2" Then
                verificarPanelActivado()
                Panel2.BorderStyle = BorderStyle.Fixed3D
                estiloForm = "panel2"
            End If
        End If
    End Sub

    Private Sub Panel3_MouseClick(sender As Object, e As MouseEventArgs) Handles Panel3.MouseClick
        If estiloForm.Equals("none") Then
            Panel3.BorderStyle = BorderStyle.Fixed3D
            estiloForm = "panel3"
        Else
            If estiloForm <> "panel3" Then
                verificarPanelActivado()
                Panel3.BorderStyle = BorderStyle.Fixed3D
                estiloForm = "panel3"
            End If
        End If
    End Sub
    Private Sub Panel4_MouseClick(sender As Object, e As MouseEventArgs) Handles Panel4.MouseClick
        If estiloForm.Equals("none") Then
            Panel4.BorderStyle = BorderStyle.Fixed3D
            estiloForm = "panel4"
        Else
            If estiloForm <> "panel4" Then
                verificarPanelActivado()
                Panel4.BorderStyle = BorderStyle.Fixed3D
                estiloForm = "panel4"
            End If
        End If
    End Sub

    Private Sub Panel5_MouseClick(sender As Object, e As MouseEventArgs) Handles Panel5.MouseClick
        If estiloForm.Equals("none") Then
            Panel5.BorderStyle = BorderStyle.Fixed3D
            estiloForm = "panel5"
        Else
            If estiloForm <> "panel5" Then
                verificarPanelActivado()
                Panel5.BorderStyle = BorderStyle.Fixed3D
                estiloForm = "panel5"
            End If
        End If

    End Sub

    Private Sub Panel6_MouseClick(sender As Object, e As MouseEventArgs) Handles Panel6.MouseClick
        If estiloForm.Equals("none") Then
            Panel6.BorderStyle = BorderStyle.Fixed3D
            estiloForm = "panel6"
        Else
            If estiloForm <> "panel6" Then
                verificarPanelActivado()
                Panel6.BorderStyle = BorderStyle.Fixed3D
                estiloForm = "panel6"
            End If
        End If

    End Sub

    ''Fin grupo de funcion borde 3D******************************************

    
End Class