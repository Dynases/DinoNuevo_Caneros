Imports System.Drawing.Printing
Imports System.IO
Imports CrystalDecisions.Shared
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports Facturacion
Imports Janus.Windows.GridEX
Imports Logica.AccesoLogica
Imports UTILITIES
Imports System

Public Class F0_RegistroBoleta
    Dim _Inter As Integer = 0
#Region "Variables Globales"
    Dim _CodCliente As Integer = 0
    Dim _CodEmpleado As Integer = 0
    Dim _CodInstitucion As Integer = 0
    Dim OcultarFact As Integer = 0
    Dim _codeBar As Integer = 1
    Dim _dias As Integer = 0
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Dim FilaSelectLote As DataRow = Nothing

    Public Programa As String
    Dim _codBoleta As Integer = 0
    Dim _Nuevo As Boolean
#End Region



    Public CodProducto As String
    Public Cantidad As Integer

    Public NombreProd As String

    Public fechaEmision As String
    Dim placa, propietario As String
    Dim pesoTara As Decimal

#Region "Metodos Privados"
    Private Sub _IniciarTodo()
        L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        MSuperTabControl.SelectedTabIndex = 0

        _prCargarComboLibreria(cbgrupo1, 11, 1)
        _prCargarComboLibreria(cbgrupo2, 11, 2)
        _prCargarComboLibreria(cbgrupo3, 11, 3)
        _prCargarComboLibreria(cbGrupoEstado, 11, 4)

        cbgrupo1.SelectedIndex = 0
        cbgrupo2.SelectedIndex = 0
        cbgrupo3.SelectedIndex = 0

        _prCargarCabecera()
        _prInhabiliitar()
        grVentas.Focus()
        Me.Text = "REGISTRO DE BOLETAS"
        Dim blah As New Bitmap(New Bitmap(My.Resources.compra), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        _prAsignarPermisos()
        _prCargarNameLabel()

        Programa = P_Principal.btVentVenta.Text
    End Sub

    Public Sub _prCargarNameLabel()
        Dim dt As DataTable = L_fnNameLabel()
        If (dt.Rows.Count > 0) Then
            _codeBar = 1
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

        tbCodigo.IsInputReadOnly = True
        tbCliente.ReadOnly = True
        tbVendedor.ReadOnly = True
        tbControlTotal.IsInputReadOnly = True
        tbPaquetes.IsInputReadOnly = True
        tbCodigoTara.ReadOnly = True
        'tbPesoTara.IsInputReadOnly = True
        tbPesoBruto.IsInputReadOnly = True
        cbgrupo1.ReadOnly = True
        cbgrupo2.ReadOnly = True
        cbgrupo3.ReadOnly = True
        cbGrupoEstado.ReadOnly = True
        btnAgregar.Enabled = False

        tbFechaVenta.IsInputReadOnly = True
        tbFechaVenta.Enabled = True


        btnModificar.Enabled = True
        btnGrabar.Enabled = False
        btnNuevo.Enabled = True
        'btnEliminar.Enabled = True



        grVentas.Enabled = True
        PanelNavegacion.Enabled = True
        grdetalle1.RootTable.Columns("img").Visible = False


        tbNit.ReadOnly = True
        TbNombre1.ReadOnly = True
        TbNombre2.ReadOnly = True
        FilaSelectLote = Nothing
    End Sub
    Private Sub _prhabilitar()
        grVentas.Enabled = False
        tbCodigo.IsInputReadOnly = False
        tbControlTotal.IsInputReadOnly = False
        tbFechaVenta.IsInputReadOnly = False
        tbFechaVenta.Enabled = True
        TbNombre1.ReadOnly = False
        tbVendedor.ReadOnly = True

        tbPaquetes.IsInputReadOnly = False
        tbCodigoTara.ReadOnly = False
        tbPesoBruto.IsInputReadOnly = False
        cbgrupo1.ReadOnly = False
        cbgrupo2.ReadOnly = False
        cbgrupo3.ReadOnly = False
        cbGrupoEstado.ReadOnly = False
        btnAgregar.Enabled = True
        btnGrabar.Enabled = True

        TbNombre2.ReadOnly = False
    End Sub

    Public Sub _prFiltrar()
        'cargo el buscador
        Dim _Mpos As Integer
        _prCargarCabecera()
        If grVentas.RowCount > 0 Then
            _Mpos = 0
            grVentas.Row = _Mpos
        Else
            _Limpiar()
            LblPaginacion.Text = "0/0"
        End If
    End Sub
    Private Sub _Limpiar()
        tbCodigo.Text = ""
        tbCliente.Clear()
        tbVendedor.Clear()
        tbTotalDo.Text = "0"
        _CodCliente = 0
        _CodEmpleado = 0
        'tbFechaVenta.Value = Now.Date
        tbControlTotal.Text = ""
        cbGrupoEstado.Value = 1
        MSuperTabControl.SelectedTabIndex = 0
        _prCargarDetalleVenta(-1)
        With grdetalle1.RootTable.Columns("img")
            .Width = 80
            .Caption = "Eliminar"
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = True
        End With

        tbNit.Clear()
        TbNombre1.Clear()
        TbNombre2.Clear()
        tbCliente.Select()

    End Sub
    Public Sub _prMostrarRegistro(_N As Integer)

        With grVentas

            TbNombre2.Text = .GetValue("idBoleta")
            tbCodigo.Text = .GetValue("nroBoleta")
            tbFechaVenta.Value = .GetValue("fchBol")
            _CodEmpleado = .GetValue("codCan")
            _CodInstitucion = .GetValue("codInst")
            tbVendedor.Text = .GetValue("nomInst")
            _CodCliente = .GetValue("codCan")
            tbCliente.Text = .GetValue("yddesc")
            tbControlTotal.Text = .GetValue("controlTotal")
            cbGrupoEstado.Value = .GetValue("estado")
            If cbGrupoEstado.Value = 1 Then
                cbGrupoEstado.BackColor = Color.Green

            ElseIf cbGrupoEstado.Value = 3 Then
                cbGrupoEstado.BackColor = Color.Red
            Else
                cbGrupoEstado.BackColor = Color.White
            End If

            tbNit.Text = .GetValue("idInstitucion").ToString
            TbNombre1.Text = .GetValue("ydcod").ToString

            lbFecha.Text = CType(.GetValue("fchBol"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("tahact").ToString
            lbUsuario.Text = .GetValue("tauact").ToString
            cbgrupo1.Value = .GetValue("cupo")
        End With

        _prCargarDetalleVenta(TbNombre2.Text)



        LblPaginacion.Text = Str(grVentas.Row + 1) + "/" + grVentas.RowCount.ToString

    End Sub

    Private Sub _prCargarDetalleVenta(_numi As String)
        Dim dt As New DataTable
        dt = L_fnDetalleBoleta(_numi)
        grdetalle1.DataSource = dt
        grdetalle1.RetrieveStructure()
        grdetalle1.AlternatingColors = True


        With grdetalle1.RootTable.Columns("numi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False

        End With

        With grdetalle1.RootTable.Columns("nroBoleta1")
            .Width = 90
            .Caption = ""
            .Visible = False
        End With
        With grdetalle1.RootTable.Columns("numLinea")
            .Width = 100
            .Caption = "Nº LINEA"
            .Visible = True

        End With
        With grdetalle1.RootTable.Columns("nroPaq")
            .Width = 90
            .Caption = "PAQUETE"
            .Visible = True
        End With
        With grdetalle1.RootTable.Columns("codTara")
            .Width = 90
            .Visible = True
            .Caption = "TARA"
        End With

        With grdetalle1.RootTable.Columns("tipCan")
            .Width = 90
            .Visible = False
            .Caption = "TIPO CAÑA"
        End With

        With grdetalle1.RootTable.Columns("ycdes3")
            .Width = 90
            .Visible = True
            .Caption = "TIPO CORTE"
        End With

        With grdetalle1.RootTable.Columns("tipcor")
            .Width = 95
            .Caption = "TIPO CORTE"
            .Visible = False
        End With
        With grdetalle1.RootTable.Columns("corte")
            .Width = 95
            .Caption = "TIPO CAÑA"
            .Visible = True
        End With
        With grdetalle1.RootTable.Columns("pesBru")
            .Width = 130
            .FormatString = "0.00"
            .Visible = True
            .Caption = "PESO BRUTO"
        End With
        With grdetalle1.RootTable.Columns("pesTara")
            .Width = 130
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .FormatString = "0.00"
            .Visible = True
            .Caption = "PESO TARA"
        End With
        With grdetalle1.RootTable.Columns("placa")
            .Width = 100
            .Visible = True
            .Caption = "PLACA".ToUpper
        End With


        With grdetalle1.RootTable.Columns("fleter")
            .Width = 150
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Caption = "FLETERO"
            .Visible = True
        End With



        With grdetalle1.RootTable.Columns("tbfact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle1.RootTable.Columns("tbhact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle1.RootTable.Columns("tbuact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle1.RootTable.Columns("pesNeto")
            .Width = 130
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .FormatString = "0.00"
            .Caption = "PESO NETO"
        End With
        With grdetalle1.RootTable.Columns("estado")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grdetalle1.RootTable.Columns("img")
            .Width = 80
            .Caption = "Eliminar".ToUpper
            .CellStyle.ImageHorizontalAlignment = ImageHorizontalAlignment.Center
            .Visible = False
        End With

        With grdetalle1
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
        End With
    End Sub

    Private Sub _prCargarCabecera()
        Dim dt As New DataTable
        dt = L_fnCabeceraBoleta()
        grVentas.DataSource = dt
        grVentas.RetrieveStructure()
        grVentas.AlternatingColors = True
        With grVentas.RootTable.Columns("idBoleta")
            .Width = 100
            .Caption = "BOLETA"
            .Visible = False

        End With

        With grVentas.RootTable.Columns("nroBoleta")
            .Width = 100
            .Caption = "BOLETA"
            .Visible = True

        End With

        With grVentas.RootTable.Columns("fchBol")
            .Width = 90
            .Caption = "FECHA"
            .Visible = True
        End With

        With grVentas.RootTable.Columns("codCan")
            .Width = 90
            .Visible = False
        End With
        With grVentas.RootTable.Columns("yddesc")
            .Width = 250
            .Caption = "CAÑERO"
            .Visible = True
        End With
        With grVentas.RootTable.Columns("codInst")
            .Width = 90
            .Visible = False
            .Caption = "FECHA"
        End With
        With grVentas.RootTable.Columns("ydcod")
            .Width = 130
            .Visible = True
            .Caption = "COD CAÑERO"
        End With
        With grVentas.RootTable.Columns("idinstitucion")
            .Width = 90
            .Visible = False
            .Caption = "FECHA"
        End With
        With grVentas.RootTable.Columns("nomInst")
            .Width = 300
            .Caption = "INSTITUCIÓN"
            .Visible = True
        End With

        With grVentas.RootTable.Columns("cupo")
            .Width = 90
            .Visible = False
            .Caption = "FECHA"
        End With
        With grVentas.RootTable.Columns("estado")
            .Width = 90
            .Visible = False
            .Caption = "FECHA"
        End With
        With grVentas.RootTable.Columns("hora")
            .Width = 160
            .Visible = False
        End With
        With grVentas.RootTable.Columns("controlTotal")
            .Width = 160
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tahact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tauact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        With grVentas
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla

        End With

        If (dt.Rows.Count <= 0) Then
            _prCargarDetalleVenta(-1)
        End If
    End Sub
    Public Sub _prGuardar()
        If _ValidarCampos() = False Then
            Exit Sub
        End If

        Dim controlTotalHead As Integer = 0
        Dim controlTotalDet As Integer = 0
        controlTotalHead = tbCodigo.Value + Convert.ToInt32(TbNombre1.Text) + Convert.ToInt32(tbNit.Text) + Convert.ToInt32(IIf(cbgrupo1.Value = 3, 13, IIf(cbgrupo1.Value = 2, 9, 6)))
        Dim dt As DataTable

        dt = CType(grdetalle1.DataSource, DataTable)
        'Ordena el detalle por codigo importante
        dt.DefaultView.Sort = "nroPaq ASC"
        dt = dt.DefaultView.ToTable


        Try
            For i As Integer = 0 To dt.Rows.Count Step 1

                Dim estado As Integer = dt.Rows(i).Item("estado")

                If (estado >= 0) Then

                    controlTotalDet = controlTotalDet + (dt.Rows(i).Item("nroPaq") * dt.Rows(i).Item("numLinea")) + (dt.Rows(i).Item("codTara") * dt.Rows(i).Item("numLinea")) + (dt.Rows(i).Item("tipCor") * dt.Rows(i).Item("numLinea") + IIf(dt.Rows(i).Item("tipCan") = 1, dt.Rows(i).Item("numLinea") * 3, dt.Rows(i).Item("numLinea") * 5)) + (dt.Rows(i).Item("pesBru") * dt.Rows(i).Item("numLinea")) + (dt.Rows(i).Item("pesTara") * dt.Rows(i).Item("numLinea"))

                End If

            Next

        Catch ex As Exception
            ' MostrarMensajeError(ex.Message)

        End Try
        Dim ef = New Efecto
        ef.tipo = 1
        ef.Context = "La boleta " + tbCodigo.Value.ToString + " se registrara con fecha: " + tbFechaVenta.Text.ToUpper
        ef.Header = "¿esta seguro de registrar esta boleta?".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            If (_Nuevo = True) Then
                If (controlTotalHead + controlTotalDet) = tbControlTotal.Value Then
                    _GuardarNuevo()
                Else
                    MostrarMensajeError("No Coincide Con el Control Total")
                End If


            Else
                If (controlTotalHead + controlTotalDet) = tbControlTotal.Value Then
                    _prGuardarModificado()
                Else
                    MostrarMensajeError("No Coincide Con el Control Total")
                End If
            End If
        End If

    End Sub




    Private Sub _prAddDetalleVenta()
        Dim Bin As New MemoryStream
        Dim img As New Bitmap(My.Resources.delete, 28, 28)
        img.Save(Bin, Imaging.ImageFormat.Png)
        CType(grdetalle1.DataSource, DataTable).Rows.Add(_fnSiguienteNumi() + 1, _fnSiguienteNumi1() + 1, 0, 0, 0, 0, "", 0, "", 0.00, 0.00, "", "", CDate("2017/01/01"), "", "", 0.00, 0, Bin.GetBuffer)

    End Sub

    Public Function _fnSiguienteNumi()
        Dim dt As DataTable = CType(grdetalle1.DataSource, DataTable)
        Dim rows() As DataRow = dt.Select("numi=MAX(numi)")
        If (rows.Count > 0) Then
            Return rows(rows.Count - 1).Item("numi")
        End If
        Return 1
    End Function
    Public Function _fnSiguienteNumi1()
        Dim dt As DataTable = CType(grdetalle1.DataSource, DataTable)
        Dim rows() As DataRow = dt.Select("estado=1")
        If (rows.Count > 0) Then
            If rows(rows.Count - 1).Item("estado") = "-1" Then
                Return rows(rows.Count - 1).Item("numLinea")
            Else
                Return rows(rows.Count - 1).Item("numLinea")
            End If

        End If
        Return 0
    End Function
    Public Function _fnAccesible()
        Return tbFechaVenta.IsInputReadOnly = False
    End Function
    Private Sub _HabilitarFocoDetalle(fila As Integer)

        grdetalle1.Focus()
        grdetalle1.Row = fila
        grdetalle1.Col = 1
    End Sub
    Private Sub _DesHabilitarProductos()

        PanelInferior.Visible = True


        grdetalle1.Select()
        grdetalle1.Col = 5
        grdetalle1.Row = grdetalle1.RowCount - 1

    End Sub
    Public Sub _fnObtenerFilaDetalle(ByRef pos As Integer, numi As Integer)
        For i As Integer = 0 To CType(grdetalle1.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _numi As Integer = CType(grdetalle1.DataSource, DataTable).Rows(i).Item("numi")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next

    End Sub


    Public Function _fnExisteProducto(idprod As Integer) As Boolean
        For i As Integer = 0 To CType(grdetalle1.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _idprod As Integer = CType(grdetalle1.DataSource, DataTable).Rows(i).Item("numi")
            Dim estado As Integer = CType(grdetalle1.DataSource, DataTable).Rows(i).Item("estado")
            If (_idprod = idprod And estado >= 0) Then

                Return True
            End If
        Next
        Return False
    End Function


    Public Sub P_PonerTotal(rowIndex As Integer)

        If (rowIndex < grdetalle1.RowCount) Then
            'grdetalle.UpdateData()
            Dim lin As Integer = grdetalle1.GetValue("tbnumi")
            Dim pos As Integer = -1
            _fnObtenerFilaDetalle(pos, lin)
            Dim cant As Double = grdetalle1.GetValue("tbcmin")
            'Dim cantidad = Format(cant,"0.00")
            Dim uni As Double = grdetalle1.GetValue("tbpbas")
            Dim cos As Double = grdetalle1.GetValue("tbpcos")
            Dim MontoDesc As Double = grdetalle1.GetValue("tbdesc")
            Dim dt As DataTable = CType(grdetalle1.DataSource, DataTable)
            If (pos >= 0) Then
                Dim TotalUnitario As Double = cant * uni
                Dim TotalCosto As Double = cant * cos
                'grDetalle.SetValue("lcmdes", montodesc)

                CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("tbptot") = TotalUnitario
                CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("tbtotdesc") = TotalUnitario - MontoDesc

                grdetalle1.SetValue("tbptot", TotalUnitario)
                grdetalle1.SetValue("tbtotdesc", TotalUnitario - MontoDesc)

                CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("tbptot2") = TotalCosto
                grdetalle1.SetValue("tbptot2", TotalCosto)

                Dim estado As Integer = CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("estado")
                If (estado = 1) Then
                    CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("estado") = 2
                End If
            End If
            _prCalcularPrecioTotal()
        End If
    End Sub

    'Public Sub _prCalcularPrecioTotal()


    '    Dim TotalDescuento As Double = 0
    '    Dim TotalCosto As Double = 0
    '    Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
    '    For i As Integer = 0 To dt.Rows.Count - 1 Step 1

    '        If (dt.Rows(i).Item("estado") >= 0) Then
    '            TotalDescuento = TotalDescuento + dt.Rows(i).Item("tbptot")
    '            TotalCosto = TotalCosto + dt.Rows(i).Item("tbptot2")
    '        End If
    '    Next




    'End Sub
    Public Sub _prEliminarFila()
        If (grdetalle1.Row >= 0) Then
            If (grdetalle1.RowCount >= 2) Then
                Dim estado As Integer = grdetalle1.GetValue("estado")
                Dim pos As Integer = -1
                Dim lin As Integer = grdetalle1.GetValue("numi")
                _fnObtenerFilaDetalle(pos, lin)
                If (estado = 0) Then
                    CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("estado") = -2
                End If
                If (estado = 1) Then
                    CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("estado") = -1
                    limpiarDetalle()
                End If

                'grdetalle.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grdetalle.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThanOrEqualTo, -3))

                grdetalle1.Select()
                grdetalle1.UpdateData()
                grdetalle1.Col = grdetalle1.RootTable.Columns("numi").Index
                grdetalle1.Row = grdetalle1.RowCount - 1
                grdetalle1.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grdetalle1.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThanOrEqualTo, 0))

                _prCalcularPrecioTotal()
            End If
        End If
        'grdetalle.Refetch()
        'grdetalle.Refresh()

    End Sub
    Public Function _ValidarCampos() As Boolean
        Try
            'txtMontoPagado1.Text = tbTotalBs.Text
            If (tbCodigo.Text = String.Empty) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor Ingrese el Número de Boleta".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                tbCodigo.Focus()
                Return False
            Else
                If _Nuevo Then
                    If L_BuscarCodBOleta(tbCodigo.Text) = True Then
                        tbCodigo.BackColor = Color.Red
                        MEP.SetError(tbCodigo, "Ingrese un código distinto!".ToUpper)
                        Return False
                    Else
                        tbCodigo.BackColor = Color.White
                        MEP.SetError(tbCodigo, String.Empty)
                    End If
                ElseIf _codBoleta = Convert.ToInt32(tbCodigo.Text) Then
                    tbCodigo.BackColor = Color.White
                    MEP.SetError(tbCodigo, String.Empty)
                    Return True
                Else
                    If L_BuscarCodBOleta(tbCodigo.Text) = True Then
                        tbCodigo.BackColor = Color.Red
                        MEP.SetError(tbCodigo, "Ingrese un código distinto!".ToUpper)
                        Return False
                    Else
                        tbCodigo.BackColor = Color.White
                        MEP.SetError(tbCodigo, String.Empty)
                    End If
                End If

            End If
            If (_CodCliente <= 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor Seleccione un Cañero con Ctrl+Enter".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                tbCliente.Focus()
                Return False

            End If
            If (_CodEmpleado <= 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor Seleccione un Vendedor con Ctrl+Enter".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                tbVendedor.Focus()
                Return False
            End If



            If (TbNombre1.Text = String.Empty) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor ponga la razon social del cliente.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                TbNombre1.Focus()
                Return False
            End If

            If (grdetalle1.RowCount = 1) Then
                grdetalle1.Row = grdetalle1.RowCount - 1
                If (grdetalle1.GetValue("nroPaq") = 0) Then
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "Por Favor Ingrese datos para Detalle de boleta".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    Return False
                End If

            End If



            Return True
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
            Return False
        End Try

    End Function
    Private Sub _prInsertarMontoNuevo(ByRef tabla As DataTable)
        'tabla.Rows.Add(0, tbMontoBs.Value, tbMontoDolar.Value, tbMontoTarej.Value, cbCambioDolar.Text, 0)
    End Sub
    Private Sub _prInsertarMontoModificar(ByRef tabla As DataTable)
        ' tabla.Rows.Add(tbCodigo.Text, tbMontoBs.Value, tbMontoDolar.Value, tbMontoTarej.Value, cbCambioDolar.Text, 2)
    End Sub
    Public Function rearmarDetalle() As DataTable
        Dim dt, dtDetalle, dtSaldos As DataTable
        Dim cantidadRepetido, contar, IdAux As Integer
        Dim ResultadoInventario = False

        dt = CType(grdetalle1.DataSource, DataTable)
        'Ordena el detalle por codigo importante
        dt.DefaultView.Sort = "nroPaq ASC"
        dt = dt.DefaultView.ToTable
        dtDetalle = dt.Copy
        dtDetalle.Clear()
        contar = 0
        Try
            For i As Integer = 0 To dt.Rows.Count - 1 Step 1
                Dim codProducto As Integer = dt.Rows(i).Item("nroPaq")
                dt.DefaultView.RowFilter = "nroPaq =  '" + codProducto.ToString() + "'"
                cantidadRepetido = dt.DefaultView.Count()
                If IdAux <> codProducto Then
                    contar = 1
                Else
                    contar += 1
                End If
                IdAux = codProducto

                'Dim cantidad As Double = dt.Rows(i).Item("tbcmin")
                Dim saldo As Double = Cantidad
                Dim estado As Integer = dt.Rows(i).Item("estado")
                Dim k As Integer = 0
                If (estado >= 0) Then



                    dtDetalle.ImportRow(dt.Rows(i))
                    Dim pos As Integer = dtDetalle.Rows.Count - 1

                    dtDetalle.Rows(pos).Item("tbptot") =
                    dtDetalle.Rows(pos).Item("tbtotdesc") = dtDetalle.Rows(pos).Item("tbdesc")
                    dtDetalle.Rows(pos).Item("tbcmin") = saldo
                    Dim precioCosto As Double = dtDetalle.Rows(pos).Item("tbpcos")
                    dtDetalle.Rows(pos).Item("tbptot2") = precioCosto * saldo
                    dtDetalle.Rows(pos).Item("tblote") = dtSaldos.Rows(k - 1).Item("iclot")
                    dtDetalle.Rows(pos).Item("tbfechaVenc") = dtSaldos.Rows(k - 1).Item("icfven")
                    saldo = 0

                End If

            Next
            Return dtDetalle
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
            Return dtDetalle
        End Try
    End Function

    Public Sub _GuardarNuevo()
        Try
            Dim numi As String = ""


            Dim dtDetalle As DataTable = grdetalle1.DataSource
            Dim res As Boolean = L_fnGrabarBoleta(tbCodigo.Text, tbFechaVenta.Value.ToString("yyyy/MM/dd"), _CodCliente,
                                                     _CodEmpleado, cbgrupo1.Value, "", dtDetalle, tbControlTotal.Text, cbGrupoEstado.Value)
            If res Then


                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                ToastNotification.Show(Me, "Código de Venta ".ToUpper + tbCodigo.Text + " Grabado con Exito.".ToUpper,
                                                  img, 2000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter
                                                  )

                _prCargarCabecera()
                _prSalir()
            Else
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, "La Venta no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

            End If


        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try


    End Sub
    Public Sub _prImiprimirNotaVenta(numi As String)
        Dim ef = New Efecto


        ef.tipo = 2
        ef.Context = "MENSAJE PRINCIPAL".ToUpper
        ef.Header = "¿desea imprimir la nota de venta?".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            P_GenerarReporte(numi)
        End If
    End Sub

    Private Sub _prGuardarModificado()

        Dim dtDetalle = grdetalle1.DataSource
        Dim res As Boolean = L_fnModificarBoleta(TbNombre2.Text, tbFechaVenta.Value.ToString("yyyy/MM/dd"), _CodCliente,
                                                     _CodInstitucion, cbgrupo1.Value, "", dtDetalle, tbControlTotal.Text, cbGrupoEstado.Value)
        If res Then

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Boleta ".ToUpper + tbCodigo.Text + " Modificado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )
            _prCargarCabecera()
            _prSalir()

        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La Boleta no pudo ser Modificada".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If
    End Sub
    Private Sub _prSalir()
        If btnGrabar.Enabled = True Then
            _prInhabiliitar()
            If grVentas.RowCount > 0 Then
                _prMostrarRegistro(0)

            End If
        Else
            Me.Close()
            '_modulo.Select()
        End If
        Dim _pos As Integer = grVentas.Row
        If grVentas.RowCount > 0 Then
            _pos = grVentas.RowCount - 1
            ''  _prMostrarRegistro(_pos)
            grVentas.Row = _pos
        End If
    End Sub
    Public Sub _prCargarIconELiminar()
        For i As Integer = 0 To CType(grdetalle1.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim Bin As New MemoryStream
            Dim img As New Bitmap(My.Resources.delete, 28, 28)
            img.Save(Bin, Imaging.ImageFormat.Png)
            CType(grdetalle1.DataSource, DataTable).Rows(i).Item("img") = Bin.GetBuffer
            grdetalle1.RootTable.Columns("img").Visible = True
        Next

    End Sub
    Public Sub _PrimerRegistro()
        Dim _MPos As Integer
        If grVentas.RowCount > 0 Then
            _MPos = 0
            ''   _prMostrarRegistro(_MPos)
            grVentas.Row = _MPos
        End If
    End Sub






    Private Function ObtenerFechaLiteral(Fecliteral As String, ciudad As String) As String
        Dim dia, mes, ano As Integer
        Dim mesl As String
        dia = Microsoft.VisualBasic.Left(Fecliteral, 2)
        mes = Microsoft.VisualBasic.Mid(Fecliteral, 4, 2)
        ano = Microsoft.VisualBasic.Mid(Fecliteral, 7, 4)
        If mes = 1 Then
            mesl = "Enero"
        End If
        If mes = 2 Then
            mesl = "Febrero"
        End If
        If mes = 3 Then
            mesl = "Marzo"
        End If
        If mes = 4 Then
            mesl = "Abril"
        End If
        If mes = 5 Then
            mesl = "Mayo"
        End If
        If mes = 6 Then
            mesl = "Junio"
        End If
        If mes = 7 Then
            mesl = "Julio"
        End If
        If mes = 8 Then
            mesl = "Agosto"
        End If
        If mes = 9 Then
            mesl = "Septiembre"
        End If
        If mes = 10 Then
            mesl = "Octubre"
        End If
        If mes = 11 Then
            mesl = "Noviembre"
        End If
        If mes = 12 Then
            mesl = "Diciembre"
        End If
        Fecliteral = ciudad + ", " + dia.ToString + " de " + mesl + " del " + ano.ToString
        Return Fecliteral
    End Function





    Public Function P_fnImageToByteArray(ByVal imageIn As Image) As Byte()
        Dim ms As New System.IO.MemoryStream()
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
        Return ms.ToArray()
    End Function





    Private Sub P_GenerarReporte(numi As String)
        Dim dt As DataTable = L_fnVentaNotaDeVenta(numi)
        If (gb_DetalleProducto) Then
            ponerDescripcionProducto(dt)
        End If
        'Dim total As Decimal = dt.Compute("SUM(Total)", "")
        '  Dim total As Decimal = Convert.ToDecimal(tbTotalBs.Text)
        'Dim totald As Double = (total / 6.96)
        Dim fechaven As String = dt.Rows(0).Item("fechaventa")
        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If
        Dim ParteEntera As Long
        Dim ParteDecimal As Decimal
        Dim ParteDecimal2 As Decimal
        ' ParteEntera = Int(total)
        ' ParteDecimal = total - Math.Truncate(total)
        ' ParteDecimal2 = CDbl(ParteDecimal) * 100


        Dim li As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(ParteEntera)) + " " +
        IIf(ParteDecimal2.ToString.Equals("0"), "00", ParteDecimal2.ToString) + "/100 Bolivianos"

        ' ParteEntera = Int(totald)
        'ParteDecimal = totald - Math.Truncate(totald)
        'ParteDecimal2 = CDbl(ParteDecimal) * 100

        Dim lid As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(ParteEntera)) + " " +
        IIf(ParteDecimal2.ToString.Equals("0"), "00", ParteDecimal2.ToString) + "/100 Dolares"
        Dim _Hora As String = Now.Hour.ToString + ":" + Now.Minute.ToString
        Dim _Ds2 = L_Reporte_Factura_Cia("2")
        Dim dt2 As DataTable = L_fnNameReporte()
        Dim ParEmp1 As String = ""
        Dim ParEmp2 As String = ""
        Dim ParEmp3 As String = ""
        Dim ParEmp4 As String = ""
        If (dt2.Rows.Count > 0) Then
            ParEmp1 = dt2.Rows(0).Item("Empresa1").ToString
            ParEmp2 = dt2.Rows(0).Item("Empresa2").ToString
            ParEmp3 = dt2.Rows(0).Item("Empresa3").ToString
            ParEmp4 = dt2.Rows(0).Item("Empresa4").ToString
        End If

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
        _FechaPar = "Cochabamba, " + _Fecha(0).Trim + " De " + _Meses(_Fecha(1) - 1).Trim + " Del " + _Fecha(2).Trim
        Dim objrep As Object = Nothing
        Dim empresaId = ObtenerEmpresaHabilitada()
        Dim empresaHabilitada As DataTable = ObtenerEmpresaTipoReporte(empresaId, Convert.ToInt32(ENReporte.NOTAVENTA))
        For Each fila As DataRow In empresaHabilitada.Rows
            Select Case fila.Item("TipoReporte").ToString
                Case ENReporteTipo.NOTAVENTA_Carta
                    objrep = New R_NotaVenta_Carta
                 '   SetParametrosNotaVenta(dt, total, li, _Hora, _Ds2, _Ds3, fila.Item("TipoReporte").ToString, objrep)
                Case ENReporteTipo.NOTAVENTA_Ticket
                    objrep = New R_NotaVenta_7_5X100
                    '   SetParametrosNotaVenta(dt, total, li, _Hora, _Ds2, _Ds3, fila.Item("TipoReporte").ToString, objrep)
            End Select
        Next
    End Sub



    Private Sub ponerDescripcionProducto(ByRef dt As DataTable)
        For Each fila As DataRow In dt.Rows
            Dim numi As Integer = fila.Item("codProducto")
            Dim dtDP As DataTable = L_fnDetalleProducto(numi)
            Dim des As String = fila.Item("producto") + vbNewLine + vbNewLine
            For Each fila2 As DataRow In dtDP.Rows
                des = des + fila2.Item("yfadesc").ToString + vbNewLine
            Next
            fila.Item("producto") = des
        Next
    End Sub





    Private Sub AsignarClienteEmpleado()
        Try
            Dim _tabla11 As DataTable = L_fnListarClientesUsuario(gi_userNumi)
            If _tabla11.Rows.Count > 0 Then
                tbCliente.Text = _tabla11.Rows(0).Item("yddesc")
                _CodCliente = _tabla11.Rows(0).Item("ydnumi") 'Codigo
                tbVendedor.Text = _tabla11.Rows(0).Item("vendedor") 'Codigo
                _CodEmpleado = _tabla11.Rows(0).Item("ydnumivend") 'Codigo
            Else
                Dim dt As DataTable
                dt = L_fnListarClientes()
                If dt.Rows.Count > 0 Then
                    Dim fila As DataRow() = dt.Select("ydnumi =MIN(ydnumi)")
                    tbCliente.Text = fila(0).ItemArray(3)
                    _CodCliente = fila(0).ItemArray(0)
                    tbVendedor.Text = fila(0).ItemArray(9)
                    _CodEmpleado = fila(0).ItemArray(8)
                End If
            End If
        Catch ex As Exception
            MostrarMensajeError("Debe asiganar un vendedor al cliente")
        End Try

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


    Private Sub F0_Ventas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _IniciarTodo()
        LabelX12.Text = gs_user
        Dim _pos As Integer = grVentas.Row
        If grVentas.RowCount > 0 Then
            _pos = grVentas.RowCount - 1
            ''  _prMostrarRegistro(_pos)
            grVentas.Row = _pos
        End If
        _prCalcularPrecioTotal()
        Timer2.Enabled = True

    End Sub
    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click

        Dim boletaAnterior As Integer
        boletaAnterior = IIf(tbCodigo.Text = "", 0, tbCodigo.Text)
        _Limpiar()
        limpiarDetalle()
        _prhabilitar()
        '' AsignarClienteEmpleado()
        tbCodigo.Text = boletaAnterior + 1
        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
        btnGrabar.Enabled = True
        PanelNavegacion.Enabled = False
        tbCodigo.Select()
        _Nuevo = True
    End Sub
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _PLimpiarErrores()
        limpiarDetalle()
        _prSalir()

    End Sub
    Private Sub _PLimpiarErrores()
        MEP.Clear()
        tbPesoBruto.BackColor = Color.White
        tbPesoTara.BackColor = Color.White
        tbCodigoTara.BackColor = Color.White
        tbPaquetes.BackColor = Color.White
        cbgrupo1.BackColor = Color.White
        cbgrupo2.BackColor = Color.White
        cbgrupo3.BackColor = Color.White
    End Sub

    Private Sub tbVendedor_KeyDown(sender As Object, e As KeyEventArgs)

        If (_fnAccesible()) Then
            If e.KeyData = Keys.Control + Keys.Enter Then

                Dim dt As DataTable

                dt = L_fnListarEmpleado()
                '              a.ydnumi, a.ydcod, a.yddesc, a.yddctnum, a.yddirec
                ',a.ydtelf1 ,a.ydfnac 

                Dim listEstCeldas As New List(Of Modelo.Celda)
                listEstCeldas.Add(New Modelo.Celda("id,", False, "ID", 50))
                listEstCeldas.Add(New Modelo.Celda("codInst", True, "ID", 50))
                listEstCeldas.Add(New Modelo.Celda("nomInst", True, "NOMBRE", 280))
                listEstCeldas.Add(New Modelo.Celda("campo1", True, "N. Cuenta".ToUpper, 150))
                Dim ef = New Efecto
                ef.tipo = 3
                ef.dt = dt
                ef.SeleclCol = 1
                ef.listEstCeldas = listEstCeldas
                ef.alto = 50
                ef.ancho = 350
                ef.Context = "Seleccione Vendedor".ToUpper
                ef.ShowDialog()
                Dim bandera As Boolean = False
                bandera = ef.band
                If (bandera = True) Then
                    Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                    If (IsNothing(Row)) Then
                        tbVendedor.Focus()
                        Return

                    End If
                    _CodEmpleado = Row.Cells("id").Value
                    tbVendedor.Text = Row.Cells("nomInst").Value
                    grdetalle1.Select()

                End If

            End If

        End If
    End Sub

    Private Sub grdetalle_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grdetalle1.EditingCell
        If (_fnAccesible()) Then
            'Habilitar solo las columnas de Precio, %, Monto y Observación
            'If (e.Column.Index = grdetalle.RootTable.Columns("yfcbarra").Index Or
            If (e.Column.Index = grdetalle1.RootTable.Columns("img").Index) Then
                ''e.Column.Index = grdetalle.RootTable.Columns("tbdesc").Index
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Else
            e.Cancel = True
        End If

    End Sub










    Private Sub _buscarRegistro(cbarra As String)
        Dim _t As DataTable
        _t = L_fnListarProductosC(cbarra)
        If _t.Rows.Count > 0 Then
            CType(grdetalle1.DataSource, DataTable).Rows(0).Item("producto") = _t.Rows(0).Item("yfcdprod1")
            CType(grdetalle1.DataSource, DataTable).Rows(0).Item("tbcmin") = 1
            CType(grdetalle1.DataSource, DataTable).Rows(0).Item("unidad") = _t.Rows(0).Item("uni")

        Else
            MsgBox("Codigo de Producto No Exite")
        End If
    End Sub



    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        _prGuardar()

    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        _PModificarRegistro()
        _PMostrarRegistro(grdetalle1.Row)
        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
        btnGrabar.Enabled = True
    End Sub
    Private Sub _PModificarRegistro()
        _Nuevo = False
        _prhabilitar()
        _codBoleta = tbCodigo.Text

        MSuperTabControl.SelectedTabIndex = 0

        _prCargarIconELiminar()

    End Sub
    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Try

            If (gb_FacturaEmite) Then

            End If



            Dim res2 As Boolean = L_fnVerificarCierreCaja(tbCodigo.Text, "V")
            If res2 Then
                Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                ToastNotification.Show(Me, "No se puede anular la venta con código ".ToUpper + tbCodigo.Text + ", ya se hizo cierre de caja, por favor primero elimine cierre de caja".ToUpper,
                                                  img, 5000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter)
                Exit Sub
            End If


            Dim result As Boolean = L_fnVerificarSiSeContabilizoVenta(tbCodigo.Text)
            If result Then
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, "La Venta no puede ser anulada porque ya fue contabilizada".ToUpper, img, 4500, eToastGlowColor.Red, eToastPosition.TopCenter)
                Exit Sub
            End If
            Dim ef = New Efecto
            ef.tipo = 2
            ef.Context = "¿esta seguro de eliminar el registro?".ToUpper
            ef.Header = "mensaje principal".ToUpper
            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            If (bandera = True) Then
                Dim mensajeError As String = ""
                Dim res As Boolean = L_fnEliminarVenta(tbCodigo.Text, mensajeError, Programa)
                If res Then
                    Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                    ToastNotification.Show(Me, "Código de Venta ".ToUpper + tbCodigo.Text + " eliminado con Exito.".ToUpper,
                                              img, 2000,
                                              eToastGlowColor.Green,
                                              eToastPosition.TopCenter)
                    _prFiltrar()
                Else
                    Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                    ToastNotification.Show(Me, mensajeError, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                End If
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub grVentas_SelectionChanged(sender As Object, e As EventArgs) Handles grVentas.SelectionChanged
        If (grVentas.RowCount >= 0 And grVentas.Row >= 0) Then
            _prMostrarRegistro(grVentas.Row)
        End If
        _prCalcularPrecioTotal()
    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        Dim _pos As Integer = grVentas.Row
        If _pos < grVentas.RowCount - 1 And _pos >= 0 Then
            _pos = grVentas.Row + 1
            '' _prMostrarRegistro(_pos)
            grVentas.Row = _pos
        End If
        _prCalcularPrecioTotal()
    End Sub

    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        Dim _pos As Integer = grVentas.Row
        If grVentas.RowCount > 0 Then
            _pos = grVentas.RowCount - 1
            ''  _prMostrarRegistro(_pos)
            grVentas.Row = _pos
        End If
        _prCalcularPrecioTotal()
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        Dim _MPos As Integer = grVentas.Row
        If _MPos > 0 And grVentas.RowCount > 0 Then
            _MPos = _MPos - 1
            ''  _prMostrarRegistro(_MPos)
            grVentas.Row = _MPos
        End If
        _prCalcularPrecioTotal()
    End Sub

    Private Sub btnPrimero_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click
        _PrimerRegistro()
        _prCalcularPrecioTotal()
    End Sub
    Private Sub grVentas_KeyDown(sender As Object, e As KeyEventArgs) Handles grVentas.KeyDown
        If e.KeyData = Keys.Enter Then
            MSuperTabControl.SelectedTabIndex = 0
            grdetalle1.Focus()

        End If
    End Sub

    Private Sub TbNit_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)
        If btnGrabar.Enabled = True Then
            'Dim nom1, nom2 As String
            'nom1 = ""
            'nom2 = ""
            'If (tbNit.Text.Trim = String.Empty) Then
            '    tbNit.Text = "0"
            'End If
            'L_Validar_Nit(tbNit.Text.Trim, nom1, nom2)
            'If nom1 = "" Then
            '    TbNombre1.Focus()
            'Else
            '    TbNombre1.Text = nom1
            '    TbNombre2.Text = nom2
            'End If
            Dim nom1, nom2 As String
            nom1 = ""
            nom2 = ""
            If (tbNit.Text.Trim <> String.Empty) Then
                L_Validar_Nit(tbNit.Text.Trim, nom1, nom2)
                If nom1 = "" Then
                    TbNombre1.Focus()
                Else
                    TbNombre1.Text = nom1
                    TbNombre2.Text = nom2
                End If
            End If

        End If
    End Sub
    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Try
            If (Not _fnAccesible()) Then

                If (gb_FacturaEmite) Then
                    If tbCodigo.Text = String.Empty Then
                        Throw New Exception("Venta no encontrada")
                    End If
                    If tbNit.Text = String.Empty Then
                        _prImiprimirNotaVenta(tbCodigo.Text)
                        ' Return
                    ElseIf (True) Then

                        Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)


                        Exit Sub
                    End If
                    _prImiprimirNotaVenta(tbCodigo.Text)
                Else
                    _prImiprimirNotaVenta(tbCodigo.Text)
                End If
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Sub TbNit_KeyPress(sender As Object, e As KeyPressEventArgs)
        g_prValidarTextBox(1, e)
    End Sub

    Private Sub swTipoVenta_Leave(sender As Object, e As EventArgs)
        grdetalle1.Select()
    End Sub


    Private Sub tbNit_Leave(sender As Object, e As EventArgs)
        grdetalle1.Select()
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

    Private Sub tbMontoBs_KeyDown(sender As Object, e As KeyEventArgs)
        If (e.KeyData = Keys.Control + Keys.A) Then
            _prGuardar()
        End If
    End Sub

    Private Sub tbMontoDolar_KeyDown(sender As Object, e As KeyEventArgs)
        If (e.KeyData = Keys.Control + Keys.A) Then
            _prGuardar()
        End If
    End Sub
    Private Sub tbMontoTarej_KeyDown(sender As Object, e As KeyEventArgs)
        If (e.KeyData = Keys.Control + Keys.A) Then
            _prGuardar()
        End If
    End Sub

    Private Sub txtMontoPagado1_KeyDown(sender As Object, e As KeyEventArgs)
        If (e.KeyData = Keys.Control + Keys.A) Then
            _prGuardar()
        End If
    End Sub

    Private Sub txtCambio1_KeyDown(sender As Object, e As KeyEventArgs)
        If (e.KeyData = Keys.Control + Keys.A) Then
            _prGuardar()
        End If
    End Sub

    Private Sub GroupPanel1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub grdetalle_Enter(sender As Object, e As EventArgs) Handles grdetalle1.Enter
        Try
            If (_fnAccesible()) Then
                If (_CodCliente <= 0) Then
                    ToastNotification.Show(Me, "           Antes de Continuar Por favor Seleccione un Cliente!!             ", My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)
                    tbCliente.Focus()

                    Return
                End If
                If (_CodEmpleado <= 0) Then


                    ToastNotification.Show(Me, "           Antes de Continuar Por favor Seleccione un Vendedor!!             ", My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)
                    tbVendedor.Focus()
                    Return

                End If

                grdetalle1.Select()
                'If _codeBar = 1 Then
                '    If gb_CodigoBarra Then
                '        grdetalle.Col = 5
                '        grdetalle.Row = 0
                '    Else
                '        grdetalle.Col = 3
                '        grdetalle.Row = 0
                '    End If
                'End If
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
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

    Private Sub tbCliente_TextChanged_1(sender As Object, e As EventArgs) Handles tbCliente.TextChanged
        If btnNuevo.Enabled = False Then

        End If
    End Sub
    Dim posicion As Integer
    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click

        If P_ValidarAgregar() Then
            L_Validar_CodigoTara(tbCodigoTara.Text.Trim, placa, pesoTara, propietario)
            If placa = "" And propietario = "" Then
                tbPesoTara.Value = 0.00
                TaraNuevo()
            Else

                L_Taras_ModificarBoleta(tbCodigoTara.Text, tbPesoTara.Value)
                L_Validar_CodigoTara(tbCodigoTara.Text.Trim, placa, pesoTara, propietario)
                tbPesoTara.Value = pesoTara

            End If
        Else
            Exit Sub
        End If

        Dim _Error As Boolean = False

        If P_ValidarAgregar() Then

            If tbNumeroLinea.Text = "" Then

                _prAddDetalleVenta()
                Dim pos As Integer = -1
                grdetalle1.Row = grdetalle1.RowCount - 1
                _fnObtenerFilaDetalle(pos, grdetalle1.GetValue("numi"))
                'Dim existe As Boolean = _fnExisteNumPaquete(tbPaquetes.Value)
                If (pos >= 0) Then
                    CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("nroPaq") = tbPaquetes.Value
                    CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("codTara") = tbCodigoTara.Text
                    CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("ycdes3") = cbgrupo2.Text
                    CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("corte") = cbgrupo3.Text
                    CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("tipCan") = cbgrupo2.Value
                    CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("tipCor") = cbgrupo3.Value
                    CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("pesBru") = tbPesoBruto.Value
                    CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("pesTara") = tbPesoTara.Value
                    CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("placa") = placa
                    CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("fleter") = propietario
                    CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("pesNeto") = tbPesoBruto.Value - tbPesoTara.Value
                    CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("estado") = 1
                    '_prAddDetalleVenta()
                    limpiarDetalle()
                    'End If
                Else



                End If
            Else
                Dim pos As Integer = -1
                Dim f, c As Integer
                pos = grdetalle1.Row

                CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("nroPaq") = tbPaquetes.Value
                CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("codTara") = tbCodigoTara.Text
                CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("ycdes3") = cbgrupo2.Text
                CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("corte") = cbgrupo3.Text
                CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("tipCan") = cbgrupo2.Value
                CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("tipCor") = cbgrupo3.Value
                CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("pesBru") = tbPesoBruto.Value
                CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("pesTara") = tbPesoTara.Value
                CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("placa") = placa
                CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("fleter") = propietario
                CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("pesNeto") = tbPesoBruto.Value - tbPesoTara.Value
                CType(grdetalle1.DataSource, DataTable).Rows(pos).Item("estado") = 1
                '_prAddDetalleVenta()
                limpiarDetalle()
                'End If
            End If

            _prCalcularPrecioTotal()
        End If

    End Sub
    Public Sub _prCalcularPrecioTotal()


        Dim TotalDescuento As Double = 0
        Dim TotalDescuento1 As Double = 0
        Dim TotalCosto As Double = 0
        Dim dt As DataTable = CType(grdetalle1.DataSource, DataTable)
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            If gs_user = "SERVICIOS" Then
                If (dt.Rows(i).Item("estado") >= 0) Then
                    TotalDescuento = TotalDescuento + Format(Format((dt.Rows(i).Item("tbptot") * 1), "0.00000"))
                    TotalDescuento1 = TotalDescuento1 + Format(dt.Rows(i).Item("tbptot"), "0.00000")
                    TotalCosto = TotalCosto + dt.Rows(i).Item("tbptot2")
                End If
            Else
                If (dt.Rows(i).Item("estado") >= 0) Then

                    TotalCosto = TotalCosto + dt.Rows(i).Item("pesNeto")
                End If
            End If


        Next
        tbTotalDo.Text = TotalCosto
    End Sub
    Public Function _fnExisteNumPaquete(idprod As Integer) As Boolean
        For i As Integer = 0 To CType(grdetalle1.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _idprod As Integer = CType(grdetalle1.DataSource, DataTable).Rows(i).Item("nroPaq")
            Dim estado As Integer = CType(grdetalle1.DataSource, DataTable).Rows(i).Item("estado")
            If (_idprod = idprod And estado >= 0) Then

                Return True
            End If
        Next
        Return False
    End Function
    Private Sub limpiarDetalle()
        'MEP.Clear()
        tbPesoBruto.Value = 0
        tbPesoTara.Value = 0
        tbCodigoTara.Text = String.Empty
        tbPaquetes.Text = String.Empty
        tbNumeroLinea.Text = String.Empty
    End Sub
    Public Function P_ValidarAgregar() As Boolean
        Dim _Error As Boolean = True
        MEP.Clear()

        If cbgrupo1.SelectedIndex = -1 Then
            cbgrupo1.BackColor = Color.Red
            MEP.SetError(cbgrupo1, "Seleccione uno por favor!".ToUpper)
            _Error = False
        Else
            cbgrupo1.BackColor = Color.White
            MEP.SetError(cbgrupo1, String.Empty)
        End If

        If cbgrupo2.SelectedIndex = -1 Then
            cbgrupo2.BackColor = Color.Red
            MEP.SetError(cbgrupo2, "Seleccione uno por favor!".ToUpper)
            _Error = False
        Else
            cbgrupo2.BackColor = Color.White
            MEP.SetError(cbgrupo2, String.Empty)
        End If
        If cbgrupo3.SelectedIndex = -1 Then
            cbgrupo3.BackColor = Color.Red
            MEP.SetError(cbgrupo3, "Seleccione uno por favor!".ToUpper)
            _Error = False
        Else
            cbgrupo3.BackColor = Color.White
            MEP.SetError(cbgrupo3, String.Empty)
        End If

        If tbPaquetes.Text.Trim = String.Empty Then
            tbPaquetes.BackColor = Color.Red
            MEP.SetError(tbPaquetes, "Ingrese numero de paquete!".ToUpper)
            _Error = False
        Else
            If tbPaquetes.Text <= 0 Then
                tbPaquetes.BackColor = Color.Red
                MEP.SetError(tbPaquetes, "Ingrese número valido para paquete!".ToUpper)
                _Error = False
            Else
                tbPaquetes.BackColor = Color.White
                MEP.SetError(tbPaquetes, String.Empty)
            End If

        End If

        If tbCodigoTara.Text.Trim = String.Empty Then
            tbCodigoTara.BackColor = Color.Red
            MEP.SetError(tbCodigoTara, "Ingrese código de Tara!".ToUpper)
            _Error = False
        Else
            tbCodigoTara.BackColor = Color.White
            MEP.SetError(tbCodigoTara, String.Empty)
        End If

        If tbPesoTara.Text.Trim = String.Empty Then
            tbPesoTara.BackColor = Color.Red
            MEP.SetError(tbPesoTara, "Ingrese peso de Tara!".ToUpper)
            _Error = False
        Else
            tbPesoTara.BackColor = Color.White
            MEP.SetError(tbPesoTara, String.Empty)
        End If
        If tbPesoBruto.Text.Trim = String.Empty Then
            tbPesoBruto.BackColor = Color.Red
            MEP.SetError(tbPesoBruto, "Ingrese peso bruto!".ToUpper)
            _Error = False

        ElseIf tbPesoBruto.Value > tbPesoTara.Value Then
            tbPesoBruto.BackColor = Color.White
            MEP.SetError(tbPesoBruto, String.Empty)

        Else
            tbPesoBruto.BackColor = Color.Red
            MEP.SetError(tbPesoBruto, "El peso bruto debe ser mayor al peso tara!".ToUpper)
            _Error = False
        End If

        MHighlighterFocus.UpdateHighlights()
        Return _Error
    End Function

    Private Sub tbCodigoTara_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles tbCodigoTara.Validating

        placa = ""
        pesoTara = 0.00
        propietario = ""
        If (tbCodigoTara.Text <> String.Empty) Then
            L_Validar_CodigoTara(tbCodigoTara.Text.Trim, placa, pesoTara, propietario)
            If placa = "" And pesoTara = 0.00 And propietario = "" Then
                tbPesoTara.Value = 0.00
                TaraNuevo()

                'ElseIf pesoTara = 0.00 Then
                '    TaraNuevo()
            Else
                tbPesoTara.Value = pesoTara
            End If
        End If
    End Sub

    Private Sub tbCliente_KeyDown_1(sender As Object, e As KeyEventArgs) Handles tbCliente.KeyDown
        If (_fnAccesible()) Then

            If e.KeyData = Keys.Control + Keys.Enter Then

                Dim dt As DataTable
                'dt = L_fnListarClientes()
                dt = L_fnListarClientesVenta()

                Dim listEstCeldas As New List(Of Modelo.Celda)
                listEstCeldas.Add(New Modelo.Celda("ydnumi,", True, "ID", 50))
                listEstCeldas.Add(New Modelo.Celda("ydcod", True, "COD. CLI", 100))
                listEstCeldas.Add(New Modelo.Celda("ydrazonsocial", False, "RAZÓN SOCIAL", 180))
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
                ef.Context = "Seleccione Cliente".ToUpper
                ef.ShowDialog()
                Dim bandera As Boolean = False
                bandera = ef.band
                If (bandera = True) Then
                    Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row

                    _CodCliente = Row.Cells("ydnumi").Value
                    tbCliente.Text = Row.Cells("ydrazonsocial").Value
                    _dias = Row.Cells("yddias").Value

                    TbNombre1.Text = Row.Cells("ydcod").Value
                    Dim dt1 As DataTable
                    dt1 = L_fnListarCaneroInstitucion(_CodCliente)
                    Dim row1 As DataRow = dt1.Rows(dt1.Rows.Count - 1)
                    tbVendedor.Text = row1("institucion")
                    tbNit.Text = row1("codInst")

                    Dim numiVendedor As Integer = IIf(IsDBNull(Row.Cells("ydnumivend").Value), 0, Row.Cells("ydnumivend").Value)
                    If (numiVendedor > 0) Then
                        ''tbVendedor.Text = Row.Cells("vendedor").Value
                        _CodEmpleado = Row.Cells("ydnumivend").Value

                        grdetalle1.Select()
                    Else
                        tbVendedor.Clear()
                        _CodEmpleado = 0
                        tbVendedor.Focus()

                    End If
                End If
            End If
        End If
    End Sub



    Private Sub grdetalle_MouseClick(sender As Object, e As MouseEventArgs) Handles grdetalle1.MouseClick
        Try
            If (_fnAccesible()) Then
                If (_CodCliente <= 0) Then
                    ToastNotification.Show(Me, "           Antes de Continuar Por favor Seleccione un Cliente!!             ", My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)
                    tbCliente.Focus()

                    Return
                Else
                    If (grdetalle1.RowCount >= 2) Then
                        If (grdetalle1.CurrentColumn.Index = grdetalle1.RootTable.Columns("img").Index) Then
                            _prEliminarFila()
                            'CalculoDescuentoXProveedor()
                        Else
                            Dim dt As DataTable = CType(grdetalle1.DataSource, DataTable)


                            If (dt.Rows(0).Item("nroPaq") > 0) Then
                                _PMostrarRegistro(grdetalle1.Row)
                                posicion = grdetalle1.Row
                            End If
                            If grdetalle1.Row >= 0 Then
                            End If
                        End If
                    Else
                        If (grdetalle1.CurrentColumn.Index <> grdetalle1.RootTable.Columns("img").Index) Then
                            Dim dt As DataTable = CType(grdetalle1.DataSource, DataTable)


                            If (dt.Rows(0).Item("nroPaq") > 0) Then
                                _PMostrarRegistro(grdetalle1.Row)
                                posicion = grdetalle1.Row
                            End If
                            If grdetalle1.Row >= 0 Then
                            End If
                        End If

                    End If
                End If
                If (_CodEmpleado <= 0) Then


                    ToastNotification.Show(Me, "           Antes de Continuar Por favor Seleccione un Vendedor!!             ", My.Resources.WARNING, 4000, eToastGlowColor.Red, eToastPosition.TopCenter)
                    tbVendedor.Focus()
                    Return

                End If

                grdetalle1.Select()
                'If _codeBar = 1 Then
                '    If gb_CodigoBarra Then
                '        grdetalle.Col = 5
                '        grdetalle.Row = 0
                '    Else
                '        grdetalle.Col = 3
                '        grdetalle.Row = 0
                '    End If
                'End If
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try

    End Sub
    Private Sub _PMostrarRegistro(_N As Integer)
        Dim dt As DataTable = CType(grdetalle1.DataSource, DataTable)
        If (IsNothing(CType(grdetalle1.DataSource, DataTable))) Then
            Return
        End If

        With grdetalle1
            tbCodigoTara.Text = .GetValue("codTara").ToString
            tbPaquetes.Text = .GetValue("nroPaq").ToString
            tbPesoTara.Value = .GetValue("pesTara")
            tbPesoBruto.Value = .GetValue("pesBru")
            cbgrupo2.Value = .GetValue("tipCan")
            cbgrupo3.Value = .GetValue("tipCor")
            tbNumeroLinea.Text = .GetValue("numLinea")
            'NumiCuenta = .GetValue("canumi").ToString

        End With
    End Sub


    Private Sub TbNombre1_KeyDown(sender As Object, e As KeyEventArgs) Handles TbNombre1.KeyDown
        If (e.KeyData = Keys.Enter) Then
            If TbNombre1.Text = String.Empty Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "DEBE INGRESAR UN CODIGO DE CAÑERO PARA REALIZAR LA BUSQUEDA".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Else
                Dim dt As DataTable
                'dt = L_fnListarClientes()
                dt = L_fnListarClientesVentas1(TbNombre1.Text)

                Dim listEstCeldas As New List(Of Modelo.Celda)
                listEstCeldas.Add(New Modelo.Celda("ydnumi,", True, "ID", 50))
                listEstCeldas.Add(New Modelo.Celda("ydcod", True, "COD. CLI", 100))
                listEstCeldas.Add(New Modelo.Celda("ydrazonsocial", False, "RAZÓN SOCIAL", 180))
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
                ef.Context = "Seleccione Cliente".ToUpper
                ef.ShowDialog()
                Dim bandera As Boolean = False
                bandera = ef.band
                If (bandera = True) Then
                    Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row

                    _CodCliente = Row.Cells("ydnumi").Value
                    tbCliente.Text = Row.Cells("ydrazonsocial").Value
                    _dias = Row.Cells("yddias").Value

                    TbNombre1.Text = Row.Cells("ydcod").Value
                    Dim dt1 As DataTable
                    dt1 = L_fnListarCaneroInstitucion(_CodCliente)
                    Dim row1 As DataRow = dt1.Rows(dt1.Rows.Count - 1)
                    tbVendedor.Text = row1("institucion")
                    tbNit.Text = row1("codInst")
                    cbgrupo1.Focus()
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
            End If

        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        lblhora1.Text = DateTime.Now.ToString("hh:mm:ss")
        If lblhora1.Text = "06:11:00" Then
            MessageBox.Show("hora de cambiar fecha")
            Dim ef = New Efecto
            ef.tipo = 2
            ef.Context = "Verificacion de registro de boletas".ToUpper
            ef.Header = "Debe cambiar de usuario, ya son las 6:00 desea cambiar la fecha".ToUpper
            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            If (bandera = True) Then
                Dim mensajeError As String = ""
                Dim res As Boolean = True 'L_fnEliminarVenta(tbCodigo.Text, mensajeError, Programa)
                If res Then
                    Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                    ToastNotification.Show(Me, "Código de Venta ".ToUpper + tbCodigo.Text + " eliminado con Exito.".ToUpper,
                                              img, 2000,
                                              eToastGlowColor.Green,
                                              eToastPosition.TopCenter)
                    '_prFiltrar()
                Else
                    Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                    ToastNotification.Show(Me, mensajeError, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                End If
            End If
        End If
        'lblFecha.Text = DateTime.Now.ToLongDateString()
    End Sub



    Private Sub TaraNuevo()
        Dim frm As New F_ClienteNuevo
        Dim dt As DataTable
        'frm.tbNit.Text = tbNit.Text
        frm.ShowDialog()


        If (frm.Cliente = True) Then ''Aqui Consulto si se inserto un nuevo Cliente cargo sus datos del nuevo cliente insertado

            dt = L_fnObtenerTara(frm.Tb_CodTara.Text)
            If (dt.Rows.Count > 0) Then
                tbCodigoTara.Text = dt.Rows(0).Item("cod")
                tbPesoTara.Value = dt.Rows(0).Item("pesoTara")
                placa = dt.Rows(0).Item("placa")
                propietario = dt.Rows(0).Item("propietario")
            End If
        End If
    End Sub
End Class