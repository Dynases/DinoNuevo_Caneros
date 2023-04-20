Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar.Controls
Imports DevComponents.DotNetBar
Imports Janus.Windows.GridEX


Public Class F1_Liquidaciones

    Dim _CodCliente, _CodInstitucion As Integer

    Private Sub tbCanero_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCanero.KeyDown
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
                tbCod.Text = Row.Cells("ydcod").Value
                _CodCliente = Row.Cells("ydnumi").Value
                tbCanero.Text = Row.Cells("ydrazonsocial").Value


            End If

        End If
    End Sub

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        Dim dt As DataTable = L_fnCargarLiquidacion(IIf(tbCod.Text = "", -1, _CodCliente), tbFecha.Value.ToString("dd-MM-yyyy"))
        _prCargarVenta1(dt)
    End Sub

    Private Sub _prCargarVenta1(dt As DataTable)


        JGrM_Buscador.DataSource = dt
        JGrM_Buscador.RetrieveStructure()
        JGrM_Buscador.AlternatingColors = True
        '   a.tamon ,IIF(tamon=1,'Boliviano','Dolar') as moneda,a.taest ,a.taobs ,
        'a.tadesc ,a.tafact ,a.tahact ,a.tauact,(Sum(b.tbptot)-a.tadesc ) as total

        With JGrM_Buscador.RootTable.Columns("trid")
            .Width = 100
            .Caption = "CODIGO"
            .Visible = False

        End With

        With JGrM_Buscador.RootTable.Columns("ydnumi")
            .Width = 90
            .Visible = False
        End With

        With JGrM_Buscador.RootTable.Columns("doc")
            .Width = 90
            .Visible = False
        End With
        With JGrM_Buscador.RootTable.Columns("alm")
            .Width = 250
            .Visible = True
            .Caption = "Tipo de Deuda"
        End With
        With JGrM_Buscador.RootTable.Columns("comb")
            .Width = 150
            .Visible = True
            .Caption = "Combustible"
            .FormatString = "0.00"
        End With

        With JGrM_Buscador.RootTable.Columns("insumos")
            .Width = 150
            .Visible = True
            .Caption = "Insumos"
            .FormatString = "0.00"
        End With

        With JGrM_Buscador.RootTable.Columns("Rest")
            .Width = 150
            .Visible = True
            .Caption = "Restructuracion"
            .FormatString = "0.00"
        End With
        With JGrM_Buscador.RootTable.Columns("otros")
            .Width = 150
            .Visible = True
            .Caption = "Otros Prestamos"
            .FormatString = "0.00"
        End With
        With JGrM_Buscador.RootTable.Columns("convenio")
            .Width = 150
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
            .Visible = True
            .Caption = "P. Convenio"
            .FormatString = "0.00"
        End With

        With JGrM_Buscador
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla

        End With


    End Sub

    Private Sub F1_Liquidaciones_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tbFecha.Value = Date.Now
    End Sub

    Private Sub tbCanero_TextChanged(sender As Object, e As EventArgs) Handles tbCanero.TextChanged
        Dim dt As DataTable
        dt = L_fnListarCaneroInstitucion(_CodCliente)
        Dim row As DataRow = dt.Rows(dt.Rows.Count - 1)
        tbInstitucion.Text = row("institucion")
        tbCodIns.Text = row("codInst")
        _CodInstitucion = row("id")
    End Sub
End Class