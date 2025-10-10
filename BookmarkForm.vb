Imports Microsoft.VisualBasic

Public Class BookmarkForm
    Private bookmarkList As List(Of BookmarkItem)
    Private ReadOnly browserInstance As Browser

    Public Sub New(browser As Browser)
        browserInstance = browser
        InitializeComponent()
    End Sub

    Private Sub BookmarkForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshBookmarks()
    End Sub

    Private Sub RefreshBookmarks()
        bookmarkList = BookmarkManager.LoadBookmarks()
        ListViewBookmarks.Items.Clear()

        For Each bookmark In bookmarkList
            Dim item As New ListViewItem(bookmark.Title)
            item.SubItems.Add(bookmark.Url)
            item.SubItems.Add(bookmark.DateAdded.ToString("g"))
            item.Tag = bookmark
            ListViewBookmarks.Items.Add(item)
        Next
    End Sub

    Private Function GetSelectedBookmark() As BookmarkItem
        If ListViewBookmarks.SelectedItems.Count = 0 Then
            Return Nothing
        End If

        Return TryCast(ListViewBookmarks.SelectedItems(0).Tag, BookmarkItem)
    End Function

    Private Async Sub OpenBookmarkAsync(openInNewTab As Boolean)
        Dim bookmark = GetSelectedBookmark()
        If bookmark Is Nothing Then
            MessageBox.Show("Select a bookmark first.", "Bookmarks", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If browserInstance Is Nothing Then
            MessageBox.Show("Browser instance not available.", "Bookmarks", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If openInNewTab Then
            Await browserInstance.AddNewTab(bookmark.Url)
        Else
            Await browserInstance.OpenBookmarkAsync(bookmark.Url)
        End If

        Close()
    End Sub

    Private Sub RenameSelectedBookmark()
        Dim bookmark = GetSelectedBookmark()
        If bookmark Is Nothing Then
            MessageBox.Show("Select a bookmark first.", "Bookmarks", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim newTitle = Interaction.InputBox("Enter a new name for this bookmark:", "Rename Bookmark", bookmark.Title)
        If newTitle Is Nothing Then
            Return
        End If

        newTitle = newTitle.Trim()
        If newTitle.Length = 0 Then
            MessageBox.Show("Bookmark name cannot be empty.", "Bookmarks", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If BookmarkManager.RenameBookmark(bookmark.Url, newTitle) Then
            RefreshBookmarks()
        Else
            MessageBox.Show("Failed to rename bookmark.", "Bookmarks", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub DeleteSelectedBookmark()
        Dim bookmark = GetSelectedBookmark()
        If bookmark Is Nothing Then
            MessageBox.Show("Select a bookmark first.", "Bookmarks", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim confirmation = MessageBox.Show($"Delete bookmark '{bookmark.Title}'?", "Delete Bookmark", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmation <> DialogResult.Yes Then
            Return
        End If

        If BookmarkManager.DeleteBookmark(bookmark.Url) Then
            RefreshBookmarks()
        Else
            MessageBox.Show("Failed to delete bookmark.", "Bookmarks", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub OpenInCurrentTabToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenInCurrentTabToolStripMenuItem.Click
        OpenBookmarkAsync(False)
    End Sub

    Private Sub OpenInNewTabToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenInNewTabToolStripMenuItem.Click
        OpenBookmarkAsync(True)
    End Sub

    Private Sub RenameBookmarkToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RenameBookmarkToolStripMenuItem.Click
        RenameSelectedBookmark()
    End Sub

    Private Sub DeleteBookmarkToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteBookmarkToolStripMenuItem.Click
        DeleteSelectedBookmark()
    End Sub

    Private Sub BtnRename_Click(sender As Object, e As EventArgs) Handles BtnRename.Click
        RenameSelectedBookmark()
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        DeleteSelectedBookmark()
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Close()
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        RefreshBookmarks()
    End Sub

    Private Sub ListViewBookmarks_DoubleClick(sender As Object, e As EventArgs) Handles ListViewBookmarks.DoubleClick
        OpenBookmarkAsync(True)
    End Sub

    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Close()
    End Sub
End Class