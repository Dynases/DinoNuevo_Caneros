<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F0_REGISTROSERVICIO
    Inherits Modelo.ModeloF0
    'Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F0_REGISTROSERVICIO))
        Dim tbFinan_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim tbMoneda_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim cbTipoCambio_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim cbleyendas_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.tbcod = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX14 = New DevComponents.DotNetBar.LabelX()
        Me.tbObs = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.tbFinan = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.LabelX9 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX8 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX7 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.tbInteres = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.tbTotal = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.codFin = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.tbCanero = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.codCan = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.tbInst = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.codIns = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.codMon = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.tbfecha = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.grPrestamo = New Janus.Windows.GridEX.GridEX()
        Me.CachedKardexClienteRes1 = New DinoM.CachedKardexClienteRes()
        Me.tbMoneda = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.cbTipoCambio = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.LabelX13 = New DevComponents.DotNetBar.LabelX()
        Me.cbleyendas = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.PanelSuperior.SuspendLayout()
        Me.PanelInferior.SuspendLayout()
        CType(Me.BubbleBarUsuario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelToolBar1.SuspendLayout()
        Me.PanelToolBar2.SuspendLayout()
        Me.PanelPrincipal.SuspendLayout()
        Me.PanelUsuario.SuspendLayout()
        Me.PanelNavegacion.SuspendLayout()
        Me.MPanelUserAct.SuspendLayout()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelContent.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.MSuperTabControlPanel1.SuspendLayout()
        CType(Me.MSuperTabControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MSuperTabControl.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupPanel1.SuspendLayout()
        CType(Me.tbFinan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbfecha, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grPrestamo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbMoneda, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbTipoCambio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbleyendas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelSuperior
        '
        Me.PanelSuperior.Size = New System.Drawing.Size(740, 72)
        Me.PanelSuperior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelSuperior.Style.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelSuperior.Style.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelSuperior.Style.BackgroundImage = CType(resources.GetObject("PanelSuperior.Style.BackgroundImage"), System.Drawing.Image)
        Me.PanelSuperior.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelSuperior.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelSuperior.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelSuperior.Style.GradientAngle = 90
        Me.PanelSuperior.StyleMouseDown.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelSuperior.StyleMouseDown.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelSuperior.StyleMouseOver.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelSuperior.StyleMouseOver.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.PanelSuperior.StyleMouseOver.BackgroundImage = CType(resources.GetObject("PanelSuperior.StyleMouseOver.BackgroundImage"), System.Drawing.Image)
        '
        'PanelInferior
        '
        Me.PanelInferior.Location = New System.Drawing.Point(0, 372)
        Me.PanelInferior.Size = New System.Drawing.Size(740, 39)
        Me.PanelInferior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelInferior.Style.BackColor1.Color = System.Drawing.Color.Transparent
        Me.PanelInferior.Style.BackColor2.Color = System.Drawing.Color.Transparent
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
        'TxtNombreUsu
        '
        Me.TxtNombreUsu.ReadOnly = True
        Me.TxtNombreUsu.Text = "DEFAULT"
        '
        'btnSalir
        '
        '
        'btnGrabar
        '
        '
        'btnModificar
        '
        '
        'btnNuevo
        '
        '
        'PanelToolBar2
        '
        Me.PanelToolBar2.Location = New System.Drawing.Point(660, 0)
        '
        'PanelPrincipal
        '
        Me.PanelPrincipal.Size = New System.Drawing.Size(740, 411)
        Me.PanelPrincipal.Controls.SetChildIndex(Me.PanelInferior, 0)
        Me.PanelPrincipal.Controls.SetChildIndex(Me.PanelUsuario, 0)
        Me.PanelPrincipal.Controls.SetChildIndex(Me.PanelSuperior, 0)
        Me.PanelPrincipal.Controls.SetChildIndex(Me.Panel1, 0)
        '
        'btnImprimir
        '
        '
        'btnUltimo
        '
        '
        'btnSiguiente
        '
        '
        'btnAnterior
        '
        '
        'btnPrimero
        '
        '
        'MPanelUserAct
        '
        Me.MPanelUserAct.Location = New System.Drawing.Point(540, 0)
        '
        'MRlAccion
        '
        '
        '
        '
        Me.MRlAccion.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.MRlAccion.ForeColor = System.Drawing.Color.Transparent
        Me.MRlAccion.Size = New System.Drawing.Size(284, 72)
        '
        'PanelContent
        '
        Me.PanelContent.Controls.Add(Me.GroupPanel1)
        Me.PanelContent.Size = New System.Drawing.Size(707, 300)
        '
        'Panel1
        '
        Me.Panel1.Size = New System.Drawing.Size(740, 300)
        '
        'MSuperTabControlPanel1
        '
        Me.MSuperTabControlPanel1.Size = New System.Drawing.Size(707, 300)
        '
        'MSuperTabControl
        '
        '
        '
        '
        '
        '
        '
        Me.MSuperTabControl.ControlBox.CloseBox.Name = ""
        '
        '
        '
        Me.MSuperTabControl.ControlBox.MenuBox.Name = ""
        Me.MSuperTabControl.ControlBox.Name = ""
        Me.MSuperTabControl.ControlBox.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.MSuperTabControl.ControlBox.MenuBox, Me.MSuperTabControl.ControlBox.CloseBox})
        Me.MSuperTabControl.Size = New System.Drawing.Size(740, 300)
        Me.MSuperTabControl.Controls.SetChildIndex(Me.MSuperTabControlPanel1, 0)
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(467, 0)
        '
        'GroupPanel1
        '
        Me.GroupPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupPanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel1.Controls.Add(Me.cbleyendas)
        Me.GroupPanel1.Controls.Add(Me.cbTipoCambio)
        Me.GroupPanel1.Controls.Add(Me.LabelX13)
        Me.GroupPanel1.Controls.Add(Me.tbcod)
        Me.GroupPanel1.Controls.Add(Me.LabelX14)
        Me.GroupPanel1.Controls.Add(Me.tbObs)
        Me.GroupPanel1.Controls.Add(Me.tbMoneda)
        Me.GroupPanel1.Controls.Add(Me.tbFinan)
        Me.GroupPanel1.Controls.Add(Me.LabelX9)
        Me.GroupPanel1.Controls.Add(Me.LabelX8)
        Me.GroupPanel1.Controls.Add(Me.LabelX7)
        Me.GroupPanel1.Controls.Add(Me.LabelX5)
        Me.GroupPanel1.Controls.Add(Me.LabelX4)
        Me.GroupPanel1.Controls.Add(Me.LabelX3)
        Me.GroupPanel1.Controls.Add(Me.LabelX2)
        Me.GroupPanel1.Controls.Add(Me.LabelX1)
        Me.GroupPanel1.Controls.Add(Me.tbInteres)
        Me.GroupPanel1.Controls.Add(Me.tbTotal)
        Me.GroupPanel1.Controls.Add(Me.codFin)
        Me.GroupPanel1.Controls.Add(Me.tbCanero)
        Me.GroupPanel1.Controls.Add(Me.codCan)
        Me.GroupPanel1.Controls.Add(Me.tbInst)
        Me.GroupPanel1.Controls.Add(Me.codIns)
        Me.GroupPanel1.Controls.Add(Me.codMon)
        Me.GroupPanel1.Controls.Add(Me.tbfecha)
        Me.GroupPanel1.Controls.Add(Me.grPrestamo)
        Me.GroupPanel1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel1.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.GroupPanel1.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.GroupPanel1.Location = New System.Drawing.Point(-2, 1)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(710, 298)
        '
        '
        '
        Me.GroupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanel1.Style.BackColorGradientAngle = 90
        Me.GroupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderBottomWidth = 1
        Me.GroupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderLeftWidth = 1
        Me.GroupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderRightWidth = 1
        Me.GroupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderTopWidth = 1
        Me.GroupPanel1.Style.CornerDiameter = 4
        Me.GroupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel1.TabIndex = 11
        Me.GroupPanel1.Text = "DATOS"
        '
        'tbcod
        '
        '
        '
        '
        Me.tbcod.Border.Class = "TextBoxBorder"
        Me.tbcod.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbcod.Location = New System.Drawing.Point(128, 6)
        Me.tbcod.Name = "tbcod"
        Me.tbcod.PreventEnterBeep = True
        Me.tbcod.Size = New System.Drawing.Size(100, 21)
        Me.tbcod.TabIndex = 46
        '
        'LabelX14
        '
        Me.LabelX14.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX14.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX14.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LabelX14.Location = New System.Drawing.Point(11, 3)
        Me.LabelX14.Name = "LabelX14"
        Me.LabelX14.Size = New System.Drawing.Size(110, 23)
        Me.LabelX14.TabIndex = 45
        Me.LabelX14.Text = "Codigo:"
        '
        'tbObs
        '
        Me.tbObs.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.tbObs.Border.Class = "TextBoxBorder"
        Me.tbObs.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbObs.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbObs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbObs.Location = New System.Drawing.Point(127, 180)
        Me.tbObs.Multiline = True
        Me.tbObs.Name = "tbObs"
        Me.tbObs.PreventEnterBeep = True
        Me.tbObs.Size = New System.Drawing.Size(455, 47)
        Me.tbObs.TabIndex = 44
        '
        'tbFinan
        '
        Me.tbFinan.ComboStyle = Janus.Windows.GridEX.ComboStyle.DropDownList
        Me.tbFinan.ControlThemedAreas = Janus.Windows.GridEX.ControlThemedAreas.Button
        tbFinan_DesignTimeLayout.LayoutString = resources.GetString("tbFinan_DesignTimeLayout.LayoutString")
        Me.tbFinan.DesignTimeLayout = tbFinan_DesignTimeLayout
        Me.tbFinan.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tbFinan.Location = New System.Drawing.Point(184, 120)
        Me.tbFinan.Name = "tbFinan"
        Me.tbFinan.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.tbFinan.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.tbFinan.SelectedIndex = -1
        Me.tbFinan.SelectedItem = Nothing
        Me.tbFinan.Size = New System.Drawing.Size(400, 21)
        Me.tbFinan.TabIndex = 32
        Me.tbFinan.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'LabelX9
        '
        Me.LabelX9.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX9.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LabelX9.Location = New System.Drawing.Point(12, 180)
        Me.LabelX9.Name = "LabelX9"
        Me.LabelX9.Size = New System.Drawing.Size(110, 23)
        Me.LabelX9.TabIndex = 26
        Me.LabelX9.Text = "Observacion:"
        '
        'LabelX8
        '
        Me.LabelX8.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX8.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LabelX8.Location = New System.Drawing.Point(11, 147)
        Me.LabelX8.Name = "LabelX8"
        Me.LabelX8.Size = New System.Drawing.Size(110, 23)
        Me.LabelX8.TabIndex = 25
        Me.LabelX8.Text = "Cantidad:"
        '
        'LabelX7
        '
        Me.LabelX7.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX7.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LabelX7.Location = New System.Drawing.Point(369, 147)
        Me.LabelX7.Name = "LabelX7"
        Me.LabelX7.Size = New System.Drawing.Size(110, 23)
        Me.LabelX7.TabIndex = 24
        Me.LabelX7.Text = "Total:"
        '
        'LabelX5
        '
        Me.LabelX5.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LabelX5.Location = New System.Drawing.Point(12, 118)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(110, 23)
        Me.LabelX5.TabIndex = 22
        Me.LabelX5.Text = "Servicio:"
        '
        'LabelX4
        '
        Me.LabelX4.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LabelX4.Location = New System.Drawing.Point(12, 62)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(110, 23)
        Me.LabelX4.TabIndex = 21
        Me.LabelX4.Text = "Cliente:"
        '
        'LabelX3
        '
        Me.LabelX3.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LabelX3.Location = New System.Drawing.Point(12, 89)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(110, 23)
        Me.LabelX3.TabIndex = 20
        Me.LabelX3.Text = "Instutición:"
        '
        'LabelX2
        '
        Me.LabelX2.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LabelX2.Location = New System.Drawing.Point(11, 32)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(110, 23)
        Me.LabelX2.TabIndex = 19
        Me.LabelX2.Text = "Moneda:"
        '
        'LabelX1
        '
        Me.LabelX1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LabelX1.Location = New System.Drawing.Point(287, 6)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(110, 23)
        Me.LabelX1.TabIndex = 18
        Me.LabelX1.Text = "Fecha:"
        '
        'tbInteres
        '
        '
        '
        '
        Me.tbInteres.Border.Class = "TextBoxBorder"
        Me.tbInteres.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbInteres.Location = New System.Drawing.Point(127, 149)
        Me.tbInteres.Name = "tbInteres"
        Me.tbInteres.PreventEnterBeep = True
        Me.tbInteres.Size = New System.Drawing.Size(85, 21)
        Me.tbInteres.TabIndex = 16
        '
        'tbTotal
        '
        '
        '
        '
        Me.tbTotal.Border.Class = "TextBoxBorder"
        Me.tbTotal.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbTotal.Location = New System.Drawing.Point(485, 149)
        Me.tbTotal.Name = "tbTotal"
        Me.tbTotal.PreventEnterBeep = True
        Me.tbTotal.Size = New System.Drawing.Size(99, 21)
        Me.tbTotal.TabIndex = 15
        '
        'codFin
        '
        '
        '
        '
        Me.codFin.Border.Class = "TextBoxBorder"
        Me.codFin.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.codFin.Location = New System.Drawing.Point(128, 120)
        Me.codFin.Name = "codFin"
        Me.codFin.PreventEnterBeep = True
        Me.codFin.Size = New System.Drawing.Size(50, 21)
        Me.codFin.TabIndex = 11
        '
        'tbCanero
        '
        '
        '
        '
        Me.tbCanero.Border.Class = "TextBoxBorder"
        Me.tbCanero.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbCanero.Location = New System.Drawing.Point(182, 62)
        Me.tbCanero.Name = "tbCanero"
        Me.tbCanero.PreventEnterBeep = True
        Me.tbCanero.Size = New System.Drawing.Size(400, 21)
        Me.tbCanero.TabIndex = 10
        '
        'codCan
        '
        '
        '
        '
        Me.codCan.Border.Class = "TextBoxBorder"
        Me.codCan.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.codCan.Location = New System.Drawing.Point(128, 62)
        Me.codCan.Name = "codCan"
        Me.codCan.PreventEnterBeep = True
        Me.codCan.Size = New System.Drawing.Size(50, 21)
        Me.codCan.TabIndex = 9
        '
        'tbInst
        '
        '
        '
        '
        Me.tbInst.Border.Class = "TextBoxBorder"
        Me.tbInst.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbInst.Location = New System.Drawing.Point(184, 89)
        Me.tbInst.Name = "tbInst"
        Me.tbInst.PreventEnterBeep = True
        Me.tbInst.Size = New System.Drawing.Size(400, 21)
        Me.tbInst.TabIndex = 8
        '
        'codIns
        '
        '
        '
        '
        Me.codIns.Border.Class = "TextBoxBorder"
        Me.codIns.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.codIns.Location = New System.Drawing.Point(128, 89)
        Me.codIns.Name = "codIns"
        Me.codIns.PreventEnterBeep = True
        Me.codIns.Size = New System.Drawing.Size(50, 21)
        Me.codIns.TabIndex = 7
        '
        'codMon
        '
        '
        '
        '
        Me.codMon.Border.Class = "TextBoxBorder"
        Me.codMon.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.codMon.Location = New System.Drawing.Point(128, 35)
        Me.codMon.Name = "codMon"
        Me.codMon.PreventEnterBeep = True
        Me.codMon.Size = New System.Drawing.Size(50, 21)
        Me.codMon.TabIndex = 5
        '
        'tbfecha
        '
        '
        '
        '
        Me.tbfecha.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbfecha.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbfecha.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown
        Me.tbfecha.ButtonDropDown.Visible = True
        Me.tbfecha.IsPopupCalendarOpen = False
        Me.tbfecha.Location = New System.Drawing.Point(413, 7)
        '
        '
        '
        '
        '
        '
        Me.tbfecha.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbfecha.MonthCalendar.CalendarDimensions = New System.Drawing.Size(1, 1)
        Me.tbfecha.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.tbfecha.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.tbfecha.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.tbfecha.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.tbfecha.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.tbfecha.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.tbfecha.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.tbfecha.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbfecha.MonthCalendar.DisplayMonth = New Date(2023, 1, 1, 0, 0, 0, 0)
        Me.tbfecha.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.tbfecha.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.tbfecha.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.tbfecha.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.tbfecha.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbfecha.MonthCalendar.TodayButtonVisible = True
        Me.tbfecha.Name = "tbfecha"
        Me.tbfecha.Size = New System.Drawing.Size(171, 21)
        Me.tbfecha.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.tbfecha.TabIndex = 4
        '
        'grPrestamo
        '
        Me.grPrestamo.BackColor = System.Drawing.Color.GhostWhite
        Me.grPrestamo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grPrestamo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grPrestamo.HeaderFormatStyle.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grPrestamo.Location = New System.Drawing.Point(0, 0)
        Me.grPrestamo.Name = "grPrestamo"
        Me.grPrestamo.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.grPrestamo.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.grPrestamo.Size = New System.Drawing.Size(704, 276)
        Me.grPrestamo.TabIndex = 12
        Me.grPrestamo.Visible = False
        Me.grPrestamo.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'tbMoneda
        '
        Me.tbMoneda.ComboStyle = Janus.Windows.GridEX.ComboStyle.DropDownList
        Me.tbMoneda.ControlThemedAreas = Janus.Windows.GridEX.ControlThemedAreas.Button
        tbMoneda_DesignTimeLayout.LayoutString = resources.GetString("tbMoneda_DesignTimeLayout.LayoutString")
        Me.tbMoneda.DesignTimeLayout = tbMoneda_DesignTimeLayout
        Me.tbMoneda.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tbMoneda.Location = New System.Drawing.Point(184, 35)
        Me.tbMoneda.Name = "tbMoneda"
        Me.tbMoneda.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.tbMoneda.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.tbMoneda.SelectedIndex = -1
        Me.tbMoneda.SelectedItem = Nothing
        Me.tbMoneda.Size = New System.Drawing.Size(191, 21)
        Me.tbMoneda.TabIndex = 34
        Me.tbMoneda.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'cbTipoCambio
        '
        Me.cbTipoCambio.ComboStyle = Janus.Windows.GridEX.ComboStyle.DropDownList
        Me.cbTipoCambio.ControlThemedAreas = Janus.Windows.GridEX.ControlThemedAreas.Button
        cbTipoCambio_DesignTimeLayout.LayoutString = resources.GetString("cbTipoCambio_DesignTimeLayout.LayoutString")
        Me.cbTipoCambio.DesignTimeLayout = cbTipoCambio_DesignTimeLayout
        Me.cbTipoCambio.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.cbTipoCambio.Location = New System.Drawing.Point(439, 36)
        Me.cbTipoCambio.Name = "cbTipoCambio"
        Me.cbTipoCambio.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbTipoCambio.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbTipoCambio.SelectedIndex = -1
        Me.cbTipoCambio.SelectedItem = Nothing
        Me.cbTipoCambio.Size = New System.Drawing.Size(145, 21)
        Me.cbTipoCambio.TabIndex = 48
        Me.cbTipoCambio.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'LabelX13
        '
        Me.LabelX13.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX13.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LabelX13.Location = New System.Drawing.Point(400, 34)
        Me.LabelX13.Name = "LabelX13"
        Me.LabelX13.Size = New System.Drawing.Size(45, 23)
        Me.LabelX13.TabIndex = 47
        Me.LabelX13.Text = "T.C.:"
        '
        'cbleyendas
        '
        Me.cbleyendas.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat
        Me.cbleyendas.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        cbleyendas_DesignTimeLayout.LayoutString = resources.GetString("cbleyendas_DesignTimeLayout.LayoutString")
        Me.cbleyendas.DesignTimeLayout = cbleyendas_DesignTimeLayout
        Me.cbleyendas.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbleyendas.Location = New System.Drawing.Point(128, 233)
        Me.cbleyendas.MaxLength = 40
        Me.cbleyendas.Name = "cbleyendas"
        Me.cbleyendas.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbleyendas.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbleyendas.SelectedIndex = -1
        Me.cbleyendas.SelectedItem = Nothing
        Me.cbleyendas.Size = New System.Drawing.Size(280, 19)
        Me.cbleyendas.TabIndex = 444
        Me.cbleyendas.Visible = False
        Me.cbleyendas.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'F0_REGISTROSERVICIO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(740, 411)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "F0_REGISTROSERVICIO"
        Me.Opacity = 0.99R
        Me.Text = "Facturar Servicio"
        Me.WindowState = System.Windows.Forms.FormWindowState.Normal
        Me.Controls.SetChildIndex(Me.PanelPrincipal, 0)
        Me.PanelSuperior.ResumeLayout(False)
        Me.PanelInferior.ResumeLayout(False)
        CType(Me.BubbleBarUsuario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelToolBar1.ResumeLayout(False)
        Me.PanelToolBar2.ResumeLayout(False)
        Me.PanelPrincipal.ResumeLayout(False)
        Me.PanelUsuario.ResumeLayout(False)
        Me.PanelUsuario.PerformLayout()
        Me.PanelNavegacion.ResumeLayout(False)
        Me.MPanelUserAct.ResumeLayout(False)
        Me.MPanelUserAct.PerformLayout()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelContent.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.MSuperTabControlPanel1.ResumeLayout(False)
        CType(Me.MSuperTabControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MSuperTabControl.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupPanel1.ResumeLayout(False)
        Me.GroupPanel1.PerformLayout()
        CType(Me.tbFinan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbfecha, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grPrestamo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbMoneda, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbTipoCambio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbleyendas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents tbFinan As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents LabelX9 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX8 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX7 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbInteres As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents tbTotal As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents codFin As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents tbCanero As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents codCan As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents tbInst As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents codIns As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents codMon As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents tbfecha As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents CachedKardexClienteRes1 As CachedKardexClienteRes
    Friend WithEvents tbObs As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents tbcod As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX14 As DevComponents.DotNetBar.LabelX
    Friend WithEvents grPrestamo As Janus.Windows.GridEX.GridEX
    Friend WithEvents tbMoneda As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents cbTipoCambio As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents LabelX13 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cbleyendas As Janus.Windows.GridEX.EditControls.MultiColumnCombo
End Class
