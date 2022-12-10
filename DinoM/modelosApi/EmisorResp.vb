Public Class EmisorResp
    Public Class Meta
        Public Property code As Integer
        Public Property message As String
    End Class

    Public Class Data
        Public Property details As String
        Public Property codigoEstado As Integer
        Public Property codigoRecepcion As String
        Public Property cuf As String
        Public Property codigoSector As String
        Public Property sucursal As Integer
        Public Property puntoVenta As Integer
        Public Property dataXml As String
        Public Property tipoEmision As Integer
    End Class

    Public Class RespEmisor
        ' Public Property response As String
        Public Property estadoEmisionEDOC As Integer
        Public Property estadoAnulacionEDOC As Integer
        Public Property codigoRecepcion As String

        Public Property fechaEmision As String
        Public Property cuis As String
        Public Property codigoControl As String
        Public Property cufd As String
        Public Property linkCodigoQR As String
        Public Property cuf As String




        Public Property codigoError As String
        Public Property mensajeRespuesta As String
    End Class

    Public Class RespVerificarNit
        ' Public Property response As String
        'Public Property codigoError As String
        Public Property codigoRecepcion As String

        Public Property fechaEmision As String
        Public Property cuis As String
        Public Property codigoControl As String
        Public Property cufd As String
        Public Property linkCodigoQR As String
        Public Property cuf As String




        Public Property codigoError As String
        Public Property mensajeRespuesta As String
    End Class


End Class
Public Class Meta
    Public Property status As Boolean
    Public Property code As Integer
    Public Property message As String
End Class

Public Class Errors
    Public Property details As Object()
    Public Property siat As Object()
End Class

Public Class Resp400
    'Public Property meta As Meta
    Public Property response As String
    Public Property codigoRecepcion As String
    Public Property estadoEmisionEdoc As Integer
    Public Property fechaEmision1 As String
    Public Property cuf As String
    Public Property cuis As String
    Public Property cufd As String
    Public Property codigoControl As String
    Public Property linkCodigoQr As String
    Public Property codigoError As String
    Public Property mensajeRespuesta As String
End Class

