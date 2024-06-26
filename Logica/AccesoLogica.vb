﻿
Imports System.Data
Imports System.Data.SqlClient
Imports Datos.AccesoDatos

Public Class AccesoLogica

    Public Shared L_Usuario As String = "DEFAULT"
    Public Shared L_IdUsuario As Integer = 0

#Region "CONFIGURACION DEL SISTEMA"

    Public Shared Function L_fnConfSistemaGeneral() As DataTable
        Dim _Tabla As DataTable
        Dim _Where, _campos As String
        Dim _dtConfSist As DataTable = D_Datos_Tabla("cnumi", "TC000", "1=1")

        _Where = "ccctc0=" + _dtConfSist.Rows(0).Item("cnumi").ToString
        _campos = "*"
        _Tabla = D_Datos_Tabla(_campos, "TC0001", _Where)
        Return _Tabla
    End Function
    Public Shared Function L_fnConfSistemaModificar(ByRef _numi As String) As Boolean
        Dim _Error As Boolean
        Dim Sql, _where As String

        Sql = "cnumi =" + _numi

        _where = "1=1"
        _Error = D_Modificar_Datos("TC000", Sql, _where)
        Return Not _Error
    End Function

#End Region

#Region "METODOS PRIVADOS"
    Public Shared Sub L_prAbrirConexion(Optional Ip As String = "", Optional UsuarioSql As String = "", Optional ClaveSql As String = "", Optional NombreBD As String = "")
        D_abrirConexion(Ip, UsuarioSql, ClaveSql, NombreBD)
    End Sub
    Public Shared Sub L_prAbrirConexionBitacora(Optional Ip As String = "", Optional UsuarioSql As String = "", Optional ClaveSql As String = "", Optional NombreBD As String = "")
        D_abrirConexionHistorial(Ip, UsuarioSql, ClaveSql, NombreBD)
    End Sub

    Public Shared Function _fnsAuditoria() As String
        Return "'" + Date.Now.Date.ToString("yyyy/MM/dd") + "', '" + Now.Hour.ToString("00") + ":" + Now.Minute.ToString("00") + "' ,'" + L_Usuario + "'"
    End Function
#End Region

#Region "LIBRERIAS"


    Public Shared Function L_prLibreriaDetalleGeneral(_cod1 As String, _cod2 As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@cncod1", _cod1))
        _listParam.Add(New Datos.DParametro("@cncod2", _cod2))
        _listParam.Add(New Datos.DParametro("@cnuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_TC0051", _listParam)

        Return _Tabla
    End Function


#End Region

#Region "VALIDAR ELIMINACION"
    Public Shared Function L_fnbValidarEliminacion(_numi As String, _tablaOri As String, _campoOri As String, ByRef _respuesta As String) As Boolean
        Dim _Tabla As DataTable
        Dim _Where, _campos As String
        _Where = "bbtori='" + _tablaOri + "' and bbtran=1"
        _campos = "bbnumi,bbtran,bbtori,bbcori,bbtdes,bbcdes,bbprog"
        _Tabla = D_Datos_Tabla(_campos, "TB002", _Where)
        _respuesta = "no se puede eliminar el registro: ".ToUpper + _numi + " por que esta siendo usado en los siguientes programas: ".ToUpper + vbCrLf

        Dim result As Boolean = True
        For Each fila As DataRow In _Tabla.Rows
            If L_fnbExisteRegEnTabla(_numi, fila.Item("bbtdes").ToString, fila.Item("bbcdes").ToString) = True Then
                _respuesta = _respuesta + fila.Item("bbprog").ToString + vbCrLf
                result = False
            End If
        Next
        Return result
    End Function

    Private Shared Function L_fnbExisteRegEnTabla(_numiOri As String, _tablaDest As String, _campoDest As String) As Boolean
        Dim _Tabla As DataTable
        Dim _Where, _campos As String
        _Where = _campoDest + "=" + _numiOri
        _campos = _campoDest
        _Tabla = D_Datos_Tabla(_campos, _tablaDest, _Where)
        If _Tabla.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "METODOS PARA EL CONTROL DE USUARIOS Y PRIVILEGIOS"

#Region "Formularios"
    Public Shared Function L_Formulario_General(_Modo As Integer, Optional _Cadena As String = "") As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        If _Modo = 0 Then
            _Where = "ZY001.yanumi=ZY001.yanumi and ZY001.yamod=TY0031.yccod3 and TY0031.yccod1=4 AND TY0031.yccod2=1 "
        Else
            _Where = "ZY001.yanumi=ZY001.yanumi and ZY001.yamod=TY0031.yccod3 and TY0031.yccod1=4 AND TY0031.yccod2=1 " + _Cadena
        End If
        Dim con As String = "ZY001.yanumi,ZY001.yaprog,ZY001.yatit,ZY001.yamod,TY0031.ycdes3  " + "ZY001,TY0031  " + _Where
        _Tabla = D_Datos_Tabla("ZY001.yanumi,ZY001.yaprog,ZY001.yatit,ZY001.yamod,TY0031.ycdes3", "ZY001,TY0031", _Where + " order by yanumi")
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function

    Public Shared Sub L_Formulario_Grabar(ByRef _numi As String, _desc As String, _direc As String, _categ As String)
        Dim _Err As Boolean
        Dim _Tabla As DataTable
        _Tabla = D_Maximo("ZY001", "yanumi", "yanumi=yanumi")
        If Not IsDBNull(_Tabla.Rows(0).Item(0)) Then
            _numi = _Tabla.Rows(0).Item(0) + 1
        Else
            _numi = "1"
        End If

        Dim Sql As String
        Sql = _numi + ",'" + _desc + "','" + _direc + "'," + _categ
        _Err = D_Insertar_Datos("ZY001", Sql)
    End Sub

    Public Shared Sub L_Formulario_Modificar(_numi As String, _desc As String, _direc As String, _categ As String)
        Dim _Err As Boolean
        Dim Sql, _where As String

        Sql = "yaprog = '" + _desc + "' , " +
        "yatit = '" + _direc + "' , " +
        "yamod = " + _categ

        _where = "yanumi = " + _numi
        _Err = D_Modificar_Datos("ZY001", Sql, _where)
    End Sub

    Public Shared Sub L_Formulario_Borrar(_Id As String)
        Dim _Where As String
        Dim _Err As Boolean
        _Where = "yanumi = " + _Id
        _Err = D_Eliminar_Datos("ZY001", _Where)
    End Sub
#End Region

#Region "Roles"
    Public Shared Function L_Rol_General(_Modo As Integer, Optional _Cadena As String = "") As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        If _Modo = 0 Then
            _Where = "ZY002.ybnumi=ZY002.ybnumi "
        Else
            _Where = "ZY002.ybnumi=ZY002.ybnumi " + _Cadena
        End If
        _Tabla = D_Datos_Tabla("ZY002.ybnumi,ZY002.ybrol", "ZY002", _Where + " order by ybnumi")
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function
    Public Shared Function L_RolDetalle_General(_Modo As Integer, Optional _idCabecera As String = "", Optional _idModulo As String = "") As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String
        If _Modo = 0 Then
            _Where = " ycnumi = ycnumi"
        Else
            _Where = " ycnumi=" + _idCabecera + " and ZY001.yamod=" + _idModulo + " and ZY0021.ycyanumi=ZY001.yanumi"
        End If
        _Tabla = D_Datos_Tabla("ZY0021.ycnumi,ZY0021.ycyanumi,ZY0021.ycshow,ZY0021.ycadd,ZY0021.ycmod,ZY0021.ycdel,ZY001.yaprog,ZY001.yatit", "ZY0021,ZY001", _Where)
        Return _Tabla
    End Function

    Public Shared Function L_RolDetalle_General2(_Modo As Integer, Optional _idCabecera As String = "", Optional _where1 As String = "") As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String
        If _Modo = 0 Then
            _Where = " ycnumi = ycnumi"
        Else
            _Where = " ycnumi=" + _idCabecera + " and " + _where1
        End If
        _Tabla = D_Datos_Tabla("ycnumi,ycyanumi,ycshow,ycadd,ycmod,ycdel", "ZY0021", _Where)
        Return _Tabla
    End Function

    Public Shared Function L_prRolDetalleGeneral(_numiRol As String, _idNombreButton As String) As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String

        _Where = "ZY0021.ycnumi=" + _numiRol + " and ZY0021.ycyanumi=ZY001.yanumi and ZY001.yaprog='" + _idNombreButton + "'"

        _Tabla = D_Datos_Tabla("ycnumi,ycyanumi,ycshow,ycadd,ycmod,ycdel", "ZY0021,ZY001", _Where)
        Return _Tabla
    End Function

    Public Shared Sub L_Rol_Grabar(ByRef _numi As String, _rol As String)
        Dim _Actualizacion As String
        Dim _Err As Boolean
        Dim _Tabla As DataTable
        _Tabla = D_Maximo("ZY002", "ybnumi", "ybnumi=ybnumi")
        If Not IsDBNull(_Tabla.Rows(0).Item(0)) Then
            _numi = _Tabla.Rows(0).Item(0) + 1
        Else
            _numi = "1"
        End If

        _Actualizacion = "'" + Date.Now.Date.ToString("yyyy/MM/dd") + "', '" + Now.Hour.ToString + ":" + Now.Minute.ToString + "' ,'" + L_Usuario + "'"

        Dim Sql As String
        Sql = _numi + ",'" + _rol + "'," + _Actualizacion
        _Err = D_Insertar_Datos("ZY002", Sql)
    End Sub
    Public Shared Sub L_RolDetalle_Grabar(_idCabecera As String, _numi1 As Integer, _show As Boolean, _add As Boolean, _mod As Boolean, _del As Boolean)
        Dim _Err As Boolean
        Dim Sql As String
        Sql = _idCabecera & "," & _numi1 & ",'" & _show & "','" & _add & "','" & _mod & "','" & _del & "'"
        _Err = D_Insertar_Datos("ZY0021", Sql)
    End Sub
    Public Shared Sub L_Rol_Modificar(_numi As String, _desc As String)
        Dim _Err As Boolean
        Dim Sql, _where As String

        Sql = "ybrol = '" + _desc + "' "

        _where = "ybnumi = " + _numi
        _Err = D_Modificar_Datos("ZY002", Sql, _where)
    End Sub
    Public Shared Sub L_Rol_Borrar(_Id As String)
        Dim _Where As String
        Dim _Err As Boolean
        _Where = "ybnumi = " + _Id
        _Err = D_Eliminar_Datos("ZY002", _Where)
    End Sub
    Public Shared Sub L_RolDetalle_Modificar(_idCabecera As String, _numi1 As Integer, _show As Boolean, _add As Boolean, _mod As Boolean, _del As Boolean)
        Dim _Err As Boolean
        Dim Sql, _where As String

        Sql = "ycshow = '" & _show & "' , " & "ycadd = '" & _add & "' , " & "ycmod = '" & _mod & "' , " & "ycdel = '" & _del & "' "

        _where = "ycnumi = " & _idCabecera & " and ycyanumi = " & _numi1
        _Err = D_Modificar_Datos("ZY0021", Sql, _where)
    End Sub
    Public Shared Sub L_RolDetalle_Borrar(_Id As String, _Id1 As String)
        Dim _Where As String
        Dim _Err As Boolean

        _Where = "ycnumi = " + _Id + " and ycyanumi = " + _Id1
        _Err = D_Eliminar_Datos("ZY0021", _Where)
    End Sub
    Public Shared Sub L_RolDetalle_Borrar(_Id As String)
        Dim _Where As String
        Dim _Err As Boolean

        _Where = "ycnumi = " + _Id
        _Err = D_Eliminar_Datos("ZY0021", _Where)
    End Sub
#End Region

#Region "Usuarios"
    Public Shared Function L_Usuario_General(_Modo As Integer, Optional _Cadena As String = "") As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        If _Modo = 0 Then
            _Where = "ZY003.ydnumi=ZY003.ydnumi and ZY002.ybnumi=ZY003.ydrol "
        Else
            _Where = "ZY003.ydnumi=ZY003.ydnumi and ZY002.ybnumi=ZY003.ydrol " + _Cadena
        End If
        _Tabla = D_Datos_Tabla("ZY003.ydnumi,ZY003.yduser,ZY003.ydpass,ZY003.ydest,ZY003.ydcant,ZY002.ybnumi,ZY002.ybrol", "ZY003,ZY002", _Where + " order by ydnumi")
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function


    Public Shared Function L_Usuario_General2(_Modo As Integer, Optional _Cadena As String = "") As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        If _Modo = 0 Then
            _Where = "ZY003.ydnumi=ZY003.ydnumi and ZY002.ybnumi=ZY003.ydrol and TA001.aanumi=ZY003.ydsuc "
        Else
            _Where = "ZY003.ydnumi=ZY003.ydnumi and ZY002.ybnumi=ZY003.ydrol and TA001.aanumi=ZY003.ydsuc " + _Cadena
        End If

        _Tabla = D_Datos_Tabla("ZY003.ydnumi,ZY003.yduser,ZY003.ydpass,ZY003.ydest,ZY003.ydcant,ZY003.ydfontsize,ZY002.ybnumi,ZY002.ybrol,ZY003.ydsuc,ZY003.ydall,TA001.aabdes,ZY003.ydfact,ZY003.ybhact,ZY003.ybuact,ZY003.yd_numiVend", "ZY003,ZY002,TA001", _Where + " order by ydnumi")
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function

    Public Shared Function L_Usuario_General3(_Modo As Integer, Optional _Cadena As String = "") As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        If _Modo = 0 Then
            _Where = "1=1"
        Else
            _Where = _Cadena
        End If
        _Tabla = D_Datos_Tabla("ZY003.ydnumi,ZY003.yduser", "ZY003", _Where + " order by yduser")
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function
    Public Shared Sub L_Usuario_Grabar(ByRef _numi As String, _user As String, _pass As String, _rol As String, _estado As String, _cantDias As String, _tamFuente As String, _suc As String, _allSuc As String, _numiVend As String)
        Dim _Actualizacion As String
        Dim _Err As Boolean
        Dim _Tabla As DataTable
        _Tabla = D_Maximo("ZY003", "ydnumi", "ydnumi=ydnumi")
        If Not IsDBNull(_Tabla.Rows(0).Item(0)) Then
            _numi = _Tabla.Rows(0).Item(0) + 1
        Else
            _numi = "1"
        End If

        _Actualizacion = "'" + Date.Now.Date.ToString("yyyy/MM/dd") + "', '" + Now.Hour.ToString + ":" + Now.Minute.ToString + "' ,'" + L_Usuario + "'"

        Dim Sql As String
        Sql = _numi + ",'" + _user + "'," + _rol + ",'" + _pass + "','" + _estado + "'," + _cantDias + "," + _tamFuente + "," + _suc + "," + _allSuc + "," + _Actualizacion + "," + _numiVend
        _Err = D_Insertar_Datos("ZY003", Sql)
    End Sub
    Public Shared Sub L_Usuario_Modificar(_numi As String, _user As String, _pass As String, _rol As String, _estado As String, _cantDias As String, _tamFuente As String, _suc As String, _allSuc As String, _numiVend As String)
        Dim _Err As Boolean
        Dim Sql, _where As String

        Sql = "yduser = '" + _user + "' , " +
        "ydpass = '" + _pass + "' , " +
        "ydrol = " + _rol + " , " +
        "ydest = '" + _estado + "' , " +
        "ydcant = " + _cantDias + " , " +
        "ydfontsize = " + _tamFuente + " , " +
        "ydsuc = " + _suc + " , " +
        "ydall = " + _allSuc + " , " +
        "yd_numiVend = " + _numiVend

        _where = "ydnumi = " + _numi
        _Err = D_Modificar_Datos("ZY003", Sql, _where)
    End Sub
    Public Shared Sub L_Usuario_Borrar(_Id As String)
        Dim _Where As String
        Dim _Err As Boolean
        _Where = "ydnumi = " + _Id
        _Err = D_Eliminar_Datos("ZY003", _Where)
    End Sub

    Public Shared Function L_Validar_Usuario2(_Nom As String, _Pass As String) As Boolean
        Dim _Tabla As DataTable
        _Tabla = D_Datos_Tabla("*", "ZY003", "yduser = '" + _Nom + "' AND ydpass = '" + _Pass + "'")
        If _Tabla.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function L_Validar_Usuario(_Nom As String, _Pass As String) As DataTable
        Dim _Tabla As DataTable
        _Tabla = D_Datos_Tabla("ydnumi,yduser,ydrol,ydpass,ydest,ydcant,ydfontsize,ydsuc,aabdes,ydall ", "ZY003" + " INNER JOIN	dbo.TA001
ON	dbo.ZY003.ydsuc=dbo.TA001.aanumi", "yduser = '" + _Nom + "' AND ydpass = '" + _Pass + "'")
        Return _Tabla
    End Function

    Public Shared Function CargarConfiguracion(_NomTabla As String, _TipoVenta As String) As DataTable
        Dim _Tabla As DataTable
        _Tabla = D_Datos_Tabla("* ", _NomTabla, "tipo= " + _TipoVenta + " order by orden")
        Return _Tabla
    End Function
    Public Shared Function L_fnGrabarTO001prestamos(_tipo As Integer, ByRef _tanumi As Integer, Optional _obnumito1 As String = "", Optional _oblin As String = "",
                                           Optional _obcuenta As String = "", Optional _obobs As String = "", Optional _obdebebs As Double = 0.00,
                                           Optional _obhaberbs As Double = 0.00, Optional _obdebeus As Double = 0.00, Optional _obhaberus As Double = 0.00) As Integer
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '    @tanumi ,@taalm,@tafdoc ,@taven  ,@tatven,
        '@tafvcr ,@taclpr,@tamon ,@taest  ,@taobs ,@tadesc ,@newFecha,@newHora,@tauact,@taproforma
        _listParam.Add(New Datos.DParametro("@tipo", _tipo))
        _listParam.Add(New Datos.DParametro("@tanumi", _tanumi))
        _listParam.Add(New Datos.DParametro("@obnumito1", _obnumito1))
        _listParam.Add(New Datos.DParametro("@oblin", _oblin))
        _listParam.Add(New Datos.DParametro("@obcuenta", _obcuenta))
        _listParam.Add(New Datos.DParametro("@obobs", _obobs))
        _listParam.Add(New Datos.DParametro("@obdebebs", _obdebebs))
        _listParam.Add(New Datos.DParametro("@obhaberbs", _obhaberbs))
        _listParam.Add(New Datos.DParametro("@obdebeus", _obdebeus))
        _listParam.Add(New Datos.DParametro("@obhaberus", _obhaberus))
        _Tabla = D_ProcedimientoConParam("sp_Mam_HeaAsiCont", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _tanumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _tanumi
    End Function
    Public Shared Function L_fnGrabarTO001Ingresos(_tipo As Integer, fecha As String, orden As String, cheque As String, banco As String, glosa As String,
                                                   ByRef _tanumi As Integer, Optional _obnumito1 As String = "", Optional _oblin As String = "",
                                           Optional _obcuenta As String = "", Optional _obobs As String = "", Optional _obdebebs As Double = 0.00,
                                           Optional _obhaberbs As Double = 0.00, Optional _obdebeus As Double = 0.00, Optional _obhaberus As Double = 0.00) As Integer
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '    @tanumi ,@taalm,@tafdoc ,@taven  ,@tatven,
        '@tafvcr ,@taclpr,@tamon ,@taest  ,@taobs ,@tadesc ,@newFecha,@newHora,@tauact,@taproforma
        _listParam.Add(New Datos.DParametro("@tipo", _tipo))
        _listParam.Add(New Datos.DParametro("@tanumi", _tanumi))
        _listParam.Add(New Datos.DParametro("@obnumito1", _obnumito1))
        _listParam.Add(New Datos.DParametro("@oblin", _oblin))
        _listParam.Add(New Datos.DParametro("@obcuenta", _obcuenta))
        _listParam.Add(New Datos.DParametro("@obobs", glosa))
        _listParam.Add(New Datos.DParametro("@obdebebs", _obdebebs))
        _listParam.Add(New Datos.DParametro("@obhaberbs", _obhaberbs))
        _listParam.Add(New Datos.DParametro("@obdebeus", _obdebeus))
        _listParam.Add(New Datos.DParametro("@obhaberus", _obhaberus))
        _listParam.Add(New Datos.DParametro("@tafdoc", fecha))
        _listParam.Add(New Datos.DParametro("@nombre", orden))
        _listParam.Add(New Datos.DParametro("@cheque", cheque))
        _listParam.Add(New Datos.DParametro("@banco", banco))
        _listParam.Add(New Datos.DParametro("@glosa", glosa))
        _Tabla = D_ProcedimientoConParam("sp_Mam_HeaAsiCont", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _tanumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _tanumi
    End Function
    Public Shared Function L_fnGrabarTO001IngresosDetalle(_tipo As Integer, fecha As String, orden As String, cheque As String, banco As String, glosa As String, ByRef _tanumi As Integer, Optional _obnumito1 As String = "", Optional _oblin As String = "",
                                           Optional _obcuenta As String = "", Optional _obobs As String = "", Optional _obdebebs As Double = 0.00,
                                           Optional _obhaberbs As Double = 0.00, Optional _obdebeus As Double = 0.00, Optional _obhaberus As Double = 0.00) As Integer
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '    @tanumi ,@taalm,@tafdoc ,@taven  ,@tatven,
        '@tafvcr ,@taclpr,@tamon ,@taest  ,@taobs ,@tadesc ,@newFecha,@newHora,@tauact,@taproforma
        _listParam.Add(New Datos.DParametro("@tipo", _tipo))
        _listParam.Add(New Datos.DParametro("@tanumi", _tanumi))
        _listParam.Add(New Datos.DParametro("@obnumito1", _obnumito1))
        _listParam.Add(New Datos.DParametro("@oblin", _oblin))
        _listParam.Add(New Datos.DParametro("@obcuenta", _obcuenta))
        _listParam.Add(New Datos.DParametro("@obobs", _obobs))
        _listParam.Add(New Datos.DParametro("@obdebebs", _obdebebs))
        _listParam.Add(New Datos.DParametro("@obhaberbs", _obhaberbs))
        _listParam.Add(New Datos.DParametro("@obdebeus", _obdebeus))
        _listParam.Add(New Datos.DParametro("@obhaberus", _obhaberus))
        _listParam.Add(New Datos.DParametro("@tafdoc", fecha))
        _listParam.Add(New Datos.DParametro("@nombre", orden))
        _listParam.Add(New Datos.DParametro("@cheque", cheque))
        _listParam.Add(New Datos.DParametro("@banco", banco))
        _listParam.Add(New Datos.DParametro("@glosa", glosa))
        _Tabla = D_ProcedimientoConParam("sp_Mam_HeaAsiCont", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _tanumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _tanumi
    End Function
    Public Shared Function PrecioPonderado(_NomTabla As Integer, _TipoVenta As Integer, _cantidad As Decimal) As Decimal
        Dim resultado As Decimal = 0.00000
        Dim _Tabla As DataTable
        _Tabla = D_Datos_TablaPrecioPonderado("Select dbo.obtenerSaldoAnterio(" + _NomTabla.ToString + "," + _TipoVenta.ToString + "," + _cantidad.ToString + ")")
        If _Tabla.Rows.Count > 0 Then
            resultado = _Tabla.Rows(0).Item(0)

        Else

        End If

        Return Format(resultado, "0.00000")
    End Function
    Public Shared Function obtenerCompras(_NomTabla As Integer, _TipoVenta As Integer) As Decimal
        Dim resultado As Decimal = 0.00000
        Dim _Tabla As DataTable
        _Tabla = D_Datos_TablaPrecioPonderado("Select dbo.obtenerCompras(" + _NomTabla.ToString + "," + _TipoVenta.ToString + ")")
        If _Tabla.Rows.Count > 0 Then
            resultado = _Tabla.Rows(0).Item(0)

        Else

        End If

        Return Format(resultado, "0.00000")
    End Function
    Public Shared Function obtenerUnidadesRestantes(_NomTabla As Integer, _TipoVenta As Integer) As Decimal
        Dim resultado As Decimal = 0.00000
        Dim _Tabla As DataTable
        _Tabla = D_Datos_TablaPrecioPonderado("Select dbo.obtenerUnidadesRestantes(" + _NomTabla.ToString + "," + _TipoVenta.ToString + ")")
        If _Tabla.Rows.Count > 0 Then
            resultado = _Tabla.Rows(0).Item(0)

        Else

        End If

        Return Format(resultado, "0.00000")
    End Function
    Public Shared Function ObtenerNumCuenta(_NomTabla As String, _TipoVenta As String) As DataTable
        Dim _Tabla As DataTable
        _Tabla = D_Datos_TablaInst("*", "Institucion", "id= " + _TipoVenta)
        Return _Tabla
    End Function

    Public Shared Function verificarGrupoEconomico(_NomTabla As String, _TipoVenta As String) As DataTable
        Dim _Tabla As DataTable
        _Tabla = D_Datos_TablaInst(" id,codGrupo,dbo.TY004.ydcod,dbo.TY004.ydrazonsocial", _NomTabla + " inner JOIN dbo.TY004 ON  TG001.ydnumi=dbo.TY004.ydnumi AND dbo.TY004.ydtip=1 ", "dbo.TY004.ydcod= " + _TipoVenta)
        Return _Tabla
    End Function

    Public Shared Function verificarGrupoEconomicoDet(_NomTabla As String, _TipoVenta As String) As DataTable
        Dim _Tabla As DataTable
        _Tabla = D_Datos_TablaInst(" id1,dbo.TY004.ydcod,dbo.TY004.ydrazonsocial ", _NomTabla + " inner JOIN dbo.TY004 ON  TG0011.ydnumi=dbo.TY004.ydnumi AND dbo.TY004.ydtip=1 ", "dbo.TY004.ydcod= " + _TipoVenta)
        Return _Tabla
    End Function
    Public Shared Function ObtenerNumCuentaSurtidor(_NomTabla As String, _TipoVenta As String) As DataTable
        Dim _Tabla As DataTable
        _Tabla = D_Datos_TablaInst("*", "TY0031", "yccod1=1 and yccod2=8 " + "and yccod3= " + _TipoVenta)
        Return _Tabla
    End Function
    Public Shared Function ObtenerNumCuentaProveedor(_NomTabla As String, _TipoVenta As String) As DataTable
        Dim _Tabla As DataTable
        _Tabla = D_Datos_TablaInst("*", "TY004", "ydnumi= " + _TipoVenta + "and ydtip=3")
        Return _Tabla
    End Function
    Public Shared Function CargarProductoDiesel(_NomTabla As String, _TipoVenta As String) As DataTable
        Dim _Tabla As DataTable
        _Tabla = D_Datos_Tabla("* ", _NomTabla, "yfcprod= " + _TipoVenta)
        Return _Tabla
    End Function

    Public Shared Function L_VerConfiguracion() As DataTable
        Dim _Tabla As DataTable
        _Tabla = D_Datos_Tabla("VerServicios", "SY000", "1=1")
        Return _Tabla
    End Function
    Public Shared Function TipoDescuentoEsXCantidad() As Boolean
        Dim _Tabla As DataTable
        _Tabla = D_Datos_Tabla("Count(*)", "SY000", "VerTipoDescuento = 0")
        If _Tabla.Rows.Count > 0 Then
            If _Tabla.Rows(0).Item(0) = 1 Then
                Return True
            Else
                Return False
            End If
        End If
    End Function
    Public Shared Function L_ListarUsuarios() As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String
        _Where = "1=1 Order by ydnumi"

        _Tabla = D_Datos_Tabla("ydnumi,yduser,ydrol,yd_numiVend", "ZY003", _Where)
        Return _Tabla
    End Function
    Public Shared Function L_BuscarPoUsuario(_Nom As String) As DataTable
        Dim _Tabla As DataTable
        _Tabla = D_Datos_Tabla("ydnumi,yduser,ydrol, yd_numiVend", "ZY003", "yduser = '" + _Nom + "'")
        Return _Tabla
    End Function
#End Region

#End Region
#Region "TARAS"
    Public Shared Function L_Taras(_Modo As Integer, Optional _Cadena As String = "") As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet

        _Tabla = D_Datos_TablaTara("cod,placa,pesoTara,color,propietario", "taras")
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function
#End Region
    Public Shared Function L_factorR(_Modo As Integer, Optional _Cadena As String = "") As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet

        _Tabla = D_Datos_TablaTara("id,fecha,ingreso,pcfab,pci,obtenido,ponderado,aplicado,ingAcumulado,relRacum,kgRelrdia,calculoPonderado,tafact,tahact,tauact", "factorRponderado")
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function

    Public Shared Function L_diasZafra(_Modo As Integer, Optional _Cadena As String = "") As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet

        _Tabla = D_Datos_TablaTara("id,convert(date,fechaInicio) as fechaInicio,fechaFinal,gestion", "diasZafra")
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function

    Public Shared Function traerGestionTraspasoCupo() As DataTable
        Dim _Tabla As DataTable

        _Tabla = D_Datos_Tabla(" MAX(gestion)  as ycdes3", "TotalxCanero", "estado=1")

        Return _Tabla
    End Function
    Public Shared Function L_TraspasoCupos() As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet

        _Tabla = D_Datos_Tabla1("idTraspCupo,codCaneroT codCaneroSys,canero.ydcod codCaneroUCG,caneroR.ydrazonsocial caneroUCG,fechaReg,porcentaje,cupoLibreTransferirT,CANERO.ydrazonsocial,dbo.Institucion.codInst,dbo.Institucion.nomInst,cupoRegAntT,cupoTransferirT,cupoRegNuevT,codCaneroR codCaneroSysR, caneroR.ydcod codCaneroUCGR,institucionR.codInst codInstR,institucionR.nomInst nomInstR,cupoRegAntR,cupoRegNuevR,gestion,dbo.traspasoCupos.fact,dbo.traspasoCupos.hact,dbo.traspasoCupos.uact,estado", "dbo.traspasoCupos INNER JOIN ty004 canero ON canero.ydnumi=dbo.traspasoCupos.codCaneroT AND canero.ydtip=1 INNER JOIN ty004 caneroR ON caneroR.ydnumi=dbo.traspasoCupos.codCaneroR AND caneroR.ydtip=1 INNER JOIN dbo.Institucion ON dbo.Institucion.id=canero.ydnumivend INNER JOIN dbo.Institucion institucionR ON institucionR.id=caneroR.ydnumivend")
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function
#Region "INSTITUCION"
    Public Shared Function L_Institucion(_Modo As Integer, Optional _Cadena As String = "") As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet

        _Tabla = D_Datos_Tabla1("Institucion.id,Institucion.codInst,Institucion.nomInst,Institucion.direc,Institucion.telf,cuenta.cactaucg,cuenta.canumi", "Institucion")
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function
    Public Shared Function L_pruebaFactor(_Modo As Integer, Optional _Cadena As String = "") As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet

        _Tabla = D_Datos_TablaTara("*", "datosparapruebaponderado")
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function
    Public Shared Function L_pruebaFactor_Grabar(_CodInst As Date, _nomInst As Integer, _telf As Decimal, _direc As Decimal) As Boolean
        Dim _Error As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)


        _listParam.Add(New Datos.DParametro("@fechaRegistroBoleta", _CodInst))
        _listParam.Add(New Datos.DParametro("@ingreso", _nomInst))
        _listParam.Add(New Datos.DParametro("@pcfab", _direc))
        _listParam.Add(New Datos.DParametro("@pciGeneral", _telf))


        _Tabla = D_ProcedimientoConParam("factorPonderado", _listParam)

        If _Tabla.Rows.Count > 0 Then
            '_numi = _Tabla.Rows(0).Item(0)
            _Error = False
        Else
            _Error = True
        End If
        Return Not _Error
    End Function


    Public Shared Function L_BuscarCodInst(_Numi As String) As Boolean
        Dim _Tabla As DataTable
        Dim _Err As Boolean
        Dim _Where As String = "codInst = " + _Numi
        _Tabla = D_Datos_Tabla("*", "Institucion", _Where)
        If (_Tabla.Rows.Count > 0) Then
            _Err = True
        Else
            _Err = False
        End If
        Return _Err
    End Function

    Public Shared Function L_BuscarCodBOleta(_Numi As String) As Boolean
        Dim _Tabla As DataTable
        Dim _Err As Boolean
        Dim _Where As String = "nroBoleta = " + _Numi
        _Tabla = D_Datos_Tabla("*", "heaBol", _Where)
        If (_Tabla.Rows.Count > 0) Then
            _Err = True
        Else
            _Err = False
        End If
        Return _Err
    End Function
    Public Shared Function L_BuscarCodTara(_Numi As String) As Boolean
        Dim _Tabla As DataTable
        Dim _Err As Boolean
        Dim _Where As String = "cod = " + _Numi
        _Tabla = D_Datos_Tabla("*", "taras", _Where)
        If (_Tabla.Rows.Count > 0) Then
            _Err = True
        Else
            _Err = False
        End If
        Return _Err
    End Function
    Public Shared Function L_BuscarCodCanero(_Numi As String) As Boolean
        Dim _Tabla As DataTable
        Dim _Err As Boolean
        Dim _Where As String = "ydcod = " + _Numi + " and ydtip=1"
        _Tabla = D_Datos_Tabla("*", "TY004", _Where)
        If (_Tabla.Rows.Count > 0) Then
            _Err = True
        Else
            _Err = False
        End If
        Return _Err
    End Function



    Public Shared Function L_Institucion_Grabar(_CodInst As String, _nomInst As String, _telf As String, _direc As String) As Boolean
        Dim _Error As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 551))
        _listParam.Add(New Datos.DParametro("@yfcprod", _CodInst))
        _listParam.Add(New Datos.DParametro("@yfdetpro", _nomInst))
        _listParam.Add(New Datos.DParametro("@yfimg", _direc))
        _listParam.Add(New Datos.DParametro("@yfcbarra", _telf))

        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        If _Tabla.Rows.Count > 0 Then
            '_numi = _Tabla.Rows(0).Item(0)
            _Error = False
        Else
            _Error = True
        End If
        Return Not _Error
    End Function
    Public Shared Function L_Taras_Grabar(_Cod As String, _placa As String, _pesoTara As Decimal, _color As String, _propietario As String) As Boolean
        Dim _Error As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 16))
        _listParam.Add(New Datos.DParametro("@codTara", _Cod))
        _listParam.Add(New Datos.DParametro("@placa", _placa))
        _listParam.Add(New Datos.DParametro("@pesoTara", _pesoTara))
        _listParam.Add(New Datos.DParametro("@color", _color))
        _listParam.Add(New Datos.DParametro("@propietario", _propietario))

        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        If _Tabla.Rows.Count > 0 Then
            '_numi = _Tabla.Rows(0).Item(0)
            _Error = False
        Else
            _Error = True
        End If
        Return Not _Error
    End Function

    Public Shared Function TraspasoCupo_Grabar(ByRef _tanumi As String, _codCaneroTransfiriente As Integer, _tbCupoRegAntT As Decimal, _cupotransferirT As Decimal, _cupoRegNuevt As Decimal,
               _codCaneroReceptor As Integer, _tbCupoRegAntR As Decimal, _cupoRegNuevR As Decimal, _gestion As String, _fechaReg As String, _porcentaje As Decimal, _cupoLibreTransferirT As Decimal) As Boolean
        Dim _Error As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 23))
        _listParam.Add(New Datos.DParametro("@codCaneroTransfiriente", _codCaneroTransfiriente))
        _listParam.Add(New Datos.DParametro("@tbCupoRegAntT", _tbCupoRegAntT))
        _listParam.Add(New Datos.DParametro("@cupotransferirT", _cupotransferirT))
        _listParam.Add(New Datos.DParametro("@cupoRegNuevt", _cupoRegNuevt))
        _listParam.Add(New Datos.DParametro("@codCaneroReceptor", _codCaneroReceptor))


        _listParam.Add(New Datos.DParametro("@tbCupoRegAntR", _tbCupoRegAntR))
        _listParam.Add(New Datos.DParametro("@cupoRegNuevR", _cupoRegNuevR))
        _listParam.Add(New Datos.DParametro("@gestion", _gestion))
        _listParam.Add(New Datos.DParametro("@fechaReg", _fechaReg))

        _listParam.Add(New Datos.DParametro("@porcentaje", _porcentaje))
        _listParam.Add(New Datos.DParametro("@cupoLibreTransferirT", _cupoLibreTransferirT))
        _listParam.Add(New Datos.DParametro("@estado", 1))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            '_numi = _Tabla.Rows(0).Item(0)
            _Error = False
        Else
            _Error = True
        End If
        Return Not _Error
    End Function
    Public Shared Function L_FactorR_Grabar(_pesoTara As String, _fecha As String) As Boolean

        Dim _Error As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@pcfab", _pesoTara))



        _listParam.Add(New Datos.DParametro("@fechaRegistroBoleta", _fecha))
        '_listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("factorPonderado", _listParam)

        If _Tabla.Rows.Count > 0 Then
            '_numi = _Tabla.Rows(0).Item(0)
            _Error = False
        Else
            _Error = True
        End If
        Return Not _Error
    End Function

    Public Shared Function L_DiasZafra_Grabar(_fechaInicio As String, _fechaFinal As String) As Boolean
        Dim _Error As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 21))


        _listParam.Add(New Datos.DParametro("@fechaI", _fechaInicio))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaFinal))
        '_listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_Boletas", _listParam)

        If _Tabla.Rows.Count > 0 Then
            '_numi = _Tabla.Rows(0).Item(0)
            _Error = False
        Else
            _Error = True
        End If
        Return Not _Error
    End Function
    Public Shared Function L_Institucion_Modificar(_numi As String, _codInst As String, _nomInst As String, _telf As String, _direc As String, _campo1 As String, _campo2 As String, Optional _campo3 As String = "") As Boolean
        Dim _Err As Boolean
        Dim Sql, _where As String

        Sql = "codInst = '" + _codInst + "' , " +
        "nomInst = '" + _nomInst + "' , " +
        "telf = '" + _telf + "' , " +
        "direc = '" + _direc + "' , " +
        "campo1 = " + _campo1 + " , " +
        "campo2 = " + _campo2 + " , " +
        "campo3 = " + _campo3

        _where = "id = " + _numi
        _Err = D_Modificar_Datos("Institucion", Sql, _where)
        Sql =
        " cadesc = '" + _nomInst + "' "

        _where = "canumi = " + _campo1
        _Err = D_Modificar_Datos("BDDiconCaneros.dbo.TC001", Sql, _where)
        Return _Err
    End Function

    Public Shared Function L_Taras_Modificar(_Cod As String, _placa As String, _pesoTara As String, _color As String, _propietario As String) As Boolean
        Dim _Err As Boolean
        Dim Sql, _where As String

        Sql = "cod = '" + _Cod + "' , " +
        "placa = '" + _placa + "' , " +
        "pesoTara = " + _pesoTara + " , " +
        "color = '" + _color + "' , " +
        "propietario = '" + _propietario + "'  "

        _where = "cod = " + _Cod
        _Err = D_Modificar_Datos("taras", Sql, _where)
        Return _Err
    End Function

    Public Shared Sub L_Institucion_Borrar(_Id As String)
        Dim _Where As String
        Dim _Err As Boolean
        _Where = "id = " + _Id
        _Err = D_Eliminar_Datos("Institucion", _Where)
    End Sub
    Public Shared Sub L_Asiento_Borrar(_Id As String)
        Dim _Where As String
        Dim _Err As Boolean
        _Where = "obnumito1 = " + _Id
        _Err = D_Eliminar_Datos("BDDiconCaneros.dbo.TO0011", _Where)
    End Sub

    Public Shared Sub L_Boletas_Borrar(_Id As String)
        Dim _Where As String
        Dim _Err As Boolean
        _Where = "fechaRelacionBol = " + "'" + _Id + "'"
        _Err = D_Eliminar_Datos("analisis", _Where)
    End Sub
    Public Shared Function L_AnalisisLaica(_Modo As Integer, Optional _Cadena As String = "") As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet

        _Tabla = D_Datos_TablaTara("nroBoleta , fecha ,  fechaRelacionBol ,torta , fibra ,brix , pol ,pureza ,basura ,paquete", "analisis")
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function
    Public Shared Sub L_Fponderado_Borrar(_Id As String)
        Dim _Where As String
        Dim _Err As Boolean
        _Where = "fecha = " + "'" + _Id + "'"
        _Err = D_Eliminar_Datos("factorRponderado", _Where)
    End Sub

    Public Shared Sub L_Taras_Borrar(_Id As String)
        Dim _Where As String
        Dim _Err As Boolean
        _Where = "cod = " + _Id
        _Err = D_Eliminar_Datos("taras", _Where)
    End Sub
