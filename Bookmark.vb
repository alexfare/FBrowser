Public Class Bookmark
    Public Property Url As String
    Public Property Name As String
    Public Property Icon As Image

    Public Sub New(url As String, name As String, icon As Image)
        Me.Url = url
        Me.Name = name
        Me.Icon = icon
    End Sub
End Class
