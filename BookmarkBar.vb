Public Class BookmarkBar

    Public Event BookmarkClicked(url As String)

    Private Sub BtnAddBookmark_Click(sender As Object, e As EventArgs) Handles BtnAddBookmark.Click
        Dim url As String = InputBox("Enter the URL:", "Add Bookmark")
        If Not String.IsNullOrWhiteSpace(url) Then
            AddBookmark(url)
        End If
    End Sub

    Public Sub AddBookmark(url As String)
        Dim btn As New Button() With {
            .Text = url,
            .AutoSize = True,
            .Tag = url
        }
        AddHandler btn.Click, AddressOf Bookmark_Click
        FlowLayoutPanel1.Controls.Add(btn)
    End Sub

    Private Sub Bookmark_Click(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim url As String = CType(btn.Tag, String)
        RaiseEvent BookmarkClicked(url)
    End Sub

End Class