#End Region
#Region "TY005 PRODUCTOS"
    Public Shared Function L_prLibreriaGrabar(ByRef _numi As String, _cod1 As String, _cod2 As String, _desc1 As String, _desc2 As String) As Boolean
        Dim _Error As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@ylcod1", _cod1))
        _listParam.Add(New Datos.DParametro("@ylcod2", _cod2))
        _listParam.Add(New Datos.DParametro("@desc", _desc1))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            _Error = False
        Else
            _Error = True
        End If
        Return Not _Error
    End Function

    Public Shared Function L_fnEliminarProducto(numi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TY005", "yfnumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@yfnumi", numi))
            _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function

    Public Shared Function L_fnGrabarProducto(ByRef _yfnumi As String, _yfcprod As String,
                                              _yfcbarra As String, _yfcdprod1 As String,
                                              _yfcdprod2 As String, _yfgr1 As Integer, _yfgr2 As Integer,
                                              _yfgr3 As Integer, _yfgr4 As Integer, _yfMed As Integer, _yfumin As Integer,
                                              _yfusup As Integer, _yfvsup As Double, _yfsmin As Integer, _yfap As Integer,
                                              _yfimg As String, TY0051 As DataTable,
                                              _yfdetpro As String, _yfgr5 As String,
                                              _ycodact As String, _yumed As Integer, _ycodprosin As String, _ypreciosif As Integer
                                              ) As Boolean
        Dim _resultado As Boolean
        '@yfnumi ,@yfcprod ,@yfcbarra ,@yfcdprod1 ,@yfcdprod2 ,
        '			@yfgr1 ,@yfgr2 ,@yfgr3 ,@yfgr4 ,@yfMed ,@yfumin ,@yfusup ,@yfvsup ,
        '			@yfmstk ,@yfclot ,@yfsmin ,@yfap ,@yfimg ,@newFecha,@newHora,@yfuact
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@yfnumi", _yfnumi))
        _listParam.Add(New Datos.DParametro("@yfcprod", _yfcprod))

        _listParam.Add(New Datos.DParametro("@yfcbarra", _yfcbarra))
        _listParam.Add(New Datos.DParametro("@yfcdprod1", _yfcdprod1))
        _listParam.Add(New Datos.DParametro("@yfcdprod2", _yfcdprod2))
        _listParam.Add(New Datos.DParametro("@yfgr1", _yfgr1))
        _listParam.Add(New Datos.DParametro("@yfgr2", _yfgr2))
        _listParam.Add(New Datos.DParametro("@yfgr3", _yfgr3))
        _listParam.Add(New Datos.DParametro("@yfgr4", _yfgr4))
        _listParam.Add(New Datos.DParametro("@yfMed", _yfMed))
        _listParam.Add(New Datos.DParametro("@yfumin", _yfumin))
        _listParam.Add(New Datos.DParametro("@yfusup", _yfusup))
        _listParam.Add(New Datos.DParametro("@yfvsup", _yfvsup))
        _listParam.Add(New Datos.DParametro("@yfmstk", 0))
        _listParam.Add(New Datos.DParametro("@yfclot", _yfcbarra))
        _listParam.Add(New Datos.DParametro("@yfsmin", _yfsmin))
        _listParam.Add(New Datos.DParametro("@yfap", _yfap))
        _listParam.Add(New Datos.DParametro("@yfimg", _yfimg))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@yfdetpro", _yfdetpro))
        _listParam.Add(New Datos.DParametro("@yfgr5", _yfgr5))

        _listParam.Add(New Datos.DParametro("@ycodact", _ycodact))
        _listParam.Add(New Datos.DParametro("@ygcodu", _yumed))
        _listParam.Add(New Datos.DParametro("@ycodprosin", _ycodprosin))
        _listParam.Add(New Datos.DParametro("@ypreciosif", _ypreciosif))

        _listParam.Add(New Datos.DParametro("@TY0051", "", TY0051))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _yfnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnModificarProducto(ByRef _yfnumi As String, _yfcprod As String,
                                                 _yfcbarra As String, _yfcdprod1 As String,
                                                 _yfcdprod2 As String, _yfgr1 As Integer, _yfgr2 As Integer,
                                                 _yfgr3 As Integer, _yfgr4 As Integer, _yfMed As Integer,
                                                 _yfumin As Integer, _yfusup As Integer, _yfvsup As Double,
                                                 _yfsmin As Integer, _yfap As Integer, _yfimg As String,
                                                 TY0051 As DataTable, _yfdetpro As String, _yfgr5 As String,
                                              _ycodact As String, _yumed As Integer, _ycodprosin As String, _ypreciosif As Integer) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@yfnumi", _yfnumi))
        _listParam.Add(New Datos.DParametro("@yfcprod", _yfcprod))

        _listParam.Add(New Datos.DParametro("@yfcbarra", _yfcbarra))
        _listParam.Add(New Datos.DParametro("@yfcdprod1", _yfcdprod1))
        _listParam.Add(New Datos.DParametro("@yfcdprod2", _yfcdprod2))
        _listParam.Add(New Datos.DParametro("@yfgr1", _yfgr1))
        _listParam.Add(New Datos.DParametro("@yfgr2", _yfgr2))
        _listParam.Add(New Datos.DParametro("@yfgr3", _yfgr3))
        _listParam.Add(New Datos.DParametro("@yfgr4", _yfgr4))
        _listParam.Add(New Datos.DParametro("@yfMed", _yfMed))
        _listParam.Add(New Datos.DParametro("@yfumin", _yfumin))
        _listParam.Add(New Datos.DParametro("@yfusup", _yfusup))
        _listParam.Add(New Datos.DParametro("@yfvsup", _yfvsup))
        _listParam.Add(New Datos.DParametro("@yfmstk", 0))
        _listParam.Add(New Datos.DParametro("@yfclot", 0))

        _listParam.Add(New Datos.DParametro("@yfsmin", _yfsmin))
        _listParam.Add(New Datos.DParametro("@yfap", _yfap))
        _listParam.Add(New Datos.DParametro("@yfimg", _yfimg))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@yfdetpro", _yfdetpro))
        _listParam.Add(New Datos.DParametro("@yfgr5", _yfgr5))

        _listParam.Add(New Datos.DParametro("@ycodact", _ycodact))
        _listParam.Add(New Datos.DParametro("@ygcodu", _yumed))
        _listParam.Add(New Datos.DParametro("@ycodprosin", _ycodprosin))
        _listParam.Add(New Datos.DParametro("@ypreciosif", _ypreciosif))

        _listParam.Add(New Datos.DParametro("@TY0051", "", TY0051))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _yfnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnGeneralProductos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnNameLabel() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnNameReporte() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 61))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prLibreriaClienteLGeneral(cod1 As Integer, cod2 As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@ylcod1", cod1))
        _listParam.Add(New Datos.DParametro("@ylcod2", cod2))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_prListarBanco(cod1 As Integer, cod2 As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 15))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function



    Public Shared Function L_prLibreriaClienteLGeneralZonas() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prLibreriaClienteLGeneralMeses() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prLibreriaClienteLGeneralFrecVisita() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prLibreriaClienteLGeneralPrecios() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnDetalleProducto(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _listParam.Add(New Datos.DParametro("@yfnumi", _numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnCodigoBarra() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 12))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnCodigoBarraUno(yfnumi As String) As DataTable

        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 13))
        _listParam.Add(New Datos.DParametro("@yfnumi", yfnumi))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnBuscarProductoConversion(_Numi As String) As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String = "yfnumi = " + _Numi + " ORDER BY yfnumi"
        _Tabla = D_Datos_Tabla("*", "TY005", _Where)
        Return _Tabla
    End Function

    Public Shared Function L_prCategoriaGrabar(ByRef _numi As String, _cod1 As String, _cod2 As String, _desc1 As String, _desc2 As String) As Boolean
        Dim _Error As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 55))
        _listParam.Add(New Datos.DParametro("@ylcod1", _cod1))
        _listParam.Add(New Datos.DParametro("@ylcod2", _cod2))
        _listParam.Add(New Datos.DParametro("@desc", _desc1))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            _Error = False
        Else
            _Error = True
        End If
        Return Not _Error
    End Function

    Public Shared Function L_CuentaContable() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 444))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function
#End Region

#Region "TY004 CLIENTES"


    Public Shared Function L_fnEliminarClientes(numi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TY004", "ydnumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@ydnumi", numi))
            _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
    Public Shared Function L_fnEliminarClientesConDetalleZona(numi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TY004", "ydnumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", 7))
            _listParam.Add(New Datos.DParametro("@ydnumi", numi))
            _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function


    Public Shared Function L_fnGrabarCLiente(ByRef _ydnumi As String,
                                             _ydcod As String, _ydrazonsocial As String, _yddesc As String,
                                             _ydnumiVendedor As Integer, _ydzona As Integer, _yddct As Integer,
                                             _yddctnum As String, _yddirec As String, _ydtelf1 As String,
                                             _ydtelf2 As String, _ydcat As Integer, _ydest As Integer,
                                             _ydlat As Double, _ydlongi As Double, _ydobs As String,
                                             _ydfnac As String, _ydnomfac As String, _ydtip As Integer,
                                             _ydnit As String, _yddias As String, _ydlcred As String,
                                             _ydfecing As String, _ydultvent As String, _ydimg As String,
                                             _ydrut As String, Optional _ydesciv As Integer = 1,
                                             Optional _ydEsposa As String = "", Optional _ydCiesposa As String = "",
                                             Optional _ydtipdocelec As Integer = 1, Optional _ydcorreo As String = "", Optional _ydcomplemento As String = "") As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        ' @ydnumi ,@ydcod  ,@yddesc  ,@ydzona  ,@yddct  ,
        '@yddctnum  ,@yddirec  ,@ydtelf1  ,@ydtelf2  ,@ydcat  ,@ydest  ,@ydlat  ,@ydlongi  ,
        '@ydprconsu  ,@ydobs  ,@ydfnac  ,@ydnomfac  ,@ydtip,@ydnit ,@ydfecing ,@ydultvent,@ydimg ,@newFecha,@newHora,@yduact
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@ydnumi", _ydnumi))
        _listParam.Add(New Datos.DParametro("@ydcod", _ydcod))
        _listParam.Add(New Datos.DParametro("@ydrazonsocioal", _ydrazonsocial))
        _listParam.Add(New Datos.DParametro("@yddesc", _yddesc))
        _listParam.Add(New Datos.DParametro("@ydnumivend", _ydnumiVendedor))
        _listParam.Add(New Datos.DParametro("@ydzona", _ydzona))
        _listParam.Add(New Datos.DParametro("@yddct", _yddct))
        _listParam.Add(New Datos.DParametro("@yddctnum", _yddctnum))
        _listParam.Add(New Datos.DParametro("@yddirec", _yddirec))
        _listParam.Add(New Datos.DParametro("@ydtelf1", _ydtelf1))
        _listParam.Add(New Datos.DParametro("@ydtelf2", _ydtelf2))
        _listParam.Add(New Datos.DParametro("@ydcat", _ydcat))
        _listParam.Add(New Datos.DParametro("@ydest", _ydest))
        _listParam.Add(New Datos.DParametro("@ydlat", _ydlat))
        _listParam.Add(New Datos.DParametro("@ydlongi", _ydlongi))
        _listParam.Add(New Datos.DParametro("@ydprconsu", 0))
        _listParam.Add(New Datos.DParametro("@ydobs", _ydobs))
        _listParam.Add(New Datos.DParametro("@ydfnac", _ydfnac))
        _listParam.Add(New Datos.DParametro("@ydnomfac", _ydnomfac))
        _listParam.Add(New Datos.DParametro("@ydtip", _ydtip))
        _listParam.Add(New Datos.DParametro("@ydnit", _ydnit))
        _listParam.Add(New Datos.DParametro("@yddias", _yddias))
        _listParam.Add(New Datos.DParametro("@ydlcred", _ydlcred))
        _listParam.Add(New Datos.DParametro("@ydfecing", _ydfecing))
        _listParam.Add(New Datos.DParametro("@ydultvent", _ydultvent))
        _listParam.Add(New Datos.DParametro("@ydimg", _ydimg))
        _listParam.Add(New Datos.DParametro("@ydrut", _ydrut))
        _listParam.Add(New Datos.DParametro("@ydesciv", _ydesciv))
        _listParam.Add(New Datos.DParametro("@ydesposa", _ydEsposa))
        _listParam.Add(New Datos.DParametro("@ydciesposa", _ydCiesposa))
        _listParam.Add(New Datos.DParametro("@ydtipdocelec", _ydtipdocelec))
        _listParam.Add(New Datos.DParametro("@ydcorreo", _ydcorreo))
        _listParam.Add(New Datos.DParametro("@complemento", _ydcomplemento))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _ydnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function


    Public Shared Function L_fnGrabarCLienteConDetalleZonas(ByRef _ydnumi As String, _ydcod As String, _yddesc As String, _ydnumiVendedor As Integer, _ydzona As Integer, _yddct As Integer, _yddctnum As String, _yddirec As String, _ydtelf1 As String, _ydtelf2 As String, _ydcat As Integer, _ydest As Integer, _ydlat As Double, _ydlongi As Double, _ydobs As String, _ydfnac As String, _ydnomfac As String, _ydtip As Integer, _ydnit As String, _ydfecing As String, _ydultvent As String, _ydimg As String, _detalle As DataTable) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        ' @ydnumi ,@ydcod  ,@yddesc  ,@ydzona  ,@yddct  ,
        '@yddctnum  ,@yddirec  ,@ydtelf1  ,@ydtelf2  ,@ydcat  ,@ydest  ,@ydlat  ,@ydlongi  ,
        '@ydprconsu  ,@ydobs  ,@ydfnac  ,@ydnomfac  ,@ydtip,@ydnit ,@ydfecing ,@ydultvent,@ydimg ,@newFecha,@newHora,@yduact
        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@ydnumi", _ydnumi))
        _listParam.Add(New Datos.DParametro("@ydcod", _ydcod))
        _listParam.Add(New Datos.DParametro("@yddesc", _yddesc))
        _listParam.Add(New Datos.DParametro("@ydnumivend", _ydnumiVendedor))
        _listParam.Add(New Datos.DParametro("@ydzona", _ydzona))
        _listParam.Add(New Datos.DParametro("@yddct", _yddct))
        _listParam.Add(New Datos.DParametro("@yddctnum", _yddctnum))
        _listParam.Add(New Datos.DParametro("@yddirec", _yddirec))
        _listParam.Add(New Datos.DParametro("@ydtelf1", _ydtelf1))
        _listParam.Add(New Datos.DParametro("@ydtelf2", _ydtelf2))
        _listParam.Add(New Datos.DParametro("@ydcat", _ydcat))
        _listParam.Add(New Datos.DParametro("@ydest", _ydest))
        _listParam.Add(New Datos.DParametro("@ydlat", _ydlat))
        _listParam.Add(New Datos.DParametro("@ydlongi", _ydlongi))
        _listParam.Add(New Datos.DParametro("@ydprconsu", 0))
        _listParam.Add(New Datos.DParametro("@ydobs", _ydobs))
        _listParam.Add(New Datos.DParametro("@ydfnac", _ydfnac))
        _listParam.Add(New Datos.DParametro("@ydnomfac", _ydnomfac))
        _listParam.Add(New Datos.DParametro("@ydtip", _ydtip))
        _listParam.Add(New Datos.DParametro("@ydnit", _ydnit))
        _listParam.Add(New Datos.DParametro("@ydfecing", _ydfecing))
        _listParam.Add(New Datos.DParametro("@ydultvent", _ydultvent))
        _listParam.Add(New Datos.DParametro("@ydimg", _ydimg))
        _listParam.Add(New Datos.DParametro("@TZ0013", "", _detalle))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _ydnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnModificarClientesConDetalleZonas(ByRef _ydnumi As String, _ydcod As String,
                                            _yddesc As String, _ydnumiVendedor As Integer, _ydzona As Integer,
                                            _yddct As Integer, _yddctnum As String,
                                            _yddirec As String, _ydtelf1 As String,
                                            _ydtelf2 As String, _ydcat As Integer, _ydest As Integer, _ydlat As Double, _ydlongi As Double, _ydobs As String,
                                            _ydfnac As String, _ydnomfac As String,
                                            _ydtip As Integer, _ydnit As String, _ydfecing As String, _ydultvent As String, _ydimg As String,
                                            _detalle As DataTable) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@ydnumi", _ydnumi))
        _listParam.Add(New Datos.DParametro("@ydcod", _ydcod))
        _listParam.Add(New Datos.DParametro("@yddesc", _yddesc))
        _listParam.Add(New Datos.DParametro("@ydnumivend", _ydnumiVendedor))
        _listParam.Add(New Datos.DParametro("@ydzona", _ydzona))
        _listParam.Add(New Datos.DParametro("@yddct", _yddct))
        _listParam.Add(New Datos.DParametro("@yddctnum", _yddctnum))
        _listParam.Add(New Datos.DParametro("@yddirec", _yddirec))
        _listParam.Add(New Datos.DParametro("@ydtelf1", _ydtelf1))
        _listParam.Add(New Datos.DParametro("@ydtelf2", _ydtelf2))
        _listParam.Add(New Datos.DParametro("@ydcat", _ydcat))
        _listParam.Add(New Datos.DParametro("@ydest", _ydest))
        _listParam.Add(New Datos.DParametro("@ydlat", _ydlat))
        _listParam.Add(New Datos.DParametro("@ydlongi", _ydlongi))
        _listParam.Add(New Datos.DParametro("@ydprconsu", 0))
        _listParam.Add(New Datos.DParametro("@ydobs", _ydobs))
        _listParam.Add(New Datos.DParametro("@ydfnac", _ydfnac))
        _listParam.Add(New Datos.DParametro("@ydnomfac", _ydnomfac))
        _listParam.Add(New Datos.DParametro("@ydtip", _ydtip))
        _listParam.Add(New Datos.DParametro("@ydnit", _ydnit))
        _listParam.Add(New Datos.DParametro("@ydfecing", _ydfecing))
        _listParam.Add(New Datos.DParametro("@ydultvent", _ydultvent))
        _listParam.Add(New Datos.DParametro("@ydimg", _ydimg))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TZ0013", "", _detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _ydnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnModificarClientes(ByRef _ydnumi As String, _ydcod As String,
                                            _ydrazonSocial As String, _yddesc As String, _ydnumiVendedor As Integer, _ydzona As Integer,
                                             _yddct As Integer, _yddctnum As String,
                                             _yddirec As String, _ydtelf1 As String,
                                             _ydtelf2 As String, _ydcat As Integer, _ydest As Integer, _ydlat As Double, _ydlongi As Double, _ydobs As String,
                                             _ydfnac As String, _ydnomfac As String,
                                             _ydtip As Integer, _ydnit As String, _yddias As String, _ydlcred As String, _ydfecing As String, _ydultvent As String, _ydimg As String, _ydrut As String,
                                                 Optional _ydesciv As Integer = 1, Optional _ydEsposa As String = "", Optional _ydCiesposa As String = "",
                                             Optional _ydtipdocelec As Integer = 1, Optional _ydcorreo As String = "", Optional _ydcomplemento As String = "") As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@ydnumi", _ydnumi))
        _listParam.Add(New Datos.DParametro("@ydcod", _ydcod))
        _listParam.Add(New Datos.DParametro("@ydrazonsocioal", _ydrazonSocial))
        _listParam.Add(New Datos.DParametro("@yddesc", _yddesc))
        _listParam.Add(New Datos.DParametro("@ydnumivend", _ydnumiVendedor))
        _listParam.Add(New Datos.DParametro("@ydzona", _ydzona))
        _listParam.Add(New Datos.DParametro("@yddct", _yddct))
        _listParam.Add(New Datos.DParametro("@yddctnum", _yddctnum))
        _listParam.Add(New Datos.DParametro("@yddirec", _yddirec))
        _listParam.Add(New Datos.DParametro("@ydtelf1", _ydtelf1))
        _listParam.Add(New Datos.DParametro("@ydtelf2", _ydtelf2))
        _listParam.Add(New Datos.DParametro("@ydcat", _ydcat))
        _listParam.Add(New Datos.DParametro("@ydest", _ydest))
        _listParam.Add(New Datos.DParametro("@ydlat", _ydlat))
        _listParam.Add(New Datos.DParametro("@ydlongi", _ydlongi))
        _listParam.Add(New Datos.DParametro("@ydprconsu", 0))
        _listParam.Add(New Datos.DParametro("@ydobs", _ydobs))
        _listParam.Add(New Datos.DParametro("@ydfnac", _ydfnac))
        _listParam.Add(New Datos.DParametro("@ydnomfac", _ydnomfac))
        _listParam.Add(New Datos.DParametro("@ydtip", _ydtip))
        _listParam.Add(New Datos.DParametro("@ydnit", _ydnit))
        _listParam.Add(New Datos.DParametro("@yddias", _yddias))
        _listParam.Add(New Datos.DParametro("@ydlcred", _ydlcred))
        _listParam.Add(New Datos.DParametro("@ydfecing", _ydfecing))
        _listParam.Add(New Datos.DParametro("@ydultvent", _ydultvent))
        _listParam.Add(New Datos.DParametro("@ydimg", _ydimg))
        _listParam.Add(New Datos.DParametro("@ydrut", _ydrut))
        _listParam.Add(New Datos.DParametro("@ydesciv", _ydesciv))
        _listParam.Add(New Datos.DParametro("@ydesposa", _ydEsposa))
        _listParam.Add(New Datos.DParametro("@ydciesposa", _ydCiesposa))
        _listParam.Add(New Datos.DParametro("@ydtipdocelec", _ydtipdocelec))
        _listParam.Add(New Datos.DParametro("@ydcorreo", _ydcorreo))
        _listParam.Add(New Datos.DParametro("@complemento", _ydcomplemento))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _ydnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnGeneralClientes(tipo As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 12))
        _listParam.Add(New Datos.DParametro("@ydtip", tipo))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnReporteRutasClientes(_numiVendedor As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@ydnumi", _numiVendedor))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnReporteZonasVendedor(_numiVendedor As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _listParam.Add(New Datos.DParametro("@ydnumi", _numiVendedor))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prMapaCLienteGeneral() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prMapaCLienteGeneralPorZona(_zona As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@ydzona", _zona))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerDetalleZonas(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@ydnumi", _numi))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

        Return _Tabla
    End Function


#End Region

#Region "TY006 Categorias"

    Public Shared Function L_fnGeneralProgramas() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnGeneralProgramasCategorias(numicat As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@categoria", numicat))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnGeneralDetalleLibrerias(cod1 As String, cod2 As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@cod1", cod1))
        _listParam.Add(New Datos.DParametro("@cod2", cod2))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnGeneralCategorias() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnGeneralSucursales() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarProductos(almacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@almacen", almacen))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarLotesPorProductoVenta(_almacen As Integer, _codproducto As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@producto", _codproducto))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarCategorias() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarProductosConPrecios(_almacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnGeneralFinanciadores() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 14))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnGeneralTipoCambio() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 17))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnGeneralDocumento() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 18))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnGeneralMoneda() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 16))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnGeneralServicios() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@aauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Servicios", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnGeneralTipoPrestamo(cod As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 15))
        _listParam.Add(New Datos.DParametro("@categoria ", cod))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnGrabarCategorias(_ygnumi As String, cod As String, desc As String, tipo As Integer, margen As Decimal) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@ygcod", cod))
        _listParam.Add(New Datos.DParametro("@ygdesc", desc))
        _listParam.Add(New Datos.DParametro("@ygpcv", tipo))
        _listParam.Add(New Datos.DParametro("@ygmer", margen))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _ygnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnGrabarPrecios(_ygnumi As String, _almacen As Integer, _precio As DataTable) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@TY007", "", _precio))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _ygnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnGrabarAnalisis(_ygnumi As String, _precio As DataTable) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@analisis", "", _precio))
        _listParam.Add(New Datos.DParametro("@fechaRegistroBoleta", "", _ygnumi))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Boletas", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _ygnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnGrabarLibreriasPrograma(_ygnumi As String, _dt As DataTable) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 12))
        _listParam.Add(New Datos.DParametro("@TY0031", "", _dt))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _ygnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
