Imports System.Drawing.Printing
Imports System.IO
Imports CrystalDecisions.Shared
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports Facturacion
Imports Janus.Windows.GridEX
Imports Logica.AccesoLogica
Imports UTILITIES
'importando librerias api conexion
Imports Newtonsoft.Json
Imports DinoM.LoginResp
Imports DinoM.ConnSiat
Imports DinoM.RespMetodosPago
Imports DinoM.EmisorResp
Imports DinoM.RespTipoDoc
Imports DinoM.UmedidaResp
Imports System.Xml

Public Class F0_PagoCanero
    Dim _Inter As Integer = 0
#Region "Variables Globales"
    Dim _CodCliente As Integer = 0
    Dim _CodEmpleado As Integer = 0
    Dim _CodInstitucion As Integer = 0
    Dim OcultarFact As Integer = 0
    Dim _codeBar As Integer = 1
    Dim _dias As Integer = 0
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Dim FilaSelectLote As DataRow = Nothing
    Dim Table_Producto As DataTable
    Dim G_Lote As Boolean = False '1=igual a mostrar las columnas de lote y fecha de Vencimiento

    Dim dtDescuentos As DataTable = Nothing
    Dim ConfiguracionDescuentoEsXCantidad As Boolean = True
    Public Programa As String
    Dim DescuentoXProveedorList As DataTable = New DataTable

#End Region

    'variables sifac
    Public tokenSifac As String
    Public CodActEco As String
    Public CodProSINs As String
    Public Ume As Integer
    Public preciosifac As Integer
    Public tipoDocumento As Integer
    Public correo As String
    Public _Fecha As Date

    Public CodProducto As String
    Public Cantidad As Integer
    Public PrecioU As Integer
    Public PrecioTot As Double
    Public NombreProd As String
    Public NroFact As Integer
    Public fechaEmision As String

    'variables respuesta emision
    Public codigoRecepcion As String
    Public estadoEmisionEdoc As Integer
    Public fechaEmision1 As String
    Public cuf As String
    Public cuis As String
    Public cufd As String
    Public codigoControl As String
    Public linkCodigoQr As String
    Public codigoError As String
    Public mensajeRespuesta As String
