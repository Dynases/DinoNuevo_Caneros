Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports Logica.AccesoLogica

Public Class F0_Prestamo

#Region "Variables Globales"
    Dim _CodCliente As Integer = 0
    Dim _CodEmpleado As Integer = 0
    Dim _CodInstitucion As Integer = 0
    Dim _CodProveedor As Integer = 0
    Dim cod As Integer 'CODIGO DEL CAÑERO
    Dim _codDocumento As Integer = 0
    Dim _codTipoCambio As Integer = 0
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem


#End Region

    Public Sub iniciarcomponentes()
        'tbInst.ReadOnly = True
        'codIns.ReadOnly = True
        'tbfecha.Value = Date.Now
        'codCan.ReadOnly = True
        'tbCanero.ReadOnly = True
        'codFin.ReadOnly = True
        'codPres.ReadOnly = True
        'codMon.ReadOnly = True
        'tbCodProv.ReadOnly = True
        'tbProv.ReadOnly = True
        tbcod.Enabled = False
        'btnGrabar.Enabled = False
        'tbFinan.ReadOnly = True
        'tbPrest.ReadOnly = True
        _prCargarComboFinanciador(tbFinan)
        _prCargarComboMoneda(tbMoneda)
        _prCargarComboDocumento(cbDocumento)
        _Inhabilitar()

        _prCargarPrestamo()
        Limpiar()
        If grPrestamo.RowCount > 0 Then
            grPrestamo.Row = 0
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
    End Sub

    Public Sub _MostrarRegistro()

        With grPrestamo
            tbcod.Text = .GetValue("tbid")
            tbfecha.Text = .GetValue("tbfec").ToString
            codMon.Text = .GetValue("tbmon").ToString
            tbMoneda.Value = .GetValue("tbmon")
            If .GetValue("tbtcam") <> 0 Then
                cbTipoCambio.Value = .GetValue("tbtcam")
            Else
                cbTipoCambio.Text = ""
            End If
            codIns.Text = .GetValue("codInst").ToString
            tbInst.Text = .GetValue("nomInst").ToString
            codCan.Text = .GetValue("ydcod").ToString
            cod = .GetValue("tbcan").ToString
            tbCanero.Text = .GetValue("ydrazonsocial").ToString
            codFin.Text = .GetValue("tbfin").ToString
            tbFinan.Value = .GetValue("tbfin").ToString
            codPres.Text = .GetValue("tbtpre").ToString
            tbPrest.Value = .GetValue("tbtpre")
            If .GetValue("tbidprov").ToString <> 0 Then
                tbCodProv.Text = .GetValue("tbidprov").ToString
            Else
                tbCodProv.Text = ""
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
            .Width = 150
            .Caption = "LOTE"
            .Visible = False
        End With
        With grPrestamo.RootTable.Columns("tbfec")
            .Width = 120
            .Caption = "FECHA VENC."
            .Visible = False
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
            .Width = 90
            .Visible = False
        End With
        With grPrestamo.RootTable.Columns("nomInst")
            .Width = 90
            .Visible = False
        End With

        With grPrestamo.RootTable.Columns("tbcan")
            .Caption = "ARTÍCULO"
            .Width = 180
            .Visible = False

        End With
        With grPrestamo.RootTable.Columns("ydcod")
            .Caption = "DESCRIPCIÓN DE ARTÍCULO"
            .Width = 280
            .Visible = False

        End With
        With grPrestamo.RootTable.Columns("ydrazonsocial")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
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
            .Width = 110
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False

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
            .Width = 120
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = False
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
        fila(1) = "SELECCIONE TIPO DE CAMBIO"
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
        fila(1) = "SELECCIONE TIPO DE PRESTAMO"
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


        Dim res As Boolean = L_fnGrabarPrestamo(tbfecha.Value.ToString("yyyy/MM/dd"), tbMoneda.Value, IIf(tbMoneda.Value = 1, cbTipoCambio.Value, 0), _CodInstitucion,
                                              cod, codFin.Text, codPres.Text, IIf(tbCodProv.Visible = False, 0, _CodProveedor),
                                              _codDocumento, tbCite.Text, tbTotal.Text,
                                              CDbl(tbInteres.Text), tbObs.Text)
        If res Then
            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "El Prestamo Fue Registrado Correctamente".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "El Prestamo No Pudo Ser Registrado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        End If

    End Sub

    Public Sub _Modificar()


        Dim res As Boolean = L_fnModificarPrestamo(tbcod.Text, tbfecha.Value.ToString("yyyy/MM/dd"), tbMoneda.Value, IIf(tbMoneda.Value = 1, cbTipoCambio.Value, 0), codIns.Text,
                                              codCan.Text, codFin.Text, codPres.Text, IIf(tbCodProv.Visible = False, 0, tbCodProv.Text),
                                              _codDocumento, tbCite.Text, tbTotal.Text,
                                              CDbl(tbInteres.Text), tbObs.Text)
        If res Then
            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "El Prestamo Fue Registrado Correctamente".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "El Prestamo No Pudo Ser Registrado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
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
        If tbCanero.Text = "" Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione un Cañero".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

            Return True
        End If
        If tbFinan.Value = 100 Then
            If tbProv.Text = "" Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor Ingrese un Proveedor".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                Return True
            End If
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
        If tbMoneda.Value = 0 Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Ingrese una Moneda".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return True
        End If
        If cbDocumento.Value = 0 Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Ingrese un Documento".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
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
            _prCargarPrestamo()
            grPrestamo.Row = 0
            _MostrarRegistro()
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
        _Habilitar()
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
End Class