#End Region

#Region "TZ001 Zonas"
    Public Shared Function L_fnGeneralZona() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@zauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TZ001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarPuntosPorZona(_zanumi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@zanumi", _zanumi))
        _listParam.Add(New Datos.DParametro("@zauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TZ001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnGrabarZona(_zanumi As String, _zaciudad As Integer, _zaprovincia As Integer, _zazona As Integer, _zacolor As String, point As DataTable) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '@zanumi,@zacity ,@zaprovi ,@zazona ,@zacolor   ,@newFecha,@newHora,@zauact
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@zanumi", _zanumi))
        _listParam.Add(New Datos.DParametro("@zacity", _zaciudad))
        _listParam.Add(New Datos.DParametro("@zaprovi", _zaprovincia))
        _listParam.Add(New Datos.DParametro("@zazona", _zazona))
        _listParam.Add(New Datos.DParametro("@zacolor", _zacolor))
        _listParam.Add(New Datos.DParametro("@zauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@Tz0012", "", point))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TZ001", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _zanumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnModificaZona(_zanumi As String, _zaciudad As Integer, _zaprovincia As Integer, _zazona As Integer, _zacolor As String, point As DataTable) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@zanumi", _zanumi))
        _listParam.Add(New Datos.DParametro("@zacity", _zaciudad))
        _listParam.Add(New Datos.DParametro("@zaprovi", _zaprovincia))
        _listParam.Add(New Datos.DParametro("@zazona", _zazona))
        _listParam.Add(New Datos.DParametro("@zacolor", _zacolor))
        _listParam.Add(New Datos.DParametro("@zauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@Tz0012", "", point))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TZ001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _zanumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnEliminarZona(numi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TZ001", "zanumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@zanumi", numi))
            _listParam.Add(New Datos.DParametro("@zauact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TZ001", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
#End Region
#Region "heabolBoletas"
    Public Shared Function L_fnCabeceraBoleta() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Boletas", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_Taras_ModificarBoleta(_Cod As String, _pesoTara As String) As Boolean
        Dim _Err As Boolean
        Dim Sql, _where As String

        Sql = "cod = '" + _Cod + "' , " +
        "pesoTara = " + _pesoTara + "  "

        _where = "cod = " + _Cod
        _Err = D_Modificar_Datos("taras", Sql, _where)
        Return _Err
    End Function

    Public Shared Function L_fnListarClientesVentas1(numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 182))
        _listParam.Add(New Datos.DParametro("@tanumi", numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarClientesVentas11(numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 34))
        _listParam.Add(New Datos.DParametro("@tanumi", numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function
    Public Shared Sub L_Validar_CodigoTara(_Cod As String, ByRef _placa As String, ByRef _PesoTara As Decimal, ByRef _propietario As String)
        Dim _Tabla As DataTable

        _Tabla = D_Datos_Tabla("*", "taras", "cod = '" + _Cod + "'")

        If _Tabla.Rows.Count > 0 Then
            _placa = _Tabla.Rows(0).Item(1)
            _PesoTara = _Tabla.Rows(0).Item(2)
            _propietario = _Tabla.Rows(0).Item(4)
        End If
    End Sub
    Public Shared Function L_fnObtenerTara(_RazonSocial As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 12))
        _listParam.Add(New Datos.DParametro("@taidCore", _RazonSocial))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Boletas", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prReporteCañeroUno(cod As String, fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 13))
        _listParam.Add(New Datos.DParametro("@Cliente", cod))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Boletas", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prReporteCañeroTodos(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 14))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Boletas", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prReporteInstitucionUna(cod As String, fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 15))
        _listParam.Add(New Datos.DParametro("@CodInst", cod))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Boletas", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prReporteInstitucionUnaSola(cod As String, fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 18))
        _listParam.Add(New Datos.DParametro("@CodInst", cod))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Boletas", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteInstitucionTodos(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 16))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Boletas", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prReporteInstitucionSolos(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 17))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Boletas", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prReporteQcincoTodos(CodIns As Integer, CodCan As Integer, fechaI As String, fechaF As String, almacen As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 23))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Boletas", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prReporteRep330todInst(CodIns As Integer, CodCan As Integer, fechaI As String, fechaF As String, almacen As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 26))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Boletas", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteRep370todInst(CodIns As Integer, CodCan As Integer, fechaI As String, fechaF As String, almacen As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 27))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Boletas", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteRep930(CodIns As Integer, CodCan As Integer, fechaI As String, fechaF As String, almacen As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 28))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Boletas", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteRep390(CodIns As Integer, CodCan As Integer, fechaI As String, fechaF As String, almacen As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 30))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Boletas", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteRetiroCaneroUno(CodIns As Integer, CodCan As Integer, fechaI As String, fechaF As String, almacen As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _listParam.Add(New Datos.DParametro("@cliente", CodCan))
        _listParam.Add(New Datos.DParametro("@vendedor", CodIns))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@almacen", almacen))
        _Tabla = D_ProcedimientoConParam("Sp_Mam_ReporteVentas", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prReporteOtrosSurtidores(CodIns As Integer, CodCan As Integer, fechaI As String, fechaF As String, almacen As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 44))
        _listParam.Add(New Datos.DParametro("@codCan", CodCan))
        _listParam.Add(New Datos.DParametro("@codInst", CodIns))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@codSurtidor", almacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Boletas", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteRetiroCaneroUno1(CodIns As Integer, CodCan As Integer, fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 12))
        _listParam.Add(New Datos.DParametro("@cliente", CodCan))
        _listParam.Add(New Datos.DParametro("@vendedor", CodIns))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _Tabla = D_ProcedimientoConParam("Sp_Mam_ReporteVentas", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteReSumenDiario(CodIns As Integer, CodCan As Integer, fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 29))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Boletas", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarInstitucion() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 17))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarCaneros() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 31))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarCanerosxInst(Cod As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 13))
        _listParam.Add(New Datos.DParametro("@ydcod", Cod))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarCanerosxInst2(Cod As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 14))
        _listParam.Add(New Datos.DParametro("@ydcod", Cod))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

        Return _Tabla
    End Function
