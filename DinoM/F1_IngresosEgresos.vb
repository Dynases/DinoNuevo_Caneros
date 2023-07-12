Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports System.IO
Imports Janus.Windows.GridEX

Public Class F1_IngresosEgresos

#Region "Variable Globales"
    Dim _Inter As Integer = 0
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Dim RutaGlobal As String = gs_CarpetaRaiz
    Dim RutaTemporal As String = "C:\Temporal"
    Dim Modificado As Boolean = False
    Dim nameImg As String = "Default.jpg"
    Dim Socio As Boolean = False
    Dim NumiCuentaContable As Integer = 0
    Dim nuevo As Integer = 0
#End Region
#Region "METODOS PRIVADOS"


    Private Sub _prIniciarTodo()

        Me.Text = "I N G R E S O S / E G R E S O S"
        _prCargarComboLibreria(cbConcepto1, 9, 1)
        _prCargarComboLibreria(cbTipPago1, 1, 11)
        _prCargarComboActivo(cbActivo)
        _PMIniciarTodo()
        '_prAsignarPermisos()
        _prCargarLengthTextBox()

        GroupPanelBuscador.Style.BackColor = Color.FromArgb(13, 71, 161)
        GroupPanelBuscador.Style.BackColor2 = Color.FromArgb(13, 71, 161)
        GroupPanelBuscador.Style.TextColor = Color.White
        Dim blah As Bitmap = My.Resources.checked
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        JGrM_Buscador.RootTable.HeaderFormatStyle.FontBold = TriState.True
        JGrM_Buscador.AlternatingColors = True
        btnModificar.Enabled = True
        btnEliminar.Enabled = True


    End Sub

    Private Sub _prCargarComboActivo(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarActivoDisponible()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("canumi").Width = 60
            .DropDownList.Columns("canumi").Caption = "COD"
            .DropDownList.Columns.Add("cadesc").Width = 500
            .DropDownList.Columns("cadesc").Caption = "ACTIVOS DISPONIBLES"
            .ValueMember = "canumi"
            .DisplayMember = "cadesc"
            .DataSource = dt
            .Refresh()
        End With
        If (CType(cbActivo.DataSource, DataTable).Rows.Count > 0) Then
            cbActivo.SelectedIndex = 0
        End If
    End Sub
    Public Sub _prCargarLengthTextBox()
        tbDescripcion.MaxLength = 200
        cbConcepto1.MaxLength = 40

    End Sub



    Private Function _fnActionNuevo() As Boolean
        'Funcion que me devuelve True si esta en la actividad crear nuevo Tipo de Equipo
        Return tbcodigo.Text.ToString.Equals("") And tbDescripcion.ReadOnly = False
    End Function

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
    Private Sub MostrarMensajeError(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.WARNING,
                               4000,
                               eToastGlowColor.Red,
                               eToastPosition.TopCenter)
    End Sub
#End Region
#Region "METODOS SOBREESCRITOS"

    Public Overrides Sub _PMOHabilitar()
        swTipo.IsReadOnly = False

        SwMoneda.IsReadOnly = False
        SwParticular.IsReadOnly = False
        dpFecha.Enabled = True
        tbDescripcion.ReadOnly = False
        tbdescCanero.ReadOnly = False
        cbConcepto1.ReadOnly = False
        cbTipPago1.ReadOnly = False
        tbMonto.IsInputReadOnly = False
        tbObservacion.ReadOnly = False
        SwParticular.Value = True
        'If swTipo.Value = True Then
        '    btnVentCobros.Enabled = True
        'End If


    End Sub
    Public Overrides Sub _PMOInhabilitar()
        nuevo = 0
        tbcodigo.ReadOnly = True
        tbIdCaja.ReadOnly = True
        swTipo.IsReadOnly = True

        SwMoneda.IsReadOnly = True
        SwParticular.IsReadOnly = True
        dpFecha.Enabled = False
        tbDescripcion.ReadOnly = True
        cbConcepto1.ReadOnly = True
        cbTipPago1.ReadOnly = True
        tbMonto.IsInputReadOnly = True
        tbObservacion.ReadOnly = True
        btnVentCobros.Enabled = False
        tbdescCanero.ReadOnly = True
    End Sub
    Public Overrides Sub _PMOHabilitarFocus()
        With MHighlighterFocus
            .SetHighlightOnFocus(tbDescripcion, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(cbConcepto1, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbMonto, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbObservacion, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbcodigo, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbIdCaja, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        End With
    End Sub
    Public Overrides Sub _PMOLimpiar()

        tbcodigo.Text = ""
        tbIdCaja.Text = ""
        swTipo.Value = True
        dpFecha.Value = Now.Date
        tbDescripcion.Text = ""
        'cbConcepto.SelectedIndex = 0
        cbConcepto1.Text = ""
        tbMonto.Value = 0
        tbObservacion.Text = ""

        tbDescripcion.Focus()
        lbNroCaja.Text = ""

        tbRecibi.Text = ""
        tbentregue.Text = ""
        tbBanco.Text = ""
        tbNroCheque.Text = ""
        tbNroOpera.Text = ""

        tbcodCanero.Text = ""
        tbcodInst.Text = ""
        tbdescCanero.Text = ""
        tbInstitucion.Text = ""
        tbfecha.Text = ""
        SwParticular.Value = True
        If SwParticular.Value = True Then
            tbcodInst.Text = "888"
            tbInstitucion.Text = "PARTICULARES (NO CAÑEROS)"
            tbdescCanero.ReadOnly = False
        End If
    End Sub
    Public Overrides Sub _PMOLimpiarErrores()
        MEP.Clear()
        tbcodigo.BackColor = Color.White
        tbIdCaja.BackColor = Color.White
        tbDescripcion.BackColor = Color.White
        cbConcepto1.BackColor = Color.White
        tbMonto.BackColor = Color.White
        tbObservacion.BackColor = Color.White
    End Sub
    Public Overrides Function _PMOValidarCampos() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()
        If cbConcepto1.Text = "" Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            MEP.SetError(cbConcepto1, "Selecione un concepto !".ToUpper)
            _ok = False
        End If

        If cbTipPago1.Value = 3 Then
            If tbNroCheque.Text = String.Empty Then
                tbNroCheque.BackColor = Color.White
                MEP.SetError(tbNroCheque, "ingrese Dato en el campo Numero de Cheque !".ToUpper)
                _ok = False
            Else
                tbNroCheque.BackColor = Color.White
                MEP.SetError(tbNroCheque, "")
            End If
            If tbBanco.Text = String.Empty Then
                tbBanco.BackColor = Color.White
                MEP.SetError(tbBanco, "ingrese Dato en el campo Banco !".ToUpper)
                _ok = False
            Else
                tbBanco.BackColor = Color.White
                MEP.SetError(tbBanco, "")
            End If
        End If

        If cbTipPago1.Value = 2 Then
            If tbNroOpera.Text = String.Empty Then
                tbNroOpera.BackColor = Color.White
                MEP.SetError(tbNroOpera, "ingrese Dato en el campo Numero de Operación !".ToUpper)
                _ok = False
            Else
                tbNroOpera.BackColor = Color.White
                MEP.SetError(tbNroOpera, "")
            End If
            If tbBanco.Text = String.Empty Then
                tbBanco.BackColor = Color.White
                MEP.SetError(tbBanco, "ingrese Dato en el campo Banco !".ToUpper)
                _ok = False
            Else
                tbBanco.BackColor = Color.White
                MEP.SetError(tbBanco, "")
            End If
        End If

        If tbDescripcion.Text = String.Empty Then
            tbDescripcion.BackColor = Color.White
            MEP.SetError(tbDescripcion, "ingrese Dato en el campo Descripcion !".ToUpper)
            _ok = False
        Else
            tbDescripcion.BackColor = Color.White
            MEP.SetError(tbDescripcion, "")
        End If

        If tbRecibi.Text = String.Empty Then
            tbRecibi.BackColor = Color.White
            MEP.SetError(tbRecibi, "ingrese Dato en el campo Recibi !".ToUpper)
            _ok = False
        Else
            tbRecibi.BackColor = Color.White
            MEP.SetError(tbRecibi, "")
        End If

        If tbentregue.Text = String.Empty Then
            tbentregue.BackColor = Color.White
            MEP.SetError(tbentregue, "ingrese Dato en el campo Entrgue !".ToUpper)
            _ok = False
        Else
            tbentregue.BackColor = Color.White
            MEP.SetError(tbentregue, "")
        End If

        If (tbMonto.Value <= 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            MEP.SetError(tbMonto, "Por Favor introduzca monto !".ToUpper)
            _ok = False
        Else
            tbMonto.BackColor = Color.White
            MEP.SetError(tbMonto, "")
        End If


        MHighlighterFocus.UpdateHighlights()
        Return _ok
    End Function

    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelo.Celda)
        Dim listEstCeldas As New List(Of Modelo.Celda)


        listEstCeldas.Add(New Modelo.Celda("ienumi", True, "Codigo", 120))
        listEstCeldas.Add(New Modelo.Celda("ieFecha", True, "Fecha", 100))
        listEstCeldas.Add(New Modelo.Celda("ieTipo", False))
        listEstCeldas.Add(New Modelo.Celda("Tipo", True, "Tipo", 120))
        listEstCeldas.Add(New Modelo.Celda("ieDescripcion", True, "Descripción", 350))
        listEstCeldas.Add(New Modelo.Celda("ieConcepto", False))
        listEstCeldas.Add(New Modelo.Celda("ycdes3", True, "Concepto", 250))
        listEstCeldas.Add(New Modelo.Celda("ieMonto", True, "Monto", 150, "0.00"))
        listEstCeldas.Add(New Modelo.Celda("ieObs", False))
        listEstCeldas.Add(New Modelo.Celda("ieEstado", False))
        listEstCeldas.Add(New Modelo.Celda("ieIdCaja", False))
        listEstCeldas.Add(New Modelo.Celda("iefact", False))
        listEstCeldas.Add(New Modelo.Celda("iehact", False))
        listEstCeldas.Add(New Modelo.Celda("ieuact", False))
        listEstCeldas.Add(New Modelo.Celda("tafdoc", False))
        listEstCeldas.Add(New Modelo.Celda("ieIdAsig", False))
        ''listEstCeldas.Add(New Modelo.Celda("ObsAsignacion", False))
        listEstCeldas.Add(New Modelo.Celda("codCanero", False))
        listEstCeldas.Add(New Modelo.Celda("codIns", False))
        listEstCeldas.Add(New Modelo.Celda("NroCaja", True, "Nro. Caja", 100))
        listEstCeldas.Add(New Modelo.Celda("tcobservacion", False, "Obs Asignacion", 100))
        listEstCeldas.Add(New Modelo.Celda("codCan", False, "Id Asig", 100))
        listEstCeldas.Add(New Modelo.Celda("nombreCliente", False, "Id Asig", 100))
        listEstCeldas.Add(New Modelo.Celda("codIsnt", False, "Id Asig", 100))
        listEstCeldas.Add(New Modelo.Celda("nombreInst", False, "Id Asig", 100))
        listEstCeldas.Add(New Modelo.Celda("recibi", False, "Id Asig", 100))
        listEstCeldas.Add(New Modelo.Celda("entregue", False, "Id Asig", 100))
        listEstCeldas.Add(New Modelo.Celda("activoDisponible", False, "Id Asig", 100))
        listEstCeldas.Add(New Modelo.Celda("cuentaContable", False, "Id Asig", 100))
        listEstCeldas.Add(New Modelo.Celda("nOperacion", False, "Id Asig", 100))
        listEstCeldas.Add(New Modelo.Celda("nCheque", False, "Id Asig", 100))
        listEstCeldas.Add(New Modelo.Celda("banco", False, "Id Asig", 100))
        listEstCeldas.Add(New Modelo.Celda("swCliente", False, "Id Asig", 100))
        listEstCeldas.Add(New Modelo.Celda("swPago", False))
        listEstCeldas.Add(New Modelo.Celda("swMoneda", False, "Id Asig", 100))
        Return listEstCeldas

    End Function
    Public Overrides Function _PMOGetTablaBuscador() As DataTable
        Dim dtBuscador As DataTable = L_prIngresoEgresoGeneral()
        Return dtBuscador
    End Function


    Public Overrides Sub _PMOMostrarRegistro(_N As Integer)

        JGrM_Buscador.Row = _MPos

        't.canumi , t.canombre, t.cacuenta, t.caobs, t.cafact, t.cahact, t.cauact 
        With JGrM_Buscador
            tbcodigo.Text = .GetValue("ienumi").ToString
            tbIdCaja.Text = .GetValue("ieIdCaja").ToString
            dpFecha.Value = .GetValue("ieFecha")
            swTipo.Value = .GetValue("ieTipo")
            tbDescripcion.Text = .GetValue("ieDescripcion").ToString
            cbConcepto1.Value = .GetValue("ieConcepto")
            tbMonto.Value = .GetValue("ieMonto")
            tbObservacion.Text = .GetValue("ieObs").ToString
            lbNroCaja.Text = .GetValue("NroCaja")

            lbFecha.Text = CType(.GetValue("iefact"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("iehact").ToString
            lbUsuario.Text = .GetValue("ieuact").ToString
            tbcodCanero.Text = .GetValue("codCan").ToString
            tbcodInst.Text = .GetValue("codIsnt").ToString
            tbdescCanero.Text = .GetValue("nombreCliente").ToString
            tbInstitucion.Text = .GetValue("nombreInst").ToString
            tbfecha.Text = .GetValue("tafdoc").ToString


            tbRecibi.Text = .GetValue("recibi").ToString
            tbentregue.Text = .GetValue("entregue").ToString
            idActDis.Text = .GetValue("activoDisponible").ToString
            idCuenCont.Text = .GetValue("cuentaContable").ToString
            tbNroOpera.Text = .GetValue("nOperacion").ToString
            tbNroCheque.Text = .GetValue("nCheque").ToString
            tbBanco.Text = .GetValue("banco").ToString
            SwParticular.Value = .GetValue("swCliente").ToString
            cbTipPago1.Value = .GetValue("swPago")
            SwMoneda.Value = .GetValue("swMoneda").ToString
            idCanero.Text = .GetValue("codCanero").ToString
            idInstitucion.Text = .GetValue("codIns").ToString



            'diseño de la grilla para el Total
            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed
        End With
        With JGrM_Buscador.RootTable.Columns("ieMonto")
            .AggregateFunction = AggregateFunction.Sum
        End With

        LblPaginacion.Text = Str(_MPos + 1) + "/" + JGrM_Buscador.RowCount.ToString

    End Sub
    Public Overrides Function _PMOGrabarRegistro() As Boolean

        Dim tipo As Integer = IIf(swTipo.Value = True, 1, 0)
        Dim res As Boolean = L_prIngresoEgresoGrabar(tbcodigo.Text, dpFecha.Value, tipo, tbDescripcion.Text, cbConcepto1.Value, tbMonto.Value, tbObservacion.Text, gs_NroCaja, IIf(tbIdCaja.Text = "", 0, tbIdCaja.Text), "155",
                                                     idCanero.Text, tbdescCanero.Text, idInstitucion.Text, tbInstitucion.Text, tbRecibi.Text, tbentregue.Text, idActDis.Text, idCuenCont.Text, tbNroOpera.Text, tbNroCheque.Text, tbBanco.Text, SwParticular.Value, cbTipPago1.Value, SwMoneda.Value)
        If res Then
            imprimir(tbcodigo.Text)

            Modificado = False
            _PMOLimpiar()

            ToastNotification.Show(Me, "Codigo de Ingreso/Egreso".ToUpper + tbcodigo.Text + " Grabado con éxito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
        End If
        Return True

    End Function
    Public Overrides Function _PMOModificarRegistro() As Boolean
        Dim res As Boolean
        Dim tipo As Integer = IIf(swTipo.Value = True, 1, 0)
        If (Modificado = False) Then
            res = L_prIngresoEgresoModificar(tbcodigo.Text, dpFecha.Value, tipo, tbDescripcion.Text, cbConcepto1.Value, tbMonto.Value, tbObservacion.Text, idCanero.Text, tbdescCanero.Text, idInstitucion.Text, tbInstitucion.Text, tbRecibi.Text, tbentregue.Text, idActDis.Text, idCuenCont.Text, tbNroOpera.Text, tbNroCheque.Text, tbBanco.Text, SwParticular.Value, cbTipPago1.Value, SwMoneda.Value)

        Else
            res = L_prIngresoEgresoModificar(tbcodigo.Text, dpFecha.Value, tipo, tbDescripcion.Text, cbConcepto1.Value, tbMonto.Value, tbObservacion.Text)
        End If
        If res Then
            Modificado = False
            _PMInhabilitar()
            _PMPrimerRegistro()
            ToastNotification.Show(Me, "Codigo de Ingreso/Egreso".ToUpper + tbcodigo.Text + " modificado con éxito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
        End If
        Return res

    End Function


    Public Overrides Sub _PMOEliminarRegistro()
        'If tbIdCaja.Text = 0 Then
        Dim info As New TaskDialogInfo("¿esta seguro de eliminar el registro?".ToUpper, eTaskDialogIcon.Delete, "advertencia".ToUpper, "mensaje principal".ToUpper, eTaskDialogButton.Yes Or eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.Blue)
            Dim result As eTaskDialogResult = TaskDialog.Show(info)
            If result = eTaskDialogResult.Yes Then
                Dim mensajeError As String = ""
                Dim res As Boolean = L_prIngresoEgresoBorrar(tbcodigo.Text, mensajeError)
                If res Then
                    ToastNotification.Show(Me, "Codigo de Ingreso/Egreso ".ToUpper + tbcodigo.Text + " eliminado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                    _PMFiltrar()
                Else
                    ToastNotification.Show(Me, mensajeError, My.Resources.WARNING, 8000, eToastGlowColor.Red, eToastPosition.TopCenter)
                End If
            End If
        'Else
        'ToastNotification.Show(Me, "No puede Eliminar un Ingreso/Egreso, ya se hizo cierre de caja, por favor primero elimine cierre de caja".ToUpper, My.Resources.WARNING, 5000, eToastGlowColor.Red, eToastPosition.TopCenter)
        'End If
    End Sub


#End Region
#Region "EVENTOS"
    Private Sub F1_IngresosEgresos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()

    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        tbDescripcion.Focus()
        nuevo = 1
    End Sub
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        If btnGrabar.Enabled = True Then
            _PMInhabilitar()
            _PMPrimerRegistro()
            PanelNavegacion.Enabled = True
        Else
            Close()
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

    Private Sub btConcepto_Click(sender As Object, e As EventArgs) Handles btConcepto.Click
        Dim numi As String = ""

        If L_prLibreriaGrabar(numi, "9", "1", cbConcepto1.Text, "") Then
            _prCargarComboLibreria(cbConcepto1, "9", "1")
            cbConcepto1.SelectedIndex = CType(cbConcepto1.DataSource, DataTable).Rows.Count - 1
        End If
    End Sub

    Private Sub cbConcepto1_ValueChanged(sender As Object, e As EventArgs) Handles cbConcepto1.ValueChanged
        If cbConcepto1.SelectedIndex < 0 And cbConcepto1.Text <> String.Empty Then
            btConcepto.Visible = True
        Else
            btConcepto.Visible = False
        End If
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        'If tbIdCaja.Text > 0 Then
        '    ToastNotification.Show(Me, "No puede Modificar un Ingreso/Egreso, ya se hizo cierre de caja, por favor primero elimine cierre de caja".ToUpper, My.Resources.WARNING, 5000, eToastGlowColor.Red, eToastPosition.TopCenter)
        '    btnNuevo.Enabled = True
        '    btnModificar.Enabled = True
        '    btnGrabar.Enabled = False
        '    btnEliminar.Enabled = True
        '    _PMInhabilitar()
        '    '_PMFiltrar()
        '    'Exit Sub
        'End If
        _PMOHabilitar()
    End Sub

    Private Sub TextBoxX3_TextChanged(sender As Object, e As EventArgs) Handles tbcodCanero.TextChanged

    End Sub

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles btnVentCobros.Click
        Dim frm As New F_listaVentasCobrar
        'Dim frm2 As New F1_AsignacionCuentas


        frm.ShowDialog()
        'frm2.ShowDialog()

        tbIdCaja.Text = frm.Codigo
        tbfecha.Text = frm.Fecha
        tbMonto.Text = frm.Total
        tbcodCanero.Text = frm.Codcanero
        tbdescCanero.Text = frm.Canero
        tbcodInst.Text = frm.Codinstitucion
        tbInstitucion.Text = frm.Institucion
        tbDescripcion.Text = "PAGO POR VTA DE INSUMOS S/O " + frm.Codigo
        'tbAsigDesc.Text = frm2.tbObservacion.Text
        'tbAsigDesc.Text = frm2.Observacion
        'tbCodAsig.Text = frm2.CodAsig
        btnVentCobros.Enabled = False
    End Sub

    Private Sub swTipo_ValueChanged(sender As Object, e As EventArgs) Handles swTipo.ValueChanged
        If swTipo.Value = True Then


        Else
            btnVentCobros.Enabled = False
            tbIdCaja.Text = ""
            tbfecha.Text = ""
            tbMonto.Text = ""
            tbcodCanero.Text = ""
            tbdescCanero.Text = ""
            tbcodInst.Text = ""
            tbInstitucion.Text = ""
            tbDescripcion.Text = ""


        End If
    End Sub
    Private Sub imprimir(cod As String)

        Dim dt As DataTable
        If swTipo.Value = True Then
            dt = L_recibo(cod)


            'Literal 
            Dim _Autorizacion, _Nit, _Fechainv, _Total, _Key, _Cod_Control, _Hora,
            _Literal, _TotalDecimal, _TotalDecimal2 As String
            Dim _TotalLi As Decimal
            Dim monedaLiteral As String
            _TotalLi = Format(tbMonto.Value, "0.00")
            _TotalDecimal = _TotalLi - Math.Truncate(_TotalLi)
            _TotalDecimal2 = CDbl(_TotalDecimal) * 100

            'Dim li As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_Total) - CDbl(_TotalDecimal)) + " con " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + "/100 Bolivianos"
            If SwMoneda.Value = True Then
                monedaLiteral = "/100 DOLARES AMERICANOS"
            Else
                monedaLiteral = "/100 BOLIVIANOS"
            End If
            _Literal = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(_TotalLi) - CDbl(_TotalDecimal)) + "  " + IIf(_TotalDecimal2.Equals("0"), "00", _TotalDecimal2) + monedaLiteral
            P_Global.Visualizador = New Visualizador
            Dim objrep As New R_reciboIngreso


            objrep.SetDataSource(dt)
            objrep.SetParameterValue("literal", _Literal.ToUpper)
            objrep.SetParameterValue("fecha", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"))
            objrep.SetParameterValue("fechaLiteral", DateTime.Now.ToLongDateString())
            P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
            P_Global.Visualizador.ShowDialog() 'Comentar
            P_Global.Visualizador.BringToFront()
        Else

        End If


    End Sub
    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        imprimir(tbcodigo.Text)
    End Sub

    Private Sub cbActivo_ValueChanged(sender As Object, e As EventArgs) Handles cbActivo.ValueChanged
        _prCargarComboCuentasContables(cbCuenta)
    End Sub
    Private Sub _prCargarComboCuentasContables(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim Finan As Integer = cbActivo.Value
        codActivo.Text = Finan
        Dim dt As New DataTable
        dt = L_fnListarCuentaContable(Finan)

        'a.ylcod1 ,a.yldes1 
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("canumi").Width = 70
            .DropDownList.Columns("canumi").Caption = "COD"
            .DropDownList.Columns.Add("cadesc").Width = mCombo.Width - 70
            .DropDownList.Columns("cadesc").Caption = "DESCRIPCION"
            .ValueMember = "canumi"
            .DisplayMember = "cadesc"
            .DataSource = dt
            .Refresh()
        End With
        If (CType(cbCuenta.DataSource, DataTable).Rows.Count > 0) Then
            cbCuenta.SelectedIndex = 0
        End If
    End Sub

    Private Sub cbCuenta_ValueChanged(sender As Object, e As EventArgs) Handles cbCuenta.ValueChanged
        Dim Pres As String = cbCuenta.Value
        codCuenta.Text = Pres
    End Sub

    Private Sub SwParticular_ValueChanged(sender As Object, e As EventArgs) Handles SwParticular.ValueChanged
        If SwParticular.Value = True And btnGrabar.Enabled = True Then

            idInstitucion.Text = "73"
            tbcodInst.Text = "888"
            tbInstitucion.Text = "PARTICULARES (NO CAÑEROS)"
            tbdescCanero.ReadOnly = False
            btnVentCobros.Enabled = False
            CheckBox1.Checked = False
            tbdescCanero.Text = ""
            tbcodCanero.Text = ""
        Else
            If btnGrabar.Enabled = True Then
                tbcodInst.Text = ""
                tbInstitucion.Text = ""
                tbdescCanero.Text = ""
                tbcodCanero.Text = ""

                tbdescCanero.ReadOnly = True

            End If

        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            If swTipo.Value = True Then
                If SwParticular.Value = False Then
                    If btnGrabar.Enabled = True Then
                        btnVentCobros.Enabled = True
                        tbcodCanero.Text = ""
                        tbdescCanero.Text = ""
                        tbcodInst.Text = ""
                        tbInstitucion.Text = ""
                    End If
                End If
                End If
        Else
            btnVentCobros.Enabled = False
        End If

    End Sub

    Private Sub tbdescCanero_KeyDown(sender As Object, e As KeyEventArgs) Handles tbdescCanero.KeyDown
        If e.KeyData = Keys.Control + Keys.Enter Then
            If CheckBox1.Checked = False And SwParticular.Value = False Then
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
                    tbcodCanero.Text = Row.Cells("ydcod").Value
                    idCanero.Text = Row.Cells("ydnumi").Value
                    tbdescCanero.Text = Row.Cells("ydrazonsocial").Value
                    '_dias = Row.Cells("yddias").Value
                    'tbNit.Text = Row.Cells("ydnit").Value
                    'TbNombre1.Text = Row.Cells("ydnomfac").Value
                    'tipoDocumento = Row.Cells("ydtipdocelec").Value
                    'correo = Row.Cells("ydcorreo").Value
                    'tbComplemento.Text = Row.Cells("ydcompleCi").Value
                    Dim numiVendedor As Integer = IIf(IsDBNull(Row.Cells("ydnumivend").Value), 0, Row.Cells("ydnumivend").Value)

                    Dim dt1 As DataTable
                        dt1 = L_fnListarCaneroInstitucion(Row.Cells("ydnumi").Value)
                    Dim row1 As DataRow = dt1.Rows(dt1.Rows.Count - 1)
                    tbInstitucion.Text = row1("institucion")
                    idInstitucion.Text = row1("id")
                    tbcodInst.Text = row1("codInst")

                    If (numiVendedor > 0) Then
                        ''tbVendedor.Text = Row.Cells("vendedor").Value
                        ' _CodEmpleado = Row.Cells("ydnumivend").Value

                        ' grdetalle.Select()
                        'Table_Producto = Nothing
                    Else
                        'tbVendedor.Clear()
                        '_CodEmpleado = 0
                        'tbVendedor.Focus()
                        'Table_Producto = Nothing

                    End If
                End If
            End If

        End If
    End Sub


#End Region
End Class