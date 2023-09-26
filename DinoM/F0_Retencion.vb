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

Public Class F0_Retenciones
    Dim _Inter As Integer = 0
#Region "Variables Globales"
    Dim _CodCliente As Integer = 0
    Dim Modificar As Boolean = False
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

    Dim TComb, TCont, TConv, TInsu, TSho, TOtSu, TRest, RComb, RCont, RConv, RInsu, RSho, ROtSu, ROprevConv, RRest As Double
#End Region


#Region "Metodos Privados"
    Private Sub _IniciarTodo()
        L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        MSuperTabControl.SelectedTabIndex = 0
        'Me.WindowState = FormWindowState.Maximized

        '_prValidarLote()


        'lbTipoMoneda.Visible = False

        P_prCargarVariablesIndispensables()

        _prCargarVenta()
        _prInhabiliitar()
        grVentas.Focus()
        Me.Text = "VENTAS"
        Dim blah As New Bitmap(New Bitmap(My.Resources.compra), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        _prAsignarPermisos()
        P_prCargarParametro()
        '_prValidadFactura()
        _prCargarNameLabel()
        _prCargarComboGestion(cbGestion)

        'COnfiguracion previa para Pantalla de facturacion o Nota de venta
        If gb_FacturaEmite Then
            btnModificar.Visible = True
        Else

        End If
        DescuentoXProveedorList = ObtenerDescuentoPorProveedor()
        ConfiguracionDescuentoEsXCantidad = TipoDescuentoEsXCantidad()
        'SwDescuentoProveedor.Visible = IIf(ConfiguracionDescuentoEsXCantidad, False, True)
        tbFechaVenta.Value = Date.Now
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

    Private Sub _prCargarComboGestion(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarGestiones()
        With mCombo
            .DropDownList.Columns.Clear()

            .DropDownList.Columns.Add("gestion").Width = 80
            .DropDownList.Columns("gestion").Caption = "GESTION"
            .ValueMember = "gestion"
            .DisplayMember = "gestion"
            .DataSource = dt
            .Refresh()
        End With
    End Sub
    Private Sub _prCargarQuincena(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarQuincena(cbGestion.Value)
        With mCombo
            .DropDownList.Columns.Clear()

            .DropDownList.Columns.Add("quincena").Width = 100
            .DropDownList.Columns("quincena").Caption = "QUINCENA"
            .DropDownList.Columns.Add("inicioQuin").Width = 105
            .DropDownList.Columns("inicioQuin").Caption = "INICIO"
            .DropDownList.Columns.Add("finQuin").Width = 105
            .DropDownList.Columns("finQuin").Caption = "FIN"
            .DropDownList.Columns.Add("gestion").Width = 95
            .DropDownList.Columns("gestion").Caption = "GESTION"
            .ValueMember = "quincena"
            .DisplayMember = "quincena"
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

        cbQuincena.ReadOnly = True
        cbGestion.ReadOnly = True
        TextBoxX3.ReadOnly = True
        TextBoxX8.ReadOnly = True
        TextBoxX9.ReadOnly = True
        tbFechaVenta.Enabled = False

        txtEstado.ReadOnly = True

        CheckGrupo.Enabled = False

        btnModificar.Enabled = True
        btnGrabar.Enabled = False
        btnNuevo.Enabled = True

        SwitchButton1.IsReadOnly = True
        'btnEliminar.Enabled = True

        '------------REVISAR ESTADO


        'If grVentas.GetValue("taest") = 1 Then
        '    btnEliminar.Enabled = True
        'Else
        '    btnEliminar.Enabled = False
        'End If



        'txtCambio1.IsInputReadOnly = True

        'txtMontoPagado1.IsInputReadOnly = True

        grVentas.Enabled = True
        PanelNavegacion.Enabled = True
        'grdetalle.RootTable.Columns("img").Visible = False



        TbNombre2.ReadOnly = True
        tbCanero.ReadOnly = True
        tbCodCanero.ReadOnly = True
        FilaSelectLote = Nothing
    End Sub

    Private Sub _prhabilitarModificar()
        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnGrabar.Enabled = True
        SwitchButton1.IsReadOnly = False
    End Sub
    Private Sub _prhabilitar()

        cbQuincena.ReadOnly = False
        cbGestion.ReadOnly = False
        TextBoxX3.ReadOnly = False

        tbFechaVenta.Enabled = True

        txtEstado.ReadOnly = True

        'CheckGrupo.Enabled = True

        btnModificar.Enabled = False
        btnGrabar.Enabled = True
        btnNuevo.Enabled = False
        'grVentas.Enabled = False
        'tbCodigo.ReadOnly = False
        CheckGrupo.Checked = False
        'tbFechaVenta.IsInputReadOnly = False
        'tbFechaVenta.Enabled = True

        'btnGrabar.Enabled = False
        'If gs_user = "ALMACEN" Then
        '    btnBitacora.Enabled = False
        'Else
        '    btnBitacora.Enabled = True
        'End If


        SwitchButton1.IsReadOnly = False
        TbNombre2.ReadOnly = False

        'dtDescuentos = L_fnListarDescuentosTodos()
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

        cbQuincena.Clear()
        cbGestion.Clear()
        TextBoxX3.Clear()

        tbCodCanero.Clear()
        tbCanero.Clear()
        txtEstado.ReadOnly = True

        tbcodInst.Clear()
        tbInstitucion.Clear()
        idInstitucion.Clear()

        Dim dt As DataTable = cargarDeudasporCanero(-1, -1, -1)
        dt.Clear()
        grdetalle.DataSource = dt
        dt = cargarGrupoCanero(-1)
        dt.Clear()
        grGrupoEco.DataSource = dt
        dt = L_fnListarCanerosxInst2(-1)
        dt.Clear()
        grCanero.DataSource = dt


        tbId.Clear()

        txtEstado.Clear()
        TextBoxX4.Clear()
        TextBoxX5.Clear()
        TextBoxX6.Clear()
        TextBoxX7.Clear()
        TextBoxX8.Clear()
        TextBoxX9.Clear()

        tbTConv.Clear()
        tbTCont.Clear()
        tbTRest.Clear()
        tbTComb.Clear()
        tbTInsu.Clear()
        tbTSho.Clear()
        tbTOtSu.Clear()

        tbRConv.Clear()
        tbrProvConv.Clear()
        tbRCont.Clear()
        tbRRest.Clear()
        tbRComb.Clear()
        tbRInsu.Clear()
        tbRSho.Clear()
        tbROtSu.Clear()
        tbTotalD.Clear()
        tbTotalR.Clear()
        'tbCodigo.Clear()
        'tbInstitucion.Clear()

        '_CodCliente = 0
        '_CodEmpleado = 0
        'tbFechaVenta.Value = Now.Date

        '_prCargarDetalleVenta(-1)
        'MSuperTabControl.SelectedTabIndex = 0


        'txtEstado.BackColor = Color.White
        'txtEstado.Clear()

        'With grdetalle.RootTable.Columns("img")
        '    .Width = 80
        '    .Caption = "Eliminar"
        '    .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
        '    .Visible = True
        'End With
        '_prAddDetalleVenta()

        TbNombre2.Clear()

        FilaSelectLote = Nothing

    End Sub
    Public Sub _prMostrarRegistro(_N As Integer)

        With grVentas

            If .GetValue("trRetCob") = 1 Then
                SwitchButton1.Value = True
            Else
                SwitchButton1.Value = False
            End If
            tbId.Text = .GetValue("trid").ToString
            cbQuincena.Text = .GetValue("trquin").ToString
            cbGestion.Text = .GetValue("trges").ToString
            TextBoxX3.Text = .GetValue("trfac").ToString
            TextBoxX4.Text = .GetValue("trcupo").ToString
            TextBoxX5.Text = .GetValue("trTIng").ToString
            TextBoxX6.Text = .GetValue("trpor").ToString
            TextBoxX7.Text = .GetValue("trIngQuin").ToString
            TextBoxX8.Text = .GetValue("trRetBs").ToString
            TextBoxX9.Text = .GetValue("trRetSus").ToString
            tbFechaVenta.Text = CType(.GetValue("trfecci"), Date).ToString("dd/MM/yyyy")
            txtEstado.Text = .GetValue("trid").ToString
            If .GetValue("trTCan") = 1 Then
                CheckGrupo.CheckValue = True
            Else
                CheckGrupo.CheckValue = False
            End If

            tbTComb.Text = .GetValue("trTComb").ToString
            tbTConv.Text = .GetValue("trTConv").ToString
            tbTCont.Text = .GetValue("trTCont").ToString
            tbTSho.Text = .GetValue("trTSho").ToString
            tbTInsu.Text = .GetValue("trTInsu").ToString
            tbTOtSu.Text = .GetValue("trTOtSu").ToString
            tbTRest.Text = .GetValue("trTRest").ToString
            tbRComb.Text = .GetValue("trRComb").ToString
            tbRConv.Text = .GetValue("trRConv").ToString
            tbrProvConv.Text = .GetValue("trRProvConv").ToString
            tbRCont.Text = .GetValue("trRCont").ToString
            tbRSho.Text = .GetValue("trRSho").ToString
            tbRInsu.Text = .GetValue("trRInsu").ToString
            tbROtSu.Text = .GetValue("trROtSu").ToString
            tbRRest.Text = .GetValue("trRRest").ToString
            tbTotalD.Text = .GetValue("trTotalD").ToString
            tbTotalR.Text = .GetValue("trTotalR").ToString
            tbcodInst.Text = .GetValue("codInst").ToString
            tbInstitucion.Text = .GetValue("nomInst").ToString
            tbCodCanero.Text = .GetValue("ydcod").ToString
            tbCanero.Text = .GetValue("ydrazonsocial").ToString
            idInstitucion.Text = .GetValue("trins").ToString
            _CodCliente = .GetValue("trcan").ToString
            _prCargarListaCanerosxInstitucion(idInstitucion.Text)
            _prCargarGrupoEco(_CodCliente)

        End With

        _prCargarDetalleVenta2(txtEstado.Text)
        'tbMdesc.Value = grVentas.GetValue("tadesc")
        '_prCalcularPrecioTotal()
        'Calcular montos
        'Dim tMonto As DataTable = L_fnMostrarMontos(tbCodigo.Text)

        LblPaginacion.Text = Str(grVentas.Row + 1) + "/" + grVentas.RowCount.ToString
        'btnContabilizar.Visible = True
    End Sub

    Private Sub _prCargarDetalleVenta(_numi As String)
        Dim dt As New DataTable

        If CheckGrupo.Checked = True Then
            If SwitchButton1.Value = True Then
                dt = cargarDeudasporGrupo(cbGestion.Value, tbFechaVenta.Value.ToString("dd/MM/yyyy"), CType(grGrupoEco.DataSource, DataTable))
            Else
                dt = cargarDeudasporGrupoCobranza(tbFechaVenta.Value.ToString("dd/MM/yyyy"), CType(grGrupoEco.DataSource, DataTable))
            End If
        Else
            If SwitchButton1.Value = True Then
                dt = cargarDeudasporCanero(_numi, cbGestion.Value, tbFechaVenta.Value.ToString("dd/MM/yyyy"))
            Else
                dt = cargaCobranzaporCanero(_numi, tbFechaVenta.Value.ToString("dd/MM/yyyy"))
            End If
        End If

        grdetalle.DataSource = dt
        grdetalle.RetrieveStructure()
        grdetalle.AlternatingColors = True
        ' a.tbnumi ,a.tbtv1numi ,a.tbty5prod ,b.yfcdprod1 as producto,a.tbest ,a.tbcmin ,a.tbumin ,Umin .ycdes3 as unidad,a.tbpbas ,a.tbptot,a.tbdesc ,a.tbobs ,
        'a.tbfact ,a.tbhact ,a.tbuact

        With grdetalle.RootTable.Columns("fecha")
            .Width = 100
            .Caption = "Fecha"
            .Visible = True
        End With

        With grdetalle.RootTable.Columns("codcan")
            .Width = 90
            .Visible = True
            .Caption = "Cod. Can."
        End With
        With grdetalle.RootTable.Columns("ydnumi")
            .Width = 90
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("ydrazonsocial")
            .Caption = "Código".ToUpper
            .Width = 100
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("doc")
            .Caption = "Doc.".ToUpper
            .Width = 40
            '.Visible = gb_CodigoBarra
            .Visible = True
        End With
        With grdetalle.RootTable.Columns("taalm")
            .Caption = "Descripción de Artículo".ToUpper
            .Width = 440
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tipo")
            .Caption = "Tipo Deuda"
            .Width = 180
            .Visible = True
        End With

        With grdetalle.RootTable.Columns("tipod")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("capital")
            .Width = 110
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Capital"
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With
        With grdetalle.RootTable.Columns("capital1")
            .Width = 110
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00"
            .Caption = "Capital1"
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With
        With grdetalle.RootTable.Columns("aporte")
            .Width = 110
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Aporte"
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With
        With grdetalle.RootTable.Columns("aporte1")
            .Width = 110
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00"
            .Caption = "Aporte1"
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With
        With grdetalle.RootTable.Columns("deuda")
            .Width = 90
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Deuda"
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With
        With grdetalle.RootTable.Columns("cobrar")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Cobrar"
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With
        With grdetalle.RootTable.Columns("convenio")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Convenio"
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With
        With grdetalle.RootTable.Columns("amortizacion")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Amortizacion"
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"
        End With
        With grdetalle.RootTable.Columns("saldo")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00"
            .Caption = "Saldo"
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"

        End With
        With grdetalle.RootTable.Columns("saldo1")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00"
            .Caption = "Saldo"
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"

        End With
        With grdetalle.RootTable.Columns("descApor")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Desc.Aporte"
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"

        End With

        With grdetalle.RootTable.Columns("aporteDiesel")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Aporte Diesel Propio"
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"

        End With

        With grdetalle.RootTable.Columns("aporteDiesel1")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00"
            .Caption = "Aporte Diesel Propio1"
            .AggregateFunction = AggregateFunction.Sum
            .TotalFormatString = "0.00"

        End With
        With grdetalle
            '.DefaultFilterRowComparison = FilterConditionOperator.Contains
            '.FilterMode = FilterMode.Automatic
            '.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007

            .RowHeaders = InheritableBoolean.True
            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed

        End With
    End Sub

    Private Sub _prCargarDetalleVenta2(_numi As String)
        Dim dt As New DataTable

        dt = L_fnDetalleRetencion(_numi)


        grdetalle.DataSource = dt
        grdetalle.RetrieveStructure()
        grdetalle.AlternatingColors = True
        ' a.tbnumi ,a.tbtv1numi ,a.tbty5prod ,b.yfcdprod1 as producto,a.tbest ,a.tbcmin ,a.tbumin ,Umin .ycdes3 as unidad,a.tbpbas ,a.tbptot,a.tbdesc ,a.tbobs ,
        'a.tbfact ,a.tbhact ,a.tbuact
        With grdetalle.RootTable.Columns("trnumi")
            .Width = 100
            .Caption = "Fecha"
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("trid")
            .Width = 100
            .Caption = "Fecha"
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("fecha")
            .Width = 100
            .Caption = "Fecha"
            .Visible = True
        End With

        With grdetalle.RootTable.Columns("ydcod")
            .Width = 90
            .Visible = True
            .Caption = "Cod. Can."
        End With
        With grdetalle.RootTable.Columns("ydnumi")
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

        With grdetalle.RootTable.Columns("yddesc")
            .Caption = "Código".ToUpper
            .Width = 100
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("doc")
            .Caption = "Doc.".ToUpper
            .Width = 40
            '.Visible = gb_CodigoBarra
            .Visible = True
        End With
        With grdetalle.RootTable.Columns("taalm")
            .Caption = "Descripción de Artículo".ToUpper
            .Width = 440
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("tipo")
            .Caption = "Tipo Deuda"
            .Width = 180
            .Visible = True
        End With

        With grdetalle.RootTable.Columns("tipod")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        With grdetalle.RootTable.Columns("capital")
            .Width = 110
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Capital"
            .AggregateFunction = AggregateFunction.Sum
        End With
        With grdetalle.RootTable.Columns("capital1")
            .Width = 110
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00"
            .Caption = "Capital"
            .AggregateFunction = AggregateFunction.Sum
        End With
        With grdetalle.RootTable.Columns("aporte")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Aporte"
            .AggregateFunction = AggregateFunction.Sum
        End With
        With grdetalle.RootTable.Columns("aporte1")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00"
            .Caption = "Aporte"
            .AggregateFunction = AggregateFunction.Sum
        End With
        With grdetalle.RootTable.Columns("aporteDiesel")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Aporte Diesel"
            .AggregateFunction = AggregateFunction.Sum
        End With
        With grdetalle.RootTable.Columns("aporteDiesel1")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00"
            .Caption = "Aporte Diesel"
            .AggregateFunction = AggregateFunction.Sum
        End With
        With grdetalle.RootTable.Columns("deuda")
            .Width = 90
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Deuda"
            .AggregateFunction = AggregateFunction.Sum
        End With
        With grdetalle.RootTable.Columns("amortizacion")
            .Width = 90
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Amortizacion"
            .AggregateFunction = AggregateFunction.Sum
        End With
        With grdetalle.RootTable.Columns("descAporte")
            .Width = 90
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Desc.Aport"
            .AggregateFunction = AggregateFunction.Sum
        End With
        With grdetalle.RootTable.Columns("cobrar")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Cobrar"
            .AggregateFunction = AggregateFunction.Sum
        End With
        With grdetalle.RootTable.Columns("saldo")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .FormatString = "0.00"
            .Caption = "Saldo"
            .AggregateFunction = AggregateFunction.Sum

        End With
        With grdetalle.RootTable.Columns("convenio")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Convenio"
            .AggregateFunction = AggregateFunction.Sum

        End With
        With grdetalle.RootTable.Columns("saldo1")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .FormatString = "0.00"
            .Caption = "Saldo"
            .AggregateFunction = AggregateFunction.Sum

        End With

        With grdetalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
            If Modificar = True Then
                .RowHeaders = InheritableBoolean.True
                .TotalRow = InheritableBoolean.True
                .TotalRowFormatStyle.BackColor = Color.Gold
                .TotalRowPosition = TotalRowPosition.BottomFixed
            Else
                .RowHeaders = InheritableBoolean.False
                .TotalRow = InheritableBoolean.False
            End If
        End With
    End Sub
    Private Sub _prCargarGrupoEco(_numi As String)
        Dim dt As New DataTable


        dt = cargarGrupoCanero(_numi)

        grGrupoEco.DataSource = dt
        grGrupoEco.RetrieveStructure()
        grGrupoEco.AlternatingColors = True
        ' a.tbnumi ,a.tbtv1numi ,a.tbty5prod ,b.yfcdprod1 as producto,a.tbest ,a.tbcmin ,a.tbumin ,Umin .ycdes3 as unidad,a.tbpbas ,a.tbptot,a.tbdesc ,a.tbobs ,
        'a.tbfact ,a.tbhact ,a.tbuact

        With grGrupoEco.RootTable.Columns("ydnumi")
            .Width = 100
            .Caption = "Fecha"
            .Visible = False
        End With

        With grGrupoEco.RootTable.Columns("ydcod")
            .Width = 90
            .Visible = True
            .Caption = "Cod. Can."
        End With

        With grGrupoEco.RootTable.Columns("ydrazonsocial")
            .Caption = "Cañero".ToUpper
            .Width = 200
            .Visible = True
        End With

        With grGrupoEco.RootTable.Columns("estado")
            .Caption = "Doc.".ToUpper
            .Width = 40
            '.Visible = gb_CodigoBarra
            .Visible = False
        End With
        With grGrupoEco.RootTable.Columns("TOTALCUPO")
            .Width = 100
            .Visible = False
            .Caption = "AVANCE"
        End With
        With grGrupoEco.RootTable.Columns("CANAINGRESADA")
            .Width = 90
            .Visible = True
            .Caption = "CAÑA INGRESADA"
            .FormatString = "0.00"
        End With
        With grGrupoEco.RootTable.Columns("AVANCE")
            .Width = 90
            .Visible = True
            .Caption = "AVANCE"
            .FormatString = "0.00"
        End With


        With grGrupoEco
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With
    End Sub

    Private Sub _prCargarVenta()
        Dim dt As New DataTable

        dt = L_fnGeneralRetencion()

        grVentas.DataSource = dt
        grVentas.RetrieveStructure()
        grVentas.AlternatingColors = True
        '   a.tamon ,IIF(tamon=1,'Boliviano','Dolar') as moneda,a.taest ,a.taobs ,
        'a.tadesc ,a.tafact ,a.tahact ,a.tauact,(Sum(b.tbptot)-a.tadesc ) as total

        With grVentas.RootTable.Columns("trid")
            .Width = 100
            .Caption = "Codigo"
            .Visible = True

        End With

        With grVentas.RootTable.Columns("trfecci")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False

        End With
        With grVentas.RootTable.Columns("trquin")
            .Width = 90
            .Visible = True
            .Caption = "Quincena"
        End With

        With grVentas.RootTable.Columns("trges")
            .Width = 90
            .Visible = False
        End With
        With grVentas.RootTable.Columns("trins")
            .Width = 90
            .Visible = False
            .Caption = "FECHA"
        End With

        With grVentas.RootTable.Columns("codInst")
            .Width = 90
            .Visible = True
            .Caption = "Cod. Inst."
        End With
        With grVentas.RootTable.Columns("trRetCob")
            .Width = 120
            .Visible = False
            .Caption = "Tipo"
        End With
        With grVentas.RootTable.Columns("tipo")
            .Width = 120
            .Visible = True
            .Caption = "Tipo"
        End With
        With grVentas.RootTable.Columns("nomInst")
            .Width = 160
            .Visible = True
            .Caption = "Institucion"
        End With
        With grVentas.RootTable.Columns("trcan")
            .Width = 100
            '.CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
            .Caption = "Cañero"
        End With
        With grVentas.RootTable.Columns("ydcod")
            .Width = 100
            '.CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "Cod. Cañero"
        End With
        With grVentas.RootTable.Columns("ydrazonsocial")
            .Width = 200
            '.CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "Cañero"
        End With
        With grVentas.RootTable.Columns("trfac")
            .Width = 250
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
            .Caption = "CLIENTE"
        End With
        With grVentas.RootTable.Columns("trcupo")
            .Width = 250
            .Visible = False
            .Caption = "INSTITUCION".ToUpper
        End With


        With grVentas.RootTable.Columns("trTIng")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        With grVentas.RootTable.Columns("trpor")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("trIngQuin")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With


        With grVentas.RootTable.Columns("trRetBs")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("trRetSus")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
            .Caption = "MONEDA"
        End With
        With grVentas.RootTable.Columns("trTComb")
            .Width = 200
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
            .Caption = "OBSERVACION"
        End With
        With grVentas.RootTable.Columns("trRComb")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("trTInsu")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("trRInsu")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("trTRest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("trRRest")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("trTCont")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("trRCont")
            .Width = 100
            .Caption = "COD. Institucion"
            .Visible = False
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With
        With grVentas.RootTable.Columns("trTSho")
            .Width = 100
            .Caption = "COD. Institucion"
            .Visible = False
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With
        With grVentas.RootTable.Columns("trRSho")
            .Width = 150
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .Caption = "TOTAL"
            .FormatString = "0.00"
        End With
        With grVentas.RootTable.Columns("trTOtSu")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("trROtSu")
            .Width = 100
            .Caption = "COD. Institucion"
            .Visible = False
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With
        With grVentas.RootTable.Columns("trTConv")
            .Width = 100
            .Caption = "COD. Institucion"
            .Visible = False
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With
        With grVentas.RootTable.Columns("trRConv")
            .Width = 150
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .Caption = "TOTAL"
            .FormatString = "0.00"
        End With
        With grVentas.RootTable.Columns("trTotalD")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("trTotalR")
            .Width = 100
            .Caption = "COD. Institucion"
            .Visible = False
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With
        With grVentas.RootTable.Columns("tralm")
            .Width = 100
            .Caption = "COD. Institucion"
            .Visible = False
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With
        With grVentas.RootTable.Columns("trvend")
            .Width = 150
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
            .Caption = "TOTAL"
            .FormatString = "0.00"
        End With
        With grVentas.RootTable.Columns("trTCan")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("trfec")
            .Width = 100
            .Caption = "COD. Institucion"
            .Visible = False
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With
        With grVentas.RootTable.Columns("trhor")
            .Width = 100
            .Caption = "COD. Institucion"
            .Visible = False
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With
        With grVentas.RootTable.Columns("trest")
            .Width = 150
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
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
            _prCargarDetalleVenta2(-1)
        End If
    End Sub
    Public Sub _prGuardar()

        If _ValidarCampos() = False Then
            Exit Sub
        End If

        If (txtEstado.Text = "") Then

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
        'If (cbSucursal.SelectedIndex < 0) Then
        '    Return
        'End If
        'Dim nombreGrupos As DataTable = L_fnNameLabel()
        'Dim dt As New DataTable

        'If (G_Lote = True) Then
        '    dt = L_fnListarProductos(cbSucursal.Value, _cliente)
        'Else
        '    dt = L_fnListarProductosSinLote(cbSucursal.Value, _cliente, CType(grdetalle.DataSource, DataTable))
        'End If

        'grProductos.DataSource = dt
        'grProductos.RetrieveStructure()
        'grProductos.AlternatingColors = True

        'If gb_TipoAyuda = ENProductoAyuda.SUPERMERCADO Then
        '    ArmarGrillaProducto(nombreGrupos, False)
        'ElseIf gb_TipoAyuda = ENProductoAyuda.FARMACIA Then
        '    ArmarGrillaProducto(nombreGrupos, True)
        'End If
        '_prAplicarCondiccionJanusSinLote()
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
    Public Function _fnAccesible() As Boolean
        'Return tbFechaVenta.IsInputReadOnly = False
        If btnNuevo.Enabled = False Or Modificar = True Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Sub _HabilitarProductos()

        PanelInferior.Visible = False
        _prCargarProductos(Str(_CodCliente))

    End Sub
    Private Sub _HabilitarFocoDetalle(fila As Integer)
        _prCargarProductos(Str(_CodCliente))
        grdetalle.Focus()
        grdetalle.Row = fila
        grdetalle.Col = 2
    End Sub
    Private Sub _DesHabilitarProductos()

        PanelInferior.Visible = True


        grdetalle.Select()
        grdetalle.Col = 5
        grdetalle.Row = grdetalle.RowCount - 1

    End Sub
    Public Sub _fnObtenerFilaDetalle(ByRef pos As Integer, numi As Integer)
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _numi As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("tbnumi")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next

    End Sub

    Public Sub _fnObtenerFilaDetalleProducto(ByRef pos As Integer, numi As Integer)
        'For i As Integer = 0 To CType(grProductos.DataSource, DataTable).Rows.Count - 1 Step 1
        '    Dim _numi As Integer = CType(grProductos.DataSource, DataTable).Rows(i).Item("yfnumi")
        '    If (_numi = numi) Then
        '        pos = i
        '        Return
        '    End If
        'Next

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

        CalcularDescuentosTotal()

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

            If (_CodInstitucion <= 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor Seleccione una Institucion con Ctrl+Enter".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                tbInstitucion.Focus()
                Return False

            End If
            If (_CodCliente <= 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor Seleccione un Cañero".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

                Return False

            End If
            If SwitchButton1.Value = True Then
                If (TextBoxX3.Text = String.Empty) Then
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "Por Favor Ingrese un factor".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    TextBoxX3.Focus()
                    Return False
                End If
            End If

            'If (grdetalle.GetTotal(grdetalle.RootTable.Columns("cobrar"), AggregateFunction.Sum) <= 0 Or grdetalle.GetTotal(grdetalle.RootTable.Columns("descAporte"), AggregateFunction.Sum) <= 0) Then
            '    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            '    ToastNotification.Show(Me, "Por Favor Ingrese un monto a cobrar".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            '    Return False

            'End If

            If (grdetalle.GetTotal(grdetalle.RootTable.Columns("cobrar"), AggregateFunction.Sum) <= 0) Then
                If (grdetalle.GetTotal(grdetalle.RootTable.Columns("convenio"), AggregateFunction.Sum) <= 0) Then
                    If (grdetalle.GetTotal(grdetalle.RootTable.Columns("amortizacion"), AggregateFunction.Sum) <= 0) Then
                        If (grdetalle.GetTotal(grdetalle.RootTable.Columns("descApor"), AggregateFunction.Sum) <= 0) Then
                            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                            ToastNotification.Show(Me, "Por Favor Ingrese un monto a cobrar".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

                            Return False
                        End If
                    End If
                End If


            End If
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
        detalle.DefaultView.RowFilter = "estado >= '" + 0.ToString() + "'"
        detalle = detalle.DefaultView.ToTable
        For Each fila As DataRow In detalle.Rows
            Dim idProducto = fila.Item("tbty5prod")
            If aux <> idProducto Then
                '  dtSaldos = L_fnObteniendoSaldosTI001(fila.Item("tbty5prod"), cbSucursal.Value)
                Dim inventario = dtSaldos.Compute("SUM(iccven)", String.Empty)

                detalle.DefaultView.RowFilter = "tbty5prod = '" + fila.Item("tbty5prod").ToString() + "'"
                Dim productoRepeditos = detalle.DefaultView.ToTable
                Dim saldo = productoRepeditos.Compute("SUM(tbcmin)", String.Empty)
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
            Return False
        End If
        Return True
    End Function

    Public Sub _GuardarNuevo()
        Try

            Dim tipo As String
            If SwitchButton1.Value = False Then
                tipo = "Cobranza"
            Else
                tipo = "Retencion"
            End If
            Dim ef = New Efecto


            ef.tipo = 2
            ef.Context = "MENSAJE PRINCIPAL".ToUpper
            ef.Header = "¿Desea guardar el registro como una " + tipo + "?".ToUpper
            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            Dim res As Boolean
            If (bandera = True) Then
                'Dim dtDetalle As DataTable = rearmarDetalle()
                If SwitchButton1.Value = True Then
                    res = L_fnGrabarRetencion(tbFechaVenta.Value.ToString("dd-MM-yyyy"), CDbl(cbQuincena.Value), CDbl(cbGestion.Value), CInt(idInstitucion.Text), _CodCliente,
                                                     CDbl(TextBoxX3.Text), CDbl(TextBoxX4.Text), CDbl(TextBoxX5.Text), CDbl(TextBoxX6.Text), CDbl(TextBoxX7.Text), CDbl(TextBoxX8.Text),
                                                     CDbl(TextBoxX9.Text), TComb, RComb, TInsu, RInsu, TRest, RRest, TCont, RCont, TSho, RSho, TOtSu, ROtSu, TConv, RConv, ROprevConv, CDbl(tbTotalD.Text),
                                                     CDbl(tbTotalR.Text), gi_userSuc, gi_userNumi, IIf(CheckGrupo.Checked = False, 0, 1), CType(grdetalle.DataSource, DataTable), 1)
                Else
                    res = L_fnGrabarRetencion(tbFechaVenta.Value.ToString("dd-MM-yyyy"), 0, 0, CInt(idInstitucion.Text), _CodCliente,
                                                     0, 0, 0, 0, 0, 0, 0, TComb, RComb, TInsu, RInsu, TRest, RRest, TCont, RCont, TSho, RSho, TOtSu, ROtSu, TConv, RConv, ROprevConv, CDbl(tbTotalD.Text),
                                                     CDbl(tbTotalR.Text), gi_userSuc, gi_userNumi, 0, CType(grdetalle.DataSource, DataTable), 0)
                End If

            Else
                res = False
            End If
            If res Then
                'res = P_fnGrabarFacturarTFV001(numi)
                'Emite factura

                '_Limpiar()
                _prCargarVenta()


                _prSalir()


                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                ToastNotification.Show(Me, "Código de " + tipo.ToUpper + tbCodigo.Text + " Grabado con Exito.".ToUpper,
                                                  img, 2000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter
                                                  )



                Table_Producto = Nothing

            Else
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, "La " + tipo + " no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

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

    Private Sub _prGuardarModificado()
        L_fnGuardarModificado(CInt(txtEstado.Text), CType(grdetalle.DataSource, DataTable))
        Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
        ToastNotification.Show(Me, "COdigo de retencion " + txtEstado.Text + " modificado con exito".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.TopCenter)
        _prInhabiliitar()
        _Limpiar()
        _prCargarVenta()

    End Sub
    Private Sub _prSalir()
        If btnGrabar.Enabled = True Then
            _prInhabiliitar()
            Modificar = False
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

    Private Sub P_prCargarParametro()
        'El sistema factura?

        'Si factura, preguntar si, Se incluye el Importe ICE / IEHD / TASAS?


    End Sub
    Private Sub P_GenerarReporte(numi As String)
        Dim dt As DataTable = L_fnRetencion(tbId.Text)


        Dim objrep As New R_LiquidacionXcobrar
        SetParametrosNotaVenta(dt, objrep)
    End Sub

    Private Sub SetParametrosNotaVenta(dt As DataTable, objrep As Object)
        objrep.SetDataSource(dt)

        objrep.SetParameterValue("usuario", gs_user)
        P_Global.Visualizador = New Visualizador
        P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
        P_Global.Visualizador.ShowDialog() 'Comentar
        P_Global.Visualizador.BringToFront() 'Comentar

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

    Private Sub AsignarClienteEmpleado()
        Try
            Dim _tabla11 As DataTable = L_fnListarClientesUsuario(gi_userNumi)
            If _tabla11.Rows.Count > 0 Then
                tbInstitucion.Text = _tabla11.Rows(0).Item("yddesc")
                _CodCliente = _tabla11.Rows(0).Item("ydnumi") 'Codigo
                _CodEmpleado = _tabla11.Rows(0).Item("ydnumivend") 'Codigo
            Else
                Dim dt As DataTable
                dt = L_fnListarClientes()
                If dt.Rows.Count > 0 Then
                    Dim fila As DataRow() = dt.Select("ydnumi =MIN(ydnumi)")
                    tbInstitucion.Text = fila(0).ItemArray(3)
                    _CodCliente = fila(0).ItemArray(0)
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
        lbNroCaja.Text = gs_user
        LabelAlmacen.Text = gi_userSuc
        LabelAlmacen.Text = gs_userSucNom

        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
        btnGrabar.Enabled = True
        PanelNavegacion.Enabled = False
        tbInstitucion.Select()
        SwitchButton1.Value = False
        cbQuincena.Enabled = False
        cbGestion.Enabled = False
        tbFechaVenta.Enabled = True
        TextBoxX3.Enabled = False
        'CheckGrupo.Enabled = False

    End Sub
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _prSalir()

    End Sub

    Public _MListEstBuscador As List(Of Modelo.Celda)
    Private Sub tbCliente_KeyDown(sender As Object, e As KeyEventArgs)

        If (_fnAccesible()) Then
            If e.KeyData = Keys.Control + Keys.Enter Then
                Dim dt As DataTable
                dt = L_fnListarInstitucion()

                Dim listEstCeldas As New List(Of Modelo.Celda)
                listEstCeldas.Add(New Modelo.Celda("id,", False, "ID", 50))
                listEstCeldas.Add(New Modelo.Celda("CodInst", True, "CODIGO", 50))
                listEstCeldas.Add(New Modelo.Celda("nomInst", True, "NOMBRE", 280))
                listEstCeldas.Add(New Modelo.Celda("direc", True, "DIRECCION".ToUpper, 150))
                listEstCeldas.Add(New Modelo.Celda("telf", True, "TELEFONO", 220))
                listEstCeldas.Add(New Modelo.Celda("campo1", False, "campo1".ToUpper, 200))
                listEstCeldas.Add(New Modelo.Celda("campo2", False, "F.campo2".ToUpper, 150))
                listEstCeldas.Add(New Modelo.Celda("campo3", False, "campo3".ToUpper, 200))
                listEstCeldas.Add(New Modelo.Celda("fact", False, "F.fact".ToUpper, 150))
                listEstCeldas.Add(New Modelo.Celda("hact", False, "hact".ToUpper, 200))
                listEstCeldas.Add(New Modelo.Celda("uact", False, "F.uact".ToUpper, 150))
                Dim ef = New Efecto
                ef.tipo = 3
                ef.dt = dt
                ef.SeleclCol = 1
                ef.listEstCeldas = listEstCeldas
                ef.alto = 50
                ef.ancho = 350
                ef.Context = "Seleccione Institucion".ToUpper
                ef.ShowDialog()
                Dim bandera As Boolean = False
                bandera = ef.band
                If (bandera = True) Then
                    Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                    If (IsNothing(Row)) Then
                        ' tbInsCan.Focus()
                        Return
                    End If
                    tbcodInst.Text = Row.Cells("CodInst").Value
                    tbInstitucion.Text = Row.Cells("nomInst").Value
                    idInstitucion.Text = Row.Cells("id").Value
                    _CodInstitucion = Row.Cells("id").Value
                    _prCargarListaCanerosxInstitucion(idInstitucion.Text)
                End If

            End If

        End If
    End Sub
    Private Sub _prCargarListaCanerosxInstitucion(_numi As String)
        Dim dt As New DataTable
        dt = L_fnListarCanerosxInst2(CInt(_numi))
        grCanero.DataSource = dt
        grCanero.RetrieveStructure()
        grCanero.AlternatingColors = True


        With grCanero.RootTable.Columns("ydnumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False
        End With

        With grCanero.RootTable.Columns("ydcod")
            .Width = 65
            .Visible = True
            .Caption = "CODIGO"
        End With
        With grCanero.RootTable.Columns("ydrazonsocial")
            .Width = 280
            .Visible = True
            .Caption = "CANERO"
        End With
        With grCanero.RootTable.Columns("ydtelf1")
            .Width = 90
            .Visible = False
        End With
        With grCanero.RootTable.Columns("canaingresada")
            .Width = 90
            .Visible = True
            .Caption = "CAÑA INGRESADA"
            .FormatString = "0.00"
        End With
        With grCanero.RootTable.Columns("AVANCE")
            .Width = 90
            .Visible = True
            .Caption = "AVANCE"
            .FormatString = "0.00"
        End With
        With grCanero.RootTable.Columns("totalcupo")
            .Width = 100
            .Visible = False
            .Caption = "AVANCE"
        End With

        With grCanero
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2003



        End With
    End Sub

    Private Sub tbVendedor_KeyDown(sender As Object, e As KeyEventArgs)

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

                        Return

                    End If
                    _CodEmpleado = Row.Cells("id").Value
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
            If gs_user = "SERVICIOS" Then
                If (e.Column.Index = grdetalle.RootTable.Columns("tbpbas").Index Or
               e.Column.Index = grdetalle.RootTable.Columns("tbcmin").Index) Then

                    e.Cancel = False

                Else
                    e.Cancel = True
                End If
            Else
                If (e.Column.Index = grdetalle.RootTable.Columns("cobrar").Index Or e.Column.Index = grdetalle.RootTable.Columns("descApor").Index) Then

                    e.Cancel = False
                    'ActualizarTotales()

                Else
                    e.Cancel = True
                    If SwitchButton1.Value = True Then
                        If e.Column.Index = grdetalle.RootTable.Columns("convenio").Index And SwitchButton1.Value = True And grdetalle.GetValue("taalm") = 10016 Then
                            e.Cancel = False
                        Else
                            e.Cancel = True

                        End If
                    Else
                        If e.Column.Index = grdetalle.RootTable.Columns("amortizacion").Index Then
                            e.Cancel = False
                        Else
                            e.Cancel = True

                        End If
                    End If


                End If



            End If


        Else
            e.Cancel = True
        End If

    End Sub

    Private Sub AjustarGrid()
        For pos As Integer = 0 To grdetalle.RowCount - 1 Step 1
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporteDiesel") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporteDiesel") + CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporteDiesel1")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporteDiesel1") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporteDiesel")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte") + CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte1")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte1") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("capital") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("capital") + CType(grdetalle.DataSource, DataTable).Rows(pos).Item("capital1")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("capital1") = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("capital")
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporteDiesel") = IIf(CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cobrar") < CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporteDiesel"), CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cobrar") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cobrar"), 0.00)
            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte") = IIf(CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cobrar") > CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporteDiesel"), IIf((CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cobrar") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporteDiesel")) > CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte"), 0.00, CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cobrar") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporteDiesel1")), CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporteDiesel")) 'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cobrar") - CType(grdetalle.DataSource, DataTable).Rows(pos).Item("cobrar"), 0.00)
            Dim capNue As Double
            If Convert.ToDecimal(grdetalle.GetValue("cobrar")) > Convert.ToDecimal(grdetalle.GetValue("aporteDiesel")) Then
                If (Convert.ToDecimal(grdetalle.GetValue("cobrar")) - Convert.ToDecimal(grdetalle.GetValue("aporteDiesel"))) > Convert.ToDecimal(grdetalle.GetValue("aporte")) Then
                    If ((Convert.ToDecimal(grdetalle.GetValue("cobrar")) - Convert.ToDecimal(grdetalle.GetValue("aporteDiesel"))) - Convert.ToDecimal(grdetalle.GetValue("aporte"))) > Convert.ToDecimal(grdetalle.GetValue("capital")) Then
                        capNue = 0.00
                    Else
                        capNue = Convert.ToDecimal(grdetalle.GetValue("capital")) - ((Convert.ToDecimal(grdetalle.GetValue("cobrar")) - Convert.ToDecimal(grdetalle.GetValue("aporteDiesel"))) - Convert.ToDecimal(grdetalle.GetValue("aporte")))
                    End If
                Else
                    capNue = Convert.ToDecimal(grdetalle.GetValue("capital"))
                End If
            Else
                capNue = Convert.ToDecimal(grdetalle.GetValue("capital"))
            End If

            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("capital") = capNue
        Next
    End Sub

    Private Sub grdetalle_KeyDown(sender As Object, e As KeyEventArgs) Handles grdetalle.KeyDown

        Try
            If (e.KeyCode = Keys.Enter) Then
                If Convert.ToDouble(grdetalle.GetValue("cobrar")) <= (Convert.ToDouble(grdetalle.GetValue("capital1")) + Convert.ToDouble(grdetalle.GetValue("aporte1")) + Convert.ToDouble(grdetalle.GetValue("aporteDiesel1"))) Then
                    Dim pos As Integer = Convert.ToInt32(grdetalle.CurrentRow.RowIndex.ToString)


                    Dim aporteDiesel As Decimal = Convert.ToDouble(grdetalle.GetValue("aporteDiesel1"))
                    Dim aporte As Decimal = Convert.ToDouble(grdetalle.GetValue("aporte1"))
                    Dim capital As Decimal = Convert.ToDouble(grdetalle.GetValue("capital1"))
                    Dim capitalNuevo As Decimal

                    Dim prueba = Convert.ToDouble(grdetalle.GetValue("aporte1"))
                    If ((Convert.ToDecimal(grdetalle.GetValue("descApor")))) <= prueba Then

                        aporte = prueba - ((Convert.ToDecimal(grdetalle.GetValue("descApor"))))
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte") = aporte
                    Else
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("descApor") = 0
                        Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                        ToastNotification.Show(Me, "error descuento aporte".ToUpper,
                                                      img, 5000,
                                                      eToastGlowColor.Green,
                                                      eToastPosition.TopCenter)
                    End If
                    '_fnObtenerFilaDetalle(pos, lin)
                    If (((Convert.ToDecimal(grdetalle.GetValue("amortizacion")))) + Convert.ToDecimal(grdetalle.GetValue("cobrar"))) <= (aporte + capital + aporteDiesel) Then
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporteDiesel") = IIf(Convert.ToDecimal(grdetalle.GetValue("cobrar")) < aporteDiesel, aporteDiesel - Convert.ToDecimal(grdetalle.GetValue("cobrar")), 0.00)      'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte") = grdetalle.GetValue("tbcmin")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte") = IIf(Convert.ToDecimal(grdetalle.GetValue("cobrar")) > aporteDiesel, IIf((Convert.ToDecimal(grdetalle.GetValue("cobrar")) - aporteDiesel) > aporte, 0.00, aporte - (Convert.ToDecimal(grdetalle.GetValue("cobrar")) - aporteDiesel)), aporte)      'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte") = grdetalle.GetValue("tbcmin")

                        If Convert.ToDecimal(grdetalle.GetValue("cobrar")) > aporteDiesel Then
                            If (Convert.ToDecimal(grdetalle.GetValue("cobrar")) - aporteDiesel) > aporte Then
                                If ((Convert.ToDecimal(grdetalle.GetValue("cobrar")) - aporteDiesel) - aporte) > capital Then
                                    capitalNuevo = 0.00
                                Else
                                    capitalNuevo = capital - ((Convert.ToDecimal(grdetalle.GetValue("cobrar")) - aporteDiesel) - aporte)
                                End If
                            Else
                                capitalNuevo = capital
                            End If
                        Else
                            capitalNuevo = capital
                        End If
                        If ((Convert.ToDecimal(grdetalle.GetValue("amortizacion")))) <= Convert.ToDouble(grdetalle.GetValue("capital1")) Then
                            capitalNuevo = capitalNuevo - ((Convert.ToDecimal(grdetalle.GetValue("amortizacion"))))
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("capital") = capitalNuevo
                        Else
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("capital") = capitalNuevo
                            'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("amortizacion") = 0
                            ' Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                            'ToastNotification.Show(Me, "la amortizacion no puede ser mayor al capital".ToUpper,
                            '                              img, 5000,
                            '                              eToastGlowColor.Green,
                            '                              eToastPosition.TopCenter)
                        End If


                        ActualizarTotales()
                        grdetalle.Row = grdetalle.Row + 1
                        ActualizarTotales()
                        grdetalle.Row = grdetalle.Row - 1



                    Else

                        If (Convert.ToDecimal(grdetalle.GetValue("amortizacion"))) > Convert.ToDouble(grdetalle.GetValue("capital")) Then
                            ' CType(grdetalle.DataSource, DataTable).Rows(pos).Item("amortizacion") = 0
                        End If
                        Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                        ToastNotification.Show(Me, "el cobro no puede ser mayor al capital mas su aporte".ToUpper,
                                                  img, 5000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter)
                        ActualizarTotales()
                        grdetalle.Row = grdetalle.Row + 1
                        ActualizarTotales()
                        grdetalle.Row = grdetalle.Row - 1
                    End If

                    '_HabilitarProductos()

                Else

                End If
            End If
            If e.KeyCode = Keys.Up Then
                If Convert.ToDouble(grdetalle.GetValue("cobrar")) <= (Convert.ToDouble(grdetalle.GetValue("capital1")) + Convert.ToDouble(grdetalle.GetValue("aporte1")) + Convert.ToDouble(grdetalle.GetValue("aporteDiesel1"))) Then
                    Dim pos As Integer = Convert.ToInt32(grdetalle.CurrentRow.RowIndex.ToString)


                    Dim aporteDiesel As Decimal = Convert.ToDouble(grdetalle.GetValue("aporteDiesel1"))
                    Dim aporte As Decimal = Convert.ToDouble(grdetalle.GetValue("aporte1"))
                    Dim capital As Decimal = Convert.ToDouble(grdetalle.GetValue("capital1"))
                    Dim capitalNuevo As Decimal

                    Dim prueba = Convert.ToDouble(grdetalle.GetValue("aporte1"))
                    If ((Convert.ToDecimal(grdetalle.GetValue("descApor")))) <= prueba Then

                        aporte = prueba - ((Convert.ToDecimal(grdetalle.GetValue("descApor"))))
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte") = aporte
                    Else
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("descApor") = 0
                        Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                        ToastNotification.Show(Me, "error descuento aporte".ToUpper,
                                                      img, 5000,
                                                      eToastGlowColor.Green,
                                                      eToastPosition.TopCenter)
                    End If
                    '_fnObtenerFilaDetalle(pos, lin)
                    If (((Convert.ToDecimal(grdetalle.GetValue("amortizacion")))) + Convert.ToDecimal(grdetalle.GetValue("cobrar"))) <= (aporte + capital + aporteDiesel) Then
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporteDiesel") = IIf(Convert.ToDecimal(grdetalle.GetValue("cobrar")) < aporteDiesel, aporteDiesel - Convert.ToDecimal(grdetalle.GetValue("cobrar")), 0.00)      'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte") = grdetalle.GetValue("tbcmin")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte") = IIf(Convert.ToDecimal(grdetalle.GetValue("cobrar")) > aporteDiesel, IIf((Convert.ToDecimal(grdetalle.GetValue("cobrar")) - aporteDiesel) > aporte, 0.00, aporte - (Convert.ToDecimal(grdetalle.GetValue("cobrar")) - aporteDiesel)), aporte)      'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte") = grdetalle.GetValue("tbcmin")

                        If Convert.ToDecimal(grdetalle.GetValue("cobrar")) > aporteDiesel Then
                            If (Convert.ToDecimal(grdetalle.GetValue("cobrar")) - aporteDiesel) > aporte Then
                                If ((Convert.ToDecimal(grdetalle.GetValue("cobrar")) - aporteDiesel) - aporte) > capital Then
                                    capitalNuevo = 0.00
                                Else
                                    capitalNuevo = capital - ((Convert.ToDecimal(grdetalle.GetValue("cobrar")) - aporteDiesel) - aporte)
                                End If
                            Else
                                capitalNuevo = capital
                            End If
                        Else
                            capitalNuevo = capital
                        End If
                        If ((Convert.ToDecimal(grdetalle.GetValue("amortizacion")))) <= Convert.ToDouble(grdetalle.GetValue("capital1")) Then
                            capitalNuevo = capitalNuevo - ((Convert.ToDecimal(grdetalle.GetValue("amortizacion"))))
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("capital") = capitalNuevo
                        Else
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("capital") = capitalNuevo
                            'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("amortizacion") = 0
                            ' Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                            'ToastNotification.Show(Me, "la amortizacion no puede ser mayor al capital".ToUpper,
                            '                              img, 5000,
                            '                              eToastGlowColor.Green,
                            '                              eToastPosition.TopCenter)
                        End If


                        ActualizarTotales()
                        grdetalle.Row = grdetalle.Row + 1
                        ActualizarTotales()
                        grdetalle.Row = grdetalle.Row - 1



                    Else

                        If (Convert.ToDecimal(grdetalle.GetValue("amortizacion"))) > Convert.ToDouble(grdetalle.GetValue("capital")) Then
                            ' CType(grdetalle.DataSource, DataTable).Rows(pos).Item("amortizacion") = 0
                        End If
                        Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                        ToastNotification.Show(Me, "el cobro no puede ser mayor al capital mas su aporte".ToUpper,
                                                  img, 5000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter)
                        ActualizarTotales()
                        grdetalle.Row = grdetalle.Row + 1
                        ActualizarTotales()
                        grdetalle.Row = grdetalle.Row - 1
                    End If

                    '_HabilitarProductos()

                Else

                End If
            ElseIf e.KeyCode = Keys.Down Then
                If Convert.ToDouble(grdetalle.GetValue("cobrar")) <= (Convert.ToDouble(grdetalle.GetValue("capital1")) + Convert.ToDouble(grdetalle.GetValue("aporte1")) + Convert.ToDouble(grdetalle.GetValue("aporteDiesel1"))) Then
                    Dim pos As Integer = Convert.ToInt32(grdetalle.CurrentRow.RowIndex.ToString)


                    Dim aporteDiesel As Decimal = Convert.ToDouble(grdetalle.GetValue("aporteDiesel1"))
                    Dim aporte As Decimal = Convert.ToDouble(grdetalle.GetValue("aporte1"))
                    Dim capital As Decimal = Convert.ToDouble(grdetalle.GetValue("capital1"))
                    Dim capitalNuevo As Decimal

                    Dim prueba = Convert.ToDouble(grdetalle.GetValue("aporte1"))
                    If ((Convert.ToDecimal(grdetalle.GetValue("descApor")))) <= prueba Then

                        aporte = prueba - ((Convert.ToDecimal(grdetalle.GetValue("descApor"))))
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte") = aporte
                    Else
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("descApor") = 0
                        Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                        ToastNotification.Show(Me, "error descuento aporte".ToUpper,
                                                      img, 5000,
                                                      eToastGlowColor.Green,
                                                      eToastPosition.TopCenter)
                    End If
                    '_fnObtenerFilaDetalle(pos, lin)
                    If (((Convert.ToDecimal(grdetalle.GetValue("amortizacion")))) + Convert.ToDecimal(grdetalle.GetValue("cobrar"))) <= (aporte + capital + aporteDiesel) Then
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporteDiesel") = IIf(Convert.ToDecimal(grdetalle.GetValue("cobrar")) < aporteDiesel, aporteDiesel - Convert.ToDecimal(grdetalle.GetValue("cobrar")), 0.00)      'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte") = grdetalle.GetValue("tbcmin")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte") = IIf(Convert.ToDecimal(grdetalle.GetValue("cobrar")) > aporteDiesel, IIf((Convert.ToDecimal(grdetalle.GetValue("cobrar")) - aporteDiesel) > aporte, 0.00, aporte - (Convert.ToDecimal(grdetalle.GetValue("cobrar")) - aporteDiesel)), aporte)      'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte") = grdetalle.GetValue("tbcmin")

                        If Convert.ToDecimal(grdetalle.GetValue("cobrar")) > aporteDiesel Then
                            If (Convert.ToDecimal(grdetalle.GetValue("cobrar")) - aporteDiesel) > aporte Then
                                If ((Convert.ToDecimal(grdetalle.GetValue("cobrar")) - aporteDiesel) - aporte) > capital Then
                                    capitalNuevo = 0.00
                                Else
                                    capitalNuevo = capital - ((Convert.ToDecimal(grdetalle.GetValue("cobrar")) - aporteDiesel) - aporte)
                                End If
                            Else
                                capitalNuevo = capital
                            End If
                        Else
                            capitalNuevo = capital
                        End If
                        If ((Convert.ToDecimal(grdetalle.GetValue("amortizacion")))) <= Convert.ToDouble(grdetalle.GetValue("capital1")) Then
                            capitalNuevo = capitalNuevo - ((Convert.ToDecimal(grdetalle.GetValue("amortizacion"))))
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("capital") = capitalNuevo
                        Else
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("amortizacion") = 0
                            ' Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                            'ToastNotification.Show(Me, "la amortizacion no puede ser mayor al capital".ToUpper,
                            '                              img, 5000,
                            '                              eToastGlowColor.Green,
                            '                              eToastPosition.TopCenter)
                        End If


                        ActualizarTotales()
                        grdetalle.Row = grdetalle.Row + 1
                        ActualizarTotales()
                        grdetalle.Row = grdetalle.Row - 1



                    Else

                        If (Convert.ToDecimal(grdetalle.GetValue("amortizacion"))) > Convert.ToDouble(grdetalle.GetValue("capital")) Then
                            ' CType(grdetalle.DataSource, DataTable).Rows(pos).Item("amortizacion") = 0
                        End If
                        Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                        ToastNotification.Show(Me, "el cobro no puede ser mayor al capital mas su aporte".ToUpper,
                                                  img, 5000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter)
                        ActualizarTotales()
                        grdetalle.Row = grdetalle.Row + 1
                        ActualizarTotales()
                        grdetalle.Row = grdetalle.Row - 1
                        grdetalle.RootTable.Columns("cobrar").AggregateFunction = AggregateFunction.Sum
                    End If

                    '_HabilitarProductos()

                Else

                End If
            ElseIf e.KeyCode = Keys.Left Then
                If Convert.ToDouble(grdetalle.GetValue("cobrar")) <= (Convert.ToDouble(grdetalle.GetValue("capital1")) + Convert.ToDouble(grdetalle.GetValue("aporte1")) + Convert.ToDouble(grdetalle.GetValue("aporteDiesel1"))) Then
                    Dim pos As Integer = Convert.ToInt32(grdetalle.CurrentRow.RowIndex.ToString)


                    Dim aporteDiesel As Decimal = Convert.ToDouble(grdetalle.GetValue("aporteDiesel1"))
                    Dim aporte As Decimal = Convert.ToDouble(grdetalle.GetValue("aporte1"))
                    Dim capital As Decimal = Convert.ToDouble(grdetalle.GetValue("capital1"))
                    Dim capitalNuevo As Decimal

                    Dim prueba = Convert.ToDouble(grdetalle.GetValue("aporte1"))
                    If ((Convert.ToDecimal(grdetalle.GetValue("descApor")))) <= prueba Then

                        aporte = prueba - ((Convert.ToDecimal(grdetalle.GetValue("descApor"))))
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte") = aporte
                    Else
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("descApor") = 0
                        Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                        ToastNotification.Show(Me, "error descuento aporte".ToUpper,
                                                      img, 5000,
                                                      eToastGlowColor.Green,
                                                      eToastPosition.TopCenter)
                    End If
                    '_fnObtenerFilaDetalle(pos, lin)
                    If (((Convert.ToDecimal(grdetalle.GetValue("amortizacion")))) + Convert.ToDecimal(grdetalle.GetValue("cobrar"))) <= (aporte + capital + aporteDiesel) Then
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporteDiesel") = IIf(Convert.ToDecimal(grdetalle.GetValue("cobrar")) < aporteDiesel, aporteDiesel - Convert.ToDecimal(grdetalle.GetValue("cobrar")), 0.00)      'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte") = grdetalle.GetValue("tbcmin")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte") = IIf(Convert.ToDecimal(grdetalle.GetValue("cobrar")) > aporteDiesel, IIf((Convert.ToDecimal(grdetalle.GetValue("cobrar")) - aporteDiesel) > aporte, 0.00, aporte - (Convert.ToDecimal(grdetalle.GetValue("cobrar")) - aporteDiesel)), aporte)      'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte") = grdetalle.GetValue("tbcmin")

                        If Convert.ToDecimal(grdetalle.GetValue("cobrar")) > aporteDiesel Then
                            If (Convert.ToDecimal(grdetalle.GetValue("cobrar")) - aporteDiesel) > aporte Then
                                If ((Convert.ToDecimal(grdetalle.GetValue("cobrar")) - aporteDiesel) - aporte) > capital Then
                                    capitalNuevo = 0.00
                                Else
                                    capitalNuevo = capital - ((Convert.ToDecimal(grdetalle.GetValue("cobrar")) - aporteDiesel) - aporte)
                                End If
                            Else
                                capitalNuevo = capital
                            End If
                        Else
                            capitalNuevo = capital
                        End If
                        If ((Convert.ToDecimal(grdetalle.GetValue("amortizacion")))) <= Convert.ToDouble(grdetalle.GetValue("capital1")) Then
                            capitalNuevo = capitalNuevo - ((Convert.ToDecimal(grdetalle.GetValue("amortizacion"))))
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("capital") = capitalNuevo
                        Else
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("amortizacion") = 0
                            ' Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                            'ToastNotification.Show(Me, "la amortizacion no puede ser mayor al capital".ToUpper,
                            '                              img, 5000,
                            '                              eToastGlowColor.Green,
                            '                              eToastPosition.TopCenter)
                        End If


                        ActualizarTotales()
                        grdetalle.Row = grdetalle.Row + 1
                        ActualizarTotales()
                        grdetalle.Row = grdetalle.Row - 1



                    Else

                        If (Convert.ToDecimal(grdetalle.GetValue("amortizacion"))) > Convert.ToDouble(grdetalle.GetValue("capital")) Then
                            ' CType(grdetalle.DataSource, DataTable).Rows(pos).Item("amortizacion") = 0
                        End If
                        Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                        ToastNotification.Show(Me, "el cobro no puede ser mayor al capital mas su aporte".ToUpper,
                                                  img, 5000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter)
                        ActualizarTotales()
                        grdetalle.Row = grdetalle.Row + 1
                        ActualizarTotales()
                        grdetalle.Row = grdetalle.Row - 1
                        grdetalle.RootTable.Columns("cobrar").AggregateFunction = AggregateFunction.Sum
                    End If

                    '_HabilitarProductos()

                Else

                End If
            ElseIf e.KeyCode = Keys.Right Then
                If Convert.ToDouble(grdetalle.GetValue("cobrar")) <= (Convert.ToDouble(grdetalle.GetValue("capital1")) + Convert.ToDouble(grdetalle.GetValue("aporte1")) + Convert.ToDouble(grdetalle.GetValue("aporteDiesel1"))) Then
                    Dim pos As Integer = Convert.ToInt32(grdetalle.CurrentRow.RowIndex.ToString)


                    Dim aporteDiesel As Decimal = Convert.ToDouble(grdetalle.GetValue("aporteDiesel1"))
                    Dim aporte As Decimal = Convert.ToDouble(grdetalle.GetValue("aporte1"))
                    Dim capital As Decimal = Convert.ToDouble(grdetalle.GetValue("capital1"))
                    Dim capitalNuevo As Decimal

                    Dim prueba = Convert.ToDouble(grdetalle.GetValue("aporte1"))
                    If ((Convert.ToDecimal(grdetalle.GetValue("descApor")))) <= prueba Then

                        aporte = prueba - ((Convert.ToDecimal(grdetalle.GetValue("descApor"))))
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte") = aporte
                    Else
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("descApor") = 0
                        Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                        ToastNotification.Show(Me, "error descuento aporte".ToUpper,
                                                      img, 5000,
                                                      eToastGlowColor.Green,
                                                      eToastPosition.TopCenter)
                    End If
                    '_fnObtenerFilaDetalle(pos, lin)
                    If (((Convert.ToDecimal(grdetalle.GetValue("amortizacion")))) + Convert.ToDecimal(grdetalle.GetValue("cobrar"))) <= (aporte + capital + aporteDiesel) Then
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporteDiesel") = IIf(Convert.ToDecimal(grdetalle.GetValue("cobrar")) < aporteDiesel, aporteDiesel - Convert.ToDecimal(grdetalle.GetValue("cobrar")), 0.00)      'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte") = grdetalle.GetValue("tbcmin")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte") = IIf(Convert.ToDecimal(grdetalle.GetValue("cobrar")) > aporteDiesel, IIf((Convert.ToDecimal(grdetalle.GetValue("cobrar")) - aporteDiesel) > aporte, 0.00, aporte - (Convert.ToDecimal(grdetalle.GetValue("cobrar")) - aporteDiesel)), aporte)      'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte") = grdetalle.GetValue("tbcmin")

                        If Convert.ToDecimal(grdetalle.GetValue("cobrar")) > aporteDiesel Then
                            If (Convert.ToDecimal(grdetalle.GetValue("cobrar")) - aporteDiesel) > aporte Then
                                If ((Convert.ToDecimal(grdetalle.GetValue("cobrar")) - aporteDiesel) - aporte) > capital Then
                                    capitalNuevo = 0.00
                                Else
                                    capitalNuevo = capital - ((Convert.ToDecimal(grdetalle.GetValue("cobrar")) - aporteDiesel) - aporte)
                                End If
                            Else
                                capitalNuevo = capital
                            End If
                        Else
                            capitalNuevo = capital
                        End If
                        If ((Convert.ToDecimal(grdetalle.GetValue("amortizacion")))) <= Convert.ToDouble(grdetalle.GetValue("capital1")) Then
                            capitalNuevo = capitalNuevo - ((Convert.ToDecimal(grdetalle.GetValue("amortizacion"))))
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("capital") = capitalNuevo
                        Else
                            CType(grdetalle.DataSource, DataTable).Rows(pos).Item("amortizacion") = 0
                            ' Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                            'ToastNotification.Show(Me, "la amortizacion no puede ser mayor al capital".ToUpper,
                            '                              img, 5000,
                            '                              eToastGlowColor.Green,
                            '                              eToastPosition.TopCenter)
                        End If


                        ActualizarTotales()
                        grdetalle.Row = grdetalle.Row + 1
                        ActualizarTotales()
                        grdetalle.Row = grdetalle.Row - 1



                    Else

                        If (Convert.ToDecimal(grdetalle.GetValue("amortizacion"))) > Convert.ToDouble(grdetalle.GetValue("capital")) Then
                            ' CType(grdetalle.DataSource, DataTable).Rows(pos).Item("amortizacion") = 0
                        End If
                        Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                        ToastNotification.Show(Me, "el cobro no puede ser mayor al capital mas su aporte".ToUpper,
                                                  img, 5000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter)
                        ActualizarTotales()
                        grdetalle.Row = grdetalle.Row + 1
                        ActualizarTotales()
                        grdetalle.Row = grdetalle.Row - 1
                        grdetalle.RootTable.Columns("cobrar").AggregateFunction = AggregateFunction.Sum
                    End If

                    '_HabilitarProductos()

                Else

                End If
            End If
            If (Not _fnAccesible()) Then
                Return
            End If

salirIf:

        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
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
            'dt = L_fnListarProductos(cbSucursal.Value, Str(_CodCliente))  ''1=Almacen
            Table_Producto = dt.Copy
        Else
            'dt = L_fnListarProductosSinLote(cbSucursal.Value, Str(_CodCliente), CType(grdetalle.DataSource, DataTable).Clone)  ''1=Almacen
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



    Public Sub CalcularDescuentosTotal()

        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)

        Dim sumaDescuento As Double = 0

        For i As Integer = 0 To dt.Rows.Count - 1 Step 1

            If (dt.Rows(i).Item("estado") >= 0) Then

                sumaDescuento += dt.Rows(i).Item("tbdesc")

            End If

        Next



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
    Private Sub grdetalle_CellValueChanged(sender As Object, e As ColumnActionEventArgs)
    End Sub




    Private Sub grdetalle_CellEdited_1(sender As Object, e As ColumnActionEventArgs)
        ActualizarTotales()
    End Sub
    Private Sub grdetalle_MouseClick(sender As Object, e As MouseEventArgs)
        Try
            If (Not _fnAccesible()) Then
                Return
            End If
            'If (grdetalle.RowCount >= 2) Then
            '    If (grdetalle.CurrentColumn.Index = grdetalle.RootTable.Columns("img").Index) Then
            '        _prEliminarFila()
            '        CalculoDescuentoXProveedor()
            '    End If
            'End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try

    End Sub
    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click


        If Modificar = True Then
            _prGuardarModificado()
            Modificar = False
        Else
            _prGuardar()
        End If
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        _prhabilitarModificar()
        Modificar = True

        _prCargarDetalleVenta2(txtEstado.Text)
        AjustarGrid()
        'Try

        '    Dim dt As DataTable
        '    'dt = L_fnListarClientes()
        '    dt = L_fnListarClientesVentas(_CodCliente)

        '    _CodEmpleado = dt.Rows(0).Item(8)

        '    'swTipoVenta.Value = grVentas.GetValue("tatven")

        '    If (grVentas.RowCount > 0) Then
        '        If (gb_FacturaEmite) Then
        '            If (P_fnValidarFacturaVigente()) Then
        '                Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

        '                ToastNotification.Show(Me, "No se puede modificar la venta con codigo ".ToUpper + tbCodigo.Text + ", su factura esta validada por impuesto.".ToUpper,
        '                                          img, 2000,
        '                                          eToastGlowColor.Green,
        '                                          eToastPosition.TopCenter)
        '                Exit Sub
        '            End If
        '        End If

        '        _prhabilitar()


        '        btnNuevo.Enabled = False
        '        btnModificar.Enabled = False
        '        btnEliminar.Enabled = False
        '        btnGrabar.Enabled = True

        '        PanelNavegacion.Enabled = False
        '        _prCargarIconELiminar()
        '    End If
        'Catch ex As Exception
        '    MostrarMensajeError(ex.Message)
        'End Try
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
            '_prMostrarRegistro(_pos)
            grVentas.Row = _pos
        End If
    End Sub

    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        Dim _pos As Integer = grVentas.Row
        If grVentas.RowCount > 0 Then
            _pos = grVentas.RowCount - 1
            '_prMostrarRegistro(_pos)
            grVentas.Row = _pos
        End If
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click

        Dim _MPos As Integer = grVentas.Row
        If _MPos > 0 And grVentas.RowCount > 0 Then
            _MPos = _MPos - 1
            '_prMostrarRegistro(_MPos)
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


    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        P_GenerarReporte(tbId.Text)
    End Sub

    Private Sub TbNit_KeyPress(sender As Object, e As KeyPressEventArgs)
        g_prValidarTextBox(1, e)
    End Sub

    Private Sub swTipoVenta_Leave(sender As Object, e As EventArgs)
        grdetalle.Select()
    End Sub

    Private Sub cbSucursal_ValueChanged(sender As Object, e As EventArgs)
        Table_Producto = Nothing
    End Sub

    Private Sub tbNit_Leave(sender As Object, e As EventArgs)
        grdetalle.Select()
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

    Private Sub TextBoxX2_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtCambio1_KeyDown(sender As Object, e As KeyEventArgs)
        If (e.KeyData = Keys.Control + Keys.A) Then
            _prGuardar()
        End If
    End Sub

    Private Sub TextBoxX3_TextChanged(sender As Object, e As EventArgs) Handles TextBoxX3.TextChanged
        If btnGrabar.Enabled = True Then
            If TextBoxX3.Text <> "" Then
                If TextBoxX7.Text <> "" Then
                    TextBoxX8.Text = CDbl(TextBoxX7.Text) * IIf(TextBoxX3.Text = "", 0, CDbl(TextBoxX3.Text))
                    TextBoxX9.Text = CDbl(TextBoxX7.Text) * IIf(TextBoxX3.Text = "", 0, CDbl(TextBoxX3.Text)) * 6.96
                End If
            End If
        End If
    End Sub

    Private Sub CheckGrupo_CheckedChanged(sender As Object, e As EventArgs) Handles CheckGrupo.CheckedChanged
        _prCargarDetalleVenta(_CodCliente)
        ActualizarTotales()
        tbCanero.Clear()
        tbCodCanero.Clear()
    End Sub

    Private Sub cbQuincena_ValueChanged(sender As Object, e As EventArgs) Handles cbQuincena.ValueChanged
        If btnNuevo.Enabled = False Then
            If SwitchButton1.Value = True Then
                Dim dt As DataTable = cbQuincena.DataSource
                tbFechaVenta.Value = dt.Rows(cbQuincena.Value - 1).Item("finQuin")
            End If
        End If
    End Sub

    Private Sub cbGestion_ValueChanged(sender As Object, e As EventArgs)
        If SwitchButton1.Value = True Then
            If cbQuincena.Text <> String.Empty And cbQuincena.Text <> "0" Then
                Dim dt As DataTable = cargarFechaCierre(cbGestion.Value, cbQuincena.Value)
                tbFechaVenta.Value = dt.Rows(0).Item("fecha")
            End If
        End If
    End Sub

    Private Sub SwitchButton1_ValueChanged(sender As Object, e As EventArgs) Handles SwitchButton1.ValueChanged
        If btnGrabar.Enabled = True Then
            If SwitchButton1.Value = False Then
                cbGestion.Enabled = False
                cbQuincena.Enabled = False
                tbFechaVenta.Enabled = True
                TextBoxX3.Enabled = False
                'CheckGrupo.Enabled = False
            Else
                cbQuincena.Enabled = True
                cbGestion.Enabled = True
                tbFechaVenta.Enabled = False
                TextBoxX3.Enabled = True
                'CheckGrupo.Enabled = True
            End If
        End If
        If SwitchButton1.Value = True Then
            CheckGrupo.Enabled = True
        Else
            CheckGrupo.Enabled = False
        End If
        _Limpiar()
    End Sub

    Private Sub TextBoxX4_TextChanged(sender As Object, e As EventArgs)
        If TextBoxX5.Text <> "" And TextBoxX5.Text <> "0.00" And TextBoxX4.Text <> "" And TextBoxX4.Text <> "0.00" Then
            TextBoxX6.Text = (CDbl(TextBoxX5.Text) * 100 / CDbl(TextBoxX4.Text)).ToString("0.00")
        End If
    End Sub



    Private Sub grGrupoEco_EditingCell(sender As Object, e As EditingCellEventArgs)
        e.Cancel = True
    End Sub


    Private Sub tbInstitucion_KeyUp(sender As Object, e As KeyEventArgs) Handles tbInstitucion.KeyUp
        If (_fnAccesible()) Then
            If e.KeyData = Keys.Control + Keys.Enter Then
                Dim dt As DataTable
                dt = L_fnListarInstitucion()

                Dim listEstCeldas As New List(Of Modelo.Celda)
                listEstCeldas.Add(New Modelo.Celda("id,", False, "ID", 50))
                listEstCeldas.Add(New Modelo.Celda("CodInst", True, "CODIGO", 50))
                listEstCeldas.Add(New Modelo.Celda("nomInst", True, "NOMBRE", 280))
                listEstCeldas.Add(New Modelo.Celda("direc", True, "DIRECCION".ToUpper, 150))
                listEstCeldas.Add(New Modelo.Celda("telf", True, "TELEFONO", 220))
                listEstCeldas.Add(New Modelo.Celda("campo1", False, "campo1".ToUpper, 200))
                listEstCeldas.Add(New Modelo.Celda("campo2", False, "F.campo2".ToUpper, 150))
                listEstCeldas.Add(New Modelo.Celda("campo3", False, "campo3".ToUpper, 200))
                listEstCeldas.Add(New Modelo.Celda("fact", False, "F.fact".ToUpper, 150))
                listEstCeldas.Add(New Modelo.Celda("hact", False, "hact".ToUpper, 200))
                listEstCeldas.Add(New Modelo.Celda("uact", False, "F.uact".ToUpper, 150))
                Dim ef = New Efecto
                ef.tipo = 3
                ef.dt = dt
                ef.SeleclCol = 1
                ef.listEstCeldas = listEstCeldas
                ef.alto = 50
                ef.ancho = 350
                ef.Context = "Seleccione Institucion".ToUpper
                ef.ShowDialog()
                Dim bandera As Boolean = False
                bandera = ef.band
                If (bandera = True) Then
                    Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                    If (IsNothing(Row)) Then
                        ' tbInsCan.Focus()
                        Return
                    End If
                    tbcodInst.Text = Row.Cells("CodInst").Value
                    tbInstitucion.Text = Row.Cells("nomInst").Value
                    idInstitucion.Text = Row.Cells("id").Value
                    _CodInstitucion = Row.Cells("id").Value
                    _prCargarListaCanerosxInstitucion(idInstitucion.Text)
                End If

            End If

        End If
    End Sub



    Private Sub grCanero_EditingCell_1(sender As Object, e As EditingCellEventArgs) Handles grCanero.EditingCell
        If (e.Column.Index = grCanero.RootTable.Columns("ydnumi").Index Or
             e.Column.Index = grCanero.RootTable.Columns("ydrazonsocial").Index) Then

            e.Cancel = True
        Else
            e.Cancel = False
        End If
    End Sub

    Private Sub grdetalle_Click(sender As Object, e As EventArgs) Handles grdetalle.Click
        If btnNuevo.Enabled = True Then
            If filaEditada IsNot Nothing Then
                If Convert.ToDouble(grdetalle.GetValue("cobrar")) < (Convert.ToDouble(grdetalle.GetValue("capital1")) + Convert.ToDouble(grdetalle.GetValue("aporte1")) + Convert.ToDouble(grdetalle.GetValue("aporteDiesel1"))) Then
                    Dim pos As Integer = Convert.ToInt32(grdetalle.CurrentRow.RowIndex.ToString)


                    Dim aporteDiesel As Decimal = CType(grdetalle.DataSource, DataTable).Rows(filaEditada.AbsolutePosition - IIf(filaEditada.AbsolutePosition = 0, 0, 1)).Item("aporteDiesel1")
                    Dim aporte As Decimal = CType(grdetalle.DataSource, DataTable).Rows(filaEditada.AbsolutePosition - IIf(filaEditada.AbsolutePosition = 0, 0, 1)).Item("aporte1")
                    Dim capital As Decimal = CType(grdetalle.DataSource, DataTable).Rows(filaEditada.AbsolutePosition - IIf(filaEditada.AbsolutePosition = 0, 0, 1)).Item("capital1")
                    Dim capitalNuevo As Decimal

                    Dim prueba = Convert.ToDouble(CType(grdetalle.DataSource, DataTable).Rows(filaEditada.AbsolutePosition - IIf(filaEditada.AbsolutePosition = 0, 0, 1)).Item("aporte1"))
                    If ((Convert.ToDecimal(CType(grdetalle.DataSource, DataTable).Rows(filaEditada.AbsolutePosition - IIf(filaEditada.AbsolutePosition = 0, 0, 1)).Item("descApor")))) <= prueba Then

                        aporte = prueba - ((Convert.ToDecimal(CType(grdetalle.DataSource, DataTable).Rows(filaEditada.AbsolutePosition - IIf(filaEditada.AbsolutePosition = 0, 0, 1)).Item("descApor"))))
                        CType(grdetalle.DataSource, DataTable).Rows(filaEditada.AbsolutePosition - IIf(filaEditada.AbsolutePosition = 0, 0, 1)).Item("aporte") = aporte
                    Else
                        CType(grdetalle.DataSource, DataTable).Rows(filaEditada.AbsolutePosition - IIf(filaEditada.AbsolutePosition = 0, 0, 1)).Item("descApor") = 0
                        Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                        ToastNotification.Show(Me, "error descuento aporte".ToUpper,
                                                      img, 5000,
                                                      eToastGlowColor.Green,
                                                      eToastPosition.TopCenter)
                    End If
                    '_fnObtenerFilaDetalle(pos, lin)
                    If (((Convert.ToDecimal(CType(grdetalle.DataSource, DataTable).Rows(filaEditada.AbsolutePosition - IIf(filaEditada.AbsolutePosition = 0, 0, 1)).Item("amortizacion")))) + Convert.ToDecimal(CType(grdetalle.DataSource, DataTable).Rows(filaEditada.AbsolutePosition - IIf(filaEditada.AbsolutePosition = 0, 0, 1)).Item("cobrar"))) <= (aporte + capital + aporteDiesel) Then
                        CType(grdetalle.DataSource, DataTable).Rows(filaEditada.AbsolutePosition - IIf(filaEditada.AbsolutePosition = 0, 0, 1)).Item("aporteDiesel") = IIf(Convert.ToDecimal(CType(grdetalle.DataSource, DataTable).Rows(filaEditada.AbsolutePosition - IIf(filaEditada.AbsolutePosition = 0, 0, 1)).Item("cobrar")) < aporteDiesel, aporteDiesel - Convert.ToDecimal(CType(grdetalle.DataSource, DataTable).Rows(filaEditada.AbsolutePosition - IIf(filaEditada.AbsolutePosition = 0, 0, 1)).Item("cobrar")), 0.00)      'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte") = grdetalle.GetValue("tbcmin")
                        CType(grdetalle.DataSource, DataTable).Rows(filaEditada.AbsolutePosition - IIf(filaEditada.AbsolutePosition = 0, 0, 1)).Item("aporte") = IIf(Convert.ToDecimal(CType(grdetalle.DataSource, DataTable).Rows(filaEditada.AbsolutePosition - IIf(filaEditada.AbsolutePosition = 0, 0, 1)).Item("cobrar")) > aporteDiesel, IIf((Convert.ToDecimal(CType(grdetalle.DataSource, DataTable).Rows(filaEditada.AbsolutePosition - IIf(filaEditada.AbsolutePosition = 0, 0, 1)).Item("cobrar")) - aporteDiesel) > aporte, 0.00, aporte - (Convert.ToDecimal(CType(grdetalle.DataSource, DataTable).Rows(filaEditada.AbsolutePosition - IIf(filaEditada.AbsolutePosition = 0, 0, 1)).Item("cobrar")) - aporteDiesel)), aporte)      'CType(grdetalle.DataSource, DataTable).Rows(pos).Item("aporte") = grdetalle.GetValue("tbcmin")

                        If Convert.ToDecimal(CType(grdetalle.DataSource, DataTable).Rows(filaEditada.AbsolutePosition - IIf(filaEditada.AbsolutePosition = 0, 0, 1)).Item("cobrar")) > aporteDiesel Then
                            If (Convert.ToDecimal(CType(grdetalle.DataSource, DataTable).Rows(filaEditada.AbsolutePosition - IIf(filaEditada.AbsolutePosition = 0, 0, 1)).Item("cobrar")) - aporteDiesel) > aporte Then
                                If ((Convert.ToDecimal(CType(grdetalle.DataSource, DataTable).Rows(filaEditada.AbsolutePosition - IIf(filaEditada.AbsolutePosition = 0, 0, 1)).Item("cobrar")) - aporteDiesel) - aporte) > capital Then
                                    capitalNuevo = 0.00
                                Else
                                    capitalNuevo = capital - ((Convert.ToDecimal(CType(grdetalle.DataSource, DataTable).Rows(filaEditada.AbsolutePosition - IIf(filaEditada.AbsolutePosition = 0, 0, 1)).Item("cobrar")) - aporteDiesel) - aporte)
                                End If
                            Else
                                capitalNuevo = capital
                            End If
                        Else
                            capitalNuevo = capital
                        End If
                        If ((Convert.ToDecimal(CType(grdetalle.DataSource, DataTable).Rows(filaEditada.AbsolutePosition - IIf(filaEditada.AbsolutePosition = 0, 0, 1)).Item("amortizacion")))) <= Convert.ToDouble(CType(grdetalle.DataSource, DataTable).Rows(filaEditada.AbsolutePosition - IIf(filaEditada.AbsolutePosition = 0, 0, 1)).Item("capital1")) Then
                            capitalNuevo = capitalNuevo - ((Convert.ToDecimal(CType(grdetalle.DataSource, DataTable).Rows(filaEditada.AbsolutePosition - IIf(filaEditada.AbsolutePosition = 0, 0, 1)).Item("amortizacion"))))
                            CType(grdetalle.DataSource, DataTable).Rows(filaEditada.AbsolutePosition - IIf(filaEditada.AbsolutePosition = 0, 0, 1)).Item("capital") = capitalNuevo
                        Else
                            CType(grdetalle.DataSource, DataTable).Rows(filaEditada.AbsolutePosition - IIf(filaEditada.AbsolutePosition = 0, 0, 1)).Item("amortizacion") = 0
                            ' Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                            'ToastNotification.Show(Me, "la amortizacion no puede ser mayor al capital".ToUpper,
                            '                              img, 5000,
                            '                              eToastGlowColor.Green,
                            '                              eToastPosition.TopCenter)
                        End If


                        ActualizarTotales()
                        grdetalle.Row = grdetalle.Row + 1
                        ActualizarTotales()
                        grdetalle.Row = grdetalle.Row - 1



                    Else

                        If (Convert.ToDecimal(CType(grdetalle.DataSource, DataTable).Rows(filaEditada.AbsolutePosition - IIf(filaEditada.AbsolutePosition = 0, 0, 1)).Item("amortizacion"))) > Convert.ToDouble(CType(grdetalle.DataSource, DataTable).Rows(filaEditada.AbsolutePosition - IIf(filaEditada.AbsolutePosition = 0, 0, 1)).Item("capital")) Then
                            ' CType(grdetalle.DataSource, DataTable).Rows(pos).Item("amortizacion") = 0
                        End If
                        Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                        ToastNotification.Show(Me, "el cobro no puede ser mayor al capital mas su aporte".ToUpper,
                                                  img, 5000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter)
                        ActualizarTotales()


                    End If

                    '_HabilitarProductos()

                Else

                End If
                filaEditada = Nothing
            End If

        End If

    End Sub

    Private Sub cbGestion_ValueChanged_1(sender As Object, e As EventArgs) Handles cbGestion.ValueChanged
        _prCargarQuincena(cbQuincena)
        ' _Limpiar()
    End Sub


    Private filaEditada As Janus.Windows.GridEX.GridEXRow = Nothing
    Private Sub grdetalle_CellValueChanged_1(sender As Object, e As ColumnActionEventArgs) Handles grdetalle.CellValueChanged
        Try
            ' Obtener la fila editada
            filaEditada = grdetalle.CurrentRow
            ' Obtener el nombre de la columna editada
            Dim columnaEditada As String = e.Column.Key
            Dim columnaEditad As Double = (Convert.ToDouble(grdetalle.GetValue("capital")) + Convert.ToDouble(grdetalle.GetValue("aporte")) + Convert.ToDouble(grdetalle.GetValue("aporteDiesel")))
            ' Realizar acciones basadas en la columna editada
            'MessageBox.Show("Se editó la columna: " & columnaEditada)
            If columnaEditada = "cobrar" Then
                If Convert.ToDouble(grdetalle.GetValue("cobrar")) > (Convert.ToDouble(grdetalle.GetValue("capital")) + Convert.ToDouble(grdetalle.GetValue("aporte")) + Convert.ToDouble(grdetalle.GetValue("aporteDiesel"))) Then
                    MessageBox.Show("El COBRO NO PUEDE SER MAYOR")
                    Dim currentRow As Janus.Windows.GridEX.GridEXRow = grdetalle.CurrentRow
                    Dim valorActual = currentRow.Cells("cobrar").Value

                    currentRow.Cells("cobrar").Value = 0

                End If
            ElseIf columnaEditada = "amortizacion" Then
                If Convert.ToDouble(grdetalle.GetValue("amortizacion")) > (Convert.ToDouble(grdetalle.GetValue("capital"))) Then
                    MessageBox.Show("LA AMORTIZACION NO PUEDE SER MAYOR")
                    Dim currentRow As Janus.Windows.GridEX.GridEXRow = grdetalle.CurrentRow
                    currentRow.Cells("amortizacion").Value = 0

                End If
            ElseIf columnaEditada = "descApor" Then
                If Convert.ToDouble(grdetalle.GetValue("descApor")) > (Convert.ToDouble(grdetalle.GetValue("aporte1"))) Then
                    MessageBox.Show("EL DESCUENTO APORTE NO PUEDE SER MAYOR")
                    Dim currentRow As Janus.Windows.GridEX.GridEXRow = grdetalle.CurrentRow
                    currentRow.Cells("descApor").Value = 0

                End If
            End If

            If (Not IsNumeric(grdetalle.GetValue("descApor")) Or grdetalle.GetValue("descApor").ToString = String.Empty) Then

                'L_fnListarDescuentosTodos
                Dim pos As Integer = Convert.ToInt32(grdetalle.CurrentRow.RowIndex.ToString)

                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("descApor") = 0

            End If



        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub







    Private Sub grCanero_KeyDown_1(sender As Object, e As KeyEventArgs) Handles grCanero.KeyDown
        If grCanero.RowCount > 0 Then
            tbCodCanero.Text = IIf(grCanero.GetValue("ydrazonsocial") = "", "", grCanero.GetValue("ydcod"))
            tbCanero.Text = IIf(grCanero.GetValue("ydrazonsocial") = "", "", grCanero.GetValue("ydrazonsocial"))
            If SwitchButton1.Value = True Then
                If cbQuincena.Text <> String.Empty And cbGestion.Text <> String.Empty Then
                    If e.KeyData = Keys.Enter Then
                        _CodCliente = grCanero.GetValue("ydnumi")
                        TextBoxX4.Text = 0.00
                        TextBoxX5.Text = 0.00
                        TextBoxX6.Text = 0.00


                        CargarTotalQuincena(cbGestion.Value, _CodCliente)
                        _prCargarGrupoEco(_CodCliente)

                        _prCargarDetalleVenta(_CodCliente)


                        ActualizarTotales()

                    End If
                Else
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "Por Favor Seleccione una Gestion y una Quincena".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

                End If
            Else
                If e.KeyData = Keys.Enter Then

                    _CodCliente = grCanero.GetValue("ydnumi")

                    _prCargarGrupoEco(_CodCliente)

                    _prCargarDetalleVenta(_CodCliente)


                    ActualizarTotales()

                End If
            End If
        End If
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
    Public Sub _GuardarNuevoImprimirBoleta()
        Try
            Dim numi As String = ""
            Dim tabla As DataTable = L_fnMostrarMontos(0)
            Dim factura = gb_FacturaEmite

            ''Verifica si existe estock para los productos
            If _prExisteStockParaProducto() Then
                ' Dim Succes As String = Emisor(tokenSifac)
                'If Succes = 2 Or Succes = 8 Or Succes = 5 Then
                Dim dtDetalle As DataTable = rearmarDetalle()
                Dim res As Boolean = True 'L_fnGrabarVenta(numi, "", tbFechaVenta.Value.ToString("yyyy/MM/dd"), gi_userNumi,
                ' IIf(swTipoVenta.Value = True, 1, 0), IIf(swTipoVenta.Value = True,
                'Now.Date.ToString("yyyy/MM/dd"), tbFechaVenc.Value.ToString("yyyy/MM/dd")),
                ' _CodCliente, IIf(swMoneda.Value = True, 1, 0),
                '  tbObservacion.Text, tbMdesc.Value, tbIce.Value, tbTotalBs.Text,
                '  dtDetalle, cbSucursal.Value, 0, tabla, _CodEmpleado, Programa)
                If res Then
                    'res = P_fnGrabarFacturarTFV001(numi)
                    'Emite factura
                    'If (gb_FacturaEmite) Then
                    '    If tbNit.Text <> String.Empty Then

                    '        P_fnGenerarFactura(numi)
                    '        _prImiprimirNotaVenta(numi)
                    '    Else
                    '        _prImiprimirNotaVenta(numi)
                    '    End If
                    ' Else
                    _prImiprimirNotaVenta(numi)
                    ' End If
                    _prCargarVenta1()
                    _prInhabiliitar()
                    If grVentas.RowCount > 0 Then
                        _prMostrarRegistro(0)

                    End If

                    'contabilizar()

                    Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                    ToastNotification.Show(Me, "Código de Venta ".ToUpper + tbCodigo.Text + " Grabado con Exito.".ToUpper,
                                                  img, 2000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter
                                                  )


                    '_Limpiar()
                    'Table_Producto = Nothing

                Else
                    Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                    ToastNotification.Show(Me, "La Venta no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                End If

            End If


            'Else
            '    MessageBox.Show(mensajeRespuesta)
            'End If



        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try


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
            .FormatString = "0.00000"
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

    Private Sub tbCliente_TextChanged(sender As Object, e As EventArgs)
        If btnNuevo.Enabled = True Then
            'Dim dt As DataTable
            'dt = L_fnListarCaneroInstitucion(_CodCliente)
            'Dim row As DataRow = dt.Rows(dt.Rows.Count - 1)

        End If



    End Sub

    Private Sub ActualizarTotales()
        Dim dtIngEgre As DataTable = CType(grdetalle.DataSource, DataTable)
        TConv = IIf(IsDBNull(dtIngEgre.Compute("Sum(deuda)", " taalm=10016 or taalm=100160")), 0, dtIngEgre.Compute("Sum(deuda)", "taalm=10016 or taalm=100161"))
        TCont = IIf(IsDBNull(dtIngEgre.Compute("Sum(deuda)", "taalm  = 10001 or taalm  = 100011  or taalm  = 10002 or taalm  = 100021 or taalm  = 10003 or taalm  = 100031 or taalm  = 10004 or taalm  = 100041 or taalm=10011 or taalm=100111 or taalm=10012 or taalm=100121 or taalm=10013 or taalm=100131  or taalm=10015 or taalm=100151 or taalm = 10006 or taalm=100061")), 0, dtIngEgre.Compute("Sum(deuda)", "taalm  = 10001 or taalm  = 100011 or taalm  = 10002 or taalm  = 100021 or taalm  = 10003 or taalm  = 100031 or taalm  = 10004 or taalm  = 100041 or taalm=10011 or taalm=100111  or taalm=10012 or taalm=100121 or taalm=10013 or taalm=100131 or taalm=10015 or taalm=100151 or taalm=10006 or taalm=100061"))
        TRest = IIf(IsDBNull(dtIngEgre.Compute("Sum(deuda)", "taalm  = 10005")), 0, dtIngEgre.Compute("Sum(deuda)", "taalm  = 10005"))
        TComb = IIf(IsDBNull(dtIngEgre.Compute("Sum(deuda)", "taalm  = 10007 or taalm  = 100071 or taalm  = 3 or taalm  = 4")), 0, dtIngEgre.Compute("Sum(deuda)", "taalm  = 10007 or taalm  = 100071  or taalm  = 3 or taalm  = 4"))
        TInsu = IIf(IsDBNull(dtIngEgre.Compute("Sum(deuda)", "taalm  = 1")), 0, dtIngEgre.Compute("Sum(deuda)", "taalm  = 1"))
        TSho = IIf(IsDBNull(dtIngEgre.Compute("Sum(deuda)", "taalm  = 10008 or taalm  = 100081 or taalm  = 2")), 0, dtIngEgre.Compute("Sum(deuda)", "taalm  = 10008 or taalm  = 100081 or taalm  = 2"))
        TOtSu = IIf(IsDBNull(dtIngEgre.Compute("Sum(deuda)", "taalm  = 10009 or taalm  = 100091")), 0, dtIngEgre.Compute("Sum(deuda)", "taalm  = 10009 or taalm  = 100091"))

        tbTConv.Text = TConv.ToString("0.00")
        tbTCont.Text = TCont.ToString("0.00")
        tbTRest.Text = TRest.ToString("0.00")
        tbTComb.Text = TComb.ToString("0.00")
        tbTInsu.Text = TInsu.ToString("0.00")
        tbTSho.Text = TSho.ToString("0.00")
        tbTOtSu.Text = TOtSu.ToString("0.00")



        RConv = IIf(IsDBNull(dtIngEgre.Compute("Sum(cobrar)+ Sum(amortizacion)", "taalm=10016 or taalm=100161")), 0, dtIngEgre.Compute("Sum(cobrar) + Sum(amortizacion)", "taalm=10016 or taalm=100161"))
        RCont = IIf(IsDBNull(dtIngEgre.Compute("Sum(cobrar)+ Sum(amortizacion)", "taalm  = 10001 or taalm  = 100011 or taalm  = 10002 or taalm  = 100021 or taalm  = 10003 or taalm  = 100031 or taalm  = 10004 or taalm  = 100041 or taalm=10011 or taalm=100111  or taalm=10012 or taalm=100121 or taalm=10013 or taalm=100131 or taalm=10015 or taalm=100151  or taalm=10006 or taalm=100061")), 0, dtIngEgre.Compute("Sum(cobrar) + Sum(amortizacion)", "taalm  = 10001 or taalm  = 100011 or taalm  = 10002 or taalm  = 100021 or taalm  = 10003 or taalm  = 100031 or taalm  = 10004 or taalm  = 100041 or taalm=10011 or taalm=100111  or taalm=10012 or taalm=100121 or taalm=10013 or taalm=100131 or taalm=10015 or taalm=100151  or taalm=10006 or taalm=100061"))
        RRest = IIf(IsDBNull(dtIngEgre.Compute("Sum(cobrar)+ Sum(amortizacion)", "taalm  = 10005 or taalm  = 100051")), 0, dtIngEgre.Compute("Sum(cobrar) + Sum(amortizacion)", "taalm  = 10005 or taalm  = 100051"))
        RComb = IIf(IsDBNull(dtIngEgre.Compute("Sum(cobrar) + Sum(amortizacion)", "taalm  = 10007 or taalm  = 100071 or taalm  = 3 or taalm  = 4")), 0, dtIngEgre.Compute("Sum(cobrar) + Sum(amortizacion)", "taalm  = 10007 or taalm  = 100071 or taalm  = 3 or taalm  = 4"))
        RInsu = IIf(IsDBNull(dtIngEgre.Compute("Sum(cobrar) + Sum(amortizacion)", "taalm  = 1")), 0, dtIngEgre.Compute("Sum(cobrar) + Sum(amortizacion)", "taalm  = 1"))
        RSho = IIf(IsDBNull(dtIngEgre.Compute("Sum(cobrar) + Sum(amortizacion)", "taalm  = 10008 or taalm  = 100081 or taalm  = 2")), 0, dtIngEgre.Compute("Sum(cobrar) + Sum(amortizacion)", "taalm  = 10008 or taalm  = 100081 or taalm  = 2"))
        ROtSu = IIf(IsDBNull(dtIngEgre.Compute("Sum(cobrar) + Sum(amortizacion)", "taalm  = 10009 or taalm  = 100091")), 0, dtIngEgre.Compute("Sum(cobrar) + Sum(amortizacion)", "taalm  = 10009 or  taalm  = 100091"))
        ROprevConv = IIf(IsDBNull(dtIngEgre.Compute("Sum(convenio) + Sum(amortizacion)", "taalm  = 10016 or taalm  = 100161")), 0, dtIngEgre.Compute("Sum(convenio) + Sum(amortizacion)", "taalm  = 10016 or taalm  = 100161"))

        tbRConv.Text = RConv.ToString("0.00")
        tbRCont.Text = RCont.ToString("0.00")
        tbRRest.Text = RRest.ToString("0.00")
        tbRComb.Text = RComb.ToString("0.00")
        tbRInsu.Text = RInsu.ToString("0.00")
        tbRSho.Text = RSho.ToString("0.00")
        tbROtSu.Text = ROtSu.ToString("0.00")
        tbrProvConv.Text = ROprevConv.ToString("0.00")


        tbTotalD.Text = (TComb + TConv + TCont + TInsu + TSho + TRest + TOtSu).ToString("0.00")
        tbTotalR.Text = (RComb + RConv + RCont + RInsu + RSho + RRest + ROtSu + ROprevConv).ToString("0.00")

    End Sub

    Private Sub CargarTotalQuincena(gestion As String, codCan As Integer)
        Dim dt1 As DataTable = cbQuincena.DataSource
        Dim dt As DataTable = L_fnCargarCupo(codCan, gestion, CStr(dt1.Rows(cbQuincena.Value - 1).Item("inicioQuin")), CStr(dt1.Rows(cbQuincena.Value - 1).Item("finQuin"))
)
        TextBoxX4.Text = dt.Rows(0).Item("cupo")
        TextBoxX5.Text = dt.Rows(0).Item("ingresado")
        TextBoxX7.Text = dt.Rows(0).Item("quincena")

    End Sub


    Private Sub grCanero_EditingCell(sender As Object, e As EditingCellEventArgs)
        'If (e.Column.Index = grCanero.RootTable.Columns("ydnumi").Index Or
        '      e.Column.Index = grCanero.RootTable.Columns("ydrazonsocial").Index) Then

        '    e.Cancel = True
        'Else
        '    e.Cancel = False
        'End If
        e.Cancel = True
    End Sub


#End Region



End Class