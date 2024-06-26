Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports Logica.AccesoLogica

Public Class Pr_OtroSurtidor
#Region "ATRIBUTOS"
    Dim _Dsencabezado As DataSet
    Dim _CodCliente As Integer = 0
    Dim _Nuevo As Boolean
    Private _Pos As Integer
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Public _nameButton As String
    Dim NumiVendedor As Integer = 0
    Dim _CodEmpleado As Integer = 0
#End Region
    Public Sub iniciarcomponentes()
        tbInsCan.ReadOnly = True
        tbCod.ReadOnly = True
        tbNomCan.ReadOnly = True
        tbCodCan.ReadOnly = True
        CheckTodos.CheckValue = True
        CheckTodosAlmacen.CheckValue = True
        CheckTodosCan.CheckValue = True
        tbFechaI.Value = Date.Now
        tbFechaF.Value = Date.Now
        LabelX2.Visible = True
        tbCodCan.Visible = True
        tbNomCan.Visible = True
        CheckUnaCan.Visible = True
        CheckTodosCan.Visible = True
        _prCargarComboLibreria(tbAlmacen, 1, 8)
    End Sub
    Private Sub _prCargarComboLibreria(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo, cod1 As String, cod2 As String)
        Dim dt As New DataTable
        dt = L_prLibreriaClienteLGeneral(cod1, cod2)
        With mCombo
            .DropDownList.Columns.Clear()
            .DropDownList.Columns.Add("yccod3").Width = 70
            .DropDownList.Columns("yccod3").Caption = "COD"
            .DropDownList.Columns.Add("ycdes3").Width = 285
            .DropDownList.Columns("ycdes3").Caption = "DESCRIPCION"
            .ValueMember = "yccod3"
            .DisplayMember = "ycdes3"
            .DataSource = dt
            .Refresh()
        End With
    End Sub
    Public Sub _prInterpretarDatos(ByRef _dt As DataTable)
        _dt = L_prReporteOtrosSurtidores(IIf(CheckTodos.Checked = True, 0, tbCod.Text), IIf(CheckTodosCan.Checked = True, 0, tbCodCan.Text), tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), IIf(CheckTodosAlmacen.Checked = True, -1, tbAlmacen.Value))

        'If CheckUna.Checked = True And CheckTodosCan.Checked = True Then
        '    _dt = L_prReporteOtrosSurtidores(tbCod.Text, 0, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), tbAlmacen.Value)
        'End If
        'If CheckUna.Checked = True And CheckUnaCan.Checked = True Then
        '    _dt = L_prReporteOtrosSurtidores(tbCod.Text, tbCodCan.Text, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), tbAlmacen.Value)
        'End If
        'If CheckTodos.Checked = True Then
        '    _dt = L_prReporteOtrosSurtidores(0, 0, tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), tbAlmacen.Value)

        'End If
        'If CheckTodos.Checked = True And CheckTodosCan.Checked = True Then
    End Sub
    Private Function Validar() As Boolean
        If CheckUna.Checked = True And tbInsCan.Text = "" Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione una Institucion".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomLeft)
            Return True
        End If
        If CheckUna.Checked = True And CheckUnaCan.Checked = True And tbNomCan.Text = "" Then
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione un Cañero".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomLeft)
            Return True
        End If
        Return False
    End Function
    Private Sub _prCargarReporte()
        If Validar() Then
            Exit Sub
        End If
        Dim _dt As New DataTable
        _prInterpretarDatos(_dt)
        If (_dt.Rows.Count > 0) Then
            Dim objrep As New R_ReporteOtrosSurtidores
            objrep.SetDataSource(_dt)
            Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
            Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
            Dim Institucion As String = tbInsCan.Text
            Dim CodIns As String = tbCod.Text
            Dim CodCan As String = tbCodCan.Text
            Dim Canero As String = tbNomCan.Text
            Dim almacen As String = gs_userSucNom
            'objrep.SetParameterValue("CodCan", CodCan)
            'objrep.SetParameterValue("CodIns", CodIns)
            'objrep.SetParameterValue("Canero", Canero)
            'objrep.SetParameterValue("Institucion", Institucion)
            objrep.SetParameterValue("fechaI", fechaI)
            objrep.SetParameterValue("fechaF", fechaF)
            CrystalReportViewer1.ReportSource = objrep
            CrystalReportViewer1.Show()
            CrystalReportViewer1.BringToFront()
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.mensaje, 50, 50)
            ToastNotification.Show(Me, "No existen datos para mostrar".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomLeft)
        End If
    End Sub
    Private Sub Pr_ProductoRetiradoxCañero_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub ButtonX1_Click(sender As Object, e As EventArgs) Handles ButtonX1.Click
        Me.Close()
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

    Private Sub CheckUnaCan_CheckedChanged(sender As Object, e As EventArgs) Handles CheckUnaCan.CheckedChanged
        If CheckUnaCan.Checked = True Then
            If CheckTodosCan.Checked = True Then
                CheckTodosCan.Checked = False
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
                    tbCodCan.Text = Row.Cells("ydcod").Value
                    tbNomCan.Text = Row.Cells("ydrazonsocial").Value
                    'btnGenerar.Focus()
                End If

            End If

        End If
    End Sub

    Private Sub ButtonX2_Click(sender As Object, e As EventArgs) Handles ButtonX2.Click
        _prCargarReporte()
    End Sub

    Private Sub tbNomCan_TextChanged(sender As Object, e As EventArgs) Handles tbNomCan.TextChanged

    End Sub

    Private Sub CheckTodosAlmacen_CheckedChanged(sender As Object, e As EventArgs) Handles CheckTodosAlmacen.CheckedChanged
        If CheckTodosAlmacen.Checked = True Then
            If CheckUnaALmacen.Checked = True Then
                CheckUnaALmacen.Checked = False
                'tbCodCan.Text = ""
                'tbNomCan.Text = ""
            End If
        End If


    End Sub

    Private Sub CheckUnaALmacen_CheckedChanged(sender As Object, e As EventArgs) Handles CheckUnaALmacen.CheckedChanged
        If CheckUnaALmacen.Checked = True Then
            If CheckTodosAlmacen.Checked = True Then
                CheckTodosAlmacen.Checked = False
            End If
        End If


    End Sub
End Class