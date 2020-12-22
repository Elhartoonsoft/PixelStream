Public Class ImageStream
    Inherits PixelStream
    Private Bitmap As Bitmap = Nothing
    Public ReadOnly Property BaseBitmap As Bitmap
        Get
            Return Bitmap
        End Get
    End Property
    Sub New(ByVal _Image As Bitmap)
        MyBase.New(_Image.Width, _Image.Height)
        Bitmap = _Image
    End Sub
    Sub New(ByVal _Length As Long, ByVal MaxWidth As Integer)
        MyBase.New(_Length, MaxWidth)
        Bitmap = New Bitmap(Width, Height)
    End Sub
    Public Overrides Sub SetPixel(e As Pixel)
        Dim P As Color = Bitmap.GetPixel(e.X, e.Y)
        If e.Index = 1 Then
            Bitmap.SetPixel(e.X, e.Y, Color.FromArgb(255, e.Value, P.G, P.B))
        ElseIf e.Index = 2 Then
            Bitmap.SetPixel(e.X, e.Y, Color.FromArgb(255, P.R, e.Value, P.B))
        ElseIf e.Index = 3 Then
            Bitmap.SetPixel(e.X, e.Y, Color.FromArgb(255, P.R, P.G, e.Value))
        End If
    End Sub
    Public Overrides Sub GetPixel(e As Pixel)
        Dim P As Color = Bitmap.GetPixel(e.X, e.Y)
        If e.Index = 1 Then
            e.Value = P.R
        ElseIf e.Index = 2 Then
            e.Value = P.G
        ElseIf e.Index = 3 Then
            e.Value = P.B
        End If
    End Sub
End Class
