<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Browser
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Browser))
        TabControl1 = New TabControl()
        TabPage1 = New TabPage()
        FlowLayoutPanel1 = New FlowLayoutPanel()
        BtnBack = New Button()
        BtnRefresh = New Button()
        BtnForward = New Button()
        TxtURL = New TextBox()
        BtnGo = New Button()
        Panel1 = New Panel()
        WebView2 = New Microsoft.Web.WebView2.WinForms.WebView2()
        MenuStrip1 = New MenuStrip()
        NewTabToolStripMenuItem = New ToolStripMenuItem()
        HistoryToolStripMenuItem = New ToolStripMenuItem()
        HistoryToolStripMenuItem1 = New ToolStripMenuItem()
        ViewHistoryToolStripMenuItem1 = New ToolStripMenuItem()
        ClearHistoryToolStripMenuItem1 = New ToolStripMenuItem()
        BookmarkToolStripMenuItem = New ToolStripMenuItem()
        ViewBookmarksToolStripMenuItem = New ToolStripMenuItem()
        AddToBookmarksToolStripMenuItem = New ToolStripMenuItem()
        ViewDownloadsToolStripMenuItem = New ToolStripMenuItem()
        CacheManagementToolStripMenuItem = New ToolStripMenuItem()
        ViewCacheToolStripMenuItem = New ToolStripMenuItem()
        DeleteCacheToolStripMenuItem = New ToolStripMenuItem()
        EditCacheToolStripMenuItem = New ToolStripMenuItem()
        SettingsToolStripMenuItem = New ToolStripMenuItem()
        AboutToolStripMenuItem = New ToolStripMenuItem()
        TabControl1.SuspendLayout()
        TabPage1.SuspendLayout()
        FlowLayoutPanel1.SuspendLayout()
        Panel1.SuspendLayout()
        CType(WebView2, ComponentModel.ISupportInitialize).BeginInit()
        MenuStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Dock = DockStyle.Fill
        TabControl1.DrawMode = TabDrawMode.OwnerDrawFixed
        TabControl1.Location = New Point(0, 24)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(800, 579)
        TabControl1.SizeMode = TabSizeMode.Fixed
        TabControl1.TabIndex = 0
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(FlowLayoutPanel1)
        TabPage1.Controls.Add(Panel1)
        TabPage1.Location = New Point(4, 24)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3)
        TabPage1.Size = New Size(792, 551)
        TabPage1.TabIndex = 0
        TabPage1.Text = "TabPage1"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' FlowLayoutPanel1
        ' 
        FlowLayoutPanel1.Controls.Add(BtnBack)
        FlowLayoutPanel1.Controls.Add(BtnRefresh)
        FlowLayoutPanel1.Controls.Add(BtnForward)
        FlowLayoutPanel1.Controls.Add(TxtURL)
        FlowLayoutPanel1.Controls.Add(BtnGo)
        FlowLayoutPanel1.Dock = DockStyle.Top
        FlowLayoutPanel1.Location = New Point(3, 3)
        FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        FlowLayoutPanel1.Size = New Size(786, 34)
        FlowLayoutPanel1.TabIndex = 7
        ' 
        ' BtnBack
        ' 
        BtnBack.Location = New Point(3, 3)
        BtnBack.Name = "BtnBack"
        BtnBack.Size = New Size(66, 23)
        BtnBack.TabIndex = 0
        BtnBack.Text = "Back"
        BtnBack.UseVisualStyleBackColor = True
        ' 
        ' BtnRefresh
        ' 
        BtnRefresh.Location = New Point(75, 3)
        BtnRefresh.Name = "BtnRefresh"
        BtnRefresh.Size = New Size(56, 23)
        BtnRefresh.TabIndex = 1
        BtnRefresh.Text = "Refresh"
        BtnRefresh.UseVisualStyleBackColor = True
        ' 
        ' BtnForward
        ' 
        BtnForward.Location = New Point(137, 3)
        BtnForward.Name = "BtnForward"
        BtnForward.Size = New Size(61, 23)
        BtnForward.TabIndex = 2
        BtnForward.Text = "Forward"
        BtnForward.UseVisualStyleBackColor = True
        ' 
        ' TxtURL
        ' 
        TxtURL.Location = New Point(204, 3)
        TxtURL.Name = "TxtURL"
        TxtURL.Size = New Size(447, 23)
        TxtURL.TabIndex = 3
        ' 
        ' BtnGo
        ' 
        BtnGo.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        BtnGo.Location = New Point(657, 3)
        BtnGo.Name = "BtnGo"
        BtnGo.Size = New Size(56, 23)
        BtnGo.TabIndex = 4
        BtnGo.Text = "Go"
        BtnGo.UseVisualStyleBackColor = True
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(WebView2)
        Panel1.Location = New Point(3, 43)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(781, 524)
        Panel1.TabIndex = 6
        ' 
        ' WebView2
        ' 
        WebView2.AllowExternalDrop = True
        WebView2.CreationProperties = Nothing
        WebView2.DefaultBackgroundColor = Color.White
        WebView2.Location = New Point(5, 3)
        WebView2.Name = "WebView2"
        WebView2.Size = New Size(773, 526)
        WebView2.TabIndex = 5
        WebView2.ZoomFactor = 1R
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.Items.AddRange(New ToolStripItem() {NewTabToolStripMenuItem, HistoryToolStripMenuItem, SettingsToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(800, 24)
        MenuStrip1.TabIndex = 6
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' NewTabToolStripMenuItem
        ' 
        NewTabToolStripMenuItem.Name = "NewTabToolStripMenuItem"
        NewTabToolStripMenuItem.Size = New Size(64, 20)
        NewTabToolStripMenuItem.Text = "New Tab"
        ' 
        ' HistoryToolStripMenuItem
        ' 
        HistoryToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {HistoryToolStripMenuItem1, BookmarkToolStripMenuItem, ViewDownloadsToolStripMenuItem, CacheManagementToolStripMenuItem})
        HistoryToolStripMenuItem.Name = "HistoryToolStripMenuItem"
        HistoryToolStripMenuItem.Size = New Size(135, 20)
        HistoryToolStripMenuItem.Text = "Browser Management"
        ' 
        ' HistoryToolStripMenuItem1
        ' 
        HistoryToolStripMenuItem1.DropDownItems.AddRange(New ToolStripItem() {ViewHistoryToolStripMenuItem1, ClearHistoryToolStripMenuItem1})
        HistoryToolStripMenuItem1.Name = "HistoryToolStripMenuItem1"
        HistoryToolStripMenuItem1.Size = New Size(181, 22)
        HistoryToolStripMenuItem1.Text = "History"
        ' 
        ' ViewHistoryToolStripMenuItem1
        ' 
        ViewHistoryToolStripMenuItem1.Name = "ViewHistoryToolStripMenuItem1"
        ViewHistoryToolStripMenuItem1.Size = New Size(142, 22)
        ViewHistoryToolStripMenuItem1.Text = "View History"
        ' 
        ' ClearHistoryToolStripMenuItem1
        ' 
        ClearHistoryToolStripMenuItem1.Name = "ClearHistoryToolStripMenuItem1"
        ClearHistoryToolStripMenuItem1.Size = New Size(142, 22)
        ClearHistoryToolStripMenuItem1.Text = "Clear History"
        ' 
        ' BookmarkToolStripMenuItem
        ' 
        BookmarkToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ViewBookmarksToolStripMenuItem, AddToBookmarksToolStripMenuItem})
        BookmarkToolStripMenuItem.Name = "BookmarkToolStripMenuItem"
        BookmarkToolStripMenuItem.Size = New Size(181, 22)
        BookmarkToolStripMenuItem.Text = "Bookmarks"
        ' 
        ' ViewBookmarksToolStripMenuItem
        ' 
        ViewBookmarksToolStripMenuItem.Name = "ViewBookmarksToolStripMenuItem"
        ViewBookmarksToolStripMenuItem.Size = New Size(161, 22)
        ViewBookmarksToolStripMenuItem.Text = "View Bookmarks"
        ' 
        ' AddToBookmarksToolStripMenuItem
        ' 
        AddToBookmarksToolStripMenuItem.Name = "AddToBookmarksToolStripMenuItem"
        AddToBookmarksToolStripMenuItem.Size = New Size(161, 22)
        AddToBookmarksToolStripMenuItem.Text = "Bookmark Page"
        ' 
        ' ViewDownloadsToolStripMenuItem
        ' 
        ViewDownloadsToolStripMenuItem.Name = "ViewDownloadsToolStripMenuItem"
        ViewDownloadsToolStripMenuItem.Size = New Size(181, 22)
        ViewDownloadsToolStripMenuItem.Text = "View Downloads"
        ' 
        ' CacheManagementToolStripMenuItem
        ' 
        CacheManagementToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ViewCacheToolStripMenuItem, DeleteCacheToolStripMenuItem, EditCacheToolStripMenuItem})
        CacheManagementToolStripMenuItem.Name = "CacheManagementToolStripMenuItem"
        CacheManagementToolStripMenuItem.Size = New Size(181, 22)
        CacheManagementToolStripMenuItem.Text = "Cache Management"
        ' 
        ' ViewCacheToolStripMenuItem
        ' 
        ViewCacheToolStripMenuItem.Name = "ViewCacheToolStripMenuItem"
        ViewCacheToolStripMenuItem.Size = New Size(143, 22)
        ViewCacheToolStripMenuItem.Text = "View Cache"
        ' 
        ' DeleteCacheToolStripMenuItem
        ' 
        DeleteCacheToolStripMenuItem.Name = "DeleteCacheToolStripMenuItem"
        DeleteCacheToolStripMenuItem.Size = New Size(143, 22)
        DeleteCacheToolStripMenuItem.Text = "Delete Cache"
        ' 
        ' EditCacheToolStripMenuItem
        ' 
        EditCacheToolStripMenuItem.Name = "EditCacheToolStripMenuItem"
        EditCacheToolStripMenuItem.Size = New Size(143, 22)
        EditCacheToolStripMenuItem.Text = "Edit Cache"
        ' 
        ' SettingsToolStripMenuItem
        ' 
        SettingsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {AboutToolStripMenuItem})
        SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        SettingsToolStripMenuItem.Size = New Size(61, 20)
        SettingsToolStripMenuItem.Text = "Settings"
        ' 
        ' AboutToolStripMenuItem
        ' 
        AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        AboutToolStripMenuItem.Size = New Size(107, 22)
        AboutToolStripMenuItem.Text = "About"
        ' 
        ' Browser
        ' 
        AcceptButton = BtnGo
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 603)
        Controls.Add(TabControl1)
        Controls.Add(MenuStrip1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MainMenuStrip = MenuStrip1
        Name = "Browser"
        Text = "FBrowser"
        TabControl1.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        FlowLayoutPanel1.ResumeLayout(False)
        FlowLayoutPanel1.PerformLayout()
        Panel1.ResumeLayout(False)
        CType(WebView2, ComponentModel.ISupportInitialize).EndInit()
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TxtURL As TextBox
    Friend WithEvents BtnGo As Button
    Friend WithEvents BtnForward As Button
    Friend WithEvents BtnRefresh As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents WebView2 As Microsoft.Web.WebView2.WinForms.WebView2
    Friend WithEvents Panel1 As Panel
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HistoryToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NewTabToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HistoryToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ViewHistoryToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ClearHistoryToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents BookmarkToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ViewBookmarksToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AddToBookmarksToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ViewDownloadsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CacheManagementToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ViewCacheToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DeleteCacheToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EditCacheToolStripMenuItem As ToolStripMenuItem

End Class