#Region "Metodos Privados"
    Private Sub _IniciarTodo()
        L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        MSuperTabControl.SelectedTabIndex = 0
        'Me.WindowState = FormWindowState.Maximized

        _prValidarLote()
        _prCargarComboLibreriaSucursal(cbSucursal)
        _prCargarComboLibreria(cbCambioDolar, 7, 1)
        _prCargarComboLibreria(cbFormaPago, 12, 1)
        cbCambioDolar.Value = 1
        cbFormaPago.Value = 1
        'lbTipoMoneda.Visible = False
        swMoneda.Visible = True
        P_prCargarVariablesIndispensables()
        _prCargarPagos()
        _prInhabiliitar()
        grVentas.Focus()
        Me.Text = "VENTAS"
        Dim blah As New Bitmap(New Bitmap(My.Resources.compra), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        _prAsignarPermisos()
        _prValidadFactura()
        _prCargarNameLabel()

        Programa = P_Principal.btVentVenta.Text
    End Sub

    Public Sub _prCargarNameLabel()
        Dim dt As DataTable = L_fnNameLabel()
        If (dt.Rows.Count > 0) Then
            _codeBar = 1 'dt.Rows(0).Item("codeBar")
        End If
    End Sub
    Sub _prValidadFactura()
        'If (OcultarFact = 1) Then
        '    GroupPanelFactura2.Visible = False
        '    GroupPanelFactura.Visible = False
        'Else
        '    GroupPanelFactura2.Visible = True
        '    GroupPanelFactura.Visible = True
        'End If

    End Sub
    Public Sub _prValidarLote()
        Dim dt As DataTable = L_fnPorcUtilidad()
        If (dt.Rows.Count > 0) Then
            Dim lot As Integer = dt.Rows(0).Item("VerLote")
            OcultarFact = dt.Rows(0).Item("VerFactManual")
            If (lot = 1) Then
                G_Lote = True
            Else
                G_Lote = False
            End If

        End If
    End Sub
    Private Sub _prCargarComboLibreriaSucursal(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarSucursales()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("aanumi").Width = 60
            .DropDownList.Columns("aanumi").Caption = "COD"
            .DropDownList.Columns.Add("aabdes").Width = 500
            .DropDownList.Columns("aabdes").Caption = "SUCURSAL"
            .ValueMember = "aanumi"
            .DisplayMember = "aabdes"
            .DataSource = dt
            .Refresh()
        End With
    End Sub
    Private Sub _prAsignarPermisos()

        Dim dtRolUsu As DataTable = L_prRolDetalleGeneral(gi_userRol, _nameButton)

        Dim show As Boolean = dtRolUsu.Rows(0).Item("ycshow")
        Dim add As Boolean = dtRolUsu.Rows(0).Item("ycadd")
        Dim modif As Boolean = dtRolUsu.Rows(0).Item("ycmod")
        Dim del As Boolean = dtRolUsu.Rows(0).Item("ycdel")

        If add = False Then
            btnNuevo.Visible = False
        End If
        If modif = False Then
            btnModificar.Visible = True
        End If
        If del = False Then
            btnEliminar.Visible = False
        End If
    End Sub
    Private Sub _prInhabiliitar()

        tbCodigo.ReadOnly = True
        tbCliente.ReadOnly = True
        tbVendedor.ReadOnly = False
        tbFechaPago.IsInputReadOnly = True
        tbFechaPago.Enabled = False
        swMoneda.IsReadOnly = True




        btnModificar.Enabled = True

        btnGrabar.Enabled = False
        btnNuevo.Enabled = True
        'btnEliminar.Enabled = True

        'If grVentas.GetValue("taest") = 1 Then
        '    btnEliminar.Enabled = True
        'Else
        '    btnEliminar.Enabled = False
        'End If



        grVentas.Enabled = True
        PanelNavegacion.Enabled = True
        'grdetalle.RootTable.Columns("img").Visible = False


        TbNombre2.ReadOnly = True
        cbSucursal.ReadOnly = True
        FilaSelectLote = Nothing
    End Sub
    Private Sub _prhabilitar()
        'btnContabilizar.Visible = False
        grVentas.Enabled = False
        tbCodigo.ReadOnly = False

        tbFechaPago.IsInputReadOnly = False
        tbFechaPago.Enabled = True

        swMoneda.IsReadOnly = False

        btnGrabar.Enabled = False

        TbNombre2.ReadOnly = False



        If (tbCodigo.Text.Length > 0) Then
            cbSucursal.ReadOnly = True
        Else
            cbSucursal.ReadOnly = False

        End If


        dtDescuentos = L_fnListarDescuentosTodos()
    End Sub






    Public Sub _prFiltrar()
        'cargo el buscador
        Dim _Mpos As Integer
        _prCargarPagos()
        If grVentas.RowCount > 0 Then
            _Mpos = 0
            grVentas.Row = _Mpos
        Else
            _Limpiar()
            LblPaginacion.Text = "0/0"
        End If
    End Sub
    Private Sub _Limpiar()

        tbCodigo.Clear()
        tbCliente.Clear()
        tbVendedor.Clear()

        swMoneda.Value = False

        _CodCliente = 0
        _CodEmpleado = 0
        tbFechaPago.Value = Now.Date
        tbfechadeudal.Value = Now.Date
        _prCargarDetalleVenta(-1)
        MSuperTabControl.SelectedTabIndex = 0

        correo = ""
        tipoDocumento = -1

        With grdetalle.RootTable.Columns("img")
            .Width = 80
            .Caption = "Eliminar"
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = True
        End With
        '_prAddDetalleVenta()

        FilaSelectLote = Nothing

        ' tbCliente.Focus()
        ' Table_Producto = Nothing
    End Sub
    Public Sub _prMostrarRegistro(_N As Integer)
        Dim cod As String
        '' grVentas.Row = _N
        '     a.tanumi ,a.taalm ,a.tafdoc ,a.taven ,vendedor .yddesc as vendedor ,a.tatven ,a.tafvcr ,a.taclpr,
        'cliente.yddesc as cliente ,a.tamon ,IIF(tamon=1,'Boliviano','Dolar') as moneda,a.taest ,a.taobs ,
        'a.tadesc ,a.tafact ,a.tahact ,a.tauact,(Sum(b.tbptot)-a.tadesc ) as total,taproforma

        With grVentas

            cbSucursal.Value = .GetValue("pralm")
            tbFechaPago.Value = .GetValue("prfec")
            _CodInstitucion = .GetValue("prinst")
            tbVendedor.Text = .GetValue("nomInst")
            tbinteres.Text = .GetValue("printe")
            _CodCliente = .GetValue("prcan")
            tbCliente.Text = .GetValue("ydrazonsocial")
            swMoneda.Value = .GetValue("prmon")
            cod = .GetValue("prnumi")

            'If grVentas.GetValue("taest") = 1 Then

            '    btnEliminar.Enabled = True
            'Else

            '    btnEliminar.Enabled = False
            'End If

            'lbFecha.Text = CType(.GetValue("tafact"), Date).ToString("dd/MM/yyyy")
            'lbHora.Text = .GetValue("tahact").ToString
            'lbUsuario.Text = .GetValue("tauact").ToString

        End With

        _prCargarDetallePago(cod)
        '_prCargarDetalleVenta(tbCodigo.Text)

        '_prCalcularPrecioTotal()

    End Sub

    Private Sub _prCargarDetallePago(_numi As String)
        Dim dt As New DataTable
        dt = L_fnDetallePagos(_numi)
        grdetalle.DataSource = dt
        grdetalle.RetrieveStructure()
        grdetalle.AlternatingColors = True

        With grdetalle.RootTable.Columns("numi")
            .Width = 50
            .Caption = "CODIGO"
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("tanumi")
            .Width = 80
            .Caption = "CODIGO"
            .Visible = True
        End With

        With grdetalle.RootTable.Columns("prnumi")
            .Width = 90
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbdetalle")
            .Width = 450
            .Caption = "DETALLE"
            .Visible = True
        End With
        With grdetalle.RootTable.Columns("tbdebe")
            .Width = 180
            .TextAlignment = 3
            .Caption = "DEBE"
            .Visible = True
            .FormatString = "0.00"
        End With
        With grdetalle.RootTable.Columns("tbapag")
            .Width = 180
            .TextAlignment = 3
            .Caption = "A PAGAR"
            .Visible = True
            .FormatString = "0.00"
        End With
        With grdetalle.RootTable.Columns("tbapor")
            .Width = 180
            .TextAlignment = 3
            .Caption = "APORTE"
            .Visible = True
            .FormatString = "0.00"
        End With
        With grdetalle.RootTable.Columns("tbtot")
            .Width = 180
            .TextAlignment = 3
            .Caption = "TOTAL"
            .Visible = True
            .FormatString = "0.00"
        End With



        Dim Total As String = grVentas.RowCount
        Dim Actual As String = grVentas.Row + 1
        LblPaginacion.Text = Actual + "/" + Total
    End Sub
    Private Sub _prCargarDetalleVenta(_numi As String)
        Dim dt As New DataTable
        dt = L_fnDetalleVenta(_numi)
        grdetalle.DataSource = dt
        grdetalle.RetrieveStructure()
        grdetalle.AlternatingColors = True
        ' a.tbnumi ,a.tbtv1numi ,a.tbty5prod ,b.yfcdprod1 as producto,a.tbest ,a.tbcmin ,a.tbumin ,Umin .ycdes3 as unidad,a.tbpbas ,a.tbptot,a.tbdesc ,a.tbobs ,
        'a.tbfact ,a.tbhact ,a.tbuact

        With grdetalle.RootTable.Columns("tbnumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("tbtv1numi")
            .Width = 90
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbty5prod")
            .Width = 90
            .Visible = False
        End With
        'If _codeBar = 2 Then
        '    With grdetalle.RootTable.Columns("yfcbarra")
        '        .Caption = "Cod.Barra"
        '        .Width = 100
        '        .Visible = True

        '    End With
        'Else
        '    With grdetalle.RootTable.Columns("yfcbarra")
        '        .Caption = "Cod.Barra"
        '        .Width = 100
        '        .Visible = False
        '    End With
        'End If

        With grdetalle.RootTable.Columns("Codigo")
            .Caption = "Código".ToUpper
            .Width = 100
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("yfcbarra")
            .Caption = "C.B.".ToUpper
            .Width = 40
            .Visible = gb_CodigoBarra
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("producto")
            .Caption = "Productos".ToUpper
            .Width = 440
            .Visible = True
        End With

        With grdetalle.RootTable.Columns("tbest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("tbcmin")
            .Width = 110
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00000"
            .Caption = "Cantidad".ToUpper
        End With
        With grdetalle.RootTable.Columns("tbumin")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("unidad")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "UN.".ToUpper
        End With
        With grdetalle.RootTable.Columns("tbpbas")
            .Width = 90
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Precio U.".ToUpper
        End With
        With grdetalle.RootTable.Columns("tbptot")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00"
            .Caption = "SubTotal".ToUpper
        End With
        With grdetalle.RootTable.Columns("tbporc")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00"
            .Caption = "P.Desc(%)".ToUpper

        End With
        With grdetalle.RootTable.Columns("tbdesc")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00"
            .Caption = "M.Desc".ToUpper
        End With
        With grdetalle.RootTable.Columns("tbtotdesc")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Total".ToUpper
        End With
        With grdetalle.RootTable.Columns("tbobs")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbpcos")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbptot2")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbfact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbhact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbuact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("estado")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("img")
            .Width = 80
            .Caption = "Eliminar".ToUpper
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = False
        End With
        If (G_Lote = True) Then
            With grdetalle.RootTable.Columns("tblote")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
                .Caption = "LOTE"
            End With
            With grdetalle.RootTable.Columns("tbfechaVenc")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
                .Caption = "FECHA VENC."
                .FormatString = "yyyy/MM/dd"
            End With

        Else
            With grdetalle.RootTable.Columns("tblote")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
                .Caption = "LOTE"
            End With
            With grdetalle.RootTable.Columns("tbfechaVenc")
                .Width = 120
                .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
                .Visible = False
                .Caption = "FECHA VENC."
                .FormatString = "yyyy/MM/dd"
            End With
        End If
        With grdetalle.RootTable.Columns("stock")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbfamilia")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("tbProveedorId")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        'With grdetalle.RootTable.Columns("yfclot")
        '    .Width = 120
        '    .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        '    .Visible = False
        'End With

        With grdetalle.RootTable.Columns("ygcodact")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("ygcodu")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("ygcodsin")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With


        With grdetalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With
    End Sub

    Private Sub _prCargarPagos()
        Dim dt As New DataTable
        dt = L_fnGeneralPagos()
        grVentas.DataSource = dt
        grVentas.RetrieveStructure()
        grVentas.AlternatingColors = True
    End Sub

    Private Sub _prCargarVenta()
        Dim dt As New DataTable
        dt = L_fnGeneralVenta(gi_userSuc)
        grVentas.DataSource = dt
        grVentas.RetrieveStructure()
        grVentas.AlternatingColors = True
        '   a.tamon ,IIF(tamon=1,'Boliviano','Dolar') as moneda,a.taest ,a.taobs ,
        'a.tadesc ,a.tafact ,a.tahact ,a.tauact,(Sum(b.tbptot)-a.tadesc ) as total

        With grVentas.RootTable.Columns("tanumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = True

        End With

        With grVentas.RootTable.Columns("taalm")
            .Width = 90
            .Visible = False
        End With

        With grVentas.RootTable.Columns("taproforma")
            .Width = 90
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tafdoc")
            .Width = 90
            .Visible = True
            .Caption = "FECHA"
        End With

        With grVentas.RootTable.Columns("aabdes")
            .Width = 90
            .Visible = False
            .Caption = "FECHA"
        End With

        With grVentas.RootTable.Columns("taven")
            .Width = 160
            .Visible = False
        End With
        With grVentas.RootTable.Columns("vendedor")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "VENDEDOR"
        End With
        With grVentas.RootTable.Columns("cliente")
            .Width = 250
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "CLIENTE"
        End With
        With grVentas.RootTable.Columns("institucion")
            .Width = 250
            .Visible = True
            .Caption = "INSTITUCION".ToUpper
        End With


        With grVentas.RootTable.Columns("tatven")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        With grVentas.RootTable.Columns("tafvcr")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("taclpr")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With


        With grVentas.RootTable.Columns("tamon")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("moneda")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
            .Caption = "MONEDA"
        End With
        With grVentas.RootTable.Columns("taobs")
            .Width = 200
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "OBSERVACION"
        End With
        With grVentas.RootTable.Columns("tadesc")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("taest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("taice")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tafact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tahact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tauact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("NroCaja")
            .Width = 100
            .Caption = "COD. Institucion"
            .Visible = False
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With
        With grVentas.RootTable.Columns("total")
            .Width = 150
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "TOTAL"
            .FormatString = "0.00"
        End With
        With grVentas
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla

        End With

        If (dt.Rows.Count <= 0) Then
            _prCargarDetalleVenta(-1)
        End If
    End Sub
    Public Sub _prGuardar()

        If _ValidarCampos() = False Then
            Exit Sub
        End If

        If (tbCodigo.Text = String.Empty) Then

            _GuardarNuevo()

        Else
            'If (tbCodigo.Text <> String.Empty) Then
            '    _prGuardarModificado()
            '    ''    _prInhabiliitar() RODRIGO RLA

            'End If
        End If
    End Sub
    Public Sub actualizarSaldoSinLote(ByRef dt As DataTable)
        'b.yfcdprod1 ,a.iclot ,a.icfven  ,a.iccven 

        '      a.tbnumi ,a.tbtv1numi ,a.tbty5prod ,b.yfcdprod1 as producto,a.tbest ,a.tbcmin ,a.tbumin ,Umin .ycdes3 as unidad,a.tbpbas ,a.tbptot ,a.tbobs ,
        'a.tbpcos,a.tblote ,a.tbfechaVenc , a.tbptot2, a.tbfact ,a.tbhact ,a.tbuact,1 as estado,Cast(null as Image) as img,
        'Cast (0 as decimal (18,2)) as stock
        Dim _detalle As DataTable = CType(grdetalle.DataSource, DataTable)

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim sum As Integer = 0
            Dim codProducto As Integer = dt.Rows(i).Item("yfnumi")
            For j As Integer = 0 To grdetalle.RowCount - 1 Step 1
                grdetalle.Row = j
                Dim estado As Integer = grdetalle.GetValue("estado")
                If (estado = 0) Then
                    If (codProducto = grdetalle.GetValue("tbty5prod")) Then
                        sum = sum + grdetalle.GetValue("tbcmin")
                    End If
                End If
            Next
            dt.Rows(i).Item("stock") = dt.Rows(i).Item("stock") - sum
        Next

    End Sub

    Private Sub _prCargarProductos(_cliente As String)
        If (cbSucursal.SelectedIndex < 0) Then
            Return
        End If
        Dim nombreGrupos As DataTable = L_fnNameLabel()
        Dim dt As New DataTable

        If (G_Lote = True) Then
            dt = L_fnListarProductos(cbSucursal.Value, _cliente)
        Else
            dt = L_fnListarProductosSinLote(cbSucursal.Value, _cliente, CType(grdetalle.DataSource, DataTable))
        End If




    End Sub





    Public Sub actualizarSaldo(ByRef dt As DataTable, CodProducto As Integer)
        'b.yfcdprod1 ,a.iclot ,a.icfven  ,a.iccven 

        '      a.tbnumi ,a.tbtv1numi ,a.tbty5prod ,b.yfcdprod1 as producto,a.tbest ,a.tbcmin ,a.tbumin ,Umin .ycdes3 as unidad,a.tbpbas ,a.tbptot ,a.tbobs ,
        'a.tbpcos,a.tblote ,a.tbfechaVenc , a.tbptot2, a.tbfact ,a.tbhact ,a.tbuact,1 as estado,Cast(null as Image) as img,
        'Cast (0 as decimal (18,2)) as stock
        Dim _detalle As DataTable = CType(grdetalle.DataSource, DataTable)

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim lote As String = dt.Rows(i).Item("iclot")
            Dim FechaVenc As Date = dt.Rows(i).Item("icfven")
            Dim sum As Integer = 0
            For j As Integer = 0 To _detalle.Rows.Count - 1
                Dim estado As Integer = _detalle.Rows(j).Item("estado")
                If (estado = 0) Then
                    If (lote = _detalle.Rows(j).Item("tblote") And
                        FechaVenc = _detalle.Rows(j).Item("tbfechaVenc") And CodProducto = _detalle.Rows(j).Item("tbty5prod")) Then
                        sum = sum + _detalle.Rows(j).Item("tbcmin")
                    End If
                End If
            Next
            dt.Rows(i).Item("iccven") = dt.Rows(i).Item("iccven") - sum
        Next

    End Sub



    Private Sub _prAddDetalleVenta()
        '   a.tbnumi ,a.tbtv1numi ,a.tbty5prod ,b.yfcdprod1 as producto,a.tbest ,a.tbcmin ,a.tbumin ,Umin .ycdes3 as unidad,a.tbpbas ,a.tbptot ,a.tbobs ,
        'a.tbpcos,a.tblote ,a.tbfechaVenc , a.tbptot2, a.tbfact ,a.tbhact ,a.tbuact,1 as estado,Cast(null as Image) as img
        Dim Bin As New MemoryStream
        Dim img As New Bitmap(My.Resources.delete, 28, 28)
        img.Save(Bin, Imaging.ImageFormat.Png)
        CType(grdetalle.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, 0, 0, "", 0, "", 0, 0, 0, "", 0, 0, 0, 0, 0, "", 0, "20170101", CDate("2017/01/01"), 0, Now.Date, "", "", 0, Bin.GetBuffer, 0, 0, 0)
    End Sub

    Public Function _fnSiguienteNumi()
        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
        Dim rows() As DataRow = dt.Select("tbnumi=MAX(tbnumi)")
        If (rows.Count > 0) Then
            Return rows(rows.Count - 1).Item("tbnumi")
        End If
        Return 1
    End Function
    Public Function _fnAccesible()
        Return tbFechaPago.IsInputReadOnly = False
    End Function

    Private Sub _HabilitarFocoDetalle(fila As Integer)
        _prCargarProductos(Str(_CodCliente))
        grdetalle.Focus()
        grdetalle.Row = fila
        grdetalle.Col = 2
    End Sub

    Public Sub _fnObtenerFilaDetalle(ByRef pos As Integer, numi As Integer)
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _numi As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tanumi")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next

    End Sub



    Public Function _fnExisteProducto(idprod As Integer) As Boolean
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _idprod As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbty5prod")
            Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado")
            If (_idprod = idprod And estado >= 0) Then

                Return True
            End If
        Next
        Return False
    End Function

    Public Function _fnExisteProductoConLote(idprod As Integer, lote As String, fechaVenci As Date) As Boolean
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _idprod As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbty5prod")
            Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado")
            '          a.tbnumi ,a.tbtv1numi ,a.tbty5prod ,b.yfcdprod1 as producto,a.tbest ,a.tbcmin ,a.tbumin ,Umin .ycdes3 as unidad,a.tbpbas ,a.tbptot ,a.tbobs ,
            'a.tbpcos,a.tblote ,a.tbfechaVenc , a.tbptot2, a.tbfact ,a.tbhact ,a.tbuact,1 as estado,Cast(null as Image) as img,
            'Cast (0 as decimal (18,2)) as stock
            Dim _LoteDetalle As String = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tblote")
            Dim _FechaVencDetalle As Date = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbfechaVenc")
            If (_idprod = idprod And estado >= 0 And lote = _LoteDetalle And fechaVenci = _FechaVencDetalle) Then

                Return True
            End If
        Next
        Return False
    End Function
    Public Sub P_PonerTotal(rowIndex As Integer)



        If (rowIndex < grdetalle.RowCount) Then
            'grdetalle.UpdateData()
            Dim lin As Integer = grdetalle.GetValue("tbnumi")
            Dim pos As Integer = -1
            _fnObtenerFilaDetalle(pos, lin)
            Dim cant As Double = grdetalle.GetValue("tbcmin")
            'Dim cantidad = Format(cant,"0.00")
            Dim uni As Double = grdetalle.GetValue("tbpbas")
            Dim cos As Double = grdetalle.GetValue("tbpcos")
            Dim MontoDesc As Double = grdetalle.GetValue("tbdesc")
            Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
            If (pos >= 0) Then
                Dim TotalUnitario As Double = cant * uni
                Dim TotalCosto As Double = cant * cos
                'grDetalle.SetValue("lcmdes", montodesc)

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = TotalUnitario
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = TotalUnitario - MontoDesc

                grdetalle.SetValue("tbptot", TotalUnitario)
                grdetalle.SetValue("tbtotdesc", TotalUnitario - MontoDesc)

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = TotalCosto
                grdetalle.SetValue("tbptot2", TotalCosto)

                Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")
                If (estado = 1) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                End If
            End If
            _prCalcularPrecioTotal()
        End If
    End Sub

    Public Sub _prCalcularPrecioTotal()


        Dim TotalDescuento As Double = 0
        Dim TotalDescuento1 As Double = 0
        Dim TotalCosto As Double = 0
        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1

            If (dt.Rows(i).Item("estado") >= 0) Then
                TotalDescuento = TotalDescuento + Format(Format((dt.Rows(i).Item("tbpbas") * Convert.ToDecimal(cbCambioDolar.Text)), "0.00") * dt.Rows(i).Item("tbcmin"), "0.00")
                TotalDescuento1 = TotalDescuento1 + Format(dt.Rows(i).Item("tbpbas") * dt.Rows(i).Item("tbcmin"), "0.00")

                TotalCosto = TotalCosto + dt.Rows(i).Item("tbptot2")
            End If
        Next


        'grdetalle.UpdateData()
        Dim montoDo As Decimal



        Dim subtotal = Convert.ToDouble(Format(TotalDescuento, "0.00"))



    End Sub
    Public Sub _prEliminarFila()
        If (grdetalle.Row >= 0) Then
            If (grdetalle.RowCount >= 2) Then
                Dim estado As Integer = grdetalle.GetValue("estado")
                Dim pos As Integer = -1
                Dim lin As Integer = grdetalle.GetValue("tbnumi")
                _fnObtenerFilaDetalle(pos, lin)
                If (estado = 0) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = -2
                End If
                If (estado = 1) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = -1
                End If

                'grdetalle.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grdetalle.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThanOrEqualTo, -3))

                grdetalle.Select()
                grdetalle.UpdateData()
                grdetalle.Col = grdetalle.RootTable.Columns("yfcbarra").Index
                grdetalle.Row = grdetalle.RowCount - 1
                grdetalle.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grdetalle.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThanOrEqualTo, 0))
                _prCalcularPrecioTotal()
            End If
        End If
        'grdetalle.Refetch()
        'grdetalle.Refresh()

    End Sub
    Public Function _ValidarCampos() As Boolean
        Try

            If (_CodCliente <= 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor Seleccione un Cliente con Ctrl+Enter".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                tbCliente.Focus()
                Return False

            End If

            If (_CodEmpleado <= 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor Seleccione un Vendedor con Ctrl+Enter".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                tbVendedor.Focus()
                Return False
            End If
            If (cbSucursal.SelectedIndex < 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor Seleccione una Sucursal".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                cbSucursal.Focus()
                Return False
            End If





            If (grdetalle.RowCount = 1) Then
                grdetalle.Row = grdetalle.RowCount - 1
                If (grdetalle.GetValue("tbty5prod") = 0) Then
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "Por Favor Seleccione  un detalle de producto".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    Return False
                End If

            End If

            If grdetalle.GetValue("pagar").ToString = "" Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Ingrese un monto a pagar".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                Return False
            End If

            Dim total As Double = grdetalle.GetValue("total").ToString
            Dim pagar As Double = grdetalle.GetValue("pagar").ToString

            If (pagar > total) Then

                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "El valor a pagar debe ser menor o igual al total".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                Return False


            End If

            If pagar = 0.00 Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "El monto a pagar debe ser superior a 0".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                Return False
            End If
            'Validación para controlar caducidad de Dosificacion


            Return True
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
            Return False
        End Try

    End Function

    Public Function rearmarDetalle() As DataTable
        Dim dt, dtDetalle, dtSaldos As DataTable
        Dim cantidadRepetido, contar, IdAux As Integer
        Dim ResultadoInventario = False

        dt = CType(grdetalle.DataSource, DataTable)
        'Ordena el detalle por codigo importante
        dt.DefaultView.Sort = "tbty5prod ASC"
        dt = dt.DefaultView.ToTable
        dtDetalle = dt.Copy
        dtDetalle.Clear()
        contar = 0
        Try
            For i As Integer = 0 To dt.Rows.Count - 1 Step 1
                Dim codProducto As Integer = dt.Rows(i).Item("tbty5prod")
                dt.DefaultView.RowFilter = "tbty5prod =  '" + codProducto.ToString() + "'"
                cantidadRepetido = dt.DefaultView.Count()
                If IdAux <> codProducto Then
                    contar = 1
                Else
                    contar += 1
                End If
                IdAux = codProducto

                'Evita llamar a saldo cada iteracion
                If contar = 1 Then
                    dtSaldos = L_fnObteniendoSaldosTI001(codProducto, 1)
                    dtSaldos.DefaultView.Sort = "icfven ASC"
                    dtSaldos = dtSaldos.DefaultView.ToTable
                End If
                'dtSaldos.DefaultView.RowFilter = "iccven >  '" + 0.ToString() + "'"
                'dtSaldos = dtSaldos.DefaultView.ToTable
                Dim cantidad As Double = dt.Rows(i).Item("tbcmin")
                Dim saldo As Double = cantidad
                Dim estado As Integer = dt.Rows(i).Item("estado")
                Dim k As Integer = 0
                If (estado >= 0) Then
                    If (dtSaldos.Rows.Count <= 0) Then
                        dtDetalle.ImportRow(dt.Rows(i))
                    Else
                        While (k <= dtSaldos.Rows.Count - 1 And saldo > 0)

                            Dim inventario As Double = dtSaldos.Rows(k).Item("iccven")
                            If (inventario >= saldo) Then
                                dtDetalle.ImportRow(dt.Rows(i))
                                Dim pos As Integer = dtDetalle.Rows.Count - 1

                                Dim precio As Double = dtDetalle.Rows(pos).Item("tbpbas")
                                Dim total As Decimal = CStr(Format(precio * saldo, "####0.00"))

                                dtDetalle.Rows(pos).Item("tbptot") = total
                                dtDetalle.Rows(pos).Item("tbtotdesc") = total - dtDetalle.Rows(pos).Item("tbdesc")
                                'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = total
                                'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = saldo
                                dtDetalle.Rows(pos).Item("tbcmin") = saldo

                                Dim precioCosto As Double = dtDetalle.Rows(pos).Item("tbpcos")
                                dtDetalle.Rows(pos).Item("tbptot2") = precioCosto * saldo
                                dtDetalle.Rows(pos).Item("tblote") = dtSaldos.Rows(k).Item("iclot")
                                dtDetalle.Rows(pos).Item("tbfechaVenc") = dtSaldos.Rows(k).Item("icfven")
                                dtSaldos.Rows(k).Item("iccven") = inventario - saldo
                                saldo = 0

                            Else
                                'Cuando el Invetanrio es menor
                                If (k <= dtSaldos.Rows.Count - 1 And inventario > 0) Then

                                    dtDetalle.ImportRow(dt.Rows(i))
                                    Dim pos As Integer = dtDetalle.Rows.Count - 1

                                    Dim precio As Double = dtDetalle.Rows(pos).Item("tbpbas")
                                    Dim total As Decimal = CStr(Format(precio * inventario, "####0.00"))
                                    dtDetalle.Rows(pos).Item("tbptot") = total
                                    dtDetalle.Rows(pos).Item("tbtotdesc") = total - dtDetalle.Rows(pos).Item("tbdesc")
                                    'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = total
                                    'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = inventario
                                    dtDetalle.Rows(pos).Item("tbcmin") = inventario

                                    Dim precioCosto As Double = dtDetalle.Rows(pos).Item("tbpcos")
                                    dtDetalle.Rows(pos).Item("tbptot2") = precioCosto * inventario
                                    dtDetalle.Rows(pos).Item("tblote") = dtSaldos.Rows(k).Item("iclot")
                                    dtDetalle.Rows(pos).Item("tbfechaVenc") = dtSaldos.Rows(k).Item("icfven")

                                    saldo = saldo - inventario
                                    'Actualiza el inventario en la Tabla
                                    dtSaldos.Rows(k).Item("iccven") = dtSaldos.Rows(k).Item("iccven") - inventario
                                End If
                            End If
                            k += 1
                        End While
                        If saldo <> 0 Then
                            dtDetalle.ImportRow(dt.Rows(i))
                            Dim pos As Integer = dtDetalle.Rows.Count - 1
                            Dim precio As Double = dtDetalle.Rows(pos).Item("tbpbas")
                            Dim total As Decimal = CStr(Format(precio * saldo, "####0.00"))
                            dtDetalle.Rows(pos).Item("tbptot") = total
                            dtDetalle.Rows(pos).Item("tbtotdesc") = total - dtDetalle.Rows(pos).Item("tbdesc")
                            dtDetalle.Rows(pos).Item("tbcmin") = saldo
                            Dim precioCosto As Double = dtDetalle.Rows(pos).Item("tbpcos")
                            dtDetalle.Rows(pos).Item("tbptot2") = precioCosto * saldo
                            dtDetalle.Rows(pos).Item("tblote") = dtSaldos.Rows(k - 1).Item("iclot")
                            dtDetalle.Rows(pos).Item("tbfechaVenc") = dtSaldos.Rows(k - 1).Item("icfven")
                            saldo = 0
                        End If
                    End If
                End If
            Next
            Return dtDetalle
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
            Return dtDetalle
        End Try
    End Function
    Private Function _prExisteStockParaProducto() As Boolean
        Dim dtSaldos As DataTable
        Dim aux As Integer = 0
        Dim detalle = CType(grdetalle.DataSource, DataTable)
        Dim Lmensaje As List(Of String) = New List(Of String)
        ' detalle.DefaultView.RowFilter = "estado >= '" + 0.ToString() + "'"
        detalle = detalle.DefaultView.ToTable
        For Each fila As DataRow In detalle.Rows
            Dim idProducto = fila.Item("numi")
            If aux <> idProducto Then
                'dtSaldos = L_fnObteniendoSaldosTI001(fila.Item("tbty5prod"), cbSucursal.Value)
                'Dim inventario = dtSaldos.Compute("SUM(iccven)", String.Empty)

                'detalle.DefaultView.RowFilter = "tbty5prod = '" + fila.Item("tbty5prod").ToString() + "'"
                'Dim productoRepeditos = detalle.DefaultView.ToTable
                'Dim saldo = productoRepeditos.Compute("SUM(tbcmin)", String.Empty)
                ''Dim saldo = fila.Item("tbcmin")
                Dim total As Double = fila.Item("total").ToString
                Dim pagar As Double = fila.Item("pagar").ToString
                If pagar > total Then
                    Dim mensaje As String = "Existen montos a pagar mayor al total - PRODUCTO: " + fila.Item("tbdetalle")
                    Lmensaje.Add(mensaje)
                    'Throw New Exception("No existe stock para el producto:" + fila.Item("producto") + " stock actual =" + inventario)
                End If
            End If
            aux = idProducto
            'dtSaldos.Select = "icfven ASC"
            'dtSaldos = dtSaldos.DefaultView.ToTable
        Next
        If Lmensaje.Count > 0 Then
            Dim mensaje = ""
            For Each item As String In Lmensaje
                mensaje = mensaje + "- " + item + vbCrLf
            Next
            MostrarMensajeError(mensaje)
            Return False
        End If
        Return True
    End Function

    Public Sub _GuardarNuevo()
        Try
            L_fnPagarCuenta(tbFechaPago.Value.ToString("yyyy-MM-dd"), _CodCliente, _CodInstitucion, tbinteres.Text, cbFormaPago.Value, cbSucursal.Value, If(swMoneda.Value = True, 1, 0), CType(grdetalle.DataSource, DataTable))
            _Limpiar()
            _prCargarPagos()
            _prSalir()

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Pago ".ToUpper + tbCodigo.Text + " Grabado con Exito.".ToUpper,
                                                  img, 2000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter
                                                  )

            'Dim numi As String = ""
            'Dim tabla As DataTable = L_fnMostrarMontos(0)
            'Dim factura = gb_FacturaEmite

            '''Verifica si existe estock para los productos
            'If _prExisteStockParaProducto() Then


            '    Dim dtDetalle As DataTable = rearmarDetalle()
            '        'Dim res As Boolean = L_fnGrabarVenta(numi, "", tbFechaVenta.Value.ToString("yyyy/MM/dd"), gi_userNumi,
            '        '                                     IIf(swTipoVenta.Value = True, 1, 0), IIf(swTipoVenta.Value = True,
            '        '                                    Now.Date.ToString("yyyy/MM/dd"), tbFechaVenc.Value.ToString("yyyy/MM/dd")),
            '        '                                     _CodCliente, IIf(swMoneda.Value = True, 1, 0),
            '        '                                      tbObservacion.Text, tbMdesc.Value, tbIce.Value, tbTotalBs.Text,
            '        '                                      dtDetalle, cbSucursal.Value, 0, tabla, _CodEmpleado, Programa)
            '        If tbCodigo.Text <> String.Empty Then 'res Then
            '            'res = P_fnGrabarFacturarTFV001(numi)
            '            'Emite factura
            '            If (gb_FacturaEmite) Then

            '            Else
            '                '_prImiprimirNotaVenta(numi)
            '            End If
            '            '_Limpiar()
            '            _prCargarVenta()
            '            _prSalir()


            '            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            '            ToastNotification.Show(Me, "Código de Venta ".ToUpper + tbCodigo.Text + " Grabado con Exito.".ToUpper,
            '                                      img, 2000,
            '                                      eToastGlowColor.Green,
            '                                      eToastPosition.TopCenter
            '                                      )



            '            Table_Producto = Nothing

            '        Else
            '            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            '            ToastNotification.Show(Me, "La Venta no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

            '        End If

            '    Else
            '    'MessageBox.Show(mensajeRespuesta)
            'End If


        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try


    End Sub

    Private Sub _prGuardarModificado()
        '    Dim tabla As DataTable = L_fnMostrarMontos(0)

        '    Dim dtDetalle As DataTable = rearmarDetalle()
        '    Dim res As Boolean = L_fnModificarVenta(tbCodigo.Text, tbFechaVenta.Value.ToString("yyyy/MM/dd"), gi_userNumi, IIf(swTipoVenta.Value = True, 1, 0),
        '                                            IIf(swTipoVenta.Value = True, Now.Date.ToString("yyyy/MM/dd"), tbFechaVenc.Value.ToString("yyyy/MM/dd")),
        '                                            _CodCliente, IIf(swMoneda.Value = True, 1, 0), tbObservacion.Text, tbMdesc.Value, tbIce.Value, tbTotalBs.Text, dtDetalle, cbSucursal.Value, 0, tabla, _CodEmpleado, Programa)

        '    'numi, "", tbFechaVenta.Value.ToString("yyyy/MM/dd"), gi_userNumi,
        '    '                                                 IIf(swTipoVenta.Value = True, 1, 0), IIf(swTipoVenta.Value = True,
        '    '                                                Now.Date.ToString("yyyy/MM/dd"), tbFechaVenc.Value.ToString("yyyy/MM/dd")),
        '    '                                                 _CodCliente, IIf(swMoneda.Value = True, 1, 0),
        '    '                                                  tbObservacion.Text, tbMdesc.Value, tbIce.Value, tbTotalBs.Text,
        '    '                                                  dtDetalle, cbSucursal.Value, 0, tabla, _CodEmpleado, Programa)
        '    If res Then
        '        'If (gb_FacturaEmite) Then
        '        '    L_fnEliminarDatos("TFV001", "fvanumi=" + tbCodigo.Text.Trim)
        '        '    L_fnEliminarDatos("TFV0011", "fvbnumi=" + tbCodigo.Text.Trim)
        '        '    P_fnGenerarFactura(tbCodigo.Text.Trim)
        '        'End If
        '        _prImiprimirNotaVenta(tbCodigo.Text)
        '        Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
        '        ToastNotification.Show(Me, "Código de Venta ".ToUpper + tbCodigo.Text + " Modificado con Exito.".ToUpper,
        '                                  img, 2000,
        '                                  eToastGlowColor.Green,
        '                                  eToastPosition.TopCenter
        '                                  )
        '        _prCargarVenta()
        '        _prSalir()
        '    Else
        '        Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
        '        ToastNotification.Show(Me, "La Venta no pudo ser Modificada".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        '    End If
    End Sub
    Private Sub _prSalir()
        If btnGrabar.Enabled = True Then
            _prInhabiliitar()

            If grVentas.RowCount > 0 Then
                _prMostrarRegistro(0)

            End If
        Else
            Me.Close()
            '_modulo.Select()
        End If
    End Sub
    Public Sub _prCargarIconELiminar()
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim Bin As New MemoryStream
            Dim img As New Bitmap(My.Resources.delete, 28, 28)
            img.Save(Bin, Imaging.ImageFormat.Png)
            CType(grdetalle.DataSource, DataTable).Rows(i).Item("img") = Bin.GetBuffer
            grdetalle.RootTable.Columns("img").Visible = True
        Next

    End Sub
    Public Sub _PrimerRegistro()
        Dim _MPos As Integer
        If grVentas.RowCount > 0 Then
            _MPos = 0
            ''   _prMostrarRegistro(_MPos)
            grVentas.Row = _MPos
        End If
    End Sub




    Private Function P_fnValidarFactura() As Boolean
        Return True
    End Function





    Private Function ObtenerFechaLiteral(Fecliteral As String, ciudad As String) As String
        Dim dia, mes, ano As Integer
        Dim mesl As String
        dia = Microsoft.VisualBasic.Left(Fecliteral, 2)
        mes = Microsoft.VisualBasic.Mid(Fecliteral, 4, 2)
        ano = Microsoft.VisualBasic.Mid(Fecliteral, 7, 4)
        If mes = 1 Then
            mesl = "Enero"
        End If
        If mes = 2 Then
            mesl = "Febrero"
        End If
        If mes = 3 Then
            mesl = "Marzo"
        End If
        If mes = 4 Then
            mesl = "Abril"
        End If
        If mes = 5 Then
            mesl = "Mayo"
        End If
        If mes = 6 Then
            mesl = "Junio"
        End If
        If mes = 7 Then
            mesl = "Julio"
        End If
        If mes = 8 Then
            mesl = "Agosto"
        End If
        If mes = 9 Then
            mesl = "Septiembre"
        End If
        If mes = 10 Then
            mesl = "Octubre"
        End If
        If mes = 11 Then
            mesl = "Noviembre"
        End If
        If mes = 12 Then
            mesl = "Diciembre"
        End If
        Fecliteral = ciudad + ", " + dia.ToString + " de " + mesl + " del " + ano.ToString
        Return Fecliteral
    End Function




    Public Function P_fnImageToByteArray(ByVal imageIn As Image) As Byte()
        Dim ms As New System.IO.MemoryStream()
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
        Return ms.ToArray()
    End Function


    Private Function P_fnValidarFacturaVigente() As Boolean
        Dim est As String = L_fnObtenerDatoTabla("TFV001", "fvaest", "fvanumi=" + tbCodigo.Text.Trim)
        Return (est.Equals("True"))
    End Function

    Private Sub P_prCargarVariablesIndispensables()
        If (gb_FacturaEmite) Then
            gi_IVA = CDbl(IIf(L_fnGetIVA().Rows(0).Item("scdebfis").ToString.Equals(""), gi_IVA, L_fnGetIVA().Rows(0).Item("scdebfis").ToString))
            gi_ICE = CDbl(IIf(L_fnGetICE().Rows(0).Item("scice").ToString.Equals(""), gi_ICE, L_fnGetICE().Rows(0).Item("scice").ToString))
        End If

    End Sub






    Private Sub ponerDescripcionProducto(ByRef dt As DataTable)
        For Each fila As DataRow In dt.Rows
            Dim numi As Integer = fila.Item("codProducto")
            Dim dtDP As DataTable = L_fnDetalleProducto(numi)
            Dim des As String = fila.Item("producto") + vbNewLine + vbNewLine
            For Each fila2 As DataRow In dtDP.Rows
                des = des + fila2.Item("yfadesc").ToString + vbNewLine
            Next
            fila.Item("producto") = des
        Next
    End Sub



    Sub _prCargarProductoDeLaProforma(numiProforma As Integer)
        Dim dt As DataTable = L_fnListarProductoProforma(numiProforma)

        '        a.pbnumi ,a.pbtp1numi ,a.pbty5prod ,producto ,a.pbcmin ,a.pbumin ,Umin .ycdes3 as unidad,
        'a.pbpbas ,a.pbptot,a.pbporc,a.pbdesc ,a.pbtotdesc,
        'stock, pcosto
        If (dt.Rows.Count > 0) Then
            CType(grdetalle.DataSource, DataTable).Rows.Clear()
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim numiproducto As Integer = dt.Rows(i).Item("pbty5prod")
                Dim nameproducto As String = dt.Rows(i).Item("producto")
                Dim lote As String = ""
                Dim FechaVenc As Date = Now.Date
                Dim cant As Double = dt.Rows(i).Item("pbcmin")
                Dim iccven As Double = 0
                _prPedirLotesProducto(lote, FechaVenc, iccven, numiproducto, nameproducto, cant)
                _prAddDetalleVenta()
                grdetalle.Row = grdetalle.RowCount - 1
                If (lote <> String.Empty) Then
                    If (cant <= iccven) Then

                        grdetalle.SetValue("tbptot", dt.Rows(i).Item("pbptot"))
                        grdetalle.SetValue("tbtotdesc", dt.Rows(i).Item("pbtotdesc"))
                        grdetalle.SetValue("tbdesc", dt.Rows(i).Item("pbdesc"))
                        grdetalle.SetValue("tbcmin", cant)
                        grdetalle.SetValue("tbptot2", dt.Rows(i).Item("pcosto") * cant)

                    Else
                        Dim tot As Double = dt.Rows(i).Item("pbpbas") * iccven
                        grdetalle.SetValue("tbptot", tot)
                        grdetalle.SetValue("tbtotdesc", tot)
                        grdetalle.SetValue("tbdesc", 0)
                        grdetalle.SetValue("tbcmin", iccven)
                        grdetalle.SetValue("tbptot2", dt.Rows(i).Item("pcosto") * iccven)
                    End If
                    grdetalle.SetValue("tbty5prod", numiproducto)
                    grdetalle.SetValue("producto", nameproducto)
                    grdetalle.SetValue("tbumin", dt.Rows(i).Item("pbumin"))
                    grdetalle.SetValue("unidad", dt.Rows(i).Item("unidad"))
                    grdetalle.SetValue("tbpbas", dt.Rows(i).Item("pbpbas"))


                    If (gb_FacturaIncluirICE) Then
                        grdetalle.SetValue("tbpcos", dt.Rows(i).Item("pcosto"))

                    Else
                        grdetalle.SetValue("tbpcos", 0)
                    End If

                    grdetalle.SetValue("tblote", lote)
                    grdetalle.SetValue("tbfechaVenc", FechaVenc)
                    grdetalle.SetValue("stock", iccven)

                End If

                'grdetalle.Refetch()
                'grdetalle.Refresh()


            Next

            grdetalle.Select()
            _prCalcularPrecioTotal()
        End If
    End Sub
    Public Sub _prPedirLotesProducto(ByRef lote As String, ByRef FechaVenc As Date, ByRef iccven As Double, CodProducto As Integer, nameProducto As String, cant As Integer)
        Dim dt As New DataTable
        dt = L_fnListarLotesPorProductoVenta(cbSucursal.Value, CodProducto)  ''1=Almacen
        'b.yfcdprod1 ,a.iclot ,a.icfven  ,a.iccven 
        Dim listEstCeldas As New List(Of Modelo.Celda)
        listEstCeldas.Add(New Modelo.Celda("yfcdprod1,", False, "", 150))
        listEstCeldas.Add(New Modelo.Celda("iclot", True, "LOTE", 150))
        listEstCeldas.Add(New Modelo.Celda("icfven", True, "FECHA VENCIMIENTO", 180, "dd/MM/yyyy"))
        listEstCeldas.Add(New Modelo.Celda("iccven", True, "Stock".ToUpper, 150, "0.00"))
        Dim ef = New Efecto
        ef.tipo = 3
        ef.dt = dt
        ef.SeleclCol = 2
        ef.listEstCeldas = listEstCeldas
        ef.alto = 50
        ef.ancho = 350
        ef.Context = "Producto ".ToUpper + nameProducto + "  cantidad=" + Str(cant)
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        'b.yfcdprod1 ,a.iclot ,a.icfven  ,a.iccven 
        If (bandera = True) Then
            Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
            lote = Row.Cells("iclot").Value
            FechaVenc = Row.Cells("icfven").Value
            iccven = Row.Cells("iccven").Value
        End If
    End Sub
    Private Sub AsignarClienteEmpleado()
        Try
            Dim _tabla11 As DataTable = L_fnListarClientesUsuario(gi_userNumi)
            If _tabla11.Rows.Count > 0 Then
                tbCliente.Text = _tabla11.Rows(0).Item("yddesc")
                _CodCliente = _tabla11.Rows(0).Item("ydnumi") 'Codigo
                tbVendedor.Text = _tabla11.Rows(0).Item("vendedor") 'Codigo
                _CodEmpleado = _tabla11.Rows(0).Item("ydnumivend") 'Codigo
            Else
                Dim dt As DataTable
                dt = L_fnListarClientes()
                If dt.Rows.Count > 0 Then
                    Dim fila As DataRow() = dt.Select("ydnumi =MIN(ydnumi)")
                    tbCliente.Text = fila(0).ItemArray(3)
                    _CodCliente = fila(0).ItemArray(0)
                    tbVendedor.Text = fila(0).ItemArray(9)
                    _CodEmpleado = fila(0).ItemArray(8)
                End If
            End If
        Catch ex As Exception
            MostrarMensajeError("Debe asiganar un vendedor al cliente")
        End Try

    End Sub
    Private Sub MostrarMensajeError(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.WARNING,
                               5000,
                               eToastGlowColor.Red,
                               eToastPosition.TopCenter)

    End Sub
    Private Sub MostrarMensajeOk(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.OK,
                               5000,
                               eToastGlowColor.Green,
                               eToastPosition.TopCenter)
    End Sub
#End Region

#Region "Eventos Formulario"
    Private Sub F0_Ventas_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        _IniciarTodo()

    End Sub
    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        _Limpiar()
        _prhabilitar()
        '' AsignarClienteEmpleado()

        cbSucursal.Value = gi_userSuc
        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
        btnGrabar.Enabled = True
        PanelNavegacion.Enabled = False
        tbCliente.Select()
    End Sub
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _prSalir()

    End Sub


    Private Sub tbCliente_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCliente.KeyDown

        If (_fnAccesible()) Then

            If e.KeyData = Keys.Control + Keys.Enter Then

                Dim dt As DataTable
                'dt = L_fnListarClientes()
                dt = L_fnListarClientesVenta()

                Dim listEstCeldas As New List(Of Modelo.Celda)
                listEstCeldas.Add(New Modelo.Celda("ydnumi,", True, "ID", 50))
                listEstCeldas.Add(New Modelo.Celda("ydcod", True, "COD. CLI", 100))
                listEstCeldas.Add(New Modelo.Celda("ydrazonsocial", True, "RAZÓN SOCIAL", 180))
                listEstCeldas.Add(New Modelo.Celda("yddesc", True, "NOMBRE", 280))
                listEstCeldas.Add(New Modelo.Celda("yddctnum", True, "N. Documento".ToUpper, 150))
                listEstCeldas.Add(New Modelo.Celda("yddirec", True, "DIRECCIÓN", 220))
                listEstCeldas.Add(New Modelo.Celda("ydtelf1", True, "Teléfono".ToUpper, 200))
                listEstCeldas.Add(New Modelo.Celda("ydfnac", True, "F.Nacimiento".ToUpper, 150, "MM/dd,YYYY"))
                listEstCeldas.Add(New Modelo.Celda("ydnumivend,", False, "ID", 50))
                listEstCeldas.Add(New Modelo.Celda("vendedor,", False, "ID", 50))
                listEstCeldas.Add(New Modelo.Celda("yddias", False, "CRED", 50))
                listEstCeldas.Add(New Modelo.Celda("ydnomfac", False, "Nombre Factura", 50))
                listEstCeldas.Add(New Modelo.Celda("ydnit", False, "Nit/CI", 50))
                listEstCeldas.Add(New Modelo.Celda("ydtipdocelec", False, "Nit/CI", 50))
                listEstCeldas.Add(New Modelo.Celda("ydcorreo", False, "Nit/CI", 50))
                listEstCeldas.Add(New Modelo.Celda("ydcompleCi", False, "Nit/CI", 50))
                Dim ef = New Efecto
                ef.tipo = 3
                ef.dt = dt
                ef.SeleclCol = 2
                ef.listEstCeldas = listEstCeldas
                ef.alto = 50
                ef.ancho = 200
                ef.Context = "Seleccione Cliente".ToUpper
                ef.ShowDialog()
                Dim bandera As Boolean = False
                bandera = ef.band
                If (bandera = True) Then
                    Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row

                    _CodCliente = Row.Cells("ydnumi").Value
                    tbCliente.Text = Row.Cells("ydrazonsocial").Value
                    _dias = Row.Cells("yddias").Value

                    tipoDocumento = Row.Cells("ydtipdocelec").Value
                    correo = Row.Cells("ydcorreo").Value
                    'If btnNuevo.Enabled = True Then
                    '    Dim dt1 As DataTable
                    '    dt1 = L_fnListarCaneroInstitucion(_CodCliente)
                    '    Dim row1 As DataRow = dt1.Rows(dt1.Rows.Count - 1)
                    '    tbVendedor.Text = row1("institucion")
                    'End If
                    Dim numiVendedor As Integer = IIf(IsDBNull(Row.Cells("ydnumivend").Value), 0, Row.Cells("ydnumivend").Value)
                    If (numiVendedor > 0) Then
                        ''tbVendedor.Text = Row.Cells("vendedor").Value
                        _CodEmpleado = Row.Cells("ydnumivend").Value

                        'grdetalle.Select()
                        Table_Producto = Nothing
                    Else
                        tbVendedor.Clear()
                        _CodEmpleado = 0
                        tbVendedor.Focus()
                        Table_Producto = Nothing

                    End If
                End If
            End If
        End If
    End Sub
    Private Sub tbVendedor_KeyDown(sender As Object, e As KeyEventArgs) Handles tbVendedor.KeyDown

        If (_fnAccesible()) Then
            If e.KeyData = Keys.Control + Keys.Enter Then

                Dim dt As DataTable

                dt = L_fnListarEmpleado()
                '              a.ydnumi, a.ydcod, a.yddesc, a.yddctnum, a.yddirec
                ',a.ydtelf1 ,a.ydfnac 

                Dim listEstCeldas As New List(Of Modelo.Celda)
                listEstCeldas.Add(New Modelo.Celda("id,", False, "ID", 50))
                listEstCeldas.Add(New Modelo.Celda("codInst", True, "ID", 50))
                listEstCeldas.Add(New Modelo.Celda("nomInst", True, "NOMBRE", 280))
                listEstCeldas.Add(New Modelo.Celda("campo1", True, "N. Cuenta".ToUpper, 150))
                Dim ef = New Efecto
                ef.tipo = 3
                ef.dt = dt
                ef.SeleclCol = 1
                ef.listEstCeldas = listEstCeldas
                ef.alto = 50
                ef.ancho = 350
                ef.Context = "Seleccione Vendedor".ToUpper
                ef.ShowDialog()
                Dim bandera As Boolean = False
                bandera = ef.band
                If (bandera = True) Then
                    Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                    If (IsNothing(Row)) Then
                        tbVendedor.Focus()
                        Return

                    End If
                    _CodEmpleado = Row.Cells("id").Value
                    tbVendedor.Text = Row.Cells("nomInst").Value
                    grdetalle.Select()

                End If

            End If

        End If
    End Sub


    Private Sub grdetalle_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grdetalle.EditingCell
        If (_fnAccesible()) Then
            'Habilitar solo las columnas de Precio, %, Monto y Observación
            'If (e.Column.Index = grdetalle.RootTable.Columns("yfcbarra").Index Or 'e.Column.Index = grdetalle.RootTable.Columns("tbpbas").Index) 
            ''e.Column.Index = grdetalle.RootTable.Columns("tbdesc").Index
            If (e.Column.Index = grdetalle.RootTable.Columns("pagar").Index) Then

                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Else
            e.Cancel = True
        End If

    End Sub


    Private Sub grdetalle_KeyDown(sender As Object, e As KeyEventArgs) Handles grdetalle.KeyDown


    End Sub

    Private Sub ponerProducto2(codigo As String, total As Decimal, pos As Integer)
        Try
            grdetalle.DataChanged = True
            CType(grdetalle.DataSource, DataTable).AcceptChanges()
            Dim fila As DataRow() = Table_Producto.Select("yfcbarra='" + codigo.Trim + "'", "")
            If (fila.Count > 0) Then
                If pos = -1 Then
                    If (grdetalle.GetValue("tbty5prod") <= 0) Then
                        _prAddDetalleVenta()
                        grdetalle.Row = grdetalle.RowCount - 1
                    End If
                    _fnObtenerFilaDetalle(pos, grdetalle.GetValue("tbnumi"))
                End If
                Dim cantidad = Format(total / CDbl(fila(0).ItemArray(15)), "0.00")
                Dim precio = fila(0).ItemArray(15)
                total = cantidad * precio
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbty5prod") = fila(0).ItemArray(0)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("codigo") = fila(0).ItemArray(1)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcbarra") = fila(0).ItemArray(2)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = fila(0).ItemArray(3)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbumin") = fila(0).ItemArray(13)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("unidad") = fila(0).ItemArray(14)
                'tbcmin
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas") = precio
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = total
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = total
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = cantidad
                If (gb_FacturaIncluirICE) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = fila(0).ItemArray(17)
                Else
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = 0
                End If
                ''Modif
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = fila(0).ItemArray(16)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = fila(0).ItemArray(16) * cantidad
                '
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = fila(0).ItemArray(17) - cantidad
                'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tblote") = grProductos.GetValue("iclot")
                'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbfechaVenc") = grProductos.GetValue("icfven")
                'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = grProductos.GetValue("iccven")
                _prCalcularPrecioTotal()
            End If
        Catch ex As Exception
            Throw New Exception
        End Try

    End Sub
    Private Sub cargarProductos()
        Dim dt As DataTable
        If (G_Lote = True) Then
            dt = L_fnListarProductos(cbSucursal.Value, Str(_CodCliente))  ''1=Almacen
            Table_Producto = dt.Copy
        Else
            dt = L_fnListarProductosSinLote(cbSucursal.Value, Str(_CodCliente), CType(grdetalle.DataSource, DataTable).Clone)  ''1=Almacen
            Table_Producto = dt.Copy
        End If
    End Sub
    Private Function existeProducto(codigo As String) As Boolean
        Return (Table_Producto.Select("yfcbarra='" + codigo.Trim() + "'", "").Count > 0)
    End Function

    Private Function verificarExistenciaUnica(codigo As String) As Boolean
        Dim cont As Integer = 0
        For Each fila As GridEXRow In grdetalle.GetRows()
            If (fila.Cells("yfcbarra").Value.ToString.Trim = codigo.Trim) Then
                cont += 1
            End If
        Next
        Return (cont >= 1)
    End Function

    Private Sub ponerProducto(codigo As String, ByRef resultado As Boolean)
        Try
            grdetalle.DataChanged = True
            CType(grdetalle.DataSource, DataTable).AcceptChanges()
            Dim fila As DataRow() = Table_Producto.Select("yfcbarra='" + codigo.Trim + "'", "")
            If (fila.Count > 0) Then
                'Si tiene stock > 0
                If fila(0).ItemArray(20) > 0 Then
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, grdetalle.GetValue("tbnumi"))
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbty5prod") = fila(0).ItemArray(0)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("codigo") = fila(0).ItemArray(1)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcbarra") = fila(0).ItemArray(2)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = fila(0).ItemArray(3)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbfamilia") = fila(0).ItemArray(14)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbumin") = fila(0).ItemArray(16)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("unidad") = fila(0).ItemArray(17)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas") = fila(0).ItemArray(18)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = fila(0).ItemArray(18)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = fila(0).ItemArray(18)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = 1
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbFamilia") = fila(0).ItemArray(8)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbProveedorId") = fila(0).ItemArray(8)
                    If (gb_FacturaIncluirICE) Then
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = fila(0).ItemArray(19)
                    Else
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = 0
                    End If
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = fila(0).ItemArray(19)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = fila(0).ItemArray(19)
                    'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = fila(0).ItemArray(17)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = fila(0).ItemArray(20)
                    'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tblote") = grProductos.GetValue("iclot")
                    'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbfechaVenc") = grProductos.GetValue("icfven")
                    'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = grProductos.GetValue("iccven")

                    CalcularDescuentos(grdetalle.GetValue("tbty5prod"), 1, grdetalle.GetValue("tbpbas"), pos)
                    _prCalcularPrecioTotal()
                    resultado = True
                Else
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "El Producto: ".ToUpper + fila(0).ItemArray(3) + " NO CUENTA CON STOCK DISPONIBLE", img, 5000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    FilaSelectLote = Nothing
                    resultado = False
                End If
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub sumarCantidad(codigo As String)
        Try
            Dim fila As DataRow() = CType(grdetalle.DataSource, DataTable).Select(" estado=0 and yfcbarra='" + codigo.Trim + "'", "")
            If (fila.Count > 0) Then
                grdetalle.UpdateData()
                Dim pos1 As Integer = -1
                _fnObtenerFilaDetalle(pos1, fila(0).Item("tbnumi"))
                Dim cant As Integer = fila(0).Item("tbcmin") + 1
                Dim stock As Integer = fila(0).Item("stock")
                'If (cant > stock) Then
                Dim lin As Integer = grdetalle.GetRow(pos1).Cells("tbnumi").Value
                'Dim pos2 As Integer = -1
                '_fnObtenerFilaDetalle(pos2, lin)
                CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("tbcmin") = cant
                CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("tbptot") = CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("tbpbas") * cant
                CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("tbptot2") = grdetalle.GetRow(pos1).Cells("tbpcos").Value * cant
                CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos1).Item("tbpbas") * cant
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                'ToastNotification.Show(Me, "La cantidad de la venta no debe ser mayor al del stock" & vbCrLf &
                '        "Stock=" + Str(stock).ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                grdetalle.SetValue("yfcbarra", "")
                grdetalle.SetValue("tbcmin", 0)
                grdetalle.SetValue("tbptot", 0)
                grdetalle.SetValue("tbptot2", 0)
                grdetalle.DataChanged = True
                'grdetalle.Refetch()
                grdetalle.Refresh()
                '_prCalcularPrecioTotal()
                'Else
                '    If (cant = stock) Then
                '        'grdetalle.SelectedFormatStyle.ForeColor = Color.Blue
                '        'grdetalle.CurrentRow.Cells.Item(e.Column).FormatStyle = New GridEXFormatStyle
                '        'grdetalle.CurrentRow.Cells(e.Column).FormatStyle.BackColor = Color.Pink
                '        'grdetalle.CurrentRow.Cells.Item(e.Column).FormatStyle.BackColor = Color.DodgerBlue
                '        'grdetalle.CurrentRow.Cells.Item(e.Column).FormatStyle.ForeColor = Color.White
                '        'grdetalle.CurrentRow.Cells.Item(e.Column).FormatStyle.FontBold = TriState.True
                '    End If
                'End If

                _prCalcularPrecioTotal()
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub _buscarRegistro(cbarra As String)
        Dim _t As DataTable
        _t = L_fnListarProductosC(cbarra)
        If _t.Rows.Count > 0 Then
            CType(grdetalle.DataSource, DataTable).Rows(0).Item("producto") = _t.Rows(0).Item("yfcdprod1")
            CType(grdetalle.DataSource, DataTable).Rows(0).Item("tbcmin") = 1
            CType(grdetalle.DataSource, DataTable).Rows(0).Item("unidad") = _t.Rows(0).Item("uni")

        Else
            MsgBox("Codigo de Producto No Exite")
        End If
    End Sub
    Private Sub grProductos_KeyDown(sender As Object, e As KeyEventArgs)


    End Sub



    Public Sub CalcularDescuentos(ProductoId As Integer, Cantidad As Integer, Precio As Integer, Posicion As Integer)
        If ConfiguracionDescuentoEsXCantidad = False Then
            Return
        End If

        Dim fila As DataRow() = dtDescuentos.Select("ProductoId=" + Str(ProductoId).ToString.Trim + "", "")

        For Each dr As DataRow In fila

            Dim CantidadInicial As Integer = dr.Item("CantidadInicial")
            Dim CantidadFinal As Integer = dr.Item("CantidadFinal")
            Dim PrecioDescuento As Double = dr.Item("Precio")

            If (Cantidad >= CantidadInicial And Cantidad <= CantidadFinal) Then

                Dim SubTotalDescuento As Double = Cantidad * PrecioDescuento
                Dim Descuento As Double = (Cantidad * Precio) - SubTotalDescuento
                CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("tbdesc") = Descuento
                grdetalle.SetValue("tbdesc", Descuento)
                CType(grdetalle.DataSource, DataTable).Rows(Posicion).Item("tbtotdesc") = (grdetalle.GetValue("tbpbas") * grdetalle.GetValue("tbcmin")) - Descuento

                grdetalle.SetValue("tbtotdesc", ((grdetalle.GetValue("tbpbas") * grdetalle.GetValue("tbcmin")) - Descuento))
                Return


            End If

        Next




    End Sub
    Private Sub grdetalle_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles grdetalle.CellValueChanged

        Try
            If (e.Column.Index = grdetalle.RootTable.Columns("pagar").Index) Then
                If (Not IsNumeric(grdetalle.GetValue("pagar")) Or grdetalle.GetValue("pagar").ToString = String.Empty) Then
                    Dim lin As Integer = grdetalle.GetValue("tanumi")
                    Dim pos As Integer = -1
                    Dim rowIndex As Integer = grdetalle.Row
                    _fnObtenerFilaDetalle(pos, lin)

                    grdetalle.SetValue("pagar", 0)
                Else
                    Dim total As Double = grdetalle.GetValue("total").ToString
                    Dim pagar As Double = grdetalle.GetValue("pagar").ToString

                    If (pagar > total) Then

                        MessageBox.Show("El Valor A Pagar Es Mayor Al Total")
                        grdetalle.SetValue("pagar", 0.00)


                    End If
                End If
            End If

        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub grdetalle_MouseClick(sender As Object, e As MouseEventArgs) Handles grdetalle.MouseClick

    End Sub
    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click


        _prGuardar()


    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Try

            Dim dt As DataTable
            'dt = L_fnListarClientes()
            dt = L_fnListarClientesVentas(_CodCliente)

            _CodEmpleado = dt.Rows(1).Item(8)
            tipoDocumento = dt.Rows(0)("ydtipdocelec") 'dt.Rows(1).Item(8) ' dt.Row.Cells("ydtipdocelec").Value

            'swTipoVenta.Value = grVentas.GetValue("tatven")

            If (grVentas.RowCount > 0) Then
                If (gb_FacturaEmite) Then
                    If (P_fnValidarFacturaVigente()) Then
                        Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                        ToastNotification.Show(Me, "No se puede modificar la venta con codigo ".ToUpper + tbCodigo.Text + ", su factura esta validada por impuesto.".ToUpper,
                                                  img, 2000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter)
                        Exit Sub
                    End If
                End If

                _prhabilitar()

                btnNuevo.Enabled = False
                btnModificar.Enabled = False
                btnEliminar.Enabled = False
                btnGrabar.Enabled = True

                PanelNavegacion.Enabled = False
                _prCargarIconELiminar()
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Try

            If (gb_FacturaEmite) Then
                If (P_fnValidarFacturaVigente()) Then
                    Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                    ToastNotification.Show(Me, "No se puede anular la venta con código ".ToUpper + tbCodigo.Text + ", su factura esta vigente, por favor primero anule la factura".ToUpper,
                                                  img, 3000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter)
                    Exit Sub
                End If
            End If



            Dim res2 As Boolean = L_fnVerificarCierreCaja(tbCodigo.Text, "V")
            If res2 Then
                Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                ToastNotification.Show(Me, "No se puede anular la venta con código ".ToUpper + tbCodigo.Text + ", ya se hizo cierre de caja, por favor primero elimine cierre de caja".ToUpper,
                                                  img, 5000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter)
                Exit Sub
            End If


            Dim result As Boolean = L_fnVerificarSiSeContabilizoVenta(tbCodigo.Text)
            If result Then
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, "La Venta no puede ser anulada porque ya fue contabilizada".ToUpper, img, 4500, eToastGlowColor.Red, eToastPosition.TopCenter)
                Exit Sub
            End If
            Dim ef = New Efecto
            ef.tipo = 2
            ef.Context = "¿esta seguro de eliminar el registro?".ToUpper
            ef.Header = "mensaje principal".ToUpper
            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            If (bandera = True) Then
                Dim mensajeError As String = ""
                Dim res As Boolean = L_fnEliminarVenta(tbCodigo.Text, mensajeError, Programa)
                If res Then
                    Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                    ToastNotification.Show(Me, "Código de Venta ".ToUpper + tbCodigo.Text + " eliminado con Exito.".ToUpper,
                                              img, 2000,
                                              eToastGlowColor.Green,
                                              eToastPosition.TopCenter)
                    _prFiltrar()
                Else
                    Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                    ToastNotification.Show(Me, mensajeError, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                End If
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub grVentas_SelectionChanged(sender As Object, e As EventArgs) Handles grVentas.SelectionChanged
        If (grVentas.RowCount >= 0 And grVentas.Row >= 0) Then
            _prMostrarRegistro(grVentas.Row)
        End If
    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        Dim _pos As Integer = grVentas.Row
        If _pos < grVentas.RowCount - 1 And _pos >= 0 Then
            _pos = grVentas.Row + 1

            grVentas.Row = _pos
            '_prMostrarRegistro(_pos)
        End If
    End Sub

    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        Dim _pos As Integer = grVentas.Row
        If grVentas.RowCount > 0 Then
            _pos = grVentas.RowCount - 1
            ''  _prMostrarRegistro(_pos)
            grVentas.Row = _pos
        End If
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        Dim _MPos As Integer = grVentas.Row
        If _MPos > 0 And grVentas.RowCount > 0 Then
            _MPos = _MPos - 1
            ''  _prMostrarRegistro(_MPos)
            grVentas.Row = _MPos
        End If
    End Sub

    Private Sub btnPrimero_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click
        _PrimerRegistro()
    End Sub
    Private Sub grVentas_KeyDown(sender As Object, e As KeyEventArgs) Handles grVentas.KeyDown
        If e.KeyData = Keys.Enter Then
            MSuperTabControl.SelectedTabIndex = 0
            grdetalle.Focus()

        End If
    End Sub



    Private Sub TbNit_KeyPress(sender As Object, e As KeyPressEventArgs)
        g_prValidarTextBox(1, e)
    End Sub

    Private Sub swTipoVenta_Leave(sender As Object, e As EventArgs)
        grdetalle.Select()
    End Sub

    Private Sub cbSucursal_ValueChanged(sender As Object, e As EventArgs) Handles cbSucursal.ValueChanged
        Table_Producto = Nothing
    End Sub

    Private Sub tbNit_Leave(sender As Object, e As EventArgs)
        grdetalle.Select()
    End Sub







    Private Sub cbCambioDolar_ValueChanged_1(sender As Object, e As EventArgs)
        If cbCambioDolar.SelectedIndex < 0 And cbCambioDolar.Text <> String.Empty Then
            btgrupo1.Visible = True
        Else
            btgrupo1.Visible = False
        End If
    End Sub
    Private Sub btgrupo1_Click(sender As Object, e As EventArgs)
        Dim numi As String = ""

        If L_prLibreriaGrabar(numi, "7", "1", cbCambioDolar.Text, "") Then
            _prCargarComboLibreria(cbCambioDolar, "7", "1")
            cbCambioDolar.SelectedIndex = CType(cbCambioDolar.DataSource, DataTable).Rows.Count - 1
        End If
    End Sub
    Private Sub _prCargarComboLibreria(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo, cod1 As String, cod2 As String)
        Dim dt As New DataTable
        dt = L_prLibreriaClienteLGeneral(cod1, cod2)
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("yccod3").Width = 70
            .DropDownList.Columns("yccod3").Caption = "COD"
            .DropDownList.Columns.Add("ycdes3").Width = 200
            .DropDownList.Columns("ycdes3").Caption = "DESCRIPCION"
            .ValueMember = "yccod3"
            .DisplayMember = "ycdes3"
            .DataSource = dt
            .Refresh()
        End With
    End Sub

    Private Sub tbMontoBs_KeyDown(sender As Object, e As KeyEventArgs)
        If (e.KeyData = Keys.Control + Keys.A) Then
            _prGuardar()
        End If
    End Sub

    Private Sub tbMontoDolar_KeyDown(sender As Object, e As KeyEventArgs)
        If (e.KeyData = Keys.Control + Keys.A) Then
            _prGuardar()
        End If
    End Sub
    Private Sub tbMontoTarej_KeyDown(sender As Object, e As KeyEventArgs)
        If (e.KeyData = Keys.Control + Keys.A) Then
            _prGuardar()
        End If
    End Sub

    Private Sub txtMontoPagado1_KeyDown(sender As Object, e As KeyEventArgs)
        If (e.KeyData = Keys.Control + Keys.A) Then
            _prGuardar()
        End If
    End Sub

    Private Sub txtCambio1_KeyDown(sender As Object, e As KeyEventArgs)
        If (e.KeyData = Keys.Control + Keys.A) Then
            _prGuardar()
        End If
    End Sub

    Private Sub GroupPanel1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub grdetalle_Enter(sender As Object, e As EventArgs) Handles grdetalle.Enter

    End Sub



    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        _Inter = _Inter + 1
        If _Inter = 1 Then
            Me.WindowState = FormWindowState.Normal
        Else
            Me.Opacity = 100
            Timer1.Enabled = False
        End If
    End Sub

    Private Sub _prCargarVenta1()
        Dim dt As New DataTable
        dt = L_fnGeneralVenta(gi_userSuc)
        grVentas.DataSource = dt
        grVentas.RetrieveStructure()
        grVentas.AlternatingColors = True
        '   a.tamon ,IIF(tamon=1,'Boliviano','Dolar') as moneda,a.taest ,a.taobs ,
        'a.tadesc ,a.tafact ,a.tahact ,a.tauact,(Sum(b.tbptot)-a.tadesc ) as total

        With grVentas.RootTable.Columns("tanumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = True

        End With

        With grVentas.RootTable.Columns("taalm")
            .Width = 90
            .Visible = False
        End With

        With grVentas.RootTable.Columns("taproforma")
            .Width = 90
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tafdoc")
            .Width = 90
            .Visible = True
            .Caption = "FECHA"
        End With

        With grVentas.RootTable.Columns("aabdes")
            .Width = 90
            .Visible = False
            .Caption = "FECHA"
        End With

        With grVentas.RootTable.Columns("taven")
            .Width = 160
            .Visible = False
        End With
        With grVentas.RootTable.Columns("vendedor")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "VENDEDOR"
        End With
        With grVentas.RootTable.Columns("cliente")
            .Width = 250
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "CLIENTE"
        End With
        With grVentas.RootTable.Columns("institucion")
            .Width = 250
            .Visible = True
            .Caption = "INSTITUCION".ToUpper
        End With


        With grVentas.RootTable.Columns("tatven")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        With grVentas.RootTable.Columns("tafvcr")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("taclpr")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With


        With grVentas.RootTable.Columns("tamon")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("moneda")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
            .Caption = "MONEDA"
        End With
        With grVentas.RootTable.Columns("taobs")
            .Width = 200
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "OBSERVACION"
        End With
        With grVentas.RootTable.Columns("tadesc")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("taest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("taice")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tafact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tahact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tauact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("NroCaja")
            .Width = 100
            .Caption = "COD. Institucion"
            .Visible = False
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With
        With grVentas.RootTable.Columns("total")
            .Width = 150
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "TOTAL"
            .FormatString = "0.00"
        End With
        With grVentas
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla

        End With

        If (dt.Rows.Count <= 0) Then
            _prCargarDetalleVenta(-1)
        End If
    End Sub
    'Private Sub TbNombre1_KeyDown(sender As Object, e As KeyEventArgs) Handles TbNombre1.KeyDown
    '    If (e.KeyData = Keys.Enter) Then
    '        grdetalle.Col = 7
    '        grdetalle.Row = 0
    '        grdetalle.Select()

    '    End If
    'End Sub

    Private Sub CalculoDescuentoXProveedor()
        If ConfiguracionDescuentoEsXCantidad = True Then
            Return
        End If

        If grdetalle.RowCount < 0 Or DescuentoXProveedorList.Rows.Count < 0 Or DescuentoXProveedorList.Rows.Count = 0 Or SwDescuentoProveedor.Value = False Then
            Return
        End If
        Dim productoId As Integer = 0
        Dim totalDescontadoXAgrupacionProveedor As Decimal = 0
        Dim montoDescuento As Decimal = 0, subTotalDescuento As Decimal = 0
        Dim totalXProveedor As Decimal = 0, subTotalVenta As Decimal = 0, totalAcumuladoDescEspecial As Decimal = 0

        Dim detalle = CType(grdetalle.DataSource, DataTable)
        Dim detalleLista As List(Of DataRow) = detalle.AsEnumerable().ToList()
        Dim DescuentoXProveedorLista As List(Of DataRow) = DescuentoXProveedorList.AsEnumerable().ToList()

        Dim proveedorIDArray = (From proc In detalleLista
                                Where proc.ItemArray(ENDetalleVenta.estadoDetalle) <> -1
                                Select proc.ItemArray(ENDetalleVenta.proveedorId)).Distinct().ToArray()
        Dim existeProveedorEnTablaDescuento As Boolean = False

        For Each proveedorID As Integer In proveedorIDArray

            existeProveedorEnTablaDescuento = (From desc In DescuentoXProveedorLista
                                               Where desc.ItemArray(ENDescuentoXProveedor.proveedorId) = proveedorID).Count() > 0
            totalDescontadoXAgrupacionProveedor = 0

            totalXProveedor = ObtenerSumaTotalXProveedor(detalleLista, proveedorID)
            If existeProveedorEnTablaDescuento Then

                Dim porcentajeDescuento = ObtenerPorcentajeDescuento(DescuentoXProveedorLista, totalXProveedor, False, proveedorID)

                If porcentajeDescuento.Length <> 0 Then
                    montoDescuento = (totalXProveedor * porcentajeDescuento(0)) / 100
                    subTotalDescuento += montoDescuento
                Else
                    totalAcumuladoDescEspecial += totalXProveedor
                End If
            End If

            'Obtiene la suma total por proveedor
            subTotalVenta += totalXProveedor
        Next
        'Calculo descuento especial
        If totalAcumuladoDescEspecial <> 0 Then
            Dim porcentajeDescuento = ObtenerPorcentajeDescuento(DescuentoXProveedorLista, totalAcumuladoDescEspecial, True, 0)
            If porcentajeDescuento.Length <> 0 Then
                montoDescuento = (totalAcumuladoDescEspecial * porcentajeDescuento(0)) / 100
                subTotalDescuento += montoDescuento
            End If
        End If

        If subTotalVenta = 0 Then
            Return
        End If

        Dim montoDo As Decimal

    End Sub

    Private Function ObtenerPorcentajeDescuento(listaDescuento As List(Of DataRow), total As Decimal, esDescuentoEspecial As Boolean, proveedorID As Integer) As Object

        If esDescuentoEspecial Then
            'Obtiene el porcentaje de descuento especial
            Return (From desc In listaDescuento
                    Where desc.ItemArray(ENDescuentoXProveedor.Estado) = 2 _
                     And desc.ItemArray(ENDescuentoXProveedor.MontoInicial) <= total _
                     And desc.ItemArray(ENDescuentoXProveedor.MontoFinal) >= total
                    Select desc.ItemArray(ENDescuentoXProveedor.DescuentoPorcentaje)).ToArray()
        Else
            'Obtiene el porcentaje de descuento por proveedor


            Return (From desc In listaDescuento
                    Where desc.ItemArray(ENDescuentoXProveedor.proveedorId) = proveedorID _
                     And desc.ItemArray(ENDescuentoXProveedor.MontoInicial) <= total _
                     And desc.ItemArray(ENDescuentoXProveedor.MontoFinal) >= total
                    Select desc.ItemArray(ENDescuentoXProveedor.DescuentoPorcentaje)).ToArray()
        End If
    End Function
    Private Function ObtenerSumaTotalXProveedor(detalleLista As List(Of DataRow), proveedorID As Integer) As Decimal
        Return (From proc In detalleLista
                Where proc.ItemArray(ENDetalleVenta.estado) >= 0 _
                    And proc.ItemArray(ENDetalleVenta.proveedorId) = proveedorID
                Select Convert.ToDecimal(proc.ItemArray(ENDetalleVenta.totalDescuento))).Sum()
    End Function

    Private Sub tbCliente_TextChanged(sender As Object, e As EventArgs) Handles tbCliente.TextChanged
        If btnNuevo.Enabled = False Then
            Dim dt As DataTable
            dt = L_fnListarCaneroInstitucion(_CodCliente)
            Dim row As DataRow = dt.Rows(dt.Rows.Count - 1)
            tbVendedor.Text = row("institucion")
            _CodInstitucion = row("id")
        End If



    End Sub

    Private Sub btnCont_Click(sender As Object, e As EventArgs)

    End Sub


#End Region


    'funciones de conexion con sifac para facturacion

    Public Function ObtToken(idserviciosEdoc As Integer)
        Dim api = New DBApi()

        '' httpClient.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Bearer", "Your Key")
        Dim url = "https://labbo-emp-auth-v2-1.guru-soft.com/ServicioEDOC?Id=" + idserviciosEdoc.ToString

        Dim headers = New List(Of Parametro) From {
            New Parametro("Authorization", "Basic VXNlclVuQ2FHdWFRQTo4cXRsTldKNFQyajdUaDk4"),
            New Parametro("Cookie", "ARRAffinity=20358cd7aa5d6b0695f01ef171fc9a95880154357830e1c6bb513b73834a2e5f; ARRAffinitySameSite=20358cd7aa5d6b0695f01ef171fc9a95880154357830e1c6bb513b73834a2e5f")
        }

        Dim parametros = New List(Of Parametro)

        Dim response = api.MGet(url, headers, parametros) ', Lenvio)
        'Dim json = JsonConvert.SerializeObject(Lenvio)
        ''MsgBox(json)

        Dim result = JsonConvert.DeserializeObject(Of Respuest)(response)
        Dim resultError = JsonConvert.DeserializeObject(Of Resp400)(response)
        Dim Token As String
        Dim json1 = JsonConvert.SerializeObject(response)
        '' MsgBox(json1)
        If result Is Nothing Then
            'MessageBox.Show("intente de nuevo")
            Token = "400"
        Else
            If (result.codigoError Is Nothing) Then
                Token = result.token.ToString
                fechaEmision = result.expedido.ToString
            Else
                Token = "400"
            End If
        End If
        Return Token
    End Function

    Private Sub btbBuscar_Click(sender As Object, e As EventArgs) Handles btbBuscar.Click
        _prCargarTxCobrar()
    End Sub
    Private Sub _prCargarTxCobrar()
        Dim dt As New DataTable
        dt = L_fnCuentasxCobrar(_CodCliente, tbinteres.Text, tbFechaPago.Text)
        grdetalle.DataSource = dt
        grdetalle.RetrieveStructure()
        grdetalle.AlternatingColors = True
        '   a.tamon ,IIF(tamon=1,'Boliviano','Dolar') as moneda,a.taest ,a.taobs ,
        'a.tadesc ,a.tafact ,a.tahact ,a.tauact,(Sum(b.tbptot)-a.tadesc ) as total

        With grdetalle.RootTable.Columns("numi")
            .Width = 90
            .TextAlignment = 3
            .Caption = "NUMERO"
            .Visible = False

        End With
        With grdetalle.RootTable.Columns("tanumi")
            .Width = 90

            .Caption = "NUMERO"
            .Visible = True

        End With

        With grdetalle.RootTable.Columns("taalm")
            .Width = 50
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("tafdoc")
            .Width = 110
            .Visible = True
            .Caption = "FECHA"
        End With

        With grdetalle.RootTable.Columns("idInst")
            .Width = 90
            .Visible = False
            .Caption = "FECHA"
        End With

        With grdetalle.RootTable.Columns("ydnumi")
            .Width = 160
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tbty5prod")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "CÓDIGO"
        End With
        With grdetalle.RootTable.Columns("tbcmin")
            .Width = 250
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
            .Caption = ""
        End With
        With grdetalle.RootTable.Columns("tbpbas")
            .Width = 250
            .Visible = False
            .Caption = "INSTITUCION".ToUpper
        End With


        With grdetalle.RootTable.Columns("tbdebe")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .TextAlignment = 3
            .Visible = True
            .Caption = "DEBE".ToUpper
            .FormatString = "0.00"
        End With
        With grdetalle.RootTable.Columns("pago")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .TextAlignment = 3
            .Visible = True
            .Caption = "pago".ToUpper
            .FormatString = "0.00"
        End With

        With grdetalle.RootTable.Columns("aporte")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .TextAlignment = 3
            .Visible = True
            .Caption = "aporte".ToUpper
            .FormatString = "0.00"
        End With
        With grdetalle.RootTable.Columns("total")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .TextAlignment = 3
            .Visible = True
            .Caption = "Total".ToUpper
            .FormatString = "0.00"
        End With
        With grdetalle.RootTable.Columns("tbdetalle")
            .Width = 500
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "descripción".ToUpper
        End With
        With grdetalle.RootTable.Columns("est")
            .Width = 250
            .Visible = False
            .Caption = "INSTITUCION".ToUpper
        End With
        With grdetalle.RootTable.Columns("pagar")
            .Width = 80
            .Caption = "A pagar".ToUpper
            '.CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .TextAlignment = 3
            .Visible = True
            .FormatString = "0.00"
        End With

    End Sub


End Class