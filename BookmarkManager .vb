Imports System.IO

Public Class BookmarkManager
    Public Shared bookmarkFilePath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Temp\FBrowser\bookmarks.csv")

    Public Shared Sub SaveBookmark(url As String, title As String)
        Dim dateAdded As DateTime = DateTime.Now

        ' Ensure directory and file exist
        EnsureDirectoryAndFileExist()

        ' Append bookmark to file
        Using writer As New StreamWriter(bookmarkFilePath, True)
            writer.WriteLine($"{dateAdded},{title},{url}")
        End Using
    End Sub

    Public Shared Function LoadBookmarks() As List(Of BookmarkItem)
        Dim bookmarkList As New List(Of BookmarkItem)

        ' Ensure directory and file exist
        EnsureDirectoryAndFileExist()

        ' Read bookmarks from file
        Using reader As New StreamReader(bookmarkFilePath)
            While Not reader.EndOfStream
                Dim line As String = reader.ReadLine()
                If Not String.IsNullOrEmpty(line) Then
                    Dim data() As String = line.Split(","c)
                    If data.Length = 3 Then
                        Dim dateAdded As DateTime
                        If DateTime.TryParse(data(0), dateAdded) Then
                            bookmarkList.Add(New BookmarkItem(data(2), data(1), dateAdded))
                        End If
                    End If
                End If
            End While
        End Using

        Return bookmarkList
    End Function

    Private Shared Sub EnsureDirectoryAndFileExist()
        Dim folderPath As String = Path.GetDirectoryName(bookmarkFilePath)
        If Not Directory.Exists(folderPath) Then
            Directory.CreateDirectory(folderPath)
        End If

        If Not File.Exists(bookmarkFilePath) Then
            File.Create(bookmarkFilePath).Dispose()
        End If
    End Sub
End Class
