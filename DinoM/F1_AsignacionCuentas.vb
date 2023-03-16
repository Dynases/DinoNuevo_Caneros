Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX
Imports System.IO
Imports DevComponents.DotNetBar.Controls

Public Class F1_AsignacionCuentas
    Dim _Inter As Integer = 0

#Region "Variables Locales"

    Dim RutaGlobal As String = gs_CarpetaRaiz
    Dim RutaTemporal As String = "C:\Temporal"
    Dim Modificado As Boolean = False
    Dim nameImg As String = "Default.jpg"
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
#End Region
#Region "Metodos Privados"
    Private Sub _prIniciarTodo()
        _prCargarComboCompania(cbActivo)
        '_prCargarComboCuentasContables(cbCuenta)
        Me.Text = "ASIGNACION DE CUENTAS"
        Me.tbCodAsig.ReadOnly = True


        _PMIniciarTodo()
    End Sub

    Private Sub _prCargarComboCompania(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
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

    Public Sub _prStyleJanus()
        GroupPanelBuscador.Style.BackColor = Color.FromArgb(13, 71, 161)
        GroupPanelBuscador.Style.BackColor2 = Color.FromArgb(13, 71, 161)
        GroupPanelBuscador.Style.TextColor = Color.White
        JGrM_Buscador.RootTable.HeaderFormatStyle.FontBold = TriState.True
    End Sub
#End Region


    Public Overrides Sub _PMOHabilitar()
        cbActivo.ReadOnly = False
        cbCuenta.ReadOnly = False

        tbNroOpera.ReadOnly = False
        tbNroCheque.ReadOnly = False
        tbBanco.ReadOnly = False
        tbObservacion.ReadOnly = False



        cbActivo.Focus()
    End Sub

    Public Overrides Sub _PMOInhabilitar()
        cbActivo.ReadOnly = True
        cbCuenta.ReadOnly = True


        tbNroOpera.ReadOnly = True
        tbNroCheque.ReadOnly = True
        tbBanco.ReadOnly = True
        tbObservacion.ReadOnly = True


        JGrM_Buscador.Focus()
        ' SuperTabItem1.Visible = False
    End Sub

    Public Overrides Sub _PMOLimpiar()
        ' If (CType(cbActivo.DataSource, DataTable).Rows.Count > 0) Then
        ' cbActivo.SelectedIndex = 0
        'End If
        'If (CType(cbCuenta.DataSource, DataTable).Rows.Count > 0) Then
        ' cbCuenta.SelectedIndex = 0
        'End If
        codActivo.Text = ""
        cbActivo.Text = ""
        codCuenta.Text = ""
        cbCuenta.Text = ""
        tbNroOpera.Clear()
        tbNroCheque.Clear()
        tbBanco.Clear()
        tbObservacion.Clear()
        tbCodAsig.Text = ""
        tbCodIng2.Text = ""

    End Sub

    Public Overrides Sub _PMOLimpiarErrores()
        MEP.Clear()
        cbActivo.BackColor = Color.White
        cbCuenta.BackColor = Color.White
        tbNroOpera.BackColor = Color.White
        tbNroCheque.BackColor = Color.White
        tbBanco.BackColor = Color.White
        tbObservacion.BackColor = Color.White
        
    End Sub
    Public Overrides Function _PMOValidarCampos() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()
        If cbActivo.Text = "" Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            MEP.SetError(cbActivo, "Selecione un Activo Disponible !".ToUpper)
            _ok = False
        End If
        If cbCuenta.Text = "" Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            MEP.SetError(cbCuenta, "Selecione una Cuenta !".ToUpper)
            _ok = False
        End If

        MHighlighterFocus.UpdateHighlights()
        Return _ok
    End Function


    Public Overrides Function _PMOGetTablaBuscador() As DataTable
        Dim dtBuscador As DataTable = L_fnGeneralAsigCuenta()
        Return dtBuscador
    End Function
    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelo.Celda)
        Dim listEstCeldas As New List(Of Modelo.Celda)
        'a.aanumi ,a.aabdes ,a.aadir ,a.aatel ,a.aalat ,a.aalong ,a.aaimg,aata2dep ,a.aafact ,a.aahact ,a.aauact

        listEstCeldas.Add(New Modelo.Celda("tcnumi", True, "Código".ToUpper, 80))
        listEstCeldas.Add(New Modelo.Celda("tccuen", False, "Cuentas".ToUpper, 80))
        listEstCeldas.Add(New Modelo.Celda("cadesc", True, "Banco".ToUpper, 300))
        listEstCeldas.Add(New Modelo.Celda("tcnumcheq", True, "Nro Cheque".ToUpper, 150))
        listEstCeldas.Add(New Modelo.Celda("tcbanco", True, "Banco".ToUpper, 150))
        listEstCeldas.Add(New Modelo.Celda("tcnumoper", True, "Nro Operacion".ToUpper, 150))
        listEstCeldas.Add(New Modelo.Celda("tcobservacion", True, "Observacion".ToUpper, 300))
        listEstCeldas.Add(New Modelo.Celda("tcorden", True, "Nro Orden".ToUpper, 300))
        listEstCeldas.Add(New Modelo.Celda("tcfact", False))
        listEstCeldas.Add(New Modelo.Celda("tchact", False))
        listEstCeldas.Add(New Modelo.Celda("tcuact", False))
        Return listEstCeldas
    End Function


    Public Overrides Sub _PMOMostrarRegistro(_N As Integer)
        JGrM_Buscador.Row = _MPos
        'a.aanumi ,a.aabdes ,a.aadir ,a.aatel ,a.aalat ,a.aalong ,a.aaimg,aata2dep ,a.aafact ,a.aahact ,a.aauact
        Dim dt As DataTable = CType(JGrM_Buscador.DataSource, DataTable)
        Try
            tbCodAsig.Text = JGrM_Buscador.GetValue("tcnumi").ToString
        Catch ex As Exception
            Exit Sub
        End Try
        With JGrM_Buscador
            _codasig = .GetValue("tcnumi").ToString
            codActivo.Text = .GetValue("capadre").ToString
            cbActivo.Value = .GetValue("capadre").ToString
            codCuenta.Text = .GetValue("tccuen").ToString
            cbCuenta.Text = .GetValue("cadesc").ToString

            tbNroCheque.Text = .GetValue("tcnumcheq")
            tbBanco.Text = .GetValue("tcbanco").ToString
            tbNroOpera.Text = .GetValue("tcnumoper")
            _observacion = .GetValue("tcobservacion")
            tbObservacion.Text = .GetValue("tcobservacion").ToString
            lbFecha.Text = CType(.GetValue("tcfact"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("tchact").ToString
            lbUsuario.Text = .GetValue("tcuact").ToString
            tbCodIng2.Text = .GetValue("tcorden").ToString

        End With
        LblPaginacion.Text = Str(_MPos + 1) + "/" + JGrM_Buscador.RowCount.ToString
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

    Private Sub LabelX4_Click(sender As Object, e As EventArgs) Handles LabelX4.Click

    End Sub

    Private Sub MPanelSup_Paint(sender As Object, e As PaintEventArgs) Handles MPanelSup.Paint

    End Sub

    Private Sub F1_AsignacionCuentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        StartPosition = FormStartPosition.CenterParent
        _prIniciarTodo()
        _PMOLimpiar()
        btnNuevo.PerformClick()

    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click


        If btnGrabar.Enabled = True Then
            _PMInhabilitar()
            '_PMPrimerRegistro()

        Else
            '  Public _modulo As SideNavItem
            '_modulo.Select()
            Me.Close()
        End If
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        _PMOLimpiar()

    End Sub

    Private Sub cbActivo_ValueChanged(sender As Object, e As EventArgs) Handles cbActivo.ValueChanged
        _prCargarComboCuentasContables(cbCuenta)
        If cbActivo.Value = 100 Then
        Else
        End If
    End Sub

    Private Sub cbCuenta_ValueChanged(sender As Object, e As EventArgs) Handles cbCuenta.ValueChanged
        Dim Pres As String = cbCuenta.Value
        codCuenta.Text = Pres
        'CargarInteres(Pres)
    End Sub


    Public Overrides Function _PMOGrabarRegistro() As Boolean


        Dim res As Boolean = L_fnGrabarAsignacion(tbCodAsig.Text, codCuenta.Text, tbNroCheque.Text, tbBanco.Text, tbNroOpera.Text, tbObservacion.Text, tbCodIng2.Text)
        If res Then

            Modificado = False
            '_fnMoverImagenRuta(RutaGlobal + "\Imagenes\Imagenes Banco", nameImg)
            'nameImg = "Default.jpg"

            _PMOLimpiar()
            ToastNotification.Show(Me, "Codigo de BANCO ".ToUpper + tbCodAsig.Text + " Grabado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)

            Me.Close()
        End If
        Return res
 
    End Function
    Public Overrides Function _PMOModificarRegistro() As Boolean
        Dim tipo As Integer = 1
        Dim nsoc As Integer = 1
        Dim res As Boolean

        If (Modificado = False) Then
            res = L_prAsignacionModificar(tbCodAsig.Text, codCuenta.Text, tbNroCheque.Text, tbBanco.Text, tbNroOpera.Text, tbObservacion.Text)

        Else
            res = L_prAsignacionModificar(tbCodAsig.Text, codCuenta.Text, tbNroCheque.Text, tbBanco.Text, tbNroOpera.Text, tbObservacion.Text)
        End If

        If res Then



            Modificado = False
            _PMInhabilitar()
            _PMPrimerRegistro()
            ToastNotification.Show(Me, "Codigo de BANCO ".ToUpper + tbCodAsig.Text + " modificado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
        End If
        Return res
    End Function


    Public Overrides Sub _PMOEliminarRegistro()
        Dim info As New TaskDialogInfo("¿esta seguro de eliminar el registro?".ToUpper, eTaskDialogIcon.Delete, "advertencia".ToUpper, "mensaje principal".ToUpper, eTaskDialogButton.Yes Or eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.Blue)
        Dim result As eTaskDialogResult = TaskDialog.Show(info)
        If result = eTaskDialogResult.Yes Then
            Dim mensajeError As String = ""
            Dim res As Boolean = L_prAsignacionBorrar(tbCodAsig.Text, mensajeError)
            If res Then

                '_PrEliminarImage()
                ToastNotification.Show(Me, "Codigo de Banco ".ToUpper + tbCodAsig.Text + " eliminado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                _PMFiltrar()
            Else
                ToastNotification.Show(Me, mensajeError, My.Resources.WARNING, 8000, eToastGlowColor.Red, eToastPosition.TopCenter)
            End If
        End If
    End Sub



    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click


    End Sub

    Private Sub tbBanco_TextChanged(sender As Object, e As EventArgs) Handles tbBanco.TextChanged

    End Sub

    Private Sub tbCodIng_TextChanged(sender As Object, e As EventArgs)


    End Sub
    Private _codasig As String
    Public ReadOnly Property CodAsig As String
        Get
            Return _codasig
        End Get
    End Property

    Private _observacion As String
    Public ReadOnly Property Observacion As String
        Get
            Return _observacion
        End Get
    End Property
    Private Sub JGrM_Buscador_DoubleClick(sender As Object, e As EventArgs) Handles JGrM_Buscador.DoubleClick
        Dim frm As New F1_IngresosEgresos()
        Dim frm2 As New F_listaVentasCobrar()

        'frm.Show()
        If JGrM_Buscador.Row >= 0 Then
            _PMOMostrarRegistro(JGrM_Buscador.Row)

        End If


        Me.Close()

    End Sub


End Class