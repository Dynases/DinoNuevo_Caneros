
Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports System.IO

Imports DevComponents.DotNetBar.Controls

Public Class F0_GrupoCanero
    Dim _Inter As Integer = 0
#Region "Variables Globales"
    Public _nameButton As String
    Public _tab As SuperTabItem
    Dim Lote As Boolean = False
    Public _modulo As SideNavItem
    Dim Table_producto As DataTable
    Dim FilaSelectLote As DataRow = Nothing

    Dim _CodCliente As Integer = 0
    Dim _CodEmpleado As Integer = 0
    Dim _CodInstitucion As Integer = 0
#End Region
#Region "Metodos Privados"
    Private Sub _IniciarTodo()
        MSuperTabControl.SelectedTabIndex = 0
        '' L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        _prValidarLote()
        'Me.WindowState = FormWindowState.Maximized

        _prCargarVenta()
        _prInhabiliitar()
        'Dim blah As New Bitmap(New Bitmap(My.Resources.compra), 20, 20)
        'Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        'Me.Icon = ico
        _prAsignarPermisos()
        Me.Text = "GRUPOS ECONÓMICOS"
        tbObservacion.MaxLength = 100
    End Sub
    Public Sub _prValidarLote()
        Dim dt As DataTable = L_fnPorcUtilidad()
        If (dt.Rows.Count > 0) Then
            Dim lot As Integer = dt.Rows(0).Item("VerLote")
            If (lot = 1) Then
                Lote = True
            Else
                Lote = False
            End If

        End If
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
    Private Sub _prInhabiliitar()

        tbObservacion.ReadOnly = True
        tbFecha.IsInputReadOnly = True

        ''''''''''
        btnModificar.Enabled = True
        btnGrabar.Enabled = False
        btnNuevo.Enabled = True
        btnEliminar.Enabled = True
        grmovimiento.Enabled = True

        grdetalle.RootTable.Columns("img").Visible = False


        If (GPanelCanero.Visible = True) Then
            _DesHabilitarProductos()
        End If

        PanelInferior.Enabled = True
        FilaSelectLote = Nothing
    End Sub
    Private Sub _prhabilitar()
        'cbConcepto.ReadOnly = False
        tbObservacion.ReadOnly = False
        tbFecha.IsInputReadOnly = False
        grmovimiento.Enabled = False
        ''  tbCliente.ReadOnly = False  por que solo podra seleccionar Cliente
        ''  tbVendedor.ReadOnly = False

        btnGrabar.Enabled = True
    End Sub
    Public Sub _prFiltrar()
        'cargo el buscador
        Dim _Mpos As Integer
        _prCargarVenta()
        If grmovimiento.RowCount > 0 Then
            _Mpos = 0
            grmovimiento.Row = _Mpos
        Else
            _Limpiar()
            LblPaginacion.Text = "0/0"
        End If
    End Sub
    Private Sub _Limpiar()
        tbCodigo.Clear()
        tbObservacion.Clear()
        tbFecha.Value = Now.Date
        tbCodCanero.Clear()
        tbCodInst.Clear()
        tbCliente.Clear()
        tbVendedor.Clear()
        _prCargarDetalleGrupo(-1)


        With grdetalle.RootTable.Columns("img")
            .Width = 80
            .Caption = "Eliminar"
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = True
        End With

        If (GPanelCanero.Visible = True) Then
            GPanelCanero.Visible = False
            PanelInferior.Visible = True
        End If

        _prAddDetalleVenta()
        FilaSelectLote = Nothing

        If grCanero.RowCount > 0 Then
            CType(grCanero.DataSource, DataTable).Rows.Clear()
        End If

    End Sub
    Public Sub _prMostrarRegistro(_N As Integer)
        '      a.ibid ,a.ibfdoc ,a.ibconcep ,b.cpdesc as concepto,a.ibobs ,a.ibest ,a.ibalm ,a.ibiddc 
        ',a.ibfact ,a.ibhact ,a.ibuact,ibdepdest
        With grmovimiento
            tbCodigo.Text = .GetValue("codGrupo")
            tbFecha.Value = .GetValue("fecha")
            tbObservacion.Text = .GetValue("observacion")
            tbCodCanero.Text = .GetValue("ydcod")
            tbCliente.Text = .GetValue("yddesc")
            tbCodInst.Text = .GetValue("codInst")
            tbVendedor.Text = .GetValue("nomInst")
            lbFecha.Text = CType(.GetValue("ibfact"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("ibhact").ToString
            lbUsuario.Text = .GetValue("ibuact").ToString
        End With

        _prCargarDetalleGrupo(tbCodigo.Text)
        LblPaginacion.Text = Str(grmovimiento.Row + 1) + "/" + grmovimiento.RowCount.ToString

    End Sub

    Private Sub _prCargarDetalleGrupo(_numi As String)
        Dim dt As New DataTable
        dt = L_fnDetalleGrupoEconomico(_numi)
        grdetalle.DataSource = dt
        grdetalle.RetrieveStructure()
        grdetalle.AlternatingColors = True

        With grdetalle.RootTable.Columns("ydnumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False

        End With

        With grdetalle.RootTable.Columns("ydcod")
            .Width = 140
            .Caption = "COD. CAÑERO"
            .Visible = True
        End With

        With grdetalle.RootTable.Columns("ydrazonsocial")
            .Caption = "NOMBRE CAÑERO"
            .Width = 180
            .Visible = True


        End With



        With grdetalle.RootTable.Columns("img")
            .Width = 80
            .Caption = "Eliminar".ToUpper
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = False
        End With
        With grdetalle.RootTable.Columns("estado")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        With grdetalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
            .BoundMode = Janus.Data.BoundMode.Bound
            .RowHeaders = InheritableBoolean.True
        End With
    End Sub

    Private Sub _prCargarVenta()
        Dim dt As New DataTable
        dt = L_fnGeneralGrupoCanero()
        grmovimiento.DataSource = dt
        grmovimiento.RetrieveStructure()
        grmovimiento.AlternatingColors = True


        With grmovimiento.RootTable.Columns("id")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = True

        End With

        With grmovimiento.RootTable.Columns("fecha")
            .Width = 90
            .Visible = True
            .Caption = "FECHA"
            .FormatString = "yyyy/MM/dd"
        End With
        With grmovimiento.RootTable.Columns("codGrupo")
            .Width = 90
            .Visible = False
        End With

        With grmovimiento.RootTable.Columns("ydcod")
            .Width = 160
            .Visible = True
            .Caption = "CONCEPTO"
        End With
        With grmovimiento.RootTable.Columns("observacion")
            .Width = 250
            .Visible = True
            .Caption = "observacion".ToUpper
        End With



        With grmovimiento.RootTable.Columns("yddesc")
            .Width = 200
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "ALMACEN"
        End With
        With grmovimiento.RootTable.Columns("codInst")
            .Width = 90
            .Visible = False
        End With
        With grmovimiento.RootTable.Columns("nomInst")
            .Width = 90
            .Visible = False
        End With



        If (dt.Rows.Count <= 0) Then
            _prCargarDetalleGrupo(-1)
        End If
    End Sub
    Public Sub actualizarSaldoSinLote(ByRef dt As DataTable)
        'b.yfcdprod1 ,a.iclot ,a.icfven  ,a.iccven 

        'a.yfnumi  ,a.yfcdprod1  ,a.yfcdprod2,Sum(b.iccven ) as stock
        'Dim _detalle As DataTable = CType(grdetalle.DataSource, DataTable)
        Dim _dtDetalle As DataTable = CType(grdetalle.DataSource, DataTable)
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim sum As Integer = 0
            Dim codProducto As Integer = dt.Rows(i).Item("yfnumi")
            For j As Integer = 0 To _dtDetalle.Rows.Count - 1 Step 1

                Dim estado As Integer = _dtDetalle.Rows(j).Item("estado")
                If (estado = 0) Then
                    If (codProducto = _dtDetalle.Rows(j).Item("iccprod")) Then
                        sum = sum + _dtDetalle.Rows(j).Item("iccant")
                    End If
                End If
            Next
            dt.Rows(i).Item("stock") = dt.Rows(i).Item("stock") - sum
        Next

    End Sub
    Private Sub _prCargarProductos()
        Dim dt As New DataTable
        dt = L_fnListarCanerosxInst(CInt(_CodInstitucion))
        grCanero.DataSource = dt
        grCanero.RetrieveStructure()
        grCanero.AlternatingColors = True


        With grCanero.RootTable.Columns("ydnumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False
        End With

        With grCanero.RootTable.Columns("ydcod")
            .Width = 120
            .Visible = True
            .Caption = "COD. CAÑERO"
        End With
        With grCanero.RootTable.Columns("ydrazonsocial")
            .Width = 300
            .Visible = True
            .Caption = "NOMBRE CAÑERO"
        End With
        With grCanero.RootTable.Columns("ydtelf1")
            .Width = 90
            .Visible = False
        End With



        With grCanero
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2003



        End With
    End Sub


    Private Sub _prAddDetalleVenta()
        'If grdetalle.RowCount > 0 Then
        '    Return
        'End If
        Dim Bin As New MemoryStream
        Dim img As New Bitmap(My.Resources.delete, 28, 28)
        'img.Save(Bin, Imaging.ImageFormat.Png)
        CType(grdetalle.DataSource, DataTable).Rows.Add(0, 0, "", Bin.GetBuffer, 0)
    End Sub
    Public Function _fnSiguienteNumi()
        Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
        Dim mayor As Integer = 0
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim data As Integer = IIf(IsDBNull(CType(grdetalle.DataSource, DataTable).Rows(i).Item("icid")), 0, CType(grdetalle.DataSource, DataTable).Rows(i).Item("icid"))
            If (data > mayor) Then
                mayor = data

            End If
        Next
        Return mayor
    End Function
    Public Function _fnAccesible()
        Return tbFecha.IsInputReadOnly = False
    End Function
    Private Sub _HabilitarProductos()
        GPanelCanero.Visible = True

        PanelInferior.Visible = False
        _prCargarProductos()
        grCanero.Focus()
        grCanero.MoveTo(grCanero.FilterRow)
        grCanero.Col = 1
    End Sub
    Private Sub _DesHabilitarProductos()
        'If (GPanelCanero.Visible = True) Then
        '    GPanelCanero.Visible = False
        '    PanelInferior.Visible = True
        '    grdetalle.Select()
        '    grdetalle.Col = 4
        '    grdetalle.Row = grdetalle.RowCount - 1
        'End If
        'FilaSelectLote = Nothing
    End Sub
    Public Sub _fnObtenerFilaDetalle(ByRef pos As Integer, numi As Integer)
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _numi As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("ydcod")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next

    End Sub

    Public Function _fnExisteProducto(idprod As Integer) As Boolean
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _idprod As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("ydcod")
            Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado")
            If (_idprod = idprod And estado >= 0) Then

                Return True
            End If
        Next
        Return False
    End Function
    Public Sub _prEliminarFila()
        If (grdetalle.Row >= 0) Then
            If (grdetalle.RowCount >= 1) Then
                Dim estado As Integer = grdetalle.GetValue("estado")
                Dim pos As Integer = -1
                Dim lin As Integer = grdetalle.GetValue("ydcod")
                _fnObtenerFilaDetalle(pos, lin)
                If (estado = 0) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = -2

                End If
                If (estado = 1) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = -1
                End If
                grdetalle.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grdetalle.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThanOrEqualTo, 0))

                grdetalle.Select()
                grdetalle.Col = 4
                grdetalle.Row = grdetalle.RowCount - 1
            End If
        End If


    End Sub
    Public Function _ValidarCampos() As Boolean

        If (grdetalle.RowCount <= 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Inserte un Detalle".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            grdetalle.Focus()

            Return False

        End If
        'If (grdetalle.RowCount = 1) Then
        '    If (CType(grdetalle.DataSource, DataTable).Rows(0).Item("iccprod") = 0) Then
        '        Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
        '        ToastNotification.Show(Me, "Por Favor Inserte un Detalle".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        '        grdetalle.Focus()

        '        Return False
        '    End If
        'End If
        Return True
    End Function

    Sub _prGuardarTraspaso()
        Dim numi As String = ""
        Dim res As Boolean = True 'L_prMovimientoChoferGrabar(numi, tbFecha.Value.ToString("yyyy/MM/dd"), cbConcepto.Value, tbObservacion.Text, cbAlmacenOrigen.Value, cbDepositoDestino.Value, 0, CType(grdetalle.DataSource, DataTable))
        If res Then

            Dim numDestino As String = ""
            Dim resDestino As Boolean = True 'L_prMovimientoChoferGrabar(numDestino, tbFecha.Value.ToString("yyyy/MM/dd"), 5, tbObservacion.Text, cbDepositoDestino.Value, cbAlmacenOrigen.Value, numi, CType(grdetalle.DataSource, DataTable))
            If resDestino Then

                _prCargarVenta()

                _Limpiar()
                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                ToastNotification.Show(Me, "Código de Movimiento ".ToUpper + tbCodigo.Text + " Grabado con Exito.".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter
                                          )
            End If

        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "El Movimiento no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        End If
    End Sub
    Public Sub _GuardarNuevo()
        'ByRef _ibid As String, _ibfdoc As String, _ibconcep As Integer, _ibobs As String, _almacen As Integer

        'If (cbConcepto.Value = 6) Then
        '    _prGuardarTraspaso()
        '    Return

        'End If


        Dim res As Boolean = L_fnAgregarGrupo(tbCodigo.Text, _CodCliente, tbFecha.Value.ToString("dd/MM/yyyy"), tbObservacion.Text, CType(grdetalle.DataSource, DataTable)) 'L_prMovimientoChoferGrabar(numi, tbFecha.Value.ToString("yyyy/MM/dd"), cbConcepto.Value, tbObservacion.Text, cbAlmacenOrigen.Value, 0, 0, CType(grdetalle.DataSource, DataTable))
        If res Then

            _prCargarVenta()

            _Limpiar()
            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Movimiento ".ToUpper + tbCodigo.Text + " Grabado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "El Movimiento no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        End If


    End Sub
    Private Sub _prGuardarModificado()
        _CodCliente = grmovimiento.GetValue("ydnumi")
        Dim res As Boolean = L_prGrupoModificar(tbCodigo.Text, _CodCliente, tbFecha.Value.ToString("dd/MM/yyyy"), tbObservacion.Text, CType(grdetalle.DataSource, DataTable))
        If res Then

            _prCargarVenta()

            _prSalir()

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Movimiento ".ToUpper + tbCodigo.Text + " Modificado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )

        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La Venta no pudo ser Modificada".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If
    End Sub
    Private Sub _prSalir()
        If btnGrabar.Enabled = True Then
            _prInhabiliitar()
            If grmovimiento.RowCount > 0 Then

                _prMostrarRegistro(0)

            End If
        Else

            ' _modulo.Select()
            Me.Close()

        End If
    End Sub
    Public Sub _prCargarIconELiminar()

        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim Bin As New MemoryStream
            Dim img As New Bitmap(My.Resources.delete, 28, 28)
            img.Save(Bin, Imaging.ImageFormat.Png)
            CType(grdetalle.DataSource, DataTable).Rows(i).Item("img") = Bin.GetBuffer
            grdetalle.RootTable.Columns("img").Visible = True
        Next
    End Sub
    Public Sub _PrimerRegistro()
        Dim _MPos As Integer
        If grmovimiento.RowCount > 0 Then
            _MPos = 0
            ''   _prMostrarRegistro(_MPos)
            grmovimiento.Row = _MPos
        End If
    End Sub

    Public Sub InsertarProductosSinLote()
        Try
            Dim pos As Integer = -1
            grdetalle.Row = grdetalle.RowCount - 1
            _fnObtenerFilaDetalle(pos, grdetalle.GetValue("ydcod"))

            Dim existe As Boolean = _fnExisteProducto(grCanero.GetValue("ydcod"))
            If ((pos >= 0) And (Not existe)) Then
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("ydnumi") = grCanero.GetValue("ydnumi")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("ydcod") = grCanero.GetValue("ydcod")
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("ydrazonsocial") = grCanero.GetValue("ydrazonsocial")


                ''    _DesHabilitarProductos()

                _prAddDetalleVenta()

                _prCargarProductos()
                'grproducto.RemoveFilters()
                grCanero.Focus()
                grCanero.MoveTo(grCanero.FilterRow)
                grCanero.Col = 1
            Else
                If (existe) Then
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "El cañero ya existe en el detalle".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    grCanero.RemoveFilters()
                    grCanero.Focus()
                    grCanero.MoveTo(grCanero.FilterRow)
                    grCanero.Col = 1
                End If

            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try

    End Sub

    Public Function _fnExisteProductoConLote(idprod As Integer, lote As String, fechaVenci As Date) As Boolean
        For i As Integer = 0 To CType(grdetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _idprod As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("iccprod")
            Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(i).Item("estado")
            '          a.tbnumi ,a.tbtv1numi ,a.tbty5prod ,b.yfcdprod1 as producto,a.tbest ,a.tbcmin ,a.tbumin ,Umin .ycdes3 as unidad,a.tbpbas ,a.tbptot ,a.tbobs ,
            'a.tbpcos,a.tblote ,a.tbfechaVenc , a.tbptot2, a.tbfact ,a.tbhact ,a.tbuact,1 as estado,Cast(null as Image) as img,
            'Cast (0 as decimal (18,2)) as stock
            Dim _LoteDetalle As String = CType(grdetalle.DataSource, DataTable).Rows(i).Item("iclot")
            Dim _FechaVencDetalle As Date = CType(grdetalle.DataSource, DataTable).Rows(i).Item("icfvenc")
            If (_idprod = idprod And estado >= 0 And lote = _LoteDetalle And fechaVenci = _FechaVencDetalle) Then

                Return True
            End If
        Next
        Return False
    End Function
    Public Sub _fnObtenerFilaDetalleProducto(ByRef pos As Integer, numi As Integer)
        For i As Integer = 0 To CType(grCanero.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _numi As Integer = CType(grCanero.DataSource, DataTable).Rows(i).Item("yfnumi")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next

    End Sub

    Private Sub MostrarMensajeError(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.WARNING,
                               5000,
                               eToastGlowColor.Red,
                               eToastPosition.TopCenter)

    End Sub
    Private Sub MostrarMensajeOk(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.OK,
                               5000,
                               eToastGlowColor.Green,
                               eToastPosition.TopCenter)
    End Sub
#End Region

#Region "Eventos Formulario"



    Private Sub F0_Movimiento_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _IniciarTodo()
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        _Limpiar()
        _prhabilitar()

        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
        btnGrabar.Enabled = True
        PanelInferior.Enabled = False
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _prSalir()
    End Sub

    Private Sub grdetalle_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grdetalle.EditingCell
        'If (_fnAccesible()) Then

        '    'Habilitar solo las columnas de Precio, %, Monto y Observación
        '    If (e.Column.Index = grdetalle.RootTable.Columns("iccant").Index) Then
        '        e.Cancel = False
        '    Else
        '        If ((e.Column.Index = grdetalle.RootTable.Columns("iclot").Index Or
        '            e.Column.Index = grdetalle.RootTable.Columns("icfvenc").Index) And Lote = True) Then 'And (cbConcepto.Value = 1)
        '            e.Cancel = False
        '        Else
        '            e.Cancel = True
        '        End If
        '    End If


        'Else
        '    e.Cancel = True
        'End If
    End Sub

    Private Sub grdetalle_KeyDown(sender As Object, e As KeyEventArgs) Handles grdetalle.KeyDown
        If (Not _fnAccesible()) Then
            Return
        End If

        'If (e.KeyData = Keys.Enter) Then
        '    Dim f, c As Integer
        '    c = grdetalle.Col
        '    f = grdetalle.Row

        '    If (grdetalle.Col = grdetalle.RootTable.Columns("ydcod").Index) Then
        '        If (grdetalle.GetValue("producto") <> String.Empty) Then
        '            _prAddDetalleVenta()
        '            _HabilitarProductos()
        '        Else
        '            ToastNotification.Show(Me, "Seleccione un Producto Por Favor", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
        '        End If

        '    End If
        '    If (grdetalle.Col = grdetalle.RootTable.Columns("ydcod").Index) Then
        '        If (grdetalle.GetValue("producto") <> String.Empty) Then
        '            _prAddDetalleVenta()
        '            _HabilitarProductos()
        '        Else
        '            ToastNotification.Show(Me, "Seleccione un Producto Por Favor", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
        '        End If

        '    End If
salirIf:
        'End If

        If (e.KeyData = Keys.Control + Keys.Enter And grdetalle.Row >= 0 And
            grdetalle.Col = grdetalle.RootTable.Columns("ydcod").Index) Then
            Dim indexfil As Integer = grdetalle.Row
            Dim indexcol As Integer = grdetalle.Col
            _HabilitarProductos()

        End If
        If (e.KeyData = Keys.Escape And grdetalle.Row >= 0) Then

            _prEliminarFila()


        End If



    End Sub


    Private Sub grproducto_KeyDown(sender As Object, e As KeyEventArgs) Handles grCanero.KeyDown
        If (Not _fnAccesible()) Then
            Return
        End If


        If (e.KeyData = Keys.Enter) Then
            Dim f, c As Integer
            c = grCanero.Col
            f = grCanero.Row
            If (f >= 0) Then

                If (IsNothing(FilaSelectLote)) Then

                    InsertarProductosSinLote()

                Else


                    Dim pos As Integer = -1
                    grdetalle.Row = grdetalle.RowCount - 1
                    _fnObtenerFilaDetalle(pos, grdetalle.GetValue("icid"))
                    Dim numiProd As Integer = FilaSelectLote.Item("yfnumi")
                    Dim lote As String = grCanero.GetValue("iclot")
                    Dim FechaVenc As Date = grCanero.GetValue("icfven")
                    If (Not _fnExisteProductoConLote(numiProd, lote, FechaVenc)) Then
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("iccprod") = FilaSelectLote.Item("yfnumi")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("producto") = FilaSelectLote.Item("yfcdprod1")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("iccant") = 1

                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("stock") = grCanero.GetValue("stock")

                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("iclot") = grCanero.GetValue("iclot")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("icfvenc") = grCanero.GetValue("icfven")

                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("Laboratorio") = FilaSelectLote.Item("Laboratorio")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("Presentacion") = FilaSelectLote.Item("Presentacion")
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("yfcprod") = FilaSelectLote.Item("yfcprod")

                        FilaSelectLote = Nothing
                        _DesHabilitarProductos()

                    Else
                        Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                        ToastNotification.Show(Me, "El producto con el lote ya existe modifique su cantidad".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    End If



                End If


            End If
        End If
        If e.KeyData = Keys.Escape Then
            CType(grCanero.DataSource, DataTable).Rows.Clear()

            _DesHabilitarProductos()
        End If
    End Sub

    Private Sub grdetalle_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles grdetalle.CellValueChanged

        If (e.Column.Index = grdetalle.RootTable.Columns("iccant").Index) Then
            If (Not IsNumeric(grdetalle.GetValue("iccant")) Or grdetalle.GetValue("iccant").ToString = String.Empty) Then

                'grDetalle.GetRow(rowIndex).Cells("cant").Value = 1
                '  grDetalle.CurrentRow.Cells.Item("cant").Value = 1
                Dim lin As Integer = grdetalle.GetValue("icid")
                Dim pos As Integer = -1
                _fnObtenerFilaDetalle(pos, lin)
                CType(grdetalle.DataSource, DataTable).Rows(pos).Item("iccant") = 1

                Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")

                If (estado = 1) Then
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                End If

            Else
                If (grdetalle.GetValue("iccant") > 0) Then
                    Dim lin As Integer = grdetalle.GetValue("icid")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")

                    If (estado = 1) Then
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                    End If

                Else
                    Dim lin As Integer = grdetalle.GetValue("icid")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grdetalle.DataSource, DataTable).Rows(pos).Item("iccant") = 1
                    Dim estado As Integer = CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado")

                    If (estado = 1) Then
                        CType(grdetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                    End If

                End If
            End If
        End If
    End Sub



    Private Sub grdetalle_MouseClick(sender As Object, e As MouseEventArgs) Handles grdetalle.MouseClick
        If (Not _fnAccesible()) Then
            Return
        End If

        If (grdetalle.RowCount >= 1) Then
            If (grdetalle.CurrentColumn.Index = grdetalle.RootTable.Columns("img").Index) Then
                If grdetalle.GetValue("ydnumi") = 0 And grdetalle.GetValue("ydcod") = 0 And grdetalle.GetValue("ydrazonsocial") = "" Then
                Else
                    _prEliminarFila()
                End If
            End If
        End If


    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        If _ValidarCampos() = False Then
            Exit Sub
        End If

        If (tbCodigo.Text = String.Empty) Then
            _GuardarNuevo()
        Else
            If (tbCodigo.Text <> String.Empty) Then
                _prGuardarModificado()
                ''    _prInhabiliitar()

            End If
        End If
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        If (grdetalle.RowCount > 0) Then

            _prhabilitar()
            btnNuevo.Enabled = False
            btnModificar.Enabled = False
            btnEliminar.Enabled = False
            btnGrabar.Enabled = True
            _prAddDetalleVenta()
            PanelInferior.Enabled = False
            _prCargarIconELiminar()
        Else
            _prhabilitar()
            btnNuevo.Enabled = False
            btnModificar.Enabled = False
            btnEliminar.Enabled = False
            btnGrabar.Enabled = True
            _prAddDetalleVenta()
            PanelInferior.Enabled = False
            _prCargarIconELiminar()
            _prAddDetalleVenta()

        End If
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim ef = New Efecto


        ef.tipo = 2
        ef.Context = "¿esta seguro de eliminar el registro?".ToUpper
        ef.Header = "mensaje principal".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            Dim mensajeError As String = ""
            L_prGrupoEliminar(tbCodigo.Text)
            'If res Then


            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)

                ToastNotification.Show(Me, "Código de Movimiento ".ToUpper + tbCodigo.Text + " eliminado con Exito.".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter)

                _prFiltrar()

            'Else
            '    Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            '    ToastNotification.Show(Me, mensajeError, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            'End If
        End If
    End Sub

    Private Sub grmovimiento_SelectionChanged(sender As Object, e As EventArgs) Handles grmovimiento.SelectionChanged
        If (grmovimiento.RowCount >= 0 And grmovimiento.Row >= 0) Then

            _prMostrarRegistro(grmovimiento.Row)
        End If
    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        Dim _pos As Integer = grmovimiento.Row
        If _pos < grmovimiento.RowCount - 1 Then
            _pos = grmovimiento.Row + 1
            '' _prMostrarRegistro(_pos)
            grmovimiento.Row = _pos
        End If
    End Sub

    Private Sub btnPrimero_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click
        _PrimerRegistro()
    End Sub

    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        Dim _pos As Integer = grmovimiento.Row
        If grmovimiento.RowCount > 0 Then
            _pos = grmovimiento.RowCount - 1
            ''  _prMostrarRegistro(_pos)
            grmovimiento.Row = _pos
        End If
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        Dim _MPos As Integer = grmovimiento.Row
        If _MPos > 0 And grmovimiento.RowCount > 0 Then
            _MPos = _MPos - 1
            ''  _prMostrarRegistro(_MPos)
            grmovimiento.Row = _MPos
        End If
    End Sub

    Private Sub grdetalle_Enter(sender As Object, e As EventArgs) Handles grdetalle.Enter
        If (grdetalle.RowCount > 0) Then
            grdetalle.Select()
            grdetalle.Col = 3
            grdetalle.Row = 0
        End If
    End Sub

    Private Sub grmovimiento_KeyDown(sender As Object, e As KeyEventArgs) Handles grmovimiento.KeyDown
        If e.KeyData = Keys.Enter Then
            MSuperTabControl.SelectedTabIndex = 0
            grdetalle.Focus()

        End If
    End Sub

    Private Sub cbAlmacen_KeyDown(sender As Object, e As KeyEventArgs)
        If (_fnAccesible()) Then
            If e.KeyData = Keys.Enter Then
                grdetalle.Focus()
                'grdetalle.Select()
                grdetalle.Col = 2
                grdetalle.Row = 0
            End If
        End If
    End Sub

    Private Sub cbConcepto_ValueChanged(sender As Object, e As EventArgs)



    End Sub

    Private Sub cbAlmacenOrigen_ValueChanged(sender As Object, e As EventArgs)
        If (_fnAccesible() And tbCodigo.Text = String.Empty) Then
            CType(grdetalle.DataSource, DataTable).Rows.Clear()
            _prAddDetalleVenta()
            _DesHabilitarProductos()

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

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        P_GenerarReporte()
    End Sub
    Private Sub P_GenerarReporte()
        Try
            Dim dtDetalle As DataTable = L_fnDetalleMovimiento(tbCodigo.Text)

            If Not IsNothing(P_Global.Visualizador) Then
                P_Global.Visualizador.Close()
            End If

            P_Global.Visualizador = New Visualizador
            Dim objrep As New R_Movimiento

            objrep.SetDataSource(dtDetalle)
            objrep.SetParameterValue("idMovimiento", tbCodigo.Text)
            objrep.SetParameterValue("fecha", tbFecha.Text)
            objrep.SetParameterValue("obs", tbObservacion.Text)
            objrep.SetParameterValue("usuario", L_Usuario)

            P_Global.Visualizador.CrGeneral.ReportSource = objrep
            P_Global.Visualizador.Show()
            P_Global.Visualizador.BringToFront()
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub tbCliente_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCliente.KeyDown
        If (_fnAccesible()) Then

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
                ef.Context = "Seleccione Cañero".ToUpper
                ef.ShowDialog()
                Dim bandera As Boolean = False
                bandera = ef.band
                If (bandera = True) Then
                    Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                    '_codCaneroUcg = Row.Cells("ydcod").Value
                    If verificarcanero(Row.Cells("ydnumi").Value) Then
                        _CodCliente = Row.Cells("ydnumi").Value
                        tbCliente.Text = Row.Cells("ydrazonsocial").Value
                        tbCodCanero.Text = Row.Cells("ydcod").Value

                        '_dias = Row.Cells("yddias").Value
                        'tbNit.Text = Row.Cells("ydnit").Value
                        'TbNombre1.Text = Row.Cells("ydnomfac").Value
                        'tipoDocumento = Row.Cells("ydtipdocelec").Value
                        'correo = Row.Cells("ydcorreo").Value
                        'tbComplemento.Text = Row.Cells("ydcompleCi").Value
                        Dim numiVendedor As Integer = IIf(IsDBNull(Row.Cells("ydnumivend").Value), 0, Row.Cells("ydnumivend").Value)
                        If (numiVendedor > 0) Then
                            ' tbVendedor.Text = Row.Cells("vendedor").Value
                            _CodEmpleado = Row.Cells("ydnumivend").Value

                            grdetalle.Select()
                            Table_producto = Nothing
                        Else
                            tbVendedor.Clear()
                            _CodEmpleado = 0
                            tbVendedor.Focus()
                            Table_producto = Nothing

                        End If

                    Else
                        Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                        ToastNotification.Show(Me, "El cañero ya se encuentra asignado a un grupo economico".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.TopCenter)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub tbCliente_TextChanged(sender As Object, e As EventArgs) Handles tbCliente.TextChanged
        If btnNuevo.Enabled = False Then
            Dim dt As DataTable
            dt = L_fnListarCaneroInstitucion(_CodCliente)
            Dim row As DataRow = dt.Rows(dt.Rows.Count - 1)
            tbVendedor.Text = row("institucion")
            tbCodInst.Text = row("codInst")
            _CodInstitucion = row("id")
        End If
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Dim dt1 As DataTable
        dt1 = verificarGrupoEconomico("tg001", codCaneroB.Text)
        If dt1.Rows.Count > 0 Then
            '_numi = _Tabla.Rows(0).Item(0)
            '_Error = False
            MessageBox.Show("EL CAÑERO !---" + codCaneroB.Text + "-" + dt1.Rows(0).Item(3).ToString + "---! PERTENECE AL GRUPO ECONÓMICO --->" + dt1.Rows(0).Item(1).ToString)
        Else
            dt1 = verificarGrupoEconomicoDet("tg0011", codCaneroB.Text)
            If dt1.Rows.Count > 0 Then
                '_numi = _Tabla.Rows(0).Item(0)
                '_Error = False
                MessageBox.Show("EL CAÑERO !---" + codCaneroB.Text + "-" + dt1.Rows(0).Item(2).ToString + "---! PERTENECE AL GRUPO ECONÓMICO --->" + dt1.Rows(0).Item(0).ToString)
            Else

                MessageBox.Show("NO PERTENECE A NINGÚN GRUPO ECONÓMICO")
            End If
        End If
    End Sub
#End Region
End Class