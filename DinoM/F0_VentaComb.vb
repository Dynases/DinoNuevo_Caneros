Imports System.Drawing.Printing
Imports System.IO
Imports CrystalDecisions.Shared
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports Facturacion
Imports Janus.Windows.GridEX
Imports Logica.AccesoLogica
Imports UTILITIES
Public Class F0_VentaComb
    Dim _Inter As Integer = 0
#Region "Variables Globales"
    Dim _CodCliente As Integer = 0
    Dim _CodCaneroUcg As Integer = 0
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
    Dim _Nuevo As Boolean = False
    Dim _CantidadComprado As Integer = 0
    Dim dtDescuentos As DataTable = Nothing
    Dim ConfiguracionDescuentoEsXCantidad As Boolean = True
    Public Programa As String
    Dim DescuentoXProveedorList As DataTable = New DataTable
    Dim codCont As String = "" 'CODIGO DE ASIENTO CONTABLE

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        _Inter = _Inter + 1
        If _Inter = 1 Then
            Me.WindowState = FormWindowState.Normal
        Else
            Me.Opacity = 100
            Timer1.Enabled = False
        End If
    End Sub

#End Region
#Region "Metodos Privados"
    Private Sub _IniciarTodo()
        L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        MSuperTabControl.SelectedTabIndex = 0
        'Me.WindowState = FormWindowState.Maximized
        cbSucursal.Value = 3
        _prValidarLote()
        _prCargarComboLibreriaSucursal(cbSucursal)
        _prCargarComboLibreria(cbCambioDolar, 7, 1)
        _prCargarComboLibreria(cbTipoSolicitud, 10, 1)
        _prCargarComboLibreria(cbSurtidor, 1, 10)
        _prCargarComboLibreria(cbDespachador, 1, 9)
        cbCambioDolar.Value = 1

        'lbTipoMoneda.Visible = False
        swMoneda.Visible = False
        P_prCargarVariablesIndispensables()
        _prCargarVenta()
        _prInhabiliitar()
        grVentas.Focus()
        Me.Text = "DISTRIBUCION COMBUSTIBLE PROPIO"
        Dim blah As New Bitmap(New Bitmap(My.Resources.compra), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        _prAsignarPermisos()
        P_prCargarParametro()
        _prValidadFactura()
        _prCargarNameLabel()
        'COnfiguracion previa para Pantalla de facturacion o Nota de venta
        If gb_FacturaEmite Then
            btnModificar.Visible = True
        Else
            tbObservacion.Visible = True
            lblObservacion.Visible = True
        End If
        DescuentoXProveedorList = ObtenerDescuentoPorProveedor()
        ConfiguracionDescuentoEsXCantidad = TipoDescuentoEsXCantidad()
        SwDescuentoProveedor.Visible = IIf(ConfiguracionDescuentoEsXCantidad, False, True)
        SwDescuentoProveedor.Visible = False
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
            btnModificar.Visible = False
        End If
        If del = False Then
            btnEliminar.Visible = False
        End If
    End Sub
    Private Sub _prInhabiliitar()

        tbCodigo.ReadOnly = True
        tbCliente.ReadOnly = True
        tbVendedor.ReadOnly = True
        tbFechaVenta.IsInputReadOnly = True
        tbFechaVenta.Enabled = False
        swMoneda.IsReadOnly = True
        tbFechaVenc.IsInputReadOnly = True
        swTipoVenta.IsReadOnly = True
        SwSurtidor.IsReadOnly = True
        txtEstado.ReadOnly = True

        tbTramOrden.ReadOnly = True
        tbNitTraOrden.ReadOnly = True
        tbRetSurtidor.ReadOnly = True
        tbNitRetSurtidor.ReadOnly = True

        tbPlaca.ReadOnly = True
        tbObservacion.ReadOnly = True

        cbDespachador.ReadOnly = True
        cbTipoSolicitud.ReadOnly = True
        cbSurtidor.ReadOnly = True

        tbAutoriza.ReadOnly = True


        'Datos facturacion
        tbNroAutoriz.ReadOnly = True
        tbNroFactura.ReadOnly = True
        tbCodigoControl.ReadOnly = True
        dtiFechaFactura.IsInputReadOnly = True
        dtiFechaFactura.ButtonDropDown.Enabled = False

        btnModificar.Enabled = True
        btnGrabar.Enabled = False
        btnNuevo.Enabled = True
        'btnEliminar.Enabled = True

        If grVentas.GetValue("taest") = 1 Then
            btnEliminar.Enabled = True
        Else
            btnEliminar.Enabled = False
        End If

        tbSubTotal.IsInputReadOnly = True
        tbMdesc.IsInputReadOnly = True
        tbIce.IsInputReadOnly = True
        tbPrueba.IsInputReadOnly = True
        tbMontoBs.IsInputReadOnly = True
        tbMontoDolar.IsInputReadOnly = True
        tbMontoTarej.IsInputReadOnly = True
        'txtCambio1.IsInputReadOnly = True

        'txtMontoPagado1.IsInputReadOnly = True

        grVentas.Enabled = True
        PanelNavegacion.Enabled = True
        'grdetalle.RootTable.Columns("img").Visible = False
        'If (GPanelProductos.Visible = True) Then 'REVISAR DESPUES IMPORTANTE
        '    _DesHabilitarProductos()
        'End If

        tbNit.ReadOnly = True
        TbNombre1.ReadOnly = True
        TbNombre2.ReadOnly = True
        cbSucursal.ReadOnly = True
        FilaSelectLote = Nothing

        _Nuevo = False
    End Sub
    Private Sub _prhabilitar()

        grVentas.Enabled = False
        tbCodigo.ReadOnly = False
        ''  tbCliente.ReadOnly = False  por que solo podra seleccionar Cliente
        ''  tbVendedor.ReadOnly = False
        tbFechaVenc.IsInputReadOnly = False
        SwSurtidor.Value = True
        swTipoVenta.IsReadOnly = False
        swTipoVenta.Value = 0
        tbFechaVenta.IsInputReadOnly = False
        tbFechaVenta.Enabled = True

        swMoneda.IsReadOnly = False
        SwSurtidor.IsReadOnly = True
        btnGrabar.Enabled = True

        tbNit.ReadOnly = False
        TbNombre1.ReadOnly = False
        TbNombre2.ReadOnly = False

        'Datos facturacion
        tbNroAutoriz.ReadOnly = False
        tbNroFactura.ReadOnly = False
        tbCodigoControl.ReadOnly = False
        dtiFechaFactura.IsInputReadOnly = False

        tbMontoBs.IsInputReadOnly = False
        tbMontoDolar.IsInputReadOnly = False
        tbMontoTarej.IsInputReadOnly = False
        'tbMdesc.IsInputReadOnly = False

        tbTramOrden.ReadOnly = False
        tbNitTraOrden.ReadOnly = False
        tbRetSurtidor.ReadOnly = False
        tbNitRetSurtidor.ReadOnly = False


        tbPlaca.ReadOnly = False
        tbObservacion.ReadOnly = False

        cbDespachador.ReadOnly = False
        cbTipoSolicitud.ReadOnly = False
        cbSurtidor.ReadOnly = False
        tbAutoriza.ReadOnly = False

        'txtCambio1.IsInputReadOnly = False
        'txtMontoPagado1.IsInputReadOnly = False

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
        _prCargarVenta()
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

        swMoneda.Value = True
        lbNroCaja.Text = ""
        tbObservacion.Text = ""
        _CodCliente = 0
        _CodEmpleado = 0
        tbFechaVenta.Value = Now.Date
        swTipoVenta.Value = True
        tbFechaVenc.Visible = False
        lbCredito.Visible = False
        _prCargarDetalleVenta(-1)
        MSuperTabControl.SelectedTabIndex = 0
        tbSubTotal.Value = 0
        tbPdesc.Value = 0
        tbMdesc.Value = 0
        tbIce.Value = 0
        tbPrueba.Value = 0
        tbMontoBs.Value = 0
        tbMontoDolar.Value = 0
        tbMontoTarej.Value = 0
        'txtCambio1.Value = 0
        'txtMontoPagado1.Value = 0
        txtCambio1.Text = "0.00"
        txtMontoPagado1.Text = "0.00"
        tbTotalBs.Text = "0.00"
        tbTotalDo.Text = "0.00"

        txtEstado.BackColor = Color.White
        txtEstado.Clear()


        tbTramOrden.Text = ""
        tbNitTraOrden.Text = ""
        tbPlaca.Text = ""
        tbRetSurtidor.Text = ""
        tbNitRetSurtidor.Text = ""

        tbAutoriza.Text = 0

        With grdetalle.RootTable.Columns("img")
            .Width = 80
            .Caption = "Eliminar"
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = False
        End With
        _prAddDetalleVenta()
        If (GPanelProductos.Visible = True) Then ''REVISAR DESPUES IMPORTANTE
            GPanelProductos.Visible = False

            PanelInferior.Visible = True
        End If
        tbCliente.Focus()
        _prCargarProductos(Str(_CodCliente))

        InsertarProductosSinLote()
        tbNit.Clear()
        TbNombre1.Clear()
        TbNombre2.Clear()
        tbNit.Select()
        tbNroAutoriz.Clear()
        tbNroFactura.Clear()
        tbCodigoControl.Clear()
        dtiFechaFactura.Value = Now.Date
        If (CType(cbSucursal.DataSource, DataTable).Rows.Count > 0) Then
            cbSucursal.SelectedIndex = 0
        End If
        FilaSelectLote = Nothing

        ' tbCliente.Focus()
        Table_Producto = Nothing
    End Sub
    Public Sub _prMostrarRegistro(_N As Integer)
        '' grVentas.Row = _N
        '     a.tanumi ,a.taalm ,a.tafdoc ,a.taven ,vendedor .yddesc as vendedor ,a.tatven ,a.tafvcr ,a.taclpr,
        'cliente.yddesc as cliente ,a.tamon ,IIF(tamon=1,'Boliviano','Dolar') as moneda,a.taest ,a.taobs ,
        'a.tadesc ,a.tafact ,a.tahact ,a.tauact,(Sum(b.tbptot)-a.tadesc ) as total,taproforma

        With grVentas
            LabelAlmacen.Text = .GetValue("aabdes")
            cbSucursal.Value = .GetValue("taalm")
            tbCodigo.Text = .GetValue("tanumi")
            tbFechaVenta.Value = .GetValue("tafdoc")
            _CodEmpleado = .GetValue("taven")
            _CodInstitucion = .GetValue("NroCaja")
            tbVendedor.Text = .GetValue("institucion")
            _CodCliente = .GetValue("taclpr")
            tbCliente.Text = .GetValue("cliente")
            swMoneda.Value = .GetValue("tamon")
            _CodCaneroUcg = .GetValue("ydcod")
            tbFechaVenc.Value = .GetValue("tafvcr")
            swTipoVenta.Value = .GetValue("tatven")
            'SwConta.Value = IIf(.GetValue("taproforma") = 0, 1, 0)
            If .GetValue("tctiposurtidor") = True Then
                _prCargarComboLibreria(cbSurtidor, 1, 10)
            Else
                _prCargarComboLibreria(cbSurtidor, 1, 8)
            End If
            SwSurtidor.Value = IIf(.GetValue("tctiposurtidor") = True, 1, 0)

            tbObservacion.Text = .GetValue("taobs")
            lbNroCaja.Text = .GetValue("vendedor")

            tbTramOrden.Text = .GetValue("tcentregado")
            tbNitTraOrden.Text = .GetValue("tcentregadoci")
            cbDespachador.Value = .GetValue("tcdespachador")
            tbPlaca.Text = .GetValue("tcplaca")
            tbRetSurtidor.Text = .GetValue("tcretiro")
            tbNitRetSurtidor.Text = .GetValue("tcnitretiro")
            TbNombre1.Text = .GetValue("tcfacnombre")
            tbNit.Text = .GetValue("tcfacnit")
            cbTipoSolicitud.Value = .GetValue("tcsolicitud")
            cbSurtidor.Value = .GetValue("tcsurtidor")
            tbAutoriza.Text = .GetValue("tcnroautorizacion")
            If grVentas.GetValue("taest") = 1 Then
                txtEstado.Text = "VIGENTE"
                txtEstado.BackColor = Color.Green
                btnEliminar.Enabled = True
            Else
                txtEstado.Text = "ANULADO"
                txtEstado.BackColor = Color.Red
                btnEliminar.Enabled = False
            End If
            'If (gb_FacturaEmite) Then
            Dim dt As DataTable = L_fnObtenerTabla("TFV001", "fvanitcli, fvadescli1, fvadescli2, fvaautoriz, fvanfac, fvaccont, fvafec", "fvanumi=" + tbCodigo.Text.Trim)
            If (dt.Rows.Count = 1) Then
                tbNit.Text = dt.Rows(0).Item("fvanitcli").ToString
                TbNombre1.Text = dt.Rows(0).Item("fvadescli1").ToString
                TbNombre2.Text = dt.Rows(0).Item("fvadescli2").ToString

                tbNroAutoriz.Text = dt.Rows(0).Item("fvaautoriz").ToString
                tbNroFactura.Text = dt.Rows(0).Item("fvanfac").ToString
                tbCodigoControl.Text = dt.Rows(0).Item("fvaccont").ToString
                dtiFechaFactura.Value = dt.Rows(0).Item("fvafec")
            Else
                tbNit.Clear()
                TbNombre1.Clear()
                TbNombre2.Clear()

                tbNroAutoriz.Clear()
                tbNroFactura.Clear()
                tbCodigoControl.Clear()
                dtiFechaFactura.Value = "2000/01/01"
            End If
            'End If

            lbFecha.Text = CType(.GetValue("tafact"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("tahact").ToString
            lbUsuario.Text = .GetValue("tauact").ToString
            codCont = .GetValue("taproforma").ToString
        End With

        _prCargarDetalleVenta(tbCodigo.Text)
        tbMdesc.Value = grVentas.GetValue("tadesc")
        tbIce.Value = grVentas.GetValue("taice")
        _prCalcularPrecioTotal()
        'Calcular montos
        Dim tMonto As DataTable = L_fnMostrarMontos(tbCodigo.Text)
        If tMonto.Rows.Count > 0 Then

            tbMontoTarej.Value = tMonto.Rows(0).Item("tgMontTare").ToString
            cbCambioDolar.Text = tMonto.Rows(0).Item("tgCambioDol").ToString
            tbMontoBs.Value = tMonto.Rows(0).Item("tgMontBs").ToString
            tbMontoDolar.Value = tMonto.Rows(0).Item("tgMontDol").ToString

            txtMontoPagado1.Text = tbMontoBs.Value + (tbMontoDolar.Value * IIf(cbCambioDolar.Text = "", 0, Convert.ToDecimal(cbCambioDolar.Text))) + tbMontoTarej.Value

            If Convert.ToDecimal(tbTotalBs.Text) <> 0 And Convert.ToDecimal(txtMontoPagado1.Text) >= Convert.ToDecimal(tbTotalBs.Text) Then
                txtCambio1.Text = Convert.ToDecimal(txtMontoPagado1.Text) - Convert.ToDecimal(tbTotalBs.Text)
            Else
                txtCambio1.Text = "0.00"
            End If
        End If
        LblPaginacion.Text = Str(grVentas.Row + 1) + "/" + grVentas.RowCount.ToString

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
        With grdetalle.RootTable.Columns("yfdetprod")
            .Caption = "Cod.Barra"
            .Width = 100
            .Visible = False

        End With
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
            .Width = 320
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
            .FormatString = "0.00000"
            .Caption = "Precio U.".ToUpper
        End With
        With grdetalle.RootTable.Columns("tbptot")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00000"
            .Caption = "SubTotal".ToUpper
        End With
        With grdetalle.RootTable.Columns("tbporc")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00000"
            .Caption = "P.Desc(%)".ToUpper

        End With
        With grdetalle.RootTable.Columns("tbdesc")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00000"
            .Caption = "M.Desc".ToUpper
        End With
        With grdetalle.RootTable.Columns("tbtotdesc")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00000"
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

    Private Sub _prCargarVenta()
        Dim dt As New DataTable
        dt = L_fnGeneralVentaCombustible()
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

        With grVentas.RootTable.Columns("taven")
            .Width = 160
            .Visible = False
        End With

        With grVentas.RootTable.Columns("vendedor")
            .Width = 150
            .Visible = True
            .Caption = "VENDEDOR".ToUpper
        End With
        With grVentas.RootTable.Columns("institucion")
            .Width = 250
            .Visible = True
            .Caption = "INSTITUCION".ToUpper
        End With
        With grVentas.RootTable.Columns("aabdes")
            .Width = 150
            .Visible = True
            .Caption = "SURTIDOR".ToUpper
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
        With grVentas.RootTable.Columns("ydcod")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("cliente")
            .Width = 250
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "CLIENTE"
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
            .Caption = "NRO. CAJA"
            .Visible = False
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With
        With grVentas.RootTable.Columns("total")
            .Width = 150
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "TOTAL"
            .FormatString = "0.00000"
        End With
        With grVentas
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla

        End With

        With grVentas.RootTable.Columns("tcentregado")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        With grVentas.RootTable.Columns("tcentregadoci")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tcdespachador")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tcplaca")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tcretiro")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tcnitretiro")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tcfacnombre")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tcfacnit")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tcsolicitud")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tcsurtidor")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tctiposurtidor")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        If (dt.Rows.Count <= 0) Then
            _prCargarDetalleVenta(-1)
        End If

        Dim _pos As Integer = grVentas.Row
        If grVentas.RowCount > 0 Then
            _pos = grVentas.RowCount - 1
            ''  _prMostrarRegistro(_pos)
            grVentas.Row = _pos
        End If
    End Sub

    Dim fechaOriginal As String
    Public Sub _prGuardar()

        txtMontoPagado1.Text = tbTotalBs.Text
        If _ValidarCampos() = False Then
            Exit Sub
        End If


        If (tbCodigo.Text = String.Empty) Then
            Dim dt1 As DataTable
            Dim cuenta As String
            dt1 = ObtenerNumCuenta("Institucion", _CodEmpleado)
            cuenta = dt1.Rows(0).Item(7)
            If cuenta = "0" Then
                MessageBox.Show("ESTA INSTITUCION NO CUENTA CON NUMERO DE CUENTA PARA GENERAR ASIENTO CONTABLE CONTACTARSE CON EL ADM. SISTEMAS")
            Else
                Dim resultado = New DialogResult()
                Dim mensaje = New mensajePersonalizado()
                mensaje.Label4.Text = IIf(swTipoVenta.Value = True, "CONTADO", "CREDITO")
                mensaje.Label3.Text = tbFechaVenta.Text
                mensaje.Label2.Text = "LA FECHA DE VENTA DE SURTIDOR PROPIO ES:"
                mensaje.Label6.Text = cbSurtidor.Text
                resultado = mensaje.ShowDialog()
                If resultado = DialogResult.OK Then
                    _GuardarNuevo()
                Else

                End If
            End If


        Else
            If (tbCodigo.Text <> String.Empty) Then
                If tbFechaVenta.Text = fechaOriginal Then
                    Dim dt11 As DataTable
                    Dim cuenta1 As String
                    dt11 = ObtenerNumCuenta("Institucion", _CodEmpleado)
                    cuenta1 = dt11.Rows(0).Item(7)
                    If cuenta1 = "0" Then
                        MessageBox.Show("ESTA INSTITUCION NO CUENTA CON NUMERO DE CUENTA PARA GENERAR ASIENTO CONTABLE CONTACTARSE CON EL ADM. SISTEMAS")
                    Else
                        Dim resultado = New DialogResult()
                        Dim mensaje = New mensajePersonalizado()
                        mensaje.Label4.Text = IIf(swTipoVenta.Value = True, "CONTADO", "CREDITO")
                        mensaje.Label3.Text = tbFechaVenta.Text
                        mensaje.Label2.Text = "LA FECHA DE VENTA DE SURTIDOR PROPIO ES:"
                        mensaje.Label6.Text = cbSurtidor.Text
                        resultado = mensaje.ShowDialog()
                        If resultado = DialogResult.OK Then
                            _prGuardarModificado()
                        Else

                        End If
                    End If
                Else
                    MessageBox.Show("PARA MODIFICAR FECHA CONTACTE CON EL DESARROLLADOR POR FAVOR!")
                End If



            End If
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
        CType(grdetalle.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, 0, 0, "", 0, "", "", 0, 0, 0, "", 0, 0, 0, 0, 0, "", 0, "20170101", CDate("2017/01/01"), 0, Now.Date, "", "", 0, Bin.GetBuffer, 0, 0, 0)
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
        Return tbFechaVenta.IsInputReadOnly = False
    End Function

    Public Sub _fnObtenerFilaDetalle(ByRef pos As Integer, numi As Integer)
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _numi As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbnumi")
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

        '' CalcularDescuentosTotal()

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
        Dim TotalCosto As Double = 0
        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1

            If (dt.Rows(i).Item("estado") >= 0) Then
                TotalDescuento = TotalDescuento + dt.Rows(i).Item("tbptot")
                TotalCosto = TotalCosto + dt.Rows(i).Item("tbptot2")
            End If
        Next


        'grdetalle.UpdateData()
        Dim montoDo As Decimal
        Dim montodesc As Double = tbMdesc.Value
        Dim pordesc As Double = ((montodesc * 100) / TotalDescuento)
        tbPdesc.Value = pordesc
        Dim subtotal = Convert.ToDouble(Format(TotalDescuento, "0.00000"))
        tbSubTotal.Value = subtotal

        'tbTotalBs.Text = total.ToString()
        tbTotalBs.Text = tbSubTotal.Value - montodesc
        montoDo = Convert.ToDecimal(tbTotalBs.Text) / IIf(cbCambioDolar.Text = "", 1, Convert.ToDecimal(cbCambioDolar.Text))
        tbTotalDo.Text = Format(montoDo, "0.00")
        tbIce.Value = TotalCosto * (gi_ICE / 100)
        'calcularCambio()

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


            If (tbRetSurtidor.Text = String.Empty) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor llene el campo retiro surtidor.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                tbRetSurtidor.Focus()
                Return False
            End If

            If (tbNitRetSurtidor.Text = String.Empty) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor ponga el nit del retiro surtidor.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                tbNitRetSurtidor.Focus()
                Return False
            End If

            'Validar datos de factura

            If (SwSurtidor.Value = True) Then
                'If (tbNit.Text = String.Empty) Then
                '    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                '    ToastNotification.Show(Me, "Por Favor ponga el nit del cliente.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                '    tbNit.Focus()
                '    Return False
                'End If

                'If (TbNombre1.Text = String.Empty) Then
                '    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                '    ToastNotification.Show(Me, "Por Favor ponga la razon social del cliente.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                '    TbNombre1.Focus()
                '    Return False
                'End If

                If (tbTramOrden.Text = String.Empty) Then
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "Por Favor ponga el tramite de orden.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    tbTramOrden.Focus()
                    Return False
                End If

                If (tbNitTraOrden.Text = String.Empty) Then
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "Por Favor ponga el nit del tramite de orden.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    tbNitTraOrden.Focus()
                    Return False
                End If

                If (tbPlaca.Text = String.Empty) Then
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "Por Favor ponga el número de placa.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    tbPlaca.Focus()
                    Return False
                End If

            End If

            If swTipoVenta.Value = True Then
                If (Convert.ToDecimal(txtMontoPagado1.Text) = 0) Then
                    Throw New Exception("El monto Pagado debe ser mayor 0")
                    Return False
                End If
                If (Convert.ToDecimal(txtMontoPagado1.Text) < Convert.ToDecimal(tbTotalBs.Text)) Then
                    Throw New Exception("El monto Pagado debe ser mayor al monto Total")
                    Return False
                End If
            End If

            If (grdetalle.RowCount = 1) Then
                grdetalle.Row = grdetalle.RowCount - 1
                If (grdetalle.GetValue("tbty5prod") = 0) Then
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "Por Favor Seleccione  un detalle de producto".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    Return False
                End If

            End If

            'Validación para controlar caducidad de Dosificacion
            'If tbNit.Text <> String.Empty Then
            '    Dim fecha As String = Now.Date
            '    Dim dtDosificacion As DataSet = L_DosificacionCajas("1", "1", fecha, gs_NroCaja)
            '    If dtDosificacion.Tables(0).Rows.Count = 0 Then
            '        'dtDosificacion.Tables.Cast(Of DataTable)().Any(Function(x) x.DefaultView.Count = 0)
            '        Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            '        ToastNotification.Show(Me, "La Dosificación para las facturas ya caducó, ingrese nueva dosificación".ToUpper, img, 3500, eToastGlowColor.Red, eToastPosition.BottomCenter)
            '        Return False
            '    End If
            'End If

            Return True
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
            Return False
        End Try

    End Function
    Private Sub _prInsertarMontoNuevo(ByRef tabla As DataTable)
        tabla.Rows.Add(0, tbMontoBs.Value, tbMontoDolar.Value, tbMontoTarej.Value, cbCambioDolar.Text, 0)
    End Sub
    Private Sub _prInsertarMontoModificar(ByRef tabla As DataTable)
        tabla.Rows.Add(tbCodigo.Text, tbMontoBs.Value, tbMontoDolar.Value, tbMontoTarej.Value, cbCambioDolar.Text, 2)
    End Sub
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
                                Dim total As Decimal = CStr(Format(precio * saldo, "####0.00000"))

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
                                    Dim total As Decimal = CStr(Format(precio * inventario, "####0.00000"))
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
                            Dim total As Decimal = CStr(Format(precio * saldo, "####0.00000"))
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
        detalle.DefaultView.RowFilter = "estado >= '" + 0.ToString() + "'"
        detalle = detalle.DefaultView.ToTable
        For Each fila As DataRow In detalle.Rows
            Dim idProducto = fila.Item("tbty5prod")
            If aux <> idProducto Then
                dtSaldos = L_fnObteniendoSaldosTI001(fila.Item("tbty5prod"), 3)
                Dim inventario = dtSaldos.Compute("SUM(iccven)", String.Empty)

                detalle.DefaultView.RowFilter = "tbty5prod = '" + fila.Item("tbty5prod").ToString() + "'"
                Dim productoRepeditos = detalle.DefaultView.ToTable
                Dim saldo = productoRepeditos.Compute("SUM(tbcmin)", String.Empty)
                'If (tbCodigo.Text <> String.Empty) Then
                '    inventario = inventario + saldo
                'End If

                'Dim saldo = fila.Item("tbcmin")
                If inventario < saldo Then
                    Dim mensaje As String = "No existe stock para el producto: " + fila.Item("producto") + " stock actual = " + inventario.ToString()
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
            Return True
        End If
        Return True
    End Function

    Public Sub _GuardarNuevo()

        Try
            Dim numi As String = ""
            Dim tabla As DataTable = L_fnMostrarMontos(0)
            Dim factura = gb_FacturaEmite
            _prInsertarMontoNuevo(tabla)
            ''Verifica si existe estock para los productos
            If SwSurtidor.Value = True Then
                If _prExisteStockParaProducto() Then
                    Dim dtDetalle As DataTable = rearmarDetalle()
                    Dim res As Boolean = L_fnGrabarVentaCombustible(numi, "", tbFechaVenta.Value.ToString("yyyy/MM/dd"), gi_userNumi,
                                                         IIf(swTipoVenta.Value = True, 1, 0), IIf(swTipoVenta.Value = True,
                                                        Now.Date.ToString("yyyy/MM/dd"), tbFechaVenc.Value.ToString("yyyy/MM/dd")),
                                                         _CodCliente, IIf(swMoneda.Value = True, 1, 0),
                                                        tbTramOrden.Text + " - " + tbNitTraOrden.Text + " - PLACA: " + tbPlaca.Text + " - Autoriz.:" + tbAutoriza.Text, tbMdesc.Value, tbIce.Value, tbTotalBs.Text,
                                                          dtDetalle, cbSucursal.Value, 0, tabla, _CodEmpleado, Programa, tbTramOrden.Text,
                                                          tbNitTraOrden.Text, cbDespachador.Value, tbPlaca.Text, tbRetSurtidor.Text, tbNitRetSurtidor.Text,
                                                          TbNombre1.Text, tbNit.Text, cbTipoSolicitud.Value, cbSurtidor.Value, SwSurtidor.Value, tbAutoriza.Text, (grdetalle.GetValue("tbcmin") * 0.01) / 6.96)
                    If res Then
                        tbCodigo.Text = numi
                        contabilizar()
                        _prImiprimirNotaVenta(numi)

                    End If

                    Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                    ToastNotification.Show(Me, "Código de Venta ".ToUpper + tbCodigo.Text + " Grabado con Exito.".ToUpper,
                                              img, 2000,
                                              eToastGlowColor.Green,
                                              eToastPosition.TopCenter
                                              )

                    _Limpiar()


                    Table_Producto = Nothing
                    _prSalir()
                    _prCargarVenta()

                Else
                    Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                    ToastNotification.Show(Me, "La Venta no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

                End If
                'End If

            Else
                Dim dtDetalle As DataTable = rearmarDetalle()
                Dim res As Boolean = L_fnGrabarVentaCombustible(numi, "", tbFechaVenta.Value.ToString("yyyy/MM/dd"), gi_userNumi,
                                                     IIf(swTipoVenta.Value = True, 1, 0), IIf(swTipoVenta.Value = True,
                                                    Now.Date.ToString("yyyy/MM/dd"), tbFechaVenc.Value.ToString("yyyy/MM/dd")),
                                                     _CodCliente, IIf(swMoneda.Value = True, 1, 0),
                                                     tbTramOrden.Text + " - " + tbNitTraOrden.Text + " - PLACA: " + tbPlaca.Text + " - Autoriz.:" + tbAutoriza.Text, tbMdesc.Value, tbIce.Value, tbTotalBs.Text,
                                                      dtDetalle, cbSucursal.Value, 0, tabla, _CodEmpleado, Programa, tbTramOrden.Text, tbNitTraOrden.Text, cbDespachador.Value, tbPlaca.Text, tbRetSurtidor.Text, tbNitRetSurtidor.Text, TbNombre1.Text, tbNit.Text, cbTipoSolicitud.Value, cbSurtidor.Value, SwSurtidor.Value, tbAutoriza.Text, grdetalle.GetValue("tbcmin") * 0.01)
                If res Then

                    _prImiprimirNotaVenta(numi)
                End If

                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                ToastNotification.Show(Me, "Código de Venta ".ToUpper + tbCodigo.Text + " Grabado con Exito.".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter
                                          )
                _Limpiar()


                Table_Producto = Nothing
                _prSalir()
                _prCargarVenta()
                'contabilizarPrestamo()
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try


    End Sub
    Public Sub _prImiprimirNotaVenta(numi As String)
        Dim ef = New Efecto


        ef.tipo = 2
        ef.Context = "MENSAJE PRINCIPAL".ToUpper
        ef.Header = "¿desea imprimir la nota de venta?".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            P_GenerarReporte(numi)
        End If
    End Sub
    Public Sub _prImiprimirFacturaPreimpresa(numi As String)
        Dim ef = New Efecto


        ef.tipo = 2
        ef.Context = "MENSAJE PRINCIPAL".ToUpper
        ef.Header = "¿desea imprimir la factura Preimpresa?".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            P_GenerarReporteFactura(numi)
        End If
    End Sub
    Private Sub _prGuardarModificado()
        Dim tabla As DataTable = L_fnMostrarMontos(0)
        _prInsertarMontoModificar(tabla)
        If _prExisteStockParaProducto() Then
            Dim dtDetalle As DataTable = rearmarDetalle()
            Dim res As Boolean = L_fnModificarVentaDieselPropio(tbCodigo.Text, "", tbFechaVenta.Value.ToString("yyyy/MM/dd"), gi_userNumi,
                                                     IIf(swTipoVenta.Value = True, 1, 0), IIf(swTipoVenta.Value = True,
                                                    Now.Date.ToString("yyyy/MM/dd"), tbFechaVenc.Value.ToString("yyyy/MM/dd")),
                                                     _CodCliente, IIf(swMoneda.Value = True, 1, 0),
                                                      tbTramOrden.Text + " - " + tbNitTraOrden.Text + " - PLACA: " + tbPlaca.Text + " - Autoriz.:" + tbAutoriza.Text, tbMdesc.Value, tbIce.Value, tbTotalBs.Text,
                                                      dtDetalle, cbSucursal.Value, 0, tabla, _CodEmpleado, Programa, tbTramOrden.Text, tbNitTraOrden.Text, cbDespachador.Value, tbPlaca.Text, tbRetSurtidor.Text, tbNitRetSurtidor.Text, TbNombre1.Text, tbNit.Text, cbTipoSolicitud.Value, cbSurtidor.Value, SwSurtidor.Value, tbAutoriza.Text)
            If res Then
                L_Asiento_Borrar(codCont)
                contabilizarPrestamoDetalle()
                '_prImiprimirNotaVenta(tbCodigo.Text)
                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                ToastNotification.Show(Me, "Código de Venta ".ToUpper + tbCodigo.Text + " Modificado con Exito.".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter
                                          )
                _prCargarVenta()
                _prSalir()
            Else
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, "La Venta no pudo ser Modificada".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

            End If
        End If
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
    Private Sub grVentas_SelectionChanged(sender As Object, e As EventArgs) Handles grVentas.SelectionChanged
        If (grVentas.RowCount >= 0 And grVentas.Row >= 0) Then
            _prMostrarRegistro(grVentas.Row)
        End If
    End Sub
    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        Dim _pos As Integer = grVentas.Row
        If _pos < grVentas.RowCount - 1 And _pos >= 0 Then
            _pos = grVentas.Row + 1
            '' _prMostrarRegistro(_pos)
            grVentas.Row = _pos
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
            ''_prMostrarRegistro(_MPos)
            grVentas.Row = _MPos
        End If
    End Sub


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


    Private Sub P_prCargarVariablesIndispensables()
        If (gb_FacturaEmite) Then
            gi_IVA = CDbl(IIf(L_fnGetIVA().Rows(0).Item("scdebfis").ToString.Equals(""), gi_IVA, L_fnGetIVA().Rows(0).Item("scdebfis").ToString))
            gi_ICE = CDbl(IIf(L_fnGetICE().Rows(0).Item("scice").ToString.Equals(""), gi_ICE, L_fnGetICE().Rows(0).Item("scice").ToString))
        End If

    End Sub

    Private Sub P_prCargarParametro()
        'El sistema factura?

        'Si factura, preguntar si, Se incluye el Importe ICE / IEHD / TASAS?
        If (gb_FacturaEmite) Then
            lbIce.Visible = gb_FacturaIncluirICE
            tbIce.Visible = gb_FacturaIncluirICE
        Else
            lbIce.Visible = False
            tbIce.Visible = False
        End If

    End Sub
    Private Sub P_GenerarReporte(numi As String)
        Dim dt As DataTable = L_fnVentaNotaDeVenta(numi)
        If (gb_DetalleProducto) Then
            ponerDescripcionProducto(dt)
        End If
        'Dim total As Decimal = dt.Compute("SUM(Total)", "")
        Dim total As Decimal = Convert.ToDecimal(tbTotalBs.Text)
        Dim totald As Double = (total * 6.96)
        Dim fechaven As String = dt.Rows(0).Item("fechaventa")
        Dim retiro As String = dt.Rows(0).Item("RETIRO")
        Dim fechaImpresion As String = Today.ToLongDateString
        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If
        Dim ParteEntera As Long
        Dim ParteDecimal As Decimal
        Dim ParteDecimal2 As Decimal
        ParteEntera = Int(total)
        ParteDecimal = total - Math.Truncate(total)
        ParteDecimal2 = CDbl(ParteDecimal) * 100


        Dim li As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(ParteEntera)) + " " +
        IIf(ParteDecimal2.ToString.Equals("0"), "00", ParteDecimal2.ToString) + "/100 Bolivianos"

        ParteEntera = Int(totald)
        ParteDecimal = totald - Math.Truncate(totald)
        ParteDecimal2 = CDbl(ParteDecimal) * 100

        Dim lid As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(ParteEntera)) + " " +
        IIf(ParteDecimal2.ToString.Equals("0"), "00", ParteDecimal2.ToString) + "/100 Dolares"
        Dim _Hora As String = Now.Hour.ToString + ":" + Now.Minute.ToString
        Dim _Ds2 = L_Reporte_Factura_Cia("2")
        Dim dt2 As DataTable = L_fnNameReporte()
        Dim ParEmp1 As String = ""
        Dim ParEmp2 As String = ""
        Dim ParEmp3 As String = ""
        Dim ParEmp4 As String = ""
        If (dt2.Rows.Count > 0) Then
            ParEmp1 = dt2.Rows(0).Item("Empresa1").ToString
            ParEmp2 = dt2.Rows(0).Item("Empresa2").ToString
            ParEmp3 = dt2.Rows(0).Item("Empresa3").ToString
            ParEmp4 = dt2.Rows(0).Item("Empresa4").ToString
        End If

        Dim _Ds3 = L_ObtenerRutaImpresora("2") ' Datos de Impresion de Facturación
        If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
            P_Global.Visualizador = New Visualizador 'Comentar
        End If
        Dim _FechaAct As String
        Dim _FechaPar As String
        Dim _Fecha() As String
        Dim _Meses() As String = {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"}
        _FechaAct = fechaven
        _Fecha = Split(_FechaAct, "-")
        _FechaPar = "Cochabamba, " + _Fecha(0).Trim + " De " + _Meses(_Fecha(1) - 1).Trim + " Del " + _Fecha(2).Trim
        Dim objrep As Object = Nothing
        Dim empresaId = ObtenerEmpresaHabilitada()
        Dim empresaHabilitada As DataTable = ObtenerEmpresaTipoReporte(empresaId, Convert.ToInt32(ENReporte.NOTAVENTA))
        For Each fila As DataRow In empresaHabilitada.Rows
            Select Case fila.Item("TipoReporte").ToString
                Case ENReporteTipo.NOTAVENTA_Carta
                    objrep = New R_NotaVenta_Carta_Diesel
                    SetParametrosNotaVenta(dt, total, li, _Hora, _Ds2, _Ds3, fila.Item("TipoReporte").ToString, objrep, retiro)
                Case ENReporteTipo.NOTAVENTA_Ticket
                    objrep = New R_NotaVenta_7_5X100
                    SetParametrosNotaVenta(dt, total, li, _Hora, _Ds2, _Ds3, fila.Item("TipoReporte").ToString, objrep, "")
            End Select
        Next
    End Sub

    Private Sub SetParametrosNotaVenta(dt As DataTable, total As Decimal, li As String, _Hora As String, _Ds2 As DataSet, _Ds3 As DataSet, tipoReporte As String, objrep As Object, retiro As String)

        Select Case tipoReporte
            Case ENReporteTipo.NOTAVENTA_Carta
                objrep.SetDataSource(dt)
                objrep.SetParameterValue("Literal", li)
                objrep.SetParameterValue("TipoVenta", IIf(swTipoVenta.Value = True, "CONTADO", "CRÉDITO"))
                objrep.SetParameterValue("Logo", gb_UbiLogo)
                objrep.SetParameterValue("NotaAdicional1", gb_NotaAdicional)
                objrep.SetParameterValue("Descuento", tbMdesc.Value)
                objrep.SetParameterValue("fechaImpresion", Today.Date)
                objrep.SetParameterValue("Total", total)
                objrep.SetParameterValue("retiro", retiro)
            Case ENReporteTipo.NOTAVENTA_Ticket
                objrep.SetDataSource(dt)
                objrep.SetParameterValue("ECasaMatriz", _Ds2.Tables(0).Rows(0).Item("scsuc").ToString)
                objrep.SetParameterValue("ECiudadPais", _Ds2.Tables(0).Rows(0).Item("scpai").ToString)
                objrep.SetParameterValue("EDuenho", _Ds2.Tables(0).Rows(0).Item("scnom").ToString) '?
                objrep.SetParameterValue("Direccionpr", _Ds2.Tables(0).Rows(0).Item("scdir").ToString)
                objrep.SetParameterValue("Hora", _Hora)
                objrep.SetParameterValue("ENombre", _Ds2.Tables(0).Rows(0).Item("scneg").ToString) '?
                objrep.SetParameterValue("Literal1", li)
        End Select
        If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
            P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
            P_Global.Visualizador.ShowDialog() 'Comentar
            P_Global.Visualizador.BringToFront() 'Comentar
        Else
            Dim pd As New PrintDocument()
            pd.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
            If (Not pd.PrinterSettings.IsValid) Then
                ToastNotification.Show(Me, "La Impresora ".ToUpper + _Ds3.Tables(0).Rows(0).Item("cbrut").ToString + Chr(13) + "No Existe".ToUpper,
                                       My.Resources.WARNING, 5 * 1000,
                                       eToastGlowColor.Blue, eToastPosition.BottomRight)
            Else
                objrep.PrintOptions.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString

                Dim c As Integer
                Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                doctoprint.PrinterSettings.PrinterName = _Ds3.Tables(0).Rows(0).Item("cbrut").ToString
                Dim rawKind As Integer
                For c = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                    If doctoprint.PrinterSettings.PaperSizes(c).PaperName = "orden retiro" Then
                        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(c).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(c)))
                        Exit For
                    End If
                Next
                objrep.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)

                objrep.PrintToPrinter(1, True, 0, 0)
            End If
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

    Private Sub P_GenerarReporteFactura(numi As String)
        Dim dt As DataTable = L_fnVentaFactura(numi)
        Dim total As Double = dt.Compute("SUM(Total)", "")

        Dim ParteEntera As Long
        Dim ParteDecimal As Double
        ParteEntera = Int(total)
        ParteDecimal = total - ParteEntera
        Dim li As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(ParteEntera)) + " con " +
        IIf(ParteDecimal.ToString.Equals("0"), "00", ParteDecimal.ToString) + "/100 Bolivianos"


        Dim objrep As New R_FacturaFarmacia
        '' GenerarNro(_dt)
        ''objrep.SetDataSource(Dt1Kardex)
        'imprimir
        If PrintDialog1.ShowDialog = DialogResult.OK Then
            objrep.SetDataSource(dt)
            objrep.SetParameterValue("TotalEscrito", li)
            objrep.SetParameterValue("nit", tbNit.Text)
            objrep.SetParameterValue("Total", total)
            objrep.SetParameterValue("cliente", TbNombre1.Text + " " + TbNombre2.Text)
            objrep.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName

            objrep.PrintToPrinter(1, False, 1, 1)
            objrep.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
        End If
        'objrep.SetDataSource(dt)
        'objrep.SetParameterValue("TotalEscrito", li)
        'objrep.SetParameterValue("nit", TbNit.Text)
        'objrep.SetParameterValue("Total", total)
        'P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
        'P_Global.Visualizador.Show() 'Comentar
        'P_Global.Visualizador.BringToFront() 'Comentar



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

    Private Sub F0_VentaComb_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _IniciarTodo()
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click

        _Limpiar()
        _prhabilitar()
        'AsignarClienteEmpleado()
        cbSucursal.Value = 3
        lbNroCaja.Text = gs_user
        LabelAlmacen.Text = gi_userSuc
        LabelAlmacen.Text = cbSucursal.Text
        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
        btnGrabar.Enabled = True
        PanelNavegacion.Enabled = False
        tbCliente.Select()

        _Nuevo = True
        cbTipoSolicitud.Value = 1
        cbDespachador.Value = 1
        cbSurtidor.SelectedIndex = 0
    End Sub
    Private Sub _prCargarProductos(_cliente As String)
        If (cbSucursal.SelectedIndex < 0) Then
            Return
        End If
        Dim nombreGrupos As DataTable = L_fnNameLabel()
        Dim dt As New DataTable


        dt = L_fnListarProductoDiesel(cbSucursal.Value, _cliente, CType(grdetalle.DataSource, DataTable))

            grProductos.DataSource = dt
        grProductos.RetrieveStructure()
        grProductos.AlternatingColors = True


    End Sub
    Public Sub InsertarProductosSinLote()
        Dim pos As Integer = -1
        grdetalle.Row = grdetalle.RowCount - 1
        _fnObtenerFilaDetalle(pos, grdetalle.GetValue("tbnumi"))
        Dim existe As Boolean = _fnExisteProducto(grProductos.GetValue("yfnumi"))
        If ((pos >= 0) And (Not existe)) Then
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbty5prod") = grProductos.GetValue("yfnumi")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("codigo") = grProductos.GetValue("yfcprod")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcbarra") = grProductos.GetValue("yfcbarra")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = grProductos.GetValue("yfcdprod1")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbumin") = grProductos.GetValue("yfumin")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("unidad") = grProductos.GetValue("UnidMin")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas") = grProductos.GetValue("yhprecio")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = grProductos.GetValue("yhprecio")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = grProductos.GetValue("yhprecio")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbFamilia") = grProductos.GetValue("yfgr4")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbProveedorId") = grProductos.GetValue("DescuentoId")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = 1
            'If (gb_FacturaIncluirICE) Then
            '    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = grProductos.GetValue("pcos")
            'Else
            '    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = 0
            'End If
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos") = grProductos.GetValue("pcos")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = grProductos.GetValue("pcos")

            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = grProductos.GetValue("stock")
            _prCalcularPrecioTotal()
            _DesHabilitarProductos()
        Else
            If (existe) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "El producto ya existe en el detalle".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            End If
        End If
    End Sub

    Private Sub _DesHabilitarProductos()
        GPanelProductos.Visible = False
        PanelInferior.Visible = True


        grdetalle.Select()
        grdetalle.Col = 5
        grdetalle.Row = grdetalle.RowCount - 1

    End Sub
    Private Sub grProductos_KeyDown(sender As Object, e As KeyEventArgs) Handles grProductos.KeyDown

    End Sub

    Private Sub grdetalle_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grdetalle.EditingCell
        If (_fnAccesible()) Then
            'Habilitar solo las columnas de Precio, %, Monto y Observación
            'If (e.Column.Index = grdetalle.RootTable.Columns("yfcbarra").Index Or
            If (e.Column.Index = grdetalle.RootTable.Columns("tbcmin").Index Or
                e.Column.Index = grdetalle.RootTable.Columns("tbporc").Index Or
                e.Column.Index = grdetalle.RootTable.Columns("tbpbas").Index) Then
                ''e.Column.Index = grdetalle.RootTable.Columns("tbdesc").Index
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Else
            e.Cancel = True
        End If
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
            If (e.Column.Index = grdetalle.RootTable.Columns("tbcmin").Index) Or (e.Column.Index = grdetalle.RootTable.Columns("tbpbas").Index) Then
                If (Not IsNumeric(grdetalle.GetValue("tbcmin")) Or grdetalle.GetValue("tbcmin").ToString = String.Empty) Then

                    'L_fnListarDescuentosTodos
                    Dim lin As Integer = grdetalle.GetValue("tbnumi")
                    Dim pos As Integer = -1
                    Dim rowIndex As Integer = grdetalle.Row
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = 1
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas")

                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas")
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos")
                    grdetalle.SetValue("tbcmin", 1)
                    grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))


                    CalcularDescuentos(grdetalle.GetValue("tbty5prod"), 1, grdetalle.GetValue("tbpbas"), pos)

                    P_PonerTotal(rowIndex)
                Else
                    If (grdetalle.GetValue("tbcmin") > 0) Then
                        Dim rowIndex As Integer = grdetalle.Row
                        Dim porcdesc As Double = grdetalle.GetValue("tbporc")
                        Dim montodesc As Double = ((grdetalle.GetValue("tbpbas") * grdetalle.GetValue("tbcmin")) * (porcdesc / 100))
                        Dim lin As Integer = grdetalle.GetValue("tbnumi")
                        Dim pos As Integer = -1
                        _fnObtenerFilaDetalle(pos, lin)
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = grdetalle.GetValue("tbpbas") * grdetalle.GetValue("tbcmin")

                        'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = montodesc
                        'grdetalle.SetValue("tbdesc", montodesc)
                        'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = (grdetalle.GetValue("tbpbas") * grdetalle.GetValue("tbcmin")) - montodesc

                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = grdetalle.GetValue("tbpcos") * grdetalle.GetValue("tbcmin")


                        CalcularDescuentos(grdetalle.GetValue("tbty5prod"), grdetalle.GetValue("tbcmin"), grdetalle.GetValue("tbpbas"), pos)


                        P_PonerTotal(rowIndex)
                        CalculoDescuentoXProveedor()

                    Else
                        Dim rowIndex As Integer = grdetalle.Row
                        Dim lin As Integer = grdetalle.GetValue("tbnumi")
                        Dim pos As Integer = -1
                        _fnObtenerFilaDetalle(pos, lin)
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbcmin") = 1
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas")

                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpbas")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot2") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbpcos")
                        grdetalle.SetValue("tbcmin", 1)
                        grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))

                        CalcularDescuentos(grdetalle.GetValue("tbty5prod"), grdetalle.GetValue("tbcmin"), grdetalle.GetValue("tbpbas"), pos)


                        _prCalcularPrecioTotal()
                        P_PonerTotal(rowIndex)
                        CalculoDescuentoXProveedor()
                        'grdetalle.SetValue("tbcmin", 1)
                        'grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))

                    End If
                End If
            End If
            '''''''''''''''''''''PORCENTAJE DE DESCUENTO '''''''''''''''''''''
            If (e.Column.Index = grdetalle.RootTable.Columns("tbporc").Index) Then
                If (Not IsNumeric(grdetalle.GetValue("tbporc")) Or grdetalle.GetValue("tbporc").ToString = String.Empty) Then

                    'grDetalle.GetRow(rowIndex).Cells("cant").Value = 1
                    '  grDetalle.CurrentRow.Cells.Item("cant").Value = 1
                    Dim lin As Integer = grdetalle.GetValue("tbnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot")
                    'grdetalle.SetValue("tbcmin", 1)
                    'grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))
                Else
                    If (grdetalle.GetValue("tbporc") > 0 And grdetalle.GetValue("tbporc") <= 100) Then

                        Dim porcdesc As Double = grdetalle.GetValue("tbporc")
                        Dim montodesc As Double = (grdetalle.GetValue("tbptot") * (porcdesc / 100))
                        Dim lin As Integer = grdetalle.GetValue("tbnumi")
                        Dim pos As Integer = -1
                        _fnObtenerFilaDetalle(pos, lin)
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = montodesc
                        grdetalle.SetValue("tbdesc", montodesc)

                        Dim rowIndex As Integer = grdetalle.Row
                        P_PonerTotal(rowIndex)
                        CalculoDescuentoXProveedor()
                    Else
                        Dim lin As Integer = grdetalle.GetValue("tbnumi")
                        Dim pos As Integer = -1
                        _fnObtenerFilaDetalle(pos, lin)
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot")
                        grdetalle.SetValue("tbporc", 0)
                        grdetalle.SetValue("tbdesc", 0)
                        grdetalle.SetValue("tbtotdesc", grdetalle.GetValue("tbptot"))
                        _prCalcularPrecioTotal()
                        'grdetalle.SetValue("tbcmin", 1)
                        'grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))

                    End If
                End If
            End If


            '''''''''''''''''''''MONTO DE DESCUENTO '''''''''''''''''''''
            If (e.Column.Index = grdetalle.RootTable.Columns("tbdesc").Index) Then
                If (Not IsNumeric(grdetalle.GetValue("tbdesc")) Or grdetalle.GetValue("tbdesc").ToString = String.Empty) Then

                    'grDetalle.GetRow(rowIndex).Cells("cant").Value = 1
                    '  grDetalle.CurrentRow.Cells.Item("cant").Value = 1
                    Dim lin As Integer = grdetalle.GetValue("tbnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot")
                    'grdetalle.SetValue("tbcmin", 1)
                    'grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))
                Else
                    If (grdetalle.GetValue("tbdesc") > 0 And grdetalle.GetValue("tbdesc") <= grdetalle.GetValue("tbptot")) Then

                        Dim montodesc As Double = grdetalle.GetValue("tbdesc")
                        Dim pordesc As Double = ((montodesc * 100) / grdetalle.GetValue("tbptot"))

                        Dim lin As Integer = grdetalle.GetValue("tbnumi")
                        Dim pos As Integer = -1
                        _fnObtenerFilaDetalle(pos, lin)
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = montodesc
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = pordesc

                        grdetalle.SetValue("tbporc", pordesc)
                        Dim rowIndex As Integer = grdetalle.Row
                        P_PonerTotal(rowIndex)

                    Else
                        Dim lin As Integer = grdetalle.GetValue("tbnumi")
                        Dim pos As Integer = -1
                        _fnObtenerFilaDetalle(pos, lin)
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbporc") = 0
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbdesc") = 0
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("tbptot")
                        grdetalle.SetValue("tbporc", 0)
                        grdetalle.SetValue("tbdesc", 0)
                        grdetalle.SetValue("tbtotdesc", grdetalle.GetValue("tbptot"))
                        _prCalcularPrecioTotal()
                        'grdetalle.SetValue("tbcmin", 1)
                        'grdetalle.SetValue("tbptot", grdetalle.GetValue("tbpbas"))

                    End If
                End If
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
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
        tbSubTotal.Value = subTotalVenta
        tbMdesc.Value = subTotalDescuento
        tbTotalBs.Text = Format(subTotalVenta - subTotalDescuento, "0.00")
        montoDo = Convert.ToDecimal(tbTotalBs.Text) / IIf(cbCambioDolar.Text = "", 1, Convert.ToDecimal(cbCambioDolar.Text))
        tbTotalDo.Text = Format(montoDo, "0.00")
        calcularCambio()
    End Sub

    Private Function ObtenerSumaTotalXProveedor(detalleLista As List(Of DataRow), proveedorID As Integer) As Decimal
        Return (From proc In detalleLista
                Where proc.ItemArray(ENDetalleVenta.estado) >= 0 _
                    And proc.ItemArray(ENDetalleVenta.proveedorId) = proveedorID
                Select Convert.ToDecimal(proc.ItemArray(ENDetalleVenta.totalDescuento))).Sum()
    End Function

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

    Sub calcularCambio()
        If tbMontoBs.Value <> 0 And tbMontoBs.Text <> String.Empty Then
            txtMontoPagado1.Text = tbMontoBs.Value + (tbMontoDolar.Value * IIf(cbCambioDolar.Text = "", 0, Convert.ToDecimal(cbCambioDolar.Text))) + tbMontoTarej.Value
            If Convert.ToDecimal(tbTotalBs.Text) <> 0 And Convert.ToDecimal(txtMontoPagado1.Text) >= Convert.ToDecimal(tbTotalBs.Text) Then
                txtCambio1.Text = Convert.ToDecimal(txtMontoPagado1.Text) - Convert.ToDecimal(tbTotalBs.Text)
            Else
                txtCambio1.Text = "0.00"
            End If
        End If
    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        _prGuardar()

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
                    _CodCaneroUcg = Row.Cells("ydcod").Value
                    tbCliente.Text = Row.Cells("ydrazonsocial").Value
                    _dias = Row.Cells("yddias").Value
                    tbNit.Text = Row.Cells("ydnit").Value
                    TbNombre1.Text = Row.Cells("ydnomfac").Value
                    'tbNitFacturarA.Text = Row.Cells("ydnit").Value
                    'tbFact.Text = Row.Cells("ydnomfac").Value
                    Dim numiVendedor As Integer = IIf(IsDBNull(Row.Cells("ydnumivend").Value), 0, Row.Cells("ydnumivend").Value)
                    If (numiVendedor > 0) Then
                        ''tbVendedor.Text = Row.Cells("vendedor").Value
                        _CodEmpleado = Row.Cells("ydnumivend").Value
                        tbTramOrden.Select()
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

    Private Sub tbCliente_TextChanged(sender As Object, e As EventArgs) Handles tbCliente.TextChanged
        If btnNuevo.Enabled = False Then
            Dim dt As DataTable
            dt = L_fnListarCaneroInstitucion(_CodCliente)
            Dim row As DataRow = dt.Rows(dt.Rows.Count - 1)
            tbVendedor.Text = row("institucion")
            _CodInstitucion = row("id")

        End If
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Try
            'tbNit.Text = tbNitFacturarA.Text
            If (Not _fnAccesible()) Then



                _prImiprimirNotaVenta(tbCodigo.Text)


            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Public Function contabilizar() As Integer
        Dim codigoVenta = tbCodigo.Text
        Dim codCanero = "Entrega de diesel " + grdetalle.GetValue("tbcmin").ToString + " lts. / ." + tbCliente.Text + " " + Convert.ToString(_CodCaneroUcg) + " (" + codigoVenta + ")" 'obobs
        Dim total = Convert.ToDecimal(tbTotalBs.Text) / 6.96 'para obtener debe haber
        Dim dt, dt1, dtDetalle As DataTable
        Dim cuenta As String
        Dim debebs, haberbs, debeus, haberus As Double
        dt1 = ObtenerNumCuenta("Institucion", _CodInstitucion)  'obcuenta=ncuenta

        Dim resTO001 = L_fnGrabarTO001(12, Convert.ToInt32(codigoVenta), "false") 'numi cabecera to001

        For a As Integer = 8 To 8 Step 1
            dt = CargarConfiguracion("configuracion", a) 'oblin=orden
            dtDetalle = L_fnDetalleVenta1(codigoVenta)

            Dim oblin As Integer = 1
            Dim totalCosto As Double = 0.00
            For Each row In dt.Rows
                '    Select Case row("cuenta")

                If row("cuenta") = "-1" Then
                    For Each detalle In dtDetalle.Rows
                        cuenta = detalle("yfclot")
                        If row("dh") = 1 Then
                            debeus = (Convert.ToDouble(detalle("tbpcos")) * Convert.ToDouble(row("porcentaje"))) / 100
                            debebs = debeus * 6.96
                            haberus = 0.00
                            haberbs = 0.00
                            totalCosto = totalCosto + Convert.ToDouble(detalle("tbpcos"))
                        Else
                            haberus = (Convert.ToDouble(detalle("tbpcos")) * Convert.ToDouble(row("porcentaje"))) / 100
                            haberbs = haberus * 6.96
                            debeus = 0.00
                            debebs = 0.00
                            totalCosto = totalCosto + Convert.ToDouble(detalle("tbpcos"))
                        End If

                        Dim resTO00112 As Boolean = L_fnGrabarTO001(2, Convert.ToInt32(codigoVenta), resTO001, oblin, cuenta, codCanero, debebs, haberbs, debeus, haberus)
                        oblin = oblin + 1
                    Next

                    If row("cuenta") = "-1" Then
                        Continue For
                    End If

                End If
                If row("cuenta") = "-2" Then
                    cuenta = dt1.Rows(0).Item(7)

                Else
                    cuenta = row("cuenta")
                End If
                If row("dh") = 1 Then
                    debeus = (IIf(row("tipo") = 8, Convert.ToDouble(total), totalCosto) * Convert.ToDouble(row("porcentaje"))) / 100
                    debebs = debeus * 6.96
                    haberus = 0.00
                    haberbs = 0.00
                Else
                    haberus = (IIf(row("tipo") = 8, Convert.ToDouble(total), totalCosto) * Convert.ToDouble(row("porcentaje"))) / 100
                    haberbs = haberus * 6.96
                    debeus = 0.00
                    debebs = 0.00
                End If
                Dim resTO0011 As Boolean = L_fnGrabarTO001(2, Convert.ToInt32(codigoVenta), resTO001, oblin, cuenta, codCanero, debebs, haberbs, debeus, haberus)
                oblin = oblin + 1
            Next
        Next

        L_Actualiza_Venta_Contabiliza(codigoVenta, resTO001)
        Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
        ToastNotification.Show(Me, " Venta ".ToUpper + tbCodigo.Text + " Contabilizada con Exito.".ToUpper,
                                              img, 2000,
                                              eToastGlowColor.Green,
                                              eToastPosition.TopCenter
                                              )
        '_prCargarVenta()
    End Function


    Private Sub SwConta_ValueChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _prSalir()
    End Sub

    Private Sub SwSurtidor_ValueChanged(sender As Object, e As EventArgs) Handles SwSurtidor.ValueChanged
        If (SwSurtidor.Value = True) Then
            swTipoVenta.Value = False
            swTipoVenta.IsReadOnly = False
            cbSurtidor.Clear()
            _prCargarComboLibreria(cbSurtidor, 1, 10)
            cbSurtidor.Value = 1
            tbTramOrden.Visible = True
            tbNitTraOrden.Visible = True
            cbDespachador.Visible = True
            LabelX25.Visible = True
            LabelX27.Visible = True

            LabelX31.Visible = True
            If _Nuevo = True Then
                cbSucursal.Value = 3
            End If

        Else
            swTipoVenta.Value = False
            swTipoVenta.IsReadOnly = True
            cbSurtidor.Clear()
            _prCargarComboLibreria(cbSurtidor, 1, 8)
            cbSurtidor.Value = 0
            tbTramOrden.Visible = False
            tbNitTraOrden.Visible = False

            tbTramOrden.Clear()
            tbNitTraOrden.Clear()
            TbNombre1.Clear()
            tbNit.Clear()
            cbDespachador.Visible = False
            LabelX25.Visible = False
            LabelX27.Visible = False

            LabelX31.Visible = False
            If _Nuevo = True Then
                cbSucursal.Value = 3
            End If

        End If


    End Sub


    Private Sub grdetalle_FormattingRow(sender As Object, e As RowLoadEventArgs) Handles grdetalle.FormattingRow

    End Sub

    Private Sub swTipoVenta_ValueChanged(sender As Object, e As EventArgs) Handles swTipoVenta.ValueChanged
        If (swTipoVenta.Value = False) Then
            lbCredito.Visible = False
            tbFechaVenc.Visible = False
            tbFechaVenc.Value = DateAdd(DateInterval.Day, _dias, Now.Date)
        Else
            lbCredito.Visible = False
            tbFechaVenc.Visible = False
        End If
    End Sub



    Private Sub contabilizarPrestamoDetalle()
        Dim dt, dt1, dtDetalle As DataTable
        Dim codigoVenta = tbCodigo.Text
        'dt1 = L_BuscarCodCanero(1)

        Dim codCanero As String = "P/Ord:. " + tbCodigo.Text + " -Diesel " + Convert.ToString(Format(grdetalle.GetValue("tbcmin"), 0.00)) + " Lts. -" + _CodCaneroUcg.ToString + "-" + tbCliente.Text.Trim   'obobs
        Dim total = tbTotalDo.Text 'para obtener debe haber

        Dim cuenta As String
        Dim debebs, haberbs, debeus, haberus As Double
        dt1 = ObtenerNumCuenta("Institucion", _CodInstitucion) 'obcuenta=ncuenta obtener cuenta de institucion
        ' dt2 = ObtenerNumCuentaSurtidor("TY0031", cbSurtidor.Value) ' ObtenerNumCuentaSurtidor()
        L_fnGrabarTO001(4, Convert.ToInt32(codigoVenta))

        For a As Integer = 8 To 8 Step 1
            dt = CargarConfiguracion("configuracion", a) 'oblin=orden
            dtDetalle = L_fnDetalleVenta1(codigoVenta)

            Dim oblin As Integer = 1
            Dim totalCosto As Double = 0.00
            For Each row In dt.Rows
                '    Select Case row("cuenta")

                If row("cuenta") = "-1" Then
                    For Each detalle In dtDetalle.Rows
                        cuenta = detalle("yfclot")
                        If row("dh") = 1 Then
                            debeus = (Convert.ToDouble(detalle("tbpcos")) * Convert.ToDouble(row("porcentaje"))) / 100
                            debebs = debeus * 6.96
                            haberus = 0.00
                            haberbs = 0.00
                            totalCosto = totalCosto + Convert.ToDouble(detalle("tbpcos"))
                        Else
                            haberus = (Convert.ToDouble(detalle("tbpcos")) * Convert.ToDouble(row("porcentaje"))) / 100
                            haberbs = haberus * 6.96
                            debeus = 0.00
                            debebs = 0.00
                            totalCosto = totalCosto + Convert.ToDouble(detalle("tbpcos"))
                        End If

                        Dim resTO00112 As Boolean = L_fnGrabarTO001(2, Convert.ToInt32(codigoVenta), codCont, oblin, cuenta, codCanero, debebs, haberbs, debeus, haberus)
                        oblin = oblin + 1
                    Next

                    If row("cuenta") = "-1" Then
                        Continue For
                    End If

                End If
                If row("cuenta") = "-2" Then
                    cuenta = dt1.Rows(0).Item(7)

                Else
                    cuenta = row("cuenta")
                End If
                If row("dh") = 1 Then
                    debeus = (IIf(row("tipo") = 8, Convert.ToDouble(total), totalCosto) * Convert.ToDouble(row("porcentaje"))) / 100
                    debebs = debeus * 6.96
                    haberus = 0.00
                    haberbs = 0.00
                Else
                    haberus = (IIf(row("tipo") = 8, Convert.ToDouble(total), totalCosto) * Convert.ToDouble(row("porcentaje"))) / 100
                    haberbs = haberus * 6.96
                    debeus = 0.00
                    debebs = 0.00
                End If
                Dim resTO0011 As Boolean = L_fnGrabarTO001(2, Convert.ToInt32(codigoVenta), codCont, oblin, cuenta, codCanero, debebs, haberbs, debeus, haberus)
                oblin = oblin + 1
            Next
        Next

        'L_Actualiza_Venta_Contabiliza(codigoVenta, resTO001)
    End Sub


    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Try
            If (L_fnVerificarCObros(tbCodigo.Text, cbSucursal.Value)) Then

                Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)
                ToastNotification.Show(Me, "No se puede anular la venta con código ".ToUpper + tbCodigo.Text + ", porque tiene pagos realizados.".ToUpper,
                                                      img, 5000,
                                                      eToastGlowColor.Green,
                                                      eToastPosition.TopCenter)
                Exit Sub
            Else

                Dim ef = New Efecto
                ef.tipo = 2
                ef.Context = "¿esta seguro de ANULAR el registro?".ToUpper
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
                End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click

        If grVentas.GetValue("taest") <> 1 Then
            MessageBox.Show("No se puede editar por que esta anulado")
        Else
            If (L_fnVerificarCObros(tbCodigo.Text, cbSucursal.Value)) Then

                Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)
                ToastNotification.Show(Me, "No se puede editar la venta con código ".ToUpper + tbCodigo.Text + ", porque tiene pagos realizados.".ToUpper,
                                                  img, 5000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter)
                Exit Sub

            End If
            fechaOriginal = tbFechaVenta.Text
            _prhabilitar()
            Dim dt As DataTable
            dt = L_fnListarClientesVentas(_CodCliente)
            _CodCaneroUcg = dt.Rows(0).Item(1)

            _CodEmpleado = dt.Rows(0).Item(8)
            btnNuevo.Enabled = False
            btnModificar.Enabled = False
            btnEliminar.Enabled = False
            btnGrabar.Enabled = True
        End If

    End Sub



#End Region
End Class