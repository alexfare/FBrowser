Imports System.IO
Imports System.Diagnostics
Imports System.Threading

Public Class BrowserHistoryManager
    Private Shared historyFilePath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Temp\FBrowser\history.csv")

    Private Shared Sub EnsureDirectoryAndFileExist()
        Dim folderPath As String = Path.GetDirectoryName(historyFilePath)
        If Not Directory.Exists(folderPath) Then
            Directory.CreateDirectory(folderPath)
        End If

        If Not File.Exists(historyFilePath) Then
            File.Create(historyFilePath).Dispose()
        End If
    End Sub

    Public Shared Sub SaveHistory(historyList As List(Of HistoryItem))
        EnsureDirectoryAndFileExist()

        Using writer As New StreamWriter(historyFilePath, False)
            For Each item In historyList
                writer.WriteLine($"{item.VisitDate},{item.Title},{item.URL}")
            Next
        End Using
    End Sub

    Public Shared Function LoadHistory() As List(Of HistoryItem)
        EnsureDirectoryAndFileExist()

        Dim historyList As New List(Of HistoryItem)

        Using reader As New StreamReader(historyFilePath)
            While Not reader.EndOfStream
                Dim line As String = reader.ReadLine()
                If Not String.IsNullOrEmpty(line) Then
                    Dim data() As String = line.Split(","c)
                    If data.Length = 3 Then
                        Dim visitDate As DateTime
                        If DateTime.TryParse(data(0), visitDate) Then
                            historyList.Add(New HistoryItem(data(2), data(1), visitDate))
                        End If
                    End If
                End If
            End While
        End Using

        Return historyList
    End Function

    Public Shared Sub ClearHistory()
        EnsureDirectoryAndFileExist()
        File.WriteAllText(historyFilePath, String.Empty) 'Clear the file
    End Sub



End Class
