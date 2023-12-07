Imports System.Drawing.Printing
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports Janus.Windows.GridEX
Imports Logica.AccesoLogica
Imports UTILITIES
Public Class F0_Prestamo

#Region "Variables Globales"
    Dim _CodCliente As Integer = 0
    Dim _CodEmpleado As Integer = 0
    Dim _CodInstitucion As Integer = 0
    Dim _CodProveedor As Integer = 0
    Dim cod As Integer 'CODIGO DEL CAÑERO
    Dim codCont As String 'CODIGO DE ASIENTO CONTABLE
    Dim _codDocumento As Integer = 0
    Dim _codTipoCambio As Integer = 0
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem


#End Region

    Public Sub iniciarcomponentes()

        tbcod.Enabled = False

        _prCargarComboFinanciador(tbFinan)
        _prCargarComboMoneda(tbMoneda)
        _prCargarComboDocumento(cbDocumento)
        _Inhabilitar()

        _prCargarPrestamo()
        Limpiar()
        If grPrestamo.RowCount > 0 Then

            _MostrarRegistro()
        End If

          LblPaginacion.Text = CStr(grPrestamo.Row + 1) + "/" + CStr(grPrestamo.RowCount)

    End Sub

    Private Sub _Inhabilitar()
        tbfecha.Enabled = False
        codMon.ReadOnly = True
        tbMoneda.ReadOnly = True
        cbTipoCambio.ReadOnly = True
        codIns.ReadOnly = True
        tbInst.ReadOnly = True
        codCan.ReadOnly = True
        tbCanero.ReadOnly = True
        codFin.ReadOnly = True
        tbFinan.ReadOnly = True
        codPres.ReadOnly = True
        tbPrest.ReadOnly = True
        tbCodProv.ReadOnly = True
        tbProv.ReadOnly = True
        cbDocumento.ReadOnly = True
        tbCite.ReadOnly = True
        tbTotal.ReadOnly = True
        tbInteres.ReadOnly = True
        tbObs.ReadOnly = True
        btnGrabar.Enabled = False
        btnAnterior.Enabled = True
        btnEliminar.Enabled = True
        btnModificar.Enabled = True
        btnNuevo.Enabled = True
        btnPrimero.Enabled = True
        btnSiguiente.Enabled = True
        btnUltimo.Enabled = True
        grPrestamo.Enabled = True
    End Sub
    Private Sub _Habilitar()
        tbfecha.Enabled = True
        codMon.ReadOnly = False
        tbMoneda.ReadOnly = False
        cbTipoCambio.ReadOnly = False
        codIns.ReadOnly = False
        tbInst.ReadOnly = False
        codCan.ReadOnly = False
        tbCanero.ReadOnly = False
        codFin.ReadOnly = False
        tbFinan.ReadOnly = False
        codPres.ReadOnly = False
        tbPrest.ReadOnly = False
        tbCodProv.ReadOnly = False
        tbProv.ReadOnly = False
        cbDocumento.ReadOnly = False
        tbCite.ReadOnly = False
        tbTotal.ReadOnly = False
        tbInteres.ReadOnly = False
        tbObs.ReadOnly = False
        btnGrabar.Enabled = True
        btnAnterior.Enabled = False
        btnEliminar.Enabled = False
        btnModificar.Enabled = False
        btnNuevo.Enabled = False
        btnPrimero.Enabled = False
        btnSiguiente.Enabled = False
        btnUltimo.Enabled = False
        grPrestamo.Enabled = False
    End Sub

    Public Sub Limpiar()
        tbcod.Text = ""
        tbfecha.Value = Date.Now
        codMon.Text = ""
        tbMoneda.SelectedIndex = 0
        cbTipoCambio.Value = 0
        codIns.Text = ""
        tbInst.Text = ""
        codCan.Text = ""
        tbCanero.Text = ""
        codFin.Text = ""
        tbFinan.SelectedIndex = 0
        codPres.Text = ""
        tbPrest.SelectedIndex = 0
        tbCodProv.Text = ""
        tbProv.Text = ""
        cbDocumento.SelectedIndex = 0
        tbCite.Text = ""
        tbTotal.Text = ""
        tbInteres.Text = ""
        tbObs.Text = ""
        txtEstado.BackColor = Color.White
        txtEstado.Clear()

    End Sub

    Public Sub _MostrarRegistro()

        With grPrestamo
            tbcod.Text = .GetValue("tbid").ToString
            tbfecha.Text = .GetValue("tbfec").ToString
            codMon.Text = .GetValue("tbmon").ToString
            tbMoneda.Value = .GetValue("tbmon")
            cbTipoCambio.Value = .GetValue("tbtcam")
            codIns.Text = .GetValue("codInst").ToString
            tbInst.Text = .GetValue("nomInst").ToString
            _CodInstitucion = .GetValue("tbins").ToString
            codCan.Text = .GetValue("ydcod").ToString
            _CodProveedor = .GetValue("tbprov").ToString
            cod = .GetValue("tbcan").ToString
            tbCanero.Text = .GetValue("ydrazonsocial").ToString
            codFin.Text = .GetValue("tbfin").ToString
            tbFinan.Value = .GetValue("tbfin").ToString
            codPres.Text = .GetValue("tbtpre").ToString
            tbPrest.Value = .GetValue("tbtpre")
            If .GetValue("tbidprov").ToString <> 0 Then
                tbCodProv.Text = .GetValue("tbidprov").ToString
            Else
                tbCodProv.Text = "0"
            End If
            If .GetValue("tbnomprov").ToString <> "" Then
                tbProv.Text = .GetValue("tbnomprov").ToString
            Else
                tbProv.Text = ""
            End If
            cbDocumento.Value = .GetValue("tbdoc")
            tbCite.Text = .GetValue("tbcite").ToString
            tbTotal.Text = .GetValue("tbcap").ToString
            tbInteres.Text = .GetValue("tbapor").ToString
            tbObs.Text = .GetValue("tbobs").ToString
            codCont = .GetValue("tbcodCont").ToString
            If .GetValue("tbestado") = 1 Then
                txtEstado.Text = "VIGENTE"
                txtEstado.BackColor = Color.Green
                btnEliminar.Enabled = True
                btnModificar.Enabled = True
            Else
                txtEstado.Text = "ANULADO"
                txtEstado.BackColor = Color.Red
                btnEliminar.Enabled = False
                btnModificar.Enabled = False
            End If
        End With
    End Sub

    Private Sub CargarInteres(cod As Integer)
        Dim dt As New DataTable
        dt = L_fnTraerInteres(cod)
        If btnGrabar.Enabled = True Then
            If dt.Rows.Count > 0 Then
                tbInteres.Text = dt.Rows(0).Item("ycdes1").ToString
            Else

            End If
        End If

    End Sub
    Private Sub _prCargarPrestamo()
        Dim dt As New DataTable
        dt = L_fnGeneralPrestamos()
        grPrestamo.DataSource = dt
        grPrestamo.RetrieveStructure()
        grPrestamo.AlternatingColors = True

        With grPrestamo.RootTable.Columns("tbid")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = True
        End With
        With grPrestamo.RootTable.Columns("tbfec")
            .Width = 120
            .Caption = "FECHA VENC."
            .Visible = True
            .FormatString = "dd/MM/yyyy"
        End With

        With grPrestamo.RootTable.Columns("tbmon")
            .Width = 150
            .Caption = "LOTE"
            .Visible = False

        End With
        With grPrestamo.RootTable.Columns("tbtcam")
            .Width = 120
            .Caption = "FECHA VENC."
            .Visible = False

        End With

        With grPrestamo.RootTable.Columns("tbins")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False
        End With

        With grPrestamo.RootTable.Columns("codInst")
            .Caption = "CODINST"
            .Width = 90
            .Visible = True
        End With
        With grPrestamo.RootTable.Columns("nomInst")
            .Caption = "INSTITUCION"
            .Width = 150
            .Visible = True
        End With

        With grPrestamo.RootTable.Columns("tbcan")
            .Caption = "ARTÍCULO"
            .Width = 180
            .Visible = False

        End With
        With grPrestamo.RootTable.Columns("ydcod")
            .Caption = "CODCANERO"
            .Width = 100
            .Visible = True

        End With
        With grPrestamo.RootTable.Columns("ydrazonsocial")
            .Caption = "CANERO"
            .Width = 150
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
        End With

        With grPrestamo.RootTable.Columns("tbfin")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False

        End With
        With grPrestamo.RootTable.Columns("tbtpre")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grPrestamo.RootTable.Columns("tbprov")
            .Width = 80
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False

        End With
        With grPrestamo.RootTable.Columns("tbidprov")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False

        End With
        With grPrestamo.RootTable.Columns("tbnomprov")
            .Caption = "PROVEEDOR"
            .Width = 110
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True

        End With
        With grPrestamo.RootTable.Columns("tbdoc")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False

        End With
        With grPrestamo.RootTable.Columns("tbcite")
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
        End With
        With grPrestamo.RootTable.Columns("tbcap")
            .Caption = "APORTE"
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
        End With

        With grPrestamo.RootTable.Columns("tbapor")
            .Width = 100
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False

        End With
        With grPrestamo.RootTable.Columns("tbobs")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grPrestamo.RootTable.Columns("tbcodcont")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grPrestamo.RootTable.Columns("tbestado")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grPrestamo
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla

        End With
    End Sub

    Private Sub _prCargarComboDocumento(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnGeneralDocumento()
        Dim fila = dt.NewRow()
        fila(0) = 0
        fila(1) = "SELECCIONE DOCUMENTO"
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
        If (CType(cbDocumento.DataSource, DataTable).Rows.Count > 0) Then
            cbDocumento.SelectedIndex = 0
        End If
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
    Private Sub _prCargarComboFinanciador(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnGeneralFinanciadores()
        Dim fila = dt.NewRow()
        fila(0) = 0
        fila(1) = "SELECCIONE FINANCIADOR"
        dt.Rows.InsertAt(fila, 0)
        'a.ylcod1 ,a.yldes1 
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("ylcod2").Width = 70
            .DropDownList.Columns("ylcod2").Caption = "COD"
            .DropDownList.Columns.Add("yldes2").Width = mCombo.Width - 70
            .DropDownList.Columns("yldes2").Caption = "DESCRIPCION"
            .ValueMember = "ylcod2"
            .DisplayMember = "yldes2"
            .DataSource = dt
            .Refresh()
        End With
        If (CType(tbFinan.DataSource, DataTable).Rows.Count > 0) Then
            tbFinan.SelectedIndex = 0
        End If
    End Sub

    Private Sub _prCargarComboMoneda(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnGeneralMoneda()
        Dim fila = dt.NewRow()
        fila(0) = 0
        fila(1) = "SELECCIONE MONEDA"
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
        If (CType(tbMoneda.DataSource, DataTable).Rows.Count > 0) Then
            tbMoneda.SelectedIndex = 0
        End If
    End Sub

    Private Sub _prCargarComboTipoPrestamo(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim Finan As Integer = tbFinan.Value
        codFin.Text = Finan
        Dim dt As New DataTable
        dt = L_fnGeneralTipoPrestamo(Finan)
        Dim fila = dt.NewRow()
        fila(0) = 0
        fila(1) = "SELECCIONE TIPO PRESTAMO"
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
        If (CType(tbPrest.DataSource, DataTable).Rows.Count > 0) Then
            tbPrest.SelectedIndex = 0
        End If
    End Sub
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        If btnGrabar.Enabled = True Then
            _Inhabilitar()
            If grPrestamo.RowCount > 0 Then
                grPrestamo.Row = 0
                _MostrarRegistro()
            End If
        Else
            '  Public _modulo As SideNavItem
            Me.Close()
        End If
    End Sub

    Private Sub F0_Prestamo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        iniciarcomponentes()
    End Sub

    Public Sub _GuardarNuevo()
        Dim numi As String = ""

        Dim res As Boolean = L_fnGrabarPrestamo(numi, tbfecha.Value.ToString("yyyy/MM/dd"), tbMoneda.Value, IIf(tbMoneda.Value = 1, cbTipoCambio.Value, 0), _CodInstitucion,
                                              cod, codFin.Text, codPres.Text, IIf(tbCodProv.Visible = False, 0, _CodProveedor),
                                              _codDocumento, tbCite.Text, tbTotal.Text,
                                              CDbl(tbInteres.Text), tbObs.Text)
        If res Then
            _prCargarPrestamo()


            _MostrarRegistro()

            If codPres.Text <> "10016" And codPres.Text <> "10005" And tbCodProv.Text <> 0 Then
                contabilizarPrestamo()
            End If

            _prImiprimirNotaPrestamo(numi)
            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "El Prestamo Fue Registrado Correctamente".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "El Prestamo No Pudo Ser Registrado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        End If

    End Sub

    Public Sub _Modificar()


        Dim res As Boolean = L_fnModificarPrestamo(tbcod.Text, tbfecha.Value.ToString("yyyy/MM/dd"), tbMoneda.Value, IIf(tbMoneda.Value = 1, cbTipoCambio.Value, 0), codIns.Text,
                                              codCan.Text, codFin.Text, codPres.Text, IIf(tbCodProv.Visible = False, 0, IIf(codPres.Text = "10016" Or codPres.Text = "10005", 2, tbCodProv.Text)),
                                              _codDocumento, tbCite.Text, tbTotal.Text,
                                              CDbl(tbInteres.Text), tbObs.Text)
        If res Then
            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "El Prestamo No Pudo Ser Registrado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        Else
            If codPres.Text <> "10016" And codPres.Text <> "10005" Then
                L_Asiento_Borrar(codCont)

                contabilizarPrestamoDetalle()
            End If

            _prImiprimirNotaPrestamo(tbcod.Text)
            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "El Prestamo Fue Registrado Correctamente".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        End If

    End Sub

    Private Sub tbMoneda_ValueChanged(sender As Object, e As EventArgs) Handles tbMoneda.ValueChanged
        codMon.Text = tbMoneda.Value
        If tbMoneda.Value = 2 Then
            cbTipoCambio.Enabled = False
        Else
            If tbMoneda.Value = 1 Then
                cbTipoCambio.Enabled = True
                _prCargarComboTipoCambio(cbTipoCambio)
            End If

        End If
    End Sub

    Private Sub tbFinan_ValueChanged(sender As Object, e As EventArgs) Handles tbFinan.ValueChanged
        _prCargarComboTipoPrestamo(tbPrest)
        If tbFinan.Value = 100 Then
            tbCodProv.Visible = True
            tbProv.Visible = True
        Else

            tbCodProv.Visible = False
            tbProv.Visible = False
        End If
    End Sub

    Private Sub tbInst_TextChanged(sender As Object, e As EventArgs) Handles tbInst.TextChanged

    End Sub

    Private Sub tbCanero_TextChanged(sender As Object, e As EventArgs) Handles tbCanero.TextChanged
        If btnGrabar.Enabled = True Then
            Dim dt As DataTable
            dt = L_fnListarCaneroInstitucion(cod)
            Dim row As DataRow = dt.Rows(dt.Rows.Count - 1)
            tbInst.Text = row("institucion")
            codIns.Text = row("codInst")
            _CodInstitucion = row("id")
        End If
    End Sub

    Private Sub tbCanero_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCanero.KeyDown
        If e.KeyData = Keys.Control + Keys.Enter Then

            Dim dt As DataTable
            'dt = L_fnListarClientes()
            dt = L_fnListarClientesVenta()

            Dim listEstCeldas As New List(Of Modelo.Celda)
            listEstCeldas.Add(New Modelo.Celda("ydnumi,", False, "ID", 50))
            listEstCeldas.Add(New Modelo.Celda("ydcod", True, "COD. CLI", 100))
            listEstCeldas.Add(New Modelo.Celda("ydrazonsocial", True, "RAZÓN SOCIAL", 180))
            listEstCeldas.Add(New Modelo.Celda("yddesc", False, "NOMBRE", 280))
            listEstCeldas.Add(New Modelo.Celda("yddctnum", False, "N. Documento".ToUpper, 150))
            listEstCeldas.Add(New Modelo.Celda("yddirec", False, "DIRECCIÓN", 220))
            listEstCeldas.Add(New Modelo.Celda("ydtelf1", False, "Teléfono".ToUpper, 200))
            listEstCeldas.Add(New Modelo.Celda("ydfnac", False, "F.Nacimiento".ToUpper, 150, "MM/dd,YYYY"))
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



                Dim numiVendedor As Integer = IIf(IsDBNull(Row.Cells("ydnumivend").Value), 0, Row.Cells("ydnumivend").Value)
                If (numiVendedor > 0) Then
                    ''tbVendedor.Text = Row.Cells("vendedor").Value
                    cod = CInt(Row.Cells("ydnumi").Value)
                    codCan.Text = Row.Cells("ydcod").Value
                    tbCanero.Text = Row.Cells("ydrazonsocial").Value
                    'grdetalle.Select()
                    'Table_Producto = Nothing
                Else
                    tbInst.Clear()
                    codCan.Text = 0
                    'tbInst.Focus()
                    'Table_Producto = Nothing

                End If
            End If
        End If
    End Sub

    Private Sub tbPrest_ValueChanged(sender As Object, e As EventArgs) Handles tbPrest.ValueChanged
        Dim Pres As Integer = tbPrest.Value
        codPres.Text = Pres
        CargarInteres(Pres)
    End Sub

    Private Sub LabelX11_Click(sender As Object, e As EventArgs) Handles LabelX11.Click

    End Sub

    Private Sub TextBoxX1_KeyDown(sender As Object, e As KeyEventArgs) Handles tbProv.KeyDown

        If e.KeyData = Keys.Control + Keys.Enter Then

            Dim dt As DataTable

            dt = L_fnListarProveedores()
            '              a.ydnumi, a.ydcod, a.yddesc, a.yddctnum, a.yddirec
            ',a.ydtelf1 ,a.ydfnac 
            If dt.Rows.Count = 0 Then
                Throw New Exception("Lista de proveedores vacia")
            End If
            Dim listEstCeldas As New List(Of Modelo.Celda)
            listEstCeldas.Add(New Modelo.Celda("ydnumi,", True, "COD ORIG.", 90))
            listEstCeldas.Add(New Modelo.Celda("ydcod", True, "COD PROV.", 90))
            listEstCeldas.Add(New Modelo.Celda("yddesc", True, "NOMBRE", 280))
            listEstCeldas.Add(New Modelo.Celda("yddctnum", True, "N. Documento".ToUpper, 150))
            listEstCeldas.Add(New Modelo.Celda("yddirec", True, "DIRECCION", 220))
            listEstCeldas.Add(New Modelo.Celda("ydtelf1", True, "Telefono".ToUpper, 200))
            listEstCeldas.Add(New Modelo.Celda("ydfnac", False, "F.Nacimiento".ToUpper, 150, "MM/dd,YYYY"))
            Dim ef = New Efecto
            ef.tipo = 3
            ef.dt = dt
            ef.SeleclCol = 2
            ef.listEstCeldas = listEstCeldas
            ef.alto = 50
            ef.ancho = 350
            ef.Context = "Seleccione Proveedor".ToUpper
            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            If (bandera = True) Then
                Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row

                _CodProveedor = Row.Cells("ydnumi").Value
                tbProv.Text = Row.Cells("yddesc").Value
                'tbCodProv.Text = (Row.Cells("ydnumi").Value + " ' - '" + Row.Cells("ydcod").Value).ToString
                tbCodProv.Text = Row.Cells("ydcod").Value

            End If
        End If


    End Sub

    Public Function validarCampos() As Boolean
        If tbMoneda.Value = 0 Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Ingrese un Tipo de Moneda".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return True
        End If
        If tbMoneda.Value = 1 Then
            If cbTipoCambio.Value = 0 Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor Ingrese un Tipo de Cambio".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                Return True
            End If
        End If
        If tbCanero.Text = "" Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione un Cañero".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

            Return True
        End If
        If tbFinan.Value = 0 Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Ingrese un Financiador".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return True
        End If
        If tbPrest.Value = 0 Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Ingrese un Tipo de Prestamo".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return True
        End If
        If tbFinan.Value = 100 Then
            If tbProv.Text = "" Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor Ingrese un Proveedor".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                Return True
            End If
        End If
        If cbDocumento.Value = 0 Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Ingrese un Documento".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return True
        End If
        If tbCite.Text = "" Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Ingrese un Cite".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return True
        End If
        If tbTotal.Text = "" Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Ingrese un Capital".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return True
        End If
        If tbInteres.Text = "" Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Ingrese un Aporte Anual".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return True
        End If
        Return False
    End Function

    Private Sub cbDocumento_ValueChanged(sender As Object, e As EventArgs) Handles cbDocumento.ValueChanged
        _codDocumento = cbDocumento.Value
    End Sub

    Private Sub cbTipoCambio_ValueChanged(sender As Object, e As EventArgs) Handles cbTipoCambio.ValueChanged
        _codTipoCambio = cbTipoCambio.Value
    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        If validarCampos() Then
            Exit Sub
        End If
        If tbcod.Text = "" Then
            _GuardarNuevo()
            _Inhabilitar()

        Else
            _Modificar()
            _Inhabilitar()
            _prCargarPrestamo()
            grPrestamo.Row = 0
            _MostrarRegistro()
        End If
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        Limpiar()
        _Habilitar()

    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        If (L_fnVerificarCObros(tbcod.Text, codPres.Text)) Then

            Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)
            ToastNotification.Show(Me, "No se puede editar la venta con código ".ToUpper + tbcod.Text + ", porque tiene pagos realizados.".ToUpper,
                                                  img, 5000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter)
            Exit Sub
        Else
            _Habilitar()
        End If

    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        If grPrestamo.Row + 1 <= grPrestamo.RowCount - 1 Then
            grPrestamo.Row = grPrestamo.Row + 1
            _MostrarRegistro()
            LblPaginacion.Text = CStr(grPrestamo.Row + 1) + "/" + CStr(grPrestamo.RowCount)
        End If
    End Sub

    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        grPrestamo.Row = grPrestamo.RowCount - 1
        _MostrarRegistro()
        LblPaginacion.Text = CStr(grPrestamo.Row + 1) + "/" + CStr(grPrestamo.RowCount)
    End Sub

    Private Sub btnPrimero_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click
        grPrestamo.Row = 0
        _MostrarRegistro()
        LblPaginacion.Text = CStr(grPrestamo.Row + 1) + "/" + CStr(grPrestamo.RowCount)
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        If grPrestamo.Row - 1 >= 0 Then
            grPrestamo.Row = grPrestamo.Row - 1
            _MostrarRegistro()
            LblPaginacion.Text = CStr(grPrestamo.Row + 1) + "/" + CStr(grPrestamo.RowCount)
        End If
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        _prImiprimirNotaPrestamo(tbcod.Text)
    End Sub

    Public Sub _prImiprimirNotaPrestamo(numi As String)
        Dim ef = New Efecto


        ef.tipo = 2
        ef.Context = "MENSAJE PRINCIPAL".ToUpper
        ef.Header = "¿desea imprimir la nota de prestamo?".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            P_GenerarReporte(numi)
        End If
    End Sub
    Private Sub P_GenerarReporte(numi As String)
        Dim dt As DataTable = L_NotaDePrestamo(numi)

        Dim total As Decimal = Convert.ToDecimal(tbTotal.Text)
        Dim totald As Double = (total / 6.96)
        Dim fechaven As String = dt.Rows(0).Item("tbfec")
        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If

        Dim _Hora As String = Now.Hour.ToString + ":" + Now.Minute.ToString
        Dim _Ds2, _Ds3
        If codPres.Text = "10016" Or codPres.Text = "10005" Or codPres.Text = "100151" Or codPres.Text = "100161" Or codPres.Text = "100121" Or codPres.Text = "100131" Or codPres.Text = "100011" Or codPres.Text = "100021" Or codPres.Text = "100031" Or codPres.Text = "100041" Or codPres.Text = "100051" Or codPres.Text = "100061" Or codPres.Text = "100071" Or codPres.Text = "100081" Or codPres.Text = "100091" Or codPres.Text = "100101" Or codPres.Text = "100111" Then
            _Ds2 = L_Reporte_Factura_Cia("3")

            _Ds3 = L_ObtenerRutaImpresora("3")
        Else
            _Ds2 = L_Reporte_Factura_Cia("2")

            _Ds3 = L_ObtenerRutaImpresora("2")
        End If
        ' Datos de Impresion de Facturación
        'If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
        '    P_Global.Visualizador = New Visualizador 'Comentar
        'End If
        Dim _FechaAct As String
        Dim _FechaPar As String
        Dim _Fecha() As String
        Dim _Meses() As String = {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"}
        _FechaAct = fechaven
        _Fecha = Split(_FechaAct, "-")

        Dim objrep As Object = Nothing
        Dim empresaId = ObtenerEmpresaHabilitada()
        Dim empresaHabilitada As DataTable = ObtenerEmpresaTipoReporte(empresaId, Convert.ToInt32(ENReporte.NOTAVENTA))
        For Each fila As DataRow In empresaHabilitada.Rows
            If codPres.Text = "10016" Or codPres.Text = "10005" Or codPres.Text = "100151" Or codPres.Text = "100161" Or codPres.Text = "100121" Or codPres.Text = "100131" Or codPres.Text = "100011" Or codPres.Text = "100021" Or codPres.Text = "100031" Or codPres.Text = "100041" Or codPres.Text = "100051" Or codPres.Text = "100061" Or codPres.Text = "100071" Or codPres.Text = "100081" Or codPres.Text = "100091" Or codPres.Text = "100101" Or codPres.Text = "100111" Then
                objrep = New R_NotaPrestamoshoping
                SetParametrosNotaVenta(dt, total, _Hora, _Ds2, _Ds3, fila.Item("TipoReporte").ToString, objrep, tbPrest.Text)
            Else
                objrep = New R_NotaPrestamo_Cartashoping
                SetParametrosNotaVenta(dt, total, _Hora, _Ds2, _Ds3, fila.Item("TipoReporte").ToString, objrep, "")

            End If
        Next
    End Sub

    Private Sub SetParametrosNotaVenta(dt As DataTable, total As Decimal, _Hora As String, _Ds2 As DataSet, _Ds3 As DataSet, tipoReporte As String, objrep As Object, prestamo As String)

        Select Case tipoReporte
            Case ENReporteTipo.NOTAVENTA_Carta
                objrep.SetDataSource(dt)
                If codPres.Text = "10016" Or codPres.Text = "10005" Or codPres.Text = "100151" Or codPres.Text = "100161" Or codPres.Text = "100121" Or codPres.Text = "100131" Or codPres.Text = "100011" Or codPres.Text = "100021" Or codPres.Text = "100031" Or codPres.Text = "100041" Or codPres.Text = "100051" Or codPres.Text = "100061" Or codPres.Text = "100071" Or codPres.Text = "100081" Or codPres.Text = "100091" Or codPres.Text = "100101" Or codPres.Text = "100111" Then
                    objrep.SetParameterValue("prestamo", prestamo)

                End If

                objrep.SetParameterValue("Logo", gb_UbiLogo)
                objrep.SetParameterValue("NotaAdicional1", gb_NotaAdicional)

                objrep.SetParameterValue("Total", total)
                objrep.SetParameterValue("fechaImpresion", DateTime.Now())

            Case ENReporteTipo.NOTAVENTA_Ticket
                objrep.SetDataSource(dt)
                objrep.SetParameterValue("ECasaMatriz", _Ds2.Tables(0).Rows(0).Item("scsuc").ToString)
                objrep.SetParameterValue("ECiudadPais", _Ds2.Tables(0).Rows(0).Item("scpai").ToString)
                objrep.SetParameterValue("EDuenho", _Ds2.Tables(0).Rows(0).Item("scnom").ToString) '?
                objrep.SetParameterValue("Direccionpr", _Ds2.Tables(0).Rows(0).Item("scdir").ToString)
                objrep.SetParameterValue("Hora", _Hora)
                objrep.SetParameterValue("ENombre", _Ds2.Tables(0).Rows(0).Item("scneg").ToString) '?

        End Select
        If (_Ds3.Tables(0).Rows(0).Item("cbvp")) Then ') Then 'Vista Previa de la Ventana de Vizualización 1 = True 0 = False
            P_Global.Visualizador = New Visualizador
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
                    If doctoprint.PrinterSettings.PaperSizes(c).PaperName = "factura" Then
                        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(c).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(c)))
                        Exit For
                    End If
                Next
                objrep.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)

                objrep.PrintToPrinter(1, True, 0, 0)

            End If
        End If
    End Sub
    Private Sub contabilizarPrestamo()
        Dim codigoVenta = tbcod.Text
        Dim codCanero = "P/Ord:. " + codigoVenta + " Prestamo " + tbTotal.Text + "  " + codCan.Text + "-" + tbCanero.Text.Trim   'obobs
        Dim total = tbTotal.Text 'para obtener debe haber
        Dim dt, dt1, dtDetalle As DataTable
        Dim cuenta As String
        Dim debebs, haberbs, debeus, haberus As Double
        dt1 = ObtenerNumCuenta("Institucion", _CodInstitucion) 'obcuenta=ncuenta obtener cuenta de institucion
        dtDetalle = ObtenerNumCuentaProveedor("Institucion", _CodProveedor)


        Dim resTO001 = L_fnGrabarTO001prestamos(3, Convert.ToInt32(codigoVenta), "false", "", codigoVenta) 'numi cabecera to001
        'Dim resTO0011 As Boolean = L_fnGrabarTO001(Convert.ToInt32(codigoVenta))

        For a As Integer = 6 To 6 Step 1
            dt = CargarConfiguracion("configuracion", a) 'oblin=orden

            'dtDetalle = L_fnDetalleVenta1(codigoVenta)


            Dim oblin As Integer = 1
            'Dim totalCosto As Double = 0.00
            For Each row In dt.Rows

                If row("cuenta") = "-2" Then
                    If codCan.Text = 1530 Then
                        cuenta = 312
                    Else
                        cuenta = dt1.Rows(0).Item(7)
                    End If


                Else
                    cuenta = dtDetalle.Rows(0).Item(10) 'row("cuenta")
                End If
                If row("dh") = 1 Then
                    debeus = Math.Round(Convert.ToDouble(total) * Convert.ToDouble(row("porcentaje")) / 100, 2)
                    debebs = Math.Round(debeus * 6.96, 2)
                    haberus = 0.00
                    haberbs = 0.00
                Else

                    haberus = Math.Round(Convert.ToDouble(total) * Convert.ToDouble(row("porcentaje")) / 100, 2)
                    haberbs = Math.Round(haberus * 6.96, 2)
                    debeus = 0.00
                    debebs = 0.00
                End If
                Dim resTO0011 As Boolean = L_fnGrabarTO001(2, Convert.ToInt32(codigoVenta), resTO001, oblin, cuenta, codCanero, debebs, haberbs, debeus, haberus)
                oblin = oblin + 1
            Next
        Next
        Dim resp = L_fnObtenerDiferenciaAsientoContable(resTO001)
        L_Actualiza_Prestamo_Contabiliza(codigoVenta, resTO001)
    End Sub
    Private Sub contabilizarPrestamoDetalle()
        Dim codigoVenta = tbcod.Text
        Dim codCanero = "P/Ord:. " + codigoVenta + " Prestamo " + tbTotal.Text + "  " + codCan.Text + "-" + tbCanero.Text.Trim   'obobs
        Dim total = tbTotal.Text 'para obtener debe haber
        Dim dt, dt1, dtDetalle As DataTable
        Dim cuenta As String
        Dim debebs, haberbs, debeus, haberus As Double
        dt1 = ObtenerNumCuenta("Institucion", _CodInstitucion) 'obcuenta=ncuenta obtener cuenta de institucion
        dtDetalle = ObtenerNumCuentaProveedor("", _CodProveedor)


        ' Dim resTO001 = L_fnGrabarTO001prestamos(3, Convert.ToInt32(codigoVenta), "false") 'numi cabecera to001
        For a As Integer = 6 To 6 Step 1
            dt = CargarConfiguracion("configuracion", a) 'oblin=orden

            'dtDetalle = L_fnDetalleVenta1(codigoVenta)


            Dim oblin As Integer = 1
            'Dim totalCosto As Double = 0.00
            For Each row In dt.Rows

                If row("cuenta") = "-2" Then
                    cuenta = dt1.Rows(0).Item(7)

                Else
                    cuenta = dtDetalle.Rows(0).Item(10) 'row("cuenta")
                End If
                If row("dh") = 1 Then
                    debeus = Math.Round(Convert.ToDouble(total) * Convert.ToDouble(row("porcentaje")) / 100, 2)
                    debebs = Math.Round(debeus * 6.96, 2)
                    haberus = 0.00
                    haberbs = 0.00
                Else

                    haberus = Math.Round(Convert.ToDouble(total) * Convert.ToDouble(row("porcentaje")) / 100, 2)
                    haberbs = Math.Round(haberus * 6.96, 2)
                    debeus = 0.00
                    debebs = 0.00
                End If
                Dim resTO0011 As Boolean = L_fnGrabarTO001(2, Convert.ToInt32(codigoVenta), codCont, oblin, cuenta, codCanero, debebs, haberbs, debeus, haberus)
                oblin = oblin + 1
            Next
        Next
        Dim resp = L_fnObtenerDiferenciaAsientoContable(codCont)
        'L_Actualiza_Venta_Contabiliza(codigoVenta, resTO001)
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If (L_fnVerificarCObros(tbcod.Text, codPres.Text)) Then

            Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)
            ToastNotification.Show(Me, "No se puede anular el prestamo con código ".ToUpper + tbcod.Text + ", porque tiene pagos realizados.".ToUpper,
                                                  img, 5000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter)
            Exit Sub
        Else
            If (tbcod.Text <> String.Empty) Then
                Dim result As DialogResult = MessageBox.Show("¿Está seguro de ANULAR el prestamo con número de orden:" + tbcod.Text + "?", "PRESTAMO ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)

                If result = DialogResult.OK Then
                    Dim mensajeError As String = ""
                    Dim res As Boolean = L_fnEliminarPrestamos(tbcod.Text, codPres.Text)
                    If res Then
                        Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                        ToastNotification.Show(Me, "Código de Prestamo  ".ToUpper + tbcod.Text + " anulado con Exito.".ToUpper,
                                                  img, 2000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter)

                        _MostrarRegistro()

                    Else
                        Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                        ToastNotification.Show(Me, mensajeError, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    End If
                ElseIf result = DialogResult.Cancel Then
                    ' Código para ejecutar si se hace clic en "Cancelar"
                End If


            End If
        End If
    End Sub

    Private Sub grPrestamo_KeyDown(sender As Object, e As KeyEventArgs) Handles grPrestamo.KeyDown
        If e.KeyData = Keys.Enter Then
            MSuperTabControl.SelectedTabIndex = 0
            grPrestamo.Focus()

        End If
    End Sub

    Private Sub grPrestamo_SelectionChanged(sender As Object, e As EventArgs) Handles grPrestamo.SelectionChanged
        If (grPrestamo.RowCount >= 0 And grPrestamo.Row >= 0) Then
            _MostrarRegistro()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        contabilizarPrestamo()
    End Sub
End Class