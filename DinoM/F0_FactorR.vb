Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Public Class F0_FactorR
    Dim pciGeneral As Decimal
    Dim ingreso As Decimal
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

        Me.Text = "FACTOR   R   PONDERADO"
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
        ' Tb_CodTara.MaxLength = 6

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
            Tb_CodTara.Text = .GetValue("id").ToString

            tbPesoTara.Text = .GetValue("pcfab").ToString
            tbFecha.Value = .GetValue("fecha").ToString

        End With
    End Sub

    Private Sub _PInhabilitar()
        ' Tb_Id.ReadOnly = True
        Tb_CodTara.ReadOnly = True

        tbPesoTara.ReadOnly = True


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


    End Sub

    Private Sub _PHabilitarFocus()
        MHighlighterFocus.SetHighlightOnFocus(Tb_CodTara, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)

        Tb_CodTara.TabIndex = 1


    End Sub

    Private Sub _PCargarBuscador()
        _Dsencabezado = New DataSet
        _Dsencabezado = L_factorR(0)

        JGr_Buscador.BoundMode = BoundMode.Bound
        JGr_Buscador.DataSource = _Dsencabezado.Tables(0) ' _Dsencabezado.Tables(0) ' dt
        JGr_Buscador.RetrieveStructure()

        With JGr_Buscador.RootTable.Columns("id")
            .Visible = True
            .Caption = "CODIGO".ToUpper

            .Width = 70
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        End With
        With JGr_Buscador.RootTable.Columns("fecha")
            '.Visible = False
            .Caption = "FECHA".ToUpper
            .Width = 100
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        End With
        With JGr_Buscador.RootTable.Columns("ingreso")
            .Visible = True
            .Caption = "ingreso".ToUpper
            .Width = 100
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .FormatString = "0.00"
        End With
        With JGr_Buscador.RootTable.Columns("pcfab")
            .Visible = True
            .Caption = "pcfab".ToUpper
            .Width = 100
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .FormatString = "0.000"
        End With
        With JGr_Buscador.RootTable.Columns("pci")
            .Visible = True
            .Caption = "pci".ToUpper
            .Width = 100
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .FormatString = "0.000"
        End With
        With JGr_Buscador.RootTable.Columns("obtenido")
            .Visible = True
            .Caption = "OBtenido".ToUpper
            .Width = 100
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .FormatString = "0.0000"
        End With
        With JGr_Buscador.RootTable.Columns("ponderado")
            .Visible = True
            .Caption = "ponderado".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .FormatString = "0.0000"
        End With
        With JGr_Buscador.RootTable.Columns("aplicado")
            .Visible = True
            .Caption = "aplicado".ToUpper
            .Width = 200
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .FormatString = "0.0000"
        End With

        With JGr_Buscador.RootTable.Columns("ingAcumulado")
            .Visible = True
            .Caption = "ING. ACUMULADO".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

        End With
        With JGr_Buscador.RootTable.Columns("relRacum")
            .Visible = True
            .Caption = "REL. ACUM".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

        End With
        With JGr_Buscador.RootTable.Columns("kgRelrdia")
            .Visible = True
            .Caption = "KG. REL. DIA".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

        End With
        With JGr_Buscador.RootTable.Columns("calculoPonderado")
            .Visible = True
            .Caption = "calculo ponderado".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .FormatString = "0.0000"
        End With
        With JGr_Buscador.RootTable.Columns("tafact")
            .Visible = False
            .Caption = "ponderado".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

        End With
        With JGr_Buscador.RootTable.Columns("tahact")
            .Visible = False
            .Caption = "ponderado".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center

        End With
        With JGr_Buscador.RootTable.Columns("tauact")
            .Visible = False
            .Caption = "ponderado".ToUpper
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
        'Tb_CodTara.ReadOnly = False

        tbPesoTara.ReadOnly = False
        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
        btnGrabar.Enabled = True
    End Sub

    Private Sub _PLimpiar()
        ' Tb_Id.Text = String.Empty
        Tb_CodTara.Text = String.Empty
        tbPesoTara.BackColor = Color.White
        tbPesoTara.Text = String.Empty

        LblPaginacion.Text = String.Empty
    End Sub

    Public Function P_Validar() As Boolean
        Dim _Error As Boolean = True
        MEP.Clear()
        If VerificarInicioZafra(tbFecha.Value) = False Then
            MessageBox.Show("No se puede grabar por que no existe registro de inicio de zafra." + tbFecha.Text)
            _Error = False
        End If
        If VerificarFechaFactorPonderado(tbFecha.Value) = True Then
            MessageBox.Show("No se puede grabar por que ya existe factor para esta fecha." + tbFecha.Text)
            _Error = False
        End If
        If VerificarAnalisis(tbFecha.Value) = False Then
                MessageBox.Show("No se puede grabar por que no existe boletas con esta fecha de registro " + tbFecha.Text)
                _Error = False
            Else

            End If


            If tbPesoTara.Text.Trim = String.Empty Then
            tbPesoTara.BackColor = Color.Red
            MEP.SetError(tbPesoTara, "Ingrese el peso de tara!".ToUpper)
            _Error = False
        Else
            tbPesoTara.BackColor = Color.White
            MEP.SetError(tbPesoTara, String.Empty)
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

                L_FactorR_Grabar(tbPesoTara.Text, tbFecha.Text)

                Tb_CodTara.Focus()

                ToastNotification.Show(Me, " Grabado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)



                'actualizar el grid de buscador
                _PCargarBuscador()
                _PLimpiar()


            Else

                ' L_Taras_Modificar(Tb_CodTara.Text, Tb_Placa.Text, tbPesoTara.Value, Tb_Color.Text, Tb_Propietario.Text)

                ToastNotification.Show(Me, " Modificado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)

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

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        _Dsencabezado = New DataSet
        _Dsencabezado = L_pruebaFactor(0)
        For Each fila As DataRow In _Dsencabezado.Tables(0).Rows()

            L_pruebaFactor_Grabar(fila(0), fila(1), fila(3), fila(2))
        Next

    End Sub
End Class