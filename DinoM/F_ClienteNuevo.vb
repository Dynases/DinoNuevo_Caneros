
Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid
Imports Presentacion.F1_ServicioVenta

Public Class F_ClienteNuevo
    Dim TableVehiculo As DataTable
    Public Razonsocial As String = ""
    Public Nit As String = ""
    Public Cliente As Boolean = False
    Dim _codInsti As Integer = 0


    Public Sub _priniciarTodo()

        Tb_CodTara.CharacterCasing = CharacterCasing.Upper
        Tb_Placa.CharacterCasing = CharacterCasing.Upper
        'tbNit.CharacterCasing = CharacterCasing.Upper
        _LengthTextBox()
    End Sub
    Public Sub _LengthTextBox()
        Tb_CodTara.MaxLength = 50
        Tb_Placa.MaxLength = 200
        'tbNit.MaxLength = 20
    End Sub

    Public Sub _prHabilitarFocus()
        With MHighlighterFocus
            .SetHighlightOnFocus(Tb_CodTara, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(Tb_Placa, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(btnguardar, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(btnsalir, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
        End With
    End Sub
    Public Function _prValidar() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()

        'If Tb_Placa.Text.Trim = String.Empty Then
        '    Tb_Placa.BackColor = Color.Red
        '    MEP.SetError(Tb_Placa, "Ingrese número de placa!".ToUpper)
        '    _ok = False
        'Else
        '    Tb_Placa.BackColor = Color.White
        '    MEP.SetError(Tb_Placa, String.Empty)
        'End If

        If tbPesoTara.Text.Trim = String.Empty Then
            tbPesoTara.BackColor = Color.Red
            MEP.SetError(tbPesoTara, "Ingrese el peso de tara!".ToUpper)
            _ok = False
        Else
            tbPesoTara.BackColor = Color.White
            MEP.SetError(tbPesoTara, String.Empty)
        End If

        If Tb_Propietario.Text.Trim = String.Empty Then
            Tb_Propietario.BackColor = Color.Red
            MEP.SetError(Tb_Propietario, "Ingrese el nombre del propietario!".ToUpper)
            _ok = False
        Else
            Tb_Propietario.BackColor = Color.White
            MEP.SetError(Tb_Propietario, String.Empty)
        End If

        If Tb_CodTara.Text.Trim = String.Empty Then
            Tb_CodTara.BackColor = Color.Red
            MEP.SetError(Tb_CodTara, "Ingrese un código de tara!".ToUpper)
            _ok = False

        Else
            If True Then
                If L_BuscarCodTara(Tb_CodTara.Text) = True Then
                    Tb_CodTara.BackColor = Color.Red
                    MEP.SetError(Tb_CodTara, "Ingrese un código distinto!".ToUpper)
                    _ok = False
                Else
                    Tb_CodTara.BackColor = Color.White
                    MEP.SetError(Tb_CodTara, String.Empty)
                End If
            ElseIf _codInsti = Convert.ToInt32(Tb_CodTara.Text) Then
                _ok = True
            Else
                If L_BuscarCodTara(Tb_CodTara.Text) = True Then
                    Tb_CodTara.BackColor = Color.Red
                    MEP.SetError(Tb_CodTara, "Ingrese un código distinto!".ToUpper)
                    _ok = False
                Else
                    Tb_CodTara.BackColor = Color.White
                    MEP.SetError(Tb_CodTara, String.Empty)
                End If
            End If
            If _ok = True Then
                Tb_CodTara.BackColor = Color.White
                MEP.SetError(Tb_CodTara, String.Empty)
            End If
        End If
        MHighlighterFocus.UpdateHighlights()
        Return _ok

    End Function
    Private Sub F_ClienteNuevoServicio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _priniciarTodo()
        _prHabilitarFocus()
    End Sub



    Private Sub btnguardar_Click(sender As Object, e As EventArgs) Handles btnguardar.Click
        If (_prValidar()) Then

            ' Dim res As Boolean = L_fnGrabarCLiente("", "", Tb_Placa.Text, Tb_CodTara.Text, 1, 1, 1, "", "", "", "", 70, 1, 0, 0, "", Now.Date.ToString("yyyy/MM/dd"), Tb_Placa.Text, 1, 0, 0, Now.Date.ToString("yyyy/MM/dd"), Now.Date.ToString("yyyy/MM/dd"), "Default.jpg", 1)
            Dim res As Boolean = L_Taras_Grabar(Tb_CodTara.Text, Tb_Placa.Text, tbPesoTara.Value, Tb_Color.Text, Tb_Propietario.Text)
            If True Then
                ToastNotification.Show(Me, "El Codigo Tara: ".ToUpper + Tb_CodTara.Text + " Grabado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                Cliente = True
                Razonsocial = Tb_Placa.Text
                ' Nit = tbNit.Text
                Me.Close()
            End If
        End If


    End Sub


    Private Sub btnsalir_Click(sender As Object, e As EventArgs) Handles btnsalir.Click
        Me.Close()
    End Sub


    Private Sub HabilitarEnter(sender As Object, e As KeyPressEventArgs)
        If (e.KeyChar = ChrW(Keys.Enter)) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub tbNombre_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tb_CodTara.KeyPress
        HabilitarEnter(sender, e)
    End Sub

    Private Sub tbRazonSocial_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tb_Placa.KeyPress
        HabilitarEnter(sender, e)
    End Sub

    Private Sub LabelX1_Click(sender As Object, e As EventArgs) Handles LabelX1.Click

    End Sub

    Private Sub LabelX20_Click(sender As Object, e As EventArgs) Handles LabelX20.Click

    End Sub

    Private Sub tbPesoTara_ValueChanged(sender As Object, e As EventArgs) Handles tbPesoTara.ValueChanged

    End Sub

    Private Sub Tb_Color_TextChanged(sender As Object, e As EventArgs) Handles Tb_Color.TextChanged

    End Sub
End Class