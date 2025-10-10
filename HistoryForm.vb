Public Class HistoryForm
    Private historyList As List(Of HistoryItem)
    Private browserInstance As Browser ' Reference to the Browser form

    ' Constructor to accept the Browser instance
    Public Sub New(browser As Browser)
        browserInstance = browser ' Store the reference
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
        ClearHistoryHandler()
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        ' Refresh the history list
        historyList = BrowserHistoryManager.LoadHistory()
        ListBox1.Items.Clear()
        For Each item In historyList
            ListBox1.Items.Add(item.ToString())
        Next
    End Sub

    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Me.Close()
    End Sub

    Public Sub ClearHistoryHandler()
        historyList.Clear()
        ListBox1.Items.Clear()

        If browserInstance IsNot Nothing Then
            browserInstance.ClearHistoryData()
        Else
            BrowserHistoryManager.ClearHistory()
        End If
    End Sub

    Private Sub ListBox1_DoubleClick(sender As Object, e As EventArgs) Handles ListBox1.DoubleClick
        ' Ensure an item is selected
        If ListBox1.SelectedIndex <> -1 Then
            ' Get the selected item
            Dim selectedItem As String = ListBox1.SelectedItem.ToString()

            ' Extract the URL from the selected item
            ' The URL is inside parentheses, e.g., "(https://chat.deepseek.com/)"
            Dim startIndex As Integer = selectedItem.IndexOf("(") + 1
            Dim endIndex As Integer = selectedItem.IndexOf(")")
            If startIndex > 0 AndAlso endIndex > startIndex Then
                Dim url As String = selectedItem.Substring(startIndex, endIndex - startIndex)

                ' Open the URL in a new tab using the Browser instance
                If browserInstance IsNot Nothing Then
                    browserInstance.AddNewTab(url)
                Else
                    MessageBox.Show("Browser instance not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                MessageBox.Show("No valid URL found in the selected history item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub
End Class