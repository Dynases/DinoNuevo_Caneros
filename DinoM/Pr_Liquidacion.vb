Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar.Controls
Imports DevComponents.DotNetBar


Public Class Pr_Liquidacion

    Dim _CodCliente As Integer = 0
    Dim _CodInstitucion As Integer = 0

    Private Sub _prCargarQuincena(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo)
        Dim dt As New DataTable
        dt = TraerTipoPrestamos()
        dt.Rows.Add(0, "Todos")
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("yccod3").Width = 60
            .DropDownList.Columns("yccod3").Caption = "CODIGO"
            .DropDownList.Columns.Add("ycdes3").Width = 100
            .DropDownList.Columns("ycdes3").Caption = "PRESTAMO"
            .ValueMember = "yccod3"
            .DisplayMember = "ycdes3"
            .DataSource = dt
            .Refresh()
        End With
    End Sub
    Private Sub tbInsCan_KeyDown(sender As Object, e As KeyEventArgs) Handles tbInsCan.KeyDown
        If (CheckUna.Checked) Then
            If e.KeyData = Keys.Control + Keys.Enter Then
                Dim dt As DataTable
                dt = L_fnListarInstitucion()

                Dim listEstCeldas As New List(Of Modelo.Celda)
                listEstCeldas.Add(New Modelo.Celda("id,", False, "ID", 50))
                listEstCeldas.Add(New Modelo.Celda("CodInst", True, "CODIGO", 50))
                listEstCeldas.Add(New Modelo.Celda("nomInst", True, "NOMBRE", 280))
                listEstCeldas.Add(New Modelo.Celda("direc", True, "DIRECCION".ToUpper, 150))
                listEstCeldas.Add(New Modelo.Celda("telf", True, "TELEFONO", 220))
                listEstCeldas.Add(New Modelo.Celda("campo1", False, "campo1".ToUpper, 200))
                listEstCeldas.Add(New Modelo.Celda("campo2", False, "F.campo2".ToUpper, 150))
                listEstCeldas.Add(New Modelo.Celda("campo3", False, "campo3".ToUpper, 200))
                listEstCeldas.Add(New Modelo.Celda("fact", False, "F.fact".ToUpper, 150))
                listEstCeldas.Add(New Modelo.Celda("hact", False, "hact".ToUpper, 200))
                listEstCeldas.Add(New Modelo.Celda("uact", False, "F.uact".ToUpper, 150))
                Dim ef = New Efecto
                ef.tipo = 3
                ef.dt = dt
                ef.SeleclCol = 1
                ef.listEstCeldas = listEstCeldas
                ef.alto = 50
                ef.ancho = 350
                ef.Context = "Seleccione Institucion".ToUpper
                ef.ShowDialog()
                Dim bandera As Boolean = False
                bandera = ef.band
                If (bandera = True) Then
                    Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                    If (IsNothing(Row)) Then
                        tbInsCan.Focus()
                        Return
                    End If
                    _CodInstitucion = Row.Cells("id").Value
                    tbCod.Text = Row.Cells("CodInst").Value
                    tbInsCan.Text = Row.Cells("nomInst").Value
                    'btnGenerar.Focus()
                End If

            End If

        End If
    End Sub

    Private Sub tbInsCan_TextChanged(sender As Object, e As EventArgs) Handles tbInsCan.TextChanged
        If tbInsCan.Text = "" Then
            LabelX2.Visible = False
            tbCodCan.Visible = False
            tbNomCan.Visible = False
            CheckUnaCan.Visible = False
            CheckTodosCan.Visible = False
        Else
            LabelX2.Visible = True
            tbCodCan.Visible = True
            tbNomCan.Visible = True
            CheckUnaCan.Visible = True
            CheckTodosCan.Visible = True
        End If

    End Sub

    Private Sub tbNomCan_KeyDown(sender As Object, e As KeyEventArgs) Handles tbNomCan.KeyDown
        If (CheckUnaCan.Checked) Then
            If e.KeyData = Keys.Control + Keys.Enter Then
                Dim dt As DataTable
                dt = L_fnListarCanerosxInst(CInt(tbCod.Text))

                Dim listEstCeldas As New List(Of Modelo.Celda)

                listEstCeldas.Add(New Modelo.Celda("ydnumi", False, "Código".ToUpper, 80))
                listEstCeldas.Add(New Modelo.Celda("ydcod", True, "Cod. Cañero".ToUpper, 100))
                listEstCeldas.Add(New Modelo.Celda("ydrazonsocial", True, "Nombre".ToUpper, 180))

                listEstCeldas.Add(New Modelo.Celda("ydtelf1", True, "telefono".ToUpper, 100))
                Dim ef = New Efecto
                ef.tipo = 3
                ef.dt = dt
                ef.SeleclCol = 1
                ef.listEstCeldas = listEstCeldas
                ef.alto = 50
                ef.ancho = 350
                ef.Context = "Seleccione Cañero".ToUpper
                ef.ShowDialog()
                Dim bandera As Boolean = False
                bandera = ef.band
                If (bandera = True) Then
                    Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                    If (IsNothing(Row)) Then
                        tbNomCan.Focus()
                        Return
                    End If
                    _CodCliente = Row.Cells("ydnumi").Value
                    tbCodCan.Text = Row.Cells("ydcod").Value
                    tbNomCan.Text = Row.Cells("ydrazonsocial").Value
                    'btnGenerar.Focus()
                End If

            End If

        End If
    End Sub

    Private Sub CheckUna_CheckedChanged(sender As Object, e As EventArgs) Handles CheckUna.CheckedChanged
        If CheckUna.Checked = True Then
            If CheckTodos.Checked = True Then
                CheckTodos.Checked = False
            End If
        End If
    End Sub

    Private Sub CheckTodos_CheckedChanged(sender As Object, e As EventArgs) Handles CheckTodos.CheckedChanged
        If CheckTodos.Checked = True Then
            If CheckUna.Checked = True Then
                CheckUna.Checked = False
                tbCod.Text = ""
                tbInsCan.Text = ""
            End If
        End If
    End Sub

    Private Sub CheckUnaCan_CheckedChanged(sender As Object, e As EventArgs) Handles CheckUnaCan.CheckedChanged
        If CheckUnaCan.Checked = True Then
            If CheckTodosCan.Checked = True Then
                CheckTodosCan.Checked = False
            End If
        End If
    End Sub

    Private Sub CheckTodosCan_CheckedChanged(sender As Object, e As EventArgs) Handles CheckTodosCan.CheckedChanged
        If CheckTodosCan.Checked = True Then
            If CheckUnaCan.Checked = True Then
                CheckUnaCan.Checked = False
                tbCodCan.Text = ""
                tbNomCan.Text = ""
            End If
        End If
    End Sub
    Private Function interpretarDatos() As DataTable
        Dim dt As DataTable = CargarCCPagosSaldos(IIf(CheckTodosCan.Checked = True, -1, _CodCliente), IIf(CheckTodos.Checked = True, -1, _CodInstitucion), IIf(cbQuincena.Value = 0, -1, cbQuincena.Value), tbFechaI.Value.ToString("yyyy/MM/dd"))

        Return dt
    End Function
    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        Dim dt As DataTable = interpretarDatos()
        If dt.Rows.Count > 0 Then
            Dim objrep As New R_CCPagosSaldos
            objrep.SetDataSource(dt)

            objrep.SetParameterValue("prestamo", cbQuincena.Text)
            objrep.SetParameterValue("fecha", tbFechaI.Value.ToString("dd/MM/yyyy"))
            MReportViewer.ReportSource = objrep
            MReportViewer.Show()
            MReportViewer.BringToFront()
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "NO HAY DATOS PARA MOSTRAR".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomLeft)
        End If
    End Sub

    Private Sub SuperTabControlPanelRegistro_Click(sender As Object, e As EventArgs) Handles SuperTabControlPanelRegistro.Click

    End Sub

    Private Sub Pr_Liquidacion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prCargarQuincena(cbQuincena)
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class