Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports Janus.Windows.GridEX
Public Class F1_TotalCana

    Dim _CodCliente, _CodInstitucion As Integer
    Dim _Nuevo As Boolean
    Dim _Dsencabezado As DataTable
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
        tbGestion.Enabled = True
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
        listEstCeldas.Add(New Modelo.Celda("total", True, "Total", 250, "0.00"))


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
            If .GetValue("estado").ToString = "1" Then
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
        With JGrM_Buscador.RootTable.Columns("numi")
            .Caption = "Codigo".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        End With
        With JGrM_Buscador.RootTable.Columns("codCan")
            .Caption = "Cod. Can.".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        End With
        With JGrM_Buscador.RootTable.Columns("ydrazonsocial")
            .Caption = "Nombre".ToUpper
            .Width = 150
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        End With
        With JGrM_Buscador.RootTable.Columns("fecha")
            .Caption = "Fecha".ToUpper
            .Width = 250
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        End With
        With JGrM_Buscador.RootTable.Columns("gestion")
            .Caption = "Gestion".ToUpper
            .Width = 250
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        End With
        With JGrM_Buscador.RootTable.Columns("total")
            .Caption = "Total".ToUpper
            .Width = 250
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        End With
        With JGrM_Buscador.RootTable.Columns("ydcod")
            .Caption = "Total".ToUpper
            .Width = 150
            .Visible = False
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        End With
        With JGrM_Buscador.RootTable.Columns("codIns")
            .Caption = "Total".ToUpper
            .Width = 150
            .Visible = False
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        End With
        With JGrM_Buscador.RootTable.Columns("codInst")
            .Caption = "Total".ToUpper
            .Width = 150
            .Visible = False
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        End With
        With JGrM_Buscador.RootTable.Columns("nomInst")
            .Caption = "Total".ToUpper
            .Width = 150
            .Visible = False
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        End With

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
        If txtEstado.Text = "VIGENTE" Then
            btnEliminar.Enabled = True
            btnModificar.Enabled = True
        Else
            btnEliminar.Enabled = False
            btnModificar.Enabled = False
        End If
    End Sub

    Private Sub Guardar()
        Dim res As Boolean
        If _Nuevo = True Then
            res = L_fnRegistrarCañaComprometida(_CodCliente, _CodInstitucion, tbFecha.Value.ToString("dd-MM-yyyy"), CInt(tbGestion.Text), CDbl(TextBoxX4.Text))
            If res Then
                Dim img As Bitmap = New Bitmap(My.Resources.save, 50, 50)

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
        Else
            res = L_fnEditarCañaComprometida(tbId.Text, CDbl(TextBoxX4.Text))
            If res Then
                Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

                ToastNotification.Show(Me, "El registro no se pudo modificar".ToUpper,
                                                  img, 5000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter)

                Limpiar()
                CargarDatos()
            Else
                Dim img As Bitmap = New Bitmap(My.Resources.GRABACION_EXITOSA, 50, 50)

                ToastNotification.Show(Me, "El registro se modifico exitosamente".ToUpper,
                                                  img, 5000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter)
            End If
        End If
        Limpiar()
        CargarDatos()
    End Sub

    Private Sub JGrM_Buscador_SelectionChanged(sender As Object, e As EventArgs) Handles JGrM_Buscador.SelectionChanged
        If (JGrM_Buscador.RowCount >= 0 And JGrM_Buscador.Row >= 0) Then
            _PMOMostrarRegistro(JGrM_Buscador.Row)
        End If
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        Limpiar()
        Habilitar()
        _Nuevo = True
        txtEstado.Text = "VIGENTE"
        txtEstado.BackColor = Color.Green
    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        Guardar()
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        If txtEstado.Text = "VIGENTE" Then
            Habilitar()
            _Nuevo = False
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

            ToastNotification.Show(Me, "NO ES POSIBLE EDITAR ESTE REGISTRO".ToUpper,
                                                  img, 5000,
                                                  eToastGlowColor.Green,
                                                  eToastPosition.TopCenter)
        End If

    End Sub

    Private Sub ButtonX3_Click(sender As Object, e As EventArgs) Handles ButtonX3.Click
        _Dsencabezado = New DataTable
        _Dsencabezado = L_CanaComprometida()
        Dim ef = New Efecto


        Dim row As DataRow = _Dsencabezado.Rows(0) ' Segunda fila (índice 1)
        Dim gestionNueva As Integer = (Convert.ToInt32(row("gestion")) + 1)
        ef.tipo = 2
        ef.Context = "GENERAR DATOS PARA LA SIGUIENTE GESTIÓN ".ToUpper
        ef.Header = "Existen datos en la gestión " & "<b>" & row("Gestion").ToString() & "</b> " & "¿Esta seguro de generar datos para la gestión  " & "<b>" & gestionNueva & "</b> " & "?"
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            generarSiguienteGestion(gestionNueva)
            Dim img As Bitmap = New Bitmap(My.Resources.save, 50, 50)

            ToastNotification.Show(Me, "El registro se completo exitosamente".ToUpper,
                                              img, 5000,
                                              eToastGlowColor.Green,
                                              eToastPosition.TopCenter)

            IniciarTodo()
        Else

        End If
    End Sub
    Public Function _ValidarCampos() As Boolean
        Try
            If (tbCanero.Text = String.Empty) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor seleccione al cañero.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                tbCanero.Focus()
                Return False
            End If

            If (tbInstitucion.Text = String.Empty) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor seleccione a la institucion.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                tbInstitucion.Focus()
                Return False
            End If



            'Validar datos de factura
            If (tbGestion.Text = String.Empty) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor ingrese la gestion.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                tbGestion.Focus()
                Return False
            End If

            If (TextBoxX4.Text = String.Empty) Then
                Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor ingrese el cupo.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                TextBoxX4.Focus()
                Return False
            End If


            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function
    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        If _ValidarCampos() = False Then
            Exit Sub
        End If
        Guardar()
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        If btnGrabar.Enabled = True Then
            InHabilitar()
            IniciarTodo()
        Else
            Me.Close()
        End If
    End Sub




End Class