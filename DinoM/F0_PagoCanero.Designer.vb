<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F0_PagoCanero
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F0_PagoCanero))
        Dim cbFormaPago_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim cbCambioDolar_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim cbSucursal_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.GroupPanel2 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btbBuscar = New DevComponents.DotNetBar.ButtonX()
        Me.swMoneda = New DevComponents.DotNetBar.Controls.SwitchButton()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.tbinteres = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.cbFormaPago = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.btgrupo1 = New DevComponents.DotNetBar.ButtonX()
        Me.cbCambioDolar = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.tbfechadeudal = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX17 = New DevComponents.DotNetBar.LabelX()
        Me.TbNombre2 = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.cbSucursal = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.tbFechaPago = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.lbCtrlEnter = New DevComponents.DotNetBar.LabelX()
        Me.LabelX10 = New DevComponents.DotNetBar.LabelX()
        Me.tbVendedor = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.tbCliente = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.tbCodigo = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.lbFVenta = New DevComponents.DotNetBar.LabelX()
        Me.lbIdVenta = New DevComponents.DotNetBar.LabelX()
        Me.gpDetalleVenta = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.grdetalle = New Janus.Windows.GridEX.GridEX()
        Me.SuperTabItem1 = New DevComponents.DotNetBar.SuperTabItem()
        Me.SuperTabControlPanel1 = New DevComponents.DotNetBar.SuperTabControlPanel()
        Me.GroupPanel3 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.grVentas = New Janus.Windows.GridEX.GridEX()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.btnDuplicar = New DevComponents.DotNetBar.ButtonX()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.PanelFondoDetalle = New System.Windows.Forms.Panel()
        Me.PanelEncabezado = New System.Windows.Forms.Panel()
        Me.SwDescuentoProveedor = New DevComponents.DotNetBar.Controls.SwitchButton()
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
        Me.GroupPanel2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.cbFormaPago, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbCambioDolar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbfechadeudal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbSucursal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbFechaPago, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpDetalleVenta.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.grdetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuperTabControlPanel1.SuspendLayout()
        Me.GroupPanel3.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.grVentas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelFondoDetalle.SuspendLayout()
        Me.PanelEncabezado.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelSuperior
        '
        Me.PanelSuperior.Controls.Add(Me.SwDescuentoProveedor)
        Me.PanelSuperior.Margin = New System.Windows.Forms.Padding(2)
        Me.PanelSuperior.Size = New System.Drawing.Size(1232, 72)
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
        Me.PanelSuperior.Controls.SetChildIndex(Me.SwDescuentoProveedor, 0)
        Me.PanelSuperior.Controls.SetChildIndex(Me.PanelToolBar1, 0)
        Me.PanelSuperior.Controls.SetChildIndex(Me.PanelToolBar2, 0)
        Me.PanelSuperior.Controls.SetChildIndex(Me.MRlAccion, 0)
        Me.PanelSuperior.Controls.SetChildIndex(Me.PictureBox1, 0)
        '
        'PanelInferior
        '
        Me.PanelInferior.Location = New System.Drawing.Point(0, 616)
        Me.PanelInferior.Margin = New System.Windows.Forms.Padding(2)
        Me.PanelInferior.Size = New System.Drawing.Size(1232, 39)
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
        Me.TxtNombreUsu.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtNombreUsu.ReadOnly = True
        Me.TxtNombreUsu.Size = New System.Drawing.Size(135, 32)
        Me.TxtNombreUsu.Text = "DEFAULT"
        '
        'btnSalir
        '
        '
        'btnGrabar
        '
        '
        'btnEliminar
        '
        Me.btnEliminar.Text = "ANULAR"
        '
        'btnModificar
        '
        '
        'btnNuevo
        '
        '
        'PanelToolBar2
        '
        Me.PanelToolBar2.Controls.Add(Me.btnDuplicar)
        Me.PanelToolBar2.Location = New System.Drawing.Point(972, 0)
        Me.PanelToolBar2.Margin = New System.Windows.Forms.Padding(2)
        Me.PanelToolBar2.Size = New System.Drawing.Size(260, 72)
        Me.PanelToolBar2.Controls.SetChildIndex(Me.btnImprimir, 0)
        Me.PanelToolBar2.Controls.SetChildIndex(Me.btnDuplicar, 0)
        '
        'PanelPrincipal
        '
        Me.PanelPrincipal.Margin = New System.Windows.Forms.Padding(2)
        Me.PanelPrincipal.Size = New System.Drawing.Size(1232, 655)
        Me.PanelPrincipal.Controls.SetChildIndex(Me.PanelInferior, 0)
        Me.PanelPrincipal.Controls.SetChildIndex(Me.PanelUsuario, 0)
        Me.PanelPrincipal.Controls.SetChildIndex(Me.PanelSuperior, 0)
        Me.PanelPrincipal.Controls.SetChildIndex(Me.Panel1, 0)
        '
        'btnImprimir
        '
        Me.btnImprimir.Margin = New System.Windows.Forms.Padding(4)
        Me.btnImprimir.Size = New System.Drawing.Size(260, 72)
        '
        'btnUltimo
        '
        Me.btnUltimo.Margin = New System.Windows.Forms.Padding(2)
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
        Me.MPanelUserAct.Location = New System.Drawing.Point(1032, 0)
        Me.MPanelUserAct.Margin = New System.Windows.Forms.Padding(2)
        '
        'MRlAccion
        '
        '
        '
        '
        Me.MRlAccion.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.MRlAccion.Margin = New System.Windows.Forms.Padding(2)
        Me.MRlAccion.Size = New System.Drawing.Size(596, 72)
        Me.MRlAccion.Visible = False
        '
        'PanelContent
        '
        Me.PanelContent.Controls.Add(Me.PanelEncabezado)
        Me.PanelContent.Margin = New System.Windows.Forms.Padding(2)
        Me.PanelContent.Size = New System.Drawing.Size(1199, 544)
        Me.PanelContent.TabIndex = 3
        '
        'Panel1
        '
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Size = New System.Drawing.Size(1232, 544)
        '
        'MSuperTabControlPanel1
        '
        Me.MSuperTabControlPanel1.Margin = New System.Windows.Forms.Padding(2)
        Me.MSuperTabControlPanel1.Size = New System.Drawing.Size(1199, 544)
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
        Me.MSuperTabControl.Controls.Add(Me.SuperTabControlPanel1)
        Me.MSuperTabControl.Margin = New System.Windows.Forms.Padding(2)
        Me.MSuperTabControl.SelectedTabIndex = 1
        Me.MSuperTabControl.Size = New System.Drawing.Size(1232, 544)
        Me.MSuperTabControl.Tabs.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.SuperTabItem1})
        Me.MSuperTabControl.Controls.SetChildIndex(Me.MSuperTabControlPanel1, 0)
        Me.MSuperTabControl.Controls.SetChildIndex(Me.SuperTabControlPanel1, 0)
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(779, 0)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4)
        '
        'GroupPanel2
        '
        Me.GroupPanel2.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel2.Controls.Add(Me.Panel2)
        Me.GroupPanel2.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupPanel2.Location = New System.Drawing.Point(0, 0)
        Me.GroupPanel2.Name = "GroupPanel2"
        Me.GroupPanel2.Size = New System.Drawing.Size(1199, 150)
        '
        '
        '
        Me.GroupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanel2.Style.BackColorGradientAngle = 90
        Me.GroupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderBottomWidth = 1
        Me.GroupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderLeftWidth = 1
        Me.GroupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderRightWidth = 1
        Me.GroupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderTopWidth = 1
        Me.GroupPanel2.Style.CornerDiameter = 4
        Me.GroupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel2.TabIndex = 230
        Me.GroupPanel2.Text = "DATOS GENERALES"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btbBuscar)
        Me.Panel2.Controls.Add(Me.swMoneda)
        Me.Panel2.Controls.Add(Me.LabelX5)
        Me.Panel2.Controls.Add(Me.tbinteres)
        Me.Panel2.Controls.Add(Me.LabelX4)
        Me.Panel2.Controls.Add(Me.cbFormaPago)
        Me.Panel2.Controls.Add(Me.LabelX3)
        Me.Panel2.Controls.Add(Me.btgrupo1)
        Me.Panel2.Controls.Add(Me.cbCambioDolar)
        Me.Panel2.Controls.Add(Me.tbfechadeudal)
        Me.Panel2.Controls.Add(Me.LabelX1)
        Me.Panel2.Controls.Add(Me.LabelX2)
        Me.Panel2.Controls.Add(Me.LabelX17)
        Me.Panel2.Controls.Add(Me.TbNombre2)
        Me.Panel2.Controls.Add(Me.cbSucursal)
        Me.Panel2.Controls.Add(Me.tbFechaPago)
        Me.Panel2.Controls.Add(Me.lbCtrlEnter)
        Me.Panel2.Controls.Add(Me.LabelX10)
        Me.Panel2.Controls.Add(Me.tbVendedor)
        Me.Panel2.Controls.Add(Me.tbCliente)
        Me.Panel2.Controls.Add(Me.tbCodigo)
        Me.Panel2.Controls.Add(Me.lbFVenta)
        Me.Panel2.Controls.Add(Me.lbIdVenta)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1193, 127)
        Me.Panel2.TabIndex = 0
        '
        'btbBuscar
        '
        Me.btbBuscar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btbBuscar.BackColor = System.Drawing.Color.LightGray
        Me.btbBuscar.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange
        Me.btbBuscar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.MEP.SetIconAlignment(Me.btbBuscar, System.Windows.Forms.ErrorIconAlignment.BottomRight)
        Me.btbBuscar.Image = Global.DinoM.My.Resources.Resources.BUSQUEDA
        Me.btbBuscar.ImageFixedSize = New System.Drawing.Size(40, 40)
        Me.btbBuscar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right
        Me.btbBuscar.Location = New System.Drawing.Point(831, 55)
        Me.btbBuscar.Name = "btbBuscar"
        Me.btbBuscar.Size = New System.Drawing.Size(115, 55)
        Me.btbBuscar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btbBuscar.TabIndex = 399
        Me.btbBuscar.Text = "BUSCAR"
        Me.btbBuscar.TextColor = System.Drawing.Color.Black
        '
        'swMoneda
        '
        '
        '
        '
        Me.swMoneda.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.swMoneda.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.swMoneda.Location = New System.Drawing.Point(829, 29)
        Me.swMoneda.Name = "swMoneda"
        Me.swMoneda.OffBackColor = System.Drawing.Color.LawnGreen
        Me.swMoneda.OffText = "DOLAR"
        Me.swMoneda.OnBackColor = System.Drawing.Color.Gold
        Me.swMoneda.OnText = "BOLIVIANO"
        Me.swMoneda.Size = New System.Drawing.Size(120, 22)
        Me.swMoneda.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.swMoneda.TabIndex = 398
        Me.swMoneda.Value = True
        Me.swMoneda.ValueObject = "Y"
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
        Me.LabelX5.Location = New System.Drawing.Point(685, 27)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX5.Size = New System.Drawing.Size(70, 23)
        Me.LabelX5.TabIndex = 397
        Me.LabelX5.Text = "Interes % :"
        '
        'tbinteres
        '
        Me.tbinteres.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.tbinteres.Border.Class = "TextBoxBorder"
        Me.tbinteres.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbinteres.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbinteres.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbinteres.Location = New System.Drawing.Point(755, 29)
        Me.tbinteres.Name = "tbinteres"
        Me.tbinteres.PreventEnterBeep = True
        Me.tbinteres.Size = New System.Drawing.Size(50, 22)
        Me.tbinteres.TabIndex = 396
        Me.tbinteres.Text = "6"
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
        Me.LabelX4.Location = New System.Drawing.Point(460, 67)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX4.Size = New System.Drawing.Size(101, 16)
        Me.LabelX4.TabIndex = 395
        Me.LabelX4.Text = "Forma De Pago:"
        '
        'cbFormaPago
        '
        cbFormaPago_DesignTimeLayout.LayoutString = resources.GetString("cbFormaPago_DesignTimeLayout.LayoutString")
        Me.cbFormaPago.DesignTimeLayout = cbFormaPago_DesignTimeLayout
        Me.cbFormaPago.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFormaPago.Location = New System.Drawing.Point(578, 62)
        Me.cbFormaPago.Name = "cbFormaPago"
        Me.cbFormaPago.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbFormaPago.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbFormaPago.SelectedIndex = -1
        Me.cbFormaPago.SelectedItem = Nothing
        Me.cbFormaPago.Size = New System.Drawing.Size(230, 22)
        Me.cbFormaPago.TabIndex = 394
        Me.cbFormaPago.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
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
        Me.LabelX3.Location = New System.Drawing.Point(460, 27)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX3.Size = New System.Drawing.Size(32, 16)
        Me.LabelX3.TabIndex = 393
        Me.LabelX3.Text = "T.C.:"
        '
        'btgrupo1
        '
        Me.btgrupo1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btgrupo1.BackColor = System.Drawing.Color.Transparent
        Me.btgrupo1.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat
        Me.btgrupo1.Image = Global.DinoM.My.Resources.Resources.add
        Me.btgrupo1.ImageFixedSize = New System.Drawing.Size(25, 23)
        Me.btgrupo1.Location = New System.Drawing.Point(652, 26)
        Me.btgrupo1.Name = "btgrupo1"
        Me.btgrupo1.Size = New System.Drawing.Size(28, 23)
        Me.btgrupo1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btgrupo1.TabIndex = 392
        Me.btgrupo1.Visible = False
        '
        'cbCambioDolar
        '
        Me.cbCambioDolar.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat
        Me.cbCambioDolar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        cbCambioDolar_DesignTimeLayout.LayoutString = resources.GetString("cbCambioDolar_DesignTimeLayout.LayoutString")
        Me.cbCambioDolar.DesignTimeLayout = cbCambioDolar_DesignTimeLayout
        Me.cbCambioDolar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCambioDolar.Location = New System.Drawing.Point(578, 27)
        Me.cbCambioDolar.Name = "cbCambioDolar"
        Me.cbCambioDolar.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbCambioDolar.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbCambioDolar.SelectedIndex = -1
        Me.cbCambioDolar.SelectedItem = Nothing
        Me.cbCambioDolar.Size = New System.Drawing.Size(70, 22)
        Me.cbCambioDolar.TabIndex = 391
        Me.cbCambioDolar.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'tbfechadeudal
        '
        '
        '
        '
        Me.tbfechadeudal.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbfechadeudal.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbfechadeudal.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown
        Me.tbfechadeudal.ButtonDropDown.Visible = True
        Me.tbfechadeudal.Font = New System.Drawing.Font("Georgia", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbfechadeudal.IsPopupCalendarOpen = False
        Me.tbfechadeudal.Location = New System.Drawing.Point(93, 33)
        '
        '
        '
        '
        '
        '
        Me.tbfechadeudal.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbfechadeudal.MonthCalendar.CalendarDimensions = New System.Drawing.Size(1, 1)
        Me.tbfechadeudal.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.tbfechadeudal.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.tbfechadeudal.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.tbfechadeudal.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.tbfechadeudal.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.tbfechadeudal.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.tbfechadeudal.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.tbfechadeudal.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbfechadeudal.MonthCalendar.DisplayMonth = New Date(2017, 2, 1, 0, 0, 0, 0)
        Me.tbfechadeudal.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.tbfechadeudal.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.tbfechadeudal.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.tbfechadeudal.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.tbfechadeudal.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbfechadeudal.MonthCalendar.TodayButtonVisible = True
        Me.tbfechadeudal.Name = "tbfechadeudal"
        Me.tbfechadeudal.Size = New System.Drawing.Size(116, 26)
        Me.tbfechadeudal.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.tbfechadeudal.TabIndex = 389
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
        Me.LabelX1.Location = New System.Drawing.Point(9, 31)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX1.Size = New System.Drawing.Size(88, 23)
        Me.LabelX1.TabIndex = 390
        Me.LabelX1.Text = "Deudas Al:"
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
        Me.LabelX2.Location = New System.Drawing.Point(12, 65)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX2.Size = New System.Drawing.Size(60, 23)
        Me.LabelX2.TabIndex = 268
        Me.LabelX2.Text = "Cañero:"
        '
        'LabelX17
        '
        Me.LabelX17.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX17.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX17.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX17.Location = New System.Drawing.Point(460, 89)
        Me.LabelX17.Name = "LabelX17"
        Me.LabelX17.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX17.Size = New System.Drawing.Size(90, 23)
        Me.LabelX17.TabIndex = 271
        Me.LabelX17.Text = "Pagar:"
        '
        'TbNombre2
        '
        Me.TbNombre2.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.TbNombre2.Border.Class = "TextBoxBorder"
        Me.TbNombre2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.TbNombre2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TbNombre2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.TbNombre2.Location = New System.Drawing.Point(179, 6)
        Me.TbNombre2.Name = "TbNombre2"
        Me.TbNombre2.PreventEnterBeep = True
        Me.TbNombre2.Size = New System.Drawing.Size(30, 22)
        Me.TbNombre2.TabIndex = 0
        Me.TbNombre2.Visible = False
        '
        'cbSucursal
        '
        cbSucursal_DesignTimeLayout.LayoutString = resources.GetString("cbSucursal_DesignTimeLayout.LayoutString")
        Me.cbSucursal.DesignTimeLayout = cbSucursal_DesignTimeLayout
        Me.cbSucursal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSucursal.Location = New System.Drawing.Point(578, 90)
        Me.cbSucursal.Name = "cbSucursal"
        Me.cbSucursal.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbSucursal.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbSucursal.SelectedIndex = -1
        Me.cbSucursal.SelectedItem = Nothing
        Me.cbSucursal.Size = New System.Drawing.Size(230, 22)
        Me.cbSucursal.TabIndex = 270
        Me.cbSucursal.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'tbFechaPago
        '
        '
        '
        '
        Me.tbFechaPago.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbFechaPago.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaPago.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown
        Me.tbFechaPago.ButtonDropDown.Visible = True
        Me.tbFechaPago.Font = New System.Drawing.Font("Georgia", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbFechaPago.IsPopupCalendarOpen = False
        Me.tbFechaPago.Location = New System.Drawing.Point(315, 27)
        '
        '
        '
        '
        '
        '
        Me.tbFechaPago.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaPago.MonthCalendar.CalendarDimensions = New System.Drawing.Size(1, 1)
        Me.tbFechaPago.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.tbFechaPago.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.tbFechaPago.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.tbFechaPago.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.tbFechaPago.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.tbFechaPago.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.tbFechaPago.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.tbFechaPago.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaPago.MonthCalendar.DisplayMonth = New Date(2017, 2, 1, 0, 0, 0, 0)
        Me.tbFechaPago.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.tbFechaPago.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.tbFechaPago.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.tbFechaPago.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.tbFechaPago.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbFechaPago.MonthCalendar.TodayButtonVisible = True
        Me.tbFechaPago.Name = "tbFechaPago"
        Me.tbFechaPago.Size = New System.Drawing.Size(116, 26)
        Me.tbFechaPago.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.tbFechaPago.TabIndex = 0
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
        Me.lbCtrlEnter.Location = New System.Drawing.Point(370, 59)
        Me.lbCtrlEnter.Name = "lbCtrlEnter"
        Me.lbCtrlEnter.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbCtrlEnter.Size = New System.Drawing.Size(60, 10)
        Me.lbCtrlEnter.TabIndex = 352
        Me.lbCtrlEnter.Text = "Ctrl+Enter"
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
        Me.LabelX10.Location = New System.Drawing.Point(12, 92)
        Me.LabelX10.Name = "LabelX10"
        Me.LabelX10.SingleLineColor = System.Drawing.SystemColors.Control
        Me.LabelX10.Size = New System.Drawing.Size(80, 23)
        Me.LabelX10.TabIndex = 269
        Me.LabelX10.Text = "Institución:"
        '
        'tbVendedor
        '
        Me.tbVendedor.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.tbVendedor.Border.Class = "TextBoxBorder"
        Me.tbVendedor.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbVendedor.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbVendedor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbVendedor.Location = New System.Drawing.Point(93, 93)
        Me.tbVendedor.Name = "tbVendedor"
        Me.tbVendedor.PreventEnterBeep = True
        Me.tbVendedor.Size = New System.Drawing.Size(338, 21)
        Me.tbVendedor.TabIndex = 0
        '
        'tbCliente
        '
        Me.tbCliente.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.tbCliente.Border.Class = "TextBoxBorder"
        Me.tbCliente.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbCliente.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCliente.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.tbCliente.Location = New System.Drawing.Point(93, 66)
        Me.tbCliente.Name = "tbCliente"
        Me.tbCliente.PreventEnterBeep = True
        Me.tbCliente.Size = New System.Drawing.Size(338, 21)
        Me.tbCliente.TabIndex = 0
        '
        'tbCodigo
        '
        Me.tbCodigo.BackColor = System.Drawing.Color.LightGray
        '
        '
        '
        Me.tbCodigo.Border.Class = "TextBoxBorder"
        Me.tbCodigo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbCodigo.DisabledBackColor = System.Drawing.Color.White
        Me.tbCodigo.Enabled = False
        Me.tbCodigo.Font = New System.Drawing.Font("Georgia", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCodigo.ForeColor = System.Drawing.Color.Black
        Me.tbCodigo.Location = New System.Drawing.Point(93, 4)
        Me.tbCodigo.Name = "tbCodigo"
        Me.tbCodigo.PreventEnterBeep = True
        Me.tbCodigo.Size = New System.Drawing.Size(80, 26)
        Me.tbCodigo.TabIndex = 0
        Me.tbCodigo.TabStop = False
        '
        'lbFVenta
        '
        Me.lbFVenta.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbFVenta.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbFVenta.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbFVenta.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.lbFVenta.Location = New System.Drawing.Point(228, 29)
        Me.lbFVenta.Name = "lbFVenta"
        Me.lbFVenta.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbFVenta.Size = New System.Drawing.Size(88, 23)
        Me.lbFVenta.TabIndex = 263
        Me.lbFVenta.Text = "Fecha Pago:"
        '
        'lbIdVenta
        '
        Me.lbIdVenta.AutoSize = True
        Me.lbIdVenta.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbIdVenta.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbIdVenta.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbIdVenta.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.lbIdVenta.Location = New System.Drawing.Point(9, 9)
        Me.lbIdVenta.Name = "lbIdVenta"
        Me.lbIdVenta.SingleLineColor = System.Drawing.SystemColors.Control
        Me.lbIdVenta.Size = New System.Drawing.Size(59, 16)
        Me.lbIdVenta.TabIndex = 262
        Me.lbIdVenta.Text = "Id Venta:"
        '
        'gpDetalleVenta
        '
        Me.gpDetalleVenta.BackColor = System.Drawing.Color.White
        Me.gpDetalleVenta.CanvasColor = System.Drawing.SystemColors.Control
        Me.gpDetalleVenta.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.gpDetalleVenta.Controls.Add(Me.Panel5)
        Me.gpDetalleVenta.DisabledBackColor = System.Drawing.Color.Empty
        Me.gpDetalleVenta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gpDetalleVenta.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gpDetalleVenta.Location = New System.Drawing.Point(0, 0)
        Me.gpDetalleVenta.Name = "gpDetalleVenta"
        Me.gpDetalleVenta.Size = New System.Drawing.Size(1199, 394)
        '
        '
        '
        Me.gpDetalleVenta.Style.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.gpDetalleVenta.Style.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.gpDetalleVenta.Style.BackColorGradientAngle = 90
        Me.gpDetalleVenta.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpDetalleVenta.Style.BorderBottomWidth = 1
        Me.gpDetalleVenta.Style.BorderColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.gpDetalleVenta.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpDetalleVenta.Style.BorderLeftWidth = 1
        Me.gpDetalleVenta.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpDetalleVenta.Style.BorderRightWidth = 1
        Me.gpDetalleVenta.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.gpDetalleVenta.Style.BorderTopWidth = 1
        Me.gpDetalleVenta.Style.CornerDiameter = 4
        Me.gpDetalleVenta.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.gpDetalleVenta.Style.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gpDetalleVenta.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.gpDetalleVenta.Style.TextColor = System.Drawing.Color.White
        Me.gpDetalleVenta.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.gpDetalleVenta.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.gpDetalleVenta.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.gpDetalleVenta.TabIndex = 3
        Me.gpDetalleVenta.Text = "DETALLE"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.White
        Me.Panel5.Controls.Add(Me.grdetalle)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1193, 371)
        Me.Panel5.TabIndex = 0
        '
        'grdetalle
        '
        Me.grdetalle.BackColor = System.Drawing.Color.GhostWhite
        Me.grdetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdetalle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdetalle.HeaderFormatStyle.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold)
        Me.grdetalle.Location = New System.Drawing.Point(0, 0)
        Me.grdetalle.Name = "grdetalle"
        Me.grdetalle.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.grdetalle.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.grdetalle.Size = New System.Drawing.Size(1193, 371)
        Me.grdetalle.TabIndex = 3
        Me.grdetalle.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'SuperTabItem1
        '
        Me.SuperTabItem1.AttachedControl = Me.SuperTabControlPanel1
        Me.SuperTabItem1.GlobalItem = False
        Me.SuperTabItem1.Name = "SuperTabItem1"
        Me.SuperTabItem1.Text = "BUSCAR"
        '
        'SuperTabControlPanel1
        '
        Me.SuperTabControlPanel1.Controls.Add(Me.GroupPanel3)
        Me.SuperTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SuperTabControlPanel1.Location = New System.Drawing.Point(0, 0)
        Me.SuperTabControlPanel1.Name = "SuperTabControlPanel1"
        Me.SuperTabControlPanel1.Size = New System.Drawing.Size(1286, 518)
        Me.SuperTabControlPanel1.TabIndex = 0
        Me.SuperTabControlPanel1.TabItem = Me.SuperTabItem1
        '
        'GroupPanel3
        '
        Me.GroupPanel3.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel3.Controls.Add(Me.Panel6)
        Me.GroupPanel3.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanel3.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel3.Location = New System.Drawing.Point(0, 0)
        Me.GroupPanel3.Name = "GroupPanel3"
        Me.GroupPanel3.Size = New System.Drawing.Size(1286, 518)
        '
        '
        '
        Me.GroupPanel3.Style.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupPanel3.Style.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupPanel3.Style.BackColorGradientAngle = 90
        Me.GroupPanel3.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderBottomWidth = 1
        Me.GroupPanel3.Style.BorderColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupPanel3.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderLeftWidth = 1
        Me.GroupPanel3.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderRightWidth = 1
        Me.GroupPanel3.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderTopWidth = 1
        Me.GroupPanel3.Style.CornerDiameter = 4
        Me.GroupPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel3.Style.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel3.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel3.Style.TextColor = System.Drawing.Color.White
        Me.GroupPanel3.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel3.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel3.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel3.TabIndex = 4
        Me.GroupPanel3.Text = "BUSCADOR  VENTAS"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.White
        Me.Panel6.Controls.Add(Me.grVentas)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1280, 495)
        Me.Panel6.TabIndex = 0
        '
        'grVentas
        '
        Me.grVentas.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grVentas.BackColor = System.Drawing.Color.GhostWhite
        Me.grVentas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grVentas.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None
        Me.grVentas.FocusStyle = Janus.Windows.GridEX.FocusStyle.Solid
        Me.grVentas.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grVentas.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.UseRowStyle
        Me.grVentas.HeaderFormatStyle.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grVentas.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.grVentas.Location = New System.Drawing.Point(0, 0)
        Me.grVentas.Name = "grVentas"
        Me.grVentas.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.grVentas.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.grVentas.SelectedFormatStyle.BackColor = System.Drawing.Color.DodgerBlue
        Me.grVentas.SelectedFormatStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grVentas.SelectedFormatStyle.ForeColor = System.Drawing.Color.White
        Me.grVentas.SelectOnExpand = False
        Me.grVentas.Size = New System.Drawing.Size(1280, 495)
        Me.grVentas.TabIndex = 0
        Me.grVentas.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'btnDuplicar
        '
        Me.btnDuplicar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnDuplicar.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange
        Me.btnDuplicar.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnDuplicar.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDuplicar.Image = Global.DinoM.My.Resources.Resources._14
        Me.btnDuplicar.ImageFixedSize = New System.Drawing.Size(48, 48)
        Me.btnDuplicar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnDuplicar.Location = New System.Drawing.Point(180, 0)
        Me.btnDuplicar.Name = "btnDuplicar"
        Me.btnDuplicar.Size = New System.Drawing.Size(80, 72)
        Me.btnDuplicar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnDuplicar.TabIndex = 13
        Me.btnDuplicar.Text = "DUPLICAR"
        Me.btnDuplicar.TextColor = System.Drawing.Color.White
        Me.btnDuplicar.Visible = False
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'PanelFondoDetalle
        '
        Me.PanelFondoDetalle.Controls.Add(Me.gpDetalleVenta)
        Me.PanelFondoDetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelFondoDetalle.Location = New System.Drawing.Point(0, 150)
        Me.PanelFondoDetalle.Name = "PanelFondoDetalle"
        Me.PanelFondoDetalle.Size = New System.Drawing.Size(1199, 394)
        Me.PanelFondoDetalle.TabIndex = 5
        '
        'PanelEncabezado
        '
        Me.PanelEncabezado.Controls.Add(Me.PanelFondoDetalle)
        Me.PanelEncabezado.Controls.Add(Me.GroupPanel2)
        Me.PanelEncabezado.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEncabezado.Location = New System.Drawing.Point(0, 0)
        Me.PanelEncabezado.Name = "PanelEncabezado"
        Me.PanelEncabezado.Size = New System.Drawing.Size(1199, 544)
        Me.PanelEncabezado.TabIndex = 1
        '
        'SwDescuentoProveedor
        '
        '
        '
        '
        Me.SwDescuentoProveedor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.SwDescuentoProveedor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SwDescuentoProveedor.Location = New System.Drawing.Point(884, 22)
        Me.SwDescuentoProveedor.Name = "SwDescuentoProveedor"
        Me.SwDescuentoProveedor.OffBackColor = System.Drawing.Color.LawnGreen
        Me.SwDescuentoProveedor.OffText = "DESC. MANUAL"
        Me.SwDescuentoProveedor.OnBackColor = System.Drawing.Color.Gold
        Me.SwDescuentoProveedor.OnText = "DESC. AUTOMATICO"
        Me.SwDescuentoProveedor.Size = New System.Drawing.Size(170, 28)
        Me.SwDescuentoProveedor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.SwDescuentoProveedor.TabIndex = 387
        Me.SwDescuentoProveedor.Value = True
        Me.SwDescuentoProveedor.ValueObject = "Y"
        Me.SwDescuentoProveedor.Visible = False
        '
        'F0_PagoCanero
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1232, 655)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "F0_PagoCanero"
        Me.Text = "F0_Venta2"
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
        Me.GroupPanel2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.cbFormaPago, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbCambioDolar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbfechadeudal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbSucursal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbFechaPago, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpDetalleVenta.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        CType(Me.grdetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SuperTabControlPanel1.ResumeLayout(False)
        Me.GroupPanel3.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        CType(Me.grVentas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelFondoDetalle.ResumeLayout(False)
        Me.PanelEncabezado.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents gpDetalleVenta As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents grdetalle As Janus.Windows.GridEX.GridEX
    Friend WithEvents GroupPanel2 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents lbTipoMoneda As DevComponents.DotNetBar.LabelX
    Friend WithEvents SuperTabControlPanel1 As DevComponents.DotNetBar.SuperTabControlPanel
    Friend WithEvents SuperTabItem1 As DevComponents.DotNetBar.SuperTabItem
    Friend WithEvents GroupPanel3 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents grVentas As Janus.Windows.GridEX.GridEX
    Friend WithEvents PrintDialog1 As PrintDialog
    Protected WithEvents btnDuplicar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents Timer1 As Timer
    Friend WithEvents PanelFondoDetalle As Panel
    Friend WithEvents PanelEncabezado As Panel
    Friend WithEvents SwDescuentoProveedor As DevComponents.DotNetBar.Controls.SwitchButton
    Friend WithEvents Panel2 As Panel
    Friend WithEvents swMoneda As DevComponents.DotNetBar.Controls.SwitchButton
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbinteres As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cbFormaPago As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btgrupo1 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents cbCambioDolar As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents tbfechadeudal As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX17 As DevComponents.DotNetBar.LabelX
    Friend WithEvents TbNombre2 As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents cbSucursal As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents tbFechaPago As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents lbCtrlEnter As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX10 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbVendedor As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents tbCliente As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents tbCodigo As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lbFVenta As DevComponents.DotNetBar.LabelX
    Friend WithEvents lbIdVenta As DevComponents.DotNetBar.LabelX
    Friend WithEvents btbBuscar As DevComponents.DotNetBar.ButtonX
End Class
