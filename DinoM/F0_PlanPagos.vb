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
#End Region

    Private Sub IniciarComponentes()
        _prCargarComboMoneda(cbMoneda)
    End Sub
    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub TextBoxX7_TextChanged(sender As Object, e As EventArgs) Handles tbPlazo.TextChanged

    End Sub

    Private Sub TextBoxX1_TextChanged(sender As Object, e As EventArgs) Handles TextBoxX1.TextChanged

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
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With
    End Sub
    Private Sub TextBoxX1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxX1.KeyDown
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

    Private Sub CargarPlanes()
        Dim dt As DataTable = _CargarPlanPagos(_CodCliente)

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
            .FormatString = "yyyy/MM/dd"
            .Visible = True
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
            grPlanPagos.DataSource = dt
            _prAddDetalleVenta()
            ButtonX1.Visible = True
        Else
            cargarPagos()
            ButtonX1.Visible = False
        End If
    End Sub

    Private Sub cargarPagos()
        Dim dt As DataTable = _CargarPagos(_CodCliente)

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
        CargarPlanes()
        cargarPagos()
    End Sub

    Private Sub grPlanPagos_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grPlanPagos.EditingCell
        If (e.Column.Index = grPlanPagos.RootTable.Columns("tpfecha").Index Or
              e.Column.Index = grPlanPagos.RootTable.Columns("tpsaldo").Index Or
              e.Column.Index = grPlanPagos.RootTable.Columns("tpamort").Index Or
              e.Column.Index = grPlanPagos.RootTable.Columns("tpinteres").Index) Then

            e.Cancel = False
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub grPlanPagos_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles grPlanPagos.CellValueChanged
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
        Dim res As Boolean = _fnGuargarPlandePagos(_CodCliente, _CodInstitucion, tbBanco.Text, tbFechaD.Value.ToString("dd/MM/yyyy"), cbMoneda.Value, CDbl(tbMonto.Text), tbOperacion.Text, tbPlazo.Text, CType(grPlanPagos.DataSource, DataTable))

        If res Then
            'SI se agrego
            CargarPlanes()
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
End Class