Public Class HistoryForm
    Private historyList As List(Of HistoryItem)

    Public Sub New(history As List(Of HistoryItem))
        InitializeComponent()
        historyList = history
    End Sub

    Private Sub HistoryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListBox1.Items.Clear()
        For Each item In historyList
            ListBox1.Items.Add(item)
        Next
    End Sub

    Private Sub btnClearHistory_Click(sender As Object, e As EventArgs) Handles btnClearHistory.Click
        historyList.Clear()
        ListBox1.Items.Clear()
    End Sub
End Class
