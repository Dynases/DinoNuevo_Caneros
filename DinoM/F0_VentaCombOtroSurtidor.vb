Imports System.Drawing.Printing
Imports System.IO
Imports CrystalDecisions.Shared
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports Facturacion
Imports Janus.Windows.GridEX
Imports Logica.AccesoLogica
Imports UTILITIES
Public Class F0_VentaCombOtroSurtidor
    Dim _Inter As Integer = 0
#Region "Variables Globales"
    Dim _CodCliente As Integer = 0
    Dim _CodCaneroUcg As Integer = 0
    Dim _CodEmpleado As Integer = 0
    Dim _CodInstitucion As Integer = 0
    Dim OcultarFact As Integer = 0
    Dim _codeBar As Integer = 1
    Dim _dias As Integer = 0
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Dim FilaSelectLote As DataRow = Nothing
    Dim Table_Producto As DataTable
    Dim G_Lote As Boolean = False '1=igual a mostrar las columnas de lote y fecha de Vencimiento
    Dim _Nuevo As Boolean = False
    Dim codCont As String = "" 'CODIGO DE ASIENTO CONTABLE
    Dim dtDescuentos As DataTable = Nothing
    Dim ConfiguracionDescuentoEsXCantidad As Boolean = True
    Public Programa As String
    Dim DescuentoXProveedorList As DataTable = New DataTable

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        _Inter = _Inter + 1
        If _Inter = 1 Then
            Me.WindowState = FormWindowState.Normal
        Else
            Me.Opacity = 100
            Timer1.Enabled = False
        End If
    End Sub

