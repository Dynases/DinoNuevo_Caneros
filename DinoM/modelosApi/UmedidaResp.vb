﻿Public Class UmedidaResp
    Public Class Meta
        Public Property status As Boolean
        Public Property code As Integer
        Public Property message As String
    End Class

    Public Class Data
        Public Property codigoClasificador As String
        Public Property descripcion As String
    End Class

    Public Class Umedida
        Public Property codigo As String
        Public Property descripcion As String
    End Class
End Class