#End Region
#Region "TV001 Ventas"
    Public Shared Function L_fnGeneralVenta(almacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", almacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerDiferenciaAsientoContable(almacen As Integer) As Double
        Dim _resultado As Double
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 37))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@tanumi", almacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = _Tabla.Rows(0).Item(1)
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnObtenertOTALmOVIMIENTO(almacen As Integer) As Double
        Dim _resultado As Double
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 46))
        _listParam.Add(New Datos.DParametro("@ibid", almacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_tI002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = _Tabla.Rows(0).Item(0)
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnObtenerCuentaProducto(producto As Integer) As Double
        Dim _resultado As Double
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 48))
        _listParam.Add(New Datos.DParametro("@ibid", producto))
        _Tabla = D_ProcedimientoConParam("sp_Mam_tI002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = _Tabla.Rows(0).Item(0)
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnObtenerPrecioProducto(almacen As Integer, producto As Integer) As Double
        Dim _resultado As Double
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 47))
        _listParam.Add(New Datos.DParametro("@almacen", almacen))
        _listParam.Add(New Datos.DParametro("@ibid", producto))
        _Tabla = D_ProcedimientoConParam("sp_Mam_tI002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = _Tabla.Rows(0).Item(0)
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnGeneralVentaTodos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 333))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnCuentasxCobrar(idCanero As Integer, tbinteres As String, fechaPago As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 24))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@cliente", idCanero))
        _listParam.Add(New Datos.DParametro("@tafvcr", fechaPago))
        _listParam.Add(New Datos.DParametro("@taobs", tbinteres))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function ActualizarPrecioCostoPonderado(sucursal As Integer, numiProd As String, precio As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 17))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@caalm", sucursal))
        _listParam.Add(New Datos.DParametro("@canumi", numiProd))
        _listParam.Add(New Datos.DParametro("@precioCosto", precio))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Tc001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnGeneralVentaCombustible() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 33))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnGeneralVentaCombustibleOtroSurtidor() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 29))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnGeneralVentaServicios() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("VentaServicio", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnDetalleVenta(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@tanumi", _numi))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnDetalleBoleta(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@tanumi", _numi))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Boletas", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnDetalleVenta1(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 44))
        _listParam.Add(New Datos.DParametro("@tanumi", _numi))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnEliminarDescuento(Id As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", -1))
        _listParam.Add(New Datos.DParametro("@Id", Id))
        _listParam.Add(New Datos.DParametro("@Usuario", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Descuentos", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarProductosDescuentos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@Usuario", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Descuentos", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarDescuentos(ProductoId As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@Usuario", L_Usuario))
        _listParam.Add(New Datos.DParametro("@ProductoId", ProductoId))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Descuentos", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarDescuentosTodos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@Usuario", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Descuentos", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnGrabarPreciosDescuentos(ByRef numi As String, codpro As String, desde As Integer, hasta As Integer, precio As Double) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@ProductoId", codpro))
        _listParam.Add(New Datos.DParametro("@CantidadInicial", desde))
        _listParam.Add(New Datos.DParametro("@CantidadFinal", hasta))
        _listParam.Add(New Datos.DParametro("@Precio", precio))
        _listParam.Add(New Datos.DParametro("@FechaInicial", Now.Date.ToString("yyyy/MM/dd")))
        _listParam.Add(New Datos.DParametro("@FechaFinal", Now.Date.ToString("yyyy/MM/dd")))
        _listParam.Add(New Datos.DParametro("@Estado", 1))
        _listParam.Add(New Datos.DParametro("@usuario", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Descuentos", _listParam)

        If _Tabla.Rows.Count > 0 Then
            numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnDetalleVentaServicios(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@tanumi", _numi))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("VentaServicio", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnVentaNotaDeVenta1(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@tanumi", _numi))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_NotaDePrestamo(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 23))
        _listParam.Add(New Datos.DParametro("@tbid", _numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnVentaNotaDeVenta(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 101))
        _listParam.Add(New Datos.DParametro("@tanumi", _numi))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnVentaNotaDeVentaOtros(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 30))
        _listParam.Add(New Datos.DParametro("@tanumi", _numi))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnVentaNotaDeVentaServicios(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@tanumi", _numi))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("VentaServicio", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnVentaFactura(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 13))
        _listParam.Add(New Datos.DParametro("@tanumi", _numi))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarProductos(_almacen As String, _cliente As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@cliente", _cliente))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Marco_TV001", _listParam)

        Return _Tabla
    End Function


    'funcion para llenar los datos de la grilla en la venta
    Public Shared Function L_fnListarProductosC(_yfcBarra As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _listParam.Add(New Datos.DParametro("@yfcbarra", _yfcBarra))
        '_listParam.Add(New Datos.DParametro("@cliente", _cliente))
        '_listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        '_listParam.Add(New Datos.DParametro("@TV0011", "", _detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)



        Return _Tabla

    End Function

    Public Shared Function L_fnListarProductosSinLote(_almacen As String, _cliente As String, _detalle As DataTable) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@cliente", _cliente))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TV0011", "", _detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarProductoDiesel(_almacen As String, _cliente As String, _detalle As DataTable) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 99))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@cliente", _cliente))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TV0011", "", _detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarClientes() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarProforma() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarProductoProforma(_panumi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 12))
        _listParam.Add(New Datos.DParametro("@panumi", _panumi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarEmpleado() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 77))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarClientesUsuario(idUsuario As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 15))
        _listParam.Add(New Datos.DParametro("@ydNumiUsu", idUsuario))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_fnGrabarVenta(ByRef _tanumi As String, _taidCorelativo As String, _tafdoc As String, _taven As Integer, _tatven As Integer, _tafvcr As String, _taclpr As Integer,
                                           _tamon As Integer, _taobs As String,
                                           _tadesc As Double, _taice As Double,
                                           _tatotal As Double, detalle As DataTable, _almacen As Integer, _taprforma As Integer, Monto As DataTable, _NroCaja As Integer,
                                           _programa As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '    @tanumi ,@taalm,@tafdoc ,@taven  ,@tatven,
        '@tafvcr ,@taclpr,@tamon ,@taest  ,@taobs ,@tadesc ,@newFecha,@newHora,@tauact,@taproforma
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@tanumi", _tanumi))
        _listParam.Add(New Datos.DParametro("@taproforma", _taprforma))
        _listParam.Add(New Datos.DParametro("@taidCore", _taidCorelativo))
        _listParam.Add(New Datos.DParametro("@taalm", _almacen))
        _listParam.Add(New Datos.DParametro("@tafdoc", _tafdoc))
        _listParam.Add(New Datos.DParametro("@taven", _taven))
        _listParam.Add(New Datos.DParametro("@tatven", _tatven))
        _listParam.Add(New Datos.DParametro("@tafvcr", _tafvcr))
        _listParam.Add(New Datos.DParametro("@taclpr", _taclpr))
        _listParam.Add(New Datos.DParametro("@tamon", _tamon))
        _listParam.Add(New Datos.DParametro("@taest", 1))
        _listParam.Add(New Datos.DParametro("@taobs", _taobs))
        _listParam.Add(New Datos.DParametro("@tadesc", _tadesc))
        _listParam.Add(New Datos.DParametro("@taice", _taice))
        _listParam.Add(New Datos.DParametro("@tatotal", _tatotal))
        _listParam.Add(New Datos.DParametro("@taNrocaja", _NroCaja))
        _listParam.Add(New Datos.DParametro("@bcprograma", _programa))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TV0011", "", detalle))
        _listParam.Add(New Datos.DParametro("@TV0014", "", Monto))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _tanumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnGrabarBoleta(ByRef _nroBoleta As String, _fchBol As String, _codCan As String, _codInst As String, _cupo As String, _hora As String,
                                            detalle As DataTable, _controlTotal As String, _estado As Integer) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '    @tanumi ,@taalm,@tafdoc ,@taven  ,@tatven,
        '@tafvcr ,@taclpr,@tamon ,@taest  ,@taobs ,@tadesc ,@newFecha,@newHora,@tauact,@taproforma
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@nroBoleta", _nroBoleta))
        _listParam.Add(New Datos.DParametro("@fchBol", _fchBol))
        _listParam.Add(New Datos.DParametro("@codCan", _codCan))
        _listParam.Add(New Datos.DParametro("@codInst", _codInst))
        _listParam.Add(New Datos.DParametro("@cupo", _cupo))
        _listParam.Add(New Datos.DParametro("@hora", _hora))
        _listParam.Add(New Datos.DParametro("@detBol", "", detalle))
        _listParam.Add(New Datos.DParametro("@controlTotal", _controlTotal))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@estado", _estado))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Boletas", _listParam)


        If _Tabla.Rows.Count > 0 Then
            '_tanumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnGrabarVentaCombustible(ByRef _tanumi As String, _taidCorelativo As String, _tafdoc As String, _taven As Integer, _tatven As Integer, _tafvcr As String, _taclpr As Integer,
                                           _tamon As Integer, _taobs As String,
                                           _tadesc As Double, _taice As Double,
                                           _tatotal As Double, detalle As DataTable, _almacen As Integer, _taprforma As Integer, Monto As DataTable, _NroCaja As Integer,
                                           _programa As String, _tcentregado As String, _tcentregadoci As String, _tcdespachador As Integer, _tcplaca As String, _tcretiro As String, _tcnitretiro As String,
                                           _tcfacnombre As String, _tcfacnit As String, _tiposoli As Integer, _surtidor As Integer, _tiposurtidor As Boolean, _Autorizacion As Integer, _AporteDiesel As Decimal) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '    @tanumi ,@taalm,@tafdoc ,@taven  ,@tatven,
        '@tafvcr ,@taclpr,@tamon ,@taest  ,@taobs ,@tadesc ,@newFecha,@newHora,@tauact,@taproforma
        _listParam.Add(New Datos.DParametro("@tipo", 22))
        _listParam.Add(New Datos.DParametro("@tanumi", _tanumi))
        _listParam.Add(New Datos.DParametro("@taproforma", _taprforma))
        _listParam.Add(New Datos.DParametro("@taidCore", _taidCorelativo))
        _listParam.Add(New Datos.DParametro("@taalm", _almacen))
        _listParam.Add(New Datos.DParametro("@tafdoc", _tafdoc))
        _listParam.Add(New Datos.DParametro("@taven", _taven))
        _listParam.Add(New Datos.DParametro("@tatven", _tatven))
        _listParam.Add(New Datos.DParametro("@tafvcr", _tafvcr))
        _listParam.Add(New Datos.DParametro("@taclpr", _taclpr))
        _listParam.Add(New Datos.DParametro("@tamon", _tamon))
        _listParam.Add(New Datos.DParametro("@taest", 1))
        _listParam.Add(New Datos.DParametro("@taobs", _taobs))
        _listParam.Add(New Datos.DParametro("@tadesc", _tadesc))
        _listParam.Add(New Datos.DParametro("@taice", _taice))
        _listParam.Add(New Datos.DParametro("@tatotal", _tatotal))
        _listParam.Add(New Datos.DParametro("@taNrocaja", _NroCaja))
        _listParam.Add(New Datos.DParametro("@bcprograma", _programa))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TV0011", "", detalle))
        _listParam.Add(New Datos.DParametro("@TV0014", "", Monto))
        _listParam.Add(New Datos.DParametro("@tcentregado", _tcentregado))
        _listParam.Add(New Datos.DParametro("@tcentregadoci", _tcentregadoci))
        _listParam.Add(New Datos.DParametro("@tcdespachador", _tcdespachador))
        _listParam.Add(New Datos.DParametro("@tcplaca", _tcplaca))
        _listParam.Add(New Datos.DParametro("@tcretiro", _tcretiro))
        _listParam.Add(New Datos.DParametro("@tcnitretiro", _tcnitretiro))
        _listParam.Add(New Datos.DParametro("@tcfacnombre", _tcfacnombre))
        _listParam.Add(New Datos.DParametro("@tcfacnit", _tcfacnit))
        _listParam.Add(New Datos.DParametro("@tiposoli", _tiposoli))
        _listParam.Add(New Datos.DParametro("@surtidor", _surtidor))
        _listParam.Add(New Datos.DParametro("@tctiposurtidor", _tiposurtidor))
        _listParam.Add(New Datos.DParametro("@autorizacion", _Autorizacion))
        _listParam.Add(New Datos.DParametro("@aporteDiesel", _AporteDiesel))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _tanumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnGrabarVentaCombustibleOtroS(ByRef _tanumi As String, _tcentregado As String, _tcentregadoci As String, _tcdespachador As Integer, _tcplaca As String, _tcretiro As String, _tcnitretiro As String,
                                           _tcfacnombre As String, _tcfacnit As String, _tiposoli As Integer, _surtidor As Integer, _tiposurtidor As Boolean, _Autorizacion As Integer, _Cantidad As Decimal, _Precio As Decimal, _Total As Decimal,
                                                           _TipVenta As Integer, _cliente As Integer, _fdecha As String, _observacion As String
                                           ) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '    @tanumi ,@taalm,@tafdoc ,@taven  ,@tatven,
        '@tafvcr ,@taclpr,@tamon ,@taest  ,@taobs ,@tadesc ,@newFecha,@newHora,@tauact,@taproforma
        _listParam.Add(New Datos.DParametro("@tipo", 28))
        _listParam.Add(New Datos.DParametro("@tanumi", _tanumi))
        _listParam.Add(New Datos.DParametro("@tcentregado", _tcentregado))
        _listParam.Add(New Datos.DParametro("@tcentregadoci", _tcentregadoci))
        _listParam.Add(New Datos.DParametro("@tcdespachador", _tcdespachador))
        _listParam.Add(New Datos.DParametro("@tcplaca", _tcplaca))
        _listParam.Add(New Datos.DParametro("@tcretiro", _tcretiro))
        _listParam.Add(New Datos.DParametro("@tcnitretiro", _tcnitretiro))
        _listParam.Add(New Datos.DParametro("@tcfacnombre", _tcfacnombre))
        _listParam.Add(New Datos.DParametro("@tcfacnit", _tcfacnit))
        _listParam.Add(New Datos.DParametro("@tiposoli", _tiposoli))
        _listParam.Add(New Datos.DParametro("@taest", 1))
        _listParam.Add(New Datos.DParametro("@surtidor", _surtidor))
        _listParam.Add(New Datos.DParametro("@tctiposurtidor", _tiposurtidor))
        _listParam.Add(New Datos.DParametro("@autorizacion", _Autorizacion))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@cantidad", _Cantidad))
        _listParam.Add(New Datos.DParametro("@precio", _Precio))
        _listParam.Add(New Datos.DParametro("@total", _Total))
        _listParam.Add(New Datos.DParametro("@tatven", _TipVenta))
        _listParam.Add(New Datos.DParametro("@cliente", _cliente))
        _listParam.Add(New Datos.DParametro("@tafdoc", _fdecha))
        _listParam.Add(New Datos.DParametro("@taobs", _observacion))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _tanumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnEditarVentaCombustibleOtroS(ByRef _tanumi As String, _tcentregado As String, _tcentregadoci As String, _tcdespachador As Integer, _tcplaca As String, _tcretiro As String, _tcnitretiro As String,
                                           _tcfacnombre As String, _tcfacnit As String, _tiposoli As Integer, _surtidor As Integer, _tiposurtidor As Boolean, _Autorizacion As Integer, _Cantidad As Decimal, _Precio As Decimal, _Total As Decimal,
                                                           _TipVenta As Integer, _cliente As Integer, _fdecha As String, _observacion As String
                                           ) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '    @tanumi ,@taalm,@tafdoc ,@taven  ,@tatven,
        '@tafvcr ,@taclpr,@tamon ,@taest  ,@taobs ,@tadesc ,@newFecha,@newHora,@tauact,@taproforma
        _listParam.Add(New Datos.DParametro("@tipo", 31))
        _listParam.Add(New Datos.DParametro("@tanumi", _tanumi))
        _listParam.Add(New Datos.DParametro("@tcentregado", _tcentregado))
        _listParam.Add(New Datos.DParametro("@tcentregadoci", _tcentregadoci))
        _listParam.Add(New Datos.DParametro("@tcdespachador", _tcdespachador))
        _listParam.Add(New Datos.DParametro("@tcplaca", _tcplaca))
        _listParam.Add(New Datos.DParametro("@tcretiro", _tcretiro))
        _listParam.Add(New Datos.DParametro("@tcnitretiro", _tcnitretiro))
        _listParam.Add(New Datos.DParametro("@tcfacnombre", _tcfacnombre))
        _listParam.Add(New Datos.DParametro("@tcfacnit", _tcfacnit))
        _listParam.Add(New Datos.DParametro("@tiposoli", _tiposoli))
        _listParam.Add(New Datos.DParametro("@taest", 1))
        _listParam.Add(New Datos.DParametro("@surtidor", _surtidor))
        _listParam.Add(New Datos.DParametro("@tctiposurtidor", _tiposurtidor))
        _listParam.Add(New Datos.DParametro("@autorizacion", _Autorizacion))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@cantidad", _Cantidad))
        _listParam.Add(New Datos.DParametro("@precio", _Precio))
        _listParam.Add(New Datos.DParametro("@total", _Total))
        _listParam.Add(New Datos.DParametro("@tatven", _TipVenta))
        _listParam.Add(New Datos.DParametro("@cliente", _cliente))
        _listParam.Add(New Datos.DParametro("@tafdoc", _fdecha))
        _listParam.Add(New Datos.DParametro("@taobs", _observacion))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _tanumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnGrabarTO001(_tipo As Integer, ByRef _tanumi As Integer, Optional _obnumito1 As String = "", Optional _oblin As String = "",
                                           Optional _obcuenta As String = "", Optional _obobs As String = "", Optional _obdebebs As Double = 0.00,
                                           Optional _obhaberbs As Double = 0.00, Optional _obdebeus As Double = 0.00, Optional _obhaberus As Double = 0.00,
                                           Optional _oataalm As String = "", Optional _oanumor As String = "", Optional _oanumfac As String = "") As Integer
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '    @tanumi ,@taalm,@tafdoc ,@taven  ,@tatven,
        '@tafvcr ,@taclpr,@tamon ,@taest  ,@taobs ,@tadesc ,@newFecha,@newHora,@tauact,@taproforma
        _listParam.Add(New Datos.DParametro("@tipo", _tipo))
        _listParam.Add(New Datos.DParametro("@tanumi", _tanumi))
        _listParam.Add(New Datos.DParametro("@obnumito1", _obnumito1))
        _listParam.Add(New Datos.DParametro("@oblin", _oblin))
        _listParam.Add(New Datos.DParametro("@obcuenta", _obcuenta))
        _listParam.Add(New Datos.DParametro("@obobs", _obobs))
        _listParam.Add(New Datos.DParametro("@obdebebs", _obdebebs))
        _listParam.Add(New Datos.DParametro("@obhaberbs", _obhaberbs))
        _listParam.Add(New Datos.DParametro("@obdebeus", _obdebeus))
        _listParam.Add(New Datos.DParametro("@obhaberus", _obhaberus))
        _listParam.Add(New Datos.DParametro("@oataalm", _oataalm))
        _listParam.Add(New Datos.DParametro("@oanumor", _oanumor))
        _listParam.Add(New Datos.DParametro("@oanumfac", _oanumfac))
        _Tabla = D_ProcedimientoConParam("sp_Mam_HeaAsiCont", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _tanumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _tanumi
    End Function

    Public Shared Function L_fnGrabarVentaServicios(ByRef _tanumi As String, _tafdoc As String, _taven As Integer, _tatven As Integer, _tafvcr As String, _taclpr As Integer,
                                           _tamon As Integer, _taobs As String,
                                           _tadesc As Double, _taice As Double,
                                           _tatotal As Double, detalle As DataTable, _almacen As Integer) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '  (@tanumi,@taalm,@tafdoc ,@taven  ,@tatven,
        '@tafvcr ,@taclpr,@tamon ,@taest  ,@taobs ,@tadesc, @taice ,@tatotal ,@newFecha,@newHora,@tauact)
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@tanumi", _tanumi))

        _listParam.Add(New Datos.DParametro("@taalm", _almacen))
        _listParam.Add(New Datos.DParametro("@tafdoc", _tafdoc))
        _listParam.Add(New Datos.DParametro("@taven", _taven))
        _listParam.Add(New Datos.DParametro("@tatven", _tatven))
        _listParam.Add(New Datos.DParametro("@tafvcr", _tafvcr))
        _listParam.Add(New Datos.DParametro("@taclpr", _taclpr))
        _listParam.Add(New Datos.DParametro("@tamon", _tamon))
        _listParam.Add(New Datos.DParametro("@taest", 1))
        _listParam.Add(New Datos.DParametro("@taobs", _taobs))
        _listParam.Add(New Datos.DParametro("@tadesc", _tadesc))
        _listParam.Add(New Datos.DParametro("@taice", _taice))
        _listParam.Add(New Datos.DParametro("@tatotal", _tatotal))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TV0011", "", detalle))
        _Tabla = D_ProcedimientoConParam("VentaServicio", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _tanumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnModificarVentaServicios(ByRef _tanumi As String, _tafdoc As String, _taven As Integer, _tatven As Integer, _tafvcr As String, _taclpr As Integer,
                                           _tamon As Integer, _taobs As String,
                                           _tadesc As Double, _taice As Double,
                                           _tatotal As Double, detalle As DataTable, _almacen As Integer) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '  (@tanumi,@taalm,@tafdoc ,@taven  ,@tatven,
        '@tafvcr ,@taclpr,@tamon ,@taest  ,@taobs ,@tadesc, @taice ,@tatotal ,@newFecha,@newHora,@tauact)
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@tanumi", _tanumi))

        _listParam.Add(New Datos.DParametro("@taalm", _almacen))
        _listParam.Add(New Datos.DParametro("@tafdoc", _tafdoc))
        _listParam.Add(New Datos.DParametro("@taven", _taven))
        _listParam.Add(New Datos.DParametro("@tatven", _tatven))
        _listParam.Add(New Datos.DParametro("@tafvcr", _tafvcr))
        _listParam.Add(New Datos.DParametro("@taclpr", _taclpr))
        _listParam.Add(New Datos.DParametro("@tamon", _tamon))
        _listParam.Add(New Datos.DParametro("@taest", 1))
        _listParam.Add(New Datos.DParametro("@taobs", _taobs))
        _listParam.Add(New Datos.DParametro("@tadesc", _tadesc))
        _listParam.Add(New Datos.DParametro("@taice", _taice))
        _listParam.Add(New Datos.DParametro("@tatotal", _tatotal))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TV0011", "", detalle))
        _Tabla = D_ProcedimientoConParam("VentaServicio", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _tanumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnModificarBoleta(ByRef _nroBoleta As String, _fchBol As String, _codCan As String, _codInst As String, _cupo As String, _hora As String,
                                            detalle As DataTable, controlTotal As String, _estado As Integer) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@nroBoleta", _nroBoleta))
        _listParam.Add(New Datos.DParametro("@fchBol", _fchBol))
        _listParam.Add(New Datos.DParametro("@codCan", _codCan))
        _listParam.Add(New Datos.DParametro("@codInst", _codInst))
        _listParam.Add(New Datos.DParametro("@cupo", _cupo))
        _listParam.Add(New Datos.DParametro("@hora", _hora))
        _listParam.Add(New Datos.DParametro("@detBol", "", detalle))
        _listParam.Add(New Datos.DParametro("@controlTotal", controlTotal))
        _listParam.Add(New Datos.DParametro("@estado", _estado))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Boletas", _listParam)


        If _Tabla.Rows.Count > 0 Then
            '_tanumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnModificarVenta(_tanumi As String, _tafdoc As String, _taven As Integer, _tatven As Integer, _tafvcr As String, _taclpr As Integer,
                                           _tamon As Integer, _taobs As String,
                                           _tadesc As Double, _taice As Double, _tatotal As Double, detalle As DataTable, _almacen As Integer, _taprforma As Integer,
                                              monto As DataTable, _NroCaja As Integer,
                                           _programa As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@tanumi", _tanumi))
        _listParam.Add(New Datos.DParametro("@taalm", _almacen))
        _listParam.Add(New Datos.DParametro("@taproforma", _taprforma))
        _listParam.Add(New Datos.DParametro("@tafdoc", _tafdoc))
        _listParam.Add(New Datos.DParametro("@taven", _taven))
        _listParam.Add(New Datos.DParametro("@tatven", _tatven))
        _listParam.Add(New Datos.DParametro("@tafvcr", _tafvcr))
        _listParam.Add(New Datos.DParametro("@taclpr", _taclpr))
        _listParam.Add(New Datos.DParametro("@tamon", _tamon))
        _listParam.Add(New Datos.DParametro("@taest", 1))
        _listParam.Add(New Datos.DParametro("@taobs", _taobs))
        _listParam.Add(New Datos.DParametro("@tadesc", _tadesc))
        _listParam.Add(New Datos.DParametro("@taice", _taice))
        _listParam.Add(New Datos.DParametro("@taNrocaja", _NroCaja))
        _listParam.Add(New Datos.DParametro("@bcprograma", _programa))
        _listParam.Add(New Datos.DParametro("@tatotal", _tatotal))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TV0011", "", detalle))
        _listParam.Add(New Datos.DParametro("@TV0014", "", monto))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _tanumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnModificarVentaDieselPropio(ByRef _tanumi As String, _taidCorelativo As String, _tafdoc As String, _taven As Integer, _tatven As Integer, _tafvcr As String, _taclpr As Integer,
                                           _tamon As Integer, _taobs As String,
                                           _tadesc As Double, _taice As Double,
                                           _tatotal As Double, detalle As DataTable, _almacen As Integer, _taprforma As Integer, Monto As DataTable, _NroCaja As Integer,
                                           _programa As String, _tcentregado As String, _tcentregadoci As String, _tcdespachador As Integer, _tcplaca As String, _tcretiro As String, _tcnitretiro As String,
                                           _tcfacnombre As String, _tcfacnit As String, _tiposoli As Integer, _surtidor As Integer, _tiposurtidor As Boolean, _Autorizacion As Integer) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '    @tanumi ,@taalm,@tafdoc ,@taven  ,@tatven,
        '@tafvcr ,@taclpr,@tamon ,@taest  ,@taobs ,@tadesc ,@newFecha,@newHora,@tauact,@taproforma
        _listParam.Add(New Datos.DParametro("@tipo", 222))
        _listParam.Add(New Datos.DParametro("@tanumi", _tanumi))
        _listParam.Add(New Datos.DParametro("@taproforma", _taprforma))
        _listParam.Add(New Datos.DParametro("@taidCore", _taidCorelativo))
        _listParam.Add(New Datos.DParametro("@taalm", _almacen))
        _listParam.Add(New Datos.DParametro("@tafdoc", _tafdoc))
        _listParam.Add(New Datos.DParametro("@taven", _taven))
        _listParam.Add(New Datos.DParametro("@tatven", _tatven))
        _listParam.Add(New Datos.DParametro("@tafvcr", _tafvcr))
        _listParam.Add(New Datos.DParametro("@taclpr", _taclpr))
        _listParam.Add(New Datos.DParametro("@tamon", _tamon))
        _listParam.Add(New Datos.DParametro("@taest", 1))
        _listParam.Add(New Datos.DParametro("@taobs", _taobs))
        _listParam.Add(New Datos.DParametro("@tadesc", _tadesc))
        _listParam.Add(New Datos.DParametro("@taice", _taice))
        _listParam.Add(New Datos.DParametro("@tatotal", _tatotal))
        _listParam.Add(New Datos.DParametro("@taNrocaja", _NroCaja))
        _listParam.Add(New Datos.DParametro("@bcprograma", _programa))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TV0011", "", detalle))
        _listParam.Add(New Datos.DParametro("@TV0014", "", Monto))
        _listParam.Add(New Datos.DParametro("@tcentregado", _tcentregado))
        _listParam.Add(New Datos.DParametro("@tcentregadoci", _tcentregadoci))
        _listParam.Add(New Datos.DParametro("@tcdespachador", _tcdespachador))
        _listParam.Add(New Datos.DParametro("@tcplaca", _tcplaca))
        _listParam.Add(New Datos.DParametro("@tcretiro", _tcretiro))
        _listParam.Add(New Datos.DParametro("@tcnitretiro", _tcnitretiro))
        _listParam.Add(New Datos.DParametro("@tcfacnombre", _tcfacnombre))
        _listParam.Add(New Datos.DParametro("@tcfacnit", _tcfacnit))
        _listParam.Add(New Datos.DParametro("@tiposoli", _tiposoli))
        _listParam.Add(New Datos.DParametro("@surtidor", _surtidor))
        _listParam.Add(New Datos.DParametro("@tctiposurtidor", _tiposurtidor))
        _listParam.Add(New Datos.DParametro("@autorizacion", _Autorizacion))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _tanumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnEliminarVenta(numi As String, ByRef mensaje As String, programa As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TV001", "tanumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@tanumi", numi))
            _listParam.Add(New Datos.DParametro("@bcprograma", programa))
            _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
            _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
    Public Shared Function L_fnEliminarVentaOtroSurtidor(numi As String, ByRef mensaje As String, programa As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TV001", "tanumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", 32))
            _listParam.Add(New Datos.DParametro("@tanumi", numi))
            _listParam.Add(New Datos.DParametro("@bcprograma", programa))
            _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
            _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
    Public Shared Function L_fnEliminarPrestamos(numi As String, codPres As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 24))
        _listParam.Add(New Datos.DParametro("@tanumi", numi))
        _listParam.Add(New Datos.DParametro("@tbobs", codPres))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnMostrarMontos(tanumi As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 14))
        _listParam.Add(New Datos.DParametro("@tanumi", tanumi))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)
        Return _Tabla
    End Function

    Public Shared Function L_fnGrabarTxCobrar(tanumi As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 23))
        _listParam.Add(New Datos.DParametro("@tanumi", tanumi))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)
        Return _Tabla
    End Function

    Public Shared Function L_fnVerificarPagos(numi As String) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 16))
        _listParam.Add(New Datos.DParametro("@tanumi", numi))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If
        Return _resultado
    End Function

    Public Shared Function L_fnVerificarCObros(numi As String, almacen As String) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 35))
        _listParam.Add(New Datos.DParametro("@tanumi", numi))
        _listParam.Add(New Datos.DParametro("@taalm", almacen))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If
        Return _resultado
    End Function

    Public Shared Function L_fnVerificarFactura(numi As String, almacen As String) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 36))
        _listParam.Add(New Datos.DParametro("@tanumi", numi))
        _listParam.Add(New Datos.DParametro("@taalm", almacen))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If
        Return _resultado
    End Function


    Public Shared Function VerificarAnalisis(_fecha As String) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        _Tabla = D_Datos_TablaInst("*", "analisis", "fechaRelacionBol= " + "'" + _fecha + "'")
        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If
        Return _resultado

    End Function

    Public Shared Function VerificarFechaFactorPonderado(_fecha As String) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        _Tabla = D_Datos_TablaInst("*", "factorRponderado", "fecha= " + "'" + _fecha + "'")
        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If
        Return _resultado

    End Function
    Public Shared Function VerificarInicioZafra(_fecha As String) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        _Tabla = D_Datos_TablaInst("*", "diasZafra", "gestion= " + "YEAR('" + _fecha + "')")
        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If
        Return _resultado

    End Function
    Public Shared Function VerificarNumBoleta(_fecha As String) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        _Tabla = D_Datos_TablaInst("*", "analisis", "nroBoleta= " + "'" + _fecha + "'")
        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If
        Return _resultado

    End Function

    Public Shared Function VerificarNumBoletaEnRegistroBoletas(_fecha As Integer) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        _Tabla = D_Datos_TablaInst1("*", "heaBol", "  nroBoleta= " + Convert.ToString(Convert.ToInt32(_fecha)))
        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If
        Return _resultado

    End Function

    Public Shared Function L_fnVerificarSiSeContabilizoVenta(_canumi As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 17))
        _listParam.Add(New Datos.DParametro("@tanumi", _canumi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function canaLimpia(nroBoleta As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 19))
        _listParam.Add(New Datos.DParametro("@nroBoleta", nroBoleta))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Boletas", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarClientesVenta() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 18))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_fnListarClientesVentas(numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 181))
        _listParam.Add(New Datos.DParametro("@tanumi", numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnVerificarCierreCaja(numi As String, tipo As String) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 19))
        _listParam.Add(New Datos.DParametro("@tanumi", numi))
        _listParam.Add(New Datos.DParametro("@tipoTCC0013", tipo))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If
        Return _resultado
    End Function

    Public Shared Function L_prBitacora(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 25))
        _listParam.Add(New Datos.DParametro("@vcnumi", _numi))
        _listParam.Add(New Datos.DParametro("@vcuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_InstitucionCanero(_Modo As Integer, Optional _Cadena As String = "") As DataTable
        Dim _Tabla As DataTable

        _Tabla = D_Datos_Tabla1("Institucion.id,Institucion.codInst,Institucion.nomInst,Institucion.direc,Institucion.telf", "Institucion")
        Return _Tabla
    End Function

    Public Shared Function L_prLibreriaClienteCategoria(cod1 As Integer, cod2 As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 44))
        _listParam.Add(New Datos.DParametro("@ylcod1", cod1))
        _listParam.Add(New Datos.DParametro("@ylcod2", cod2))
        _listParam.Add(New Datos.DParametro("@yfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarCaneroInstitucion(codCliente As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@cliente", codCliente))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function cargarGrupoCanero(cod As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@id", cod))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TG001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarGestiones() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TG001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarQuincena(gestion As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@gestion", gestion))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TG001", _listParam)

        Return _Tabla
    End Function
    Public Shared Sub L_Actualiza_Venta_Contabiliza(_Numi As String, _numiConta As String)
        Dim _Err As Boolean
        Dim Sql, _where As String
        Sql = "taproforma = " + _numiConta
        _where = "tanumi = " + _Numi

        _Err = D_Modificar_Datos("TV001", Sql, _where)
    End Sub

    Public Shared Sub L_Actualiza_Movimiento_Contabiliza(_Numi As String, _numiConta As String)
        Dim _Err As Boolean
        Dim Sql, _where As String
        Sql = "ibcont = " + _numiConta
        _where = "ibid = " + _Numi

        _Err = D_Modificar_Datos("TI002", Sql, _where)
    End Sub
    Public Shared Sub L_Actualiza_ingreso_Contabiliza(_Numi As String, _numiConta As String)
        Dim _Err As Boolean
        Dim Sql, _where As String
        Sql = "codCont = " + _numiConta
        _where = "ienumi = " + _Numi

        _Err = D_Modificar_Datos("TIE001", Sql, _where)
    End Sub
    Public Shared Sub L_Actualiza_otroSurtidor_Contabiliza(_Numi As String, _numiConta As String)
        Dim _Err As Boolean
        Dim Sql, _where As String
        Sql = "idContabiliza = " + _numiConta
        _where = "tcvnumi = " + _Numi

        _Err = D_Modificar_Datos("TV0011cp", Sql, _where)
    End Sub
    Public Shared Sub L_Actualiza_Prestamo_Contabiliza(_Numi As String, _numiConta As String)
        Dim _Err As Boolean
        Dim Sql, _where As String
        Sql = "tbcodcont = " + _numiConta
        _where = "tbid = " + _Numi

        _Err = D_Modificar_Datos("Prestamos", Sql, _where)
    End Sub

    Public Shared Function L_prReporteDiarioVentas(almacen As Integer, fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@almacen", almacen))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _Tabla = D_ProcedimientoConParam("Sp_Mam_ReporteVentas", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prRetiroInstitucionalUno(cod As String, almacen As String, fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@Cliente", cod))
        _listParam.Add(New Datos.DParametro("@almacen", almacen))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _Tabla = D_ProcedimientoConParam("Sp_Mam_ReporteVentas", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prRetiroInstitucionalTodos(almacen As String, fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@almacen", almacen))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _Tabla = D_ProcedimientoConParam("Sp_Mam_ReporteVentas", _listParam)

        Return _Tabla
    End Function

#End Region

#Region "TC001 Compras"
    Public Shared Function L_fnGeneralCompras() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnDetalleCompra(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@canumi", _numi))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnDetalleCompra1(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 44))
        _listParam.Add(New Datos.DParametro("@canumi", _numi))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnDetalleCompraTFC001(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@canumi", _numi))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarProductosCompra(_almacen As String, _catCosto As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@CatCosto", _catCosto))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Marco_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarProveedores() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarSucursales() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarDepositos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 24))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnPorcUtilidad() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnGrabarCompra(ByRef _canumi As String, _caalm As Integer, _cafdoc As String, _caTy4prov As Integer, _catven As Integer, _cafvcr As String,
                                           _camon As Integer, _caobs As String,
                                           _cadesc As Double, _catotal As Double, detalle As DataTable, detalleCompra As DataTable, _emision As String, _numemision As String,
                                           _consigna As Integer, _retenc As Integer, _tipocambio As Double, chofer As String, camion As String, placa As String, recibio As String,
                                           entrego As String, hojaRuta As Integer) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '   @canumi ,@caalm,@cafdoc ,@caty4prov  ,@catven,
        '@cafvcr,@camon ,@caest  ,@caobs ,@cadesc ,@newFecha,@newHora,@cauact
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@canumi", _canumi))
        _listParam.Add(New Datos.DParametro("@caalm", _caalm))
        _listParam.Add(New Datos.DParametro("@cafdoc", _cafdoc))
        _listParam.Add(New Datos.DParametro("@caty4prov", _caTy4prov))
        _listParam.Add(New Datos.DParametro("@catven", _catven))
        _listParam.Add(New Datos.DParametro("@cafvcr", _cafvcr))
        _listParam.Add(New Datos.DParametro("@camon", _camon))
        _listParam.Add(New Datos.DParametro("@caest", 1))
        _listParam.Add(New Datos.DParametro("@caobs", _caobs))
        _listParam.Add(New Datos.DParametro("@cadesc", _cadesc))
        _listParam.Add(New Datos.DParametro("@catotal", _catotal))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@caemision", _emision))
        _listParam.Add(New Datos.DParametro("@canumemis", _numemision))
        _listParam.Add(New Datos.DParametro("@caconsigna", _consigna))
        _listParam.Add(New Datos.DParametro("@caretenc", _retenc))
        _listParam.Add(New Datos.DParametro("@catipocambio", _tipocambio))
        _listParam.Add(New Datos.DParametro("@TC0011", "", detalle))
        _listParam.Add(New Datos.DParametro("@TFC001", "", detalleCompra))
        _listParam.Add(New Datos.DParametro("@chofer", "", chofer))
        _listParam.Add(New Datos.DParametro("@camion", "", camion))
        _listParam.Add(New Datos.DParametro("@placa", "", placa))
        _listParam.Add(New Datos.DParametro("@recibio", "", recibio))
        _listParam.Add(New Datos.DParametro("@entrego", "", entrego))
        _listParam.Add(New Datos.DParametro("@hojaRuta", hojaRuta))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _canumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnGrabarServicio(_tbfec As String, _tbmon As Integer, tbtcam As Integer, _tbins As Integer, _tbcan As Integer,
                                           _tbfin As Integer, _tbcap As Double, _tbapor As Double, _tbobs As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '   @canumi ,@caalm,@cafdoc ,@caty4prov  ,@catven,
        '@cafvcr,@camon ,@caest  ,@caobs ,@cadesc ,@newFecha,@newHora,@cauact
        _listParam.Add(New Datos.DParametro("@tipo", 25))
        _listParam.Add(New Datos.DParametro("@tbfec", _tbfec))
        _listParam.Add(New Datos.DParametro("@tbmon", _tbmon))
        _listParam.Add(New Datos.DParametro("@tbtcam", tbtcam))
        _listParam.Add(New Datos.DParametro("@tbins", _tbins))
        _listParam.Add(New Datos.DParametro("@tbcan", _tbcan))
        _listParam.Add(New Datos.DParametro("@tbfin", _tbfin))
        _listParam.Add(New Datos.DParametro("@tbcap", _tbcap))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@tbapor", _tbapor))
        _listParam.Add(New Datos.DParametro("@tbobs", _tbobs))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnGrabarPrestamo(ByRef _tanumi As String, _tbfec As String, _tbmon As Integer, tbtcam As Integer, _tbins As Integer, _tbcan As Integer,
                                           _tbfin As Integer, _tbpre As Integer,
                                           _tbprov As Integer, _tbdoc As Integer, _tbcite As String, _tbcap As Double, _tbapor As Double, _tbobs As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '   @canumi ,@caalm,@cafdoc ,@caty4prov  ,@catven,
        '@cafvcr,@camon ,@caest  ,@caobs ,@cadesc ,@newFecha,@newHora,@cauact
        _listParam.Add(New Datos.DParametro("@tipo", 19))
        _listParam.Add(New Datos.DParametro("@id", _tanumi))
        _listParam.Add(New Datos.DParametro("@tbfec", _tbfec))
        _listParam.Add(New Datos.DParametro("@tbmon", _tbmon))
        _listParam.Add(New Datos.DParametro("@tbtcam", tbtcam))
        _listParam.Add(New Datos.DParametro("@tbins", _tbins))
        _listParam.Add(New Datos.DParametro("@tbcan", _tbcan))
        _listParam.Add(New Datos.DParametro("@tbfin", _tbfin))
        _listParam.Add(New Datos.DParametro("@tbpre", _tbpre))
        _listParam.Add(New Datos.DParametro("@tbprov", _tbprov))
        _listParam.Add(New Datos.DParametro("@tbdoc", _tbdoc))
        _listParam.Add(New Datos.DParametro("@tbcite", _tbcite))
        _listParam.Add(New Datos.DParametro("@tbcap", _tbcap))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@tbapor", _tbapor))
        _listParam.Add(New Datos.DParametro("@tbobs", _tbobs))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _resultado = True
            _tanumi = _Tabla.Rows(0).Item(0)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnModificarPrestamo(id As Integer, _tbfec As String, _tbmon As Integer, tbtcam As Integer, _tbins As Integer, _tbcan As Integer,
                                           _tbfin As Integer, _tbpre As Integer,
                                            _tbprov As Integer, _tbdoc As Integer, _tbcite As String, _tbcap As Double, _tbapor As Double, _tbobs As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '   @canumi ,@caalm,@cafdoc ,@caty4prov  ,@catven,
        '@cafvcr,@camon ,@caest  ,@caobs ,@cadesc ,@newFecha,@newHora,@cauact
        _listParam.Add(New Datos.DParametro("@tipo", 22))
        _listParam.Add(New Datos.DParametro("@tbid", id))
        _listParam.Add(New Datos.DParametro("@tbfec", _tbfec))
        _listParam.Add(New Datos.DParametro("@tbmon", _tbmon))
        _listParam.Add(New Datos.DParametro("@tbtcam", tbtcam))
        _listParam.Add(New Datos.DParametro("@tbins", _tbins))
        _listParam.Add(New Datos.DParametro("@tbcan", _tbcan))
        _listParam.Add(New Datos.DParametro("@tbfin", _tbfin))
        _listParam.Add(New Datos.DParametro("@tbpre", _tbpre))
        _listParam.Add(New Datos.DParametro("@tbprov", _tbprov))
        _listParam.Add(New Datos.DParametro("@tbdoc", _tbdoc))
        _listParam.Add(New Datos.DParametro("@tbcite", _tbcite))
        _listParam.Add(New Datos.DParametro("@tbcap", _tbcap))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@tbapor", _tbapor))
        _listParam.Add(New Datos.DParametro("@tbobs", _tbobs))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_prCompraComprobanteGeneralPorNumi(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@canumi", _numi))
        ' _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnModificarCompra(_canumi As String, _caalm As Integer, _cafdoc As String, _caTy4prov As Integer, _catven As Integer, _cafvcr As String,
                                           _camon As Integer, _caobs As String,
                                           _cadesc As Double, _catotal As Double, detalle As DataTable, detalleCompra As DataTable, _emision As Integer, _numemision As Integer,
                                           _consigna As Integer, _retenc As Integer, _tipocambio As Double) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@canumi", _canumi))
        _listParam.Add(New Datos.DParametro("@caalm", _caalm))
        _listParam.Add(New Datos.DParametro("@cafdoc", _cafdoc))
        _listParam.Add(New Datos.DParametro("@caty4prov", _caTy4prov))
        _listParam.Add(New Datos.DParametro("@catven", _catven))
        _listParam.Add(New Datos.DParametro("@cafvcr", _cafvcr))
        _listParam.Add(New Datos.DParametro("@camon", _camon))
        _listParam.Add(New Datos.DParametro("@caest", 1))
        _listParam.Add(New Datos.DParametro("@caobs", _caobs))
        _listParam.Add(New Datos.DParametro("@cadesc", _cadesc))
        _listParam.Add(New Datos.DParametro("@catotal", _catotal))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@caemision", _emision))
        _listParam.Add(New Datos.DParametro("@canumemis", _numemision))
        _listParam.Add(New Datos.DParametro("@caconsigna", _consigna))
        _listParam.Add(New Datos.DParametro("@caretenc", _retenc))
        _listParam.Add(New Datos.DParametro("@catipocambio", _tipocambio))
        _listParam.Add(New Datos.DParametro("@TC0011", "", detalle))
        _listParam.Add(New Datos.DParametro("@TFC001", "", detalleCompra))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _canumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnEliminarCompra(numi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TC001", "canumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@canumi", numi))
            _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
            _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function

    Public Shared Function L_fnVerificarSiSeContabilizo(_canumi As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@canumi", _canumi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnVerificarPagosCompras(numi As String) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _listParam.Add(New Datos.DParametro("@canumi", numi))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
    Public Shared Function L_fnNotaCompras(_canumi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 12))
        _listParam.Add(New Datos.DParametro("@canumi", _canumi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnNotaCompras2(_canumi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 13))
        _listParam.Add(New Datos.DParametro("@canumi", _canumi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_recibo(_canumi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@ienumi", _canumi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TIE001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prListarEstadoCuentasCompraTotal(idProveedor As Integer, fechai As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@prov", idProveedor))
        _listParam.Add(New Datos.DParametro("@fechai", fechai))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_EstadoCuentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_prListarEstadoCuentasCompra(idProveedor As Integer, fechai As String, fechaf As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@prov", idProveedor))
        _listParam.Add(New Datos.DParametro("@fechai", fechai))
        _listParam.Add(New Datos.DParametro("@fechaf", fechaf))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_EstadoCuentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_fnEliminarCategoria(numi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TY006", "ygnumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@ygnumi", numi))
            _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
#End Region

#Region "TA002 Deposito"
    Public Shared Function L_fnEliminarDeposito(numi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TA002", "abnumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@abnumi", numi))
            _listParam.Add(New Datos.DParametro("@abuact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TA002", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function



    Public Shared Function L_fnGrabarDeposito(ByRef _abnumi As String, _abdesc As String, _abdir As String, _abtelf As String, _ablat As Double, _ablong As Double, _abimg As String, _abest As Integer) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        '@abnumi ,@abdesc ,@abdir ,@abtelf ,@ablat ,@ablong,@abimg ,@abest ,@newFecha,@newHora,@abuact
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@abnumi", _abnumi))
        _listParam.Add(New Datos.DParametro("@abdesc", _abdesc))
        _listParam.Add(New Datos.DParametro("@abdir", _abdir))
        _listParam.Add(New Datos.DParametro("@abtelf", _abtelf))
        _listParam.Add(New Datos.DParametro("@ablat", _ablat))
        _listParam.Add(New Datos.DParametro("@ablong", _ablong))
        _listParam.Add(New Datos.DParametro("@abimg", _abimg))
        _listParam.Add(New Datos.DParametro("@abest", _abest))


        _listParam.Add(New Datos.DParametro("@abuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TA002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _abnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnModificarDepositos(ByRef _abnumi As String, _abdesc As String, _abdir As String, _abtelf As String, _ablat As Double, _ablong As Double, _abimg As String, _abest As Integer) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@abnumi", _abnumi))
        _listParam.Add(New Datos.DParametro("@abdesc", _abdesc))
        _listParam.Add(New Datos.DParametro("@abdir", _abdir))
        _listParam.Add(New Datos.DParametro("@abtelf", _abtelf))
        _listParam.Add(New Datos.DParametro("@ablat", _ablat))
        _listParam.Add(New Datos.DParametro("@ablong", _ablong))
        _listParam.Add(New Datos.DParametro("@abimg", _abimg))
        _listParam.Add(New Datos.DParametro("@abest", _abest))

        _listParam.Add(New Datos.DParametro("@abuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TA002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _abnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnGeneralDepositos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@abuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TA002", _listParam)

        Return _Tabla
    End Function



#End Region

#Region "TA001 Almacen"
    Public Shared Function L_fnEliminarAlmacen(numi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TA001", "abnumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@aanumi", numi))
            _listParam.Add(New Datos.DParametro("@aauact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TA001", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function

    Public Shared Function L_fnEliminarServicio(numi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", -1))
        _listParam.Add(New Datos.DParametro("@Id", numi))
        _listParam.Add(New Datos.DParametro("@aauact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_Servicios", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function



    Public Shared Function L_fnGrabarAlmacen(ByRef _abnumi As String, _aata2dep As Integer, _aata2depVenta As Integer, _abdesc As String, _abdir As String, _abtelf As String, _ablat As Double, _ablong As Double, _abimg As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        '@aanumi ,@aata2dep,@aadesc ,@aadir ,@aatelf ,@aalat ,@aalong,@aaimg ,@newFecha,@newHora,@aauact

        'a.aanumi ,a.aabdes ,a.aadir ,a.aatel ,a.aalat ,a.aalong ,a.aaimg,aata2dep ,a.aafact ,a.aahact ,a.aauact
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@aanumi", _abnumi))
        _listParam.Add(New Datos.DParametro("@aata2dep", _aata2dep))
        _listParam.Add(New Datos.DParametro("@aata2depVenta", _aata2depVenta))
        _listParam.Add(New Datos.DParametro("@aadesc", _abdesc))
        _listParam.Add(New Datos.DParametro("@aadir", _abdir))
        _listParam.Add(New Datos.DParametro("@aatelf", _abtelf))
        _listParam.Add(New Datos.DParametro("@aalat", _ablat))
        _listParam.Add(New Datos.DParametro("@aalong", _ablong))
        _listParam.Add(New Datos.DParametro("@aaimg", _abimg))



        _listParam.Add(New Datos.DParametro("@aauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TA001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _abnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnGrabarServicio(ByRef id As String, NombreServicio As String, DetalleServicio As String, Estado As Integer,
                                              _ycodact As String, _yumed As Integer, _ycodprosin As String, _nombreumed As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        '@Id,@NombreServicio ,@DetalleServicio ,@Estado  ,@newFecha,@aauact
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@Id", id))
        _listParam.Add(New Datos.DParametro("@NombreServicio", NombreServicio))
        _listParam.Add(New Datos.DParametro("@DetalleServicio", DetalleServicio))
        _listParam.Add(New Datos.DParametro("@Estado", Estado))
        _listParam.Add(New Datos.DParametro("@ycodact", _ycodact))
        _listParam.Add(New Datos.DParametro("@ygcodu", _yumed))
        _listParam.Add(New Datos.DParametro("@ycodprosin", _ycodprosin))
        _listParam.Add(New Datos.DParametro("@nombreumed", _nombreumed))
        _listParam.Add(New Datos.DParametro("@aauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Servicios", _listParam)

        If _Tabla.Rows.Count > 0 Then
            id = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnModificarServicio(ByRef id As String, NombreServicio As String, DetalleServicio As String, Estado As Integer) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        '@Id,@NombreServicio ,@DetalleServicio ,@Estado  ,@newFecha,@aauact
        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@Id", id))
        _listParam.Add(New Datos.DParametro("@NombreServicio", NombreServicio))
        _listParam.Add(New Datos.DParametro("@DetalleServicio", DetalleServicio))
        _listParam.Add(New Datos.DParametro("@Estado", Estado))
        _listParam.Add(New Datos.DParametro("@aauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Servicios", _listParam)

        If _Tabla.Rows.Count > 0 Then
            id = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function




    Public Shared Function L_fnModificarAlmacen(ByRef _abnumi As String, _aata2dep As Integer, _aata2depVenta As Integer, _abdesc As String, _abdir As String, _abtelf As String, _ablat As Double, _ablong As Double, _abimg As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@aanumi", _abnumi))
        _listParam.Add(New Datos.DParametro("@aata2dep", _aata2dep))
        _listParam.Add(New Datos.DParametro("@aata2depVenta", _aata2depVenta))
        _listParam.Add(New Datos.DParametro("@aadesc", _abdesc))
        _listParam.Add(New Datos.DParametro("@aadir", _abdir))
        _listParam.Add(New Datos.DParametro("@aatelf", _abtelf))
        _listParam.Add(New Datos.DParametro("@aalat", _ablat))
        _listParam.Add(New Datos.DParametro("@aalong", _ablong))
        _listParam.Add(New Datos.DParametro("@aaimg", _abimg))

        _listParam.Add(New Datos.DParametro("@aauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TA001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _abnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnGeneralAlmacen() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@aauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TA001", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_fnListarDeposito() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@aauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TA001", _listParam)

        Return _Tabla
    End Function


#End Region

#Region "TS002 Dosificacion"

    Public Shared Function L_fnEliminarDosificacion(numi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TS002", "sbnumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@numi", numi))
            _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_go_TS002", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function



    Public Shared Function L_fnGrabarDosificacion(ByRef numi As String, cia As Integer, alm As String, sfc As String,
                                                  autoriz As String, nfac As Double, key As String, fdel As String,
                                                  fal As String, nota As String, nota2 As String, est As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@numi", numi))
        _listParam.Add(New Datos.DParametro("@cia", cia))
        _listParam.Add(New Datos.DParametro("@alm", alm))
        _listParam.Add(New Datos.DParametro("@sfc", sfc))
        _listParam.Add(New Datos.DParametro("@autoriz", autoriz))
        _listParam.Add(New Datos.DParametro("@nfac", nfac))
        _listParam.Add(New Datos.DParametro("@key", key))
        _listParam.Add(New Datos.DParametro("@fdel", fdel))
        _listParam.Add(New Datos.DParametro("@fal", fal))
        _listParam.Add(New Datos.DParametro("@nota", nota))
        _listParam.Add(New Datos.DParametro("@nota2", nota2))
        _listParam.Add(New Datos.DParametro("@est", est))

        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_go_TS002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnModificarDosificacion(ByRef numi As String, cia As Integer, alm As String, sfc As String,
                                                     autoriz As String, nfac As Double, key As String, fdel As String,
                                                     fal As String, nota As String, nota2 As String, est As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@numi", numi))
        _listParam.Add(New Datos.DParametro("@cia", cia))
        _listParam.Add(New Datos.DParametro("@alm", alm))
        _listParam.Add(New Datos.DParametro("@sfc", sfc))
        _listParam.Add(New Datos.DParametro("@autoriz", autoriz))
        _listParam.Add(New Datos.DParametro("@nfac", nfac))
        _listParam.Add(New Datos.DParametro("@key", key))
        _listParam.Add(New Datos.DParametro("@fdel", fdel))
        _listParam.Add(New Datos.DParametro("@fal", fal))
        _listParam.Add(New Datos.DParametro("@nota", nota))
        _listParam.Add(New Datos.DParametro("@nota2", nota2))
        _listParam.Add(New Datos.DParametro("@est", est))

        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_go_TS002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnGeneralDosificacion() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_go_TS002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarCompaniaDosificacion() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_go_TS002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarAlmacenDosificacion() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_go_TS002", _listParam)

        Return _Tabla
    End Function

#End Region

#Region "Facturar"

    Public Shared Sub L_Grabar_Factura(_Numi As String, _Fecha As String, _Nfac As String, _NAutoriz As String, _Est As String,
                                       _NitCli As String, _CodCli As String, _DesCli1 As String, _DesCli2 As String,
                                       _A As String, _B As String, _C As String, _D As String, _E As String, _F As String,
                                       _G As String, _H As String, _CodCon As String, _FecLim As String,
                                       _Imgqr As String, _Alm As String, _Numi2 As String, _hora As String,
                                        codigoRecepcion As String, estadoEmisionEdoc As String, fechaEmision1 As String,
                                       cuf As String, cuis As String, cufd As String, codigoControl As String, linkCodigoQr As String)
        Dim Sql As String
        Try
            Sql = "" + _Numi + ", " _
                + "'" + _Fecha + "', " _
                + "" + _Nfac + ", " _
                + "" + _NAutoriz + ", " _
                + "" + _Est + ", " _
                + "'" + _NitCli + "', " _
                + "" + _CodCli + ", " _
                + "'" + _DesCli1 + "', " _
                + "'" + _DesCli2 + "', " _
                + "" + _A + ", " _
                + "" + _B + ", " _
                + "" + _C + ", " _
                + "" + _D + ", " _
                + "" + _E + ", " _
                + "" + _F + ", " _
                + "" + _G + ", " _
                + "" + _H + ", " _
                + "'" + _CodCon + "', " _
                + "'" + _FecLim + "', " _
                + "" + _Imgqr + ", " _
                + "" + _Alm + ", " _
                + "" + _Numi2 + ", " _
                + "'" + _hora + "'," _
                + "'" + codigoRecepcion + "'," _
                + "'" + estadoEmisionEdoc + "'," _
                + "'" + fechaEmision1 + "'," _
                + "'" + cuf + "'," _
                + "'" + cuis + "'," _
                + "'" + cufd + "'," _
                + "'" + codigoControl + "'," _
                + "'" + linkCodigoQr + "'"
            '_ + "'" + fechaEmision1 + "'"
            D_Insertar_Datos("TFV001", Sql)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Shared Sub L_Grabar_TPA001(_Numi As String, _Fecha As String, _Tipo As String, _CodCli As String, _Nomcli As String,
                                       _Emision As String, _Estado As String, _Total As String, _Moneda As String,
                                       _Tc As String, _Nscf As String, _NumAsiento As String)

        Dim Sql As String
        Try
            Sql = "" + _Numi + ", " _
                + "'" + _Fecha + "', " _
                + "" + _Tipo + ", " _
                + "" + _CodCli + ", " _
                + "'" + _Nomcli + "', " _
                + "" + _Emision + ", " _
                + "" + _Estado + ", " _
                + "" + _Total + ", " _
                + "" + _Moneda + ", " _
                + "" + _Tc + ", " _
                + "" + _Nscf + ", " _
                + "" + _NumAsiento + ""

            D_Insertar_Datos("BDDiconCaneros.dbo.TPA001", Sql)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Shared Sub L_Modificar_Factura(Where As String, Optional _Fecha As String = "",
                                          Optional _Nfact As String = "", Optional _NAutoriz As String = "",
                                          Optional _Est As String = "", Optional _NitCli As String = "",
                                          Optional _CodCli As String = "", Optional _DesCli1 As String = "",
                                          Optional _DesCli2 As String = "", Optional _A As String = "",
                                          Optional _B As String = "", Optional _C As String = "",
                                          Optional _D As String = "", Optional _E As String = "",
                                          Optional _F As String = "", Optional _G As String = "",
                                          Optional _H As String = "", Optional _CodCon As String = "",
                                          Optional _FecLim As String = "", Optional _Imgqr As String = "",
                                          Optional _Alm As String = "", Optional _Numi2 As String = "")
        Dim Sql As String
        Try
            Sql = IIf(_Fecha.Equals(""), "", "fvafec = '" + _Fecha + "', ") +
              IIf(_Nfact.Equals(""), "", "fvanfac = " + _Nfact + ", ") +
              IIf(_NAutoriz.Equals(""), "", "fvaautoriz = " + _NAutoriz + ", ") +
              IIf(_Est.Equals(""), "", "fvaest = " + _Est) +
              IIf(_NitCli.Equals(""), "", "fvanitcli = '" + _NitCli + "', ") +
              IIf(_CodCli.Equals(""), "", "fvacodcli = " + _CodCli + ", ") +
              IIf(_DesCli1.Equals(""), "", "fvadescli1 = '" + _DesCli1 + "', ") +
              IIf(_DesCli2.Equals(""), "", "fvadescli2 = '" + _DesCli2 + "', ") +
              IIf(_A.Equals(""), "", "fvastot = " + _A + ", ") +
              IIf(_B.Equals(""), "", "fvaimpsi = " + _B + ", ") +
              IIf(_C.Equals(""), "", "fvaimpeo = " + _C + ", ") +
              IIf(_D.Equals(""), "", "fvaimptc = " + _D + ", ") +
              IIf(_E.Equals(""), "", "fvasubtotal = " + _E + ", ") +
              IIf(_F.Equals(""), "", "fvadesc = " + _F + ", ") +
              IIf(_G.Equals(""), "", "fvatotal = " + _G + ", ") +
              IIf(_H.Equals(""), "", "fvadebfis = " + _H + ", ") +
              IIf(_CodCon.Equals(""), "", "fvaccont = '" + _CodCon + "', ") +
              IIf(_FecLim.Equals(""), "", "fvaflim = '" + _FecLim + "', ") +
              IIf(_Imgqr.Equals(""), "", "fvaimgqr = '" + _Imgqr + "', ") +
              IIf(_Alm.Equals(""), "", "fvaalm = " + _Alm + ", ") +
              IIf(_Numi2.Equals(""), "", "fvanumi2 = " + _Numi2 + ", ")
            Sql = Sql.Trim
            If (Sql.Substring(Sql.Length - 1, 1).Equals(",")) Then
                Sql = Sql.Substring(0, Sql.Length - 1)
            End If

            D_Modificar_Datos("TFV001", Sql, Where)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Shared Sub L_Grabar_Factura_Detalle(_Numi As String, _CodProd As String, _DescProd As String, _Cant As String, _Precio As String, _Numi2 As String)
        Dim Sql As String
        Try
            Sql = _Numi + ", '" + _CodProd + "', '" + _DescProd + "', " + _Cant + ", " + _Precio + ", " + _Numi2

            D_Insertar_Datos("TFV0011", Sql)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Shared Function L_Reporte_Factura(_Numi As String, _Numi2 As String) As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        _Where = " fvanumi = " + _Numi + " and fvanumi = " + _Numi2

        _Tabla = D_Datos_Tabla("*", "VR_GO_Factura", _Where)
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function
    Public Shared Function L_Reporte_FacturaCombustible(_Numi As String, _Numi2 As String) As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        _Where = " fvanumi = " + _Numi + " and fvanumi = " + _Numi2

        _Tabla = D_Datos_Tabla("*", "VR_GO_FACTURA_COMBUSTIBLE", _Where)
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function
    ''VR_MAM_FacturaServicio
    Public Shared Function L_Reporte_FacturaServicio(_Numi As String, _Numi2 As String) As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        _Where = " fvanumi = " + _Numi + " and fvanumi = " + _Numi2

        _Tabla = D_Datos_Tabla("*", "VR_MAM_FacturaServicio", _Where)
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function


    Public Shared Function L_Reporte_Factura_Cia(_Cia As String) As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        _Where = " scnumi = " + _Cia

        _Tabla = D_Datos_Tabla("*", "TS003", _Where)
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function

    Public Shared Function L_ObtenerRutaImpresora(_NroImp As String, Optional tImp As String = "") As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        If (Not _NroImp.Trim.Equals("")) Then
            _Where = " cbnumi = " + _NroImp + " and cbest = 1 order by cbnumi"
        Else
            _Where = " cbtimp = " + tImp + " and cbest = 1 order by cbnumi"
        End If
        _Tabla = D_Datos_Tabla("*", "TC002", _Where)
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function

    Public Shared Function L_fnGetIVA() As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String
        _Where = "1 = 1"
        _Tabla = D_Datos_Tabla("scdebfis", "TS003", _Where)
        Return _Tabla
    End Function

    Public Shared Function L_fnGetICE() As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String
        _Where = "1 = 1"
        _Tabla = D_Datos_Tabla("scice", "TS003", _Where)
        Return _Tabla
    End Function

    Public Shared Sub L_Grabar_Nit(_Nit As String, _Nom1 As String, _Nom2 As String)
        Dim _Err As Boolean
        Dim _Nom01, _Nom02 As String
        Dim Sql As String
        _Nom01 = ""
        _Nom02 = ""
        L_Validar_Nit(_Nit, _Nom01, _Nom02)

        If _Nom01 = "" Then
            Sql = "'" + _Nit + "', '" + _Nom1 + "', '" + _Nom2 + "'"
            _Err = D_Insertar_Datos("TS001", Sql)
        Else
            If (_Nom1 <> _Nom01) Or (_Nom2 <> _Nom02) Then
                Sql = "sanom1 = '" + _Nom1 + "' " +
                      IIf(_Nom02.ToString.Trim.Equals(""), "", ", sanom2 = '" + _Nom2 + "', ")
                _Err = D_Modificar_Datos("TS001", Sql, "sanit = '" + _Nit + "'")
            End If
        End If

    End Sub

    Public Shared Sub L_Validar_Nit(_Nit As String, ByRef _Nom1 As String, ByRef _Nom2 As String)
        Dim _Tabla As DataTable

        _Tabla = D_Datos_Tabla("*", "TS001", "sanit = '" + _Nit + "'")

        If _Tabla.Rows.Count > 0 Then
            _Nom1 = _Tabla.Rows(0).Item(2)
            _Nom2 = IIf(_Tabla.Rows(0).Item(3).ToString.Trim.Equals(""), "", _Tabla.Rows(0).Item(3))
        End If
    End Sub

    Public Shared Function L_Eliminar_Nit(_Nit As String) As Boolean
        Dim res As Boolean = False
        Try
            res = D_Eliminar_Datos("TS001", "sanit = " + _Nit)
        Catch ex As Exception
            res = False
        End Try
        Return res
    End Function

    Public Shared Function L_Dosificacion(_cia As String, _alm As String, _fecha As String) As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        _fecha = Now.Date.ToString("yyyy/MM/dd")
        _Where = "sbcia = " + _cia + " AND sbalm = " + _alm + " AND sbfdel <= '" + _fecha + "' AND sbfal >= '" + _fecha + "' AND sbest = 1"

        _Tabla = D_Datos_Tabla("*", "TS002", _Where)
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function
    Public Shared Function L_DosificacionCajas(_cia As String, _alm As String, _fecha As String, _NroCaja As String) As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        _fecha = Now.Date.ToString("yyyy/MM/dd")
        _Where = "sbcia = " + _cia + " AND sbalm = " + _alm + " AND sbsfc = " + _NroCaja + " AND sbfdel <= '" + _fecha + "' AND sbfal >= '" + _fecha + "' AND sbest = 1"

        _Tabla = D_Datos_Tabla("*", "TS002", _Where)
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function
    Public Shared Function L_DosificacionReImprimir(_cia As String, _alm As String, _fecha As String, _NroAut As String) As DataSet
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        _fecha = Now.Date.ToString("yyyy/MM/dd")
        _Where = "sbcia = " + _cia + " AND sbalm = " + _alm + " AND sbautoriz = " + _NroAut + " AND sbfdel <= '" + _fecha + "' AND sbfal >= '" + _fecha + "' "

        _Tabla = D_Datos_Tabla("*", "TS002", _Where)
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function

    Public Shared Sub L_Actualiza_Dosificacion(_Numi As String, _NumFac As String, _Numi2 As String)
        Dim _Err As Boolean
        Dim Sql, _where As String
        Sql = "sbnfac = " + _NumFac
        _where = "sbnumi = " + _Numi

        _Err = D_Modificar_Datos("TS002", Sql, _where)
    End Sub

    Public Shared Function L_fnObtenerMaxIdTabla(tabla As String, campo As String, where As String) As Long
        Dim Dt As DataTable = New DataTable
        Dt = D_Maximo(tabla, campo, where)

        If (Dt.Rows.Count > 0) Then
            If (Dt.Rows(0).Item(0).ToString.Equals("")) Then
                Return 0
            Else
                Return CLng(Dt.Rows(0).Item(0).ToString)
            End If
        Else
            Return 0
        End If
    End Function

    Public Shared Function L_fnObtenerTabla(tabla As String, campo As String, where As String) As DataTable
        Dim Dt As DataTable = D_Datos_Tabla(campo, tabla, where)
        Return Dt
    End Function

    Public Shared Function L_fnObtenerDatoTabla(tabla As String, campo As String, where As String) As String
        Dim Dt As DataTable = D_Datos_Tabla(campo, tabla, where)
        If (Dt.Rows.Count > 0) Then
            Return Dt.Rows(0).Item(campo).ToString
        Else
            Return ""
        End If
    End Function

    Public Shared Function L_fnEliminarDatos(Tabla As String, Where As String) As Boolean
        Return D_Eliminar_Datos(Tabla, Where)
    End Function

    Public Shared Function TraerEstadoFacturas() As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 25))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)
        Return _Tabla
    End Function

    Public Shared Function cambiarEstadoEmision(codigo As Integer, fecha As String, factura As Integer, codigoRecepcion As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 26))
        _listParam.Add(New Datos.DParametro("@tanumi", codigo))
        _listParam.Add(New Datos.DParametro("@taven", factura))
        _listParam.Add(New Datos.DParametro("@tcentregado", codigoRecepcion))
        _listParam.Add(New Datos.DParametro("@tafvcr", fecha))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)
        Return _Tabla
    End Function

#End Region

#Region "Anular Factura"

    Public Shared Function L_Obtener_Facturas() As DataSet
        Dim _Tabla1 As New DataTable
        Dim _Tabla2 As New DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        _Where = " 1 = 1"
        'Cambiar la logica para visualizar las facturas esto en el programa de facturas
        _Tabla1 = D_Datos_Tabla("concat(fvanfac, '_', fvaautoriz) as Archivo, fvanumi as Codigo, fvanfac as [Nro Factura], " _
                                + "fvafec as Fecha, fvacodcli as [Cod Cliente], " _
                                + " fvadescli1 as [Nombre 1], fvadescli2 as [Nombre 2], fvanitcli as Nit, " _
                                + " fvastot as Subtotal, fvadesc as Descuento, fvatotal as Total, " _
                                + " fvaccont as [Cod Control], fvaflim as [Fec Limite],fvaalm as Almacen, fvcuf as CUF, fvaest as Estado",
                                "TFV001", _Where)
        '_Tabla1.Columns(0).ColumnMapping = MappingType.Hidden
        _Ds.Tables.Add(_Tabla1)

        _Tabla2 = D_Datos_Tabla("concat(fvanfac, '_', fvaautoriz) as Archivo, fvbnumi as Codigo, fvbcprod as [Cod Producto], fvbdesprod as Descripcion, " _
                                + " fvbcant as Cantidad, fvbprecio as [Precio Unitario], (fvbcant * fvbprecio) as Precio",
                                "TFV001, TFV0011", "fvanumi = fvbnumi and fvanumi2 = fvbnumi2")
        _Ds.Tables.Add(_Tabla2)
        _Ds.Relations.Add("1", _Tabla1.Columns("Archivo"), _Tabla2.Columns("Archivo"), False)
        Return _Ds
    End Function

    Public Shared Function L_ObtenerDetalleFactura(_CodFact As String) As DataSet 'Modifcar para que solo Traiga los productos Con Stock
        Dim _Tabla As DataTable
        Dim _Ds As New DataSet
        Dim _Where As String
        _Where = "fvbnumi = " + _CodFact
        _Tabla = D_Datos_Tabla("fvbcprod as codP, fvbcant as can, '1' as sto", "TFV0011", _Where)
        _Ds.Tables.Add(_Tabla)
        Return _Ds
    End Function

#End Region

#Region "Libro de ventas"
    Public Shared Function L_fnObtenerLibroVentaAmbosTipoFactura(_CodAlm As String, _fechai As String, _FechaF As String, TipoFactura As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String = ""

        If _CodAlm > 0 Then
            If (TipoFactura = 1) Then
                _Where = "sbcia=1 and fvaalm = " + _CodAlm + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' " + " ORDER BY fvanfac"

            End If
            If (TipoFactura = 2) Then
                _Where = "sbcia=2 and fvaalm = " + _CodAlm + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' " + " ORDER BY fvanfac"

            End If
            If (TipoFactura = 3) Then
                _Where = " fvaalm = " + _CodAlm + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' " + " ORDER BY fvanfac"

            End If
        End If
        If _CodAlm = 0 Then 'todas las sucursales
            If (TipoFactura = 1) Then
                _Where = "sbcia=1 and  fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' " + " ORDER BY fvanfac"

            End If
            If (TipoFactura = 2) Then
                _Where = "sbcia=2 and  fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' " + " ORDER BY fvanfac"

            End If
            If (TipoFactura = 3) Then
                _Where = " fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' " + " ORDER BY fvanfac"

            End If


        End If
        If _CodAlm = -1 Then 'todas las sucursales menos la principal
            If (TipoFactura = 1) Then

                _Where = "sbcia=1 and fvaalm <>1 " + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' " + " ORDER BY fvanfac"

            End If
            If (TipoFactura = 2) Then
                _Where = "sbcia=2 and fvaalm <>1 " + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' " + " ORDER BY fvanfac"

            End If
            If (TipoFactura = 3) Then
                _Where = "fvaalm <>1 " + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' " + " ORDER BY fvanfac"

            End If


        End If

        Dim _select As String = "fvanumi, FORMAT(fvafec,'dd/MM/yyyy') as fvafec, fvanfac, fvaautoriz,fvaest, fvanitcli, fvadescli, fvastot, fvaimpsi, fvaimpeo, fvaimptc, fvasubtotal, fvadesc, fvatotal, fvadebfis, fvaccont,fvaflim,fvaalm,scneg, factura"

        _Tabla = D_Datos_Tabla(_select,
                               "VR_GO_LibroVenta2", _Where)
        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerLibroVenta(_CodAlm As String, _Mes As String, _Anho As String) As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String = "fvaalm = " + _CodAlm + "and Month(fvafec) = " + _Mes + " and Year(fvafec) = " + _Anho + " ORDER BY fvanfac"
        _Tabla = D_Datos_Tabla("*",
                               "VR_GO_LibroVenta", _Where)
        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerLibroVenta2(_CodAlm As String, _fechai As String, _FechaF As String, factura As Integer, TipoFactura As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String = ""
        If _CodAlm > 0 Then
            If (TipoFactura = 1) Then

                _Where = "sbcia=1 and fvaalm = " + _CodAlm + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' and factura=" + Str(factura) + " ORDER BY fvanfac"

            End If
            If (TipoFactura = 2) Then
                _Where = "sbcia=2 and fvaalm = " + _CodAlm + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' and factura=" + Str(factura) + " ORDER BY fvanfac"

            End If
            If (TipoFactura = 3) Then
                _Where = " fvaalm = " + _CodAlm + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' and factura=" + Str(factura) + " ORDER BY fvanfac"

            End If



        End If
        If _CodAlm = 0 Then 'todas las sucursales

            If (TipoFactura = 1) Then


                _Where = "sbcia=1 and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' and factura=" + Str(factura) + " ORDER BY fvanfac"

            End If
            If (TipoFactura = 2) Then
                _Where = "sbcia=2 and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' and factura=" + Str(factura) + " ORDER BY fvanfac"

            End If
            If (TipoFactura = 3) Then
                _Where = "fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' and factura=" + Str(factura) + " ORDER BY fvanfac"

            End If

        End If
        If _CodAlm = -1 Then 'todas las sucursales menos la principal
            If (TipoFactura = 1) Then


                _Where = "sbcia=1 and fvaalm <>1 " + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' and factura=" + Str(factura) + " ORDER BY fvanfac"
            End If
            If (TipoFactura = 2) Then
                _Where = "sbcia=2 and fvaalm <>1 " + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' and factura=" + Str(factura) + " ORDER BY fvanfac"

            End If
            If (TipoFactura = 3) Then
                _Where = "fvaalm <>1 " + " and fvafec >= '" + _fechai + "' and fvafec <= '" + _FechaF + "' and factura=" + Str(factura) + " ORDER BY fvanfac"

            End If


        End If
        Dim _select As String = "fvanumi, FORMAT(fvafec,'dd/MM/yyyy') as fvafec, fvanfac, fvaautoriz,fvaest, fvanitcli, fvadescli, fvastot, fvaimpsi, fvaimpeo, fvaimptc, fvasubtotal, fvadesc, fvatotal, fvadebfis, fvaccont,fvaflim,fvaalm,scneg, factura"

        _Tabla = D_Datos_Tabla(_select,
                               "VR_GO_LibroVenta2", _Where)
        Return _Tabla
    End Function

    Public Shared Function L_ObtenerAnhoFactura() As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String = "1 = 1 ORDER BY year(fvafec)"
        _Tabla = D_Datos_Tabla("Distinct(year(fvafec)) AS anho",
                               "VR_GO_LibroVenta", _Where)
        Return _Tabla
    End Function

    Public Shared Function L_ObtenerSucursalesFactura() As DataTable
        Dim _Tabla As DataTable
        Dim _Where As String = "1 = 1 ORDER BY a.scneg"
        _Tabla = D_Datos_Tabla("a.scnumi, a.scneg, a.scnit",
                               "TS003 a", _Where)
        Return _Tabla
    End Function

#End Region

#Region "COBROS DE LAS VENTAS"
    Public Shared Function L_fnObtenerLasVentasACredito() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@tduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerLosPagos(_numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@tduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@credito", _numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerLasVentasCreditoPorCliente(_numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        '_listParam.Add(New Datos.DParametro("@tduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@tenumi", _numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121Cheque", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnObtenerLasVentasCreditoPorVendedorFecha(_numi As Integer, _fecha As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 11))
        '_listParam.Add(New Datos.DParametro("@tduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@tenumi", _numi))
        _listParam.Add(New Datos.DParametro("@tefdoc", _fecha))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121Cheque", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnGrabarPagos(_numi As String, dt As DataTable) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@tduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@tdnumi", _numi))
        _listParam.Add(New Datos.DParametro("@TV00121", "", dt))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnEliminarPago(numi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TV00121", "tdnumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@tdnumi", numi))
            _listParam.Add(New Datos.DParametro("@tduact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
#End Region

#Region "COBROS DE LAS VENTAS CON CHEQUE"
    Public Shared Function L_fnCobranzasGeneral() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121Cheque", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnCobranzasObtenerLosPagos(_numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@tdnumi", _numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121Cheque", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnCobranzasObtenerLasVentasACredito() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121Cheque", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnCobranzasDetalle(_numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@tenumi", _numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121Cheque", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnCobranzasReporte(_numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@tenumi", _numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121Cheque", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnCobranzasObtenerProductosCredito(idVenta As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@idVenta", idVenta))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121Cheque", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnEliminarCobranza(numi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TV0013", "tenumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@tenumi", numi))
            _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121Cheque", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function

    Public Shared Function L_fnGrabarCobranza(_tenumi As String, _tefdoc As String, _tety4vend As Integer, _teobs As String,
                                              detalle As DataTable, _Nrocaja As Integer) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@tenumi", _tenumi))
        _listParam.Add(New Datos.DParametro("@tefdoc", _tefdoc))
        _listParam.Add(New Datos.DParametro("@tety4vend", _tety4vend))
        _listParam.Add(New Datos.DParametro("@teobs", _teobs))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@teNrocaja", _Nrocaja))
        '_listParam.Add(New Datos.DParametro("@TV00121", "", detalle))
        _listParam.Add(New Datos.DParametro("@TV00122", "", detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121Cheque", _listParam)
        If _Tabla.Rows.Count > 0 Then
            _tenumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnGrabarCobranza2(_tenumi As String, _tefdoc As String, _tety4vend As Integer, _teobs As String, detalle As DataTable) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '   @canumi ,@caalm,@cafdoc ,@caty4prov  ,@catven,
        '@cafvcr,@camon ,@caest  ,@caobs ,@cadesc ,@newFecha,@newHora,@cauact
        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@tenumi", _tenumi))
        _listParam.Add(New Datos.DParametro("@tefdoc", _tefdoc))
        _listParam.Add(New Datos.DParametro("@tety4vend", _tety4vend))
        _listParam.Add(New Datos.DParametro("@teobs", _teobs))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TV00121", "", detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121Cheque", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _tenumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnModificarCobranza(_tenumi As String, _tefdoc As String, _tety4vend As Integer, _teobs As String, detalle As DataTable) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '   @canumi ,@caalm,@cafdoc ,@caty4prov  ,@catven,
        '@cafvcr,@camon ,@caest  ,@caobs ,@cadesc ,@newFecha,@newHora,@cauact
        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@tenumi", _tenumi))
        _listParam.Add(New Datos.DParametro("@tefdoc", _tefdoc))
        _listParam.Add(New Datos.DParametro("@tety4vend", _tety4vend))
        _listParam.Add(New Datos.DParametro("@teobs", _teobs))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TV00121", "", detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121Cheque", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _tenumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnVerificarSiSeContabilizoPagoVenta(_tenumi As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@tenumi", _tenumi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV00121Cheque", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
#End Region

#Region "PAGOS DE LAS COMPRAS CON CHEQUE"
    Public Shared Function L_fnCobranzasGeneralCompra() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC00121Cheque", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarPagosCompras(_credito As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@credito", _credito))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC00121Cheque", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnCobranzasObtenerLosPagosCompra(_numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@tdnumi", _numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC00121Cheque", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnCobranzasObtenerLasVentasACreditoCompras() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC00121Cheque", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnCobranzasDetalleCompras(_numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@tenumi", _numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC00121Cheque", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnCobranzasReporteCompras(_numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@tenumi", _numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC00121Cheque", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnEliminarCobranzaCompras(numi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TC0013", "tenumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@tenumi", numi))
            _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TC00121Cheque", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
    '@tenumi ,@tefdoc,@tety4vend ,@teobs ,@newFecha ,@newHora ,@teuact 
    Public Shared Function L_fnGrabarCobranzaCompras(_tenumi As String, _tefdoc As String, _tety4vend As Integer, _teobs As String, detalle As DataTable) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '   @canumi ,@caalm,@cafdoc ,@caty4prov  ,@catven,
        '@cafvcr,@camon ,@caest  ,@caobs ,@cadesc ,@newFecha,@newHora,@cauact
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@tenumi", _tenumi))
        _listParam.Add(New Datos.DParametro("@tefdoc", _tefdoc))
        _listParam.Add(New Datos.DParametro("@tety4vend", _tety4vend))
        _listParam.Add(New Datos.DParametro("@teobs", _teobs))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TC00121", "", detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC00121Cheque", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _tenumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnModificarCobranzaCompras(_tenumi As String, _tefdoc As String, _tety4vend As Integer, _teobs As String, detalle As DataTable) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '   @canumi ,@caalm,@cafdoc ,@caty4prov  ,@catven,
        '@cafvcr,@camon ,@caest  ,@caobs ,@cadesc ,@newFecha,@newHora,@cauact
        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@tenumi", _tenumi))
        _listParam.Add(New Datos.DParametro("@tefdoc", _tefdoc))
        _listParam.Add(New Datos.DParametro("@tety4vend", _tety4vend))
        _listParam.Add(New Datos.DParametro("@teobs", _teobs))
        _listParam.Add(New Datos.DParametro("@teuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TC00121", "", detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC00121Cheque", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _tenumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnVerificarSiSeContabilizoPagoCompra(_tenumi As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@tenumi", _tenumi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC00121Cheque", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

#End Region

#Region "COBROS DE LAS COMPRAS"
    Public Shared Function L_fnObtenerLasComprasACredito() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@tduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC00121", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerLosPagosComprasCreditos(_numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@tduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@credito", _numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC00121", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnGrabarPagosCompraCredito(_numi As String, dt As DataTable) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@tduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@tdnumi", _numi))
        _listParam.Add(New Datos.DParametro("@TC00121", "", dt))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC00121", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnEliminarPagoCompraCredito(numi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TC00121", "tdnumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@tdnumi", numi))
            _listParam.Add(New Datos.DParametro("@tduact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TC00121", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
#End Region

#Region "TI002 MOVIMIENTOS "
    Public Shared Function L_fnGeneralMovimiento() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnGeneralGrupoCanero() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TG001", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_fnDetalleMovimiento(_ibid As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@ibid", _ibid))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnDetalleGrupoEconomico(_ibid As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@id", _ibid))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_Tg001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnResetearTI001(deposito As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 35))
        _listParam.Add(New Datos.DParametro("@depositoInventario", deposito))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnObtenerCabezeraCompras(Deposito As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 36))
        _listParam.Add(New Datos.DParametro("@depositoInventario", Deposito))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnObtenerCabezeraVentas(Deposito As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 39))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@depositoInventario", Deposito))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerTI0021(Deposito As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 43))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@depositoInventario", Deposito))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnEliminandoTI0021EgresoIngreso(deposito As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 44))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@depositoInventario", deposito))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObteniendoDetalleCompra(numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 37))
        _listParam.Add(New Datos.DParametro("@numi", numi))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnObteniendoDetalleVentas(numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 40))
        _listParam.Add(New Datos.DParametro("@numi", numi))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function



    Public Shared Function L_fnObteniendoSaldosTI001(producto As Integer, deposito As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 41))
        _listParam.Add(New Datos.DParametro("@cbty5prod", producto))
        _listParam.Add(New Datos.DParametro("@depositoInventario", deposito))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObteniendoCompraAnterior(producto As Integer, deposito As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 38))
        _listParam.Add(New Datos.DParametro("@producto", producto))
        _listParam.Add(New Datos.DParametro("@tanumi", deposito))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prMovimientoConcepto() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarSucursalesMovimiento() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prMovimientoListarProductos(dt As DataTable, _deposito As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _deposito))
        _Tabla = D_ProcedimientoConParam("sp_Marco_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prMovimientoListarProductosConLote(_deposito As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 31))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _deposito))
        _Tabla = D_ProcedimientoConParam("sp_Marco_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarLotesPorProductoMovimiento(_almacen As Integer, _codproducto As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 32))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@producto", _codproducto))
        _listParam.Add(New Datos.DParametro("ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prMovimientoChoferABMDetalle(numi As String, Type As Integer, detalle As DataTable) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", Type))
        _listParam.Add(New Datos.DParametro("@ibid", numi))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TI0021", "", detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prInsertarTi0021(numi As String, Type As Integer, detalle As DataTable, deposito As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", Type))
        _listParam.Add(New Datos.DParametro("@ibid", numi))
        _listParam.Add(New Datos.DParametro("@depositoInventario", deposito))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TI0021", "", detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_prMovimientoChoferGrabar(ByRef _ibid As String, _ibfdoc As String, _ibconcep As Integer, _ibobs As String, _almacen As Integer, _depositoDestino As Integer, _ibidOrigen As Integer, _detalle As DataTable) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@ibid", _ibid))
        _listParam.Add(New Datos.DParametro("@ibfdoc", _ibfdoc))
        _listParam.Add(New Datos.DParametro("@ibconcep", _ibconcep))
        _listParam.Add(New Datos.DParametro("@ibobs", _ibobs))
        _listParam.Add(New Datos.DParametro("@ibest", 1))
        _listParam.Add(New Datos.DParametro("@ibalm", _almacen))
        _listParam.Add(New Datos.DParametro("@ibdepdest", _depositoDestino))
        _listParam.Add(New Datos.DParametro("@ibiddc", 0))
        _listParam.Add(New Datos.DParametro("@ibidOrigen", _ibidOrigen))

        _listParam.Add(New Datos.DParametro("@TI0021", "", _detalle))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)
        If _Tabla.Rows.Count > 0 Then
            _ibid = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function


    Public Shared Function L_prGrabarTI002(obs As String, deposito As Integer,
                    cbnumi As Integer, fact As Date, hact As String, uact As String, cbty5prod As Integer,
                    cbcmin As Double, cblote As String, cbfechavenc As Date) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable

        Try
            Dim _listParam As New List(Of Datos.DParametro)
            _listParam.Add(New Datos.DParametro("@tipo", 38))
            _listParam.Add(New Datos.DParametro("@obs", obs))
            _listParam.Add(New Datos.DParametro("@depositoInventario", deposito))
            _listParam.Add(New Datos.DParametro("@cbnumi", cbnumi))
            _listParam.Add(New Datos.DParametro("@fact", fact))
            _listParam.Add(New Datos.DParametro("@hact", hact))
            _listParam.Add(New Datos.DParametro("@uact", uact))
            _listParam.Add(New Datos.DParametro("@cbty5prod", cbty5prod))
            _listParam.Add(New Datos.DParametro("@cbcmin", cbcmin))
            _listParam.Add(New Datos.DParametro("@cblote", cblote))
            _listParam.Add(New Datos.DParametro("@cbfechavenc", cbfechavenc))
            '_listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
            _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)
            If _Tabla.Rows.Count > 0 Then

                _resultado = True
            Else
                _resultado = False
            End If
        Catch ex As Exception
            Dim mens As String = ex.Message
        End Try



        Return _resultado
    End Function

    Public Shared Function L_prGrabarTI002Venta(obs As String, deposito As Integer,
                    cbnumi As Integer, fact As Date, hact As String, uact As String, cbty5prod As Integer,
                    cbcmin As Double, cblote As String, cbfechavenc As Date) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable

        Try
            Dim _listParam As New List(Of Datos.DParametro)
            _listParam.Add(New Datos.DParametro("@tipo", 42))
            _listParam.Add(New Datos.DParametro("@obs", obs))
            _listParam.Add(New Datos.DParametro("@depositoInventario", deposito))
            _listParam.Add(New Datos.DParametro("@cbnumi", cbnumi))
            _listParam.Add(New Datos.DParametro("@fact", fact))
            _listParam.Add(New Datos.DParametro("@hact", hact))
            _listParam.Add(New Datos.DParametro("@uact", uact))
            _listParam.Add(New Datos.DParametro("@cbty5prod", cbty5prod))
            _listParam.Add(New Datos.DParametro("@cbcmin", cbcmin))
            _listParam.Add(New Datos.DParametro("@cblote", cblote))
            _listParam.Add(New Datos.DParametro("@cbfechavenc", cbfechavenc))
            '_listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
            _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)
            If _Tabla.Rows.Count > 0 Then

                _resultado = True
            Else
                _resultado = False
            End If
        Catch ex As Exception
            Dim mens As String = ex.Message
        End Try



        Return _resultado
    End Function

    Public Shared Function L_prMovimientoModificar(ByRef _ibid As String, _ibfdoc As String, _ibconcep As Integer, _ibobs As String, _almacen As Integer, _detalle As DataTable, _ibcont As Integer) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@ibid", _ibid))
        _listParam.Add(New Datos.DParametro("@ibfdoc", _ibfdoc))
        _listParam.Add(New Datos.DParametro("@ibconcep", _ibconcep))
        _listParam.Add(New Datos.DParametro("@ibobs", _ibobs))
        _listParam.Add(New Datos.DParametro("@ibest", 1))
        _listParam.Add(New Datos.DParametro("@ibalm", _almacen))
        _listParam.Add(New Datos.DParametro("@ibiddc", 0))
        _listParam.Add(New Datos.DParametro("@ibcont", _ibcont))

        _listParam.Add(New Datos.DParametro("@TI0021", "", _detalle))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _ibid = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prMovimientoEliminar(numi As String) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", -1))
        _listParam.Add(New Datos.DParametro("@ibid", numi))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnListarProductosKardex(_almacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarLotesProductos(codproducto As Integer, _almacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 28))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@producto", codproducto))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerSaldoProducto(_almacen As Integer, _codProducto As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 23))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@producto", _codProducto))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnObtenerHistorialProducto(_codProducto As Integer, FechaI As String, FechaF As String, _almacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@producto", _codProducto))
        _listParam.Add(New Datos.DParametro("@fechaI", FechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", FechaF))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerHistorialProductoporLote(_codProducto As Integer, FechaI As String, FechaF As String, _almacen As Integer, _Lote As String, _FechaVenc As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 30))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@producto", _codProducto))
        _listParam.Add(New Datos.DParametro("@fechaI", FechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", FechaF))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@lote", _Lote))
        _listParam.Add(New Datos.DParametro("@fechaVenc", _FechaVenc))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnObtenerKardexPorProducto(_codProducto As Integer, FechaI As String, FechaF As String, _almacen As Integer, _linea As String, _casa As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 25))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@producto", _codProducto))
        _listParam.Add(New Datos.DParametro("@fechaI", FechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", FechaF))
        _listParam.Add(New Datos.DParametro("@linea", _linea))
        _listParam.Add(New Datos.DParametro("@casa", _casa))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerProductoConMovimiento(FechaI As String, FechaF As String, _almacen As Integer, _linea As String, _casa As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 26))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@fechaI", FechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", FechaF))
        _listParam.Add(New Datos.DParametro("@linea", _linea))
        _listParam.Add(New Datos.DParametro("@casa", _casa))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnObtenerKardexGeneralProductos(FechaI As String, FechaF As String, _almacen As Integer, _linea As String, _casa As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 27))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@fechaI", FechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", FechaF))
        _listParam.Add(New Datos.DParametro("@linea", _linea))
        _listParam.Add(New Datos.DParametro("@casa", _casa))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerKardexGeneralProductosporLote(FechaI As String, FechaF As String, _almacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 33))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@fechaI", FechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", FechaF))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnObtenerHistorialProductoGeneral(_codProducto As Integer, FechaI As String, _almacen As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 20))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@producto", _codProducto))
        _listParam.Add(New Datos.DParametro("@fechaI", FechaI))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function



    Public Shared Function L_fnObtenerHistorialProductoGeneralPorLote(_codProducto As Integer, FechaI As String, _almacen As Integer, _Lote As String, FechaVenc As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 29))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@producto", _codProducto))
        _listParam.Add(New Datos.DParametro("@fechaI", FechaI))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@lote", _Lote))
        _listParam.Add(New Datos.DParametro("@fechaVenc", FechaVenc))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnActualizarSaldo(_Almacen As Integer, _CodProducto As String, _Cantidad As Double) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 21))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@producto", _CodProducto))
        _listParam.Add(New Datos.DParametro("@almacen", _Almacen))
        _listParam.Add(New Datos.DParametro("@cantidad", _Cantidad))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnStockActual() As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 22))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnStockActualLote() As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 34))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TI002", _listParam)

        Return _Tabla
    End Function
#End Region

#Region "ROLES CORRECTO"

    Public Shared Function L_prRolGeneral() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_ZY002", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prRolDetalleGeneral(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@ybnumi", _numi))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_ZY002", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_prRolGrabar(ByRef _numi As String, _rol As String, _detalle As DataTable) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@ybnumi", _numi))
        _listParam.Add(New Datos.DParametro("@ybrol", _rol))
        _listParam.Add(New Datos.DParametro("@ZY0021", "", _detalle))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_ZY002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _numi = _Tabla.Rows(0).Item(0)
            _resultado = True
            'L_prTipoCambioGrabarHistorial(_numi, _fecha, _dolar, _ufv, "TIPO DE CAMBIO", 1)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prRolModificar(_numi As String, _rol As String, _detalle As DataTable) As Boolean
        Dim _resultado As Boolean

        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@ybnumi", _numi))
        _listParam.Add(New Datos.DParametro("@ybrol", _rol))
        _listParam.Add(New Datos.DParametro("@ZY0021", "", _detalle))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_dg_ZY002", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = True
            'L_prTipoCambioGrabarHistorial(_numi, _fecha, _dolar, _ufv, "TIPO DE CAMBIO", 2)
        Else
            _resultado = False
        End If

        Return _resultado
    End Function



    Public Shared Function L_prRolBorrar(_numi As String, ByRef _mensaje As String) As Boolean

        Dim _resultado As Boolean

        If L_fnbValidarEliminacion(_numi, "ZY002", "ybnumi", _mensaje) = True Then
            Dim _Tabla As DataTable

            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@ybnumi", _numi))
            _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_dg_ZY002", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
                'L_prTipoCambioGrabarHistorial(_numi, _fecha, _dolar, _ufv, "TIPO DE CAMBIO", 3)
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If

        Return _resultado
    End Function



