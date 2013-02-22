Imports Npgsql
Imports System.Data
Imports System.IO
Imports NpgsqlTypes
Public Class BD

    Public conexionBD As NpgsqlConnection
    Public transaction As NpgsqlTransaction
    Public usuario As String
    Public password As String
    Public activa As Boolean



    Public Function Conectar() As Boolean
        Try
            Dim Cadena As String = "User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=equinos_db;"
            conexionBD = New NpgsqlConnection(Cadena)
            conexionBD.Open()
            activa = True
            Conectar = True
            Exit Function
        Catch ex As Exception
            MsgBox("ERROR LA BASE DE DATOS NO SE ENCUENTRA DISPONIBLE")
            activa = False
            Conectar = False
            Exit Function
        End Try
    End Function

    Public Function desconectar() As Boolean
        Try
            conexionBD.Close()
            activa = False
            desconectar = True
            Exit Function
        Catch ex As NpgsqlException
            MsgBox(ex.Message)
            activa = False
            desconectar = False
            Exit Function
        End Try
    End Function

    Public Function consultar(ByVal sql As String, conexionBD As NpgsqlConnection) As NpgsqlDataReader

        Dim ejecutar As NpgsqlCommand = New NpgsqlCommand(sql, conexionBD)
        Dim resultadoSQL As NpgsqlDataReader
        Try
            ejecutar.Transaction = transaction
            resultadoSQL = ejecutar.ExecuteReader()
            consultar = resultadoSQL
            Exit Function
        Catch ex As NpgsqlException
            MsgBox(ex.Message)
            consultar = resultadoSQL

            Exit Function
        End Try
    End Function

    Public Sub CargarDataGridView(ByVal ElDataGridView As DataGridView, ByVal LaConsulta As String) 'nos permite llenar un datagridview con datos de una sola tabla

        Dim ElDataTable As DataTable = New DataTable
        Dim ElReader As NpgsqlDataReader
        Dim ElComando As NpgsqlCommand

        Try
            ElComando = New NpgsqlCommand(LaConsulta, conexionBD)
            ElReader = ElComando.ExecuteReader()
            If ElReader.Read Then
                MsgBox("llego")
                ElDataTable.Load(ElReader)
                ElDataGridView.DataSource = ElDataTable
            End If

        Catch ex As Exception
            MsgBox("Error al cargar el DataGridView.", MsgBoxStyle.Critical, "Error")
        End Try

    End Sub
    Public Function cargarDatos(ByVal oCon As NpgsqlConnection)

        'Dim daDatos As New NpgsqlDataAdapter ' Objeto Adaptador para leer datos de la Base de datos
        'Dim cmdExec As New NpgsqlCommand ' objeto comando para ejecutar sentencias sql
        'Dim dtDatos As New DataSet ' datatable para recibir los datos de la base de datos
        'Dim sbQuery As String ' StringBuilder para armar cadenas
        'Dim CrReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        'Try

        '    oCnn.ConnectionString = Gbcadena 'Mi cadena de conexion
        '    oCnn.Open()
        '    cmdExec = oCnn.CreateCommand
        '    cmdExec.Connection = oCnn

        '    'mi query a ejecutar
        '    sbQuery = "select a.id_articulo,a.nombre,a.codigo_barras,ar.stock, ar.numero_sucursal "
        '    sbQuery += "from "
        '    sbQuery += "articulos a, "
        '    sbQuery += "sucursales s, "
        '    sbQuery += "articulos_sucursales ar "
        '    sbQuery += "where a.id_articulo = ar.id_articulo "
        '    sbQuery += "and s.numero_sucursal=ar.numero_sucursal "
        '    sbQuery += "and ar.numero_sucursal=1 "

        '    cmdExec.CommandText = sbQuery.ToString
        '    daDatos = New NpgsqlDataAdapter(cmdExec) 'OleDbDataAdapter(cmdExec)
        '    daDatos.Fill(dtDatos)


        '    ' Asigno el reporte 
        '    'CrReport = New CrystalDecisions.CrystalReports.Engine.ReportDocument()
        '    CrReport.Load(CurDir() & "/reportes/CRPlanillas.rpt")

        '    CrReport.SetDataSource(dtDatos)

        '    Crystal.ReportSource = CurDir() & "/reportes/CRPlanillas.rpt"

        'Catch ex As Exception
        '    MessageBox.Show("excepcion: " & ex.Message, "Mostrando Reporte")
        'End Try

        Return True
    End Function

End Class
