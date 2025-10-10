Imports System.IO
Imports System.Linq
Imports System.Text

Public Class BookmarkManager
    Private Shared ReadOnly bookmarkFilePath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Temp\FBrowser\bookmarks.csv")

    Public Shared Sub SaveBookmark(url As String, title As String)
        If String.IsNullOrWhiteSpace(url) Then
            Throw New ArgumentException("URL cannot be empty.", NameOf(url))
        End If

        Dim sanitizedTitle = If(String.IsNullOrWhiteSpace(title), url, title.Trim())
        Dim bookmarks = LoadBookmarks()
        Dim existing = bookmarks.FirstOrDefault(Function(b) String.Equals(b.Url, url, StringComparison.OrdinalIgnoreCase))

        If existing IsNot Nothing Then
            existing.Title = sanitizedTitle
            existing.DateAdded = DateTime.Now
        Else
            bookmarks.Add(New BookmarkItem(url, sanitizedTitle, DateTime.Now))
        End If

        WriteAllBookmarks(bookmarks)
    End Sub

    Public Shared Function LoadBookmarks() As List(Of BookmarkItem)
        EnsureDirectoryAndFileExist()
        Dim bookmarkList As New List(Of BookmarkItem)()

        For Each line In File.ReadAllLines(bookmarkFilePath)
            If String.IsNullOrWhiteSpace(line) Then
                Continue For
            End If

            Dim bookmark = ParseLine(line)
            If bookmark IsNot Nothing Then
                bookmarkList.Add(bookmark)
            End If
        Next

        Return bookmarkList.OrderByDescending(Function(b) b.DateAdded).ToList()
    End Function

    Public Shared Function RenameBookmark(url As String, newTitle As String) As Boolean
        If String.IsNullOrWhiteSpace(url) Then
            Return False
        End If

        Dim bookmarks = LoadBookmarks()
        Dim bookmark = bookmarks.FirstOrDefault(Function(b) String.Equals(b.Url, url, StringComparison.OrdinalIgnoreCase))
        If bookmark Is Nothing Then
            Return False
        End If

        bookmark.Title = If(String.IsNullOrWhiteSpace(newTitle), bookmark.Url, newTitle.Trim())
        WriteAllBookmarks(bookmarks)
        Return True
    End Function

    Public Shared Function DeleteBookmark(url As String) As Boolean
        If String.IsNullOrWhiteSpace(url) Then
            Return False
        End If

        Dim bookmarks = LoadBookmarks()
        Dim removed = bookmarks.RemoveAll(Function(b) String.Equals(b.Url, url, StringComparison.OrdinalIgnoreCase))
        If removed > 0 Then
            WriteAllBookmarks(bookmarks)
            Return True
        End If

        Return False
    End Function

    Private Shared Function ParseLine(line As String) As BookmarkItem
        Dim pipeParts = line.Split("|"c)
        If pipeParts.Length = 3 Then
            Dim dateAdded As DateTime
            Dim ticks As Long
            If Long.TryParse(pipeParts(0), ticks) Then
                dateAdded = New DateTime(ticks, DateTimeKind.Utc).ToLocalTime()
            ElseIf DateTime.TryParse(pipeParts(0), dateAdded) Then
                ' Parsed successfully as DateTime
            Else
                dateAdded = DateTime.Now
            End If

            Dim title = DecodeBase64(pipeParts(1))
            Dim url = DecodeBase64(pipeParts(2))
            If String.IsNullOrWhiteSpace(url) Then
                Return Nothing
            End If

            If String.IsNullOrWhiteSpace(title) Then
                title = url
            End If

            Return New BookmarkItem(url, title, dateAdded)
        End If

        Dim csvParts = line.Split(","c)
        If csvParts.Length = 3 Then
            Dim dateAdded As DateTime
            If Not DateTime.TryParse(csvParts(0), dateAdded) Then
                dateAdded = DateTime.Now
            End If

            Dim title = csvParts(1)
            Dim url = csvParts(2)
            If String.IsNullOrWhiteSpace(url) Then
                Return Nothing
            End If

            If String.IsNullOrWhiteSpace(title) Then
                title = url
            End If

            Return New BookmarkItem(url, title, dateAdded)
        End If

        Return Nothing
    End Function

    Private Shared Function DecodeBase64(value As String) As String
        Try
            Dim bytes = Convert.FromBase64String(value)
            Return Encoding.UTF8.GetString(bytes)
        Catch
            Return value
        End Try
    End Function

    Private Shared Function EncodeBase64(value As String) As String
        Dim bytes = Encoding.UTF8.GetBytes(value)
        Return Convert.ToBase64String(bytes)
    End Function

    Private Shared Sub WriteAllBookmarks(bookmarks As IEnumerable(Of BookmarkItem))
        EnsureDirectoryAndFileExist()
        Using writer As New StreamWriter(bookmarkFilePath, False)
            For Each bookmark In bookmarks
                Dim dateTicks = bookmark.DateAdded.ToUniversalTime().Ticks
                Dim titleEncoded = EncodeBase64(If(bookmark.Title, bookmark.Url))
                Dim urlEncoded = EncodeBase64(bookmark.Url)
                writer.WriteLine($"{dateTicks}|{titleEncoded}|{urlEncoded}")
            Next
        End Using
    End Sub

    Private Shared Sub EnsureDirectoryAndFileExist()
        Dim folderPath As String = Path.GetDirectoryName(bookmarkFilePath)
        If Not Directory.Exists(folderPath) Then
            Directory.CreateDirectory(folderPath)
        End If

        If Not File.Exists(bookmarkFilePath) Then
            Using File.Create(bookmarkFilePath)
            End Using
        End If
    End Sub
End Class