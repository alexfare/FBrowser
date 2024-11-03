Public Class BookmarkItem
    Public Property Url As String
    Public Property Title As String
    Public Property DateAdded As DateTime

    Public Sub New(url As String, title As String, dateAdded As DateTime)
        Me.Url = url
        Me.Title = title
        Me.DateAdded = dateAdded
    End Sub
End Class
