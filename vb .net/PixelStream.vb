Imports System.IO

Public MustInherit Class PixelStream
    Inherits Stream

    Private Location As Point

    Public Width, Height As Integer

    Public Sub New(Width As Integer, Height As Integer)
        Me.Width = Width
        Me.Height = Height
    End Sub

    Sub New(ByVal _Length As Long, ByVal MaxWidth As Integer)
        Dim A As Integer = _Length / 3
        If _Length < MaxWidth Then
            Height = 1
            Width = A + 1
        Else
            Height = A / MaxWidth
            If Height = 0 Then
                Height = 1
                Width = A + 1
            Else
                Width = MaxWidth
            End If
        End If
        Height += +1
    End Sub

    Private Structure Point
        Sub New(X As Integer, Y As Integer)
            Me.X = X
            Me.Y = Y
        End Sub
        Public X, Y As Integer
    End Structure

#Region "Property"

    Public Overrides ReadOnly Property CanRead As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property CanSeek As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CanWrite As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property Length As Long
        Get
            Return ImageWidth() * Height
        End Get
    End Property

    Public Overrides Property Position As Long
        Get
            Return PointToInteger(Location, ImageWidth)
        End Get
        Set(value As Long)
            Location = IntegerToPoint(value, ImageWidth)
        End Set
    End Property
#End Region

    Private Function IntegerToPoint(Value As Integer, Width As Integer) As Point
        Dim X, Y As Integer
        Dim vlr As Integer = Value
        While True
            If vlr < Width Then Exit While
            vlr += -Width
            Y += +1
        End While
        X = vlr
        Return New Point(X, Y)
    End Function

    Private Function PointToInteger(Value As Point, Width As Integer) As Integer
        Return (Width * Value.Y) + Value.X
    End Function

    Private Function ImageWidth() As Integer
        Return Width * 3
    End Function

    Public Overrides Sub Flush()

    End Sub

    Public Overrides Sub SetLength(value As Long)

    End Sub

    Private Function GetPixel() As Pixel
        Dim P As Point = IntegerToPoint(Location.X, 3)
        Return New Pixel(P.Y, Location.Y, P.X + 1)
    End Function

    Public Overrides Sub Write(buffer() As Byte, offset As Integer, count As Integer)
        Dim Total = count
        For i = offset To Total - 1
            WriteByte(buffer(i))
        Next
    End Sub

    Public Overrides Function Seek(offset As Long, origin As SeekOrigin) As Long

    End Function

    Public Overrides Function Read(buffer() As Byte, offset As Integer, count As Integer) As Integer
        Dim SD As Integer
        Dim Total = count - 1
        For i = offset To Total
            Dim A = Total - i
            buffer(i) = ReadByte()
            SD += +1
        Next
        Return SD
    End Function

    Public Overrides Function ReadByte() As Integer
        Dim Args As Pixel = GetPixel()
        GetPixel(Args)
        Position += +1
        Return Args.Value
    End Function


    Public Overrides Sub WriteByte(value As Byte)
        Dim Args As Pixel = GetPixel()
        Args.Value = value
        SetPixel(Args)
        Position += +1
    End Sub

    Public MustOverride Sub SetPixel(e As Pixel)
    Public MustOverride Sub GetPixel(e As Pixel)

End Class

Public Class Pixel
    Public Sub New(X As Integer, Y As Integer, Index As Integer)
        _X = X
        _Y = Y
        _Index = Index
    End Sub
    Private _X As Integer
    Public ReadOnly Property X As Integer
        Get
            Return _X
        End Get
    End Property
    Private _Y As Integer
    Public ReadOnly Property Y As Integer
        Get
            Return _Y
        End Get
    End Property
    Public Property Value As Byte
    Private _Index As Integer
    Public ReadOnly Property Index As Integer
        Get
            Return _Index
        End Get
    End Property

End Class