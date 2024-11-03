Public Class HistoryItem
    Public Property URL As String
    Public Property Title As String
    Public Property VisitDate As DateTime

    Public Sub New(url As String, title As String, visitDate As DateTime)
        Me.URL = url
        Me.Title = title
        Me.VisitDate = visitDate
    End Sub

    Public Overrides Function ToString() As String
        Return $"{VisitDate}: {Title} ({URL})"
    End Function
End Class