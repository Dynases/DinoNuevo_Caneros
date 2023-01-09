Imports Logica.AccesoLogica

Public Class F0_Prestamos

    Dim cod As Integer
    Public Sub iniciarcomponentes()
        tbInst.ReadOnly = True
        codIns.ReadOnly = True
        tbfecha.Value = Date.Now
        codCan.ReadOnly = True
        tbCanero.ReadOnly = True
        codFin.ReadOnly = True
        codPres.ReadOnly = True
        codMon.ReadOnly = True
        btnModificar.Visible = False
        btnEliminar.Visible = False
        btnGrabar.Enabled = False
        _prCargarComboFinanciador(tbFinan)
        _prCargarComboMoneda(tbMoneda)


    End Sub

    Private Sub _prCargarComboMoneda(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnGeneralMoneda()

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
    Private Sub _prCargarComboFinanciador(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = L_fnGeneralFinanciadores()

        'a.ylcod1 ,a.yldes1 
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("ylcod2").Width = 70
            .DropDownList.Columns("ylcod2").Caption = "COD"
            .DropDownList.Columns.Add("yldes2").Width = mCombo.Width - 70
            .DropDownList.Columns("yldes2").Caption = "DESCRIPCION"
            .ValueMember = "ylcod2"
            .DisplayMember = "yldes2"
            .DataSource = dt
            .Refresh()
        End With
        If (CType(tbFinan.DataSource, DataTable).Rows.Count > 0) Then
            tbFinan.SelectedIndex = 0
        End If
    End Sub
    Private Sub _prCargarComboTipoPrestamo(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim Finan As Integer = tbFinan.Value
        codFin.Text = Finan
        Dim dt As New DataTable
        dt = L_fnGeneralTipoPrestamo(Finan)

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
        If (CType(tbPrest.DataSource, DataTable).Rows.Count > 0) Then
            tbPrest.SelectedIndex = 0
        End If
    End Sub
    Private Sub btnNuevo_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBoxX8_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub LabelX1_Click(sender As Object, e As EventArgs) Handles LabelX1.Click

    End Sub

    Private Sub F0_Prestamos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        iniciarcomponentes()
    End Sub

    Private Sub TextBoxX7_TextChanged(sender As Object, e As EventArgs) Handles codFin.TextChanged

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
                    codIns.Text = Row.Cells("ydnumivend").Value
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

    Private Sub tbCanero_TextChanged(sender As Object, e As EventArgs) Handles tbCanero.TextChanged
        Dim dt As DataTable
        dt = L_fnListarCaneroInstitucion(cod)
        Dim row As DataRow = dt.Rows(dt.Rows.Count - 1)
        tbInst.Text = row("institucion")
    End Sub

    Private Sub tbFinan_ValueChanged(sender As Object, e As EventArgs) Handles tbFinan.ValueChanged
        _prCargarComboTipoPrestamo(tbPrest)
    End Sub

    Private Sub tbPrest_ValueChanged(sender As Object, e As EventArgs) Handles tbPrest.ValueChanged
        Dim Pres As Integer = tbPrest.Value
        codPres.Text = Pres
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub tbMoneda_ValueChanged(sender As Object, e As EventArgs) Handles tbMoneda.ValueChanged
        codMon.Text = tbMoneda.Value
    End Sub
End Class