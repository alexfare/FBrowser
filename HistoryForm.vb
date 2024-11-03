Public Class HistoryForm
    Private historyList As List(Of HistoryItem)

    Public Sub New()
        ' Load history from the CSV file
        historyList = BrowserHistoryManager.LoadHistory()
        InitializeComponent()
    End Sub

    Private Sub HistoryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListBox1.Items.Clear()
        For Each item In historyList
            ListBox1.Items.Add(item.ToString())
        Next
    End Sub

    Private Sub ClearHistoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearHistoryToolStripMenuItem.Click
        ' Clear both the in-memory list and the CSV file
        historyList.Clear()
        BrowserHistoryManager.ClearHistory()
        ListBox1.Items.Clear()
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        BrowserHistoryManager.LoadHistory()
    End Sub

    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Me.Close()
    End Sub
End Class
