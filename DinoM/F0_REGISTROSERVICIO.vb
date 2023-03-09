Imports System.Drawing.Printing
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports Logica.AccesoLogica
Imports Newtonsoft.Json
Imports UTILITIES
Imports DinoM.LoginResp
Public Class F0_REGISTROSERVICIO
    'variables sifac
    Public tokenSifac As String
    Public CodActEco As String
    Public CodProSINs As String
    Public Ume As Integer
    Public preciosifac As Integer
    Public tipoDocumento As Integer
    Public correo As String
    Public _Fecha As Date
    Public _codCaneroUcg As String

    Public CodProducto As String
    Public Cantidad As Integer
    Public PrecioU As Integer
    Public PrecioTot As Decimal
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

        _Inhabilitar()

        _prCargarServicios()
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
            cbTipoCambio.Value = .GetValue("tbtcam")
            codIns.Text = .GetValue("codInst").ToString
            tbInst.Text = .GetValue("nomInst").ToString
            _CodInstitucion = .GetValue("tbins").ToString
            codCan.Text = .GetValue("ydcod").ToString

            cod = .GetValue("tbcan").ToString
            tbCanero.Text = .GetValue("ydrazonsocial").ToString
            codFin.Text = .GetValue("tbServicio").ToString
            tbFinan.Value = .GetValue("tbServicio")



            tbTotal.Text = .GetValue("tbcap").ToString
            tbInteres.Text = .GetValue("tbapor").ToString
            tbObs.Text = .GetValue("tbobs").ToString
            codCont = .GetValue("tbcodCont").ToString
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
    Private Sub _prCargarServicios()
        Dim dt As New DataTable
        dt = L_fnGeneralServiciosFacturado()
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
        With grPrestamo.RootTable.Columns("tbServicio")
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
        With grPrestamo.RootTable.Columns("tbcodcont")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

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
        dt = L_fnGeneralServicios()
        Dim fila = dt.NewRow()
        fila(0) = 0
        fila(1) = "SELECCIONE EL SERVICIO"
        dt.Rows.InsertAt(fila, 0)
        'a.ylcod1 ,a.yldes1 
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("Id").Width = 70
            .DropDownList.Columns("Id").Caption = "COD"
            .DropDownList.Columns.Add("NombreServicio").Width = mCombo.Width - 70
            .DropDownList.Columns("NombreServicio").Caption = "DESCRIPCION"
            .ValueMember = "Id"
            .DisplayMember = "NombreServicio"
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
        If (CType(tbMoneda.DataSource, DataTable).Rows.Count > 0) Then
            tbMoneda.SelectedIndex = 0
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

    Private Sub F0_REGISTROSERVICIO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        iniciarcomponentes()
    End Sub

    Public Sub _GuardarNuevo()

        'Dim Succes As String = Emisor(tokenSifac)
        'If Succes = 2 Or Succes = 8 Or Succes = 5 Then

        'End If
        Dim res As Boolean = L_fnGrabarServicio(tbfecha.Value.ToString("yyyy/MM/dd"), tbMoneda.Value, IIf(tbMoneda.Value = 1, cbTipoCambio.Value, 0), _CodInstitucion,
                                              cod, codFin.Text,
                                               tbTotal.Text,
                                              CDbl(tbInteres.Text), tbObs.Text)
        If res Then
            _prCargarServicios()
            grPrestamo.Row = 0
            _MostrarRegistro()
            'contabilizarPrestamo()
            '_prImiprimirNotaPrestamo(tbcod.Text)
            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "El Prestamo Fue Registrado Correctamente".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "El Prestamo No Pudo Ser Registrado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        End If

    End Sub

    Public Sub _Modificar()


        'Dim res As Boolean = L_fnModificarPrestamo(tbcod.Text, tbfecha.Value.ToString("yyyy/MM/dd"), tbMoneda.Value, codIns.Text,
        '                                      codCan.Text, codFin.Text,
        '                                      _codDocumento, tbTotal.Text,
        '                                      CDbl(tbInteres.Text), tbObs.Text)
        'If res Then
        '    Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
        '    ToastNotification.Show(Me, "El Prestamo No Pudo Ser Registrado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        'Else
        '    L_Asiento_Borrar(codCont)
        '    contabilizarPrestamoDetalle()
        '    _prImiprimirNotaPrestamo(tbcod.Text)
        '    Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
        '    ToastNotification.Show(Me, "El Prestamo Fue Registrado Correctamente".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        'End If

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
        Dim Finan As Integer = tbFinan.Value
        codFin.Text = Finan
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


    Private Sub LabelX11_Click(sender As Object, e As EventArgs)

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

    Private Sub cbTipoCambio_ValueChanged(sender As Object, e As EventArgs)
        _codTipoCambio = cbTipoCambio.Value
    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click

        tokenSifac = ObtToken(1)
        If tokenSifac = "400" Then
            Me.Close()
            MessageBox.Show("No se pudo establecer conexión con EDOC, revise su conexion de internet por favor. Intente De Nuevo")

        Else

            If validarCampos() Then
                Exit Sub
            End If
            If tbcod.Text = "" Then
                _GuardarNuevo()
                _Inhabilitar()

            Else
                _Modificar()
                _Inhabilitar()
                _prCargarServicios()
                grPrestamo.Row = 0
                _MostrarRegistro()
            End If

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
        Dim _Ds2 = L_Reporte_Factura_Cia("2")

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

        Dim objrep As Object = Nothing
        Dim empresaId = ObtenerEmpresaHabilitada()
        Dim empresaHabilitada As DataTable = ObtenerEmpresaTipoReporte(empresaId, Convert.ToInt32(ENReporte.NOTAVENTA))
        For Each fila As DataRow In empresaHabilitada.Rows
            objrep = New R_NotaPrestamo_Cartashoping
            SetParametrosNotaVenta(dt, total, _Hora, _Ds2, _Ds3, fila.Item("TipoReporte").ToString, objrep)
        Next
    End Sub

    Private Sub SetParametrosNotaVenta(dt As DataTable, total As Decimal, _Hora As String, _Ds2 As DataSet, _Ds3 As DataSet, tipoReporte As String, objrep As Object)

        Select Case tipoReporte
            Case ENReporteTipo.NOTAVENTA_Carta
                objrep.SetDataSource(dt)


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


        Dim resTO001 = L_fnGrabarTO001prestamos(3, Convert.ToInt32(codigoVenta), "false") 'numi cabecera to001
        'Dim resTO0011 As Boolean = L_fnGrabarTO001(Convert.ToInt32(codigoVenta))

        For a As Integer = 6 To 7 Step 1
            dt = CargarConfiguracion("configuracion", a) 'oblin=orden

            'dtDetalle = L_fnDetalleVenta1(codigoVenta)


            Dim oblin As Integer = 1
            'Dim totalCosto As Double = 0.00
            For Each row In dt.Rows
                '    Select Case row("cuenta")

                'If row("cuenta") = "-1" Then
                '    For Each detalle In dtDetalle.Rows
                '        cuenta = detalle("yfclot")
                '        If row("dh") = 1 Then
                '            debeus = (Convert.ToDouble(detalle("tbptot2")) * Convert.ToDouble(row("porcentaje"))) / 100
                '            debebs = debeus * 6.96
                '            haberus = 0.00
                '            haberbs = 0.00
                '            totalCosto = totalCosto + Convert.ToDouble(detalle("tbptot2"))
                '        Else
                '            haberus = (Convert.ToDouble(detalle("tbptot2")) * Convert.ToDouble(row("porcentaje"))) / 100
                '            haberbs = haberus * 6.96
                '            debeus = 0.00
                '            debebs = 0.00
                '            totalCosto = totalCosto + Convert.ToDouble(detalle("tbptot2"))
                '        End If

                '        Dim resTO00112 As Boolean = L_fnGrabarTO001(2, Convert.ToInt32(codigoVenta), resTO001, oblin, cuenta, codCanero, debebs, haberbs, debeus, haberus)
                '        oblin = oblin + 1
                '    Next


                '    If row("cuenta") = "-1" Then
                '        Continue For
                '    End If

                'End If
                If row("cuenta") = "-2" Then
                    cuenta = dt1.Rows(0).Item(7)

                Else
                    cuenta = dtDetalle.Rows(0).Item(10) 'row("cuenta")
                End If
                If row("dh") = 1 Then
                    debeus = Convert.ToDouble(total) * Convert.ToDouble(row("porcentaje")) / 100
                    debebs = debeus * 6.96
                    haberus = 0.00
                    haberbs = 0.00
                Else

                    haberus = Convert.ToDouble(total) * Convert.ToDouble(row("porcentaje")) / 100
                    haberbs = haberus * 6.96
                    debeus = 0.00
                    debebs = 0.00
                End If
                Dim resTO0011 As Boolean = L_fnGrabarTO001(2, Convert.ToInt32(codigoVenta), resTO001, oblin, cuenta, codCanero, debebs, haberbs, debeus, haberus)
                oblin = oblin + 1
            Next
        Next

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
        For a As Integer = 6 To 7 Step 1
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
                    debeus = Convert.ToDouble(total) * Convert.ToDouble(row("porcentaje")) / 100
                    debebs = debeus * 6.96
                    haberus = 0.00
                    haberbs = 0.00
                Else

                    haberus = Convert.ToDouble(total) * Convert.ToDouble(row("porcentaje")) / 100
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

    'funciones de conexion con sifac para facturacion

    Public Function ObtToken(idserviciosEdoc As Integer)
        Dim api = New DBApi()

        '' httpClient.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Bearer", "Your Key")
        Dim url = "https://bo-emp-rest-auth-v2-1.edocnube.com/ServicioEDOC?Id=" + idserviciosEdoc.ToString

        Dim headers = New List(Of Parametro) From {
            New Parametro("Authorization", "Basic ZWRvY18xMDI4Mzk1MDIzOjFlN2ZVYzlFbVU="),
            New Parametro("Cookie", "ARRAffinity=e89758e10c9869c11e2227a89658629cf00ab1218b50631917483d7ec6ac23ce; ARRAffinitySameSite=e89758e10c9869c11e2227a89658629cf00ab1218b50631917483d7ec6ac23ce; TiPMix=38.420115527070706; x-ms-routing-name=self")
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

    'Public Function Emisor(tokenObtenido)
    '    ' L_BuscarCodCanero(_CodCliente)
    '    Randomize()
    '    cbleyendas.SelectedIndex = CLng((0 - 7) * Rnd() + 7)

    '    Dim leyendas As String
    '    leyendas = cbleyendas.Value
    '    Dim api = New DBApi()
    '    Dim Emenvio = New EmisorEnvio.Emisor()

    '    Dim TDoc = tipoDocumento 'obtiene el 'Codigo Tipo de documento' 


    '    Dim array(rearmarDetalle().Rows.Count - 1) As EmisorEnvio.Detalle
    '    Dim val = 0
    '    PrecioTot = 0.00000

    '    Dim EmenvioDetalle = New EmisorEnvio.Detalle()
    '        EmenvioDetalle.actividadEconomica = row("ygcodact").ToString
    '        EmenvioDetalle.codigoProductoSin = row("ygcodsin").ToString
    '    EmenvioDetalle.codigoProducto = (codFin.Text)
    '    EmenvioDetalle.descripcion = (tbFinan.Text + " " + row("producto").ToString)
    '    EmenvioDetalle.unidadMedida = row("ygcodu").ToString
    '        EmenvioDetalle.cantidad = (row("tbcmin"))
    '        EmenvioDetalle.precioUnitario = Format((Convert.ToDecimal(row("tbpbas"))) * 6.96, "0.00000")
    '        EmenvioDetalle.montoDescuento = 0
    '        EmenvioDetalle.subTotal = Format(Format((Convert.ToDecimal(row("tbpbas"))) * 6.96, "0.00000") * (row("tbcmin")), "0.00000")
    '        EmenvioDetalle.numeroSerie = ""
    '        EmenvioDetalle.numeroImei = ""

    '        PrecioTot = PrecioTot + EmenvioDetalle.subTotal 'Format(PrecioTot + Format((Convert.ToDecimal(row("tbpbas")) * 6.96), "0.00000") * (row("tbcmin")), "0.00") 'total


    '        array(val) = EmenvioDetalle
    '        'vector = array
    '        val = val + 1


    '    Dim NumFactura As Integer
    '    Dim dsApi As DataSet
    '    Dim _Autorizacion
    '    Dim _Ds1 As New DataSet
    '    _Ds1 = L_DosificacionCajas("1", gi_userSuc, _Fecha, gs_NroCaja)

    '    _Autorizacion = _Ds1.Tables(0).Rows(0).Item("sbautoriz").ToString
    '    dsApi = L_Dosificacion("1", 1, _Fecha)
    '    NumFactura = CInt(dsApi.Tables(0).Rows(0).Item("sbnfac")) + 1
    '    Dim maxNFac As Integer = L_fnObtenerMaxIdTabla("TFV001", "fvanfac", "fvaautoriz = " + _Autorizacion)
    '    NumFactura = maxNFac + 1

    '    Emenvio.nitEmisor = 1028395023
    '    Emenvio.razonSocialEmisor = "ASOCIACION GREMIAL AGROPECUARIA UNIÓN DE CAÑEROS GUABIRA"
    '    Emenvio.municipio = "MONTERO"
    '    Emenvio.direccion = "CALLE LIBERTAD ESQ.BOLIVAR"
    '    Emenvio.telefonoEmisor = "9221563"
    '    Emenvio.nombreRazonSocial = tbCanero.Text.ToString()
    '    Emenvio.codigoTipoDocumentoIdentidad = TDoc
    '    Emenvio.codigoCliente = _codCaneroUcg.ToString
    '    Dim datePatt As String = "yyyy-MM-ddTHH:mm:ss.000"
    '    Dim localDate = DateTime.Now
    '    Dim dtString As String = localDate.ToString(datePatt)
    '    Emenvio.fechaEmision = dtString ' "2022-09-28T08:56:15.000"

    '    Emenvio.codigoSucursal = 0




    '        Emenvio.numeroFactura = NumFactura
    '    Emenvio.montoTotal = Format(PrecioTot, "0.00")
    '    Emenvio.montoTotalSujetoIva = Format(PrecioTot, "0.00")
    '    Emenvio.codigoMoneda = 2
    '    Emenvio.tipoCambio = 6.96
    '    Emenvio.montoTotalMoneda = Format(PrecioTot / 6.96, "0.00")
    '    If swTipoVenta.Value = True Then
    '        Emenvio.codigoMetodoPago = 1
    '    Else
    '        Emenvio.codigoMetodoPago = 239
    '    End If

    '    Emenvio.leyenda = leyendas
    '    Emenvio.tipoEmision = 1
    '    Emenvio.usuario = lbUsuario.Text
    '    Emenvio.nombreIntegracion = "Dino_ucg"
    '    Emenvio.email = correo

    '    Emenvio.numeroDocumento = ""
    '    Emenvio.complemento = ""

    '    ' Emenvio.numeroTarjeta = '---------------------
    '    Emenvio.codigoPuntoVenta = 0 '--------------------
    '    'Emenvio.codigoDocumentoSector = 1 '-------------------





    '    Emenvio.montoGiftCard = 0 '----------------
    '    Emenvio.descuentoAdicional = 0 '-------------------
    '    Emenvio.codigoExcepcion = 0 '---------------


    '    'Emenvio.actividadEconomica = 692000 'falta
    '    Emenvio.detalles = array
    '    Dim json = JsonConvert.SerializeObject(Emenvio)
    '    Dim url = "https://bo-emp-rest-emision-v2-1.edocnube.com/api/Emitir/EmisionFacturaCompraVentaBonificaciones"

    '    Dim headers = New List(Of Parametro) From {
    '        New Parametro("Authorization", "Bearer " + tokenObtenido),
    '        New Parametro("Content-Type", "Accept:application/json; charset=utf-8")
    '    }

    '    Dim parametros = New List(Of Parametro)

    '    Dim response = api.Post(url, headers, parametros, Emenvio)

    '    Dim result = JsonConvert.DeserializeObject(Of RespEmisor)(response)
    '    Dim resultError = JsonConvert.DeserializeObject(Of Resp400)(response)

    '    codigoRecepcion = result.codigoRecepcion
    '    estadoEmisionEdoc = result.estadoEmisionEDOC
    '    fechaEmision1 = result.fechaEmision
    '    cuf = result.cuf
    '    cuis = result.cuis
    '    cufd = result.cufd
    '    codigoControl = result.codigoControl
    '    linkCodigoQr = result.linkCodigoQR
    '    codigoError = result.codigoError
    '    mensajeRespuesta = result.mensajeRespuesta
    '    If estadoEmisionEdoc = 2 Then
    '        mensajeRespuesta = "Factura validada correctamente por Impuestos."
    '    End If
    '    MessageBox.Show(mensajeRespuesta)
    '    Dim codigo = result.estadoEmisionEDOC
    '    Dim xml As String

    '    Return codigo
    'End Function
End Class