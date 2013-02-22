Imports System.Data.SqlClient
Imports horse.utilidades
Imports horse.BD
Imports Npgsql
Imports NpgsqlTypes
Imports System.IO
Imports System.Data
Imports horse.FormInfoGeneral
Imports System.Globalization
Imports System.Threading
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
    Public idandar As String
    Public dt As DataTable
    Public Cerrar As String
    'estructura de datos declaracion
    Public Structure ParametrosRetorno
        Property datatable As DataTable
        Property bandera As String
        Property cancelar As String
    End Structure

    Public Sub New()

        ' Llamada necesaria para el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub
    'funcion obtener Valores (getValores), que va a inicializar los datos de la estructura declarada arriba

    Public Function GetValores() As ParametrosRetorno
        Dim conec As New BD 'creacion de objeto de la BD
        conexion = conec
        Dim retP As New ParametrosRetorno
        If Me.ShowDialog = DialogResult.OK Then
            retP.bandera = "NULL"
            retP.cancelar = Cerrar
        Else
            retP.datatable = dt
            retP.cancelar = Cerrar
        End If
        Dispose()
        Return retP

    End Function

    '***********BOTON QUE TRAE LA CONSULTA EN EL DATAGRID***************
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        conexion.Conectar()
        Dim descriConve = StrConv(TextBox1.Text, VbStrConv.ProperCase) 'se convierte el texto del textbox a como esta escrito en la base de datos: en forma titulo.
        Dim cadenaSQLandar = "select idandares from andares where descripcion='" + descriConve + "';"
        Dim resultadoSQLandar As NpgsqlDataReader = conexion.consultar(cadenaSQLandar, conexion.conexionBD)
        If Not resultadoSQLandar.Read Then
            'Si NO es Andar, viene y busque por SOCIACIONES.
            Dim descrConve = StrConv(TextBox1.Text, VbStrConv.ProperCase) 'se convierte el texto del textbox a como esta escrito en la base de datos: en forma titulo.
            Dim cadenaSQLasoci = "select idasociaciones from asociaciones where descripcion='" + descrConve + "';"
            Dim resultadoSQLaso As NpgsqlDataReader = conexion.consultar(cadenaSQLasoci, conexion.conexionBD)
            If Not resultadoSQLaso.Read Then
                'Si NO es ASOCIACION, busca por COLORES.
                Dim conveColor = StrConv(TextBox1.Text, VbStrConv.ProperCase) 'se convierte el texto del textbox a como esta escrito en la base de datos: en forma titulo.
                Dim cadenaSQLcolor = "select idcolores from colores where descripcion='" + conveColor + "';"
                Dim resultadoSQLcolor As NpgsqlDataReader = conexion.consultar(cadenaSQLcolor, conexion.conexionBD)
                If Not resultadoSQLcolor.Read Then
                    'SI NO es COLORES busca por RAZA
                    Dim conveRaza = StrConv(TextBox1.Text, VbStrConv.ProperCase) 'se convierte el texto del textbox a como esta escrito en la base de datos: en forma titulo.
                    Dim cadenaSQLRAZA = "select idraza from raza where descripcion='" + conveRaza + "';"
                    Dim resultadoSQLRAZA As NpgsqlDataReader = conexion.consultar(cadenaSQLRAZA, conexion.conexionBD)
                    If Not resultadoSQLRAZA.Read Then
                        'SI NO HAY RAZAS busca por SERVICIOS
                        Dim conveSer = StrConv(TextBox1.Text, VbStrConv.ProperCase) 'se convierte el texto del textbox a como esta escrito en la base de datos: en forma titulo.
                        Dim cadenaSQLSERVI = "select idservicios from servicios where descripcion='" + conveSer + "';"
                        Dim resultadoSQLSERVI As NpgsqlDataReader = conexion.consultar(cadenaSQLSERVI, conexion.conexionBD)
                        If Not resultadoSQLSERVI.Read Then
                            'SI NO HAY SERVICIOS
                            MsgBox("No hay SE HA ENCOTRADO SU BUSQUEDA") 'RESULTADO SI FINALMENTE NO HUBO NINGUN REGISTRO COMO EL QUE ESCRIBIO EN EL TEXT BOX. ACA FIN DE LA BUSQUEDA.
                            Cerrar = "no"
                        Else
                            'SI SI HAY SERVICIOS
                            Dim idSERV As String = resultadoSQLSERVI.GetInt32(0)

                            conexion.Conectar()
                            Dim consultaSQLcaballo = "select idcaballos, numero_registro as registro,r.descripcion as raza,nombre, sexo,a.descripcion as andar,co.descripcion as color,fecha_nacimiento ,fecha_registro,e.descripcion as estado,propietario,aso.descripcion as asociacion,s.descripcion as servicio from caballos c, raza r , andares a,colores co, estado_caballos e, asociaciones aso,servicios s where servicios='" + idSERV + "' and r.idraza=c.raza and a.idandares=c.andar and co.idcolores=c.color_pelaje and e.idestado_caballo=c.estado and aso.idasociaciones=c.asociacion and s.idservicios=c.servicios;"
                            Dim resultadoSQ As NpgsqlDataReader = conexion.consultar(consultaSQLcaballo, conexion.conexionBD)
                            If resultadoSQ.Read Then
                                conexion.Conectar()
                                Try
                                    If Not BuscarDatos(dt, consultaSQLcaballo) Then Throw New Exception("Sucedio un error al consultar los datos.") 'llama a la funcion BuscarDatos.
                                    If Not infoGeneral.RellenarListview(dt) Then Throw New Exception("Sucedio un error al rellenar el listado.")
                                    Me.Close()
                                Catch ex As Exception
                                    MsgBox(ex.Message, , "Error")
                                End Try
                                conexion.desconectar()
                            Else
                                MsgBox("No se ha encontrado Caballos")
                                Cerrar = "no"
                            End If
                            conexion.desconectar()
                        End If 'FIN SERVICIOS
                    Else
                        'SI SI HAY RAZAS
                        Dim idrazas As String = resultadoSQLRAZA.GetInt32(0)
                        MsgBox(idrazas)
                        conexion.Conectar()
                        Dim consultaSQLcaballo As String = "select idcaballos, numero_registro,r.descripcion as raza,nombre, sexo,a.descripcion as andar,co.descripcion as color,fecha_nacimiento,fecha_registro,e.descripcion as estado,propietario,aso.descripcion as asociacion,s.descripcion as servicio from caballos c, raza r , andares a,colores co, estado_caballos e, asociaciones aso,servicios s where raza='" + idrazas + "' and r.idraza=c.raza and a.idandares=c.andar and co.idcolores=c.color_pelaje and e.idestado_caballo=c.estado and aso.idasociaciones=c.asociacion and s.idservicios=c.servicios;"
                        Dim resultadoSQ As NpgsqlDataReader = conexion.consultar(consultaSQLcaballo, conexion.conexionBD)
                        If resultadoSQ.Read Then
                            conexion.Conectar()
                            Try
                                If Not BuscarDatos(dt, consultaSQLcaballo) Then Throw New Exception("Sucedio un error al consultar los datos.") 'llama a la funcion BuscarDatos.
                                If Not infoGeneral.RellenarListview(dt) Then Throw New Exception("Sucedio un error al rellenar el listado.")
                                Me.Close()
                            Catch ex As Exception
                                MsgBox(ex.Message, , "Error")
                            End Try
                            conexion.desconectar()
                        Else
                            MsgBox("No se ha encontrado Caballos")
                            Cerrar = "no"

                        End If
                        conexion.desconectar()
                    End If 'FIN DE si RAZAS
                Else
                    'SI SI hay colores, BUSCA EL CABALLO
                    Dim idcolor As String = resultadoSQLcolor.GetInt32(0)
                    MsgBox(idcolor)
                    conexion.Conectar()
                    Dim consultaSQLcaballo As String = "select idcaballos, numero_registro,r.descripcion as raza,nombre, sexo,a.descripcion as andar,co.descripcion as color,fecha_nacimiento,fecha_registro,e.descripcion as estado,propietario,aso.descripcion as asociacion,s.descripcion as servicio from caballos c, raza r , andares a,colores co, estado_caballos e, asociaciones aso,servicios s where color_pelaje='" + idcolor + "' and r.idraza=c.raza and a.idandares=c.andar and co.idcolores=c.color_pelaje and e.idestado_caballo=c.estado and aso.idasociaciones=c.asociacion and s.idservicios=c.servicios;"
                    Dim resultadoSQ As NpgsqlDataReader = conexion.consultar(consultaSQLcaballo, conexion.conexionBD)
                    If resultadoSQ.Read Then
                        conexion.Conectar()
                        Try
                            If Not BuscarDatos(dt, consultaSQLcaballo) Then Throw New Exception("Sucedio un error al consultar los datos.") 'llama a la funcion BuscarDatos.
                            If Not infoGeneral.RellenarListview(dt) Then Throw New Exception("Sucedio un error al rellenar el listado.")
                            Me.Close()
                        Catch ex As Exception
                            MsgBox(ex.Message, , "Error")
                        End Try
                        conexion.desconectar()
                    Else
                        MsgBox("No se ha encontrado Caballos")
                        Cerrar = "no"
                    End If
                    conexion.desconectar()
                End If 'FIN DE COLORES
            Else
                'SI SI HAY ASOCIACIONES BUSCA EL CABALLO
                Dim idaso As String = resultadoSQLaso.GetInt32(0)
                MsgBox(idaso)
                conexion.Conectar()
                Dim consultaSQLcaballo As String = "select idcaballos, numero_registro,r.descripcion as raza,nombre, sexo,a.descripcion as andar,co.descripcion as color,fecha_nacimiento,fecha_registro,e.descripcion as estado,propietario,aso.descripcion as asociacion,s.descripcion as servicio from caballos c, raza r , andares a,colores co, estado_caballos e, asociaciones aso,servicios s where color_pelaje='" + idaso + "' and r.idraza=c.raza and a.idandares=c.andar and co.idcolores=c.color_pelaje and e.idestado_caballo=c.estado and aso.idasociaciones=c.asociacion and s.idservicios=c.servicios;"
                Dim resultadoSQ As NpgsqlDataReader = conexion.consultar(consultaSQLcaballo, conexion.conexionBD)
                If resultadoSQ.Read Then
                    conexion.Conectar()
                    Try
                        If Not BuscarDatos(dt, consultaSQLcaballo) Then Throw New Exception("Sucedio un error al consultar los datos.") 'llama a la funcion BuscarDatos.
                        If Not infoGeneral.RellenarListview(dt) Then Throw New Exception("Sucedio un error al rellenar el listado.")
                        Me.Close()
                    Catch ex As Exception
                        MsgBox(ex.Message, , "Error")
                    End Try
                    conexion.desconectar()
                Else
                    MsgBox("No se ha encontrado Caballos")
                    Cerrar = "no"
                End If
                conexion.desconectar()
            End If 'FIN DE ASOSIACIONES
        Else
            'SI SI HAY ANDARES, BUSCA EL CABALLO
            idandar = resultadoSQLandar.GetInt32(0)
            MsgBox(idandar)
            conexion.Conectar()
            Dim consultaSQLcaballo As String = "select idcaballos, numero_registro,r.descripcion as raza,nombre, sexo,a.descripcion as andar,co.descripcion as color,fecha_nacimiento,fecha_registro,e.descripcion as estado,propietario,aso.descripcion as asociacion,s.descripcion as servicio from caballos c, raza r , andares a,colores co, estado_caballos e, asociaciones aso,servicios s where andar='" + idandar + "' and r.idraza=c.raza and a.idandares=c.andar and co.idcolores=c.color_pelaje and e.idestado_caballo=c.estado and aso.idasociaciones=c.asociacion and s.idservicios=c.servicios;"
            Dim resultadoSQ As NpgsqlDataReader = conexion.consultar(consultaSQLcaballo, conexion.conexionBD)
            If resultadoSQ.Read Then
                conexion.Conectar()
                'se llena el listview
                Try
                    If Not BuscarDatos(dt, consultaSQLcaballo) Then Throw New Exception("Sucedio un error al consultar los datos.") 'llama a la funcion BuscarDatos.
                    If Not infoGeneral.RellenarListview(dt) Then Throw New Exception("Sucedio un error al rellenar el listado.") 'Llama a la funcion RellenarListview
                    Me.Close()
                Catch ex As Exception
                    MsgBox(ex.Message, , "Error")
                End Try
                conexion.desconectar()
            Else
                MsgBox("No se ha encontrado Caballos")
                Cerrar = "no"
            End If
            conexion.desconectar()
        End If 'FIN ANDARES
        conexion.desconectar()
        

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Text = ""
    End Sub
    'FUncion que busca datos para para llenar el DataTable 
    Function BuscarDatos(ByRef dt As DataTable, ByVal consultaSQL As String) As Boolean
        Dim consulSQL = consultaSQL
        Dim bolResultado As Boolean = True
        Try
            If dt Is Nothing Then dt = New DataTable

            Dim cmd As New NpgsqlCommand(consulSQL, conexion.conexionBD)

            Using DA As New NpgsqlDataAdapter(cmd)
                DA.Fill(dt)
            End Using

        Catch ex As Exception
            bolResultado = False
        End Try
        Return bolResultado
    End Function
    'Funcion que llena el listview con el data table previamente llenado en la funcion anterior.
   

End Class