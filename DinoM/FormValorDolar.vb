Imports DevComponents.DotNetBar
Imports Logica.AccesoLogica
Public Class FormValorDolar
    Private Sub FormValorDolar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prCargarComboLibreria(cbCambioDolar, 7, 1)
        cbCambioDolar.SelectedIndex = cbCambioDolar.DropDownList.RecordCount - 1
        tbFechaVenta.Value = Date.Today
    End Sub

    Private Sub _prCargarComboLibreria(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo, cod1 As String, cod2 As String)
        Dim dt As New DataTable
        dt = L_prLibreriaClienteLGeneral1(cod1, cod2)
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("yccod3").Width = 70
            .DropDownList.Columns("yccod3").Caption = "COD"
            .DropDownList.Columns.Add("ycdes3").Width = 120
            .DropDownList.Columns("ycdes3").Caption = "DESCRIPCION"
            .DropDownList.Columns.Add("ycdes1").Width = 130
            .DropDownList.Columns("ycdes1").Caption = "FECHA"
            .ValueMember = "yccod3"
            .DisplayMember = "ycdes3"
            .DataSource = dt
            .Refresh()
        End With

    End Sub

    Private Sub LabelX1_Click(sender As Object, e As EventArgs) Handles LabelX1.Click

    End Sub

    Private Sub cbCambioDolar_ValueChanged(sender As Object, e As EventArgs) Handles cbCambioDolar.ValueChanged

    End Sub

    Private Sub btgrupo1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnIngresar_Click(sender As Object, e As EventArgs) Handles btnIngresar.Click
        Dim valor As String = Format(CDec(cbCambioDolar.Text), "0.00")
        Dim numi As String = ""
        Dim dt1 As New DataTable
        dt1 = L_prLibreriaClienteLGeneral1(7, 1)
        Dim ultimaFila As DataRow = dt1.Rows(dt1.Rows.Count - 1)

        Dim tipoCambio As String = ultimaFila("ycdes3").ToString()
        Dim fecha1 As String = ultimaFila("ycdes1").ToString()
        If fecha1 = Date.Today Then
            MessageBox.Show("YA SE REGISTRO EL TIPO DE CAMBIO PARA EL " + fecha1 + " ES " + tipoCambio)
            _prCargarComboLibreria(cbCambioDolar, "7", "1")
            cbCambioDolar.SelectedIndex = CType(cbCambioDolar.DataSource, DataTable).Rows.Count - 1
        Else
            If L_prLibreriaGrabarNuevoCambioDolar(numi, "7", "1", valor, tbFechaVenta.Text) Then
                _prCargarComboLibreria(cbCambioDolar, "7", "1")

                cbCambioDolar.SelectedIndex = CType(cbCambioDolar.DataSource, DataTable).Rows.Count - 1
                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                ToastNotification.Show(Me, "Registro de tipo de cambio registrado con éxito.".ToUpper,
                                         img, 2000,
                                         eToastGlowColor.Green,
                                         eToastPosition.TopCenter
                                         )
            End If
        End If


    End Sub
End Class