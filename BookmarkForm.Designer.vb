<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BookmarkForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BookmarkForm))
        Me.ListViewBookmarks = New System.Windows.Forms.ListView()
        Me.ColumnHeaderTitle = New System.Windows.Forms.ColumnHeader()
        Me.ColumnHeaderUrl = New System.Windows.Forms.ColumnHeader()
        Me.ColumnHeaderDate = New System.Windows.Forms.ColumnHeader()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.OpenInCurrentTabToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenInNewTabToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.RenameBookmarkToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteBookmarkToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BookmarksToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PanelButtons = New System.Windows.Forms.Panel()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.BtnDelete = New System.Windows.Forms.Button()
        Me.BtnRename = New System.Windows.Forms.Button()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.PanelButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListViewBookmarks
        '
        Me.ListViewBookmarks.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeaderTitle, Me.ColumnHeaderUrl, Me.ColumnHeaderDate})
        Me.ListViewBookmarks.ContextMenuStrip = Me.ContextMenuStrip1
        Me.ListViewBookmarks.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewBookmarks.FullRowSelect = True
        Me.ListViewBookmarks.HideSelection = False
        Me.ListViewBookmarks.Location = New System.Drawing.Point(0, 24)
        Me.ListViewBookmarks.MultiSelect = False
        Me.ListViewBookmarks.Name = "ListViewBookmarks"
        Me.ListViewBookmarks.Size = New System.Drawing.Size(684, 362)
        Me.ListViewBookmarks.TabIndex = 0
        Me.ListViewBookmarks.UseCompatibleStateImageBehavior = False
        Me.ListViewBookmarks.View = System.Windows.Forms.View.Details
        '
        'ColumnHeaderTitle
        '
        Me.ColumnHeaderTitle.Text = "Title"
        Me.ColumnHeaderTitle.Width = 200
        '
        'ColumnHeaderUrl
        '
        Me.ColumnHeaderUrl.Text = "URL"
        Me.ColumnHeaderUrl.Width = 320
        '
        'ColumnHeaderDate
        '
        Me.ColumnHeaderDate.Text = "Added"
        Me.ColumnHeaderDate.Width = 140
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenInCurrentTabToolStripMenuItem, Me.OpenInNewTabToolStripMenuItem, Me.ToolStripSeparator1, Me.RenameBookmarkToolStripMenuItem, Me.DeleteBookmarkToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(188, 98)
        '
        'OpenInCurrentTabToolStripMenuItem
        '
        Me.OpenInCurrentTabToolStripMenuItem.Name = "OpenInCurrentTabToolStripMenuItem"
        Me.OpenInCurrentTabToolStripMenuItem.Size = New System.Drawing.Size(187, 22)
        Me.OpenInCurrentTabToolStripMenuItem.Text = "Open in Current Tab"
        '
        'OpenInNewTabToolStripMenuItem
        '
        Me.OpenInNewTabToolStripMenuItem.Name = "OpenInNewTabToolStripMenuItem"
        Me.OpenInNewTabToolStripMenuItem.Size = New System.Drawing.Size(187, 22)
        Me.OpenInNewTabToolStripMenuItem.Text = "Open in New Tab"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(184, 6)
        '
        'RenameBookmarkToolStripMenuItem
        '
        Me.RenameBookmarkToolStripMenuItem.Name = "RenameBookmarkToolStripMenuItem"
        Me.RenameBookmarkToolStripMenuItem.Size = New System.Drawing.Size(187, 22)
        Me.RenameBookmarkToolStripMenuItem.Text = "Rename"
        '
        'DeleteBookmarkToolStripMenuItem
        '
        Me.DeleteBookmarkToolStripMenuItem.Name = "DeleteBookmarkToolStripMenuItem"
        Me.DeleteBookmarkToolStripMenuItem.Size = New System.Drawing.Size(187, 22)
        Me.DeleteBookmarkToolStripMenuItem.Text = "Delete"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.BookmarksToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(684, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CloseToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'CloseToolStripMenuItem
        '
        Me.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem"
        Me.CloseToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.CloseToolStripMenuItem.Text = "Close"
        '
        'BookmarksToolStripMenuItem
        '
        Me.BookmarksToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefreshToolStripMenuItem})
        Me.BookmarksToolStripMenuItem.Name = "BookmarksToolStripMenuItem"
        Me.BookmarksToolStripMenuItem.Size = New System.Drawing.Size(78, 20)
        Me.BookmarksToolStripMenuItem.Text = "Bookmarks"
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(113, 22)
        Me.RefreshToolStripMenuItem.Text = "Refresh"
        '
        'PanelButtons
        '
        Me.PanelButtons.Controls.Add(Me.BtnClose)
        Me.PanelButtons.Controls.Add(Me.BtnDelete)
        Me.PanelButtons.Controls.Add(Me.BtnRename)
        Me.PanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelButtons.Location = New System.Drawing.Point(0, 386)
        Me.PanelButtons.Name = "PanelButtons"
        Me.PanelButtons.Padding = New System.Windows.Forms.Padding(8)
        Me.PanelButtons.Size = New System.Drawing.Size(684, 60)
        Me.PanelButtons.TabIndex = 2
        '
        'BtnClose
        '
        Me.BtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClose.Location = New System.Drawing.Point(587, 17)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(75, 27)
        Me.BtnClose.TabIndex = 2
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'BtnDelete
        '
        Me.BtnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnDelete.Location = New System.Drawing.Point(506, 17)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(75, 27)
        Me.BtnDelete.TabIndex = 1
        Me.BtnDelete.Text = "Delete"
        Me.BtnDelete.UseVisualStyleBackColor = True
        '
        'BtnRename
        '
        Me.BtnRename.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnRename.Location = New System.Drawing.Point(425, 17)
        Me.BtnRename.Name = "BtnRename"
        Me.BtnRename.Size = New System.Drawing.Size(75, 27)
        Me.BtnRename.TabIndex = 0
        Me.BtnRename.Text = "Rename"
        Me.BtnRename.UseVisualStyleBackColor = True
        '
        'BookmarkForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(684, 446)
        Me.Controls.Add(Me.ListViewBookmarks)
        Me.Controls.Add(Me.PanelButtons)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MinimumSize = New System.Drawing.Size(500, 320)
        Me.Name = "BookmarkForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Bookmarks"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.PanelButtons.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ListViewBookmarks As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeaderTitle As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeaderUrl As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeaderDate As System.Windows.Forms.ColumnHeader
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents OpenInCurrentTabToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenInNewTabToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RenameBookmarkToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteBookmarkToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CloseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BookmarksToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PanelButtons As System.Windows.Forms.Panel
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents BtnDelete As System.Windows.Forms.Button
    Friend WithEvents BtnRename As System.Windows.Forms.Button
End Class