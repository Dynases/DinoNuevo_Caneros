﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Pr_KardexProductos
    Inherits Modelo.ModeloR0

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Pr_KardexProductos))
        Dim cbCasas_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim cbGrupos_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim cbAlmacen_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.tbFechaI = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.tbFechaF = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.checkGeneral = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.checkDetallado = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.CheckUnProvedor = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.CheckTodoslinea = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.ChechTodosCasa = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.CheckUnaCasa = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.cbCasas = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.cbGrupos = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CheckGeneralLote = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.cbAlmacen = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.SuperTabPrincipal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuperTabPrincipal.SuspendLayout()
        Me.SuperTabControlPanelRegistro.SuspendLayout()
        Me.PanelSuperior.SuspendLayout()
        Me.PanelInferior.SuspendLayout()
        CType(Me.BubbleBarUsuario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelToolBar1.SuspendLayout()
        Me.PanelPrincipal.SuspendLayout()
        Me.PanelUsuario.SuspendLayout()
        Me.MPanelUserAct.SuspendLayout()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MGPFiltros.SuspendLayout()
        Me.PanelIzq.SuspendLayout()
        CType(Me.MPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbFechaI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbFechaF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.cbCasas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbGrupos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.cbAlmacen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SuperTabPrincipal
        '
        '
        '
        '
        '
        '
        '
        Me.SuperTabPrincipal.ControlBox.CloseBox.Name = ""
        '
        '
        '
        Me.SuperTabPrincipal.ControlBox.MenuBox.Name = ""
        Me.SuperTabPrincipal.ControlBox.Name = ""
        Me.SuperTabPrincipal.ControlBox.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.SuperTabPrincipal.ControlBox.MenuBox, Me.SuperTabPrincipal.ControlBox.CloseBox})
        Me.SuperTabPrincipal.Size = New System.Drawing.Size(1362, 741)
        Me.SuperTabPrincipal.Controls.SetChildIndex(Me.SuperTabControlPanelBuscador, 0)
        Me.SuperTabPrincipal.Controls.SetChildIndex(Me.SuperTabControlPanelRegistro, 0)
        '
        'SuperTabControlPanelBuscador
        '
        Me.SuperTabControlPanelBuscador.Margin = New System.Windows.Forms.Padding(4)
        Me.SuperTabControlPanelBuscador.Size = New System.Drawing.Size(858, 561)
        '
        'SuperTabControlPanelRegistro
        '
        Me.SuperTabControlPanelRegistro.Margin = New System.Windows.Forms.Padding(4)
        Me.SuperTabControlPanelRegistro.Size = New System.Drawing.Size(1330, 741)
        Me.SuperTabControlPanelRegistro.Controls.SetChildIndex(Me.PanelInferior, 0)
        Me.SuperTabControlPanelRegistro.Controls.SetChildIndex(Me.PanelIzq, 0)
        Me.SuperTabControlPanelRegistro.Controls.SetChildIndex(Me.PanelPrincipal, 0)
        '
        'PanelSuperior
        '
        Me.PanelSuperior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelSuperior.Style.BackColor1.Color = System.Drawing.Color.Yellow
        Me.PanelSuperior.Style.BackColor2.Color = System.Drawing.Color.Khaki
        Me.PanelSuperior.Style.BackgroundImage = CType(resources.GetObject("PanelSuperior.Style.BackgroundImage"), System.Drawing.Image)
        Me.PanelSuperior.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelSuperior.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelSuperior.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelSuperior.Style.GradientAngle = 90
        Me.PanelSuperior.StyleMouseDown.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(152, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.PanelSuperior.StyleMouseDown.BorderColor.Color = System.Drawing.SystemColors.Highlight
        Me.PanelSuperior.StyleMouseDown.ForeColor.Color = System.Drawing.Color.FromArgb(CType(CType(73, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(73, Byte), Integer))
        Me.PanelSuperior.StyleMouseOver.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.PanelSuperior.StyleMouseOver.BorderColor.Color = System.Drawing.SystemColors.Highlight
        Me.PanelSuperior.StyleMouseOver.ForeColor.Color = System.Drawing.SystemColors.ControlText
        '
        'PanelInferior
        '
        Me.PanelInferior.Location = New System.Drawing.Point(0, 705)
        Me.PanelInferior.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelInferior.Size = New System.Drawing.Size(1330, 36)
        Me.PanelInferior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelInferior.Style.BackColor1.Color = System.Drawing.Color.Gold
        Me.PanelInferior.Style.BackColor2.Color = System.Drawing.Color.Gold
        Me.PanelInferior.Style.BackgroundImage = CType(resources.GetObject("PanelInferior.Style.BackgroundImage"), System.Drawing.Image)
        Me.PanelInferior.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelInferior.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelInferior.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelInferior.Style.GradientAngle = 90
        '
        'BubbleBarUsuario
        '
        '
        '
        '
        Me.BubbleBarUsuario.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BackColor = System.Drawing.Color.Transparent
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderBottomWidth = 1
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderLeftWidth = 1
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderRightWidth = 1
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderTopWidth = 1
        Me.BubbleBarUsuario.ButtonBackAreaStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.BubbleBarUsuario.ButtonBackAreaStyle.PaddingBottom = 3
        Me.BubbleBarUsuario.ButtonBackAreaStyle.PaddingLeft = 3
        Me.BubbleBarUsuario.ButtonBackAreaStyle.PaddingRight = 3
        Me.BubbleBarUsuario.ButtonBackAreaStyle.PaddingTop = 3
        Me.BubbleBarUsuario.MouseOverTabColors.BorderColor = System.Drawing.SystemColors.Highlight
        Me.BubbleBarUsuario.SelectedTabColors.BorderColor = System.Drawing.Color.Black
        '
        'btnSalir
        '
        '
        'btnGenerar
        '
        '
        'PanelPrincipal
        '
        Me.PanelPrincipal.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelPrincipal.Size = New System.Drawing.Size(967, 705)
        '
        'MPanelUserAct
        '
        Me.MPanelUserAct.Location = New System.Drawing.Point(1130, 0)
        Me.MPanelUserAct.Margin = New System.Windows.Forms.Padding(4)
        '
        'MReportViewer
        '
        Me.MReportViewer.Margin = New System.Windows.Forms.Padding(4)
        Me.MReportViewer.Size = New System.Drawing.Size(967, 705)
        Me.MReportViewer.ToolPanelWidth = 200
        '
        'MGPFiltros
        '
        Me.MGPFiltros.Controls.Add(Me.GroupBox2)
        Me.MGPFiltros.Size = New System.Drawing.Size(363, 633)
        '
        '
        '
        Me.MGPFiltros.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.MGPFiltros.Style.BackColorGradientAngle = 90
        Me.MGPFiltros.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.MGPFiltros.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.MGPFiltros.Style.BorderBottomWidth = 1
        Me.MGPFiltros.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.MGPFiltros.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.MGPFiltros.Style.BorderLeftWidth = 1
        Me.MGPFiltros.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.MGPFiltros.Style.BorderRightWidth = 1
        Me.MGPFiltros.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.MGPFiltros.Style.BorderTopWidth = 1
        Me.MGPFiltros.Style.CornerDiameter = 4
        Me.MGPFiltros.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.MGPFiltros.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.MGPFiltros.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.MGPFiltros.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.MGPFiltros.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.MGPFiltros.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        'PanelIzq
        '
        Me.PanelIzq.Size = New System.Drawing.Size(363, 705)
        Me.PanelIzq.Controls.SetChildIndex(Me.PanelSuperior, 0)
        Me.PanelIzq.Controls.SetChildIndex(Me.MGPFiltros, 0)
        '
        'tbFechaI
        '
        '
        '
        '
        Me.tbFechaI.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbFechaI.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaI.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown
        Me.tbFechaI.ButtonDropDown.Visible = True
        Me.tbFechaI.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbFechaI.IsPopupCalendarOpen = False
        Me.tbFechaI.Location = New System.Drawing.Point(145, 54)
        '
        '
        '
        '
        '
        '
        Me.tbFechaI.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaI.MonthCalendar.CalendarDimensions = New System.Drawing.Size(1, 1)
        Me.tbFechaI.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.tbFechaI.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.tbFechaI.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.tbFechaI.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.tbFechaI.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.tbFechaI.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.tbFechaI.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.tbFechaI.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaI.MonthCalendar.DisplayMonth = New Date(2017, 2, 1, 0, 0, 0, 0)
        Me.tbFechaI.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.tbFechaI.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.tbFechaI.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.tbFechaI.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.tbFechaI.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaI.MonthCalendar.TodayButtonVisible = True
        Me.tbFechaI.Name = "tbFechaI"
        Me.tbFechaI.Size = New System.Drawing.Size(120, 22)
        Me.tbFechaI.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.tbFechaI.TabIndex = 232
        '
        'LabelX4
        '
        Me.LabelX4.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX4.Location = New System.Drawing.Point(66, 53)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX4.Size = New System.Drawing.Size(74, 23)
        Me.LabelX4.TabIndex = 233
        Me.LabelX4.Text = "Fecha Del:"
        '
        'tbFechaF
        '
        '
        '
        '
        Me.tbFechaF.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbFechaF.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaF.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown
        Me.tbFechaF.ButtonDropDown.Visible = True
        Me.tbFechaF.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbFechaF.IsPopupCalendarOpen = False
        Me.tbFechaF.Location = New System.Drawing.Point(145, 85)
        '
        '
        '
        '
        '
        '
        Me.tbFechaF.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaF.MonthCalendar.CalendarDimensions = New System.Drawing.Size(1, 1)
        Me.tbFechaF.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.tbFechaF.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.tbFechaF.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.tbFechaF.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.tbFechaF.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.tbFechaF.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.tbFechaF.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.tbFechaF.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaF.MonthCalendar.DisplayMonth = New Date(2017, 2, 1, 0, 0, 0, 0)
        Me.tbFechaF.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.tbFechaF.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.tbFechaF.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.tbFechaF.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.tbFechaF.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaF.MonthCalendar.TodayButtonVisible = True
        Me.tbFechaF.Name = "tbFechaF"
        Me.tbFechaF.Size = New System.Drawing.Size(120, 22)
        Me.tbFechaF.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.tbFechaF.TabIndex = 234
        '
        'LabelX1
        '
        Me.LabelX1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX1.Location = New System.Drawing.Point(69, 85)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX1.Size = New System.Drawing.Size(52, 23)
        Me.LabelX1.TabIndex = 235
        Me.LabelX1.Text = "Al:"
        '
        'checkGeneral
        '
        Me.checkGeneral.AutoSize = True
        '
        '
        '
        Me.checkGeneral.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.checkGeneral.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.checkGeneral.CheckSignSize = New System.Drawing.Size(20, 20)
        Me.checkGeneral.EnableMarkup = False
        Me.checkGeneral.Location = New System.Drawing.Point(15, 6)
        Me.checkGeneral.Name = "checkGeneral"
        Me.checkGeneral.Size = New System.Drawing.Size(202, 22)
        Me.checkGeneral.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.checkGeneral.TabIndex = 1
        Me.checkGeneral.Text = "GENERAL POR PRODUCTO"
        '
        'checkDetallado
        '
        Me.checkDetallado.AutoSize = True
        '
        '
        '
        Me.checkDetallado.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.checkDetallado.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.checkDetallado.CheckSignSize = New System.Drawing.Size(20, 20)
        Me.checkDetallado.EnableMarkup = False
        Me.checkDetallado.Location = New System.Drawing.Point(15, 37)
        Me.checkDetallado.Name = "checkDetallado"
        Me.checkDetallado.Size = New System.Drawing.Size(111, 22)
        Me.checkDetallado.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.checkDetallado.TabIndex = 239
        Me.checkDetallado.Text = "DETALLADO"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.Panel5)
        Me.GroupBox2.Controls.Add(Me.Panel4)
        Me.GroupBox2.Controls.Add(Me.cbCasas)
        Me.GroupBox2.Controls.Add(Me.cbGrupos)
        Me.GroupBox2.Controls.Add(Me.LabelX2)
        Me.GroupBox2.Controls.Add(Me.LabelX3)
        Me.GroupBox2.Controls.Add(Me.Panel1)
        Me.GroupBox2.Controls.Add(Me.LabelX5)
        Me.GroupBox2.Controls.Add(Me.cbAlmacen)
        Me.GroupBox2.Controls.Add(Me.LabelX1)
        Me.GroupBox2.Controls.Add(Me.tbFechaF)
        Me.GroupBox2.Controls.Add(Me.LabelX4)
        Me.GroupBox2.Controls.Add(Me.tbFechaI)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(357, 610)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Datos"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.CheckUnProvedor)
        Me.Panel5.Controls.Add(Me.CheckTodoslinea)
        Me.Panel5.Font = New System.Drawing.Font("Georgia", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel5.Location = New System.Drawing.Point(259, 217)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(105, 25)
        Me.Panel5.TabIndex = 270
        '
        'CheckUnProvedor
        '
        '
        '
        '
        Me.CheckUnProvedor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CheckUnProvedor.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.CheckUnProvedor.Location = New System.Drawing.Point(0, 0)
        Me.CheckUnProvedor.Name = "CheckUnProvedor"
        Me.CheckUnProvedor.Size = New System.Drawing.Size(44, 23)
        Me.CheckUnProvedor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.CheckUnProvedor.TabIndex = 263
        Me.CheckUnProvedor.Text = "Una"
        '
        'CheckTodoslinea
        '
        '
        '
        '
        Me.CheckTodoslinea.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CheckTodoslinea.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.CheckTodoslinea.Checked = True
        Me.CheckTodoslinea.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckTodoslinea.CheckValue = "Y"
        Me.CheckTodoslinea.Location = New System.Drawing.Point(46, 0)
        Me.CheckTodoslinea.Name = "CheckTodoslinea"
        Me.CheckTodoslinea.Size = New System.Drawing.Size(55, 23)
        Me.CheckTodoslinea.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.CheckTodoslinea.TabIndex = 265
        Me.CheckTodoslinea.Text = "Todos"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.ChechTodosCasa)
        Me.Panel4.Controls.Add(Me.CheckUnaCasa)
        Me.Panel4.Font = New System.Drawing.Font("Georgia", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.Location = New System.Drawing.Point(259, 266)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(105, 25)
        Me.Panel4.TabIndex = 269
        '
        'ChechTodosCasa
        '
        '
        '
        '
        Me.ChechTodosCasa.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.ChechTodosCasa.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.ChechTodosCasa.Checked = True
        Me.ChechTodosCasa.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChechTodosCasa.CheckValue = "Y"
        Me.ChechTodosCasa.Location = New System.Drawing.Point(43, -1)
        Me.ChechTodosCasa.Name = "ChechTodosCasa"
        Me.ChechTodosCasa.Size = New System.Drawing.Size(55, 23)
        Me.ChechTodosCasa.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ChechTodosCasa.TabIndex = 266
        Me.ChechTodosCasa.Text = "Todos"
        '
        'CheckUnaCasa
        '
        '
        '
        '
        Me.CheckUnaCasa.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CheckUnaCasa.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.CheckUnaCasa.Location = New System.Drawing.Point(0, -1)
        Me.CheckUnaCasa.Name = "CheckUnaCasa"
        Me.CheckUnaCasa.Size = New System.Drawing.Size(44, 23)
        Me.CheckUnaCasa.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.CheckUnaCasa.TabIndex = 264
        Me.CheckUnaCasa.Text = "Una"
        '
        'cbCasas
        '
        Me.cbCasas.BackColor = System.Drawing.Color.White
        cbCasas_DesignTimeLayout.LayoutString = resources.GetString("cbCasas_DesignTimeLayout.LayoutString")
        Me.cbCasas.DesignTimeLayout = cbCasas_DesignTimeLayout
        Me.cbCasas.DisabledBackColor = System.Drawing.Color.White
        Me.cbCasas.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCasas.Location = New System.Drawing.Point(41, 265)
        Me.cbCasas.Name = "cbCasas"
        Me.cbCasas.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbCasas.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbCasas.SelectedIndex = -1
        Me.cbCasas.SelectedItem = Nothing
        Me.cbCasas.Size = New System.Drawing.Size(218, 22)
        Me.cbCasas.TabIndex = 267
        Me.cbCasas.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'cbGrupos
        '
        Me.cbGrupos.BackColor = System.Drawing.Color.White
        cbGrupos_DesignTimeLayout.LayoutString = resources.GetString("cbGrupos_DesignTimeLayout.LayoutString")
        Me.cbGrupos.DesignTimeLayout = cbGrupos_DesignTimeLayout
        Me.cbGrupos.DisabledBackColor = System.Drawing.Color.White
        Me.cbGrupos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbGrupos.Location = New System.Drawing.Point(41, 217)
        Me.cbGrupos.Name = "cbGrupos"
        Me.cbGrupos.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbGrupos.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbGrupos.SelectedIndex = -1
        Me.cbGrupos.SelectedItem = Nothing
        Me.cbGrupos.Size = New System.Drawing.Size(218, 22)
        Me.cbGrupos.TabIndex = 261
        Me.cbGrupos.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        Me.LabelX2.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX2.Location = New System.Drawing.Point(-1, 265)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX2.Size = New System.Drawing.Size(36, 16)
        Me.LabelX2.TabIndex = 268
        Me.LabelX2.Text = "Casa:"
        '
        'LabelX3
        '
        Me.LabelX3.AutoSize = True
        Me.LabelX3.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX3.Location = New System.Drawing.Point(-1, 217)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX3.Size = New System.Drawing.Size(42, 16)
        Me.LabelX3.TabIndex = 262
        Me.LabelX3.Text = "Linea:"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.checkDetallado)
        Me.Panel1.Controls.Add(Me.CheckGeneralLote)
        Me.Panel1.Controls.Add(Me.checkGeneral)
        Me.Panel1.Location = New System.Drawing.Point(29, 119)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(274, 93)
        Me.Panel1.TabIndex = 243
        '
        'CheckGeneralLote
        '
        Me.CheckGeneralLote.AutoSize = True
        '
        '
        '
        Me.CheckGeneralLote.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CheckGeneralLote.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.CheckGeneralLote.CheckSignSize = New System.Drawing.Size(20, 20)
        Me.CheckGeneralLote.EnableMarkup = False
        Me.CheckGeneralLote.Location = New System.Drawing.Point(15, 63)
        Me.CheckGeneralLote.Name = "CheckGeneralLote"
        Me.CheckGeneralLote.Size = New System.Drawing.Size(162, 22)
        Me.CheckGeneralLote.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.CheckGeneralLote.TabIndex = 242
        Me.CheckGeneralLote.Text = "GENERAL POR LOTE"
        '
        'LabelX5
        '
        Me.LabelX5.AutoSize = True
        Me.LabelX5.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX5.Location = New System.Drawing.Point(41, 20)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX5.Size = New System.Drawing.Size(62, 16)
        Me.LabelX5.TabIndex = 241
        Me.LabelX5.Text = "Deposito:"
        '
        'cbAlmacen
        '
        cbAlmacen_DesignTimeLayout.LayoutString = resources.GetString("cbAlmacen_DesignTimeLayout.LayoutString")
        Me.cbAlmacen.DesignTimeLayout = cbAlmacen_DesignTimeLayout
        Me.cbAlmacen.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAlmacen.Location = New System.Drawing.Point(119, 20)
        Me.cbAlmacen.Name = "cbAlmacen"
        Me.cbAlmacen.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbAlmacen.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbAlmacen.SelectedIndex = -1
        Me.cbAlmacen.SelectedItem = Nothing
        Me.cbAlmacen.Size = New System.Drawing.Size(222, 22)
        Me.cbAlmacen.TabIndex = 240
        Me.cbAlmacen.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 500
        '
        'Pr_KardexProductos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1362, 741)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Pr_KardexProductos"
        Me.Text = "KARDEX PRODUCTO"
        Me.Controls.SetChildIndex(Me.SuperTabPrincipal, 0)
        CType(Me.SuperTabPrincipal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SuperTabPrincipal.ResumeLayout(False)
        Me.SuperTabControlPanelRegistro.ResumeLayout(False)
        Me.PanelSuperior.ResumeLayout(False)
        Me.PanelInferior.ResumeLayout(False)
        CType(Me.BubbleBarUsuario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelToolBar1.ResumeLayout(False)
        Me.PanelPrincipal.ResumeLayout(False)
        Me.PanelUsuario.ResumeLayout(False)
        Me.PanelUsuario.PerformLayout()
        Me.MPanelUserAct.ResumeLayout(False)
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MGPFiltros.ResumeLayout(False)
        Me.PanelIzq.ResumeLayout(False)
        CType(Me.MPicture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbFechaI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbFechaF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        CType(Me.cbCasas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbGrupos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.cbAlmacen, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents checkDetallado As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents checkGeneral As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbFechaF As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbFechaI As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents cbAlmacen As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents CheckGeneralLote As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents Timer1 As Timer
    Friend WithEvents cbCasas As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents cbGrupos As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents CheckUnProvedor As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents ChechTodosCasa As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents CheckUnaCasa As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents CheckTodoslinea As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
End Class
