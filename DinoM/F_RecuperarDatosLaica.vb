Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports System.Data.OleDb
Imports Janus.Windows.GridEX

Public Class F_RecuperarDatosLaica

    Public InventarioImport As New DataTable
    Private Sub F_RecuperarDatosLaica_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tbFecha.Value = Now.Date
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        InventarioImport.Clear()
        Dim datePatt As String = "yyyy-MM-dd"
        Dim localDate = tbFecha.Value
        Dim dtString As String = localDate.ToString(datePatt)
        'L_Boletas_Borrar(dtString)
        MP_ImportarExcel()
        MP_PasarDatos()
    End Sub


    Private Sub MP_ImportarExcel()
        Try
            Dim folder As String = ""
            Dim doc As String = "Sheet1"
            Dim openfile1 As OpenFileDialog = New OpenFileDialog()

            If openfile1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                folder = openfile1.FileName
            End If

            If True Then
                Dim pathconn As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & folder & ";Extended Properties='Excel 12.0 Xml;HDR=Yes'"
                'Dim pathconn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & folder & ";"
                'Dim pathconn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & folder & ";Extended Properties=""Excel 8.0;HDR=YES;"""

                Dim con As OleDbConnection = New OleDbConnection(pathconn)

                Dim MyDataAdapter As OleDbDataAdapter = New OleDbDataAdapter("Select * from [" & doc & "$A:I]", con)
                con.Open()

                MyDataAdapter.Fill(InventarioImport)
                Dim InvProductos As DataTable = InventarioImport.Copy
                If VerificarAnalisis(tbFecha.Value) Then

                    If _PEliminarRegistro(tbFecha.Value) Then
                        Dim grabar As Boolean = L_fnGrabarAnalisis("", InvProductos)
                        If (grabar) Then
                            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                            ToastNotification.Show(Me, "analisis Grabado con Exito.".ToUpper,
                                              img, 2000,
                                              eToastGlowColor.Green,
                                              eToastPosition.TopCenter
                                              )
                            grprecio.DataSource = InvProductos
                            grprecio.RetrieveStructure()
                            With grprecio.RootTable.Columns("ID Ticket")

                                .Width = 85
                                .Visible = True
                            End With
                            With grprecio.RootTable.Columns("Fecha")

                                .Width = 85
                                .Visible = True
                            End With
                            With grprecio.RootTable.Columns("Peso Torta")

                                .Width = 85
                                .Visible = True
                            End With
                            With grprecio.RootTable.Columns("% Fibra Caña")

                                .Width = 85
                                .Visible = True
                            End With
                            With grprecio.RootTable.Columns("% Brix")

                                .Width = 85
                                .Visible = True
                            End With
                            With grprecio.RootTable.Columns("%POL Unificada")

                                .Width = 85
                                .Visible = True
                            End With
                            With grprecio.RootTable.Columns("% Pureza Unificada")

                                .Width = 85
                                .Visible = True
                            End With
                            With grprecio.RootTable.Columns("% Materia Extraña")

                                .Width = 85
                                .Visible = True
                            End With
                            With grprecio.RootTable.Columns("Cantidad de Paquete")

                                .Width = 85
                                .Visible = True
                            End With
                            With grprecio
                                .GroupByBoxVisible = False
                                '.FilterRowFormatStyle.BackColor = Color.Blue
                                .DefaultFilterRowComparison = FilterConditionOperator.Contains
                                '.FilterMode = FilterMode.Automatic
                                .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
                                .FilterMode = FilterMode.Automatic
                                'Diseño de la tabla
                                .VisualStyle = VisualStyle.Office2007
                                .SelectionMode = SelectionMode.SingleSelection
                                .AlternatingColors = True
                            End With

                        Else
                            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                            ToastNotification.Show(Me, "analisis no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                        End If
                    End If
                Else
                    For k = 0 To InventarioImport.Rows.Count - 1
                        If VerificarAnalisis(InventarioImport.Rows(k).Item(1)) Then
                            If _PEliminarRegistro(InventarioImport.Rows(k).Item(1)) Then
                                Dim grabar As Boolean = L_fnGrabarAnalisis("", InvProductos)
                                If (grabar) Then
                                    Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
                                    ToastNotification.Show(Me, "analisis Grabado con Exito.".ToUpper,
                                                      img, 2000,
                                                      eToastGlowColor.Green,
                                                      eToastPosition.TopCenter
                                                      )
                                    grprecio.DataSource = InvProductos
                                    grprecio.RetrieveStructure()
                                    With grprecio.RootTable.Columns("ID Ticket")

                                        .Width = 85
                                        .Visible = True
                                    End With
                                    With grprecio.RootTable.Columns("Fecha")

                                        .Width = 85
                                        .Visible = True
                                    End With
                                    With grprecio.RootTable.Columns("Peso Torta")

                                        .Width = 85
                                        .Visible = True
                                    End With
                                    With grprecio.RootTable.Columns("% Fibra Caña")

                                        .Width = 85
                                        .Visible = True
                                    End With
                                    With grprecio.RootTable.Columns("% Brix")

                                        .Width = 85
                                        .Visible = True
                                    End With
                                    With grprecio.RootTable.Columns("%POL Unificada")

                                        .Width = 85
                                        .Visible = True
                                    End With
                                    With grprecio.RootTable.Columns("% Pureza Unificada")

                                        .Width = 85
                                        .Visible = True
                                    End With
                                    With grprecio.RootTable.Columns("% Materia Extraña")

                                        .Width = 85
                                        .Visible = True
                                    End With
                                    With grprecio.RootTable.Columns("Cantidad de Paquete")

                                        .Width = 85
                                        .Visible = True
                                    End With
                                    With grprecio
                                        .GroupByBoxVisible = False
                                        '.FilterRowFormatStyle.BackColor = Color.Blue
                                        .DefaultFilterRowComparison = FilterConditionOperator.Contains
                                        '.FilterMode = FilterMode.Automatic
                                        .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
                                        .FilterMode = FilterMode.Automatic
                                        'Diseño de la tabla
                                        .VisualStyle = VisualStyle.Office2007
                                        .SelectionMode = SelectionMode.SingleSelection
                                        .AlternatingColors = True
                                    End With
                                    Exit For
                                Else
                                    Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                                    ToastNotification.Show(Me, "analisis no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                                    Exit For
                                End If
                            End If
                            Exit For
                        Else
                            Exit For
                        End If

                    Next
                End If





                con.Close()

                Dim aux = InventarioImport.Rows.Count
                Dim aux1 = InventarioImport.Columns.Count





            End If

        Catch ex As Exception
            MostrarMensajeError(ex.Message)
        End Try
    End Sub

    Private Function _PEliminarRegistro(fecha As String) As Boolean
        Dim ef = New Efecto
        Dim _Resp As Boolean

        ef.tipo = 2
        ef.Context = "¿esta seguro de reprocesar las boletas con fecha " + fecha + " ?".ToUpper
        ef.Header = "Existen boletas con fecha " + fecha + " ".ToUpper
        ef.ShowDialog()
        Dim bandera As Boolean = False
        bandera = ef.band
        If (bandera = True) Then
            _Resp = True
            Dim t As String = fecha

            L_Boletas_Borrar(fecha)
            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)

            ToastNotification.Show(Me, "Código de Institución ".ToUpper + t + " eliminado con Exito.".ToUpper,
                                              img, 2000,
                                              eToastGlowColor.Green,
                                              eToastPosition.TopCenter)

        Else
            _Resp = False
        End If
        Return _Resp
    End Function
    Private Sub MP_PasarDatos()
        'Try
        '    Dim TablaProductos As DataTable = L_fnProductoGeneral(1)
        '    Dim ProdFiltrado As DataTable
        '    Dim Numi As String
        '    Dim Tablaaux As DataTable = InventarioImport.Copy

        '    ''Validación para comprobar que no existan dos o mas filas con el mismo codigo
        '    For k = 0 To InventarioImport.Rows.Count - 1
        '        Dim aux = Tablaaux.Select("Codigo=" + InventarioImport.Rows(k).Item("Codigo").ToString)
        '        If aux.Length > 1 Then
        '            ToastNotification.Show(Me, "No se puede realizar la importación porque el codigo:  ".ToUpper & InventarioImport.Rows(k).Item("Codigo").ToString & " existe  ".ToUpper & aux.Length.ToString & " veces en la lista".ToUpper,
        '                                   My.Resources.WARNING, 5000, eToastGlowColor.Green, eToastPosition.BottomCenter)
        '            Exit Sub
        '        End If
        '    Next


        '    If InventarioImport.Rows.Count = TablaProductos.Rows.Count Then
        '        ''Hago una copia para ir cambiando el codigo flex por el codigo del sistema
        '        Dim InvProductos As DataTable = InventarioImport.Copy
        '        For i = 0 To InventarioImport.Rows.Count - 1
        '            ProdFiltrado = L_fnProductoPorCacod(InventarioImport.Rows(i).Item("Codigo").ToString)
        '            If ProdFiltrado.Rows.Count > 0 Then
        '                Numi = ProdFiltrado.Rows(0).Item("canumi").ToString
        '                InvProductos.Rows(i).Item("Codigo") = Numi
        '            Else
        '                ToastNotification.Show(Me, "No se puede realizar la importación porque el código de producto: ".ToUpper & InventarioImport.Rows(i).Item("Codigo").ToString & " no pertenece a la lista de Productos de la base de datos".ToUpper,
        '                                       My.Resources.WARNING, 5000, eToastGlowColor.Green, eToastPosition.BottomCenter)
        '                Exit Sub
        '            End If

        '        Next
        '        Dim importar As Boolean = L_fnImportarInventario(InvProductos)
        '        If importar Then
        '            ToastNotification.Show(Me, "IMPORTACIÓN DEL INVENTARIO EXITOSA!!! ",
        '                          My.Resources.OK, 5000,
        '                          eToastGlowColor.Green,
        '                          eToastPosition.BottomCenter)
        '        Else
        '            ToastNotification.Show(Me, "FALLÓ LA IMPORTACIÓN DEL INVENTARIO!!!",
        '                          My.Resources.WARNING, 4000,
        '                          eToastGlowColor.Red,
        '                          eToastPosition.BottomCenter)
        '        End If
        '    Else
        '        ToastNotification.Show(Me, "No se puede realizar la importación porque la Lista del Inventario tiene que tener ".ToUpper & TablaProductos.Rows.Count & " registros".ToUpper,
        '                               My.Resources.WARNING, 5000, eToastGlowColor.Green, eToastPosition.BottomCenter)
        '        Exit Sub
        '    End If


        'Catch ex As Exception
        '    MostrarMensajeError(ex.Message)
        'End Try
    End Sub
    Private Sub MostrarMensajeError(mensaje As String)
        ToastNotification.Show(Me, mensaje.ToUpper, My.Resources.WARNING, 5000, eToastGlowColor.Red, eToastPosition.TopCenter)
    End Sub
End Class