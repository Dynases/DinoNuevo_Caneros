Imports System.Drawing.Printing
Imports System.IO
Imports CrystalDecisions.Shared
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports Facturacion
Imports Janus.Windows.GridEX
Imports Logica.AccesoLogica


Public Class F0_PlanPagos

#Region "Variables Globales"
    Dim _CodCliente As Integer
    Dim _CodInstitucion As Integer
    Dim _IdPlanPago As Integer
#End Region

    Private Sub Habilitar()
        tbBanco.ReadOnly = False
        tbCodCan.ReadOnly = False
        tbcodInst.ReadOnly = False
        tbFechaD.Value = Date.Now
        tbMonto.ReadOnly = False
        tbOperacion.ReadOnly = False
        tbPlazo.ReadOnly = False
        cbMoneda.ReadOnly = False
        cbQuincena.ReadOnly = False
        cbTipoCambio.ReadOnly = False
        tbFechaD.Enabled = True
        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnGrabar.Enabled = True
        btnAnterior.Enabled = False
        btnPrimero.Enabled = False
        btnSiguiente.Enabled = False
        btnUltimo.Enabled = False
    End Sub
    Private Sub Inhabilitar()
        tbBanco.ReadOnly = True
        tbCodCan.ReadOnly = True
        tbcodInst.ReadOnly = True
        tbInstitucion.ReadOnly = True
        tbMonto.ReadOnly = True
        tbOperacion.ReadOnly = True
        tbPlazo.ReadOnly = True
        cbMoneda.ReadOnly = True
        cbQuincena.ReadOnly = True
        cbTipoCambio.ReadOnly = True
        TextBoxX1.ReadOnly = True
        btnNuevo.Enabled = True
        btnModificar.Enabled = True
        btnGrabar.Enabled = False
        tbFechaD.Enabled = False
        btnAnterior.Enabled = True
        btnPrimero.Enabled = True
        btnSiguiente.Enabled = True
        btnUltimo.Enabled = True
    End Sub

    Private Sub Limpiar()
        _CodCliente = 0
        _CodInstitucion = 0
        _IdPlanPago = 0
        tbBanco.Clear()
        tbCodCan.Clear()
        tbcodInst.Clear()
        tbFechaD.Value = Date.Now
        tbInstitucion.Clear()
        tbMonto.Clear()
        tbOperacion.Clear()
        tbPlazo.Clear()
        cbMoneda.Clear()
        cbQuincena.Clear()
        cbTipoCambio.Clear()
        TextBoxX1.Clear()
        ButtonX1.Visible = False
        Dim dt As DataTable = _CargarPagos(-1)
        grPagos.DataSource = dt
        dt = _CargarPlanPagos(-1)
        grPlanPagos.DataSource = dt
        dt = cargarGrupoCanero(-1)
        grGrupoEco.DataSource = dt

    End Sub
    Private Sub IniciarComponentes()
        _prCargarComboMoneda(cbMoneda)
        Inhabilitar()
        cargarRegistroPagos()
    End Sub

    Private Sub cargarRegistroPagos()
        Dim dt As DataTable = _fnCargarPagos()

        grPlandePagosC.DataSource = dt
        grPlandePagosC.RetrieveStructure()
        grPlandePagosC.AlternatingColors = True

        With grPlandePagosC.RootTable.Columns("tpnumi")
            .Width = 100
            .Caption = "Fecha"
            .Visible = False
        End With
        With grPlandePagosC.RootTable.Columns("tpcan")
            .Width = 100
            .Caption = "Fecha"
            .Visible = False
        End With
        With grPlandePagosC.RootTable.Columns("ydcod")
            .Width = 100
            .Caption = "Fecha"
            .Visible = False
        End With
        With grPlandePagosC.RootTable.Columns("ydrazonsocial")
            .Width = 100
            .Caption = "Fecha"
            .Visible = False
        End With
        With grPlandePagosC.RootTable.Columns("tpins")
            .Width = 100
            .Caption = "Fecha"
            .Visible = False
        End With
        With grPlandePagosC.RootTable.Columns("codInst")
            .Width = 100
            .Caption = "Fecha"
            .Visible = False
        End With
        With grPlandePagosC.RootTable.Columns("nomInst")
            .Width = 100
            .Caption = "Fecha"
            .Visible = False
        End With
        With grPlandePagosC.RootTable.Columns("tpban")
            .Width = 100
            .Caption = "Fecha"
            .Visible = False
        End With
        With grPlandePagosC.RootTable.Columns("tpmon")
            .Width = 100
            .Caption = "Fecha"
            .Visible = False
        End With
        With grPlandePagosC.RootTable.Columns("tbmonto")
            .Width = 100
            .Caption = "Fecha"
            .Visible = False
        End With
        With grPlandePagosC.RootTable.Columns("tbfecD")
            .Width = 100
            .Caption = "Fecha"
            .Visible = False
        End With
        With grPlandePagosC.RootTable.Columns("tpquin")
            .Width = 100
            .Caption = "Fecha"
            .Visible = False
        End With
        With grPlandePagosC.RootTable.Columns("tptcam")
            .Width = 100
            .Caption = "Fecha"
            .Visible = False
        End With
        With grPlandePagosC.RootTable.Columns("tppla")
            .Width = 100
            .Caption = "Fecha"
            .Visible = False
        End With
        With grPlandePagosC.RootTable.Columns("tpope")
            .Width = 100
            .Caption = "Fecha"
            .Visible = False
        End With

        With grPlandePagosC.RootTable.Columns("idPago")
            .Width = 100
            .Caption = "Fecha"
            .Visible = False
        End With
        If (dt.Rows.Count <= 0) Then
            CargarPlanes(0.1)
        End If
    End Sub
    Public Sub _prMostrarRegistro(_N As Integer)
        '' grVentas.Row = _N
        '     a.tanumi ,a.taalm ,a.tafdoc ,a.taven ,vendedor .yddesc as vendedor ,a.tatven ,a.tafvcr ,a.taclpr,
        'cliente.yddesc as cliente ,a.tamon ,IIF(tamon=1,'Boliviano','Dolar') as moneda,a.taest ,a.taobs ,
        'a.tadesc ,a.tafact ,a.tahact ,a.tauact,(Sum(b.tbptot)-a.tadesc ) as total,taproforma

        With grPlandePagosC
            _CodCliente = .GetValue("tpcan")
            _CodInstitucion = .GetValue("tpins")
            _IdPlanPago = .GetValue("idPago")
            TextBoxX1.Text = .GetValue("ydrazonsocial")
            tbBanco.Text = .GetValue("tpban")
            tbCodCan.Text = .GetValue("ydcod")
            tbcodInst.Text = .GetValue("codInst")
            tbFechaD.Value = .GetValue("tbfecD")
            tbInstitucion.Text = .GetValue("nomInst")
            tbMonto.Text = .GetValue("tbmonto")
            tbOperacion.Text = .GetValue("tpope")
            tbPlazo.Text = .GetValue("tppla")
            cbMoneda.Value = .GetValue("tpmon")
            cbQuincena.Value = .GetValue("tpquin")
            cbTipoCambio.Value = .GetValue("tptcam")

            CargarPlanes2(.GetValue("tpnumi"))
            _prCargarGrupoEco(_CodCliente)
            cargarPagos2(_IdPlanPago)
            LblPaginacion.Text = Str(grPlandePagosC.Row + 1) + "/" + grPlandePagosC.RowCount.ToString
        End With
    End Sub

    Private Sub _prCargarComboMoneda(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnGeneralMoneda()
        Dim fila = dt.NewRow()
        fila(0) = 0
        fila(1) = "SELECCIONE MODENA"
        dt.Rows.InsertAt(fila, 0)
        'a.ylcod1 ,a.yldes1 
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("yccod3").Width = 70
            .DropDownList.Columns("yccod3").Caption = "COD"
            .DropDownList.Columns.Add("ycdes3").Width = mCombo.Width - 70
            .DropDownList.Columns("ycdes3").Caption = "DESCRIPCION"
            .ValueMember = "yccod3"
            .DisplayMember = "ycdes3"
            .DataSource = dt
            .Refresh()
        End With
        If (CType(cbMoneda.DataSource, DataTable).Rows.Count > 0) Then
            cbMoneda.SelectedIndex = 0
        End If
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
        With grGrupoEco
            .ColumnAutoResize = True
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With
    End Sub
    Private Sub TextBoxX1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxX1.KeyDown
        If Accesible() Then
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
                    tbCodCan.Text = Row.Cells("ydcod").Value
                    TextBoxX1.Text = Row.Cells("ydrazonsocial").Value

                    Dim numiVendedor As Integer = IIf(IsDBNull(Row.Cells("ydnumivend").Value), 0, Row.Cells("ydnumivend").Value)
                    If (numiVendedor > 0) Then
                        ''tbVendedor.Text = Row.Cells("vendedor").Value
                        _CodInstitucion = Row.Cells("ydnumivend").Value
                        Dim dt1 As DataTable = L_fnListarCaneroInstitucion(_CodCliente)
                        tbInstitucion.Text = dt1.Rows(0).Item("institucion")
                        tbcodInst.Text = dt1.Rows(0).Item("codInst")
                        'grdetalle.Select()
                        'Table_Producto = Nothing

                    End If

                    _prCargarGrupoEco(_CodCliente)
                End If
            End If
        End If
    End Sub
    Public Function _fnSiguienteNumi()
        Dim dt As DataTable = CType(grPlanPagos.DataSource, DataTable)
        Dim rows() As DataRow = dt.Select("tpcuota=MAX(tpcuota)")
        If (rows.Count > 0) Then
            Return rows(rows.Count - 1).Item("tpcuota")
        End If
        Return 0
    End Function
    Private Sub _prAddDetalleVenta()
        '   a.tbnumi ,a.tbtv1numi ,a.tbty5prod ,b.yfcdprod1 as producto,a.tbest ,a.tbcmin ,a.tbumin ,Umin .ycdes3 as unidad,a.tbpbas ,a.tbptot ,a.tbobs ,
        'a.tbpcos,a.tblote ,a.tbfechaVenc , a.tbptot2, a.tbfact ,a.tbhact ,a.tbuact,1 as estado,Cast(null as Image) as img
        Dim Bin As New MemoryStream
        Dim img As New Bitmap(My.Resources.delete, 28, 28)
        img.Save(Bin, Imaging.ImageFormat.Png)
        CType(grPlanPagos.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, "", 0, 0, 0, 0)
    End Sub

    Private Sub CargarPlanes(cod As Integer)
        Dim dt As DataTable = _CargarPlanPagos(cod)

        'grPlanPagos.BoundMode = BoundMode.Unbound
        grPlanPagos.DataSource = dt
        grPlanPagos.RetrieveStructure()
        grPlanPagos.AlternatingColors = True

        'b.yfcdprod1 ,a.iclot ,a.icfven  ,a.iccven 

        With grPlanPagos.RootTable.Columns("tpcuota")
            .Width = 150
            .Caption = "CUOTA"
            .Visible = True
            .FormatString = "0"

        End With
        With grPlanPagos.RootTable.Columns("tpfecha")
            .Width = 160
            .Caption = "FECHA"
            .Visible = True
            .EditType = EditType.CalendarCombo
            '.DataTypeCode = TypeCode.DateTime.ToString("dd/MM/yyyy")
            .DefaultValue = Date.Today
            .FormatString = "dd/MM/yyyy"
        End With

        With grPlanPagos.RootTable.Columns("tpsaldo")
            .Width = 150
            .Visible = True
            .Caption = "CAPITAL"
            .FormatString = "0.00"
            '.AggregateFunction = AggregateFunction.Sum
        End With
        With grPlanPagos.RootTable.Columns("tpamort")
            .Width = 150
            .Visible = True
            .Caption = "AMORTIZACION"
            .FormatString = "0.00"
        End With
        With grPlanPagos.RootTable.Columns("tpinteres")
            .Width = 150
            .Visible = True
            .Caption = "INTERES"
            .FormatString = "0.00"
        End With
        With grPlanPagos.RootTable.Columns("tptotal")
            .Width = 150
            .Visible = True
            .Caption = "TOTAL"
            .FormatString = "0.00"
        End With
        With grPlanPagos
            '.DefaultFilterRowComparison = FilterConditionOperator.Contains
            '.FilterMode = FilterMode.Automatic
            '.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            '.TotalRow = InheritableBoolean.True
            '.TotalRowFormatStyle.BackColor = Color.Gold
            '.TotalRowPosition = TotalRowPosition.BottomFixed
            .VisualStyle = VisualStyle.Office2007
        End With

        If dt.Rows.Count = 0 Then
            'grPlanPagos.DataSource = dt
            LimpiarCampos()
            _prAddDetalleVenta()
            ButtonX1.Visible = True
        Else
            CargarRegistro()
            cargarPagos()
            ButtonX1.Visible = False
        End If
    End Sub

    Private Sub LimpiarCampos()
        tbBanco.Clear()
        tbcodInst.Clear()
        tbFechaD.Value = Date.Now
        tbMonto.Clear()
        tbOperacion.Clear()
        tbPlazo.Clear()
        cbMoneda.Clear()
        cbQuincena.Clear()
        cbTipoCambio.Clear()
        TextBoxX1.Clear()
        ButtonX1.Visible = False
        Dim dt As DataTable = _CargarPagos(-1)
        grPagos.DataSource = dt
        dt = _CargarPlanPagos(-1)
        grPlanPagos.DataSource = dt

    End Sub
    Private Sub CargarPlanes2(cod As Integer)
        Dim dt As DataTable = _CargarPlanPagos2(cod)

        'grPlanPagos.BoundMode = BoundMode.Unbound
        grPlanPagos.DataSource = dt
        grPlanPagos.RetrieveStructure()
        grPlanPagos.AlternatingColors = True

        'b.yfcdprod1 ,a.iclot ,a.icfven  ,a.iccven 

        With grPlanPagos.RootTable.Columns("tpcuota")
            .Width = 150
            .Caption = "CUOTA"
            .Visible = True
            .FormatString = "0"

        End With
        With grPlanPagos.RootTable.Columns("tpfecha")
            .Width = 160
            .Caption = "FECHA"
            .Visible = True
            .EditType = EditType.CalendarCombo
            '.DataTypeCode = TypeCode.DateTime.ToString("dd/MM/yyyy")
            .DefaultValue = Date.Today
            .FormatString = "dd/MM/yyyy"
        End With

        With grPlanPagos.RootTable.Columns("tpsaldo")
            .Width = 150
            .Visible = True
            .Caption = "CAPITAL"
            .FormatString = "0.00"
            '.AggregateFunction = AggregateFunction.Sum
        End With
        With grPlanPagos.RootTable.Columns("tpamort")
            .Width = 150
            .Visible = True
            .Caption = "AMORTIZACION"
            .FormatString = "0.00"
        End With
        With grPlanPagos.RootTable.Columns("tpinteres")
            .Width = 150
            .Visible = True
            .Caption = "INTERES"
            .FormatString = "0.00"
        End With
        With grPlanPagos.RootTable.Columns("tptotal")
            .Width = 150
            .Visible = True
            .Caption = "TOTAL"
            .FormatString = "0.00"
        End With
        With grPlanPagos
            '.DefaultFilterRowComparison = FilterConditionOperator.Contains
            '.FilterMode = FilterMode.Automatic
            '.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            '.TotalRow = InheritableBoolean.True
            '.TotalRowFormatStyle.BackColor = Color.Gold
            '.TotalRowPosition = TotalRowPosition.BottomFixed
            .VisualStyle = VisualStyle.Office2007
        End With
    End Sub

    Private Sub CargarRegistro()
        Dim dt As DataTable = CargarRegistroPlanPago(_CodCliente)
        With dt.Rows(0)
            tbBanco.Text = .Item("tpban")
            tbMonto.Text = .Item("tbmonto")
            tbFechaD.Value = .Item("tbfecd")
            tbOperacion.Text = .Item("tpope")
            tbPlazo.Text = .Item("tppla")
            _IdPlanPago = .Item("tpnumi")
            cbMoneda.Value = .Item("tpmon")
            cbQuincena.Value = .Item("tpquin")
            cbTipoCambio.Value = .Item("tptcam")
        End With


    End Sub
    Private Sub ConvertirSus(ByRef dt As DataTable)
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            dt.Rows(i).Item("tpsaldo") = dt.Rows(i).Item("tpsaldo") / CDbl(cbTipoCambio.Text)
            dt.Rows(i).Item("tpamort") = dt.Rows(i).Item("tpamort") / CDbl(cbTipoCambio.Text)
            dt.Rows(i).Item("tpinteres") = dt.Rows(i).Item("tpinteres") / CDbl(cbTipoCambio.Text)
            dt.Rows(i).Item("tptotal") = dt.Rows(i).Item("tptotal") / CDbl(cbTipoCambio.Text)
            dt.Rows(i).Item("saldo2") = dt.Rows(i).Item("tptotal") - dt.Rows(i).Item("saldo2")
            dt.Rows(i).Item("saldo") = dt.Rows(i).Item("saldo2")
        Next
    End Sub

    Private Sub ConvertirSus2(ByRef dt As DataTable)
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            dt.Rows(i).Item("tpsaldo") = dt.Rows(i).Item("tpsaldo") / CDbl(cbTipoCambio.Text)
            dt.Rows(i).Item("tpamort") = dt.Rows(i).Item("tpamort") / CDbl(cbTipoCambio.Text)
            dt.Rows(i).Item("tpinteres") = dt.Rows(i).Item("tpinteres") / CDbl(cbTipoCambio.Text)
            dt.Rows(i).Item("tptotal") = dt.Rows(i).Item("tptotal") / CDbl(cbTipoCambio.Text)
        Next
    End Sub

    Private Sub cargarPagos()
        Dim dt As DataTable = _CargarPagos(_CodCliente)
        ConvertirSus(dt)
        grPagos.DataSource = dt
        grPagos.RetrieveStructure()
        grPagos.AlternatingColors = True

        With grPagos.RootTable.Columns("ydcod")
            .Width = 150
            .Caption = "COD. SOCIO"
            .Visible = True
            .FormatString = "0"

        End With
        With grPagos.RootTable.Columns("tpcuota")
            .Width = 150
            .Caption = "CUOTA"
            .Visible = True
            .FormatString = "0"

        End With
        With grPagos.RootTable.Columns("tpfecha")
            .Width = 160
            .Caption = "FECHA"
            .FormatString = "yyyy/MM/dd"
            .Visible = True
        End With

        With grPagos.RootTable.Columns("tpsaldo")
            .Width = 150
            .Visible = True
            .Caption = "CAPITAL"
            .FormatString = "0.00"
            '.AggregateFunction = AggregateFunction.Sum
        End With
        With grPagos.RootTable.Columns("tpamort")
            .Width = 150
            .Visible = True
            .Caption = "AMORTIZACION"
            .FormatString = "0.00"
        End With
        With grPagos.RootTable.Columns("tpinteres")
            .Width = 150
            .Visible = True
            .Caption = "INTERES"
            .FormatString = "0.00"
        End With
        With grPagos.RootTable.Columns("tptotal")
            .Width = 150
            .Visible = True
            .Caption = "TOTAL"
            .FormatString = "0.00"
        End With
        With grPagos.RootTable.Columns("estado")
            .Width = 150
            .Caption = "ESTADO"
            .Visible = True
            .FormatString = "0.00"

        End With
        With grPagos.RootTable.Columns("amortizacion")
            .Width = 150
            .Caption = "AMORTIZACION"
            .Visible = True
            .FormatString = "0.00"
        End With
        With grPagos.RootTable.Columns("aporte")
            .Width = 150
            .Caption = "APORTE"
            .Visible = True
            .FormatString = "0.00"

        End With
        With grPagos.RootTable.Columns("total")
            .Width = 150
            .Caption = "TOTAL"
            .Visible = True
            .FormatString = "0.00"

        End With
        With grPagos.RootTable.Columns("saldo")
            .Width = 150
            .Caption = "SALDO"
            .Visible = True
            .FormatString = "0.00"

        End With
        With grPagos.RootTable.Columns("saldo2")
            .Width = 150
            .Caption = "SALDO"
            .Visible = False
            .FormatString = "0.00"

        End With
        With grPagos
            '.DefaultFilterRowComparison = FilterConditionOperator.Contains
            '.FilterMode = FilterMode.Automatic
            '.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            '.TotalRow = InheritableBoolean.True
            '.TotalRowFormatStyle.BackColor = Color.Gold
            '.TotalRowPosition = TotalRowPosition.BottomFixed
            .VisualStyle = VisualStyle.Office2007
        End With
    End Sub

    Private Sub cargarPagos2(cod As Integer)
        Dim dt As DataTable = _CargarPagos2(cod)
        ConvertirSus2(dt)
        grPagos.DataSource = dt
        grPagos.RetrieveStructure()
        grPagos.AlternatingColors = True

        With grPagos.RootTable.Columns("ydcod")
            .Width = 150
            .Caption = "COD. SOCIO"
            .Visible = True
            .FormatString = "0"

        End With
        With grPagos.RootTable.Columns("tpcuota")
            .Width = 150
            .Caption = "CUOTA"
            .Visible = True
            .FormatString = "0"

        End With
        With grPagos.RootTable.Columns("tpfecha")
            .Width = 160
            .Caption = "FECHA"
            .FormatString = "yyyy/MM/dd"
            .Visible = True
        End With

        With grPagos.RootTable.Columns("tpsaldo")
            .Width = 150
            .Visible = True
            .Caption = "CAPITAL"
            .FormatString = "0.00"
            '.AggregateFunction = AggregateFunction.Sum
        End With
        With grPagos.RootTable.Columns("tpamort")
            .Width = 150
            .Visible = True
            .Caption = "AMORTIZACION"
            .FormatString = "0.00"
        End With
        With grPagos.RootTable.Columns("tpinteres")
            .Width = 150
            .Visible = True
            .Caption = "INTERES"
            .FormatString = "0.00"
        End With
        With grPagos.RootTable.Columns("tptotal")
            .Width = 150
            .Visible = True
            .Caption = "TOTAL"
            .FormatString = "0.00"
        End With
        With grPagos.RootTable.Columns("estado")
            .Width = 150
            .Caption = "ESTADO"
            .Visible = True
            .FormatString = "0.00"

        End With
        With grPagos.RootTable.Columns("amortizacion")
            .Width = 150
            .Caption = "AMORTIZACION"
            .Visible = True
            .FormatString = "0.00"
        End With
        With grPagos.RootTable.Columns("aporte")
            .Width = 150
            .Caption = "APORTE"
            .Visible = True
            .FormatString = "0.00"

        End With
        With grPagos.RootTable.Columns("total")
            .Width = 150
            .Caption = "TOTAL"
            .Visible = True
            .FormatString = "0.00"

        End With
        With grPagos.RootTable.Columns("saldo")
            .Width = 150
            .Caption = "SALDO"
            .Visible = True
            .FormatString = "0.00"

        End With
        With grPagos.RootTable.Columns("saldo2")
            .Width = 150
            .Caption = "SALDO"
            .Visible = False
            .FormatString = "0.00"

        End With
        With grPagos
            '.DefaultFilterRowComparison = FilterConditionOperator.Contains
            '.FilterMode = FilterMode.Automatic
            '.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            '.TotalRow = InheritableBoolean.True
            '.TotalRowFormatStyle.BackColor = Color.Gold
            '.TotalRowPosition = TotalRowPosition.BottomFixed
            .VisualStyle = VisualStyle.Office2007
        End With
    End Sub
    Private Sub tbCodCan_TextChanged(sender As Object, e As EventArgs) Handles tbCodCan.TextChanged
        CargarPlanes(_CodCliente)
        cargarPagos()
    End Sub

    Private Sub grPlanPagos_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grPlanPagos.EditingCell
        If Accesible() Then
            If (e.Column.Index = grPlanPagos.RootTable.Columns("tpfecha").Index Or
              e.Column.Index = grPlanPagos.RootTable.Columns("tpsaldo").Index Or
              e.Column.Index = grPlanPagos.RootTable.Columns("tpamort").Index Or
              e.Column.Index = grPlanPagos.RootTable.Columns("tpinteres").Index) Then

                e.Cancel = False
            End If
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub grPlanPagos_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles grPlanPagos.CellValueChanged
        If (e.Column.Index = grPlanPagos.RootTable.Columns("tpfecha").Index) Then
            If grPlanPagos.GetValue("tpfecha").ToString <> String.Empty Then

            End If
        End If
        If (e.Column.Index = grPlanPagos.RootTable.Columns("tpsaldo").Index) Or (e.Column.Index = grPlanPagos.RootTable.Columns("tpamort").Index) Or (e.Column.Index = grPlanPagos.RootTable.Columns("tpinteres").Index) Then
            If (Not IsNumeric(grPlanPagos.GetValue("tpsaldo")) Or grPlanPagos.GetValue("tpsaldo").ToString = String.Empty) Then
                grPlanPagos.SetValue("tpsaldo", 0)
            End If
            If (Not IsNumeric(grPlanPagos.GetValue("tpamort")) Or grPlanPagos.GetValue("tpamort").ToString = String.Empty) Then
                grPlanPagos.SetValue("tpamort", 0)
            End If
            If (Not IsNumeric(grPlanPagos.GetValue("tpinteres")) Or grPlanPagos.GetValue("tpinteres").ToString = String.Empty) Then
                grPlanPagos.SetValue("tpinteres", 0)
            End If
            grPlanPagos.SetValue("tptotal", grPlanPagos.GetValue("tpamort") + grPlanPagos.GetValue("tpinteres"))

        End If
    End Sub

    Private Sub grPlanPagos_KeyDown(sender As Object, e As KeyEventArgs) Handles grPlanPagos.KeyDown
        If (e.KeyData = Keys.Enter And grPlanPagos.Row >= 0) Then 'And
            '            grdetalle.Col = grdetalle.RootTable.Columns("producto").Index) Then
            '            Dim indexfil As Integer = grdetalle.Row
            '            Dim indexcol As Integer = grdetalle.Col
            '            _HabilitarProductos()
            If grPlanPagos.GetValue("tpsaldo") <> 0 Then
                If grPlanPagos.GetValue("tpamort") <> 0 Then
                    If grPlanPagos.GetValue("tpinteres") <> 0 Then
                        If grPlanPagos.RowCount - 1 = grPlanPagos.CurrentRow.RowIndex Then
                            _prAddDetalleVenta()
                            grPlanPagos.Col = grPlanPagos.RootTable.Columns("tpsaldo").Index
                        End If

                    Else
                        grPlanPagos.Col = grPlanPagos.RootTable.Columns("tpinteres").Index
                    End If
                Else
                    grPlanPagos.Col = grPlanPagos.RootTable.Columns("tpamort").Index

                End If
            Else
                grPlanPagos.Col = grPlanPagos.RootTable.Columns("tpsaldo").Index
            End If
        End If
    End Sub
    Private Sub GuardarPlanPagos()
        For i As Integer = 0 To grPlanPagos.RowCount - 1 Step 1
            Dim a As String = CType(grPlanPagos.DataSource, DataTable).Rows(i).Item("tpfecha").ToString
            CType(grPlanPagos.DataSource, DataTable).Rows(i).Item("tpfecha") = Format(CDate(a), "dd/MM/yyyy")
        Next
        Dim res As Boolean = _fnGuargarPlandePagos(_CodCliente, _CodInstitucion, tbBanco.Text, tbFechaD.Value.ToString("dd/MM/yyyy"), cbMoneda.Value, CDbl(tbMonto.Text), tbOperacion.Text, tbPlazo.Text, cbQuincena.Value, cbTipoCambio.Value, CType(grPlanPagos.DataSource, DataTable))

        If res Then
            'SI se agrego
            CargarPlanes(_CodCliente)
        End If
    End Sub
    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        If grPlanPagos.RowCount > 1 Then
            GuardarPlanPagos()

        End If
    End Sub

    Private Sub F0_PlanPagos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IniciarComponentes()
    End Sub

    Private Sub TextBoxX2_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub MultiColumnCombo2_ValueChanged(sender As Object, e As EventArgs) Handles cbTipoCambio.ValueChanged

    End Sub

    Private Sub MultiColumnCombo1_ValueChanged(sender As Object, e As EventArgs) Handles cbQuincena.ValueChanged

    End Sub

    Private Sub LabelX8_Click(sender As Object, e As EventArgs) Handles LabelX8.Click

    End Sub

    Private Sub _prCargarComboTipoCambio(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnGeneralTipoCambio()
        Dim fila = dt.NewRow()
        fila(0) = 0
        fila(1) = "SELECCIONE TIPO CAMBIO"
        dt.Rows.InsertAt(fila, 0)
        'a.ylcod1 ,a.yldes1 
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("yccod3").Width = 70
            .DropDownList.Columns("yccod3").Caption = "COD"
            .DropDownList.Columns.Add("ycdes3").Width = mCombo.Width - 70
            .DropDownList.Columns("ycdes3").Caption = "DESCRIPCION"
            .ValueMember = "yccod3"
            .DisplayMember = "ycdes3"
            .DataSource = dt
            .Refresh()
        End With
        If (CType(cbTipoCambio.DataSource, DataTable).Rows.Count > 0) Then
            cbTipoCambio.SelectedIndex = 0
        End If
    End Sub
    Private Sub cbMoneda_ValueChanged(sender As Object, e As EventArgs) Handles cbMoneda.ValueChanged
        ' codMon.Text = tbMoneda.Value
        If cbMoneda.Value = 2 Then
            cbTipoCambio.Enabled = False
        Else
            If cbMoneda.Value = 1 Then
                cbTipoCambio.Enabled = True
                _prCargarComboTipoCambio(cbTipoCambio)
            End If

        End If
    End Sub

    Private Function Accesible() As Boolean
        If btnNuevo.Enabled = True Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Sub grPagos_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grPagos.EditingCell
        If Accesible() Then
            If (e.Column.Index = grPagos.RootTable.Columns("amortizacion").Index Or
              e.Column.Index = grPagos.RootTable.Columns("aporte").Index) Then

                e.Cancel = False
            End If
        Else
                e.Cancel = True

        End If
    End Sub

    Private Sub grPagos_FormattingRow(sender As Object, e As RowLoadEventArgs) Handles grPagos.FormattingRow

    End Sub

    Private Sub grPagos_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles grPagos.CellValueChanged
        If (e.Column.Index = grPagos.RootTable.Columns("amortizacion").Index) Or (e.Column.Index = grPagos.RootTable.Columns("aporte").Index) Then
            If (Not IsNumeric(grPagos.GetValue("amortizacion")) Or grPagos.GetValue("amortizacion").ToString = String.Empty) Then
                grPagos.SetValue("amortizacion", 0)
            End If
            If (Not IsNumeric(grPagos.GetValue("aporte")) Or grPagos.GetValue("aporte").ToString = String.Empty) Then
                grPagos.SetValue("aporte", 0)
            End If
            grPagos.SetValue("total", grPagos.GetValue("amortizacion") + grPagos.GetValue("aporte"))
            grPagos.SetValue("saldo", grPagos.GetValue("saldo2") - grPagos.GetValue("total"))

        End If
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        Limpiar()
        Habilitar()
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click

    End Sub

    Private Sub _Guardar()
        Dim res As Boolean = _fnGuargarPagos(_IdPlanPago, CType(grPagos.DataSource, DataTable))
        If res Then
            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Cobro Grabado con Exito.".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter
                                          )
            Limpiar()
        End If
    End Sub
    Private Function ValidarDatos() As Boolean
        For i As Integer = 0 To grPagos.RowCount - 1 Step 1
            'If CType(grPagos.DataSource, DataTable).Rows(i).Item("amortizacion")  0 Then
            '    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            '    ToastNotification.Show(Me, "Por Favor Seleccione un Cliente con Ctrl+Enter".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            '    Return False
            'End If
            If CType(grPagos.DataSource, DataTable).Rows(i).Item("aporte") <= 0 Then

                Return False
            End If
        Next
    End Function
    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        'If ValidarDatos() Then
        '    Exit Sub
        'End If
        _Guardar()
    End Sub

    Private Sub grPlanPagos_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grPlanPagos.CellEdited
        If e.Column.Index = grPlanPagos.RootTable.Columns("tpfecha").Index Then
            Dim a As String = grPlanPagos.GetValue("tpfecha")
            a = Format(CDate(a), "dd/MM/yyyy")
            grPlanPagos.SetValue("tpfecha", a)
        End If

    End Sub

    Private Sub grPlandePagosC_SelectionChanged(sender As Object, e As EventArgs) Handles grPlandePagosC.SelectionChanged
        If (grPlandePagosC.RowCount >= 0 And grPlandePagosC.Row >= 0) Then
            _prMostrarRegistro(grPlandePagosC.Row)
        End If
    End Sub

    Private Sub grGrupoEco_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grGrupoEco.EditingCell
        e.Cancel = True
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        If btnNuevo.Enabled = True Then
            Me.Close()
        Else
            Inhabilitar()
            Limpiar()
            cargarRegistroPagos()
        End If
    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        Dim _pos As Integer = grPlandePagosC.Row
        If _pos < grPlandePagosC.RowCount - 1 And _pos >= 0 Then
            _pos = grPlandePagosC.Row + 1
            '' _prMostrarRegistro(_pos)
            grPlandePagosC.Row = _pos
        End If
    End Sub

    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        Dim _pos As Integer = grPlandePagosC.Row
        If grPlandePagosC.RowCount > 0 Then
            _pos = grPlandePagosC.RowCount - 1
            ''  _prMostrarRegistro(_pos)
            grPlandePagosC.Row = _pos
        End If
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        Dim _MPos As Integer = grPlandePagosC.Row
        If _MPos > 0 And grPlandePagosC.RowCount > 0 Then
            _MPos = _MPos - 1
            ''  _prMostrarRegistro(_MPos)
            grPlandePagosC.Row = _MPos
        End If
    End Sub

    Private Sub btnPrimero_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click
        Dim _MPos As Integer
        If grPlandePagosC.RowCount > 0 Then
            _MPos = 0
            ''   _prMostrarRegistro(_MPos)
            grPlandePagosC.Row = _MPos
        End If
    End Sub
End Class