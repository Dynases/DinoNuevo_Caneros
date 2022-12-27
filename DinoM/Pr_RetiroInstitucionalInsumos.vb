﻿Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Public Class Pr_RetiroInstitucionalInsumos

    Public Sub iniciarcomponentes()
        tbInsCan.ReadOnly = True
        tbCod.ReadOnly = True

        CheckTodos.CheckValue = True
        tbFechaI.Value = Date.Now
        tbFechaF.Value = Date.Now
    End Sub

    Public Sub _prInterpretarDatos(ByRef _dt As DataTable)
        Dim cbSucursal As String
        cbSucursal = gi_userSuc

        If (CheckUna.Checked) Then
            _dt = L_prRetiroInstitucionalUno(tbCod.Text, cbSucursal, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"))
        End If
        If (CheckTodos.Checked) Then
            _dt = L_prRetiroInstitucionalTodos(cbSucursal, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"))
        End If

    End Sub
    Private Sub _prCargarReporte()
        If (CheckUna.Checked = True And tbInsCan.Text = "") Then
            ToastNotification.Show(Me, "SELECCIONE INSTITUCION (PRESIONANDO CTRL+ENTER)",
                        My.Resources.INFORMATION, 2000,
                        eToastGlowColor.Blue,
                        eToastPosition.BottomLeft)
            CrystalReportViewer1.ReportSource = Nothing

        Else
            Dim _dt As New DataTable
            _prInterpretarDatos(_dt)
            If (_dt.Rows.Count > 0) Then
                If CheckUna.Checked = True Then

                    Dim objrep As New RetiroInsumosUno
                    objrep.SetDataSource(_dt)
                    Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
                    Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
                    Dim sucursal As String = gs_userSucNom
                    Dim codcanero As String = tbCod.Text
                    Dim institucion As String = tbInsCan.Text
                    objrep.SetParameterValue("cod", codcanero)
                    objrep.SetParameterValue("institucion", institucion)
                    objrep.SetParameterValue("almacen", sucursal)
                    objrep.SetParameterValue("fechaI", fechaI)
                    objrep.SetParameterValue("fechaF", fechaF)
                    CrystalReportViewer1.ReportSource = objrep
                    CrystalReportViewer1.Show()
                    CrystalReportViewer1.BringToFront()
                Else
                    Dim objrep As New RetiroInstitucionalTodos
                    objrep.SetDataSource(_dt)
                    Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
                    Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
                    Dim sucursal As String = gs_userSucNom
                    objrep.SetParameterValue("sucursal", sucursal)
                    objrep.SetParameterValue("fechaI", fechaI)
                    objrep.SetParameterValue("fechaF", fechaF)
                    CrystalReportViewer1.ReportSource = objrep
                    CrystalReportViewer1.Show()
                    CrystalReportViewer1.BringToFront()
                End If
            Else

                ToastNotification.Show(Me, "NO HAY DATOS PARA LOS PARAMETROS SELECCIONADOS..!!!",
                My.Resources.INFORMATION, 2000,
                eToastGlowColor.Blue,
                eToastPosition.BottomLeft)
                CrystalReportViewer1.ReportSource = Nothing
            End If

        End If



    End Sub
    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        Me.Close()
    End Sub

    Private Sub Pr_RetiroInstitucionalInsumos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
    End Sub

    Private Sub ButtonX2_Click(sender As Object, e As EventArgs) Handles ButtonX2.Click
        _prCargarReporte()
    End Sub

    Private Sub CrystalReportViewer1_Load(sender As Object, e As EventArgs) Handles CrystalReportViewer1.Load

    End Sub
End Class