#End Region
#Region "Metodos Privados"
    Private Sub _IniciarTodo()
        L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        MSuperTabControl.SelectedTabIndex = 0
        'Me.WindowState = FormWindowState.Maximized
        cbSucursal.Value = 4
        _prValidarLote()
        _prCargarComboLibreriaSucursal(cbSucursal)
        _prCargarComboLibreria(cbTipoSolicitud, 10, 1)
        _prCargarComboLibreria(cbSurtidor, 1, 10)
        _prCargarComboLibreria(cbDespachador, 1, 9)

        'lbTipoMoneda.Visible = False

        P_prCargarVariablesIndispensables()
        _prCargarVenta()
        _prInhabiliitar()
        grVentas.Focus()
        Me.Text = "VENTAS COMBUSTIBLE"
        Dim blah As New Bitmap(New Bitmap(My.Resources.compra), 20, 20)
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        _prAsignarPermisos()

        _prValidadFactura()
        _prCargarNameLabel()
        'COnfiguracion previa para Pantalla de facturacion o Nota de venta
        'If gb_FacturaEmite Then
        '    btnModificar.Visible = False
        'Else
        '    tbObservacion.Visible = True
        '    lblObservacion.Visible = True
        'End If
        DescuentoXProveedorList = ObtenerDescuentoPorProveedor()
        ConfiguracionDescuentoEsXCantidad = TipoDescuentoEsXCantidad()
        SwDescuentoProveedor.Visible = IIf(ConfiguracionDescuentoEsXCantidad, False, True)
        SwDescuentoProveedor.Visible = False
        Programa = P_Principal.btVentVenta.Text
    End Sub

    Public Sub _prCargarNameLabel()
        Dim dt As DataTable = L_fnNameLabel()
        If (dt.Rows.Count > 0) Then
            _codeBar = 1 'dt.Rows(0).Item("codeBar")
        End If
    End Sub
    Sub _prValidadFactura()
        'If (OcultarFact = 1) Then
        '    GroupPanelFactura2.Visible = False
        '    GroupPanelFactura.Visible = False
        'Else
        '    GroupPanelFactura2.Visible = True
        '    GroupPanelFactura.Visible = True
        'End If

    End Sub
    Public Sub _prValidarLote()
        Dim dt As DataTable = L_fnPorcUtilidad()
        If (dt.Rows.Count > 0) Then
            Dim lot As Integer = dt.Rows(0).Item("VerLote")
            OcultarFact = dt.Rows(0).Item("VerFactManual")
            If (lot = 1) Then
                G_Lote = True
            Else
                G_Lote = False
            End If

        End If
    End Sub
    Private Sub _prCargarComboLibreriaSucursal(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnListarSucursales()
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("aanumi").Width = 60
            .DropDownList.Columns("aanumi").Caption = "COD"
            .DropDownList.Columns.Add("aabdes").Width = 500
            .DropDownList.Columns("aabdes").Caption = "SUCURSAL"
            .ValueMember = "aanumi"
            .DisplayMember = "aabdes"
            .DataSource = dt
            .Refresh()
        End With
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

        DoubleInput1.IsInputReadOnly = True
        DoubleInput2.IsInputReadOnly = True

        tbCodigo.ReadOnly = True
        tbCliente.ReadOnly = True
        tbVendedor.ReadOnly = True
        tbFechaVenta.IsInputReadOnly = True
        tbFechaVenta.Enabled = False

        tbFechaVenc.IsInputReadOnly = True
        swTipoVenta.IsReadOnly = True
        SwSurtidor.IsReadOnly = True
        txtEstado.ReadOnly = True

        tbTramOrden.ReadOnly = True
        tbNitTraOrden.ReadOnly = True
        tbRetSurtidor.ReadOnly = True
        tbNitRetSurtidor.ReadOnly = True

        tbPlaca.ReadOnly = True
        tbObservacion.ReadOnly = True

        cbDespachador.ReadOnly = True
        cbTipoSolicitud.ReadOnly = True
        cbSurtidor.ReadOnly = True

        tbAutoriza.ReadOnly = True




        'btnModificar.Enabled = True
        btnGrabar.Enabled = False
        btnNuevo.Enabled = True
        'btnEliminar.Enabled = True

        If grVentas.GetValue("tcestado") = 1 Then
            btnEliminar.Enabled = True
            btnModificar.Enabled = True
        Else
            btnEliminar.Enabled = False
            btnModificar.Enabled = False
        End If


        tbPrueba.IsInputReadOnly = True
        tbMontoBs.IsInputReadOnly = True
        tbMontoDolar.IsInputReadOnly = True
        tbMontoTarej.IsInputReadOnly = True
        'txtCambio1.IsInputReadOnly = True

        'txtMontoPagado1.IsInputReadOnly = True

        grVentas.Enabled = True
        PanelNavegacion.Enabled = True
        'grdetalle.RootTable.Columns("img").Visible = False
        'If (GPanelProductos.Visible = True) Then 'REVISAR DESPUES IMPORTANTE
        '    _DesHabilitarProductos()
        'End If

        tbNit.ReadOnly = True
        TbNombre1.ReadOnly = True
        TbNombre2.ReadOnly = True
        cbSucursal.ReadOnly = True
        FilaSelectLote = Nothing

        _Nuevo = False
    End Sub
    Private Sub _prhabilitar()

        DoubleInput1.IsInputReadOnly = False
        DoubleInput2.IsInputReadOnly = False
        grVentas.Enabled = False
        tbCodigo.ReadOnly = False
        ''  tbCliente.ReadOnly = False  por que solo podra seleccionar Cliente
        ''  tbVendedor.ReadOnly = False
        tbFechaVenc.IsInputReadOnly = False
        SwSurtidor.Value = False
        swTipoVenta.IsReadOnly = False
        If _Nuevo = True Then
            swTipoVenta.Value = 0
        End If

        tbFechaVenta.IsInputReadOnly = False
        tbFechaVenta.Enabled = True

        SwSurtidor.IsReadOnly = True
        btnGrabar.Enabled = True

        tbNit.ReadOnly = False
        TbNombre1.ReadOnly = False
        TbNombre2.ReadOnly = False



        tbMontoBs.IsInputReadOnly = False
        tbMontoDolar.IsInputReadOnly = False
        tbMontoTarej.IsInputReadOnly = False
        'tbMdesc.IsInputReadOnly = False

        tbTramOrden.ReadOnly = True
        tbNitTraOrden.ReadOnly = False
        tbRetSurtidor.ReadOnly = False
        tbNitRetSurtidor.ReadOnly = False


        tbPlaca.ReadOnly = False
        tbObservacion.ReadOnly = False

        cbDespachador.ReadOnly = False
        cbTipoSolicitud.ReadOnly = False
        cbSurtidor.ReadOnly = False
        tbAutoriza.ReadOnly = False

        'txtCambio1.IsInputReadOnly = False
        'txtMontoPagado1.IsInputReadOnly = False

        If (tbCodigo.Text.Length > 0) Then
            cbSucursal.ReadOnly = True
        Else
            cbSucursal.ReadOnly = False

        End If


        dtDescuentos = L_fnListarDescuentosTodos()
    End Sub






    Public Sub _prFiltrar()
        'cargo el buscador
        Dim _Mpos As Integer
        _prCargarVenta()
        If grVentas.RowCount > 0 Then
            _Mpos = 0
            grVentas.Row = _Mpos
        Else
            _Limpiar()
            LblPaginacion.Text = "0/0"
        End If
    End Sub
    Private Sub _Limpiar()

        tbCodigo.Clear()
        tbCliente.Clear()
        tbVendedor.Clear()

        DoubleInput1.Value = 0
        'DoubleInput2.Value = 0
        tbObservacion.Text = ""
        _CodCliente = 0
        _CodEmpleado = 0
        tbFechaVenta.Value = Now.Date
        swTipoVenta.Value = True
        tbFechaVenc.Visible = False
        lbCredito.Visible = False
        _prCargarDetalleVenta(-1)
        MSuperTabControl.SelectedTabIndex = 0

        tbPrueba.Value = 0
        tbMontoBs.Value = 0
        tbMontoDolar.Value = 0
        tbMontoTarej.Value = 0
        'txtCambio1.Value = 0
        'txtMontoPagado1.Value = 0
        txtCambio1.Text = "0.00"
        txtMontoPagado1.Text = "0.00"

        tbTotalDo.Text = "0.00"

        txtEstado.BackColor = Color.White
        txtEstado.Clear()


        tbTramOrden.Text = ""
        tbNitTraOrden.Text = ""
        tbPlaca.Text = ""
        tbRetSurtidor.Text = ""
        tbNitRetSurtidor.Text = ""

        tbAutoriza.Text = 0




        tbCliente.Focus()

        tbNit.Clear()
        TbNombre1.Clear()
        TbNombre2.Clear()
        tbNit.Select()

        If (CType(cbSucursal.DataSource, DataTable).Rows.Count > 0) Then
            cbSucursal.SelectedIndex = 0
        End If
        FilaSelectLote = Nothing

        ' tbCliente.Focus()
        Table_Producto = Nothing
    End Sub
    Public Sub _prMostrarRegistro(_N As Integer)
        '' grVentas.Row = _N
        '     a.tanumi ,a.taalm ,a.tafdoc ,a.taven ,vendedor .yddesc as vendedor ,a.tatven ,a.tafvcr ,a.taclpr,
        'cliente.yddesc as cliente ,a.tamon ,IIF(tamon=1,'Boliviano','Dolar') as moneda,a.taest ,a.taobs ,
        'a.tadesc ,a.tafact ,a.tahact ,a.tauact,(Sum(b.tbptot)-a.tadesc ) as total,taproforma

        With grVentas


            tbCodigo.Text = .GetValue("tcvnumi")
            tbFechaVenta.Value = .GetValue("tcfecha")

            _CodInstitucion = .GetValue("id")
            tbVendedor.Text = .GetValue("institucion")
            _CodCliente = .GetValue("ydnumi")
            tbCliente.Text = .GetValue("cliente")

            _CodCaneroUcg = .GetValue("ydcod")

            swTipoVenta.Value = .GetValue("tctipoven")
            'SwConta.Value = IIf(.GetValue("taproforma") = 0, 1, 0)
            If .GetValue("tctiposurtidor") = True Then
                _prCargarComboLibreria(cbSurtidor, 1, 10)
            Else
                _prCargarComboLibreria(cbSurtidor, 1, 8)
            End If
            SwSurtidor.Value = IIf(.GetValue("tctiposurtidor") = True, 1, 0)

            'tbTramOrden.Text = IIf(Row.Cells("yddctnum").Value = "", Row.Cells("ydnit").Value, (Row.Cells("yddctnum").Value))
            tbTramOrden.Text = .GetValue("tcentregado")
            tbNitTraOrden.Text = .GetValue("tcentregadoci")
            cbDespachador.Value = .GetValue("tcdespachador")
            tbPlaca.Text = .GetValue("tcplaca")
            tbRetSurtidor.Text = .GetValue("tcretiro")
            tbNitRetSurtidor.Text = .GetValue("tcnitretiro")
            TbNombre1.Text = .GetValue("tcfacnombre")
            tbNit.Text = .GetValue("tcfacnit")
            cbTipoSolicitud.Value = .GetValue("tcsolicitud")
            cbSurtidor.Value = .GetValue("tcsurtidor")
            tbAutoriza.Text = .GetValue("tcnroautorizacion")
            DoubleInput1.Value = .GetValue("tccantidad")
            DoubleInput2.Value = .GetValue("tcprecio")
            tbTotalDo.Text = .GetValue("tctotal")
            tbObservacion.Text = .GetValue("tcObservacion")
            If grVentas.GetValue("tcestado") = 1 Then
                txtEstado.Text = "VIGENTE"
                txtEstado.BackColor = Color.Green
                btnEliminar.Enabled = True
                btnModificar.Enabled = True
            Else
                txtEstado.Text = "ANULADO"
                txtEstado.BackColor = Color.Red
                btnEliminar.Enabled = False
                btnModificar.Enabled = False
            End If

            'End If

            lbFecha.Text = CType(.GetValue("tcfact"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("thact").ToString
            lbUsuario.Text = .GetValue("tauact").ToString
            codCont = .GetValue("idContabiliza").ToString
        End With

        LblPaginacion.Text = Str(grVentas.Row + 1) + "/" + grVentas.RowCount.ToString

    End Sub

    Private Sub _prCargarDetalleVenta(_numi As String)
        Dim dt As New DataTable
        dt = L_fnDetalleVenta(_numi)


    End Sub

    Private Sub _prCargarVenta()
        Dim dt As New DataTable
        dt = L_fnGeneralVentaCombustibleOtroSurtidor()
        grVentas.DataSource = dt
        grVentas.RetrieveStructure()
        grVentas.AlternatingColors = True
        '   a.tamon ,IIF(tamon=1,'Boliviano','Dolar') as moneda,a.taest ,a.taobs ,
        'a.tadesc ,a.tafact ,a.tahact ,a.tauact,(Sum(b.tbptot)-a.tadesc ) as total

        With grVentas.RootTable.Columns("tcvnumi")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = True

        End With

        With grVentas.RootTable.Columns("tcfecha")
            .Width = 90
            .Visible = True
            .Caption = "FECHA"
        End With

        With grVentas.RootTable.Columns("tctipoven")
            .Width = 160
            .Visible = False
        End With
        With grVentas.RootTable.Columns("ydnumi")
            .Width = 160
            .Visible = False
        End With
        With grVentas.RootTable.Columns("ydcod")
            .Width = 90
            .Visible = True
            .Caption = "CODCAÑERO".ToUpper
        End With
        With grVentas.RootTable.Columns("cliente")
            .Width = 250
            .Visible = True
            .Caption = "CAÑERO".ToUpper
        End With
        With grVentas.RootTable.Columns("id")
            .Width = 160
            .Visible = False
        End With
        With grVentas.RootTable.Columns("codInst")
            .Width = 90
            .Visible = True
            .Caption = "CODINSTITUCION".ToUpper
        End With
        With grVentas.RootTable.Columns("institucion")
            .Width = 250
            .Visible = True
            .Caption = "INSTITUCION".ToUpper
        End With
        With grVentas.RootTable.Columns("tcestado")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tcfact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("thact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tauact")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tcentregado")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With

        With grVentas.RootTable.Columns("tcentregadoci")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tcdespachador")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tcplaca")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tcretiro")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tcnitretiro")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tcfacnombre")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tcfacnit")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tcsolicitud")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tcsurtidor")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tctiposurtidor")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tcnroautorizacion")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tccantidad")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tcprecio")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tctotal")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("tcObservacion")
            .Width = 50
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = False
        End With
        With grVentas.RootTable.Columns("idContabiliza")
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

        'txtMontoPagado1.Text = tbTotalBs.Text
        If _ValidarCampos() = False Then
            Exit Sub
        End If

        If (tbCodigo.Text = String.Empty) Then
            Dim dt1 As DataTable
            Dim cuenta As String
            dt1 = ObtenerNumCuenta("Institucion", _CodEmpleado)
            cuenta = dt1.Rows(0).Item(7)
            If cuenta = "0" Then
                MessageBox.Show("ESTA INSTITUCION NO CUENTA CON NUMERO DE CUENTA PARA GENERAR ASIENTO CONTABLE CONTACTARSE CON EL ADM. SISTEMAS")
            Else
                Dim resultado = New DialogResult()
                Dim mensaje = New mensajePersonalizado()
                mensaje.Label4.Text = IIf(swTipoVenta.Value = True, "CONTADO", "CREDITO")
                mensaje.Label3.Text = tbFechaVenta.Text
                mensaje.Label6.Text = cbSurtidor.Text
                resultado = mensaje.ShowDialog()
                If resultado = DialogResult.OK Then
                    _GuardarNuevo()
                Else

                End If
            End If


        Else
            If (tbCodigo.Text <> String.Empty) Then
                If tbFechaVenta.Text = fechaOriginal Then
                    Dim dt11 As DataTable
                    Dim cuenta1 As String
                    dt11 = ObtenerNumCuenta("Institucion", _CodEmpleado)
                    cuenta1 = dt11.Rows(0).Item(7)
                    If cuenta1 = "0" Then
                        MessageBox.Show("ESTA INSTITUCION NO CUENTA CON NUMERO DE CUENTA PARA GENERAR ASIENTO CONTABLE CONTACTARSE CON EL ADM. SISTEMAS")
                    Else
                        Dim resultado = New DialogResult()
                        Dim mensaje = New mensajePersonalizado()
                        mensaje.Label4.Text = IIf(swTipoVenta.Value = True, "CONTADO", "CREDITO")
                        mensaje.Label3.Text = tbFechaVenta.Text
                        mensaje.Label6.Text = cbSurtidor.Text
                        resultado = mensaje.ShowDialog()
                        If resultado = DialogResult.OK Then
                            _prGuardarModificado()
                        Else

                        End If
                    End If
                Else
                    MessageBox.Show("PARA MODIFICAR FECHA CONTACTE CON EL DESARROLLADOR POR FAVOR!")
                End If



            End If
        End If
    End Sub







    Public Function _fnSiguienteNumi()
        'Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
        'Dim rows() As DataRow = dt.Select("tbnumi=MAX(tbnumi)")
        'If (rows.Count > 0) Then
        '    Return rows(rows.Count - 1).Item("tbnumi")
        'End If
        Return 1
    End Function
    Public Function _fnAccesible()
        Return tbFechaVenta.IsInputReadOnly = False
    End Function












    Public Sub _prCalcularPrecioTotal()


        'Dim TotalDescuento As Double = 0
        'Dim TotalCosto As Double = 0
        'Dim dt As DataTable = CType(grdetalle.DataSource, DataTable)
        'For i As Integer = 0 To dt.Rows.Count - 1 Step 1

        '    If (dt.Rows(i).Item("estado") >= 0) Then
        '        TotalDescuento = TotalDescuento + dt.Rows(i).Item("tbptot")
        '        TotalCosto = TotalCosto + dt.Rows(i).Item("tbptot2")
        '    End If
        'Next


        ''grdetalle.UpdateData()
        'Dim montoDo As Decimal
        'Dim montodesc As Double = tbMdesc.Value
        'Dim pordesc As Double = ((montodesc * 100) / TotalDescuento)
        'tbPdesc.Value = pordesc
        'Dim subtotal = Convert.ToDouble(Format(TotalDescuento, "0.00000"))
        'tbSubTotal.Value = subtotal

        ''tbTotalBs.Text = total.ToString()
        'tbTotalBs.Text = tbSubTotal.Value - montodesc
        'montoDo = Convert.ToDecimal(tbTotalBs.Text) * IIf(cbCambioDolar.Text = "", 1, Convert.ToDecimal(cbCambioDolar.Text))
        'tbTotalDo.Text = Format(montoDo, "0.00")
        'tbIce.Value = TotalCosto * (gi_ICE / 100)
        'calcularCambio()

    End Sub


    Public Function _ValidarCampos() As Boolean
        Try

            If (_CodCliente <= 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor Seleccione un Cliente con Ctrl+Enter".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                tbCliente.Focus()
                Return False

            End If
            If (_CodEmpleado <= 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor Seleccione un Vendedor con Ctrl+Enter".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                tbVendedor.Focus()
                Return False
            End If
            If (cbSucursal.SelectedIndex < 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor Seleccione una Sucursal".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                cbSucursal.Focus()
                Return False
            End If


            If (tbRetSurtidor.Text = String.Empty) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor llene el campo retiro surtidor.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                tbRetSurtidor.Focus()
                Return False
            End If

            If (tbNitRetSurtidor.Text = String.Empty) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor ponga el nit del retiro surtidor.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                tbNitRetSurtidor.Focus()
                Return False
            End If

            'Validar datos de factura

            If (SwSurtidor.Value = True) Then
                If (tbNit.Text = String.Empty) Then
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "Por Favor ponga el nit del cliente.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    tbNit.Focus()
                    Return False
                End If

                If (TbNombre1.Text = String.Empty) Then
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "Por Favor ponga la razon social del cliente.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    TbNombre1.Focus()
                    Return False
                End If

                If (tbTramOrden.Text = String.Empty) Then
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "Por Favor ponga el tramite de orden.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    tbTramOrden.Focus()
                    Return False
                End If

                If (tbNitTraOrden.Text = String.Empty) Then
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "Por Favor ponga el nit del tramite de orden.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    tbNitTraOrden.Focus()
                    Return False
                End If

                If (tbPlaca.Text = String.Empty) Then
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "Por Favor ponga el número de placa.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    tbPlaca.Focus()
                    Return False
                End If

            End If
            If (DoubleInput1.Value <= 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "La cantidad debe ser mayor a 0".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                DoubleInput1.Focus()
                Return False

            End If
            If (DoubleInput2.Value <= 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "El precio debe ser mayor a 0".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                DoubleInput2.Focus()
                Return False

            End If
            'If swTipoVenta.Value = True Then
            '    If (Convert.ToDecimal(txtMontoPagado1.Text) = 0) Then
            '        Throw New Exception("El monto Pagado debe ser mayor 0")
            '        Return False
            '    End If
            '    If (Convert.ToDecimal(txtMontoPagado1.Text) < Convert.ToDecimal(tbTotalDo.Text)) Then
            '        Throw New Exception("El monto Pagado debe ser mayor al monto Total")
            '        Return False
            '    End If
            'End If



            'Validación para controlar caducidad de Dosificacion
            If tbNit.Text <> String.Empty Then
                Dim fecha As String = Now.Date
                Dim dtDosificacion As DataSet = L_DosificacionCajas("1", "1", fecha, gs_NroCaja)
                If dtDosificacion.Tables(0).Rows.Count = 0 Then
                    'dtDosificacion.Tables.Cast(Of DataTable)().Any(Function(x) x.DefaultView.Count = 0)
                    Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                    ToastNotification.Show(Me, "La Dosificación para las facturas ya caducó, ingrese nueva dosificación".ToUpper, img, 3500, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
            Return False
        End Try

    End Function

    Public Sub _GuardarNuevo()

        Try
            Dim numi As String = ""
            ''Verifica si existe estock para los productos
            If SwSurtidor.Value = True Then

            Else

                Dim res As Boolean = L_fnGrabarVentaCombustibleOtroS(numi, tbTramOrden.Text, tbNitTraOrden.Text, cbDespachador.Value, tbPlaca.Text, tbRetSurtidor.Text, tbNitRetSurtidor.Text, TbNombre1.Text,
                                                                     tbNit.Text, cbTipoSolicitud.Value, cbSurtidor.Value, SwSurtidor.Value, tbAutoriza.Text,
                                                                     DoubleInput1.Value, DoubleInput2.Value, tbTotalDo.Text, IIf(swTipoVenta.Value = True, 1, 0),
                                                                     _CodCliente, tbFechaVenta.Value.ToString("yyyy/MM/dd"), tbObservacion.Text)

                If res Then

                    contabilizarPrestamo(numi)
                    _prImiprimirNotaVenta(numi)
                    Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                    ToastNotification.Show(Me, "Código de Venta ".ToUpper + tbCodigo.Text + " Grabado con Exito.".ToUpper,
                                              img, 2000,
                                              eToastGlowColor.Green,
                                              eToastPosition.TopCenter)

                    _Limpiar()
                    Table_Producto = Nothing
                    _prSalir()
                    _prCargarVenta()
                    Dim _pos As Integer = grVentas.Row
                    If grVentas.RowCount > 0 Then
                        _pos = grVentas.RowCount - 1
                        ''  _prMostrarRegistro(_pos)
                        grVentas.Row = _pos
                    End If
                End If
            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Public Sub _prImiprimirNotaVenta(numi As String)
        Dim ef = New Efecto


        ef.tipo = 2
        ef.Context = "MENSAJE PRINCIPAL".ToUpper
        ef.Header = "¿desea imprimir la nota de retiro?".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            P_GenerarReporte(numi)
        End If
    End Sub
    Public Sub _prImiprimirFacturaPreimpresa(numi As String)
        Dim ef = New Efecto


        ef.tipo = 2
        ef.Context = "MENSAJE PRINCIPAL".ToUpper
        ef.Header = "¿desea imprimir la factura Preimpresa?".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            P_GenerarReporteFactura(numi)
        End If
    End Sub
    Private Sub _prGuardarModificado()
        Dim res As Boolean = L_fnEditarVentaCombustibleOtroS(tbCodigo.Text, tbTramOrden.Text, tbNitTraOrden.Text, cbDespachador.Value, tbPlaca.Text, tbRetSurtidor.Text, tbNitRetSurtidor.Text, TbNombre1.Text,
                                                                    tbNit.Text, cbTipoSolicitud.Value, cbSurtidor.Value, SwSurtidor.Value, tbAutoriza.Text,
                                                                    DoubleInput1.Value, DoubleInput2.Value, tbTotalDo.Text, IIf(swTipoVenta.Value = True, 1, 0),
                                                                    _CodCliente, tbFechaVenta.Value.ToString("yyyy/MM/dd"), tbObservacion.Text)

        If res Then

            L_Asiento_Borrar(codCont)
            contabilizarPrestamoDetalle()
            _prImiprimirNotaVenta(tbCodigo.Text)
            _prSalir()
            _prCargarVenta()
            Dim _pos As Integer = grVentas.Row
            If grVentas.RowCount > 0 Then
                _pos = grVentas.RowCount - 1
                ''  _prMostrarRegistro(_pos)
                grVentas.Row = _pos
            End If
            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "Código de Prestamo Diesel ".ToUpper + tbCodigo.Text + " Modificado con Exito.".ToUpper,
            img, 2000,
            eToastGlowColor.Green,
                                       eToastPosition.TopCenter
                                         )


        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "El Prestamo Diesel no pudo ser Modificada".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

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
    End Sub
    Private Sub grVentas_SelectionChanged(sender As Object, e As EventArgs) Handles grVentas.SelectionChanged
        If (grVentas.RowCount >= 0 And grVentas.Row >= 0) Then
            _prMostrarRegistro(grVentas.Row)
        End If
    End Sub
    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        Dim _pos As Integer = grVentas.Row
        If _pos < grVentas.RowCount - 1 And _pos >= 0 Then
            _pos = grVentas.Row + 1
            '' _prMostrarRegistro(_pos)
            grVentas.Row = _pos
        End If
    End Sub

    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        Dim _pos As Integer = grVentas.Row
        If grVentas.RowCount > 0 Then
            _pos = grVentas.RowCount - 1
            ''  _prMostrarRegistro(_pos)
            grVentas.Row = _pos
        End If
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        Dim _MPos As Integer = grVentas.Row
        If _MPos > 0 And grVentas.RowCount > 0 Then
            _MPos = _MPos - 1
            ''  _prMostrarRegistro(_MPos)
            grVentas.Row = _MPos
        End If
    End Sub

    Private Sub btnPrimero_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click
        _PrimerRegistro()
    End Sub

    Public Sub _PrimerRegistro()
        Dim _MPos As Integer
        If grVentas.RowCount > 0 Then
            _MPos = 0
            ''_prMostrarRegistro(_MPos)
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



    Private Sub P_prCargarVariablesIndispensables()
        If (gb_FacturaEmite) Then
            gi_IVA = CDbl(IIf(L_fnGetIVA().Rows(0).Item("scdebfis").ToString.Equals(""), gi_IVA, L_fnGetIVA().Rows(0).Item("scdebfis").ToString))
            gi_ICE = CDbl(IIf(L_fnGetICE().Rows(0).Item("scice").ToString.Equals(""), gi_ICE, L_fnGetICE().Rows(0).Item("scice").ToString))
        End If

    End Sub


    Private Sub P_GenerarReporte(numi As String)
        Dim dt As DataTable = L_fnVentaNotaDeVentaOtros(numi)
        If (gb_DetalleProducto) Then
            ponerDescripcionProducto(dt)
        End If
        'Dim total As Decimal = dt.Compute("SUM(Total)", "")
        Dim total As Decimal = Convert.ToDecimal(tbTotalDo.Text)
        'Dim totald As Double = (total * 6.96)
        Dim fechaven As String = dt.Rows(0).Item("fechaventa")
        Dim retiro As String = dt.Rows(0).Item("RETIRO")
        ' Dim fechaImpresion As String = Today.ToLongDateString
        If Not IsNothing(P_Global.Visualizador) Then
            P_Global.Visualizador.Close()
        End If
        Dim ParteEntera As Long
        Dim ParteDecimal As Decimal
        Dim ParteDecimal2 As Decimal
        ' ParteEntera = Int(total)
        ' ParteDecimal = total - Math.Truncate(total)
        ParteDecimal2 = CDbl(ParteDecimal) * 100


        Dim li As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(ParteEntera)) + " " +
        IIf(ParteDecimal2.ToString.Equals("0"), "00", ParteDecimal2.ToString) + "/100 Bolivianos"

        ' ParteEntera = Int(totald)
        ' ParteDecimal = totald - Math.Truncate(totald)
        ParteDecimal2 = CDbl(ParteDecimal) * 100

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
                    objrep = New R_NotaVenta_Carta_Dieselotro
                    SetParametrosNotaVenta(dt, total, li, _Hora, _Ds2, _Ds3, fila.Item("TipoReporte").ToString, objrep, retiro)
                Case ENReporteTipo.NOTAVENTA_Ticket
                    objrep = New R_NotaVenta_7_5X100
                    SetParametrosNotaVenta(dt, total, li, _Hora, _Ds2, _Ds3, fila.Item("TipoReporte").ToString, objrep, "")
            End Select
        Next
    End Sub

    Private Sub SetParametrosNotaVenta(dt As DataTable, total As Decimal, li As String, _Hora As String, _Ds2 As DataSet, _Ds3 As DataSet, tipoReporte As String, objrep As Object, retiro As String)

        Select Case tipoReporte
            Case ENReporteTipo.NOTAVENTA_Carta
                objrep.SetDataSource(dt)
                objrep.SetParameterValue("Literal", li)
                objrep.SetParameterValue("TipoVenta", IIf(swTipoVenta.Value = True, "CONTADO", "CRÉDITO"))
                objrep.SetParameterValue("Logo", gb_UbiLogo)
                objrep.SetParameterValue("NotaAdicional1", gb_NotaAdicional)
                'objrep.SetParameterValue("Descuento", tbMdesc.Value)
                objrep.SetParameterValue("fechaImpresion", Today.Date)
                objrep.SetParameterValue("Total", total)
                objrep.SetParameterValue("retiro", retiro)
            Case ENReporteTipo.NOTAVENTA_Ticket
                objrep.SetDataSource(dt)
                objrep.SetParameterValue("ECasaMatriz", _Ds2.Tables(0).Rows(0).Item("scsuc").ToString)
                objrep.SetParameterValue("ECiudadPais", _Ds2.Tables(0).Rows(0).Item("scpai").ToString)
                objrep.SetParameterValue("EDuenho", _Ds2.Tables(0).Rows(0).Item("scnom").ToString) '?
                objrep.SetParameterValue("Direccionpr", _Ds2.Tables(0).Rows(0).Item("scdir").ToString)
                objrep.SetParameterValue("Hora", _Hora)
                objrep.SetParameterValue("ENombre", _Ds2.Tables(0).Rows(0).Item("scneg").ToString) '?
                objrep.SetParameterValue("Literal1", li)
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
                    If doctoprint.PrinterSettings.PaperSizes(c).PaperName = "orden retiro" Then
                        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(c).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(c)))
                        Exit For
                    End If
                Next
                objrep.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)

                objrep.PrintToPrinter(1, True, 0, 0)
            End If
        End If
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

    Private Sub P_GenerarReporteFactura(numi As String)
        Dim dt As DataTable = L_fnVentaFactura(numi)
        Dim total As Double = dt.Compute("SUM(Total)", "")

        Dim ParteEntera As Long
        Dim ParteDecimal As Double
        ParteEntera = Int(total)
        ParteDecimal = total - ParteEntera
        Dim li As String = Facturacion.ConvertirLiteral.A_fnConvertirLiteral(CDbl(ParteEntera)) + " con " +
        IIf(ParteDecimal.ToString.Equals("0"), "00", ParteDecimal.ToString) + "/100 Bolivianos"


        Dim objrep As New R_FacturaFarmacia
        '' GenerarNro(_dt)
        ''objrep.SetDataSource(Dt1Kardex)
        'imprimir
        If PrintDialog1.ShowDialog = DialogResult.OK Then
            objrep.SetDataSource(dt)
            objrep.SetParameterValue("TotalEscrito", li)
            objrep.SetParameterValue("nit", tbNit.Text)
            objrep.SetParameterValue("Total", total)
            objrep.SetParameterValue("cliente", TbNombre1.Text + " " + TbNombre2.Text)
            objrep.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName

            objrep.PrintToPrinter(1, False, 1, 1)
            objrep.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
        End If
        'objrep.SetDataSource(dt)
        'objrep.SetParameterValue("TotalEscrito", li)
        'objrep.SetParameterValue("nit", TbNit.Text)
        'objrep.SetParameterValue("Total", total)
        'P_Global.Visualizador.CrGeneral.ReportSource = objrep 'Comentar
        'P_Global.Visualizador.Show() 'Comentar
        'P_Global.Visualizador.BringToFront() 'Comentar



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

    Private Sub F0_VentaComb_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _IniciarTodo()
        Dim _pos As Integer = grVentas.Row
        If grVentas.RowCount > 0 Then
            _pos = grVentas.RowCount - 1
            ''  _prMostrarRegistro(_pos)
            grVentas.Row = _pos
        End If
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        _Nuevo = True
        _Limpiar()
        _prhabilitar()
        'AsignarClienteEmpleado()

        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
        btnGrabar.Enabled = True
        PanelNavegacion.Enabled = False
        tbCliente.Select()
        cbSucursal.Value = 4

        cbTipoSolicitud.Value = 1
        cbDespachador.Value = 1
        cbSurtidor.SelectedIndex = 0
    End Sub




    Private Function ObtenerSumaTotalXProveedor(detalleLista As List(Of DataRow), proveedorID As Integer) As Decimal
        Return (From proc In detalleLista
                Where proc.ItemArray(ENDetalleVenta.estado) >= 0 _
                    And proc.ItemArray(ENDetalleVenta.proveedorId) = proveedorID
                Select Convert.ToDecimal(proc.ItemArray(ENDetalleVenta.totalDescuento))).Sum()
    End Function

    Private Function ObtenerPorcentajeDescuento(listaDescuento As List(Of DataRow), total As Decimal, esDescuentoEspecial As Boolean, proveedorID As Integer) As Object

        If esDescuentoEspecial Then
            'Obtiene el porcentaje de descuento especial
            Return (From desc In listaDescuento
                    Where desc.ItemArray(ENDescuentoXProveedor.Estado) = 2 _
                     And desc.ItemArray(ENDescuentoXProveedor.MontoInicial) <= total _
                     And desc.ItemArray(ENDescuentoXProveedor.MontoFinal) >= total
                    Select desc.ItemArray(ENDescuentoXProveedor.DescuentoPorcentaje)).ToArray()
        Else
            'Obtiene el porcentaje de descuento por proveedor


            Return (From desc In listaDescuento
                    Where desc.ItemArray(ENDescuentoXProveedor.proveedorId) = proveedorID _
                     And desc.ItemArray(ENDescuentoXProveedor.MontoInicial) <= total _
                     And desc.ItemArray(ENDescuentoXProveedor.MontoFinal) >= total
                    Select desc.ItemArray(ENDescuentoXProveedor.DescuentoPorcentaje)).ToArray()
        End If
    End Function


    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click

        _prGuardar()

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

                    _CodCliente = Row.Cells("ydnumi").Value
                    _CodCaneroUcg = Row.Cells("ydcod").Value
                    tbCliente.Text = Row.Cells("ydrazonsocial").Value
                    _dias = Row.Cells("yddias").Value
                    tbNit.Text = Row.Cells("ydnit").Value
                    TbNombre1.Text = Row.Cells("ydnomfac").Value
                    tbTramOrden.Text = IIf(Row.Cells("yddctnum").Value = "", Row.Cells("ydnit").Value, (Row.Cells("yddctnum").Value))
                    'tbNitFacturarA.Text = Row.Cells("ydnit").Value
                    'tbFact.Text = Row.Cells("ydnomfac").Value
                    Dim numiVendedor As Integer = IIf(IsDBNull(Row.Cells("ydnumivend").Value), 0, Row.Cells("ydnumivend").Value)
                    If (numiVendedor > 0) Then
                        ''tbVendedor.Text = Row.Cells("vendedor").Value
                        _CodEmpleado = Row.Cells("ydnumivend").Value


                        Table_Producto = Nothing
                    Else
                        tbVendedor.Clear()
                        _CodEmpleado = 0
                        tbRetSurtidor.Focus()
                        Table_Producto = Nothing

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
            _CodInstitucion = row("id")
            tbRetSurtidor.Select()
        End If
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Try
            'tbNit.Text = tbNitFacturarA.Text
            If (Not _fnAccesible()) Then


                If tbCodigo.Text = String.Empty Then
                    Throw New Exception("Venta no encontrada")
                Else
                    _prImiprimirNotaVenta(tbCodigo.Text)
                End If


                ' Return


                'ReimprimirFactura(tbCodigo.Text, True, True)
                '_prImiprimirNotaVenta(tbCodigo.Text)

            End If
        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub
    Public Function contabilizar() As Integer
        Dim codigoVenta = tbCodigo.Text
        Dim codCanero = "P/Ord." + codigoVenta + " " + Convert.ToString(_CodCliente) + " " + tbCliente.Text 'obobs
        'Dim total = tbTotalBs.Text 'para obtener debe haber
        Dim dt, dt1, dtDetalle As DataTable
        Dim cuenta As String
        Dim debebs, haberbs, debeus, haberus As Double
        dt1 = ObtenerNumCuenta("Institucion", _CodInstitucion) 'obcuenta=ncuenta



        Dim resTO001 = L_fnGrabarTO001(1, Convert.ToInt32(codigoVenta)) 'numi cabecera to001
        'Dim resTO0011 As Boolean = L_fnGrabarTO001(Convert.ToInt32(codigoVenta))

        For a As Integer = 1 To 2 Step 1
            dt = CargarConfiguracion("configuracion", a) 'oblin=orden
            'Dim grdetalle1 As GridEX
            dtDetalle = L_fnDetalleVenta1(codigoVenta)

            'Dim dt As New DataTable
            'dt = L_fnDetalleVenta(_numi)
            'grdetalle.DataSource = dt

            'dtDetalle = CType(grdetalle1.DataSource, DataTable)
            'dtDetalle = dt
            Dim oblin As Integer = 1
            Dim totalCosto As Double = 0.00
            For Each row In dt.Rows
                '    Select Case row("cuenta")

                If row("cuenta") = "-1" Then
                    For Each detalle In dtDetalle.Rows
                        cuenta = detalle("yfclot")
                        If row("dh") = 1 Then
                            debeus = (Convert.ToDouble(detalle("tbpcos")) * Convert.ToDouble(row("porcentaje"))) / 100
                            debebs = debeus * 6.96
                            haberus = 0.00
                            haberbs = 0.00
                            totalCosto = totalCosto + Convert.ToDouble(detalle("tbpcos"))
                        Else
                            haberus = (Convert.ToDouble(detalle("tbpcos")) * Convert.ToDouble(row("porcentaje"))) / 100
                            haberbs = haberus * 6.96
                            debeus = 0.00
                            debebs = 0.00
                            totalCosto = totalCosto + Convert.ToDouble(detalle("tbpcos"))
                        End If

                        Dim resTO00112 As Boolean = L_fnGrabarTO001(2, Convert.ToInt32(codigoVenta), resTO001, oblin, cuenta, codCanero, debebs, haberbs, debeus, haberus)
                        oblin = oblin + 1
                    Next


                    If row("cuenta") = "-1" Then
                        Continue For
                    End If

                End If
                If row("cuenta") = "-2" Then
                    cuenta = dt1.Rows(0).Item(5)

                Else
                    cuenta = row("cuenta")
                End If
                If row("dh") = 1 Then
                    'debeus = (IIf(row("tipo") = 1, Convert.ToDouble(total), totalCosto) * Convert.ToDouble(row("porcentaje"))) / 100
                    debebs = debeus * 6.96
                    haberus = 0.00
                    haberbs = 0.00
                Else
                    ' haberus = (IIf(row("tipo") = 1, Convert.ToDouble(total), totalCosto) * Convert.ToDouble(row("porcentaje"))) / 100
                    haberbs = haberus * 6.96
                    debeus = 0.00
                    debebs = 0.00
                End If
                Dim resTO0011 As Boolean = L_fnGrabarTO001(2, Convert.ToInt32(codigoVenta), resTO001, oblin, cuenta, codCanero, debebs, haberbs, debeus, haberus)
                oblin = oblin + 1
            Next
        Next

        L_Actualiza_Venta_Contabiliza(codigoVenta, resTO001)
        Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
        ToastNotification.Show(Me, " Venta ".ToUpper + tbCodigo.Text + " Contabilizada con Exito.".ToUpper,
                                              img, 2000,
                                              eToastGlowColor.Green,
                                              eToastPosition.TopCenter
                                              )
        '_prCargarVenta()
    End Function
    Private Sub btnContabilizar_Click(sender As Object, e As EventArgs)
        Dim codigoVenta = tbCodigo.Text
        Dim codCanero = "P/Ord." + codigoVenta + " " + Convert.ToString(_CodCliente) + " " + tbCliente.Text 'obobs
        'Dim total = tbTotalBs.Text 'para obtener debe haber
        Dim dt, dt1, dtDetalle As DataTable
        Dim cuenta As String
        Dim debebs, haberbs, debeus, haberus As Double
        dt1 = ObtenerNumCuenta("Institucion", _CodInstitucion) 'obcuenta=ncuenta



        Dim resTO001 = L_fnGrabarTO001(1, Convert.ToInt32(codigoVenta)) 'numi cabecera to001
        'Dim resTO0011 As Boolean = L_fnGrabarTO001(Convert.ToInt32(codigoVenta))

        For a As Integer = 1 To 2 Step 1
            dt = CargarConfiguracion("configuracion", a) 'oblin=orden
            'Dim grdetalle1 As GridEX
            dtDetalle = L_fnDetalleVenta1(codigoVenta)

            'Dim dt As New DataTable
            'dt = L_fnDetalleVenta(_numi)
            'grdetalle.DataSource = dt

            'dtDetalle = CType(grdetalle1.DataSource, DataTable)
            'dtDetalle = dt
            Dim oblin As Integer = 1
            Dim totalCosto As Double = 0.00
            For Each row In dt.Rows
                '    Select Case row("cuenta")

                If row("cuenta") = "-1" Then
                    For Each detalle In dtDetalle.Rows
                        cuenta = detalle("yfclot")
                        If row("dh") = 1 Then
                            debeus = (Convert.ToDouble(detalle("tbpcos")) * Convert.ToDouble(row("porcentaje"))) / 100
                            debebs = debeus * 6.96
                            haberus = 0.00
                            haberbs = 0.00
                            totalCosto = totalCosto + Convert.ToDouble(detalle("tbpcos"))
                        Else
                            haberus = (Convert.ToDouble(detalle("tbpcos")) * Convert.ToDouble(row("porcentaje"))) / 100
                            haberbs = haberus * 6.96
                            debeus = 0.00
                            debebs = 0.00
                            totalCosto = totalCosto + Convert.ToDouble(detalle("tbpcos"))
                        End If

                        Dim resTO00112 As Boolean = L_fnGrabarTO001(2, Convert.ToInt32(codigoVenta), resTO001, oblin, cuenta, codCanero, debebs, haberbs, debeus, haberus)
                        oblin = oblin + 1
                    Next


                    If row("cuenta") = "-1" Then
                        Continue For
                    End If

                End If
                If row("cuenta") = "-2" Then
                    cuenta = dt1.Rows(0).Item(5)

                Else
                    cuenta = row("cuenta")
                End If
                If row("dh") = 1 Then
                    '  debeus = (IIf(row("tipo") = 1, Convert.ToDouble(total), totalCosto) * Convert.ToDouble(row("porcentaje"))) / 100
                    debebs = debeus * 6.96
                    haberus = 0.00
                    haberbs = 0.00
                Else
                    '  haberus = (IIf(row("tipo") = 1, Convert.ToDouble(total), totalCosto) * Convert.ToDouble(row("porcentaje"))) / 100
                    haberbs = haberus * 6.96
                    debeus = 0.00
                    debebs = 0.00
                End If
                Dim resTO0011 As Boolean = L_fnGrabarTO001(2, Convert.ToInt32(codigoVenta), resTO001, oblin, cuenta, codCanero, debebs, haberbs, debeus, haberus)
                oblin = oblin + 1
            Next
        Next

        L_Actualiza_Venta_Contabiliza(codigoVenta, resTO001)
        Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
        ToastNotification.Show(Me, " Venta ".ToUpper + tbCodigo.Text + " Contabilizada con Exito.".ToUpper,
                                              img, 2000,
                                              eToastGlowColor.Green,
                                              eToastPosition.TopCenter
                                              )
        _prCargarVenta()
    End Sub

    Private Sub SwConta_ValueChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _prSalir()
    End Sub

    Private Sub SwSurtidor_ValueChanged(sender As Object, e As EventArgs) Handles SwSurtidor.ValueChanged
        If (SwSurtidor.Value = True) Then
            swTipoVenta.Value = False
            swTipoVenta.IsReadOnly = False
            cbSurtidor.Clear()
            _prCargarComboLibreria(cbSurtidor, 1, 10)
            cbSurtidor.Value = 1
            tbTramOrden.Visible = True
            tbNitTraOrden.Visible = True
            TbNombre1.Visible = True
            tbNit.Visible = True
            cbDespachador.Visible = True
            LabelX25.Visible = True
            LabelX27.Visible = True
            LabelX35.Visible = True
            LabelX36.Visible = True
            LabelX31.Visible = True
            If _Nuevo = True Then
                cbSucursal.Value = 3
            End If

        Else
            swTipoVenta.Value = False
            swTipoVenta.IsReadOnly = True
            cbSurtidor.Clear()
            _prCargarComboLibreria(cbSurtidor, 1, 8)
            cbSurtidor.Value = 0
            'tbTramOrden.Visible = False
            tbNitTraOrden.Visible = False
            TbNombre1.Visible = False
            tbNit.Visible = False
            tbTramOrden.Clear()
            tbNitTraOrden.Clear()
            TbNombre1.Clear()
            tbNit.Clear()
            cbDespachador.Visible = False
            'LabelX25.Visible = False
            LabelX27.Visible = False
            LabelX35.Visible = False
            LabelX36.Visible = False
            LabelX31.Visible = False
            If _Nuevo = True Then
                cbSucursal.Value = 4
            End If

        End If


    End Sub


    Private Sub grdetalle_FormattingRow(sender As Object, e As RowLoadEventArgs)

    End Sub

    Private Sub swTipoVenta_ValueChanged(sender As Object, e As EventArgs) Handles swTipoVenta.ValueChanged
        If (swTipoVenta.Value = False) Then
            lbCredito.Visible = False
            tbFechaVenc.Visible = False
            tbFechaVenc.Value = DateAdd(DateInterval.Day, _dias, Now.Date)
        Else
            lbCredito.Visible = False
            tbFechaVenc.Visible = False
        End If
    End Sub

    Private Sub contabilizarPrestamo(cod As Integer)
        Dim dt, dt1, dt2 As DataTable
        Dim codigoVenta = cod
        'dt1 = L_BuscarCodCanero(1)

        Dim codCanero As String = "P/Ord:. " + codigoVenta.ToString + " -Diesel " + Convert.ToString(DoubleInput1.Text) + " Lts. -" + _CodCaneroUcg.ToString + "-" + tbCliente.Text.Trim   'obobs
        Dim total = 6.96 / tbTotalDo.Text 'para obtener debe haber

        Dim cuenta As String
        Dim debebs, haberbs, debeus, haberus As Double
        dt1 = ObtenerNumCuenta("Institucion", _CodInstitucion) 'obcuenta=ncuenta obtener cuenta de institucion
        dt2 = ObtenerNumCuentaSurtidor("TY0031", cbSurtidor.Value) ' ObtenerNumCuentaSurtidor()

        Dim resTO001 = L_fnGrabarTO001prestamos(11, Convert.ToInt32(codigoVenta), "false") 'numi cabecera to001

        For a As Integer = 6 To 6 Step 1
            dt = CargarConfiguracion("configuracion", a) 'oblin=orden


            Dim oblin As Integer = 1
            'Dim totalCosto As Double = 0.00
            For Each row In dt.Rows

                If row("cuenta") = "-2" Then

                    If swTipoVenta.Value = True Then
                        cuenta = 208
                    Else
                        If _CodCliente = 691 Then
                            cuenta = 312
                        Else
                            cuenta = dt1.Rows(0).Item(7)
                        End If
                    End If


                Else
                    cuenta = dt2.Rows(0).Item(3) ' dtDetalle.Rows(0).Item(10) 'row("cuenta")
                End If
                If row("dh") = 1 Then
                    debeus = Math.Round((Convert.ToDouble(tbTotalDo.Text) / 6.96) * Convert.ToDouble(row("porcentaje")) / 100, 2)
                    debebs = Math.Round(debeus * 6.96, 2)
                    haberus = 0.00
                    haberbs = 0.00
                Else

                    haberus = Math.Round((Convert.ToDouble(tbTotalDo.Text) / 6.96) * Convert.ToDouble(row("porcentaje")) / 100, 2)
                    haberbs = Math.Round(haberus * 6.96, 2)
                    debeus = 0.00
                    debebs = 0.00
                End If
                Dim resTO0011 As Boolean = L_fnGrabarTO001(2, Convert.ToInt32(codigoVenta), resTO001, oblin, cuenta, codCanero, debebs, haberbs, debeus, haberus)
                oblin = oblin + 1
            Next
        Next
        Dim resp = L_fnObtenerDiferenciaAsientoContable(resTO001)
        L_Actualiza_otroSurtidor_Contabiliza(codigoVenta, resTO001)
        Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
        ToastNotification.Show(Me, " Venta ".ToUpper + tbCodigo.Text + " Contabilizada con Exito.".ToUpper,
                                              img, 2000,
                                              eToastGlowColor.Green,
                                              eToastPosition.TopCenter
                                              )
    End Sub

    Private Sub contabilizarPrestamoDetalle()
        Dim dt, dt1, dt2 As DataTable
        Dim codigoVenta = tbCodigo.Text
        'dt1 = L_BuscarCodCanero(1)

        Dim codCanero As String = "P/Ord:. " + tbCodigo.Text + " -Diesel " + Convert.ToString(DoubleInput1.Text) + " Lts. -" + _CodCaneroUcg.ToString + "-" + tbCliente.Text.Trim   'obobs
        Dim total = 6.96 / tbTotalDo.Text 'para obtener debe haber

        Dim cuenta As String
        Dim debebs, haberbs, debeus, haberus As Double
        dt1 = ObtenerNumCuenta("Institucion", _CodInstitucion) 'obcuenta=ncuenta obtener cuenta de institucion
        dt2 = ObtenerNumCuentaSurtidor("TY0031", cbSurtidor.Value) ' ObtenerNumCuentaSurtidor()


        ' Dim resTO001 = L_fnGrabarTO001prestamos(11, Convert.ToInt32(tbCodigo.Text), "false") 'numi cabecera to001
        'Dim resTO0011 As Boolean = L_fnGrabarTO001(Convert.ToInt32(codigoVenta))

        For a As Integer = 6 To 6 Step 1
            dt = CargarConfiguracion("configuracion", a) 'oblin=orden

            'dtDetalle = L_fnDetalleVenta1(codigoVenta)


            Dim oblin As Integer = 1
            'Dim totalCosto As Double = 0.00
            For Each row In dt.Rows

                If row("cuenta") = "-2" Then
                    If _CodCliente = 691 Then
                        cuenta = 312
                    Else
                        cuenta = dt1.Rows(0).Item(7)
                    End If
                Else
                    cuenta = dt2.Rows(0).Item(3) ' dtDetalle.Rows(0).Item(10) 'row("cuenta")
                End If
                If row("dh") = 1 Then
                    debeus = Math.Round((Convert.ToDouble(tbTotalDo.Text) / 6.96) * Convert.ToDouble(row("porcentaje")) / 100, 2)
                    debebs = Math.Round(debeus * 6.96, 2)
                    haberus = 0.00
                    haberbs = 0.00
                Else

                    haberus = Math.Round((Convert.ToDouble(tbTotalDo.Text) / 6.96) * Convert.ToDouble(row("porcentaje")) / 100, 2)
                    haberbs = Math.Round(haberus * 6.96, 2)
                    debeus = 0.00
                    debebs = 0.00
                End If
                Dim resTO0011 As Boolean = L_fnGrabarTO001(2, Convert.ToInt32(codigoVenta), codCont, oblin, cuenta, codCanero, debebs, haberbs, debeus, haberus)
                oblin = oblin + 1
            Next
        Next
        Dim resp = L_fnObtenerDiferenciaAsientoContable(codCont)
        'L_Actualiza_Venta_Contabiliza(codigoVenta, resTO001)
    End Sub

    Private Sub DoubleInput1_ValueChanged(sender As Object, e As EventArgs) Handles DoubleInput1.ValueChanged
        tbTotalDo.Text = Convert.ToString(DoubleInput1.Value * DoubleInput2.Value)
    End Sub

    Private Sub DoubleInput2_ValueChanged(sender As Object, e As EventArgs) Handles DoubleInput2.ValueChanged
        tbTotalDo.Text = Convert.ToString(DoubleInput1.Value * DoubleInput2.Value)
    End Sub
    Dim fechaOriginal As String
    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        If (L_fnVerificarCObros(tbCodigo.Text, cbSucursal.Value)) Then

            Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)
            ToastNotification.Show(Me, "No se puede editar la venta con código ".ToUpper + tbCodigo.Text + ", porque tiene pagos realizados.".ToUpper,
                                                  img, 5000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter)
            Exit Sub
        Else
            fechaOriginal = tbFechaVenta.Text
            _prhabilitar()
            Dim dt As DataTable
            dt = L_fnListarClientesVentas(_CodCliente)
            _CodCaneroUcg = dt.Rows(0).Item(1)

            _CodEmpleado = dt.Rows(0).Item(8)
            btnNuevo.Enabled = False
            btnModificar.Enabled = False
            btnEliminar.Enabled = False
            btnGrabar.Enabled = True
        End If

    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If (L_fnVerificarCObros(tbCodigo.Text, cbSucursal.Value)) Then

            Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)
            ToastNotification.Show(Me, "No se puede anular la venta con código ".ToUpper + tbCodigo.Text + ", porque tiene pagos realizados.".ToUpper,
                                                  img, 5000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter)
            Exit Sub
        Else
            If (tbCodigo.Text <> String.Empty) Then
                Dim result As DialogResult = MessageBox.Show("¿Está seguro de ANULAR el prestamo de Diesel con número de orden:" + tbCodigo.Text + "?", "PRESTAMO DIESEL OTROS SURTIDORES", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)

                If result = DialogResult.OK Then
                    Dim mensajeError As String = ""
                    Dim res As Boolean = L_fnEliminarVentaOtroSurtidor(tbCodigo.Text, mensajeError, Programa)
                    If res Then
                        Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                        ToastNotification.Show(Me, "Código de Prestamo de Diesel ".ToUpper + tbCodigo.Text + " anulado con Exito.".ToUpper,
                                                  img, 2000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter)

                        _prCargarVenta()
                        Dim _pos As Integer = grVentas.Row
                        If grVentas.RowCount > 0 Then
                            _pos = grVentas.RowCount - 1
                            ''  _prMostrarRegistro(_pos)
                            grVentas.Row = _pos
                        End If

                    Else
                        Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                        ToastNotification.Show(Me, mensajeError, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                    End If
                ElseIf result = DialogResult.Cancel Then
                    ' Código para ejecutar si se hace clic en "Cancelar"
                End If


            End If
        End If
    End Sub

    Private Sub tbRetSurtidor_KeyDown(sender As Object, e As KeyEventArgs) Handles tbRetSurtidor.KeyDown
        'If e.KeyData = Keys.Enter Then
        '    tbNitRetSurtidor.Focus()
        'End If
    End Sub

    Private Sub tbNitRetSurtidor_KeyDown(sender As Object, e As KeyEventArgs) Handles tbNitRetSurtidor.KeyDown
        'If e.KeyData = Keys.Enter Then
        '    cbSurtidor.Focus()
        'End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        contabilizarPrestamo(tbCodigo.Text)
    End Sub





#End Region
End Class