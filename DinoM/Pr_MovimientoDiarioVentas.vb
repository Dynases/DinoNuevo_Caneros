Imports DevComponents.DotNetBar
Imports Logica.AccesoLogica


Public Class Pr_MovimientoDiarioVentas

    Public Sub iniciarcomponentes()
        tbFechaI.Value = Date.Now
        tbFechaF.Value = Date.Now
    End Sub

    Public Sub _prInterpretarDatos(ByRef _dt As DataTable)
        Dim cbSucrusal As Integer
        cbSucrusal = gi_userSuc
        _dt = L_prReporteDiarioVentas(cbSucrusal, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"))
    End Sub

    Private Sub _prCargarReporte()


        Dim _dt As New DataTable
        _prInterpretarDatos(_dt)
        If (_dt.Rows.Count > 0) Then
            Dim suc As String
            suc = gs_userSucNom
            Dim objrep As New ReporteDiarioVentas
            objrep.SetDataSource(_dt)
            Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
            Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
            objrep.SetParameterValue("sucursal", suc)
            objrep.SetParameterValue("fechaI", fechaI)
            objrep.SetParameterValue("fechaF", fechaF)
            CrystalReportViewer1.ReportSource = objrep
            CrystalReportViewer1.Show()
            CrystalReportViewer1.BringToFront()

        Else

            ToastNotification.Show(Me, "NO HAY DATOS PARA LOS PARAMETROS SELECCIONADOS..!!!",
            My.Resources.INFORMATION, 2000,
            eToastGlowColor.Blue,
            eToastPosition.BottomLeft)
            CrystalReportViewer1.ReportSource = Nothing
        End If

    End Sub
    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        Me.Close()
    End Sub

    Private Sub Pr_MovimientoDiarioVentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        iniciarcomponentes()
    End Sub

    Private Sub ButtonX2_Click(sender As Object, e As EventArgs) Handles ButtonX2.Click
        _prCargarReporte()
    End Sub
End Class