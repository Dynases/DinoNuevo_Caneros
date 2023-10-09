Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar.Controls
Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX


Public Class F1_Liquidaciones

    Dim _CodCliente, _CodInstitucion As Integer

    Private Sub _prCargarComboGestion(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarGestiones()
        With mCombo
            .DropDownList.Columns.Clear()

            .DropDownList.Columns.Add("gestion").Width = 60
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
    Private Sub tbCanero_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCanero.KeyDown
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
                tbCod.Text = Row.Cells("ydcod").Value
                _CodCliente = Row.Cells("ydnumi").Value
                tbCanero.Text = Row.Cells("ydrazonsocial").Value


            End If

        End If
    End Sub

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        Dim dt As DataTable
        If SwitchButton1.Value = False Then
            dt = L_fnCargarLiquidacion(IIf(tbCod.Text = "", -1, _CodCliente), tbFecha.Value.ToString("dd-MM-yyyy"))
            _prCargarVenta1(dt)
        Else
            dt = L_fnCargarLiquidacionGuardar(cbQuincena.Value, cbGestion.Value)
            _prCargarVenta1(dt)
        End If
    End Sub

    Private Sub _prCargarVenta1(dt As DataTable)


        JGrM_Buscador.DataSource = dt
        JGrM_Buscador.RetrieveStructure()
        JGrM_Buscador.AlternatingColors = True
        '   a.tamon ,IIF(tamon=1,'Boliviano','Dolar') as moneda,a.taest ,a.taobs ,
        'a.tadesc ,a.tafact ,a.tahact ,a.tauact,(Sum(b.tbptot)-a.tadesc ) as total

        With JGrM_Buscador.RootTable.Columns("trid")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = True

        End With

        With JGrM_Buscador.RootTable.Columns("ydnumi")
            .Width = 90
            .Visible = False
        End With
        With JGrM_Buscador.RootTable.Columns("ydcod")
            .Width = 50
            .Visible = True
            .Caption = "Cod. Can."
        End With
        With JGrM_Buscador.RootTable.Columns("ydrazonsocial")
            .Width = 180
            .Visible = True
            .Caption = "Cañero"
        End With
        With JGrM_Buscador.RootTable.Columns("id")
            .Width = 90
            .Visible = False
        End With
        With JGrM_Buscador.RootTable.Columns("codInst")
            .Width = 50
            .Visible = True
            .Caption = "Cod. Inst."
        End With
        With JGrM_Buscador.RootTable.Columns("nomInst")
            .Width = 180
            .Visible = True
            .Caption = "Institucion"
        End With

        With JGrM_Buscador.RootTable.Columns("doc")
            .Width = 90
            .Visible = False
        End With
        With JGrM_Buscador.RootTable.Columns("alm")
            .Width = 250
            .Visible = True
            .Caption = "Tipo de Deuda"
        End With
        With JGrM_Buscador.RootTable.Columns("comb")
            .Width = 120
            .Visible = True
            .Caption = "Combustible"
            .FormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum

        End With

        With JGrM_Buscador.RootTable.Columns("insumos")
            .Width = 120
            .Visible = True
            .Caption = "Insumos"
            .FormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum

        End With

        With JGrM_Buscador.RootTable.Columns("Rest")
            .Width = 120
            .Visible = True
            .Caption = "Restructuracion"
            .FormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum

        End With
        With JGrM_Buscador.RootTable.Columns("otros")
            .Width = 120
            .Visible = True
            .Caption = "Otros Prestamos"
            .FormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum

        End With
        With JGrM_Buscador.RootTable.Columns("convenio")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "P. Convenio"
            .FormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum

        End With

        With JGrM_Buscador
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla

            .RowHeaders = InheritableBoolean.True
            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed
        End With


    End Sub

    Private Sub F1_Liquidaciones_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        tbFecha.Value = Date.Now
        _prCargarComboGestion(cbGestion)
        _prCargarQuincena(cbQuincena)
        btnGrabar.Visible = False
        btnGrabar.Enabled = False
        ButtonX1.Enabled = True

    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub SwitchButton1_ValueChanged(sender As Object, e As EventArgs) Handles SwitchButton1.ValueChanged
        If SwitchButton1.Value = False Then
            cbQuincena.Enabled = False
            cbGestion.Enabled = False
            btnGrabar.Enabled = False
            ButtonX1.Enabled = True
            tbCanero.Enabled = True
            tbInstitucion.Enabled = True
            tbFecha.Enabled = True
        Else
            cbQuincena.Enabled = True
            cbGestion.Enabled = True
            btnGrabar.Enabled = True
            ButtonX1.Enabled = True
            tbCanero.Enabled = False
            tbInstitucion.Enabled = False
            tbFecha.Enabled = False
        End If
    End Sub
    Private Function ValidarCampos() As Boolean
        If cbQuincena.Text = "" Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Seleccione una Quincena".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            cbGestion.Focus()
            Return False
        End If
        If cbGestion.Text = "" Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Seleccione una Gestion".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            cbGestion.Focus()
            Return False
        End If
        If JGrM_Buscador.RowCount = 0 Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "NO HAY DATOS PARA GUARDAR".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return False
        End If
        Return True
    End Function

    Private Sub GuardarNuevo()
        L_fnAutorizarRetencion(CType(JGrM_Buscador.DataSource, DataTable))
        Dim img As Bitmap = New Bitmap(My.Resources.check_mark, 50, 50)
        ToastNotification.Show(Me, "Guardado con exito".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.TopCenter)

        ButtonX1.PerformClick()

        tbCanero.Clear()
        tbInstitucion.Clear()
        tbFecha.Value = Date.Now
    End Sub
    Public Overrides Function _PMOGetTablaBuscador() As DataTable
        Dim dtBuscador As DataTable
        If SwitchButton1.Value = False Then
            dtBuscador = L_fnCargarLiquidacion(IIf(tbCod.Text = "", -1, _CodCliente), tbFecha.Value.ToString("dd-MM-yyyy"))
        Else
            dtBuscador = L_fnCargarLiquidacionGuardar(cbQuincena.Value, cbGestion.Value)
        End If
        Return dtBuscador
    End Function

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        Guardar()
    End Sub
    Private Sub Guardar()
        If ValidarCampos() = False Then
            Exit Sub
        Else
            GuardarNuevo()
        End If
    End Sub


    Private Sub cbGestion_ValueChanged(sender As Object, e As EventArgs) Handles cbGestion.ValueChanged
        _prCargarQuincena(cbQuincena)
    End Sub

    Private Sub ButtonX2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ButtonX2_Click_1(sender As Object, e As EventArgs) Handles ButtonX2.Click
        Guardar()
    End Sub

    Private Sub tbCanero_TextChanged(sender As Object, e As EventArgs) Handles tbCanero.TextChanged
        Dim dt As DataTable
        dt = L_fnListarCaneroInstitucion(_CodCliente)
        Dim row As DataRow = dt.Rows(dt.Rows.Count - 1)
        tbInstitucion.Text = row("institucion")
        tbCodIns.Text = row("codInst")
        _CodInstitucion = row("id")
    End Sub
End Class