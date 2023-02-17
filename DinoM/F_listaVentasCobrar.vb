Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports System.IO
Imports Janus.Windows.GridEX
Public Class F_listaVentasCobrar

    Dim _Inter As Integer = 0
    Private Sub JGrM_Buscador_DoubleClick(sender As Object, e As EventArgs)
        MessageBox.Show("doble click")
    End Sub
    Private Sub _prIniciarTodo()
        _PMIniciarTodo()
        Me.Text = "V E N T A S   S I N   C O B R A R"


        GroupPanelBuscador.Style.BackColor = Color.FromArgb(13, 71, 161)
        GroupPanelBuscador.Style.BackColor2 = Color.FromArgb(13, 71, 161)
        GroupPanelBuscador.Style.TextColor = Color.White
        Dim blah As Bitmap = My.Resources.checked
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico

        btnModificar.Enabled = True
        btnEliminar.Enabled = True
    End Sub
    Private Sub F_listaVentasCobrar_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        _prIniciarTodo()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        _Inter = _Inter + 1
        If _Inter = 1 Then
            Me.WindowState = FormWindowState.Normal

        Else
            Me.Opacity = 100
            Timer1.Enabled = False
        End If
    End Sub


    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelo.Celda)
        Dim listEstCeldas As New List(Of Modelo.Celda)


        listEstCeldas.Add(New Modelo.Celda("tanumi", True, "CODIGO", 90))
        listEstCeldas.Add(New Modelo.Celda("taalm", True, "ALMACEN", 90))

        listEstCeldas.Add(New Modelo.Celda("tafdoc", True, "FECHA", 100))
        listEstCeldas.Add(New Modelo.Celda("taNrocaja", False, "", 350))

        listEstCeldas.Add(New Modelo.Celda("nomInst", True, "INSTITUCIÓN", 250))
        listEstCeldas.Add(New Modelo.Celda("taclpr", False, "COD.", 150))
        listEstCeldas.Add(New Modelo.Celda("yddesc", True, "CAÑERO", 250,))
        listEstCeldas.Add(New Modelo.Celda("tatotal", True, "TOTAL", 90, "0.00"))

        Return listEstCeldas

    End Function
    Public Overrides Function _PMOGetTablaBuscador() As DataTable
        Dim dtBuscador As DataTable = L_ListaVentasCobrar()
        Return dtBuscador
    End Function
    Private _canero As String
    Private _institucion As String
    Private _codcanero As String
    Private _codinstitucion As String
    Private _total As String
    Private _codigo As String
    Private _almacen As String
    Private _fecha As String
    Public ReadOnly Property Canero As String
        Get
            Return _canero
        End Get
    End Property
    Public ReadOnly Property Institucion As String
        Get
            Return _institucion
        End Get
    End Property

    Public ReadOnly Property Codinstitucion As String
        Get
            Return _codinstitucion
        End Get

    End Property

    Public ReadOnly Property Codcanero As String
        Get
            Return _codcanero
        End Get

    End Property

    Public ReadOnly Property Total As String
        Get
            Return _total
        End Get
    End Property

    Public ReadOnly Property Codigo As String
        Get
            Return _codigo
        End Get

    End Property

    Public ReadOnly Property Almacen As String
        Get
            Return _almacen
        End Get

    End Property

    Public ReadOnly Property Fecha As String
        Get
            Return _fecha
        End Get

    End Property

    Private Sub JGrM_Buscador_DoubleClick_1(sender As Object, e As EventArgs) Handles JGrM_Buscador.DoubleClick

        Dim frm As New F1_AsignacionCuentas()
        If JGrM_Buscador.Row >= 0 Then
            _PMostrarRegistro(JGrM_Buscador.Row)

        End If

        Me.Close()
        frm.tbCodIng2.Text = Codigo
        'frm.ShowDialog()

        'frm.Hide()


    End Sub



    Private Sub _PMostrarRegistro(_N As Integer)
        Dim dt As DataTable = CType(JGrM_Buscador.DataSource, DataTable)
        If (IsNothing(CType(JGrM_Buscador.DataSource, DataTable))) Then
            Return
        End If
        With JGrM_Buscador
            _codigo = .GetValue("tanumi").ToString
            _almacen = .GetValue("taalm").ToString
            _fecha = .GetValue("tafdoc").ToString
            _codinstitucion = .GetValue("taNrocaja").ToString
            _canero = .GetValue("yddesc").ToString
            _institucion = .GetValue("nomInst").ToString
            _codcanero = .GetValue("taclpr").ToString
            _total = .GetValue("tatotal").ToString

        End With


    End Sub
End Class