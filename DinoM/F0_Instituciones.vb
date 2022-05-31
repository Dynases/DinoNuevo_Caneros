Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Public Class F0_Instituciones
    Dim _Inter As Integer = 0

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

        Me.Text = "INSTITUCIONES"
        'Me.WindowState = FormWindowState.Maximized


        _MaxLengthTextBox()

        _PCargarBuscador()
        _PInhabilitar()
        _PHabilitarFocus()

        Dim blah As New Bitmap(New Bitmap(My.Resources.user), 20, 20)


        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        GroupPanel1.Style.BackColor = Color.FromArgb(13, 71, 161)
        GroupPanel1.Style.BackColor2 = Color.FromArgb(13, 71, 161)
        GroupPanel1.Style.TextColor = Color.White
        JGr_Buscador.Focus()

    End Sub
    Public Sub _MaxLengthTextBox()
        Tb_CodInst.MaxLength = 4
        Tb_NomInst.MaxLength = 200
        Tb_Telefono.MaxLength = 20
        Tb_Direccion.MaxLength = 200
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
            Tb_Id.Text = .GetValue("id").ToString
            Tb_CodInst.Text = .GetValue("codInst").ToString
            Tb_NomInst.Text = .GetValue("nomInst").ToString
            Tb_Telefono.Text = .GetValue("telf").ToString
            Tb_Direccion.Text = .GetValue("direc").ToString

        End With
    End Sub

    Private Sub _PInhabilitar()
        Tb_Id.ReadOnly = True
        Tb_CodInst.ReadOnly = True
        Tb_NomInst.ReadOnly = True

        Tb_Telefono.ReadOnly = True
        Tb_Direccion.ReadOnly = True

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
        Tb_CodInst.BackColor = Color.White
        Tb_NomInst.BackColor = Color.White

    End Sub

    Private Sub _PHabilitarFocus()
        MHighlighterFocus.SetHighlightOnFocus(Tb_CodInst, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        MHighlighterFocus.SetHighlightOnFocus(Tb_NomInst, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        MHighlighterFocus.SetHighlightOnFocus(Tb_Telefono, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        MHighlighterFocus.SetHighlightOnFocus(Tb_Direccion, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        Tb_CodInst.TabIndex = 1
        Tb_NomInst.TabIndex = 2
        Tb_Telefono.TabIndex = 3
        Tb_Direccion.TabIndex = 4

    End Sub

    Private Sub _PCargarBuscador()
        _Dsencabezado = New DataSet
        _Dsencabezado = L_Institucion(0)

        JGr_Buscador.BoundMode = BoundMode.Bound
        JGr_Buscador.DataSource = _Dsencabezado.Tables(0) ' _Dsencabezado.Tables(0) ' dt
        JGr_Buscador.RetrieveStructure()

        With JGr_Buscador.RootTable.Columns("id")
            '.Visible = False
            .Caption = "id".ToUpper
            .Width = 50
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        End With
        With JGr_Buscador.RootTable.Columns("codInst")
            '.Visible = False
            .Caption = "Cod. Institución".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        End With
        With JGr_Buscador.RootTable.Columns("nomInst")
            '   .Visible = False
            .Caption = "Institución".ToUpper
            .Width = 400
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

        End With
        With JGr_Buscador.RootTable.Columns("campo1")
            '   .Visible = False
            .Caption = "Num.Cuenta".ToUpper
            .Width = 400
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

        End With

        With JGr_Buscador.RootTable.Columns("direc")
            '.Visible = False
            .Caption = "Dirección".ToUpper
            .Width = 200
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            '.CellStyle.FontSize = gi_fuenteTamano
        End With

        With JGr_Buscador.RootTable.Columns("telf")
            .Caption = "Telefono".ToUpper
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
        Tb_CodInst.ReadOnly = False
        Tb_NomInst.ReadOnly = False

        Tb_Telefono.ReadOnly = False
        Tb_Direccion.ReadOnly = False

        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
        btnGrabar.Enabled = True
    End Sub

    Private Sub _PLimpiar()
        Tb_Id.Text = String.Empty
        Tb_CodInst.Text = String.Empty
        Tb_NomInst.Text = String.Empty
        Tb_Direccion.Text = String.Empty

        Tb_Telefono.Clear()
        LblPaginacion.Text = String.Empty
    End Sub

    Public Function P_Validar() As Boolean
        Dim _Error As Boolean = True
        MEP.Clear()
        If Tb_CodInst.Text.Trim = String.Empty Then
            Tb_CodInst.BackColor = Color.Red
            MEP.SetError(Tb_CodInst, "Ingrese código institución!".ToUpper)
            _Error = False
        Else
            Tb_CodInst.BackColor = Color.White
            MEP.SetError(Tb_CodInst, String.Empty)
        End If

        If Tb_NomInst.Text.Trim = String.Empty Then
            Tb_NomInst.BackColor = Color.Red
            MEP.SetError(Tb_NomInst, "Ingrese nombre de institución!".ToUpper)
            _Error = False
        Else
            Tb_NomInst.BackColor = Color.White
            MEP.SetError(Tb_NomInst, String.Empty)
        End If



        MHighlighterFocus.UpdateHighlights()
        Return True
    End Function
#End Region
#Region " Cancelar-Button "
    Private Sub BBtn_Cancelar_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _PSalirRegistro()
    End Sub

    Private Sub _PSalirRegistro()
        If btnGrabar.Enabled = True Then
            _PLimpiar()
            _PInhabilitar()
            _PFiltrar()
            _PCargarBuscador()
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
        Tb_CodInst.Focus()
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

                _Error = L_Institucion_Grabar(Tb_Id.Text, Tb_CodInst.Text, Tb_NomInst.Text, Tb_Telefono.Text, Tb_Direccion.Text, 0, 0, 0)

                Tb_CodInst.Focus()
                If _Error = False Then
                    ToastNotification.Show(Me, "Codigo Institución ".ToUpper + Tb_Id.Text + " Grabado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)

                End If

                'actualizar el grid de buscador
                _PCargarBuscador()
                _PLimpiar()
            Else
                _Error = L_Institucion_Modificar(Tb_Id.Text, Tb_CodInst.Text, Tb_NomInst.Text, Tb_Telefono.Text, Tb_Direccion.Text, 0, 0, 0)
                If _Error = False Then
                    ToastNotification.Show(Me, "Codigo Institución ".ToUpper + Tb_Id.Text + " Modificado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                End If
                _PCargarBuscador()
                    _Nuevo = False 'aumentado danny
                    _PInhabilitar()
                    '_PFiltrar()
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

    Private Sub Tb_Telefono_TextChanged(sender As Object, e As EventArgs) Handles Tb_Telefono.TextChanged

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
End Class