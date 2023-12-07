<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Pr_SAldosPorAlmacenLinea
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Pr_SAldosPorAlmacenLinea))
        Dim cbCasas_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim cbGrupos_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim cbAlmacen_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lbCtrlEnter = New DevComponents.DotNetBar.LabelX()
        Me.tbProveedor = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.CheckTodoslinea = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.CheckUnProvedor = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.ChechTodosCasa = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.CheckUnaCasa = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.cbCasas = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.CheckTodos = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.CheckMayorCero = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CheckTodosAlmacen = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.checkUnaAlmacen = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.checkTodosGrupos = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.checkUnaGrupo = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.cbGrupos = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.cbAlmacen = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.ButtonItem1 = New DevComponents.DotNetBar.ButtonItem()
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
        Me.GroupBox2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.cbCasas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.cbGrupos, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.PanelSuperior.Size = New System.Drawing.Size(400, 72)
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
        Me.btnGenerar.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.ButtonItem1})
        '
        'PanelPrincipal
        '
        Me.PanelPrincipal.Location = New System.Drawing.Point(400, 0)
        Me.PanelPrincipal.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelPrincipal.Size = New System.Drawing.Size(930, 705)
        '
        'MPanelUserAct
        '
        Me.MPanelUserAct.Location = New System.Drawing.Point(1130, 0)
        Me.MPanelUserAct.Margin = New System.Windows.Forms.Padding(4)
        '
        'MReportViewer
        '
        Me.MReportViewer.Margin = New System.Windows.Forms.Padding(4)
        Me.MReportViewer.Size = New System.Drawing.Size(930, 705)
        Me.MReportViewer.ToolPanelWidth = 200
        '
        'MGPFiltros
        '
        Me.MGPFiltros.Controls.Add(Me.GroupBox2)
        Me.MGPFiltros.Size = New System.Drawing.Size(400, 633)
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
        Me.PanelIzq.Size = New System.Drawing.Size(400, 705)
        Me.PanelIzq.Controls.SetChildIndex(Me.PanelSuperior, 0)
        Me.PanelIzq.Controls.SetChildIndex(Me.MGPFiltros, 0)
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.lbCtrlEnter)
        Me.GroupBox2.Controls.Add(Me.CheckUnProvedor)
        Me.GroupBox2.Controls.Add(Me.tbProveedor)
        Me.GroupBox2.Controls.Add(Me.Panel5)
        Me.GroupBox2.Controls.Add(Me.LabelX5)
        Me.GroupBox2.Controls.Add(Me.Panel4)
        Me.GroupBox2.Controls.Add(Me.LabelX4)
        Me.GroupBox2.Controls.Add(Me.cbCasas)
        Me.GroupBox2.Controls.Add(Me.Panel3)
        Me.GroupBox2.Controls.Add(Me.Panel1)
        Me.GroupBox2.Controls.Add(Me.Panel2)
        Me.GroupBox2.Controls.Add(Me.LabelX2)
        Me.GroupBox2.Controls.Add(Me.LabelX1)
        Me.GroupBox2.Controls.Add(Me.cbGrupos)
        Me.GroupBox2.Controls.Add(Me.cbAlmacen)
        Me.GroupBox2.Controls.Add(Me.LabelX3)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(394, 610)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Datos"
        '
        'lbCtrlEnter
        '
        Me.lbCtrlEnter.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbCtrlEnter.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbCtrlEnter.Font = New System.Drawing.Font("Georgia", 7.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbCtrlEnter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.lbCtrlEnter.Location = New System.Drawing.Point(228, 82)
        Me.lbCtrlEnter.Name = "lbCtrlEnter"
        Me.lbCtrlEnter.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbCtrlEnter.Size = New System.Drawing.Size(60, 10)
        Me.lbCtrlEnter.TabIndex = 353
        Me.lbCtrlEnter.Text = "Ctrl+Enter"
        '
        'tbProveedor
        '
        '
        '
        '
        Me.tbProveedor.Border.Class = "TextBoxBorder"
        Me.tbProveedor.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbProveedor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbProveedor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbProveedor.Location = New System.Drawing.Point(70, 90)
        Me.tbProveedor.MaxLength = 15
        Me.tbProveedor.Name = "tbProveedor"
        Me.tbProveedor.PreventEnterBeep = True
        Me.tbProveedor.Size = New System.Drawing.Size(218, 21)
        Me.tbProveedor.TabIndex = 264
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.CheckTodoslinea)
        Me.Panel5.Font = New System.Drawing.Font("Georgia", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel5.Location = New System.Drawing.Point(290, 129)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(105, 25)
        Me.Panel5.TabIndex = 262
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
        Me.CheckTodoslinea.Location = New System.Drawing.Point(47, 1)
        Me.CheckTodoslinea.Name = "CheckTodoslinea"
        Me.CheckTodoslinea.Size = New System.Drawing.Size(55, 23)
        Me.CheckTodoslinea.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.CheckTodoslinea.TabIndex = 252
        Me.CheckTodoslinea.Text = "Todos"
        '
        'CheckUnProvedor
        '
        '
        '
        '
        Me.CheckUnProvedor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CheckUnProvedor.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.CheckUnProvedor.Location = New System.Drawing.Point(316, 218)
        Me.CheckUnProvedor.Name = "CheckUnProvedor"
        Me.CheckUnProvedor.Size = New System.Drawing.Size(44, 23)
        Me.CheckUnProvedor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.CheckUnProvedor.TabIndex = 251
        Me.CheckUnProvedor.Text = "Una"
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
        Me.LabelX5.Location = New System.Drawing.Point(0, 92)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX5.Size = New System.Drawing.Size(72, 16)
        Me.LabelX5.TabIndex = 261
        Me.LabelX5.Text = "Proveedor:"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.ChechTodosCasa)
        Me.Panel4.Controls.Add(Me.CheckUnaCasa)
        Me.Panel4.Font = New System.Drawing.Font("Georgia", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.Location = New System.Drawing.Point(289, 177)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(105, 25)
        Me.Panel4.TabIndex = 257
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
        Me.ChechTodosCasa.Location = New System.Drawing.Point(48, 1)
        Me.ChechTodosCasa.Name = "ChechTodosCasa"
        Me.ChechTodosCasa.Size = New System.Drawing.Size(55, 23)
        Me.ChechTodosCasa.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ChechTodosCasa.TabIndex = 252
        Me.ChechTodosCasa.Text = "Todos"
        '
        'CheckUnaCasa
        '
        '
        '
        '
        Me.CheckUnaCasa.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CheckUnaCasa.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.CheckUnaCasa.Location = New System.Drawing.Point(1, 1)
        Me.CheckUnaCasa.Name = "CheckUnaCasa"
        Me.CheckUnaCasa.Size = New System.Drawing.Size(44, 23)
        Me.CheckUnaCasa.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.CheckUnaCasa.TabIndex = 251
        Me.CheckUnaCasa.Text = "Una"
        '
        'LabelX4
        '
        Me.LabelX4.AutoSize = True
        Me.LabelX4.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX4.Location = New System.Drawing.Point(0, 177)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX4.Size = New System.Drawing.Size(36, 16)
        Me.LabelX4.TabIndex = 260
        Me.LabelX4.Text = "Casa:"
        '
        'cbCasas
        '
        Me.cbCasas.BackColor = System.Drawing.Color.White
        cbCasas_DesignTimeLayout.LayoutString = resources.GetString("cbCasas_DesignTimeLayout.LayoutString")
        Me.cbCasas.DesignTimeLayout = cbCasas_DesignTimeLayout
        Me.cbCasas.DisabledBackColor = System.Drawing.Color.White
        Me.cbCasas.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCasas.Location = New System.Drawing.Point(70, 177)
        Me.cbCasas.Name = "cbCasas"
        Me.cbCasas.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbCasas.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbCasas.SelectedIndex = -1
        Me.cbCasas.SelectedItem = Nothing
        Me.cbCasas.Size = New System.Drawing.Size(218, 22)
        Me.cbCasas.TabIndex = 259
        Me.cbCasas.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.CheckTodos)
        Me.Panel3.Controls.Add(Me.CheckMayorCero)
        Me.Panel3.Location = New System.Drawing.Point(110, 237)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(200, 50)
        Me.Panel3.TabIndex = 258
        '
        'CheckTodos
        '
        '
        '
        '
        Me.CheckTodos.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CheckTodos.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.CheckTodos.Checked = True
        Me.CheckTodos.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckTodos.CheckValue = "Y"
        Me.CheckTodos.Location = New System.Drawing.Point(117, 13)
        Me.CheckTodos.Name = "CheckTodos"
        Me.CheckTodos.Size = New System.Drawing.Size(80, 23)
        Me.CheckTodos.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.CheckTodos.TabIndex = 257
        Me.CheckTodos.Text = "Todos"
        '
        'CheckMayorCero
        '
        '
        '
        '
        Me.CheckMayorCero.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CheckMayorCero.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.CheckMayorCero.Location = New System.Drawing.Point(22, 13)
        Me.CheckMayorCero.Name = "CheckMayorCero"
        Me.CheckMayorCero.Size = New System.Drawing.Size(80, 23)
        Me.CheckMayorCero.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.CheckMayorCero.TabIndex = 257
        Me.CheckMayorCero.Text = "Mayor a 0"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.CheckTodosAlmacen)
        Me.Panel1.Controls.Add(Me.checkUnaAlmacen)
        Me.Panel1.Font = New System.Drawing.Font("Georgia", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(289, 42)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(105, 25)
        Me.Panel1.TabIndex = 255
        '
        'CheckTodosAlmacen
        '
        '
        '
        '
        Me.CheckTodosAlmacen.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CheckTodosAlmacen.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.CheckTodosAlmacen.Checked = True
        Me.CheckTodosAlmacen.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckTodosAlmacen.CheckValue = "Y"
        Me.CheckTodosAlmacen.Location = New System.Drawing.Point(49, 1)
        Me.CheckTodosAlmacen.Name = "CheckTodosAlmacen"
        Me.CheckTodosAlmacen.Size = New System.Drawing.Size(55, 23)
        Me.CheckTodosAlmacen.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.CheckTodosAlmacen.TabIndex = 252
        Me.CheckTodosAlmacen.Text = "Todos"
        '
        'checkUnaAlmacen
        '
        '
        '
        '
        Me.checkUnaAlmacen.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.checkUnaAlmacen.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.checkUnaAlmacen.Location = New System.Drawing.Point(3, 1)
        Me.checkUnaAlmacen.Name = "checkUnaAlmacen"
        Me.checkUnaAlmacen.Size = New System.Drawing.Size(44, 23)
        Me.checkUnaAlmacen.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.checkUnaAlmacen.TabIndex = 251
        Me.checkUnaAlmacen.Text = "Una"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.checkTodosGrupos)
        Me.Panel2.Controls.Add(Me.checkUnaGrupo)
        Me.Panel2.Font = New System.Drawing.Font("Georgia", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(289, 86)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(105, 25)
        Me.Panel2.TabIndex = 256
        '
        'checkTodosGrupos
        '
        '
        '
        '
        Me.checkTodosGrupos.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.checkTodosGrupos.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.checkTodosGrupos.Checked = True
        Me.checkTodosGrupos.CheckState = System.Windows.Forms.CheckState.Checked
        Me.checkTodosGrupos.CheckValue = "Y"
        Me.checkTodosGrupos.Location = New System.Drawing.Point(47, 1)
        Me.checkTodosGrupos.Name = "checkTodosGrupos"
        Me.checkTodosGrupos.Size = New System.Drawing.Size(55, 23)
        Me.checkTodosGrupos.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.checkTodosGrupos.TabIndex = 252
        Me.checkTodosGrupos.Text = "Todos"
        '
        'checkUnaGrupo
        '
        '
        '
        '
        Me.checkUnaGrupo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.checkUnaGrupo.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.checkUnaGrupo.Location = New System.Drawing.Point(1, 1)
        Me.checkUnaGrupo.Name = "checkUnaGrupo"
        Me.checkUnaGrupo.Size = New System.Drawing.Size(44, 23)
        Me.checkUnaGrupo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.checkUnaGrupo.TabIndex = 251
        Me.checkUnaGrupo.Text = "Una"
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
        Me.LabelX2.Location = New System.Drawing.Point(0, 250)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX2.Size = New System.Drawing.Size(41, 16)
        Me.LabelX2.TabIndex = 250
        Me.LabelX2.Text = "Stock:"
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        Me.LabelX1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX1.Location = New System.Drawing.Point(0, 129)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX1.Size = New System.Drawing.Size(42, 16)
        Me.LabelX1.TabIndex = 250
        Me.LabelX1.Text = "Linea:"
        '
        'cbGrupos
        '
        Me.cbGrupos.BackColor = System.Drawing.Color.White
        cbGrupos_DesignTimeLayout.LayoutString = resources.GetString("cbGrupos_DesignTimeLayout.LayoutString")
        Me.cbGrupos.DesignTimeLayout = cbGrupos_DesignTimeLayout
        Me.cbGrupos.DisabledBackColor = System.Drawing.Color.White
        Me.cbGrupos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbGrupos.Location = New System.Drawing.Point(70, 129)
        Me.cbGrupos.Name = "cbGrupos"
        Me.cbGrupos.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbGrupos.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbGrupos.SelectedIndex = -1
        Me.cbGrupos.SelectedItem = Nothing
        Me.cbGrupos.Size = New System.Drawing.Size(218, 22)
        Me.cbGrupos.TabIndex = 249
        Me.cbGrupos.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'cbAlmacen
        '
        Me.cbAlmacen.BackColor = System.Drawing.Color.White
        cbAlmacen_DesignTimeLayout.LayoutString = resources.GetString("cbAlmacen_DesignTimeLayout.LayoutString")
        Me.cbAlmacen.DesignTimeLayout = cbAlmacen_DesignTimeLayout
        Me.cbAlmacen.DisabledBackColor = System.Drawing.Color.White
        Me.cbAlmacen.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAlmacen.Location = New System.Drawing.Point(70, 45)
        Me.cbAlmacen.Name = "cbAlmacen"
        Me.cbAlmacen.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbAlmacen.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbAlmacen.SelectedIndex = -1
        Me.cbAlmacen.SelectedItem = Nothing
        Me.cbAlmacen.Size = New System.Drawing.Size(218, 22)
        Me.cbAlmacen.TabIndex = 247
        Me.cbAlmacen.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
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
        Me.LabelX3.Location = New System.Drawing.Point(0, 47)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX3.Size = New System.Drawing.Size(61, 16)
        Me.LabelX3.TabIndex = 241
        Me.LabelX3.Text = "Almacen:"
        '
        'ButtonItem1
        '
        Me.ButtonItem1.GlobalItem = False
        Me.ButtonItem1.Name = "ButtonItem1"
        Me.ButtonItem1.Text = "ButtonItem1"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 500
        '
        'Pr_SAldosPorAlmacenLinea
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1362, 741)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Pr_SAldosPorAlmacenLinea"
        Me.Text = "SALDOS DE PRODUCTOS"
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
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        CType(Me.cbCasas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.cbGrupos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbAlmacen, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cbAlmacen As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents cbGrupos As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents checkTodosGrupos As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents checkUnaGrupo As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents CheckTodosAlmacen As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents checkUnaAlmacen As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents CheckMayorCero As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents CheckTodos As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents Panel3 As Panel
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents ButtonItem1 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Panel4 As Panel
    Friend WithEvents ChechTodosCasa As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents CheckUnaCasa As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cbCasas As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents Panel5 As Panel
    Friend WithEvents CheckTodoslinea As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents CheckUnProvedor As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbProveedor As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lbCtrlEnter As DevComponents.DotNetBar.LabelX
End Class
