Imports System.IO
Imports Microsoft.VisualBasic.FileIO

Public Class BookmarkManager
    Public Shared bookmarkFilePath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Temp\FBrowser\bookmarks.csv")

    Public Shared Sub SaveBookmark(url As String, title As String)
        Dim dateAdded As DateTime = DateTime.Now

        ' Ensure directory and file exist
        EnsureDirectoryAndFileExist()

        ' Append bookmark to file
        Using writer As New StreamWriter(bookmarkFilePath, True)
            Dim fields = {
                EscapeCsvField(dateAdded.ToString("o")),
                EscapeCsvField(title),
                EscapeCsvField(url)
            }
            writer.WriteLine(String.Join(",", fields))
        End Using
    End Sub

    Public Shared Function LoadBookmarks() As List(Of BookmarkItem)
        Dim bookmarkList As New List(Of BookmarkItem)

        ' Ensure directory and file exist
        EnsureDirectoryAndFileExist()

        ' Read bookmarks from file
        Using parser As New TextFieldParser(bookmarkFilePath)
            parser.SetDelimiters(",")
            parser.HasFieldsEnclosedInQuotes = True

            While Not parser.EndOfData
                Dim data() As String = parser.ReadFields()
                If data IsNot Nothing AndAlso data.Length = 3 Then
                    Dim dateAdded As DateTime
                    If DateTime.TryParse(data(0), dateAdded) Then
                        bookmarkList.Add(New BookmarkItem(data(2), data(1), dateAdded))
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
    
    Private Shared Function EscapeCsvField(value As String) As String
        If value Is Nothing Then
            value = String.Empty
        End If

        Return """" & value.Replace("""", """"") & """"
    End Function
End Class
