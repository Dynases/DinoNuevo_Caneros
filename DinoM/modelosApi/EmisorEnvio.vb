Public Class EmisorEnvio
    Public Class Detalle
        Public Property actividadEconomica As String
        Public Property codigoProductoSin As String
        Public Property codigoProducto As String
        Public Property descripcion As String
        Public Property unidadMedida As Integer
        Public Property cantidad As Integer
        Public Property precioUnitario As Double
        Public Property montoDescuento As Double
        Public Property subTotal As Double
        Public Property numeroSerie As String
        Public Property numeroImei As String
    End Class

    Public Class Emisor
        Public Property nitEmisor As Integer
        Public Property razonSocialEmisor As String
        Public Property municipio As String
        Public Property direccion As String
        Public Property telefonoEmisor As String
        Public Property fechaEmision As String
        Public Property codigoSucursal As Integer
        Public Property numeroFactura As Integer
        Public Property nombreRazonSocial As String
        Public Property leyenda As String
        Public Property tipoEmision As Integer

        Public Property nombreIntegracion As String
        Public Property codigoTipoDocumentoIdentidad As Integer
        Public Property numeroDocumento As String
        Public Property complemento As String
        Public Property codigoCliente As String
        Public Property codigoMetodoPago As Integer
        Public Property numeroTarjeta As String
        Public Property codigoPuntoVenta As Integer
        Public Property codigoDocumentoSector As Integer
        Public Property codigoMoneda As Integer
        Public Property tipoCambio As Integer
        Public Property montoTotal As Double
        Public Property montoTotalSujetoIva As Double
        Public Property montoTotalMoneda As Double
        Public Property montoGiftCard As Integer
        Public Property descuentoAdicional As Integer
        Public Property codigoExcepcion As Integer
        Public Property usuario As String
        Public Property email As String
        Public Property cuf As String
        Public Property actividadEconomica As Integer
        Public Property codigoMotivoAnulacion As Integer
        Public Property detalles As Detalle()
    End Class

    Public Class VerificarNit
        Public Property nit As String
        Public Property codigoSucursal As Integer

        Public Property codigoPuntoVenta As Integer

        Public Property nitVerificar As String

    End Class
End Class
