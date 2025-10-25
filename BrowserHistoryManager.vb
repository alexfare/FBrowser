Imports System.IO
Imports System.Diagnostics
Imports System.Collections.Generic
Imports System.Text
Imports System.Globalization
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
                Dim fields = {
                    EscapeCsv(item.VisitDate.ToString("o", CultureInfo.InvariantCulture)),
                    EscapeCsv(item.Title),
                    EscapeCsv(item.URL)
                }

                writer.WriteLine(String.Join(",", fields))
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
                    Dim data() As String = ParseCsvLine(line)
                    If data.Length = 3 Then
                        Dim visitDate As DateTime
                        If DateTime.TryParse(data(0), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, visitDate) Then
                            historyList.Add(New HistoryItem(data(2), data(1), visitDate))
                        ElseIf DateTime.TryParse(data(0), visitDate) Then
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

    Private Shared Function EscapeCsv(value As String) As String
        If value Is Nothing Then
            Return String.Empty
        End If

        Dim needsQuotes = value.IndexOf(""""c) >= 0 OrElse value.IndexOf(","c) >= 0 OrElse value.Contains(ControlChars.Cr) OrElse value.Contains(ControlChars.Lf)
        Dim escaped = value.Replace("""", """""")

        If needsQuotes Then
            Return $"""{escaped}"""
        End If

        Return escaped
    End Function

    Private Shared Function ParseCsvLine(line As String) As String()
        Dim result As New List(Of String)()
        Dim current As New StringBuilder()
        Dim inQuotes As Boolean = False
        Dim i As Integer = 0

        While i < line.Length
            Dim ch = line.Chars(i)

            If inQuotes Then
                If ch = """"c Then
                    If i + 1 < line.Length AndAlso line.Chars(i + 1) = """"c Then
                        current.Append(""""c)
                        i += 1
                    Else
                        inQuotes = False
                    End If
                Else
                    current.Append(ch)
                End If
            Else
                If ch = """"c Then
                    inQuotes = True
                ElseIf ch = ","c Then
                    result.Add(current.ToString())
                    current.Clear()
                Else
                    current.Append(ch)
                End If
            End If

            i += 1
        End While

        result.Add(current.ToString())

        Return result.ToArray()
    End Function



End Class
