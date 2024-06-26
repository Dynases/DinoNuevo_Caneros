﻿Imports DevComponents.DotNetBar
Imports Logica.AccesoLogica

Public Class Pr_BoletaMovimientoDiario

    Public Sub iniciarcomponentes()

        tbFechaI.Value = Date.Now
        tbFechaF.Value = Date.Now

    End Sub

    Public Sub _prInterpretarDatos(ByRef _dt As DataTable)

        _dt = L_prReporteCañeroTodos(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"))

    End Sub

    Private Sub _prCargarReporte()

        Dim _dt As New DataTable
        _prInterpretarDatos(_dt)
        If (_dt.Rows.Count > 0) Then

            Dim objrep As New ReporteDiario
            objrep.SetDataSource(_dt)
            Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
            Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")

            objrep.SetParameterValue("fechaI", fechaI)
            objrep.SetParameterValue("fechaF", fechaF)
            MReportViewer.ReportSource = objrep
            MReportViewer.Show()
            MReportViewer.BringToFront()





        Else

            ToastNotification.Show(Me, "NO HAY DATOS PARA LOS PARAMETROS SELECCIONADOS..!!!",
            My.Resources.INFORMATION, 2000,
            eToastGlowColor.Blue,
            eToastPosition.BottomLeft)
            MReportViewer.ReportSource = Nothing
        End If


    End Sub

    Private Sub Pr_BoletaMovimientoDiario_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        iniciarcomponentes()
    End Sub

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        _prCargarReporte()
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class