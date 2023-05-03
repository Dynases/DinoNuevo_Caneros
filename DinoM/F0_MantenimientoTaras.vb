Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Public Class F0_MantenimientoTaras
    Dim _Inter As Integer = 0
    Dim NumiCuenta As Integer
    Dim _codInsti As Integer = 0
#Region "ATRIBUTOS"
    Dim _Dsencabezado As DataSet
    Dim _Nuevo As Boolean
    Private _Pos As Integer
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Public _nameButton As String
    Dim NumiVendedor As Integer = 0
#End Region

#Region "METODOS PRIVADOS"
    Private Sub _PIniciarTodo()

        Me.Text = "MANTENIMIENTO TARAS"
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
        Tb_CodTara.MaxLength = 6
        Tb_Placa.MaxLength = 200
        Tb_Color.MaxLength = 20
        Tb_Propietario.MaxLength = 200
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
            Tb_CodTara.Text = .GetValue("cod").ToString
            Tb_Placa.Text = .GetValue("placa").ToString
            Tb_Color.Text = .GetValue("color").ToString
            Tb_Propietario.Text = .GetValue("propietario").ToString
            tbPesoTara.Text = .GetValue("pesoTara").ToString

        End With
    End Sub

    Private Sub _PInhabilitar()
        ' Tb_Id.ReadOnly = True
        Tb_CodTara.ReadOnly = True
        Tb_Placa.ReadOnly = True
        tbPesoTara.IsInputReadOnly = True

        Tb_Color.ReadOnly = True
        Tb_Propietario.ReadOnly = True

        btnNuevo.Enabled = True
        btnModificar.Enabled = True
        btnEliminar.Enabled = True
        btnGrabar.Enabled = False

        JGr_Buscador.Enabled = True

        btnGrabar.Image = My.Resources.save

        _PLimpiarErrores()
    End Sub

    Private Sub _PLimpiarErrores()
        MEP.Clear()
        Tb_CodTara.BackColor = Color.White
        Tb_Placa.BackColor = Color.White
        Tb_Propietario.BackColor = Color.White

    End Sub

    Private Sub _PHabilitarFocus()
        MHighlighterFocus.SetHighlightOnFocus(Tb_CodTara, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        MHighlighterFocus.SetHighlightOnFocus(Tb_Placa, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        MHighlighterFocus.SetHighlightOnFocus(Tb_Color, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        MHighlighterFocus.SetHighlightOnFocus(Tb_Propietario, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        Tb_CodTara.TabIndex = 1
        Tb_Placa.TabIndex = 2
        Tb_Color.TabIndex = 3
        Tb_Propietario.TabIndex = 4

    End Sub

    Private Sub _PCargarBuscador()
        _Dsencabezado = New DataSet
        _Dsencabezado = L_Taras(0)

        JGr_Buscador.BoundMode = BoundMode.Bound
        JGr_Buscador.DataSource = _Dsencabezado.Tables(0) ' _Dsencabezado.Tables(0) ' dt
        JGr_Buscador.RetrieveStructure()

        With JGr_Buscador.RootTable.Columns("cod")
            .Visible = True
            .Caption = "CODIGO".ToUpper

            .Width = 70
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        End With
        With JGr_Buscador.RootTable.Columns("placa")
            '.Visible = False
            .Caption = "PLACA".ToUpper
            .Width = 100
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        End With
        With JGr_Buscador.RootTable.Columns("pesoTara")
            .Visible = True
            .Caption = "PESO TARA".ToUpper
            .Width = 100
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .FormatString = "0.00"
        End With
        With JGr_Buscador.RootTable.Columns("color")
            .Visible = True
            .Caption = "COLOR".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

        End With
        With JGr_Buscador.RootTable.Columns("propietario")
            .Visible = True
            .Caption = "PROPIETARIO".ToUpper
            .Width = 200
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
        Tb_CodTara.ReadOnly = False
        Tb_Placa.ReadOnly = False
        Tb_Color.ReadOnly = False
        Tb_Propietario.ReadOnly = False
        tbPesoTara.IsInputReadOnly = False
        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
        btnGrabar.Enabled = True
    End Sub

    Private Sub _PLimpiar()
        ' Tb_Id.Text = String.Empty
        Tb_CodTara.Text = String.Empty
        Tb_Placa.Text = String.Empty
        Tb_Propietario.Text = String.Empty
        tbPesoTara.Text = String.Empty
        Tb_Color.Clear()
        LblPaginacion.Text = String.Empty
    End Sub

    Public Function P_Validar() As Boolean
        Dim _Error As Boolean = True
        MEP.Clear()


        If Tb_Placa.Text.Trim = String.Empty Then
            Tb_Placa.BackColor = Color.Red
            MEP.SetError(Tb_Placa, "Ingrese número de placa!".ToUpper)
            _Error = False
        Else
            Tb_Placa.BackColor = Color.White
            MEP.SetError(Tb_Placa, String.Empty)
        End If

        If tbPesoTara.Text.Trim = String.Empty Then
            tbPesoTara.BackColor = Color.Red
            MEP.SetError(tbPesoTara, "Ingrese el peso de tara!".ToUpper)
            _Error = False
        Else
            tbPesoTara.BackColor = Color.White
            MEP.SetError(tbPesoTara, String.Empty)
        End If

        If Tb_Propietario.Text.Trim = String.Empty Then
            Tb_Propietario.BackColor = Color.Red
            MEP.SetError(Tb_Propietario, "Ingrese el nombre del propietario!".ToUpper)
            _Error = False
        Else
            Tb_Propietario.BackColor = Color.White
            MEP.SetError(Tb_Propietario, String.Empty)
        End If

        If Tb_CodTara.Text.Trim = String.Empty Then
            Tb_CodTara.BackColor = Color.Red
            MEP.SetError(Tb_CodTara, "Ingrese un código de tara!".ToUpper)
            _Error = False

        Else
            If _Nuevo Then
                If L_BuscarCodTara(Tb_CodTara.Text) = True Then
                    Tb_CodTara.BackColor = Color.Red
                    MEP.SetError(Tb_CodTara, "Ingrese un código distinto!".ToUpper)
                    _Error = False
                Else
                    Tb_CodTara.BackColor = Color.White
                    MEP.SetError(Tb_CodTara, String.Empty)
                End If
            ElseIf _codInsti = Convert.ToInt32(Tb_CodTara.Text) Then
                _Error = True
            Else
                If L_BuscarCodTara(Tb_CodTara.Text) = True Then
                    Tb_CodTara.BackColor = Color.Red
                    MEP.SetError(Tb_CodTara, "Ingrese un código distinto!".ToUpper)
                    _Error = False
                Else
                    Tb_CodTara.BackColor = Color.White
                    MEP.SetError(Tb_CodTara, String.Empty)
                End If
            End If
            If _Error = True Then
                Tb_CodTara.BackColor = Color.White
                MEP.SetError(Tb_CodTara, String.Empty)
            End If
        End If
        MHighlighterFocus.UpdateHighlights()
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
        JGr_Buscador.Enabled = False
    End Sub
    Private Sub _PNuevoRegistro()
        _PHabilitar()
        'btnNuevo.Enabled = True

        _PLimpiar()
        Tb_CodTara.Focus()
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
        If P_Validar() Then

            If False Then 'btnGrabar.Tag = 0
                btnGrabar.Tag = 1
                btnGrabar.Refresh()
                Exit Sub
            Else
                btnGrabar.Tag = 0
                btnGrabar.Refresh()
            End If

            If _Nuevo Then

                L_Taras_Grabar(Tb_CodTara.Text, Tb_Placa.Text, tbPesoTara.Value, Tb_Color.Text, Tb_Propietario.Text)

                Tb_CodTara.Focus()

                ToastNotification.Show(Me, "Codigo Institución ".ToUpper + Tb_CodTara.Text + " Grabado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)



                'actualizar el grid de buscador
                _PCargarBuscador()
                _PLimpiar()


            Else

                L_Taras_Modificar(Tb_CodTara.Text, Tb_Placa.Text, tbPesoTara.Value, Tb_Color.Text, Tb_Propietario.Text)

                ToastNotification.Show(Me, "Codigo Institución ".ToUpper + Tb_CodTara.Text + " Modificado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)

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
        _codInsti = Tb_CodTara.Text
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
            Dim t As String = Tb_CodTara.Text
            L_Taras_Borrar(Tb_CodTara.Text)

            _PInhabilitar()
            _PFiltrar()
            _PCargarBuscador()

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)

            ToastNotification.Show(Me, "Código de Tara ".ToUpper + t + " eliminado con Exito.".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter)

        Else
            _PInhabilitar()
        End If

    End Sub

    Private Sub Tb_Telefono_TextChanged(sender As Object, e As EventArgs) Handles Tb_Color.TextChanged

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
                tbPesoTara.Focus()
                Return

            End If
            NumiCuenta = Row.Cells("canumi").Value
            tbPesoTara.Text = Row.Cells("cacta").Value
            'tbDescPro.Focus()

        End If

    End Sub

End Class