#End Region
#Region "REPORTES VENTAS"
    Public Shared Function L_BuscarVentasAtentidas(fechaI As String, fechaF As String, idAlmacen As Integer, idVendedor As Integer, idCliente As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@Vendedor", idVendedor))
        _listParam.Add(New Datos.DParametro("@Cliente", idCliente))
        _listParam.Add(New Datos.DParametro("@almacen", idAlmacen))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Sp_Mam_ReporteVentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_BuscarVentasCajerasProveedores(fechaI As String, fechaF As String, idAlmacen As Integer, idUsuario As Integer, idProveedor As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@usuario", idUsuario))
        _listParam.Add(New Datos.DParametro("@proveedor", idProveedor))
        _listParam.Add(New Datos.DParametro("@almacen", idAlmacen))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Sp_Mam_ReporteVentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_BuscarVentasCajerasProveedoresProductos(fechaI As String, fechaF As String, idAlmacen As Integer, idUsuario As Integer, idProveedor As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@usuario", idUsuario))
        _listParam.Add(New Datos.DParametro("@proveedor", idProveedor))
        _listParam.Add(New Datos.DParametro("@almacen", idAlmacen))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Sp_Mam_ReporteVentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_VentasProductos(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Sp_Mam_ReporteVentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_BuscarVentasCajerasProveedoresSinUsuario(fechaI As String, fechaF As String, idAlmacen As Integer, idProveedor As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@proveedor", idProveedor))
        _listParam.Add(New Datos.DParametro("@almacen", idAlmacen))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Sp_Mam_ReporteVentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_BuscarVentasCajerasProveedoresProductosSinUsuario(fechaI As String, fechaF As String, idAlmacen As Integer, idProveedor As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@proveedor", idProveedor))
        _listParam.Add(New Datos.DParametro("@almacen", idAlmacen))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Sp_Mam_ReporteVentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_DescuentoProductos() As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("Sp_Mam_ReporteVentas", _listParam)
        Return _Tabla
    End Function
#End Region
#Region "REPORTES VS VENTAS"


    Public Shared Function L_prVentasVsCostosGeneralAlmacenVendedor(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("Sp_Mam_ReporteVentasVsCostos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prVentasVsProductosGeneral(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("Sp_Mam_ReporteVentasVsCostos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prVentasVsProductosTodosALmacenesPrecio(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("Sp_Mam_ReporteVentasVsCostos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prVentasVsProductosUnaALmacenesPrecio(fechaI As String, fechaF As String,
                                                                   _almacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("Sp_Mam_ReporteVentasVsCostos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prVentasVsCostosPorVendedorTodosAlmacen(fechaI As String, fechaF As String, _numiVendedor As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@vendedor", _numiVendedor))
        _Tabla = D_ProcedimientoConParam("Sp_Mam_ReporteVentasVsCostos", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prVentasVsCostosTodosVendedorUnaAlmacen(fechaI As String, fechaF As String, _numiAlmacen As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _numiAlmacen))
        _Tabla = D_ProcedimientoConParam("Sp_Mam_ReporteVentasVsCostos", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prVentasVsCostosUnaVendedorUnaAlmacen(fechaI As String, fechaF As String, _numiAlmacen As String, _numiVendedor As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@uact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _numiAlmacen))
        _listParam.Add(New Datos.DParametro("@vendedor", _numiVendedor))
        _Tabla = D_ProcedimientoConParam("Sp_Mam_ReporteVentasVsCostos", _listParam)

        Return _Tabla
    End Function
#End Region
#Region "REPORTES GRAFICOS DE VENTAS"


    Public Shared Function L_prVentasGraficaVendedorMes(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasGraficas", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prVentasGraficaVendedorAlmacen(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasGraficas", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prVentasGraficaVendedorRendimiento(fechaI As String, fechaF As String, dt As DataTable) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@cliente", "", dt))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasGraficas", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prMovimientoProductoGeneral(CodAlmacen As String, Mes As String, Anho As String, dt As DataTable) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        If (CodAlmacen.Equals("50")) Then

            _listParam.Add(New Datos.DParametro("@tipo", 9))
            _listParam.Add(New Datos.DParametro("@Mes", Mes))
            _listParam.Add(New Datos.DParametro("@Anho", Anho))
            _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
            _listParam.Add(New Datos.DParametro("@Meses", "", dt))
        Else
            _listParam.Add(New Datos.DParametro("@tipo", 10))
            _listParam.Add(New Datos.DParametro("@Mes", Mes))
            _listParam.Add(New Datos.DParametro("@Anho", Anho))
            _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
            _listParam.Add(New Datos.DParametro("@almacen", CodAlmacen))
            _listParam.Add(New Datos.DParametro("@Meses", "", dt))
        End If

        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasGraficas", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prMovimientoProductoCantPorProducto(CodProducto As String, CodAlmacen As String, Mes As String, Anho As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        If (CodProducto.Equals("")) Then
            If (CodAlmacen.Equals("50")) Then
                _listParam.Add(New Datos.DParametro("@tipo", 8))
                _listParam.Add(New Datos.DParametro("@Mes", Mes))
                _listParam.Add(New Datos.DParametro("@Anho", Anho))
                _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
            Else
                _listParam.Add(New Datos.DParametro("@tipo", 12))
                _listParam.Add(New Datos.DParametro("@Mes", Mes))
                _listParam.Add(New Datos.DParametro("@almacen", CodAlmacen))
                _listParam.Add(New Datos.DParametro("@Anho", Anho))
                _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
            End If

        Else

            If (CodAlmacen.Equals("50")) Then
                _listParam.Add(New Datos.DParametro("@tipo", 6))
                _listParam.Add(New Datos.DParametro("@Mes", Mes))
                _listParam.Add(New Datos.DParametro("@Anho", Anho))
                _listParam.Add(New Datos.DParametro("@producto", CodProducto))
                _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
            Else
                _listParam.Add(New Datos.DParametro("@tipo", 11))
                _listParam.Add(New Datos.DParametro("@Mes", Mes))
                _listParam.Add(New Datos.DParametro("@Anho", Anho))
                _listParam.Add(New Datos.DParametro("@almacen", CodAlmacen))
                _listParam.Add(New Datos.DParametro("@producto", CodProducto))
                _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
            End If




        End If


        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasGraficas", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prMovimientoProductoObtenerImagenProducto(CodProducto As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@producto", CodProducto))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasGraficas", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prVentasGraficaListarVendedores() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasGraficas", _listParam)

        Return _Tabla
    End Function
#End Region

#Region "REPORTE DE CREDITOS"


    Public Shared Function L_prReporteCreditoGeneral(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_prReporteCreditoGeneralCompras(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 14))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteCreditoGeneralRes(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 111))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteCreditoClienteRes(fechaI As String, fechaF As String, codcli As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 112))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@cliente", codcli))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteCreditoResumen(fechaI As String, fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prListarPrecioVenta() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prListarPrecioCosto() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteUtilidad(_codAlmacen As Integer, _codCat As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _codAlmacen))
        _listParam.Add(New Datos.DParametro("@catPrecio", _codCat))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prReporteUtilidadal(_codAlmacen As Integer, _codCat As Integer, _date1 As Date) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 91))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _codAlmacen))
        _listParam.Add(New Datos.DParametro("@catPrecio", _codCat))
        _listParam.Add(New Datos.DParametro("@fechaf", _date1))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prReporteUtilidadmayor(_codAlmacen As Integer, _codCat As Integer, _date1 As Date) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 92))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _codAlmacen))
        _listParam.Add(New Datos.DParametro("@catPrecio", _codCat))
        _listParam.Add(New Datos.DParametro("@fechaf", _date1))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prReporteUtilidadConsolidado(_codAlmacen As Integer, _codCat As Integer, _date1 As Date) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 93))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _codAlmacen))
        _listParam.Add(New Datos.DParametro("@catPrecio", _codCat))
        _listParam.Add(New Datos.DParametro("@fechaf", _date1))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteUtilidadStockMayorCero(_codAlmacen As Integer, _codCat As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 13))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _codAlmacen))
        _listParam.Add(New Datos.DParametro("@catPrecio", _codCat))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteCreditoClienteTodosCuentas(fechaI As String, fechaF As String, _numiCliente As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@cliente", _numiCliente))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteCreditoProveedoresTodosCuentas(fechaI As String, fechaF As String, _numiCliente As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 15))
        _listParam.Add(New Datos.DParametro("@fechaI", fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", fechaF))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@cliente", _numiCliente))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prReporteCreditoClienteUnaCuentas(_numiCredito As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@codCredito", _numiCredito))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prReporteCreditoProveedorUnaCuentas(_numiCredito As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 16))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@codCredito", _numiCredito))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_prReporteCreditoListarCuentasPorCliente(_numiCliente As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@cliente", _numiCliente))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_prReporteCreditoListarCuentasPorProveedor(_numiCliente As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 18))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@cliente", _numiCliente))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarClienteCreditos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarProveedoresCreditos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 17))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnReporteMorosidadTodosAlmacenVendedor() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnReporteMorosidadTodosAlmacenUnVendedor(numiVendedor As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _listParam.Add(New Datos.DParametro("@vendedor", numiVendedor))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnReporteMorosidadUnAlmacenUnVendedor(numiVendedor As Integer, numialmacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 12))
        _listParam.Add(New Datos.DParametro("@vendedor", numiVendedor))
        _listParam.Add(New Datos.DParametro("@almacen", numialmacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteStockConsolidado(_codAlmacen As Integer, _codCat As Integer, _date1 As Date) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 19))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _codAlmacen))
        _listParam.Add(New Datos.DParametro("@catPrecio", _codCat))
        _listParam.Add(New Datos.DParametro("@fechaf", _date1))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteProductosStockMayorCero(_codAlmacen As Integer, _codCat As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 20))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _codAlmacen))
        _listParam.Add(New Datos.DParametro("@catPrecio", _codCat))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prReporteProductoMayorFecha(_codAlmacen As Integer, _codCat As Integer, _date1 As Date) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 21))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", _codAlmacen))
        _listParam.Add(New Datos.DParametro("@catPrecio", _codCat))
        _listParam.Add(New Datos.DParametro("@fechaf", _date1))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasCredito", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prListarEstadoCuentasClientesTotal(idCliente As Integer, fechai As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@cliente", idCliente))
        _listParam.Add(New Datos.DParametro("@fechai", fechai))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_EstadoCuentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_prListarEstadoCuentasCliente(idCliente As Integer, fechai As String, fechaf As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@cliente", idCliente))
        _listParam.Add(New Datos.DParametro("@fechai", fechai))
        _listParam.Add(New Datos.DParametro("@fechaf", fechaf))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_EstadoCuentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_prListarEstadoCuentasClienteTodos() As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_EstadoCuentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_prListarEstadoCuentasUnCliente(idCliente As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@cliente", idCliente))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_EstadoCuentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_prListarEstadoCuentasProveedoresTodos() As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_EstadoCuentas", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_prListarEstadoCuentasUnProveedor(idProveedor As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@prov", idProveedor))
        _listParam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_EstadoCuentas", _listParam)
        Return _Tabla
    End Function
#End Region

#Region "TP001 PROFORMA"
    Public Shared Function L_fnGeneralProforma() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TP001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnReporteProforma(_numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@panumi", _numi))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TP001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnReportecliente() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 12))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY004", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnReporteproducto() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY005", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnObtenerNroFactura() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 12))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TF001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnDetalleProforma(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@panumi", _numi))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TP001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarProductosProforma(_almacen As String, _cliente As String, _detalle As DataTable) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@cliente", _cliente))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TP0011", "", _detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TP001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarProductosSinLoteProforma(_almacen As String, _cliente As String, _detalle As DataTable) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@cliente", _cliente))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TP0011", "", _detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TP001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarProductosSinLoteProformaNuevo(_almacen As String, _cliente As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@cliente", _cliente))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TP001_Nuevo", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarClientesProforma() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TP001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarEmpleadoProforma() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TP001", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_fnGrabarProforma(ByRef _panumi As String, _pafdoc As String, _paven As Integer, _paclpr As Integer,
                                           _pamon As Integer, _paobs As String,
                                           _padesc As Double,
                                           _patotal As Double, detalle As DataTable, _almacen As Integer) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '     @panumi,@paalm,@pafdoc ,@paven ,@paclpr,
        '@pamon ,@paest  ,@paobs ,@padesc  ,@patotal ,@newFecha,@newHora,@pauact
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@panumi", _panumi))
        _listParam.Add(New Datos.DParametro("@paalm", _almacen))
        _listParam.Add(New Datos.DParametro("@pafdoc", _pafdoc))
        _listParam.Add(New Datos.DParametro("@paven", _paven))
        _listParam.Add(New Datos.DParametro("@paclpr", _paclpr))
        _listParam.Add(New Datos.DParametro("@pamon", _pamon))
        _listParam.Add(New Datos.DParametro("@paest", 1))
        _listParam.Add(New Datos.DParametro("@paobs", _paobs))
        _listParam.Add(New Datos.DParametro("@padesc", _padesc))
        _listParam.Add(New Datos.DParametro("@patotal", _patotal))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TP0011", "", detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TP001", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _panumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnModificarProforma(ByRef _panumi As String, _pafdoc As String, _paven As Integer, _paclpr As Integer,
                                           _pamon As Integer, _paobs As String,
                                           _padesc As Double,
                                           _patotal As Double, detalle As DataTable, _almacen As Integer) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@panumi", _panumi))
        _listParam.Add(New Datos.DParametro("@paalm", _almacen))
        _listParam.Add(New Datos.DParametro("@pafdoc", _pafdoc))
        _listParam.Add(New Datos.DParametro("@paven", _paven))
        _listParam.Add(New Datos.DParametro("@paclpr", _paclpr))
        _listParam.Add(New Datos.DParametro("@pamon", _pamon))
        _listParam.Add(New Datos.DParametro("@paest", 1))
        _listParam.Add(New Datos.DParametro("@paobs", _paobs))
        _listParam.Add(New Datos.DParametro("@padesc", _padesc))
        _listParam.Add(New Datos.DParametro("@patotal", _patotal))
        _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TP0011", "", detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TP001", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _panumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnEliminarProforma(numi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TP001", "panumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@panumi", numi))
            _listParam.Add(New Datos.DParametro("@pauact", L_Usuario))
            _Tabla = D_ProcedimientoConParam("sp_Mam_TP001", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
#End Region
#Region "VENTAS ESTADISTICOS"
    Public Shared Function L_fnObtenerVendedores(_fechaI As String, _fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))

        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasEstadisticos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerVentasVendedores(_fechaI As String, _fechaF As String, _numiVendedor As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))
        _listParam.Add(New Datos.DParametro("@vendedor", _numiVendedor))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasEstadisticos", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnObtenerCLientes(_fechaI As String, _fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))

        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasEstadisticos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerVentasClientes(_fechaI As String, _fechaF As String, _numiCliente As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))
        _listParam.Add(New Datos.DParametro("@cliente", _numiCliente))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasEstadisticos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerProductosVentasEstadistico(_fechaI As String, _fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))

        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasEstadisticos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerVentasProductosEstadistico(_fechaI As String, _fechaF As String, _numiProducto As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))
        _listParam.Add(New Datos.DParametro("@producto", _numiProducto))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasEstadisticos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerZonasVentasEstadistico(_fechaI As String, _fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))

        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasEstadisticos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerVentasZonasEstadistico(_fechaI As String, _fechaF As String, _numiZona As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))
        _listParam.Add(New Datos.DParametro("@zona", _numiZona))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasEstadisticos", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnObtenerCOBRANZASVentasEstadistico(_fechaI As String, _fechaF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))

        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasEstadisticos", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnObtenerVentasCOBRANZASEstadistico(_fechaI As String, _fechaF As String, _numiVendedor As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@fechaI", _fechaI))
        _listParam.Add(New Datos.DParametro("@fechaF", _fechaF))
        _listParam.Add(New Datos.DParametro("@vendedor", _numiVendedor))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_VentasEstadisticos", _listParam)

        Return _Tabla
    End Function
