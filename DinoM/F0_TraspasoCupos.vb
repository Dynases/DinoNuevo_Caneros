Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Public Class F0_TraspasoCupos
    Dim _Inter As Integer = 0
    Dim NumiCuenta As Integer
    Dim _codInsti As Integer = 0
#Region "ATRIBUTOS"
    Dim _Dsencabezado As DataSet
    Dim _CodCanerotransfirienteSys As Integer = 0
    Dim _CodCanerotransreceptorSys As Integer = 0
    Dim _Nuevo As Boolean
    Private _Pos As Integer
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Public _nameButton As String
    Dim NumiVendedor As Integer = 0
    Dim _CodEmpleado As Integer = 0
#End Region

#Region "METODOS PRIVADOS"
    Private Sub _PIniciarTodo()

        Me.Text = "TRASPASO DE CUPOS"
        'Me.WindowState = FormWindowState.Maximized


        _MaxLengthTextBox()

        _PCargarBuscador()
        _PInhabilitar()
        _PHabilitarFocus()
        _prAsignarPermisos()
        Dim blah As New Bitmap(New Bitmap(My.Resources.user), 20, 20)


        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        GroupPanel1.Style.BackColor = Color.FromArgb(13, 71, 161)
        GroupPanel1.Style.BackColor2 = Color.FromArgb(13, 71, 161)
        GroupPanel1.Style.TextColor = Color.White
        JGr_Buscador.Focus()
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
    Public Sub _MaxLengthTextBox()

    End Sub


    Private Sub _PFiltrar()
        _Dsencabezado = New DataSet
        _Dsencabezado = L_Usuario_General2(0)
        _Pos = 0

        If _Dsencabezado.Tables(0).Rows.Count <> 0 Then
            _PMostrarRegistro(0)
            LblPaginacion.Text = Str(1) + "/" + _Dsencabezado.Tables(0).Rows.Count.ToString
            If _Dsencabezado.Tables(0).Rows.Count > 0 Then
                btnPrimero.Visible = True
                btnAnterior.Visible = True
                btnSiguiente.Visible = True
                btnUltimo.Visible = True
            End If
        End If
    End Sub

    Private Sub _PMostrarRegistro(_N As Integer)
        Dim dt As DataTable = CType(JGr_Buscador.DataSource, DataTable)
        If (IsNothing(CType(JGr_Buscador.DataSource, DataTable))) Then
            Return
        End If
        With JGr_Buscador
            tb_Id.Text = .GetValue("idTraspCupo").ToString
            TbNombre1.Text = .GetValue("codCaneroUCG").ToString
            tbCliente.Text = .GetValue("ydRazonSocial").ToString
            tbNit.Text = .GetValue("codInst").ToString
            tbCupoResgistrado.Text = .GetValue("cupoRegAntT").ToString
            tbVendedor.Text = .GetValue("nomInst").ToString
            DoubleInput1.Value = .GetValue("cupoTransferirT").ToString
            DoubleInput4.Text = .GetValue("cupoRegNuevT").ToString
            TextBoxX1.Text = .GetValue("codCaneroUCGR").ToString
            tbClienteR.Text = .GetValue("caneroUCG").ToString
            tbNitR.Text = .GetValue("codInstR").ToString
            tbVendedorR.Text = .GetValue("nomInstR").ToString
            tbCupoRegistradoR.Text = .GetValue("cupoRegAntR").ToString
            DoubleInput2.Text = .GetValue("cupoRegNuevR").ToString
            CbGestion.Text = .GetValue("gestion").ToString
            DoubleInput3.Text = .GetValue("porcentaje").ToString
            tbFechaVenc.Text = .GetValue("fechaReg").ToString
            tbCupoActual.Text = .GetValue("cupoLibreTransferirT").ToString
            'NumiCuenta = .GetValue("canumi").ToString

        End With
    End Sub

    Private Sub _PInhabilitar()
        Tb_Id.ReadOnly = True

        ' tbFechaVenc.Enabled = False


        btnNuevo.Enabled = True
        btnModificar.Enabled = True
        btnEliminar.Enabled = True
        btnGrabar.Enabled = False
        'DoubleInput1.Enabled = False
        CbGestion.Enabled = False
        JGr_Buscador.Enabled = True

        btnGrabar.Image = My.Resources.save

        _PLimpiarErrores()
    End Sub

    Private Sub _PLimpiarErrores()
        MEP.Clear()


    End Sub

    Private Sub _PHabilitarFocus()
        'MHighlighterFocus.SetHighlightOnFocus(Tb_CodInst, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        'MHighlighterFocus.SetHighlightOnFocus(Tb_NomInst, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        'MHighlighterFocus.SetHighlightOnFocus(Tb_Telefono, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        'MHighlighterFocus.SetHighlightOnFocus(Tb_Direccion, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        'Tb_CodInst.TabIndex = 1
        'Tb_NomInst.TabIndex = 2
        'Tb_Telefono.TabIndex = 3
        'Tb_Direccion.TabIndex = 4

    End Sub

    Private Sub _PCargarBuscador()
        _Dsencabezado = New DataSet
        _Dsencabezado = L_TraspasoCupos()

        JGr_Buscador.BoundMode = BoundMode.Bound
        JGr_Buscador.DataSource = _Dsencabezado.Tables(0) ' _Dsencabezado.Tables(0) ' dt
        JGr_Buscador.RetrieveStructure()

        With JGr_Buscador.RootTable.Columns("idTraspCupo")
            .Visible = True
            .Caption = "Cod.trasp".ToUpper
            .Width = 30
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        End With
        With JGr_Buscador.RootTable.Columns("codCaneroSys")
            .Visible = False
            .Caption = "Cod. Institución".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        End With
        With JGr_Buscador.RootTable.Columns("codCaneroUcg")
            .Visible = True
            .Caption = "COD CANERO".ToUpper
            .Width = 100
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

        End With
        With JGr_Buscador.RootTable.Columns("ydRazonSocial")
            .Visible = True
            .Caption = "CANERO TRANSFIRIENTE".ToUpper
            .Width = 200
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

        End With
        With JGr_Buscador.RootTable.Columns("codInst")
            .Visible = False
            .Caption = "Cod.Cuenta".ToUpper
            .Width = 400
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

        End With

        With JGr_Buscador.RootTable.Columns("nomInst")
            .Visible = False
            .Caption = "Dirección".ToUpper
            .Width = 200
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.FontSize = gi_fuenteTamano
        End With

        With JGr_Buscador.RootTable.Columns("cupoRegAntT")
            .Caption = "CUPOANTERIOR".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

        End With

        With JGr_Buscador.RootTable.Columns("cupoTransferirT")
            .Caption = "CUPO transferir".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

        End With
        With JGr_Buscador.RootTable.Columns("cupoRegNuevT")
            .Caption = "CUPO R Nuev".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

        End With
        With JGr_Buscador.RootTable.Columns("codCaneroSysR")
            .Visible = False
            .Caption = "CodCanRecep".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

        End With

        With JGr_Buscador.RootTable.Columns("codCaneroUCGR")
            .Caption = "CodCanero".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

        End With

        With JGr_Buscador.RootTable.Columns("codInstR")
            .Visible = False
            .Caption = "CodCanRecep".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

        End With
        With JGr_Buscador.RootTable.Columns("nomInstR")
            .Visible = False
            .Caption = "CodCanRecep".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

        End With
        With JGr_Buscador.RootTable.Columns("porcentaje")
            .Visible = False
            .Caption = "CodCanRecep".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

        End With
        With JGr_Buscador.RootTable.Columns("cupoLibreTransferirT")
            .Visible = False
            .Caption = "CodCanRecep".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

        End With
        With JGr_Buscador.RootTable.Columns("cupoRegAntR")
            .Visible = True
            .Caption = "CodCanRecep".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

        End With

        With JGr_Buscador.RootTable.Columns("cupoRegNuevR")
            .Visible = True
            .Caption = "Cupo Nuevo".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

        End With
        With JGr_Buscador.RootTable.Columns("gestion")
            .Visible = True
            .Caption = "Gestion".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

        End With

        With JGr_Buscador.RootTable.Columns("fechaReg")
            .Visible = True
            .Caption = "Fecha".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

        End With
        'Habilitar Filtradores
        With JGr_Buscador
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False

            'diseño de la grilla
            JGr_Buscador.VisualStyle = VisualStyle.Office2007
        End With
    End Sub

#End Region
#Region " Metodo-Button "
    Private Sub _PHabilitar()


        DoubleInput1.IsInputReadOnly = False
        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
        btnGrabar.Enabled = True
        btnAgregar.Enabled = True
        TbNombre1.ReadOnly = False
        TextBoxX1.ReadOnly = False
        tbFechaVenc.Enabled = True
    End Sub

    Private Sub _PLimpiar()
        Tb_Id.Text = String.Empty
        tbCliente.Text = String.Empty
        TbNombre1.Text = String.Empty
        tbNit.Text = String.Empty
        tbVendedor.Text = String.Empty
        tbCupoResgistrado.Text = String.Empty
        tbCupoActual.Text = String.Empty
        DoubleInput1.Value = 0
        tbFechaVenc.Text = Now.Date
        TextBoxX1.Text = String.Empty
        tbClienteR.Text = String.Empty
        tbNitR.Text = String.Empty
        tbVendedorR.Text = String.Empty
        tbCupoRegistradoR.Text = String.Empty
        DoubleInput2.Value = 0
        DoubleInput3.Value = 0
        DoubleInput4.Value = 0
        LblPaginacion.Text = String.Empty
    End Sub

    Public Function P_Validar() As Boolean
        Dim _Error As Boolean = True
        MEP.Clear()
        If TbNombre1.Text = String.Empty Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "PRIMERO DEBE SELECCIONAR AL CAÑERO TRANSFIRIENTE".ToUpper, img, 10000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            _Error = False
        End If
        If DoubleInput1.Text = String.Empty Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "DEBE COLOCAR UN VALOR DE CUPO A TRANSFERIR".ToUpper, img, 10000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            _Error = False
        End If
        If DoubleInput1.Value <= 0 Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "DEBE COLOCAR UN VALOR MAYOR A CERO DE CUPO A TRANSFERIR".ToUpper, img, 10000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            _Error = False
        End If
        If tbCupoRegistradoR.Text = String.Empty Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "PRIMERO DEBE SELECCIONAR AL CAÑERO RECEPTOR".ToUpper, img, 10000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            _Error = False
        End If
        If DoubleInput1.Value > tbCupoActual.Text Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "NO PUEDE TRANSFERIR UN CUPO MAYOR AL CUPO LIBRE DE TRANSFERENCIA".ToUpper, img, 10000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            _Error = False
        End If
        If DoubleInput1.Value = 0 Or DoubleInput2.Value = 0 Or DoubleInput3.Value = 0 Or DoubleInput4.Value = 0 Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "PRIMERO DEBE PRESIONAR EN TRASPASO".ToUpper, img, 10000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            _Error = False
        End If

        Return _Error
    End Function


#End Region
#Region " Cancelar-Button "
    Private Sub BBtn_Cancelar_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _PSalirRegistro()
    End Sub

    Private Sub _PSalirRegistro()
        If btnGrabar.Enabled = True Then
            _PCargarBuscador()

            _PLimpiar()
            _PInhabilitar()
            _PFiltrar()

        Else
            _modulo.Select()
            Me.Close()

        End If
    End Sub
#End Region

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        _PNuevoRegistro()
        _prCargarComboLibreria(CbGestion)
        JGr_Buscador.Enabled = False
    End Sub

    Private Sub _prCargarComboLibreria(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = traerGestionTraspasoCupo()
        With mCombo
            .DropDownList.Columns.Clear()

            .DropDownList.Columns.Add("ycdes3").Width = 200
            .DropDownList.Columns("ycdes3").Caption = "DESCRIPCION"
            .ValueMember = "ycdes3"
            .DisplayMember = "ycdes3"
            .DataSource = dt
            .Refresh()
        End With
    End Sub
    Private Sub _PNuevoRegistro()
        _PHabilitar()
        ' btnNuevo.Enabled = True

        _PLimpiar()

        _Nuevo = True
    End Sub

    Private Sub F0_Instituciones_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _PIniciarTodo()

    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        _PGrabarRegistro()
    End Sub
    Private Sub _PGrabarRegistro()
        Dim _Error As Boolean = False
        _Error = P_Validar()
        If _Error = True Then

            If _Nuevo Then

                'L_Institucion_Grabar(Tb_CodInst.Text, Tb_NomInst.Text, Tb_Telefono.Text, Tb_Direccion.Text)

                'Tb_CodInst.Focus()
                Dim numi As String = ""
                Dim res As Boolean = TraspasoCupo_Grabar(numi, _CodCanerotransfirienteSys, tbCupoResgistrado.Text, DoubleInput1.Value, DoubleInput4.Value,
                                                         _CodCanerotransreceptorSys, tbCupoRegistradoR.Text, DoubleInput2.Value, Year(tbFechaVenc.Text), tbFechaVenc.Text, DoubleInput3.Value, tbCupoActual.Text)
                If res = True Then
                    ToastNotification.Show(Me, "TRASPASO GRABADO CON EXITO.".ToUpper, My.Resources.GRABACION_EXITOSA, 8000, eToastGlowColor.Green, eToastPosition.TopCenter)
                    _PCargarBuscador()
                    _PInhabilitar()
                End If

            Else

                'L_Institucion_Modificar(Tb_Id.Text, Tb_CodInst.Text, Tb_NomInst.Text, Tb_Telefono.Text, Tb_Direccion.Text, NumiCuenta, 0, 0)

                ToastNotification.Show(Me, "Codigo Institución ".ToUpper + tb_Id.Text + " Modificado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)

                _PCargarBuscador()
                _Nuevo = False 'aumentado danny
                _PInhabilitar()
                _PFiltrar()
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

    Private Sub JGr_Buscador_SelectionChanged(sender As Object, e As EventArgs) Handles JGr_Buscador.SelectionChanged
        If JGr_Buscador.Row >= 0 Then
            _PMostrarRegistro(JGr_Buscador.Row)
            LblPaginacion.Text = Str(JGr_Buscador.Row + 1) + "/" + _Dsencabezado.Tables(0).Rows.Count.ToString
        End If
    End Sub

    Private Sub JGr_Buscador_EditingCell(sender As Object, e As EditingCellEventArgs) Handles JGr_Buscador.EditingCell
        e.Cancel = True
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        _PModificarRegistro()
        JGr_Buscador.Enabled = False
    End Sub
    Private Sub _PModificarRegistro()
        _Nuevo = False
        _PHabilitar()
        '_codInsti = Tb_CodInst.Text
        'btnModificar.Enabled = True 'aumentado para q funcione con el modelo de guido
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        _PEliminarRegistro()
    End Sub
    Private Sub _PEliminarRegistro()
        Dim ef = New Efecto


        ef.tipo = 2
        ef.Context = "¿esta seguro de eliminar el registro?".ToUpper
        ef.Header = "mensaje principal".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            Dim t As String = Tb_Id.Text
            L_Institucion_Borrar(Tb_Id.Text)

            _PInhabilitar()
            _PFiltrar()
            _PCargarBuscador()

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)

            ToastNotification.Show(Me, "Código de Institución ".ToUpper + t + " eliminado con Exito.".ToUpper,
                                              img, 2000,
                                              eToastGlowColor.Green,
                                              eToastPosition.TopCenter)

        Else
            _PInhabilitar()
        End If

    End Sub

    Private Sub Tb_Telefono_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        Dim _pos As Integer = JGr_Buscador.Row
        If _pos < JGr_Buscador.RowCount - 1 Then
            _pos = JGr_Buscador.Row + 1
            '' _prMostrarRegistro(_pos)
            JGr_Buscador.Row = _pos
            LblPaginacion.Text = Str(_pos + 1) + "/" + JGr_Buscador.RowCount.ToString
        End If
    End Sub

    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        Dim _pos As Integer = JGr_Buscador.Row
        If JGr_Buscador.RowCount > 0 Then
            _pos = JGr_Buscador.RowCount - 1
            ''  _prMostrarRegistro(_pos)
            JGr_Buscador.Row = _pos
            LblPaginacion.Text = Str(JGr_Buscador.RowCount).Trim + "/" + Str(JGr_Buscador.RowCount).Trim
        End If
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        Dim _MPos As Integer = JGr_Buscador.Row
        If _MPos > 0 And JGr_Buscador.RowCount > 0 Then
            _MPos = _MPos - 1
            ''  _prMostrarRegistro(_MPos)
            JGr_Buscador.Row = _MPos
            LblPaginacion.Text = Str(_Pos + 1) + "/" + CType(JGr_Buscador.DataSource, DataTable).Rows.Count.ToString

        End If
    End Sub

    Private Sub btnPrimero_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click
        Dim _MPos As Integer
        If JGr_Buscador.RowCount > 0 Then
            _MPos = 0
            ''   _prMostrarRegistro(_MPos)
            JGr_Buscador.Row = _MPos
        End If
        LblPaginacion.Text = Str(1) + "/" + CType(JGr_Buscador.DataSource, DataTable).Rows.Count.ToString
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs)


        Dim dt As DataTable

        dt = L_CuentaContable()
        '              a.ydnumi, a.ydcod, a.yddesc, a.yddctnum, a.yddirec
        ',a.ydtelf1 ,a.ydfnac 

        Dim listEstCeldas As New List(Of Modelo.Celda)
        listEstCeldas.Add(New Modelo.Celda("canumi", True, "Cod", 50))
        listEstCeldas.Add(New Modelo.Celda("cacta", True, "Nro. CUENTA", 70))
        listEstCeldas.Add(New Modelo.Celda("cadesc", True, "NOMBRE", 280))
        Dim ef = New Efecto
        ef.tipo = 3
        ef.dt = dt
        ef.SeleclCol = 1
        ef.listEstCeldas = listEstCeldas
        ef.alto = 50
        ef.ancho = 350
        ef.Context = "Seleccione Cuenta".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
            If (IsNothing(Row)) Then
                ' tbCodBarra.Focus()
                Return

            End If
            NumiCuenta = Row.Cells("canumi").Value
            'tbCodBarra.Text = Row.Cells("cacta").Value
            'tbDescPro.Focus()

        End If

    End Sub

    Private Sub TbNombre1_KeyDown(sender As Object, e As KeyEventArgs) Handles TbNombre1.KeyDown

        If (e.KeyData = Keys.Enter) Then
                If TbNombre1.Text = String.Empty Then
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "DEBE INGRESAR UN CODIGO DE CAÑERO PARA REALIZAR LA BUSQUEDA".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                ElseIf TbNombre1.Text = TextBoxX1.Text Then
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "NO PUEDE BUSCAR AL MISMO CAÑERO".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

                Else

                    Dim dt As DataTable
                    'dt = L_fnListarClientes()
                    dt = L_fnListarClientesVentas11(TbNombre1.Text)
                If dt.Rows.Count > 0 Then
                    Dim listEstCeldas As New List(Of Modelo.Celda)
                    listEstCeldas.Add(New Modelo.Celda("ydnumi,", True, "ID", 50))
                    listEstCeldas.Add(New Modelo.Celda("ydcod", True, "COD-CANERO", 100))
                    listEstCeldas.Add(New Modelo.Celda("ydrazonsocial", True, "RAZÓN SOCIAL", 180))
                    listEstCeldas.Add(New Modelo.Celda("ydnumivend,", False, "ID", 50))
                    listEstCeldas.Add(New Modelo.Celda("vendedor,", True, "INSTITUCIÓN", 180))
                    listEstCeldas.Add(New Modelo.Celda("total,", True, "TOTAL", 180))
                    listEstCeldas.Add(New Modelo.Celda("pesoNeto,", True, "PESO NETO", 180))
                    listEstCeldas.Add(New Modelo.Celda("cupoLibre,", True, "CUPO LIBRE ", 180))
                    Dim ef = New Efecto
                    ef.tipo = 3
                    ef.dt = dt
                    ef.SeleclCol = 2
                    ef.listEstCeldas = listEstCeldas
                    ef.alto = 50
                    ef.ancho = 200
                    ef.Context = "Seleccione Cañero".ToUpper
                    ef.ShowDialog()
                    Dim bandera As Boolean = False
                    bandera = ef.band
                    If (bandera = True) Then


                        tbCliente.Text = String.Empty
                        TbNombre1.Text = String.Empty
                        tbNit.Text = String.Empty
                        tbVendedor.Text = String.Empty

                        tbCupoResgistrado.Text = String.Empty
                        tbCupoActual.Text = String.Empty

                        DoubleInput1.Value = 0
                        DoubleInput4.Value = 0

                        DoubleInput3.Value = 0



                        Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row

                        _CodCanerotransfirienteSys = Row.Cells("ydnumi").Value
                        tbCliente.Text = Row.Cells("ydrazonsocial").Value
                        '_dias = Row.Cells("yddias").Value
                        tbCupoResgistrado.Text = Row.Cells("total").Value
                        tbCupoActual.Text = Row.Cells("cupoLibre").Value
                        TbNombre1.Text = Row.Cells("ydcod").Value
                        Dim dt1 As DataTable
                        dt1 = L_fnListarCaneroInstitucion(_CodCanerotransfirienteSys)
                        Dim row1 As DataRow = dt1.Rows(dt1.Rows.Count - 1)
                        tbVendedor.Text = row1("institucion")
                        tbNit.Text = row1("codInst")
                        'cbgrupo1.Focus()
                        Dim numiVendedor As Integer = IIf(IsDBNull(Row.Cells("ydnumivend").Value), 0, Row.Cells("ydnumivend").Value)
                        If (numiVendedor > 0) Then
                            ''tbVendedor.Text = Row.Cells("vendedor").Value
                            _CodEmpleado = Row.Cells("ydnumivend").Value

                            'grdetalle1.Select()
                        Else
                            tbVendedor.Clear()
                            _CodEmpleado = 0
                            'cbgrupo1.Focus()

                        End If
                    End If
                Else
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "ESTE CAÑERO NO TIENE REGISTRADO CAÑA COMPROMETIDA -- POR FAVOR REGISTRE ANTES DE HACER UN TRASPASO--".ToUpper, img, 10000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    _CodEmpleado = 0
                    tbCupoActual.Clear()
                    tbCupoResgistrado.Clear()
                    DoubleInput1.Text = 0
                    tbNit.Clear()
                    tbVendedor.Clear()
                    tbCliente.Clear()
                End If

            End If

            End If


    End Sub

    Private Sub TextBoxX1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxX1.KeyDown

        If (e.KeyData = Keys.Enter) Then
            If TextBoxX1.Text = String.Empty Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "DEBE INGRESAR UN CODIGO DE CAÑERO PARA REALIZAR LA BUSQUEDA".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            ElseIf TextBoxX1.Text = TbNombre1.Text Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "NO PUEDE BUSCAR AL MISMO CAÑERO".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

            Else
                Dim dt As DataTable
                    'dt = L_fnListarClientes()
                    dt = L_fnListarClientesVentas11(TextBoxX1.Text)
                If dt.Rows.Count > 0 Then
                    Dim listEstCeldas As New List(Of Modelo.Celda)
                    listEstCeldas.Add(New Modelo.Celda("ydnumi,", True, "ID", 50))
                    listEstCeldas.Add(New Modelo.Celda("ydcod", True, "COD-CANERO", 100))
                    listEstCeldas.Add(New Modelo.Celda("ydrazonsocial", True, "RAZÓN SOCIAL", 180))
                    listEstCeldas.Add(New Modelo.Celda("ydnumivend,", False, "ID", 50))
                    listEstCeldas.Add(New Modelo.Celda("vendedor,", True, "INSTITUCIÓN", 180))
                    listEstCeldas.Add(New Modelo.Celda("total,", True, "TOTAL", 180))
                    listEstCeldas.Add(New Modelo.Celda("pesoNeto,", True, "PESO NETO", 180))
                    listEstCeldas.Add(New Modelo.Celda("cupoLibre,", True, "CUPO LIBRE ", 180))
                    Dim ef = New Efecto
                    ef.tipo = 3
                    ef.dt = dt
                    ef.SeleclCol = 2
                    ef.listEstCeldas = listEstCeldas
                    ef.alto = 50
                    ef.ancho = 200
                    ef.Context = "Seleccione Cañero".ToUpper
                    ef.ShowDialog()
                    Dim bandera As Boolean = False
                    bandera = ef.band
                    If (bandera = True) Then

                        TextBoxX1.Text = String.Empty
                        tbClienteR.Text = String.Empty
                        tbNitR.Text = String.Empty
                        tbVendedorR.Text = String.Empty
                        tbCupoRegistradoR.Text = String.Empty
                        DoubleInput2.Value = 0

                        Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row

                        _CodCanerotransreceptorSys = Row.Cells("ydnumi").Value
                        tbClienteR.Text = Row.Cells("ydrazonsocial").Value
                        '_dias = Row.Cells("yddias").Value
                        tbCupoRegistradoR.Text = Row.Cells("total").Value
                        tbCupoActualR.Text = Row.Cells("cupoLibre").Value
                        TextBoxX1.Text = Row.Cells("ydcod").Value
                        Dim dt1 As DataTable
                        dt1 = L_fnListarCaneroInstitucion(_CodCanerotransreceptorSys)
                        Dim row1 As DataRow = dt1.Rows(dt1.Rows.Count - 1)
                        tbVendedorR.Text = row1("institucion")
                        tbNitR.Text = row1("codInst")
                        'cbgrupo1.Focus()
                        Dim numiVendedor As Integer = IIf(IsDBNull(Row.Cells("ydnumivend").Value), 0, Row.Cells("ydnumivend").Value)
                        If (numiVendedor > 0) Then
                            ''tbVendedor.Text = Row.Cells("vendedor").Value
                            _CodEmpleado = Row.Cells("ydnumivend").Value

                            'grdetalle1.Select()
                        Else
                            tbVendedorR.Clear()
                            _CodEmpleado = 0
                            'cbgrupo1.Focus()

                        End If
                    End If
                Else
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "ESTE CAÑERO NO TIENE REGISTRADO CAÑA COMPROMETIDA -- POR FAVOR REGISTRE ANTES DE HACER UN TRASPASO--".ToUpper, img, 10000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    _CodEmpleado = 0
                    tbVendedorR.Clear()
                    tbNitR.Clear()
                    tbClienteR.Clear()
                    tbCupoRegistradoR.Clear()
                    tbCupoActualR.Clear()
                End If

            End If

            End If

    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        If TbNombre1.Text = String.Empty Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "PRIMERO DEBE SELECCIONAR AL CAÑERO TRANSFIRIENTE".ToUpper, img, 10000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        Else
            If DoubleInput1.Text = String.Empty Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "DEBE COLOCAR UN VALOR DE CUPO A TRANSFERIR".ToUpper, img, 10000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            ElseIf DoubleInput1.Value <= 0 Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "DEBE COLOCAR UN VALOR MAYOR A CERO DE CUPO A TRANSFERIR".ToUpper, img, 10000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Else
                If tbCupoRegistradoR.Text = String.Empty Then
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "PRIMERO DEBE SELECCIONAR AL CAÑERO RECEPTOR".ToUpper, img, 10000, eToastGlowColor.Red, eToastPosition.BottomCenter)

                Else
                    If DoubleInput1.Value > tbCupoActual.Text Then
                        Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                        ToastNotification.Show(Me, "NO PUEDE TRANSFERIR UN CUPO MAYOR AL CUPO LIBRE DE TRANSFERENCIA".ToUpper, img, 10000, eToastGlowColor.Red, eToastPosition.BottomCenter)

                    Else
                        DoubleInput2.Text = DoubleInput1.Value + Convert.ToDouble(tbCupoRegistradoR.Text)
                        DoubleInput3.Text = ((DoubleInput1.Value / Convert.ToDecimal(tbCupoResgistrado.Text)) * 100)
                        DoubleInput4.Text = Convert.ToDouble(tbCupoResgistrado.Text) - DoubleInput1.Value
                    End If

                End If

            End If
        End If
    End Sub
End Class