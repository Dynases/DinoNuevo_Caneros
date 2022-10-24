
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_ClienteNuevo
    Inherits System.Windows.Forms.Form

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
        Me.Tb_CodTara = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX20 = New DevComponents.DotNetBar.LabelX()
        Me.Tb_Placa = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX19 = New DevComponents.DotNetBar.LabelX()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ReflectionLabel1 = New DevComponents.DotNetBar.Controls.ReflectionLabel()
        Me.MEP = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.MHighlighterFocus = New DevComponents.DotNetBar.Validator.Highlighter()
        Me.MFlyoutUsuario = New DevComponents.DotNetBar.Controls.Flyout(Me.components)
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.btnsalir = New DevComponents.DotNetBar.ButtonX()
        Me.btnguardar = New DevComponents.DotNetBar.ButtonX()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Tb_Propietario = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.tbPesoTara = New DevComponents.Editors.DoubleInput()
        Me.Tb_Color = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.Panel1.SuspendLayout()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbPesoTara, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Tb_CodTara
        '
        Me.Tb_CodTara.AcceptsTab = True
        '
        '
        '
        Me.Tb_CodTara.Border.Class = "TextBoxBorder"
        Me.Tb_CodTara.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.Tb_CodTara.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tb_CodTara.Location = New System.Drawing.Point(100, 80)
        Me.Tb_CodTara.Name = "Tb_CodTara"
        Me.Tb_CodTara.PreventEnterBeep = True
        Me.Tb_CodTara.Size = New System.Drawing.Size(105, 22)
        Me.Tb_CodTara.TabIndex = 0
        '
        'LabelX6
        '
        Me.LabelX6.BackColor = System.Drawing.SystemColors.Control
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX6.Location = New System.Drawing.Point(12, 80)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.Size = New System.Drawing.Size(85, 23)
        Me.LabelX6.TabIndex = 145
        Me.LabelX6.Text = "CODIGO:"
        '
        'LabelX20
        '
        Me.LabelX20.BackColor = System.Drawing.SystemColors.Control
        '
        '
        '
        Me.LabelX20.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX20.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX20.Location = New System.Drawing.Point(228, 79)
        Me.LabelX20.Name = "LabelX20"
        Me.LabelX20.Size = New System.Drawing.Size(80, 23)
        Me.LabelX20.TabIndex = 153
        Me.LabelX20.Text = "PESO TARA:"
        '
        'Tb_Placa
        '
        '
        '
        '
        Me.Tb_Placa.Border.Class = "TextBoxBorder"
        Me.Tb_Placa.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.Tb_Placa.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tb_Placa.Location = New System.Drawing.Point(100, 108)
        Me.Tb_Placa.Name = "Tb_Placa"
        Me.Tb_Placa.PreventEnterBeep = True
        Me.Tb_Placa.Size = New System.Drawing.Size(108, 22)
        Me.Tb_Placa.TabIndex = 1
        '
        'LabelX19
        '
        Me.LabelX19.BackColor = System.Drawing.SystemColors.Control
        '
        '
        '
        Me.LabelX19.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX19.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX19.Location = New System.Drawing.Point(12, 107)
        Me.LabelX19.Name = "LabelX19"
        Me.LabelX19.Size = New System.Drawing.Size(50, 23)
        Me.LabelX19.TabIndex = 152
        Me.LabelX19.Text = "PLACA:"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DodgerBlue
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.ReflectionLabel1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(20, 5, 0, 5)
        Me.Panel1.Size = New System.Drawing.Size(450, 63)
        Me.Panel1.TabIndex = 157
        '
        'ReflectionLabel1
        '
        '
        '
        '
        Me.ReflectionLabel1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.ReflectionLabel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.ReflectionLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReflectionLabel1.Location = New System.Drawing.Point(20, 5)
        Me.ReflectionLabel1.Name = "ReflectionLabel1"
        Me.ReflectionLabel1.Size = New System.Drawing.Size(242, 53)
        Me.ReflectionLabel1.TabIndex = 0
        Me.ReflectionLabel1.Text = "<b><font size=""12""><font color=""#313b42"">CREAR NUEVA TARA</font></font></b>"
        '
        'MEP
        '
        Me.MEP.ContainerControl = Me
        '
        'MHighlighterFocus
        '
        Me.MHighlighterFocus.ContainerControl = Me
        '
        'MFlyoutUsuario
        '
        Me.MFlyoutUsuario.DropShadow = False
        Me.MFlyoutUsuario.Parent = Me
        '
        'LabelX1
        '
        Me.LabelX1.BackColor = System.Drawing.SystemColors.Control
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX1.Location = New System.Drawing.Point(228, 107)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(69, 23)
        Me.LabelX1.TabIndex = 165
        Me.LabelX1.Text = "COLOR:"
        '
        'btnsalir
        '
        Me.btnsalir.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnsalir.BackColor = System.Drawing.Color.Transparent
        Me.btnsalir.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnsalir.FadeEffect = False
        Me.btnsalir.FocusCuesEnabled = False
        Me.btnsalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsalir.Image = Global.DinoM.My.Resources.Resources.atras
        Me.btnsalir.ImageFixedSize = New System.Drawing.Size(20, 20)
        Me.btnsalir.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnsalir.Location = New System.Drawing.Point(319, 201)
        Me.btnsalir.Name = "btnsalir"
        Me.btnsalir.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnsalir.Size = New System.Drawing.Size(101, 42)
        Me.btnsalir.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnsalir.TabIndex = 163
        Me.btnsalir.Text = "SALIR"
        Me.btnsalir.TextColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        '
        'btnguardar
        '
        Me.btnguardar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnguardar.BackColor = System.Drawing.Color.Transparent
        Me.btnguardar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnguardar.FadeEffect = False
        Me.btnguardar.FocusCuesEnabled = False
        Me.btnguardar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnguardar.Image = Global.DinoM.My.Resources.Resources.save
        Me.btnguardar.ImageFixedSize = New System.Drawing.Size(20, 20)
        Me.btnguardar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnguardar.Location = New System.Drawing.Point(199, 201)
        Me.btnguardar.Name = "btnguardar"
        Me.btnguardar.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnguardar.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2)
        Me.btnguardar.Size = New System.Drawing.Size(101, 42)
        Me.btnguardar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnguardar.TabIndex = 6
        Me.btnguardar.Text = "AGREGAR"
        Me.btnguardar.TextColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        '
        'PictureBox1
        '
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Right
        Me.PictureBox1.Image = Global.DinoM.My.Resources.Resources.man
        Me.PictureBox1.Location = New System.Drawing.Point(386, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Padding = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.PictureBox1.Size = New System.Drawing.Size(64, 53)
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'Tb_Propietario
        '
        '
        '
        '
        Me.Tb_Propietario.Border.Class = "TextBoxBorder"
        Me.Tb_Propietario.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.Tb_Propietario.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tb_Propietario.Location = New System.Drawing.Point(100, 137)
        Me.Tb_Propietario.Name = "Tb_Propietario"
        Me.Tb_Propietario.PreventEnterBeep = True
        Me.Tb_Propietario.Size = New System.Drawing.Size(329, 22)
        Me.Tb_Propietario.TabIndex = 166
        '
        'LabelX2
        '
        Me.LabelX2.BackColor = System.Drawing.SystemColors.Control
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX2.Location = New System.Drawing.Point(12, 136)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(96, 23)
        Me.LabelX2.TabIndex = 167
        Me.LabelX2.Text = "PROPIETARIO:"
        '
        'tbPesoTara
        '
        '
        '
        '
        Me.tbPesoTara.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbPesoTara.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbPesoTara.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.tbPesoTara.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbPesoTara.Increment = 1.0R
        Me.tbPesoTara.Location = New System.Drawing.Point(304, 81)
        Me.tbPesoTara.MinValue = 0R
        Me.tbPesoTara.Name = "tbPesoTara"
        Me.tbPesoTara.Size = New System.Drawing.Size(125, 22)
        Me.tbPesoTara.TabIndex = 243
        Me.tbPesoTara.WatermarkAlignment = DevComponents.Editors.eTextAlignment.Right
        '
        'Tb_Color
        '
        '
        '
        '
        Me.Tb_Color.Border.Class = "TextBoxBorder"
        Me.Tb_Color.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.Tb_Color.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tb_Color.Location = New System.Drawing.Point(304, 109)
        Me.Tb_Color.Name = "Tb_Color"
        Me.Tb_Color.PreventEnterBeep = True
        Me.Tb_Color.Size = New System.Drawing.Size(125, 22)
        Me.Tb_Color.TabIndex = 164
        '
        'F_ClienteNuevo
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(450, 253)
        Me.Controls.Add(Me.tbPesoTara)
        Me.Controls.Add(Me.Tb_Propietario)
        Me.Controls.Add(Me.LabelX2)
        Me.Controls.Add(Me.Tb_Color)
        Me.Controls.Add(Me.LabelX1)
        Me.Controls.Add(Me.btnsalir)
        Me.Controls.Add(Me.btnguardar)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.LabelX20)
        Me.Controls.Add(Me.Tb_Placa)
        Me.Controls.Add(Me.LabelX19)
        Me.Controls.Add(Me.Tb_CodTara)
        Me.Controls.Add(Me.LabelX6)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "F_ClienteNuevo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CREAR NUEVO CLIENTE"
        Me.Panel1.ResumeLayout(False)
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbPesoTara, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Tb_CodTara As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX20 As DevComponents.DotNetBar.LabelX
    Friend WithEvents Tb_Placa As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX19 As DevComponents.DotNetBar.LabelX
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ReflectionLabel1 As DevComponents.DotNetBar.Controls.ReflectionLabel
    Friend WithEvents btnguardar As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnsalir As DevComponents.DotNetBar.ButtonX
    Friend WithEvents MEP As System.Windows.Forms.ErrorProvider
    Friend WithEvents MHighlighterFocus As DevComponents.DotNetBar.Validator.Highlighter
    Friend WithEvents MFlyoutUsuario As DevComponents.DotNetBar.Controls.Flyout
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents Tb_Propietario As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbPesoTara As DevComponents.Editors.DoubleInput
    Friend WithEvents Tb_Color As DevComponents.DotNetBar.Controls.TextBoxX
End Class