#End Region

#Region "TF001 FACTURA"
    Public Shared Function L_fnGeneralFactura() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@fauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TF001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnDetalleFactura(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@fanumi", _numi))
        _listParam.Add(New Datos.DParametro("@fauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TF001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarProductosFactura(_almacen As String, _cliente As String, _detalle As DataTable) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@cliente", _cliente))
        _listParam.Add(New Datos.DParametro("@fauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TF0011", "", _detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TF001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarProductosSinLoteFactura(_almacen As String, _cliente As String, _detalle As DataTable) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@almacen", _almacen))
        _listParam.Add(New Datos.DParametro("@cliente", _cliente))
        _listParam.Add(New Datos.DParametro("@fauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TF0011", "", _detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TF001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarClientesFactura() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TF001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarEmpleadoFactura() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TF001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnListarVentasFactura() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TF001", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_fnGrabarFactura(ByRef _fanumi As String, _fatv1numi As Integer, _fanroFact As String, _fafdoc As String, _faven As Integer, _faclpr As Integer,
                                           _famon As Integer, _faobs As String,
                                           _fadesc As Double,
                                           _fatotal As Double, _fanit As String, _fanombFact As String, detalle As DataTable, _almacen As Integer) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '     @panumi,@paalm,@pafdoc ,@paven ,@paclpr,
        '@pamon ,@paest  ,@paobs ,@padesc  ,@patotal ,@newFecha,@newHora,@pauact

        '     @fanumi,@fatv1numi ,@fanrofact ,@faalm,@fafdoc ,@faven ,@faclpr,
        '@famon ,@faest  ,@faobs ,@fadesc  ,@fatotal ,@fanit ,@fanombfact ,@newFecha,@newHora,@fauact
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@fanumi", _fanumi))
        _listParam.Add(New Datos.DParametro("@fatv1numi", _fatv1numi))
        _listParam.Add(New Datos.DParametro("@fanrofact", _fanroFact))
        _listParam.Add(New Datos.DParametro("@faalm", _almacen))
        _listParam.Add(New Datos.DParametro("@fafdoc", _fafdoc))
        _listParam.Add(New Datos.DParametro("@faven", _faven))
        _listParam.Add(New Datos.DParametro("@faclpr", _faclpr))
        _listParam.Add(New Datos.DParametro("@famon", _famon))
        _listParam.Add(New Datos.DParametro("@faest", 1))
        _listParam.Add(New Datos.DParametro("@faobs", _faobs))
        _listParam.Add(New Datos.DParametro("@fadesc", _fadesc))
        _listParam.Add(New Datos.DParametro("@fatotal", _fatotal))
        _listParam.Add(New Datos.DParametro("@fauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@fanit", _fanit))
        _listParam.Add(New Datos.DParametro("@fanombfact", _fanombFact))
        _listParam.Add(New Datos.DParametro("@TF0011", "", detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TF001", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _fanumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnModificarFactura(ByRef _fanumi As String, _fatv1numi As Integer, _fanroFact As String, _fafdoc As String, _faven As Integer, _faclpr As Integer,
                                           _famon As Integer, _faobs As String,
                                           _fadesc As Double,
                                           _fatotal As Double, _fanit As String, _fanombFact As String, detalle As DataTable, _almacen As Integer) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@fanumi", _fanumi))
        _listParam.Add(New Datos.DParametro("@fatv1numi", _fatv1numi))
        _listParam.Add(New Datos.DParametro("@fanrofact", _fanroFact))
        _listParam.Add(New Datos.DParametro("@faalm", _almacen))
        _listParam.Add(New Datos.DParametro("@fafdoc", _fafdoc))
        _listParam.Add(New Datos.DParametro("@faven", _faven))
        _listParam.Add(New Datos.DParametro("@faclpr", _faclpr))
        _listParam.Add(New Datos.DParametro("@famon", _famon))
        _listParam.Add(New Datos.DParametro("@faest", 1))
        _listParam.Add(New Datos.DParametro("@faobs", _faobs))
        _listParam.Add(New Datos.DParametro("@fadesc", _fadesc))
        _listParam.Add(New Datos.DParametro("@fatotal", _fatotal))
        _listParam.Add(New Datos.DParametro("@fauact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@fanit", _fanit))
        _listParam.Add(New Datos.DParametro("@fanombfact", _fanombFact))
        _listParam.Add(New Datos.DParametro("@TF0011", "", detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TF001", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _fanumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnImprimirFactura(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _listParam.Add(New Datos.DParametro("@fanumi", _numi))
        _listParam.Add(New Datos.DParametro("@fauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TF001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnEliminarFactura(numi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TF001", "fanumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@fanumi", numi))
            _listParam.Add(New Datos.DParametro("@fauact", L_Usuario))
            _Tabla = D_ProcedimientoConParam("sp_Mam_TF001", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function
#End Region

#Region "SALDO INICIAL DE CLIENTES"
    Public Shared Function L_fnSaldoGeneral() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@tfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

        Return _Tabla
    End Function


    Public Shared Function L_fnSaldoDetalle(_numi As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@tfuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@tfnumi", _numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

        Return _Tabla
    End Function



    Public Shared Function L_fnEliminarSaldos(numi As String, ByRef mensaje As String) As Boolean
        Dim _resultado As Boolean
        If L_fnbValidarEliminacion(numi, "TV002", "tfnumi", mensaje) = True Then
            Dim _Tabla As DataTable
            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@tfnumi", numi))
            _listParam.Add(New Datos.DParametro("@tfuact", L_Usuario))

            _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If
        Return _resultado
    End Function

    Public Shared Function L_fnGrabarDetalle(_tfnumi As String, _type As Integer, _detalle As DataTable) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '   @canumi ,@caalm,@cafdoc ,@caty4prov  ,@catven,
        '@cafvcr,@camon ,@caest  ,@caobs ,@cadesc ,@newFecha,@newHora,@cauact
        _listParam.Add(New Datos.DParametro("@tipo", _type))
        _listParam.Add(New Datos.DParametro("@tfnumi", _tfnumi))
        _listParam.Add(New Datos.DParametro("@TV0012", "", _detalle))
        _listParam.Add(New Datos.DParametro("@tfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)
        If _Tabla.Rows.Count > 0 Then
            _tfnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnGrabarSaldos(_tfnumi As String, _tfobservacion As String, _tffecha As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '   @canumi ,@caalm,@cafdoc ,@caty4prov  ,@catven,
        '@cafvcr,@camon ,@caest  ,@caobs ,@cadesc ,@newFecha,@newHora,@cauact
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@tfnumi", _tfnumi))
        _listParam.Add(New Datos.DParametro("@tffecha", _tffecha))
        _listParam.Add(New Datos.DParametro("@tfobservacion", _tfobservacion))
        _listParam.Add(New Datos.DParametro("@tfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)
        If _Tabla.Rows.Count > 0 Then
            _tfnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnModificarSaldos(_tfnumi As String, _tfobservacion As String, _tffecha As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '   @canumi ,@caalm,@cafdoc ,@caty4prov  ,@catven,
        '@cafvcr,@camon ,@caest  ,@caobs ,@cadesc ,@newFecha,@newHora,@cauact
        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@tfnumi", _tfnumi))
        _listParam.Add(New Datos.DParametro("@tffecha", _tffecha))
        _listParam.Add(New Datos.DParametro("@tfobservacion", _tfobservacion))
        _listParam.Add(New Datos.DParametro("@tfuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV002", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _tfnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
#End Region

#Region "REPORTE DE SALDOS DE PRODUCTO AGRUPADOS POR LINEAS"
    Public Shared Function L_fnObtenerGruposLibreria() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_SaldosProducto", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnTodosAlmacenTodosLineas() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_SaldosProducto", _listParam)

        Return _Tabla
    End Function
    'funcion para obetener los productos menores al stock
    Public Shared Function L_fnTodosAlmacenTodosLineasMenoresStock() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 22))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_SaldosProducto", _listParam)

        Return _Tabla
    End Function
    'funcion para obtener mayores a cero Efrain
    Public Shared Function L_fnTodosAlmacenTodosLineasMayorCero() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_SaldosProducto", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnUnaAlmacenTodosLineas(numialmacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", numialmacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_SaldosProducto", _listParam)

        Return _Tabla
    End Function
    'funcion para obtener un Almacen todas las lineas canidad menores al stock
    Public Shared Function L_fnUnaAlmacenTodosLineasMenoresStock(numialmacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 33))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", numialmacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_SaldosProducto", _listParam)

        Return _Tabla
    End Function
    'un almacen todos linea y mayor a cero Efrain
    Public Shared Function L_fnUnaAlmacenTodosLineasMayorCero(numialmacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@almacen", numialmacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_SaldosProducto", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnTodosAlmacenUnaLineas(numiLinea As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@linea", numiLinea))
        _Tabla = D_ProcedimientoConParam("sp_Mam_SaldosProducto", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnTodosAlmacenUnaLineasMenoresStock(numiLinea As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 44))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@linea", numiLinea))
        _Tabla = D_ProcedimientoConParam("sp_Mam_SaldosProducto", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnUnaAlmacenUnaLineas(numiLinea As Integer, CodAlmacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@linea", numiLinea))
        _listParam.Add(New Datos.DParametro("@almacen", CodAlmacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_SaldosProducto", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnUnaAlmacenUnaLineasMenoresStock(numiLinea As Integer, CodAlmacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 55))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@linea", numiLinea))
        _listParam.Add(New Datos.DParametro("@almacen", CodAlmacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_SaldosProducto", _listParam)

        Return _Tabla
    End Function
    ' funcion mayor un almacen una linea y stock mayor a cero
    Public Shared Function L_fnUnaAlmacenUnaLineasMayorCero(numiLinea As Integer, CodAlmacen As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@yduact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@linea", numiLinea))
        _listParam.Add(New Datos.DParametro("@almacen", CodAlmacen))
        _Tabla = D_ProcedimientoConParam("sp_Mam_SaldosProducto", _listParam)

        Return _Tabla
    End Function
#End Region

#Region "CIERRE DE CAJA TCC001"

    Public Shared Function L_fnEliminarCaja(numi As String, fecha As String) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", -1))
        _listParam.Add(New Datos.DParametro("@ccnumi", numi))
        _listParam.Add(New Datos.DParametro("@fecha", fecha))
        _listParam.Add(New Datos.DParametro("@ccuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TCC001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            numi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnGrabarCaja(ByRef _ccnumi As String, _fecha As String, _TotalGral As Decimal, _Credito As Decimal,
                                          _Tarjeta As Decimal, _ContadoBs As Decimal, _Depositos As Decimal, _Efectivo As Decimal,
                                          _Diferencia As Decimal, _Pagos As Decimal, _Turno As String, _MInicial As Decimal,
                                          _Ingresos As Decimal, _Egresos As Decimal, _Estado As Integer, _TipoCambio As Decimal,
                                          _Obs As String, _TCC0011 As DataTable, _TCC0012 As DataTable, _NroCaja As Integer) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@ccnumi", _ccnumi))
        _listParam.Add(New Datos.DParametro("@fecha", _fecha))
        _listParam.Add(New Datos.DParametro("@TotalGral", _TotalGral))
        _listParam.Add(New Datos.DParametro("@Credito", _Credito))
        _listParam.Add(New Datos.DParametro("@Tarjeta", _Tarjeta))
        _listParam.Add(New Datos.DParametro("@ContadoBs", _ContadoBs))
        _listParam.Add(New Datos.DParametro("@Depositos", _Depositos))
        _listParam.Add(New Datos.DParametro("@Efectivo", _Efectivo))
        _listParam.Add(New Datos.DParametro("@Diferencia", _Diferencia))
        _listParam.Add(New Datos.DParametro("@Pagos", _Pagos))
        _listParam.Add(New Datos.DParametro("@Turno", _Turno))
        _listParam.Add(New Datos.DParametro("@MInicial", _MInicial))
        _listParam.Add(New Datos.DParametro("@Estado", _Estado))
        _listParam.Add(New Datos.DParametro("@Ingreso", _Ingresos))
        _listParam.Add(New Datos.DParametro("@Egreso", _Egresos))
        _listParam.Add(New Datos.DParametro("@TipoCambio", _TipoCambio))
        _listParam.Add(New Datos.DParametro("@Obs", _Obs))
        _listParam.Add(New Datos.DParametro("@ccuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@Nrocaja", _NroCaja))
        _listParam.Add(New Datos.DParametro("@TCC0011", "", _TCC0011))
        _listParam.Add(New Datos.DParametro("@TCC0012", "", _TCC0012))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TCC001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _ccnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnModificarCaja(ByRef _ccnumi As String, _fecha As String, _TotalGral As Decimal, _Credito As Decimal,
                                          _Tarjeta As Decimal, _ContadoBs As Decimal, _Depositos As Decimal, _Efectivo As Decimal,
                                          _Diferencia As Decimal, _Pagos As Decimal, _Turno As String, _MInicial As Decimal,
                                          _Ingresos As Decimal, _Egresos As Decimal, _TipoCambio As Decimal, _Obs As String,
                                          _TCC0011 As DataTable, _TCC0012 As DataTable, _TCC0013 As DataTable, _NroCaja As Integer) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@ccnumi", _ccnumi))
        _listParam.Add(New Datos.DParametro("@fecha", _fecha))
        _listParam.Add(New Datos.DParametro("@TotalGral", _TotalGral))
        _listParam.Add(New Datos.DParametro("@Credito", _Credito))
        _listParam.Add(New Datos.DParametro("@Tarjeta", _Tarjeta))
        _listParam.Add(New Datos.DParametro("@ContadoBs", _ContadoBs))
        _listParam.Add(New Datos.DParametro("@Depositos", _Depositos))
        _listParam.Add(New Datos.DParametro("@Efectivo", _Efectivo))
        _listParam.Add(New Datos.DParametro("@Diferencia", _Diferencia))
        _listParam.Add(New Datos.DParametro("@Pagos", _Pagos))
        _listParam.Add(New Datos.DParametro("@Turno", _Turno))
        _listParam.Add(New Datos.DParametro("@MInicial", _MInicial))
        _listParam.Add(New Datos.DParametro("@Estado", 0))
        _listParam.Add(New Datos.DParametro("@Ingreso", _Ingresos))
        _listParam.Add(New Datos.DParametro("@Egreso", _Egresos))
        _listParam.Add(New Datos.DParametro("@TipoCambio", _TipoCambio))
        _listParam.Add(New Datos.DParametro("@Obs", _Obs))
        _listParam.Add(New Datos.DParametro("@ccuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@Nrocaja", _NroCaja))
        _listParam.Add(New Datos.DParametro("@TCC0011", "", _TCC0011))
        _listParam.Add(New Datos.DParametro("@TCC0012", "", _TCC0012))
        _listParam.Add(New Datos.DParametro("@TCC0013", "", _TCC0013))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TCC001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _ccnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prCajaGeneral() As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@ccuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TCC001", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_fnDetalleVentasPagos(fecha As String, Nrocaja As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@fecha", fecha))
        _listParam.Add(New Datos.DParametro("@Nrocaja", Nrocaja))
        _listParam.Add(New Datos.DParametro("@ccuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TCC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnDetalleCortes(idCaja As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@ccnumi", idCaja))
        _listParam.Add(New Datos.DParametro("@ccuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TCC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnDetalleDepositos(idCaja As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@ccnumi", idCaja))
        _listParam.Add(New Datos.DParametro("@ccuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TCC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prListarSoloBanco() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@ccuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TCC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnVerificarSiExisteCierreCaja(fecha As String, ccnumi As String, Nrocaja As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@fecha", fecha))
        _listParam.Add(New Datos.DParametro("@ccnumi", ccnumi))
        _listParam.Add(New Datos.DParametro("@Nrocaja", Nrocaja))
        _listParam.Add(New Datos.DParametro("@ccuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TCC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnDetalleVentasPagosPorIdCaja(fecha As String, ccnumi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@fecha", fecha))
        _listParam.Add(New Datos.DParametro("@ccnumi", ccnumi))
        _listParam.Add(New Datos.DParametro("@ccuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TCC001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnVerificarCajaAbierta(Nrocaja As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@Nrocaja", Nrocaja))
        _listParam.Add(New Datos.DParametro("@ccuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TCC001", _listParam)

        Return _Tabla
    End Function
#End Region

#Region "BANCOS"

    Public Shared Function L_prBancoGeneral() As DataTable
        Dim _Tabla As DataTable
        't.canumi , t.canombre, t.cacuenta, t.caobs, t.cafact, t.cahact, t.cauact 
        Dim _listPalam As New List(Of Datos.DParametro)

        _listPalam.Add(New Datos.DParametro("@tipo", 3))
        _listPalam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_BA001", _listPalam)

        Return _Tabla
    End Function
    Public Shared Function L_prBancoGrabar(ByRef _canumi As String, _canombre As String,
                                           _cacuenta As String, _caobs As String,
                                           _img As String, _estado As Integer) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listPalam As New List(Of Datos.DParametro)
        't.canumi , t.canombre, t.cacuenta, t.caobs, t.cafact, t.cahact, t.cauact 
        _listPalam.Add(New Datos.DParametro("@tipo", 1))
        _listPalam.Add(New Datos.DParametro("@canumi", _canumi))
        _listPalam.Add(New Datos.DParametro("@canombre", _canombre))
        _listPalam.Add(New Datos.DParametro("@canrocuenta", _cacuenta))
        _listPalam.Add(New Datos.DParametro("@caobs", _caobs))
        _listPalam.Add(New Datos.DParametro("@caestado", _estado))
        _listPalam.Add(New Datos.DParametro("@caimg", _img))
        _listPalam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_BA001", _listPalam)

        If _Tabla.Rows.Count > 0 Then
            _canumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prBancoBorrar(_numi As String, ByRef _mensaje As String) As Boolean

        Dim _resultado As Boolean

        If L_fnbValidarEliminacion(_numi, "BA001", "canumi", _mensaje) = True Then
            Dim _Tabla As DataTable

            Dim _listPalam As New List(Of Datos.DParametro)

            _listPalam.Add(New Datos.DParametro("@tipo", -1))
            _listPalam.Add(New Datos.DParametro("@canumi", _numi))
            _listPalam.Add(New Datos.DParametro("@cauact", L_Usuario))
            _Tabla = D_ProcedimientoConParam("sp_Mam_BA001", _listPalam)

            If _Tabla.Rows.Count > 0 Then
                _numi = _Tabla.Rows(0).Item(0)
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prBancoModificar(ByRef _canumi As String, _canombre As String,
                                           _cacuenta As String, _caobs As String,
                                           _img As String, _estado As Integer) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listPalam As New List(Of Datos.DParametro)
        't.canumi , t.canombre, t.cacuenta, t.caobs, t.cafact, t.cahact, t.cauact 
        _listPalam.Add(New Datos.DParametro("@tipo", 2))
        _listPalam.Add(New Datos.DParametro("@canumi", _canumi))
        _listPalam.Add(New Datos.DParametro("@canombre", _canombre))
        _listPalam.Add(New Datos.DParametro("@canrocuenta", _cacuenta))
        _listPalam.Add(New Datos.DParametro("@caobs", _caobs))
        _listPalam.Add(New Datos.DParametro("@caestado", _estado))
        _listPalam.Add(New Datos.DParametro("@caimg", _img))
        _listPalam.Add(New Datos.DParametro("@cauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_BA001", _listPalam)

        If _Tabla.Rows.Count > 0 Then
            _canumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

#End Region
#Region "INGRESOS/EGRESOS"

    Public Shared Function L_prIngresoEgresoGeneral() As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@ieuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TIE001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_ListaVentasCobrar() As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 27))
        _listParam.Add(New Datos.DParametro("@tauact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_prIngresoEgresoGrabar(ByRef _ienumi As String, _ieFecha As String, _ieTipo As String,
                                           _ieDescripcion As String, _ieConcepto As String, _ieMonto As Decimal,
                                           _ieObs As String, Optional _NroCaja As Integer = 0, Optional tbIdCaja As String = "", Optional _ieidasig As Integer = 0,
                                                   Optional idCanero As String = "", Optional tbdescCanero As String = "", Optional idInstitucion As String = "", Optional tbInstitucion As String = "", Optional tbRecibi As String = "", Optional tbentregue As String = "", Optional idActDis As String = "",
                                          Optional idCuenCont As String = "", Optional tbNroOpera As String = "", Optional tbNroCheque As String = "", Optional tbBanco As String = "", Optional SwParticular As String = "", Optional cbTipPago As String = "", Optional SwMoneda As String = "", Optional ordenDe As String = "", Optional almacen As String = "") As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@ienumi", _ienumi))
        _listParam.Add(New Datos.DParametro("@ieFecha", _ieFecha))
        _listParam.Add(New Datos.DParametro("@ieTipo", _ieTipo))
        _listParam.Add(New Datos.DParametro("@ieDescripcion", _ieDescripcion))
        _listParam.Add(New Datos.DParametro("@ieConcepto", _ieConcepto))
        _listParam.Add(New Datos.DParametro("@ieMonto", _ieMonto))
        _listParam.Add(New Datos.DParametro("@ieObs", _ieObs))
        _listParam.Add(New Datos.DParametro("@ieEstado", 1))
        _listParam.Add(New Datos.DParametro("@Nrocaja", _NroCaja))
        _listParam.Add(New Datos.DParametro("@idCaja", tbIdCaja))
        _listParam.Add(New Datos.DParametro("@ieidasig", _ieidasig))

        _listParam.Add(New Datos.DParametro("@idCanero", idCanero))
        _listParam.Add(New Datos.DParametro("@tbdescCanero", tbdescCanero))
        _listParam.Add(New Datos.DParametro("@idInstitucion", idInstitucion))
        _listParam.Add(New Datos.DParametro("@tbInstitucion", tbInstitucion))
        _listParam.Add(New Datos.DParametro("@tbRecibi", tbRecibi))
        _listParam.Add(New Datos.DParametro("@tbentregue", tbentregue))
        _listParam.Add(New Datos.DParametro("@idActDis", idActDis))
        _listParam.Add(New Datos.DParametro("@idCuenCont", idCuenCont))
        _listParam.Add(New Datos.DParametro("@tbNroOpera", tbNroOpera))
        _listParam.Add(New Datos.DParametro("@tbNroCheque", tbNroCheque))
        _listParam.Add(New Datos.DParametro("@tbBanco", tbBanco))
        _listParam.Add(New Datos.DParametro("@SwParticular", SwParticular))
        _listParam.Add(New Datos.DParametro("@cbTipPago", cbTipPago))
        _listParam.Add(New Datos.DParametro("@SwMoneda", SwMoneda))
        _listParam.Add(New Datos.DParametro("@tbOrden", ordenDe))
        _listParam.Add(New Datos.DParametro("@almacen", almacen))
        _listParam.Add(New Datos.DParametro("@ieuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TIE001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _ienumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prIngresoEgresoBorrar(_numi As String, ByRef _mensaje As String) As Boolean

        Dim _resultado As Boolean

        If L_fnbValidarEliminacion(_numi, "TIE001", "ienumi", _mensaje) = True Then
            Dim _Tabla As DataTable

            Dim _listParam As New List(Of Datos.DParametro)

            _listParam.Add(New Datos.DParametro("@tipo", -1))
            _listParam.Add(New Datos.DParametro("@ienumi", _numi))
            _listParam.Add(New Datos.DParametro("@ieuact", L_Usuario))
            _Tabla = D_ProcedimientoConParam("sp_Mam_TIE001", _listParam)

            If _Tabla.Rows.Count > 0 Then
                _numi = _Tabla.Rows(0).Item(0)
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_prIngresoEgresoModificar(ByRef _ienumi As String, _ieFecha As String, _ieTipo As String,
                                           _ieDescripcion As String, _ieConcepto As String, _ieMonto As Decimal,
                                           _ieObs As String,
                                                   Optional idCanero As String = "", Optional tbdescCanero As String = "", Optional idInstitucion As String = "", Optional tbInstitucion As String = "", Optional tbRecibi As String = "", Optional tbentregue As String = "", Optional idActDis As String = "",
                                          Optional idCuenCont As String = "", Optional tbNroOpera As String = "", Optional tbNroCheque As String = "", Optional tbBanco As String = "", Optional SwParticular As String = "", Optional cbTipPago As String = "", Optional SwMoneda As String = "") As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@ienumi", _ienumi))
        _listParam.Add(New Datos.DParametro("@ieFecha", _ieFecha))
        _listParam.Add(New Datos.DParametro("@ieTipo", _ieTipo))
        _listParam.Add(New Datos.DParametro("@ieDescripcion", _ieDescripcion))
        _listParam.Add(New Datos.DParametro("@ieConcepto", _ieConcepto))
        _listParam.Add(New Datos.DParametro("@ieMonto", _ieMonto))
        _listParam.Add(New Datos.DParametro("@ieObs", _ieObs))
        _listParam.Add(New Datos.DParametro("@ieEstado", 2))
        _listParam.Add(New Datos.DParametro("@ieuact", L_Usuario))

        _listParam.Add(New Datos.DParametro("@idCanero", idCanero))
        _listParam.Add(New Datos.DParametro("@tbdescCanero", tbdescCanero))
        _listParam.Add(New Datos.DParametro("@idInstitucion", idInstitucion))
        _listParam.Add(New Datos.DParametro("@tbInstitucion", tbInstitucion))
        _listParam.Add(New Datos.DParametro("@tbRecibi", tbRecibi))
        _listParam.Add(New Datos.DParametro("@tbentregue", tbentregue))
        _listParam.Add(New Datos.DParametro("@idActDis", idActDis))
        _listParam.Add(New Datos.DParametro("@idCuenCont", idCuenCont))
        _listParam.Add(New Datos.DParametro("@tbNroOpera", tbNroOpera))
        _listParam.Add(New Datos.DParametro("@tbNroCheque", tbNroCheque))
        _listParam.Add(New Datos.DParametro("@tbBanco", tbBanco))
        _listParam.Add(New Datos.DParametro("@SwParticular", SwParticular))
        _listParam.Add(New Datos.DParametro("@cbTipPago", cbTipPago))
        _listParam.Add(New Datos.DParametro("@SwMoneda", SwMoneda))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TIE001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _ienumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_prIngresoEgresoPorFecha(fecha As String, NroCaja As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@ieFecha", fecha))
        _listParam.Add(New Datos.DParametro("@Nrocaja", NroCaja))
        _listParam.Add(New Datos.DParametro("@ieuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TIE001", _listParam)

        Return _Tabla
    End Function
#End Region
#Region "Empresa tipo de reporte"
    Public Shared Function ObtenerEmpresaHabilitada() As Integer
        Dim _Tabla As DataTable
        Dim _resultado As Integer
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _Tabla = D_ProcedimientoConParam("sp_EmpresaReporte", _listParam)
        If _Tabla.Rows.Count > 0 Then
            _resultado = _Tabla.Rows(0).Item("Id")
        Else
            _resultado = 0
        End If
        Return _resultado
    End Function
    Public Shared Function ObtenerEmpresaTipoReporte(empresaId As Integer, reporteId As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@empresaId", empresaId))
        _listParam.Add(New Datos.DParametro("@reporteId", reporteId))
        _Tabla = D_ProcedimientoConParam("sp_EmpresaReporte", _listParam)
        Return _Tabla
    End Function
#End Region

#Region "Descuento por Proveedor"
    Public Shared Function ObtenerDescuentoPorProveedor() As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 21))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)
        Return _Tabla
    End Function
    Public Shared Function ObtenerProveedorIDXProducto(productoId As Integer) As Integer
        Dim _Tabla As DataTable
        Dim _resultado As Integer
        Dim _listParam As New List(Of Datos.DParametro)
        _listParam.Add(New Datos.DParametro("@tipo", 22))
        _listParam.Add(New Datos.DParametro("@producto", productoId))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TV001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _resultado = _Tabla.Rows(0).Item("ProveedorId")
        Else
            _resultado = 0
        End If
        Return _resultado

    End Function
#End Region

#Region "Cuentas Cañeras"

    Public Shared Function L_fnGeneralPagos() As DataTable
        Dim _Tabla As DataTable
        'Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 15))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)
        Return _Tabla
    End Function
    Public Shared Function L_fnDetallePagos(codigo As String) As DataTable
        Dim _Tabla As DataTable
        'Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 16))
        _listParam.Add(New Datos.DParametro("@interes", codigo))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)
        Return _Tabla
    End Function

    Public Shared Function L_fnPagarCuenta(fecha As String, codcan As Integer, codInst As Integer, interes As Integer, formaPago As Integer, Almacen As Integer, moneda As Integer, detalle As DataTable) As DataTable
        Dim _Tabla As DataTable
        'Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 14))
        _listParam.Add(New Datos.DParametro("@cafdoc", fecha))
        _listParam.Add(New Datos.DParametro("@cliente", codcan))
        _listParam.Add(New Datos.DParametro("@inst", codInst))
        _listParam.Add(New Datos.DParametro("@interes", interes))
        _listParam.Add(New Datos.DParametro("@forpa", formaPago))
        _listParam.Add(New Datos.DParametro("@caalm", Almacen))
        _listParam.Add(New Datos.DParametro("@camon", moneda))
        _listParam.Add(New Datos.DParametro("@TaPagar", "", detalle))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001", _listParam)
        Return _Tabla
    End Function


    Public Shared Function L_fnAgregarGrupo(cod As String, cabeza As Integer, fecha As String, obs As String, detalle As DataTable) As Boolean
        Dim _Tabla As DataTable
        'Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@codGrupo", cabeza))
        '_listParam.Add(New Datos.DParametro("@tipo", cabeza))
        _listParam.Add(New Datos.DParametro("@fecha", fecha))
        _listParam.Add(New Datos.DParametro("@observacion", obs))
        _listParam.Add(New Datos.DParametro("@TG0011", "", detalle))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TG001", _listParam)
        If _Tabla.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Shared Function L_prGrupoEliminar(cod As String) As Boolean
        Dim _Tabla As DataTable
        'Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", -1))
        _listParam.Add(New Datos.DParametro("@codGrupo", cod))

        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TG001", _listParam)
        If _Tabla.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Shared Function L_prGrupoModificar(cod As String, cabeza As Integer, fecha As String, obs As String, detalle As DataTable) As Boolean
        Dim _Tabla As DataTable
        'Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@codGrupo", cabeza))
        _listParam.Add(New Datos.DParametro("@id", cod))
        _listParam.Add(New Datos.DParametro("@fecha", fecha))
        _listParam.Add(New Datos.DParametro("@observacion", obs))
        _listParam.Add(New Datos.DParametro("@TG0011", "", detalle))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TG001", _listParam)
        If _Tabla.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Shared Function verificarcanero(cod As Integer) As Boolean
        Dim _Tabla As DataTable
        'Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 6))
        _listParam.Add(New Datos.DParametro("@id", cod))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TG001", _listParam)
        If _Tabla.Rows(0).Item("id") = 1 Then
            Return False
        Else
            Return True
        End If

    End Function

    Public Shared Function cargarDeudasporCanero(cod As Integer, quin As Integer, gest As String) As DataTable
        Dim _Tabla As DataTable
        'Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@id", cod))
        _listParam.Add(New Datos.DParametro("@codGrupo", quin))
        _listParam.Add(New Datos.DParametro("@fecha", gest))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TG001", _listParam)

        Return _Tabla


    End Function
    Public Shared Function cargarDeudasporGrupo(quin As Integer, gest As String, dt As DataTable) As DataTable
        Dim _Tabla As DataTable
        'Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@cañeros", "", dt))
        _listParam.Add(New Datos.DParametro("@codGrupo", quin))
        _listParam.Add(New Datos.DParametro("@fecha", gest))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TG001", _listParam)

        Return _Tabla


    End Function
    Public Shared Function cargaCobranzaporCanero(cod As Integer, fecha As String) As DataTable
        Dim _Tabla As DataTable
        'Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _listParam.Add(New Datos.DParametro("@id", cod))
        _listParam.Add(New Datos.DParametro("@fecha", fecha))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TG001", _listParam)

        Return _Tabla
    End Function



    Public Shared Function cargarDeudasporGrupoCobranza(fecha As String, dt As DataTable) As DataTable
        Dim _Tabla As DataTable
        'Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 12))
        _listParam.Add(New Datos.DParametro("@cañeros", "", dt))
        _listParam.Add(New Datos.DParametro("@fecha", fecha))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TG001", _listParam)

        Return _Tabla


    End Function

    Public Shared Function RevisarUltimaNota(doc As Integer, almacen As Integer, id As Integer) As Boolean
        Dim _Tabla As DataTable
        Dim res As Boolean

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 31))
        _listParam.Add(New Datos.DParametro("@trquin", almacen))
        _listParam.Add(New Datos.DParametro("@trges", doc))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)
        If _Tabla.Rows.Count > 0 Then
            If _Tabla.Rows(0).Item("trid") = id Then
                res = True
            Else
                res = False
            End If
        End If
        Return res
    End Function

    Public Shared Function RevisarUltimaNota2(doc As Integer, almacen As Integer, id As Integer) As DataTable
        Dim _Tabla As DataTable


        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 31))
        _listParam.Add(New Datos.DParametro("@trquin", almacen))
        _listParam.Add(New Datos.DParametro("@trges", doc))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function TraerDatosRetencion(id As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 33))
        _listParam.Add(New Datos.DParametro("@trid", id))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnEliminarRetencion(id As Integer) As Boolean
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", -1))
        _listParam.Add(New Datos.DParametro("@trid", id))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        Return True
    End Function
#End Region

#Region "Prestamos"
    Public Shared Function L_fnGeneralPrestamos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 20))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnGeneralServiciosFacturado() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 24))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnTraerInteres(cod As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 21))
        _listParam.Add(New Datos.DParametro("@tbpre", cod))
        _listParam.Add(New Datos.DParametro("@yguact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TY006", _listParam)

        Return _Tabla
    End Function
#End Region
#Region "asginacion de cuentas"
    Public Shared Function L_prAsignacionBorrar(_tcnumi As String, ByRef _mensaje As String) As Boolean

        Dim _resultado As Boolean

        If L_fnbValidarEliminacion(_tcnumi, "TC001a", "tcnumi", _mensaje) = True Then
            Dim _Tabla As DataTable

            Dim _listPalam As New List(Of Datos.DParametro)

            _listPalam.Add(New Datos.DParametro("@tipo", -1))
            _listPalam.Add(New Datos.DParametro("@tcnumi", _tcnumi))
            _listPalam.Add(New Datos.DParametro("@tcuact", L_Usuario))
            _Tabla = D_ProcedimientoConParam("sp_Mam_TC001a", _listPalam)

            If _Tabla.Rows.Count > 0 Then
                _tcnumi = _Tabla.Rows(0).Item(0)
                _resultado = True
            Else
                _resultado = False
            End If
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnGrabarAsignacion(ByRef _tcnumi As String, _tccuen As Integer, _tcnumcheq As String, _tcbanco As String, _tcnumoper As String, _tcobservacion As String, _tcorden As String) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '   @canumi ,@caalm,@cafdoc ,@caty4prov  ,@catven,
        '@cafvcr,@camon ,@caest  ,@caobs ,@cadesc ,@newFecha,@newHora,@cauact
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@tcnumi", _tcnumi))
        _listParam.Add(New Datos.DParametro("@tccuen", _tccuen))
        _listParam.Add(New Datos.DParametro("@tcnumcheq", _tcnumcheq))
        _listParam.Add(New Datos.DParametro("@tcbanco", _tcbanco))
        _listParam.Add(New Datos.DParametro("@tcnumoper", _tcnumoper))
        _listParam.Add(New Datos.DParametro("@tcobservacion", _tcobservacion))
        _listParam.Add(New Datos.DParametro("@tcuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@tcorden", _tcorden))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001a", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_prAsignacionModificar(ByRef _tcnumi As String, _tccuen As Integer, _tcnumcheq As String, _tcbanco As String, _tcnumoper As String, _tcobservacion As String) As Boolean
        Dim _resultado As Boolean
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)
        't.canumi , t.canombre, t.cacuenta, t.caobs, t.cafact, t.cahact, t.cauact 
        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@tcnumi", _tcnumi))
        _listParam.Add(New Datos.DParametro("@tccuen", _tccuen))
        _listParam.Add(New Datos.DParametro("@tcnumcheq", _tcnumcheq))
        _listParam.Add(New Datos.DParametro("@tcbanco", _tcbanco))
        _listParam.Add(New Datos.DParametro("@tcnumoper", _tcnumoper))
        _listParam.Add(New Datos.DParametro("@tcobservacion", _tcobservacion))
        _listParam.Add(New Datos.DParametro("@tcuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001a", _listParam)

        If _Tabla.Rows.Count > 0 Then
            _tcnumi = _Tabla.Rows(0).Item(0)
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function
    Public Shared Function L_fnGeneralAsigCuenta() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@tcuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001a", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarActivoDisponible() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@tcuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001a", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnListarCuentaContable(cod As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@categoria ", cod))
        _listParam.Add(New Datos.DParametro("@tcuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TC001a", _listParam)

        Return _Tabla
    End Function
#End Region

#Region "Retencion"
    Public Shared Function L_fnRetencionCo(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 25))
        _listParam.Add(New Datos.DParametro("@trid", _numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnRetencion(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 26))
        _listParam.Add(New Datos.DParametro("@trid", _numi))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnGrabarRetencion(fecci As String, quincena As Integer, gestion As Integer, inst As Integer, canero As Integer, factor As Decimal, cupo As Decimal,
                                           totalIng As Double, porcentaje As Double,
                                           totalIngQuin As Double, RetBs As Double,
                                           RetSus As Double, TComb As Double, RComb As Double, TInsu As Double, RInsu As Double,
                                           TRest As Double, RRest As Double, TCont As Double, RCont As Double, TSho As Double, RSho As Double,
                                           TOtSu As Double, ROtSu As Double, TConv As Double, RConv As Double, ROprevConv As Double, TotalD As Double, TotalR As Double,
                                           alm As Integer, usuario As Integer, CheckGrupo As Integer, detalle As DataTable, estRetencion As Integer) As Boolean
        Dim _Tabla As DataTable
        Dim _resultado As Boolean
        Dim _listParam As New List(Of Datos.DParametro)
        '    @tanumi ,@taalm,@tafdoc ,@taven  ,@tatven,
        '@tafvcr ,@taclpr,@tamon ,@taest  ,@taobs ,@tadesc ,@newFecha,@newHora,@tauact,@taproforma
        _listParam.Add(New Datos.DParametro("@tipo", 1))
        _listParam.Add(New Datos.DParametro("@trfecci", fecci))
        _listParam.Add(New Datos.DParametro("@trquin", quincena))
        _listParam.Add(New Datos.DParametro("@trges", gestion))
        _listParam.Add(New Datos.DParametro("@trins", inst))
        _listParam.Add(New Datos.DParametro("@trcan", canero))
        _listParam.Add(New Datos.DParametro("@trfac", factor))
        _listParam.Add(New Datos.DParametro("@trcupo", cupo))
        _listParam.Add(New Datos.DParametro("@trTIng", totalIng))
        _listParam.Add(New Datos.DParametro("@trpor", porcentaje))
        _listParam.Add(New Datos.DParametro("@trIngQuin", totalIngQuin))
        _listParam.Add(New Datos.DParametro("@trRetBs", RetBs))
        _listParam.Add(New Datos.DParametro("@trRetSus", RetSus))
        _listParam.Add(New Datos.DParametro("@trTComb", TComb))
        _listParam.Add(New Datos.DParametro("@trRComb", RComb))
        _listParam.Add(New Datos.DParametro("@trTInsu", TInsu))
        _listParam.Add(New Datos.DParametro("@trRInsu", RInsu))
        _listParam.Add(New Datos.DParametro("@trTRest", TRest))
        _listParam.Add(New Datos.DParametro("@trRRest", RRest))
        _listParam.Add(New Datos.DParametro("@trTCont", TCont))
        _listParam.Add(New Datos.DParametro("@trRCont", RCont))
        _listParam.Add(New Datos.DParametro("@trTSho", TSho))
        _listParam.Add(New Datos.DParametro("@trRSho", RSho))
        _listParam.Add(New Datos.DParametro("@trTOtSu", TOtSu))
        _listParam.Add(New Datos.DParametro("@trROtSu", ROtSu))
        _listParam.Add(New Datos.DParametro("@trTConv", TConv))
        _listParam.Add(New Datos.DParametro("@trRConv", RConv))
        _listParam.Add(New Datos.DParametro("@trRProvConv", ROprevConv))
        _listParam.Add(New Datos.DParametro("@trTotalD", TotalD))
        _listParam.Add(New Datos.DParametro("@trTotalR", TotalR))
        _listParam.Add(New Datos.DParametro("@tralm", alm))
        _listParam.Add(New Datos.DParametro("@trvend", usuario))
        _listParam.Add(New Datos.DParametro("@trTCan", CheckGrupo))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@TR0011", "", detalle))
        _listParam.Add(New Datos.DParametro("@estRet", estRetencion))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)


        If _Tabla.Rows.Count > 0 Then
            _resultado = True
        Else
            _resultado = False
        End If

        Return _resultado
    End Function

    Public Shared Function L_fnGuardarModificado(cod As Integer, dt As DataTable) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 2))
        _listParam.Add(New Datos.DParametro("@TR0013", "", dt))
        _listParam.Add(New Datos.DParametro("@trquin", cod))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnGeneralRetencion() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 3))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnDetalleRetencion(_numi As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 4))
        _listParam.Add(New Datos.DParametro("@trid", _numi))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function CargarCCPagosSaldos(codCan As Integer, codIns As Integer, codPrest As Integer, fec As String, fecF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 12))
        _listParam.Add(New Datos.DParametro("@trcan", codCan))
        _listParam.Add(New Datos.DParametro("@trins", codIns))
        _listParam.Add(New Datos.DParametro("@tralm", codPrest))
        _listParam.Add(New Datos.DParametro("@fechaF", fecF))
        _listParam.Add(New Datos.DParametro("@trfec", fec))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function CargarDeudaActual(codCan As Integer, codIns As Integer, codPrest As Integer, fec As String, fecF As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 33))
        _listParam.Add(New Datos.DParametro("@trcan", codCan))
        _listParam.Add(New Datos.DParametro("@trins", codIns))
        _listParam.Add(New Datos.DParametro("@tralm", codPrest))
        _listParam.Add(New Datos.DParametro("@fechaF", fecF))
        _listParam.Add(New Datos.DParametro("@trfec", fec))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function CargarCCPagosSaldosConAportes(codCan As Integer, codIns As Integer, codPrest As Integer, fec As String, fecF As String, codAporte As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 27))
        _listParam.Add(New Datos.DParametro("@trcan", codCan))
        _listParam.Add(New Datos.DParametro("@trins", codIns))
        _listParam.Add(New Datos.DParametro("@tralm", codPrest))
        _listParam.Add(New Datos.DParametro("@fechaF", fecF))
        _listParam.Add(New Datos.DParametro("@trfec", fec))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@traporte", codAporte))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function CargarCCPagosSaldosDetConAporte(codCan As Integer, codIns As Integer, codPrest As Integer, fec As String, fecF As String, codAporte As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 28))
        _listParam.Add(New Datos.DParametro("@trcan", codCan))
        _listParam.Add(New Datos.DParametro("@trins", codIns))
        _listParam.Add(New Datos.DParametro("@tralm", codPrest))
        _listParam.Add(New Datos.DParametro("@fechaF", fecF))
        _listParam.Add(New Datos.DParametro("@trfec", fec))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _listParam.Add(New Datos.DParametro("@traporte", codAporte))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function CargarCCPagosSaldosDet(codCan As Integer, codIns As Integer, codPrest As Integer, fec As String, fecF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 25))
        _listParam.Add(New Datos.DParametro("@trcan", codCan))
        _listParam.Add(New Datos.DParametro("@trins", codIns))
        _listParam.Add(New Datos.DParametro("@tralm", codPrest))
        _listParam.Add(New Datos.DParametro("@fechaF", fecF))
        _listParam.Add(New Datos.DParametro("@trfec", fec))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001detalle", _listParam)

        Return _Tabla
    End Function

    Public Shared Function CargarCCxSocio(codCan As Integer, codIns As Integer, codPrest As Integer, fec As String, fecF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 29))
        _listParam.Add(New Datos.DParametro("@trcan", codCan))
        _listParam.Add(New Datos.DParametro("@trins", codIns))
        _listParam.Add(New Datos.DParametro("@tralm", codPrest))
        _listParam.Add(New Datos.DParametro("@fechaF", fecF))
        _listParam.Add(New Datos.DParametro("@trfec", fec))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function CargarCCxSocioPagado(codCan As Integer, codIns As Integer, codPrest As Integer, fec As String, fecF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 32))
        _listParam.Add(New Datos.DParametro("@trcan", codCan))
        _listParam.Add(New Datos.DParametro("@trins", codIns))
        _listParam.Add(New Datos.DParametro("@tralm", codPrest))
        _listParam.Add(New Datos.DParametro("@fechaF", fecF))
        _listParam.Add(New Datos.DParametro("@trfec", fec))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function CargarCCxSocioDet(codCan As Integer, codIns As Integer, codPrest As Integer, fec As String, fecF As String) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 30))
        _listParam.Add(New Datos.DParametro("@trcan", codCan))
        _listParam.Add(New Datos.DParametro("@trins", codIns))
        _listParam.Add(New Datos.DParametro("@tralm", codPrest))
        _listParam.Add(New Datos.DParametro("@fechaF", fecF))
        _listParam.Add(New Datos.DParametro("@trfec", fec))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))

        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function TraerTipoPrestamos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 13))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function _CargarPagos(cod As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 17))
        _listParam.Add(New Datos.DParametro("@trcan", cod))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function _CargarPagos2(cod As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 22))
        _listParam.Add(New Datos.DParametro("@trcan", cod))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function _CargarPlanPagos(cod As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 15))
        _listParam.Add(New Datos.DParametro("@trcan", cod))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function _CargarPlanPagos2(cod As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 21))
        _listParam.Add(New Datos.DParametro("@trcan", cod))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function _fnGuargarPlandePagos(codcan As Integer, codins As Integer, banco As String, fecha As String, moneda As Integer, monto As Double, operacion As String, plazo As String, quincena As Integer, tipoCambio As Integer, TPP0011 As DataTable) As Boolean
        Dim _Tabla As DataTable
        Dim res As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 16))
        _listParam.Add(New Datos.DParametro("@trcan", codcan))
        _listParam.Add(New Datos.DParametro("@trins", codins))
        _listParam.Add(New Datos.DParametro("@tpban", banco))
        _listParam.Add(New Datos.DParametro("@trfec", fecha))
        _listParam.Add(New Datos.DParametro("@trquin", moneda))
        _listParam.Add(New Datos.DParametro("@trTotalD", monto))
        _listParam.Add(New Datos.DParametro("@tpope", operacion))
        _listParam.Add(New Datos.DParametro("@tppla", plazo))
        _listParam.Add(New Datos.DParametro("@trid", quincena))
        _listParam.Add(New Datos.DParametro("@trges", tipoCambio))
        _listParam.Add(New Datos.DParametro("@TPP0011", "", TPP0011))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            res = True
        Else
            res = False
        End If
        Return res
    End Function

    Public Shared Function CargarRegistroPlanPago(cod As Integer) As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 18))
        _listParam.Add(New Datos.DParametro("@trcan", cod))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        Return _Tabla
    End Function

