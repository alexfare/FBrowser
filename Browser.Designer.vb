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
        Panel1 = New Panel()
        WebView2 = New Microsoft.Web.WebView2.WinForms.WebView2()
        NavigationBar = New GradientPanel()
        NavLayout = New TableLayoutPanel()
        LblBrand = New Label()
        BtnBack = New Button()
        BtnForward = New Button()
        BtnRefresh = New Button()
        UrlContainer = New RoundedPanel()
        TxtURL = New TextBox()
        BtnGo = New Button()
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
        DeveloperToolsToolStripMenuItem = New ToolStripMenuItem()
        SettingsToolStripMenuItem = New ToolStripMenuItem()
        AboutToolStripMenuItem = New ToolStripMenuItem()
        TabControl1.SuspendLayout()
        TabPage1.SuspendLayout()
        Panel1.SuspendLayout()
        CType(WebView2, ComponentModel.ISupportInitialize).BeginInit()
        NavigationBar.SuspendLayout()
        NavLayout.SuspendLayout()
        UrlContainer.SuspendLayout()
        MenuStrip1.SuspendLayout()
        SuspendLayout()
        '
        ' TabControl1
        '
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Dock = DockStyle.Fill
        TabControl1.DrawMode = TabDrawMode.OwnerDrawFixed
        TabControl1.Font = New Font("Segoe UI", 10.0F, FontStyle.Regular, GraphicsUnit.Point)
        TabControl1.ItemSize = New Size(190, 42)
        TabControl1.Location = New Point(0, 40)
        TabControl1.Margin = New Padding(0)
        TabControl1.Name = "TabControl1"
        TabControl1.Padding = New Point(24, 6)
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(1280, 760)
        TabControl1.SizeMode = TabSizeMode.Fixed
        TabControl1.TabIndex = 0
        '
        ' TabPage1
        '
        TabPage1.BackColor = Color.FromArgb(18, 18, 20)
        TabPage1.Controls.Add(Panel1)
        TabPage1.Controls.Add(NavigationBar)
        TabPage1.Location = New Point(4, 46)
        TabPage1.Margin = New Padding(0)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(0)
        TabPage1.Size = New Size(1272, 710)
        TabPage1.TabIndex = 0
        TabPage1.Text = "Welcome"
        TabPage1.UseVisualStyleBackColor = False
        '
        ' Panel1
        '
        Panel1.BackColor = Color.FromArgb(18, 18, 20)
        Panel1.Controls.Add(WebView2)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(0, 84)
        Panel1.Margin = New Padding(0)
        Panel1.Name = "Panel1"
        Panel1.Padding = New Padding(0, 8, 0, 8)
        Panel1.Size = New Size(1272, 626)
        Panel1.TabIndex = 2
        '
        ' WebView2
        '
        WebView2.AllowExternalDrop = True
        WebView2.CreationProperties = Nothing
        WebView2.DefaultBackgroundColor = Color.FromArgb(24, 24, 28)
        WebView2.Dock = DockStyle.Fill
        WebView2.Location = New Point(0, 8)
        WebView2.Margin = New Padding(0)
        WebView2.Name = "WebView2"
        WebView2.Size = New Size(1272, 610)
        WebView2.TabIndex = 0
        WebView2.ZoomFactor = 1.0R
        '
        ' NavigationBar
        '
        NavigationBar.Controls.Add(NavLayout)
        NavigationBar.Dock = DockStyle.Top
        NavigationBar.GradientColor1 = Color.FromArgb(58, 58, 66)
        NavigationBar.GradientColor2 = Color.FromArgb(34, 34, 38)
        NavigationBar.GradientMode = Drawing2D.LinearGradientMode.Horizontal
        NavigationBar.Location = New Point(0, 0)
        NavigationBar.Margin = New Padding(0)
        NavigationBar.Name = "NavigationBar"
        NavigationBar.Padding = New Padding(20, 14, 20, 14)
        NavigationBar.Size = New Size(1272, 84)
        NavigationBar.TabIndex = 1
        '
        ' NavLayout
        '
        NavLayout.ColumnCount = 6
        NavLayout.ColumnStyles.Add(New ColumnStyle())
        NavLayout.ColumnStyles.Add(New ColumnStyle())
        NavLayout.ColumnStyles.Add(New ColumnStyle())
        NavLayout.ColumnStyles.Add(New ColumnStyle())
        NavLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        NavLayout.ColumnStyles.Add(New ColumnStyle())
        NavLayout.Controls.Add(LblBrand, 0, 0)
        NavLayout.Controls.Add(BtnBack, 1, 0)
        NavLayout.Controls.Add(BtnForward, 2, 0)
        NavLayout.Controls.Add(BtnRefresh, 3, 0)
        NavLayout.Controls.Add(UrlContainer, 4, 0)
        NavLayout.Controls.Add(BtnGo, 5, 0)
        NavLayout.Dock = DockStyle.Fill
        NavLayout.Location = New Point(20, 14)
        NavLayout.Margin = New Padding(0)
        NavLayout.Name = "NavLayout"
        NavLayout.RowCount = 1
        NavLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        NavLayout.Size = New Size(1232, 56)
        NavLayout.TabIndex = 0
        '
        ' LblBrand
        '
        LblBrand.Anchor = AnchorStyles.Left
        LblBrand.AutoSize = True
        LblBrand.Font = New Font("Segoe UI Semibold", 14.0F, FontStyle.Bold, GraphicsUnit.Point)
        LblBrand.ForeColor = Color.White
        LblBrand.Location = New Point(0, 14)
        LblBrand.Margin = New Padding(0, 0, 18, 0)
        LblBrand.Name = "LblBrand"
        LblBrand.Size = New Size(96, 28)
        LblBrand.TabIndex = 0
        LblBrand.Text = "FBrowser"
        '
        ' BtnBack
        '
        BtnBack.BackColor = Color.FromArgb(48, 48, 54)
        BtnBack.FlatAppearance.BorderSize = 0
        BtnBack.FlatStyle = FlatStyle.Flat
        BtnBack.Font = New Font("Segoe UI Symbol", 12.0F, FontStyle.Bold, GraphicsUnit.Point)
        BtnBack.ForeColor = Color.White
        BtnBack.Location = New Point(114, 0)
        BtnBack.Margin = New Padding(0, 0, 10, 0)
        BtnBack.Name = "BtnBack"
        BtnBack.Size = New Size(48, 56)
        BtnBack.TabIndex = 1
        BtnBack.Text = "←"
        BtnBack.UseVisualStyleBackColor = False
        '
        ' BtnForward
        '
        BtnForward.BackColor = Color.FromArgb(48, 48, 54)
        BtnForward.FlatAppearance.BorderSize = 0
        BtnForward.FlatStyle = FlatStyle.Flat
        BtnForward.Font = New Font("Segoe UI Symbol", 12.0F, FontStyle.Bold, GraphicsUnit.Point)
        BtnForward.ForeColor = Color.White
        BtnForward.Location = New Point(172, 0)
        BtnForward.Margin = New Padding(0, 0, 10, 0)
        BtnForward.Name = "BtnForward"
        BtnForward.Size = New Size(48, 56)
        BtnForward.TabIndex = 2
        BtnForward.Text = "→"
        BtnForward.UseVisualStyleBackColor = False
        '
        ' BtnRefresh
        '
        BtnRefresh.BackColor = Color.FromArgb(48, 48, 54)
        BtnRefresh.FlatAppearance.BorderSize = 0
        BtnRefresh.FlatStyle = FlatStyle.Flat
        BtnRefresh.Font = New Font("Segoe UI Symbol", 12.0F, FontStyle.Bold, GraphicsUnit.Point)
        BtnRefresh.ForeColor = Color.White
        BtnRefresh.Location = New Point(230, 0)
        BtnRefresh.Margin = New Padding(0, 0, 16, 0)
        BtnRefresh.Name = "BtnRefresh"
        BtnRefresh.Size = New Size(48, 56)
        BtnRefresh.TabIndex = 3
        BtnRefresh.Text = "⟳"
        BtnRefresh.UseVisualStyleBackColor = False
        '
        ' UrlContainer
        '
        UrlContainer.BorderColor = Color.FromArgb(72, 72, 78)
        UrlContainer.BorderThickness = 1
        UrlContainer.Controls.Add(TxtURL)
        UrlContainer.CornerRadius = 18
        UrlContainer.Dock = DockStyle.Fill
        UrlContainer.Location = New Point(294, 0)
        UrlContainer.Margin = New Padding(16, 0, 16, 0)
        UrlContainer.Name = "UrlContainer"
        UrlContainer.Padding = New Padding(18, 10, 18, 10)
        UrlContainer.Size = New Size(864, 56)
        UrlContainer.TabIndex = 4
        '
        ' TxtURL
        '
        TxtURL.BackColor = Color.FromArgb(32, 32, 36)
        TxtURL.BorderStyle = BorderStyle.None
        TxtURL.Dock = DockStyle.Fill
        TxtURL.Font = New Font("Segoe UI", 11.0F, FontStyle.Regular, GraphicsUnit.Point)
        TxtURL.ForeColor = Color.WhiteSmoke
        TxtURL.Location = New Point(18, 10)
        TxtURL.Margin = New Padding(0)
        TxtURL.Name = "TxtURL"
        TxtURL.Size = New Size(828, 20)
        TxtURL.TabIndex = 0
        '
        ' BtnGo
        '
        BtnGo.BackColor = Color.FromArgb(99, 102, 241)
        BtnGo.FlatAppearance.BorderSize = 0
        BtnGo.FlatStyle = FlatStyle.Flat
        BtnGo.Font = New Font("Segoe UI Semibold", 11.0F, FontStyle.Bold, GraphicsUnit.Point)
        BtnGo.ForeColor = Color.White
        BtnGo.Location = New Point(1174, 0)
        BtnGo.Margin = New Padding(0)
        BtnGo.Name = "BtnGo"
        BtnGo.Size = New Size(58, 56)
        BtnGo.TabIndex = 5
        BtnGo.Text = "Go"
        BtnGo.UseVisualStyleBackColor = False
        '
        ' MenuStrip1
        '
        MenuStrip1.BackColor = Color.FromArgb(34, 34, 38)
        MenuStrip1.Font = New Font("Segoe UI", 10.0F, FontStyle.Regular, GraphicsUnit.Point)
        MenuStrip1.ImageScalingSize = New Size(20, 20)
        MenuStrip1.Items.AddRange(New ToolStripItem() {NewTabToolStripMenuItem, HistoryToolStripMenuItem, SettingsToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Padding = New Padding(16, 6, 16, 6)
        MenuStrip1.Size = New Size(1280, 40)
        MenuStrip1.TabIndex = 1
        MenuStrip1.Text = "MenuStrip1"
        '
        ' NewTabToolStripMenuItem
        '
        NewTabToolStripMenuItem.ForeColor = Color.White
        NewTabToolStripMenuItem.Name = "NewTabToolStripMenuItem"
        NewTabToolStripMenuItem.Size = New Size(71, 28)
        NewTabToolStripMenuItem.Text = "New Tab"
        '
        ' HistoryToolStripMenuItem
        '
        HistoryToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {HistoryToolStripMenuItem1, BookmarkToolStripMenuItem, ViewDownloadsToolStripMenuItem, CacheManagementToolStripMenuItem, DeveloperToolsToolStripMenuItem})
        HistoryToolStripMenuItem.ForeColor = Color.White
        HistoryToolStripMenuItem.Name = "HistoryToolStripMenuItem"
        HistoryToolStripMenuItem.Size = New Size(151, 28)
        HistoryToolStripMenuItem.Text = "Browser Management"
        '
        ' HistoryToolStripMenuItem1
        '
        HistoryToolStripMenuItem1.DropDownItems.AddRange(New ToolStripItem() {ViewHistoryToolStripMenuItem1, ClearHistoryToolStripMenuItem1})
        HistoryToolStripMenuItem1.Name = "HistoryToolStripMenuItem1"
        HistoryToolStripMenuItem1.Size = New Size(181, 24)
        HistoryToolStripMenuItem1.Text = "History"
        '
        ' ViewHistoryToolStripMenuItem1
        '
        ViewHistoryToolStripMenuItem1.Name = "ViewHistoryToolStripMenuItem1"
        ViewHistoryToolStripMenuItem1.Size = New Size(150, 24)
        ViewHistoryToolStripMenuItem1.Text = "View History"
        '
        ' ClearHistoryToolStripMenuItem1
        '
        ClearHistoryToolStripMenuItem1.Name = "ClearHistoryToolStripMenuItem1"
        ClearHistoryToolStripMenuItem1.Size = New Size(150, 24)
        ClearHistoryToolStripMenuItem1.Text = "Clear History"
        '
        ' BookmarkToolStripMenuItem
        '
        BookmarkToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ViewBookmarksToolStripMenuItem, AddToBookmarksToolStripMenuItem})
        BookmarkToolStripMenuItem.Name = "BookmarkToolStripMenuItem"
        BookmarkToolStripMenuItem.Size = New Size(181, 24)
        BookmarkToolStripMenuItem.Text = "Bookmarks"
        '
        ' ViewBookmarksToolStripMenuItem
        '
        ViewBookmarksToolStripMenuItem.Name = "ViewBookmarksToolStripMenuItem"
        ViewBookmarksToolStripMenuItem.Size = New Size(173, 24)
        ViewBookmarksToolStripMenuItem.Text = "View Bookmarks"
        '
        ' AddToBookmarksToolStripMenuItem
        '
        AddToBookmarksToolStripMenuItem.Name = "AddToBookmarksToolStripMenuItem"
        AddToBookmarksToolStripMenuItem.Size = New Size(173, 24)
        AddToBookmarksToolStripMenuItem.Text = "Bookmark Page"
        '
        ' ViewDownloadsToolStripMenuItem
        '
        ViewDownloadsToolStripMenuItem.Name = "ViewDownloadsToolStripMenuItem"
        ViewDownloadsToolStripMenuItem.Size = New Size(181, 24)
        ViewDownloadsToolStripMenuItem.Text = "View Downloads"
        '
        ' CacheManagementToolStripMenuItem
        '
        CacheManagementToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ViewCacheToolStripMenuItem, DeleteCacheToolStripMenuItem})
        CacheManagementToolStripMenuItem.Name = "CacheManagementToolStripMenuItem"
        CacheManagementToolStripMenuItem.Size = New Size(181, 24)
        CacheManagementToolStripMenuItem.Text = "Cache Management"
        '
        ' ViewCacheToolStripMenuItem
        '
        ViewCacheToolStripMenuItem.Name = "ViewCacheToolStripMenuItem"
        ViewCacheToolStripMenuItem.Size = New Size(146, 24)
        ViewCacheToolStripMenuItem.Text = "View Cache"
        '
        ' DeleteCacheToolStripMenuItem
        '
        DeleteCacheToolStripMenuItem.Name = "DeleteCacheToolStripMenuItem"
        DeleteCacheToolStripMenuItem.Size = New Size(146, 24)
        DeleteCacheToolStripMenuItem.Text = "Delete Cache"
        '
        ' DeveloperToolsToolStripMenuItem
        '
        DeveloperToolsToolStripMenuItem.Name = "DeveloperToolsToolStripMenuItem"
        DeveloperToolsToolStripMenuItem.Size = New Size(181, 24)
        DeveloperToolsToolStripMenuItem.Text = "Developer Tools"
        '
        ' SettingsToolStripMenuItem
        '
        SettingsToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {AboutToolStripMenuItem})
        SettingsToolStripMenuItem.ForeColor = Color.White
        SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        SettingsToolStripMenuItem.Size = New Size(69, 28)
        SettingsToolStripMenuItem.Text = "Settings"
        '
        ' AboutToolStripMenuItem
        '
        AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        AboutToolStripMenuItem.Size = New Size(116, 24)
        AboutToolStripMenuItem.Text = "About"
        '
        ' Browser
        '
        AcceptButton = BtnGo
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(18, 18, 20)
        ClientSize = New Size(1280, 800)
        Controls.Add(TabControl1)
        Controls.Add(MenuStrip1)
        Font = New Font("Segoe UI", 9.0F, FontStyle.Regular, GraphicsUnit.Point)
        ForeColor = Color.WhiteSmoke
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MainMenuStrip = MenuStrip1
        MinimumSize = New Size(1024, 640)
        Name = "Browser"
        StartPosition = FormStartPosition.CenterScreen
        Text = "FBrowser"
        TabControl1.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        CType(WebView2, ComponentModel.ISupportInitialize).EndInit()
        NavigationBar.ResumeLayout(False)
        NavLayout.ResumeLayout(False)
        NavLayout.PerformLayout()
        UrlContainer.ResumeLayout(False)
        UrlContainer.PerformLayout()
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
    Friend WithEvents NavigationBar As GradientPanel
    Friend WithEvents NavLayout As TableLayoutPanel
    Friend WithEvents LblBrand As Label
    Friend WithEvents UrlContainer As RoundedPanel
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
    Friend WithEvents DeveloperToolsToolStripMenuItem As ToolStripMenuItem

End Class
