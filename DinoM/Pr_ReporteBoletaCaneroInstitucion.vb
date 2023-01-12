Imports DevComponents.DotNetBar
Imports Logica.AccesoLogica

Public Class Pr_ReporteBoletaCaneroInstitucion

    Public Sub iniciarcomponentes()
        tbInsCan.ReadOnly = True
        tbCod.ReadOnly = True

        CheckTodos.CheckValue = True
        tbFechaI.Value = Date.Now
        tbFechaF.Value = Date.Now
        swSubTipo.Visible = True
        swTipo.Visible = True



    End Sub

    Public Sub _prInterpretarDatos(ByRef _dt As DataTable)
        If (swTipo.Value = False And CheckUna.Checked) Then
            _dt = L_prReporteCañeroUno(tbCod.Text, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"))
        End If
        If (swTipo.Value = False And CheckTodos.Checked) Then
            _dt = L_prReporteCañeroTodos(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"))
        End If
        If (swTipo.Value = True And CheckUna.Checked And swSubTipo.Value = True) Then
            _dt = L_prReporteInstitucionUna(tbCod.Text, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"))
        End If
        If (swTipo.Value = True And CheckUna.Checked And swSubTipo.Value = False) Then
            _dt = L_prReporteInstitucionUnaSola(tbCod.Text, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"))
        End If
        If (swTipo.Value = True And CheckTodos.Checked And swSubTipo.Value = True) Then
            _dt = L_prReporteInstitucionTodos(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"))
        End If
        If (swTipo.Value = True And CheckTodos.Checked And swSubTipo.Value = False) Then
            _dt = L_prReporteInstitucionSolos(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"))
        End If
    End Sub
    Private Sub _prCargarReporte()
        If (CheckUna.Checked = True And tbInsCan.Text = "") Then
            ToastNotification.Show(Me, "SELECCIONE INSTITUCION/CAÑERO (PRESIONANDO CTRL+ENTER)",
                        My.Resources.INFORMATION, 2000,
                        eToastGlowColor.Blue,
                        eToastPosition.BottomLeft)
            MReportViewer.ReportSource = Nothing

        Else
            Dim _dt As New DataTable
            _prInterpretarDatos(_dt)
            If (_dt.Rows.Count > 0) Then
                If (swTipo.Value = False) Then
                    If CheckUna.Checked = True Then

                        Dim objrep As New ReporteCañero
                        objrep.SetDataSource(_dt)
                        Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
                        Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
                        Dim canero As String = tbInsCan.Text
                        Dim codcanero As String = tbCod.Text
                        objrep.SetParameterValue("codCan", codcanero)
                        objrep.SetParameterValue("cañero", canero)
                        objrep.SetParameterValue("fechaI", fechaI)
                        objrep.SetParameterValue("fechaF", fechaF)
                        MReportViewer.ReportSource = objrep
                        MReportViewer.Show()
                        MReportViewer.BringToFront()
                    Else
                        Dim objrep As New ReporteCañeroTodos
                        objrep.SetDataSource(_dt)
                        Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
                        Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
                        Dim canero As String = tbInsCan.Text
                        Dim codcanero As String = tbCod.Text
                        objrep.SetParameterValue("fechaI", fechaI)
                        objrep.SetParameterValue("fechaF", fechaF)
                        MReportViewer.ReportSource = objrep
                        MReportViewer.Show()
                        MReportViewer.BringToFront()
                    End If

                Else swTipo.Value = True

                    If CheckUna.Checked = True Then
                        If swSubTipo.Value = True Then
                            Dim objrep As New ReporteInstitucion
                            objrep.SetDataSource(_dt)
                            Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
                            Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
                            Dim canero As String = tbInsCan.Text
                            Dim codcanero As String = tbCod.Text
                            objrep.SetParameterValue("codIns", codcanero)
                            objrep.SetParameterValue("institucion", canero)
                            objrep.SetParameterValue("fechaI", fechaI)
                            objrep.SetParameterValue("fechaF", fechaF)
                            MReportViewer.ReportSource = objrep
                            MReportViewer.Show()
                            MReportViewer.BringToFront()
                        Else
                            Dim objrep As New ReporteInsitucionUnaSolo
                            objrep.SetDataSource(_dt)
                            Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
                            Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
                            Dim canero As String = tbInsCan.Text
                            Dim codcanero As String = tbCod.Text
                            objrep.SetParameterValue("codIns", codcanero)
                            objrep.SetParameterValue("institucion", canero)
                            objrep.SetParameterValue("fechaI", fechaI)
                            objrep.SetParameterValue("fechaF", fechaF)
                            MReportViewer.ReportSource = objrep
                            MReportViewer.Show()
                            MReportViewer.BringToFront()
                        End If
                    Else
                        If swSubTipo.Value = True Then
                            Dim objrep As New ReporteInstituciones
                            objrep.SetDataSource(_dt)
                            Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
                            Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
                            objrep.SetParameterValue("fechaI", fechaI)
                            objrep.SetParameterValue("fechaF", fechaF)
                            MReportViewer.ReportSource = objrep
                            MReportViewer.Show()
                            MReportViewer.BringToFront()
                        Else
                            Dim objrep As New ReporteInstitucionesSolo
                            objrep.SetDataSource(_dt)
                            Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
                            Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
                            objrep.SetParameterValue("fechaI", fechaI)
                            objrep.SetParameterValue("fechaF", fechaF)
                            MReportViewer.ReportSource = objrep
                            MReportViewer.Show()
                            MReportViewer.BringToFront()
                        End If

                    End If
                End If

            Else

                ToastNotification.Show(Me, "NO HAY DATOS PARA LOS PARAMETROS SELECCIONADOS..!!!",
                My.Resources.INFORMATION, 2000,
                eToastGlowColor.Blue,
                eToastPosition.BottomLeft)
                MReportViewer.ReportSource = Nothing
            End If

        End If



    End Sub

    Private Sub swTipo_Click(sender As Object, e As EventArgs)
        tbCod.Text = ""
        tbInsCan.Text = ""
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If swTipo.Value = True Then
            LabelX3.Text = "Institución: "

        Else
            LabelX3.Text = "Cañero: "

        End If
    End Sub

    Private Sub Pr_ReporteBoletaCaneroInstitucion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        iniciarcomponentes()
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

    Private Sub CheckUna_CheckedChanged(sender As Object, e As EventArgs) Handles CheckUna.CheckedChanged
        If CheckUna.Checked = True Then
            If CheckTodos.Checked = True Then
                CheckTodos.Checked = False
            End If
        End If
    End Sub

    Private Sub tbInsCan_KeyDown(sender As Object, e As KeyEventArgs) Handles tbInsCan.KeyDown
        If swTipo.Value = True Then

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
                        tbCod.Text = Row.Cells("CodInst").Value
                        tbInsCan.Text = Row.Cells("nomInst").Value
                        'btnGenerar.Focus()
                    End If

                End If

            End If


        Else swTipo.Value = False
            If (CheckUna.Checked) Then
                If e.KeyData = Keys.Control + Keys.Enter Then
                    Dim dt As DataTable
                    dt = L_fnListarCaneros()

                    Dim listEstCeldas As New List(Of Modelo.Celda)

                    listEstCeldas.Add(New Modelo.Celda("ydnumi", True, "Código".ToUpper, 80))
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
                            tbInsCan.Focus()
                            Return
                        End If
                        tbCod.Text = Row.Cells("ydcod").Value
                        tbInsCan.Text = Row.Cells("ydrazonsocial").Value
                        'btnGenerar.Focus()
                    End If

                End If

            End If
        End If
    End Sub

    Private Sub swTipo_ValueChanged(sender As Object, e As EventArgs) Handles swTipo.ValueChanged
        If (swTipo.Value = True) Then
            swSubTipo.Visible = True
            CheckTodos.Checked = True
            CheckUna.Checked = False
        ElseIf swTipo.Value = False Then
            swSubTipo.Visible = False
            CheckTodos.Checked = True
            'CheckUna.Checked = False
        Else
            swSubTipo.Visible = False
        End If
    End Sub

    Private Sub swSubTipo_ValueChanged(sender As Object, e As EventArgs) Handles swSubTipo.ValueChanged

    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        _prCargarReporte()
    End Sub
End Class