#End Region

#Region "Caña Comprometida"
    Public Shared Function L_fnRegistrarCañaComprometida(codCan As Integer, codIns As Integer, fecha As String, gestion As Integer, total As Double) As Boolean
        Dim _Tabla As DataTable
        Dim res As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 5))
        _listParam.Add(New Datos.DParametro("@trcan", codCan))
        _listParam.Add(New Datos.DParametro("@trins", codIns))
        _listParam.Add(New Datos.DParametro("@trfec", fecha))
        _listParam.Add(New Datos.DParametro("@trTCan", gestion))
        _listParam.Add(New Datos.DParametro("@trcupo", total))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            res = True
        Else
            res = False
        End If
        Return res
    End Function

    Public Shared Function L_fnCargarCañaComprometida() As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 7))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function cargarFechaCierre(gestion As Integer, quincena As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 11))
        _listParam.Add(New Datos.DParametro("@trges", gestion))
        _listParam.Add(New Datos.DParametro("@trquin", quincena))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnCargarLiquidacion(codCan As Integer, fecha As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 8))
        _listParam.Add(New Datos.DParametro("@trcan", codCan))
        _listParam.Add(New Datos.DParametro("@trfec", fecha))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        Return _Tabla
    End Function
    Public Shared Function L_fnCargarLiquidacionGuardar(quincena As Integer, gestion As Integer) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 9))
        _listParam.Add(New Datos.DParametro("@trquin", quincena))
        _listParam.Add(New Datos.DParametro("@trges", gestion))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnCargarCupo(codCan As Integer, gestion As String, inicioQuin As String, finQuin As String) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 10))
        _listParam.Add(New Datos.DParametro("@trcan", codCan))
        _listParam.Add(New Datos.DParametro("@trges", gestion))
        _listParam.Add(New Datos.DParametro("@inicioQuin", inicioQuin))
        _listParam.Add(New Datos.DParametro("@finQuin", finQuin))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        Return _Tabla
    End Function

    Public Shared Function L_fnAutorizarRetencion(reten As DataTable) As DataTable
        Dim _Tabla As DataTable
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 14))
        _listParam.Add(New Datos.DParametro("@retenciones", "", reten))

        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        Return _Tabla
    End Function

#End Region

#Region "Plan de pagos"
    Public Shared Function _fnGuargarPagos(IdPlanPago As Integer, TPP00121 As DataTable) As Boolean
        Dim _Tabla As DataTable
        Dim res As Boolean
        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 19))
        _listParam.Add(New Datos.DParametro("@trges", IdPlanPago))
        _listParam.Add(New Datos.DParametro("@TPP00121", "", TPP00121))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        If _Tabla.Rows.Count > 0 Then
            res = True
        Else
            res = False
        End If
        Return res
    End Function

    Public Shared Function _fnCargarPagos() As DataTable
        Dim _Tabla As DataTable

        Dim _listParam As New List(Of Datos.DParametro)

        _listParam.Add(New Datos.DParametro("@tipo", 20))
        _listParam.Add(New Datos.DParametro("@ibuact", L_Usuario))
        _Tabla = D_ProcedimientoConParam("sp_Mam_TR001", _listParam)

        Return _Tabla
    End Function

#End Region
End Class
