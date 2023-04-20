Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports Janus.Windows.GridEX
Public Class F1_TotalCana

    Dim _CodCliente, _CodInstitucion As Integer

    Private Sub IniciarTodo()
        _PMIniciarTodo()
        tbFecha.Value = Date.Now
        InHabilitar()
        'CargarDatos()
    End Sub

    Private Sub Habilitar()
        btnEliminar.Enabled = False
        btnGrabar.Enabled = True
        btnModificar.Enabled = False
        btnNuevo.Enabled = False

        tbCanero.Enabled = True
        tbGestion.ReadOnly = False
        TextBoxX4.ReadOnly = False
    End Sub
    Private Sub InHabilitar()
        btnEliminar.Enabled = True
        btnGrabar.Enabled = False
        btnModificar.Enabled = True
        btnNuevo.Enabled = True

        tbCanero.Enabled = False
        tbGestion.ReadOnly = True
        TextBoxX4.ReadOnly = True
    End Sub

    Private Sub Limpiar()

        tbCod.Clear()
        tbCanero.Clear()
        tbCodIns.Clear()
        tbInstitucion.Clear()
        tbId.Clear()
        tbFecha.Value = Date.Now
        tbGestion.Clear()
        TextBoxX4.Clear()
    End Sub
    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelo.Celda)
        Dim listEstCeldas As New List(Of Modelo.Celda)

        't.canumi , t.canombre, t.cacuenta, t.caobs, t.cafact, t.cahact, t.cauact 
        listEstCeldas.Add(New Modelo.Celda("numi", True, "Codigo", 150))
        listEstCeldas.Add(New Modelo.Celda("codCan", False))
        listEstCeldas.Add(New Modelo.Celda("ydcod", True, "Cod. Can.", 100))

        listEstCeldas.Add(New Modelo.Celda("ydrazonsocial", True, "Nombre", 250))
        listEstCeldas.Add(New Modelo.Celda("codIns", False))
        listEstCeldas.Add(New Modelo.Celda("codInst", False))
        listEstCeldas.Add(New Modelo.Celda("nomInst", False))
        listEstCeldas.Add(New Modelo.Celda("fecha", True, "Fecha", 250))
        listEstCeldas.Add(New Modelo.Celda("gestion", True, "Gestion", 250))
        listEstCeldas.Add(New Modelo.Celda("total", True, "Total", 250))


        Return listEstCeldas


    End Function
    Public Overrides Function _PMOGetTablaBuscador() As DataTable
        Dim dtBuscador As DataTable = L_fnCargarCañaComprometida()
        Return dtBuscador
    End Function
    Public Overrides Sub _PMOMostrarRegistro(_N As Integer)
        '' grVentas.Row = _N
        '     a.tanumi ,a.taalm ,a.tafdoc ,a.taven ,vendedor .yddesc as vendedor ,a.tatven ,a.tafvcr ,a.taclpr,
        'cliente.yddesc as cliente ,a.tamon ,IIF(tamon=1,'Boliviano','Dolar') as moneda,a.taest ,a.taobs ,
        'a.tadesc ,a.tafact ,a.tahact ,a.tauact,(Sum(b.tbptot)-a.tadesc ) as total,taproforma

        With JGrM_Buscador
            _CodInstitucion = .GetValue("codIns")
            _CodCliente = .GetValue("codCan")
            tbId.Text = .GetValue("numi")
            tbCanero.Text = .GetValue("ydrazonsocial")
            tbInstitucion.Text = .GetValue("nomInst")
            tbCod.Text = .GetValue("ydcod")
            tbCodIns.Text = .GetValue("codInst")
            tbFecha.Value = .GetValue("fecha").ToString
            tbGestion.Text = .GetValue("gestion")
            TextBoxX4.Text = .GetValue("total")


        End With
        With JGrM_Buscador
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla

        End With



    End Sub
    Private Sub CargarDatos()
        Dim dt As DataTable
        dt = L_fnCargarCañaComprometida()

        JGrM_Buscador.DataSource = dt
        JGrM_Buscador.RetrieveStructure()
        JGrM_Buscador.AlternatingColors = True
    End Sub
    Private Sub TextBoxX5_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCanero.KeyDown
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

    Private Sub tbCanero_TextChanged(sender As Object, e As EventArgs) Handles tbCanero.TextChanged

        Dim dt As DataTable
        dt = L_fnListarCaneroInstitucion(_CodCliente)
        Dim row As DataRow = dt.Rows(dt.Rows.Count - 1)
        tbInstitucion.Text = row("institucion")
        tbCodIns.Text = row("codInst")
        _CodInstitucion = row("id")

    End Sub

    Private Sub F1_TotalCana_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IniciarTodo()
    End Sub
    Public Overrides Function _PMOGrabarRegistro() As Boolean
        Dim res As Boolean = L_fnRegistrarCañaComprometida(_CodCliente, _CodInstitucion, tbFecha.Value.ToString("dd-MM-yyyy"), CInt(tbGestion.Text), CDbl(TextBoxX4.Text))
        If res Then
            Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

            ToastNotification.Show(Me, "El registro se completo exitosamente".ToUpper,
                                              img, 5000,
                                              eToastGlowColor.Green,
                                              eToastPosition.TopCenter)

            Limpiar()
            CargarDatos()
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

            ToastNotification.Show(Me, "El registro no se pudo completar".ToUpper,
                                              img, 5000,
                                              eToastGlowColor.Green,
                                              eToastPosition.TopCenter)
        End If
        Return res
    End Function

    Private Sub Guardar()
        If tbId.Text = "" Then
            'GuardarNuevo()
        Else

        End If
    End Sub

    Private Sub JGrM_Buscador_SelectionChanged(sender As Object, e As EventArgs) Handles JGrM_Buscador.SelectionChanged
        If (JGrM_Buscador.RowCount >= 0 And JGrM_Buscador.Row >= 0) Then
            _PMOMostrarRegistro(JGrM_Buscador.Row)
        End If
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        Limpiar()
        Habilitar()
    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click

    End Sub


End Class