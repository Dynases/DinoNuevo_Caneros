<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F1_IngresosEgresos
    Inherits Modelo.ModeloF1

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F1_IngresosEgresos))
        Dim cbConcepto_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.LabelX12 = New DevComponents.DotNetBar.LabelX()
        Me.tbCodAsig = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.tbAsigDesc = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX11 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX10 = New DevComponents.DotNetBar.LabelX()
        Me.tbfecha = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.btnVentCobros = New DevComponents.DotNetBar.ButtonX()
        Me.tbObservacion = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX8 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX7 = New DevComponents.DotNetBar.LabelX()
        Me.tbcodInst = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.tbcodCanero = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.tbInstitucion = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.tbdescCanero = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.lbNroCaja = New System.Windows.Forms.Label()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.tbIdCaja = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.btConcepto = New DevComponents.DotNetBar.ButtonX()
        Me.tbMonto = New DevComponents.Editors.DoubleInput()
        Me.lbgrupo1 = New DevComponents.DotNetBar.LabelX()
        Me.cbConcepto = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.LabelX16 = New DevComponents.DotNetBar.LabelX()
        Me.dpFecha = New System.Windows.Forms.DateTimePicker()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.tbcodigo = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.tbDescripcion = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.swTipo = New DevComponents.DotNetBar.Controls.SwitchButton()
        Me.LabelX9 = New DevComponents.DotNetBar.LabelX()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.SuperTabPrincipal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuperTabPrincipal.SuspendLayout()
        Me.SuperTabControlPanelRegistro.SuspendLayout()
        Me.PanelSuperior.SuspendLayout()
        Me.PanelInferior.SuspendLayout()
        CType(Me.BubbleBarUsuario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelToolBar1.SuspendLayout()
        Me.PanelToolBar2.SuspendLayout()
        Me.MPanelSup.SuspendLayout()
        Me.PanelPrincipal.SuspendLayout()
        Me.GroupPanelBuscador.SuspendLayout()
        CType(Me.JGrM_Buscador, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelUsuario.SuspendLayout()
        Me.PanelNavegacion.SuspendLayout()
        Me.MPanelUserAct.SuspendLayout()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupPanel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.tbMonto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbConcepto, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SuperTabPrincipal.Size = New System.Drawing.Size(1354, 699)
        Me.SuperTabPrincipal.Controls.SetChildIndex(Me.SuperTabControlPanelBuscador, 0)
        Me.SuperTabPrincipal.Controls.SetChildIndex(Me.SuperTabControlPanelRegistro, 0)
        '
        'SuperTabControlPanelBuscador
        '
        Me.SuperTabControlPanelBuscador.Location = New System.Drawing.Point(0, 0)
        Me.SuperTabControlPanelBuscador.Size = New System.Drawing.Size(1322, 709)
        '
        'SuperTabControlPanelRegistro
        '
        Me.SuperTabControlPanelRegistro.Size = New System.Drawing.Size(1322, 699)
        Me.SuperTabControlPanelRegistro.Controls.SetChildIndex(Me.PanelSuperior, 0)
        Me.SuperTabControlPanelRegistro.Controls.SetChildIndex(Me.PanelInferior, 0)
        Me.SuperTabControlPanelRegistro.Controls.SetChildIndex(Me.PanelPrincipal, 0)
        '
        'PanelSuperior
        '
        Me.PanelSuperior.Size = New System.Drawing.Size(1322, 72)
        Me.PanelSuperior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelSuperior.Style.BackColor1.Color = System.Drawing.Color.DarkSlateGray
        Me.PanelSuperior.Style.BackColor2.Color = System.Drawing.Color.DarkSlateGray
        Me.PanelSuperior.Style.BackgroundImage = CType(resources.GetObject("PanelSuperior.Style.BackgroundImage"), System.Drawing.Image)
        Me.PanelSuperior.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelSuperior.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelSuperior.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelSuperior.Style.GradientAngle = 90
        '
        'PanelInferior
        '
        Me.PanelInferior.Location = New System.Drawing.Point(0, 663)
        Me.PanelInferior.Size = New System.Drawing.Size(1322, 36)
        Me.PanelInferior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelInferior.Style.BackColor1.Color = System.Drawing.Color.DarkSlateGray
        Me.PanelInferior.Style.BackColor2.Color = System.Drawing.Color.DarkSlateGray
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
        'btnModificar
        '
        '
        'btnNuevo
        '
        Me.btnNuevo.Enabled = False
        '
        'PanelToolBar2
        '
        Me.PanelToolBar2.Location = New System.Drawing.Point(1242, 0)
        '
        'MPanelSup
        '
        Me.MPanelSup.Controls.Add(Me.Panel1)
        Me.MPanelSup.Size = New System.Drawing.Size(1322, 220)
        Me.MPanelSup.Controls.SetChildIndex(Me.PanelUsuario, 0)
        Me.MPanelSup.Controls.SetChildIndex(Me.Panel1, 0)
        '
        'PanelPrincipal
        '
        Me.PanelPrincipal.Size = New System.Drawing.Size(1322, 591)
        '
        'GroupPanelBuscador
        '
        Me.GroupPanelBuscador.Location = New System.Drawing.Point(0, 220)
        Me.GroupPanelBuscador.Size = New System.Drawing.Size(1322, 371)
        '
        '
        '
        Me.GroupPanelBuscador.Style.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupPanelBuscador.Style.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupPanelBuscador.Style.BackColorGradientAngle = 90
        Me.GroupPanelBuscador.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelBuscador.Style.BorderBottomWidth = 1
        Me.GroupPanelBuscador.Style.BorderColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupPanelBuscador.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelBuscador.Style.BorderLeftWidth = 1
        Me.GroupPanelBuscador.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelBuscador.Style.BorderRightWidth = 1
        Me.GroupPanelBuscador.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanelBuscador.Style.BorderTopWidth = 1
        Me.GroupPanelBuscador.Style.CornerDiameter = 4
        Me.GroupPanelBuscador.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanelBuscador.Style.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanelBuscador.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanelBuscador.Style.TextColor = System.Drawing.Color.White
        Me.GroupPanelBuscador.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanelBuscador.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanelBuscador.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        'JGrM_Buscador
        '
        Me.JGrM_Buscador.HeaderFormatStyle.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.JGrM_Buscador.HeaderFormatStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.JGrM_Buscador.SelectedFormatStyle.BackColor = System.Drawing.Color.DodgerBlue
        Me.JGrM_Buscador.SelectedFormatStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.JGrM_Buscador.SelectedFormatStyle.ForeColor = System.Drawing.Color.White
        Me.JGrM_Buscador.Size = New System.Drawing.Size(1316, 348)
        '
        'btnImprimir
        '
        '
        'MPanelUserAct
        '
        Me.MPanelUserAct.Location = New System.Drawing.Point(1122, 0)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.TableLayoutPanel1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1322, 220)
        Me.Panel1.TabIndex = 23
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupPanel1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1322, 220)
        Me.TableLayoutPanel1.TabIndex = 228
        '
        'GroupPanel1
        '
        Me.GroupPanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel1.Controls.Add(Me.Panel3)
        Me.GroupPanel1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanel1.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.GroupPanel1.Location = New System.Drawing.Point(3, 3)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(1316, 214)
        '
        '
        '
        Me.GroupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.MenuBackground
        Me.GroupPanel1.Style.BackColorGradientAngle = 90
        Me.GroupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarCaptionText
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
        Me.GroupPanel1.TabIndex = 227
        Me.GroupPanel1.Text = "DATOS GENERALES"
        '
        'Panel3
        '
        Me.Panel3.AutoScroll = True
        Me.Panel3.Controls.Add(Me.LabelX12)
        Me.Panel3.Controls.Add(Me.tbCodAsig)
        Me.Panel3.Controls.Add(Me.tbAsigDesc)
        Me.Panel3.Controls.Add(Me.LabelX11)
        Me.Panel3.Controls.Add(Me.LabelX10)
        Me.Panel3.Controls.Add(Me.tbfecha)
        Me.Panel3.Controls.Add(Me.btnVentCobros)
        Me.Panel3.Controls.Add(Me.tbObservacion)
        Me.Panel3.Controls.Add(Me.LabelX8)
        Me.Panel3.Controls.Add(Me.LabelX7)
        Me.Panel3.Controls.Add(Me.tbcodInst)
        Me.Panel3.Controls.Add(Me.tbcodCanero)
        Me.Panel3.Controls.Add(Me.tbInstitucion)
        Me.Panel3.Controls.Add(Me.tbdescCanero)
        Me.Panel3.Controls.Add(Me.lbNroCaja)
        Me.Panel3.Controls.Add(Me.LabelX6)
        Me.Panel3.Controls.Add(Me.LabelX4)
        Me.Panel3.Controls.Add(Me.tbIdCaja)
        Me.Panel3.Controls.Add(Me.btConcepto)
        Me.Panel3.Controls.Add(Me.tbMonto)
        Me.Panel3.Controls.Add(Me.lbgrupo1)
        Me.Panel3.Controls.Add(Me.cbConcepto)
        Me.Panel3.Controls.Add(Me.LabelX16)
        Me.Panel3.Controls.Add(Me.dpFecha)
        Me.Panel3.Controls.Add(Me.LabelX5)
        Me.Panel3.Controls.Add(Me.LabelX1)
        Me.Panel3.Controls.Add(Me.tbcodigo)
        Me.Panel3.Controls.Add(Me.LabelX2)
        Me.Panel3.Controls.Add(Me.tbDescripcion)
        Me.Panel3.Controls.Add(Me.LabelX3)
        Me.Panel3.Controls.Add(Me.swTipo)
        Me.Panel3.Controls.Add(Me.LabelX9)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1310, 191)
        Me.Panel3.TabIndex = 227
        '
        'LabelX12
        '
        Me.LabelX12.AutoSize = True
        Me.LabelX12.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX12.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX12.Location = New System.Drawing.Point(1067, 5)
        Me.LabelX12.Name = "LabelX12"
        Me.LabelX12.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX12.Size = New System.Drawing.Size(50, 16)
        Me.LabelX12.TabIndex = 398
        Me.LabelX12.Text = "Código:"
        '
        'tbCodAsig
        '
        '
        '
        '
        Me.tbCodAsig.Border.Class = "TextBoxBorder"
        Me.tbCodAsig.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbCodAsig.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCodAsig.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbCodAsig.Location = New System.Drawing.Point(1183, 0)
        Me.tbCodAsig.Name = "tbCodAsig"
        Me.tbCodAsig.PreventEnterBeep = True
        Me.tbCodAsig.Size = New System.Drawing.Size(83, 21)
        Me.tbCodAsig.TabIndex = 397
        Me.tbCodAsig.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbAsigDesc
        '
        '
        '
        '
        Me.tbAsigDesc.Border.Class = "TextBoxBorder"
        Me.tbAsigDesc.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbAsigDesc.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbAsigDesc.Location = New System.Drawing.Point(1068, 52)
        Me.tbAsigDesc.Multiline = True
        Me.tbAsigDesc.Name = "tbAsigDesc"
        Me.tbAsigDesc.PreventEnterBeep = True
        Me.tbAsigDesc.Size = New System.Drawing.Size(230, 60)
        Me.tbAsigDesc.TabIndex = 395
        '
        'LabelX11
        '
        Me.LabelX11.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX11.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX11.Location = New System.Drawing.Point(1068, 20)
        Me.LabelX11.Name = "LabelX11"
        Me.LabelX11.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX11.Size = New System.Drawing.Size(90, 23)
        Me.LabelX11.TabIndex = 396
        Me.LabelX11.Text = "Observación:"
        '
        'LabelX10
        '
        Me.LabelX10.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX10.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX10.Location = New System.Drawing.Point(740, 89)
        Me.LabelX10.Name = "LabelX10"
        Me.LabelX10.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX10.Size = New System.Drawing.Size(50, 23)
        Me.LabelX10.TabIndex = 394
        Me.LabelX10.Text = "Fecha:"
        '
        'tbfecha
        '
        '
        '
        '
        Me.tbfecha.Border.Class = "TextBoxBorder"
        Me.tbfecha.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbfecha.Enabled = False
        Me.tbfecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbfecha.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbfecha.Location = New System.Drawing.Point(796, 89)
        Me.tbfecha.Name = "tbfecha"
        Me.tbfecha.PreventEnterBeep = True
        Me.tbfecha.Size = New System.Drawing.Size(72, 21)
        Me.tbfecha.TabIndex = 393
        '
        'btnVentCobros
        '
        Me.btnVentCobros.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnVentCobros.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnVentCobros.Enabled = False
        Me.btnVentCobros.Location = New System.Drawing.Point(901, 122)
        Me.btnVentCobros.Name = "btnVentCobros"
        Me.btnVentCobros.Size = New System.Drawing.Size(75, 60)
        Me.btnVentCobros.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnVentCobros.TabIndex = 391
        Me.btnVentCobros.Text = "ver ventas sin cobros"
        '
        'tbObservacion
        '
        '
        '
        '
        Me.tbObservacion.Border.Class = "TextBoxBorder"
        Me.tbObservacion.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbObservacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbObservacion.Location = New System.Drawing.Point(588, 122)
        Me.tbObservacion.Multiline = True
        Me.tbObservacion.Name = "tbObservacion"
        Me.tbObservacion.PreventEnterBeep = True
        Me.tbObservacion.Size = New System.Drawing.Size(280, 60)
        Me.tbObservacion.TabIndex = 217
        '
        'LabelX8
        '
        Me.LabelX8.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX8.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX8.Location = New System.Drawing.Point(492, 56)
        Me.LabelX8.Name = "LabelX8"
        Me.LabelX8.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX8.Size = New System.Drawing.Size(90, 23)
        Me.LabelX8.TabIndex = 390
        Me.LabelX8.Text = "Institución:"
        '
        'LabelX7
        '
        Me.LabelX7.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX7.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX7.Location = New System.Drawing.Point(492, 23)
        Me.LabelX7.Name = "LabelX7"
        Me.LabelX7.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX7.Size = New System.Drawing.Size(90, 23)
        Me.LabelX7.TabIndex = 389
        Me.LabelX7.Text = "Cañero:"
        '
        'tbcodInst
        '
        '
        '
        '
        Me.tbcodInst.Border.Class = "TextBoxBorder"
        Me.tbcodInst.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbcodInst.Enabled = False
        Me.tbcodInst.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbcodInst.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbcodInst.Location = New System.Drawing.Point(588, 58)
        Me.tbcodInst.Name = "tbcodInst"
        Me.tbcodInst.PreventEnterBeep = True
        Me.tbcodInst.Size = New System.Drawing.Size(60, 21)
        Me.tbcodInst.TabIndex = 388
        '
        'tbcodCanero
        '
        '
        '
        '
        Me.tbcodCanero.Border.Class = "TextBoxBorder"
        Me.tbcodCanero.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbcodCanero.Enabled = False
        Me.tbcodCanero.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbcodCanero.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbcodCanero.Location = New System.Drawing.Point(588, 24)
        Me.tbcodCanero.Name = "tbcodCanero"
        Me.tbcodCanero.PreventEnterBeep = True
        Me.tbcodCanero.Size = New System.Drawing.Size(60, 21)
        Me.tbcodCanero.TabIndex = 387
        '
        'tbInstitucion
        '
        '
        '
        '
        Me.tbInstitucion.Border.Class = "TextBoxBorder"
        Me.tbInstitucion.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbInstitucion.Enabled = False
        Me.tbInstitucion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbInstitucion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbInstitucion.Location = New System.Drawing.Point(649, 58)
        Me.tbInstitucion.Name = "tbInstitucion"
        Me.tbInstitucion.PreventEnterBeep = True
        Me.tbInstitucion.Size = New System.Drawing.Size(350, 21)
        Me.tbInstitucion.TabIndex = 386
        '
        'tbdescCanero
        '
        '
        '
        '
        Me.tbdescCanero.Border.Class = "TextBoxBorder"
        Me.tbdescCanero.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbdescCanero.Enabled = False
        Me.tbdescCanero.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbdescCanero.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbdescCanero.Location = New System.Drawing.Point(649, 24)
        Me.tbdescCanero.Name = "tbdescCanero"
        Me.tbdescCanero.PreventEnterBeep = True
        Me.tbdescCanero.Size = New System.Drawing.Size(350, 21)
        Me.tbdescCanero.TabIndex = 385
        '
        'lbNroCaja
        '
        Me.lbNroCaja.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lbNroCaja.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.lbNroCaja.Location = New System.Drawing.Point(1206, 139)
        Me.lbNroCaja.Name = "lbNroCaja"
        Me.lbNroCaja.Size = New System.Drawing.Size(60, 17)
        Me.lbNroCaja.TabIndex = 384
        Me.lbNroCaja.Text = "Label1"
        Me.lbNroCaja.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbNroCaja.Visible = False
        '
        'LabelX6
        '
        Me.LabelX6.AutoSize = True
        Me.LabelX6.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX6.Location = New System.Drawing.Point(1138, 142)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX6.Size = New System.Drawing.Size(65, 16)
        Me.LabelX6.TabIndex = 383
        Me.LabelX6.Text = "Nro. Caja:"
        Me.LabelX6.Visible = False
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
        Me.LabelX4.Location = New System.Drawing.Point(55, 34)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX4.Size = New System.Drawing.Size(66, 16)
        Me.LabelX4.TabIndex = 255
        Me.LabelX4.Text = "Nº Orden:"
        '
        'tbIdCaja
        '
        '
        '
        '
        Me.tbIdCaja.Border.Class = "TextBoxBorder"
        Me.tbIdCaja.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbIdCaja.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbIdCaja.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbIdCaja.Location = New System.Drawing.Point(188, 29)
        Me.tbIdCaja.Name = "tbIdCaja"
        Me.tbIdCaja.PreventEnterBeep = True
        Me.tbIdCaja.Size = New System.Drawing.Size(83, 21)
        Me.tbIdCaja.TabIndex = 254
        Me.tbIdCaja.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btConcepto
        '
        Me.btConcepto.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btConcepto.BackColor = System.Drawing.Color.Transparent
        Me.btConcepto.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat
        Me.btConcepto.Image = Global.DinoM.My.Resources.Resources.add
        Me.btConcepto.ImageFixedSize = New System.Drawing.Size(25, 23)
        Me.btConcepto.Location = New System.Drawing.Point(414, 154)
        Me.btConcepto.Name = "btConcepto"
        Me.btConcepto.Size = New System.Drawing.Size(28, 23)
        Me.btConcepto.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btConcepto.TabIndex = 253
        Me.btConcepto.Visible = False
        '
        'tbMonto
        '
        '
        '
        '
        Me.tbMonto.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbMonto.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbMonto.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbMonto.Font = New System.Drawing.Font("Arial Black", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbMonto.Increment = 1.0R
        Me.tbMonto.IsInputReadOnly = True
        Me.tbMonto.Location = New System.Drawing.Point(588, 86)
        Me.tbMonto.Name = "tbMonto"
        Me.tbMonto.Size = New System.Drawing.Size(132, 29)
        Me.tbMonto.TabIndex = 252
        '
        'lbgrupo1
        '
        Me.lbgrupo1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbgrupo1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbgrupo1.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbgrupo1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.lbgrupo1.Location = New System.Drawing.Point(55, 152)
        Me.lbgrupo1.Name = "lbgrupo1"
        Me.lbgrupo1.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbgrupo1.Size = New System.Drawing.Size(90, 23)
        Me.lbgrupo1.TabIndex = 251
        Me.lbgrupo1.Text = "Concepto:"
        '
        'cbConcepto
        '
        Me.cbConcepto.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat
        Me.cbConcepto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        cbConcepto_DesignTimeLayout.LayoutString = resources.GetString("cbConcepto_DesignTimeLayout.LayoutString")
        Me.cbConcepto.DesignTimeLayout = cbConcepto_DesignTimeLayout
        Me.cbConcepto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbConcepto.Location = New System.Drawing.Point(188, 153)
        Me.cbConcepto.Name = "cbConcepto"
        Me.cbConcepto.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbConcepto.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbConcepto.SelectedIndex = -1
        Me.cbConcepto.SelectedItem = Nothing
        Me.cbConcepto.Size = New System.Drawing.Size(220, 22)
        Me.cbConcepto.TabIndex = 250
        Me.cbConcepto.TabStop = False
        Me.cbConcepto.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'LabelX16
        '
        Me.LabelX16.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX16.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX16.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX16.Location = New System.Drawing.Point(55, 56)
        Me.LabelX16.Name = "LabelX16"
        Me.LabelX16.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX16.Size = New System.Drawing.Size(90, 23)
        Me.LabelX16.TabIndex = 249
        Me.LabelX16.Text = "Fecha:"
        '
        'dpFecha
        '
        Me.dpFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dpFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dpFecha.Location = New System.Drawing.Point(188, 57)
        Me.dpFecha.Name = "dpFecha"
        Me.dpFecha.Size = New System.Drawing.Size(97, 22)
        Me.dpFecha.TabIndex = 248
        Me.dpFecha.TabStop = False
        '
        'LabelX5
        '
        Me.LabelX5.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX5.Location = New System.Drawing.Point(492, 122)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX5.Size = New System.Drawing.Size(90, 23)
        Me.LabelX5.TabIndex = 218
        Me.LabelX5.Text = "Observación:"
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
        Me.LabelX1.Location = New System.Drawing.Point(55, 7)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX1.Size = New System.Drawing.Size(50, 16)
        Me.LabelX1.TabIndex = 21
        Me.LabelX1.Text = "Código:"
        '
        'tbcodigo
        '
        '
        '
        '
        Me.tbcodigo.Border.Class = "TextBoxBorder"
        Me.tbcodigo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbcodigo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbcodigo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbcodigo.Location = New System.Drawing.Point(188, 2)
        Me.tbcodigo.Name = "tbcodigo"
        Me.tbcodigo.PreventEnterBeep = True
        Me.tbcodigo.Size = New System.Drawing.Size(83, 21)
        Me.tbcodigo.TabIndex = 0
        Me.tbcodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LabelX2
        '
        Me.LabelX2.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX2.Location = New System.Drawing.Point(55, 122)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX2.Size = New System.Drawing.Size(90, 23)
        Me.LabelX2.TabIndex = 23
        Me.LabelX2.Text = "Descripción:"
        '
        'tbDescripcion
        '
        '
        '
        '
        Me.tbDescripcion.Border.Class = "TextBoxBorder"
        Me.tbDescripcion.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbDescripcion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbDescripcion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbDescripcion.Location = New System.Drawing.Point(188, 124)
        Me.tbDescripcion.Name = "tbDescripcion"
        Me.tbDescripcion.PreventEnterBeep = True
        Me.tbDescripcion.Size = New System.Drawing.Size(300, 21)
        Me.tbDescripcion.TabIndex = 1
        '
        'LabelX3
        '
        Me.LabelX3.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX3.Location = New System.Drawing.Point(492, 89)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX3.Size = New System.Drawing.Size(90, 23)
        Me.LabelX3.TabIndex = 25
        Me.LabelX3.Text = "Monto:"
        '
        'swTipo
        '
        '
        '
        '
        Me.swTipo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.swTipo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.swTipo.Location = New System.Drawing.Point(188, 90)
        Me.swTipo.Name = "swTipo"
        Me.swTipo.OffBackColor = System.Drawing.Color.DodgerBlue
        Me.swTipo.OffText = "EGRESO"
        Me.swTipo.OffTextColor = System.Drawing.Color.White
        Me.swTipo.OnBackColor = System.Drawing.Color.DarkOrange
        Me.swTipo.OnText = "INGRESO"
        Me.swTipo.OnTextColor = System.Drawing.Color.White
        Me.swTipo.Size = New System.Drawing.Size(136, 22)
        Me.swTipo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.swTipo.TabIndex = 4
        Me.swTipo.Value = True
        Me.swTipo.ValueObject = "Y"
        '
        'LabelX9
        '
        Me.LabelX9.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX9.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX9.Location = New System.Drawing.Point(55, 89)
        Me.LabelX9.Name = "LabelX9"
        Me.LabelX9.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX9.Size = New System.Drawing.Size(90, 23)
        Me.LabelX9.TabIndex = 216
        Me.LabelX9.Text = "Tipo:"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 500
        '
        'F1_IngresosEgresos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1354, 699)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "F1_IngresosEgresos"
        Me.Text = "F1_IngresosEgresos"
        Me.Controls.SetChildIndex(Me.SuperTabPrincipal, 0)
        CType(Me.SuperTabPrincipal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SuperTabPrincipal.ResumeLayout(False)
        Me.SuperTabControlPanelRegistro.ResumeLayout(False)
        Me.PanelSuperior.ResumeLayout(False)
        Me.PanelInferior.ResumeLayout(False)
        CType(Me.BubbleBarUsuario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelToolBar1.ResumeLayout(False)
        Me.PanelToolBar2.ResumeLayout(False)
        Me.MPanelSup.ResumeLayout(False)
        Me.PanelPrincipal.ResumeLayout(False)
        Me.GroupPanelBuscador.ResumeLayout(False)
        CType(Me.JGrM_Buscador, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelUsuario.ResumeLayout(False)
        Me.PanelUsuario.PerformLayout()
        Me.PanelNavegacion.ResumeLayout(False)
        Me.MPanelUserAct.ResumeLayout(False)
        Me.MPanelUserAct.PerformLayout()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupPanel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.tbMonto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbConcepto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbObservacion As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbcodigo As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbDescripcion As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents swTipo As DevComponents.DotNetBar.Controls.SwitchButton
    Friend WithEvents LabelX9 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX16 As DevComponents.DotNetBar.LabelX
    Friend WithEvents dpFecha As DateTimePicker
    Friend WithEvents lbgrupo1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cbConcepto As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents tbMonto As DevComponents.Editors.DoubleInput
    Friend WithEvents Timer1 As Timer
    Friend WithEvents btConcepto As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbIdCaja As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lbNroCaja As Label
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX8 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX7 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbcodInst As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents tbcodCanero As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents tbInstitucion As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents tbdescCanero As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents btnVentCobros As DevComponents.DotNetBar.ButtonX
    Friend WithEvents tbfecha As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX10 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbAsigDesc As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX11 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX12 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbCodAsig As DevComponents.DotNetBar.Controls.TextBoxX
End Class
