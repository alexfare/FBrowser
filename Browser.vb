Imports Microsoft.VisualBasic
Imports Microsoft.Web.WebView2.Core
Imports Microsoft.Web.WebView2.WinForms

Public Class Browser
    Private historyList As New List(Of HistoryItem)
    Private ReadOnly navToolTip As New ToolTip With {
        .AutomaticDelay = 100,
        .AutoPopDelay = 4000,
        .InitialDelay = 100,
        .ReshowDelay = 50,
        .ShowAlways = True
    }

    Private Async Sub Browser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True

        ' === Load History and Clear Tabs ===
        historyList = BrowserHistoryManager.LoadHistory()
        TabControl1.TabPages.Clear()

        ' === Add First Tab ===
        Await AddNewTab("https://alexfare.com") ' TODO: Make homepage user-configurable

        AddHandler TabControl1.DrawItem, AddressOf TabControl1_DrawItem

        ' === Set Focus to URL TextBox ===
        Dim currentTab = TabControl1.SelectedTab
        Dim textBoxUrl = TryCast(currentTab?.Controls.Find("textBoxUrl", True).FirstOrDefault(), TextBox)
        textBoxUrl?.Focus()

        ' === Set Window Title with Version Info ===
        Me.Text = $"FBrowser - {My.Settings.Version}"

        ' === Apply Layout Settings (Docking, Padding, etc) ===
        SetupLayout()
    End Sub

    Private Sub SetupLayout()
        MenuStrip1.Renderer = New ToolStripProfessionalRenderer(New ModernMenuColorTable())
        MenuStrip1.BackColor = Color.White
        MenuStrip1.ForeColor = Color.FromArgb(32, 32, 32)
        MenuStrip1.Font = New Font("Segoe UI", 10.0F, FontStyle.Regular, GraphicsUnit.Point)
        MenuStrip1.Padding = New Padding(12, 8, 0, 8)
        MenuStrip1.GripStyle = ToolStripGripStyle.Hidden

        For Each item As ToolStripMenuItem In MenuStrip1.Items
            item.ForeColor = MenuStrip1.ForeColor
            item.BackColor = MenuStrip1.BackColor
            item.Font = MenuStrip1.Font

            For Each dropDownItem As ToolStripItem In item.DropDownItems
                If TypeOf dropDownItem Is ToolStripMenuItem Then
                    dropDownItem.ForeColor = MenuStrip1.ForeColor
                    dropDownItem.BackColor = Color.White
                    dropDownItem.Font = MenuStrip1.Font
                End If
            Next
        Next

        TabControl1.Appearance = TabAppearance.Normal
        TabControl1.DrawMode = TabDrawMode.OwnerDrawFixed
        TabControl1.ItemSize = New Size(180, 38)
        TabControl1.SizeMode = TabSizeMode.Fixed
        TabControl1.Padding = New Point(22, 6)
        TabControl1.BackColor = Color.White
        TabControl1.ForeColor = Color.FromArgb(32, 32, 32)
        TabControl1.Margin = New Padding(0)

        Me.BackColor = Color.FromArgb(248, 249, 252)
        Me.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular, GraphicsUnit.Point)
        Me.Padding = New Padding(0)

        TabControl1.Invalidate()
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs)
        Dim current = GetCurrentWebView()
        If current?.CoreWebView2 IsNot Nothing AndAlso current.CoreWebView2.CanGoBack Then
            current.CoreWebView2.GoBack()
        End If
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs)
        Dim current = GetCurrentWebView()
        If current?.CoreWebView2 IsNot Nothing Then
            current.Reload()
        End If
    End Sub

    Private Sub BtnForward_Click(sender As Object, e As EventArgs)
        Dim current = GetCurrentWebView()
        If current?.CoreWebView2 IsNot Nothing AndAlso current.CoreWebView2.CanGoForward Then
            current.CoreWebView2.GoForward()
        End If
    End Sub

    Private Sub BtnGo_Click(sender As Object, e As EventArgs)
        URLSearch()
    End Sub

    Private Sub TxtURL_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            e.Handled = True
            URLSearch()
        End If
    End Sub

    Private Sub TxtURL_Enter(sender As Object, e As EventArgs)
        Dim textBox = TryCast(sender, TextBox)
        textBox?.SelectAll()
    End Sub

    Private Sub URLSearch()
        Dim currentWebView = GetCurrentWebView()
        If currentWebView IsNot Nothing AndAlso currentWebView.CoreWebView2 IsNot Nothing Then
            Dim currentTab = TabControl1.SelectedTab
            Dim textBoxUrl = CType(currentTab.Controls.Find("textBoxUrl", True).FirstOrDefault(), TextBox)
            If textBoxUrl IsNot Nothing Then
                Dim input As String = textBoxUrl.Text.Trim()
                Dim url As String

                If Not input.StartsWith("http://", StringComparison.OrdinalIgnoreCase) AndAlso
                   Not input.StartsWith("https://", StringComparison.OrdinalIgnoreCase) AndAlso
                   Not input.Contains(".") Then
                    url = "https://www.google.com/search?q=" + Uri.EscapeDataString(input)
                Else
                    If Not input.StartsWith("http://", StringComparison.OrdinalIgnoreCase) AndAlso
                       Not input.StartsWith("https://", StringComparison.OrdinalIgnoreCase) Then
                        url = "https://" + input
                    Else
                        url = input
                    End If
                End If

                If Uri.IsWellFormedUriString(url, UriKind.Absolute) Then
                    currentWebView.CoreWebView2.Navigate(url)
                    UpdateTabText(TabControl1.SelectedTab, url)
                Else
                    MessageBox.Show("Please enter a valid URL or search term.")
                End If
            End If
        Else
            MessageBox.Show("Browser is not initialized. Please wait.")
        End If
    End Sub

    Private Function GetCurrentWebView() As Microsoft.Web.WebView2.WinForms.WebView2
        If TabControl1.SelectedTab IsNot Nothing Then
            Return CType(TabControl1.SelectedTab.Controls("WebView2"), Microsoft.Web.WebView2.WinForms.WebView2)
        End If
        Return Nothing
    End Function

    Public Async Function AddNewTab(Optional url As String = "") As Task
        Dim newTab As New TabPage("New Tab") With {
            .BackColor = Color.White,
            .Padding = New Padding(0)
        }

        Dim navContainer As New Panel With {
            .Dock = DockStyle.Top,
            .Height = 68,
            .BackColor = Color.FromArgb(248, 249, 252),
            .Padding = New Padding(16, 14, 16, 12)
        }

        Dim navLayout As New TableLayoutPanel With {
            .ColumnCount = 5,
            .Dock = DockStyle.Fill,
            .BackColor = Color.Transparent
        }

        navLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 44.0F))
        navLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 44.0F))
        navLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 44.0F))
        navLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        navLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 78.0F))
        navLayout.RowCount = 1
        navLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))

        Dim btnBack = CreateNavIconButton(ChrW(&H2190))
        Dim btnForward = CreateNavIconButton(ChrW(&H2192))
        Dim btnRefresh = CreateNavIconButton(ChrW(&H21BB))
        Dim textBoxUrl = CreateUrlTextBox()
        Dim btnGo = CreateGoButton()

        btnBack.Margin = New Padding(0, 0, 8, 0)
        btnForward.Margin = New Padding(0, 0, 8, 0)
        btnRefresh.Margin = New Padding(0, 0, 12, 0)
        textBoxUrl.Margin = New Padding(0, 0, 12, 0)

        btnBack.Enabled = False
        btnForward.Enabled = False
        btnRefresh.Enabled = False

        navLayout.Controls.Add(btnBack, 0, 0)
        navLayout.Controls.Add(btnForward, 1, 0)
        navLayout.Controls.Add(btnRefresh, 2, 0)
        navLayout.Controls.Add(textBoxUrl, 3, 0)
        navLayout.Controls.Add(btnGo, 4, 0)
        navContainer.Controls.Add(navLayout)

        navToolTip.SetToolTip(btnBack, "Back")
        navToolTip.SetToolTip(btnForward, "Forward")
        navToolTip.SetToolTip(btnRefresh, "Refresh")
        navToolTip.SetToolTip(btnGo, "Navigate")

        Dim webView As New WebView2 With {
            .Name = "WebView2",
            .Dock = DockStyle.Fill,
            .DefaultBackgroundColor = Color.White
        }

        newTab.Controls.Add(webView)
        newTab.Controls.Add(navContainer)

        TabControl1.TabPages.Add(newTab)
        TabControl1.SelectedTab = newTab

        AddHandler btnBack.Click, AddressOf BtnBack_Click
        AddHandler btnForward.Click, AddressOf BtnForward_Click
        AddHandler btnRefresh.Click, AddressOf BtnRefresh_Click
        AddHandler btnGo.Click, AddressOf BtnGo_Click
        AddHandler textBoxUrl.KeyPress, AddressOf TxtURL_KeyPress
        AddHandler textBoxUrl.Enter, AddressOf TxtURL_Enter

        Dim refreshNavState As Action = Sub()
                                             UpdateNavigationButtonState(webView, btnBack, btnForward, btnRefresh)
                                         End Sub

        AddHandler webView.NavigationStarting,
            Sub(sender, e)
                UpdateUrlTextBox(newTab, e.Uri)
            End Sub

        AddHandler webView.NavigationCompleted,
            Sub(sender, e)
                UpdateUrlTextBox(newTab, webView.Source.ToString())
                refreshNavState()
                OnNavigationCompleted(webView, e)
            End Sub

        AddHandler webView.CoreWebView2InitializationCompleted,
            Sub(sender, args)
                If args.IsSuccess Then
                    RemoveHandler webView.CoreWebView2.NewWindowRequested, AddressOf CoreWebView2_NewWindowRequested
                    AddHandler webView.CoreWebView2.NewWindowRequested, AddressOf CoreWebView2_NewWindowRequested
                    AddHandler webView.CoreWebView2.DocumentTitleChanged,
                        Sub() UpdateTabTitle(newTab, webView.CoreWebView2.DocumentTitle)
                    AddHandler webView.CoreWebView2.HistoryChanged,
                        Sub() refreshNavState()

                    btnRefresh.Enabled = True

                    If Not String.IsNullOrWhiteSpace(url) Then
                        NavigateToUrl(webView, url)
                        UpdateTabText(newTab, url)
                    Else
                        refreshNavState()
                    End If
                End If
            End Sub

        Try
            Await webView.EnsureCoreWebView2Async(Nothing)
            refreshNavState()
        Catch ex As Exception
            MessageBox.Show("Failed to initialize WebView2.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        textBoxUrl.Focus()
    End Function


    Private Function CreateNavIconButton(symbol As String) As Button
        Dim button As New Button With {
            .Text = symbol,
            .Dock = DockStyle.Fill,
            .FlatStyle = FlatStyle.Flat,
            .Font = New Font("Segoe UI Symbol", 11.0F, FontStyle.Regular, GraphicsUnit.Point),
            .ForeColor = Color.FromArgb(80, 80, 80),
            .BackColor = Color.Transparent,
            .Margin = New Padding(0),
            .Padding = New Padding(0),
            .Cursor = Cursors.Hand,
            .UseVisualStyleBackColor = False,
            .AutoSize = False,
            .MinimumSize = New Size(36, 36)
        }

        button.FlatAppearance.BorderSize = 0
        button.FlatAppearance.MouseOverBackColor = Color.FromArgb(227, 235, 247)
        button.FlatAppearance.MouseDownBackColor = Color.FromArgb(210, 222, 241)

        Return button
    End Function

    Private Function CreateGoButton() As Button
        Dim button As New Button With {
            .Text = "Go",
            .Dock = DockStyle.Fill,
            .FlatStyle = FlatStyle.Flat,
            .Font = New Font("Segoe UI", 9.0F, FontStyle.Bold, GraphicsUnit.Point),
            .ForeColor = Color.White,
            .BackColor = Color.FromArgb(66, 133, 244),
            .Margin = New Padding(0),
            .Padding = New Padding(0, 2, 0, 0),
            .Cursor = Cursors.Hand,
            .UseVisualStyleBackColor = False,
            .AutoSize = False,
            .MinimumSize = New Size(72, 36)
        }

        button.FlatAppearance.BorderSize = 0
        button.FlatAppearance.MouseOverBackColor = Color.FromArgb(52, 120, 235)
        button.FlatAppearance.MouseDownBackColor = Color.FromArgb(41, 104, 214)

        Return button
    End Function

    Private Function CreateUrlTextBox() As TextBox
        Return New TextBox With {
            .Name = "textBoxUrl",
            .Dock = DockStyle.Fill,
            .BorderStyle = BorderStyle.FixedSingle,
            .Font = New Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point),
            .ForeColor = Color.FromArgb(45, 45, 45),
            .BackColor = Color.White,
            .Margin = New Padding(0),
            .MinimumSize = New Size(120, 32),
            .AutoSize = False,
            .Height = 36
        }
    End Function

    Private Sub UpdateNavigationButtonState(webView As WebView2, backButton As Button, forwardButton As Button, refreshButton As Button)
        Dim hasCore = webView.CoreWebView2 IsNot Nothing
        Dim accentDisabled = Color.FromArgb(176, 176, 176)
        Dim accentEnabled = Color.FromArgb(80, 80, 80)

        backButton.Enabled = hasCore AndAlso webView.CoreWebView2.CanGoBack
        forwardButton.Enabled = hasCore AndAlso webView.CoreWebView2.CanGoForward
        refreshButton.Enabled = hasCore

        backButton.ForeColor = If(backButton.Enabled, accentEnabled, accentDisabled)
        forwardButton.ForeColor = If(forwardButton.Enabled, accentEnabled, accentDisabled)
        refreshButton.ForeColor = If(refreshButton.Enabled, accentEnabled, accentDisabled)
    End Sub



    Private Sub NavigateToUrl(webView As Microsoft.Web.WebView2.WinForms.WebView2, url As String)
        Try
            If Uri.IsWellFormedUriString(url, UriKind.Absolute) Then
                webView.CoreWebView2.Navigate(url)
                UpdateUrlTextBox(TabControl1.SelectedTab, url)
            Else
                URLSearch()
            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred while navigating to the URL.", "Navigation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Sub CoreWebView2_NewWindowRequested(sender As Object, e As CoreWebView2NewWindowRequestedEventArgs)
        e.Handled = True
        Await AddNewTab()
        Dim newWebView = GetCurrentWebView()
        If newWebView IsNot Nothing Then
            newWebView.CoreWebView2.Navigate(e.Uri)
        End If
    End Sub

    Private Sub UpdateTabText(tab As TabPage, url As String)
        Dim domainName As String = GetDomainNameFromUrl(url)
        If domainName.Length > 22 Then
            domainName = domainName.Substring(0, 22) & "…"
        End If
        tab.Text = domainName
        TabControl1.Invalidate()
    End Sub

    Private Sub UpdateTabTitle(tab As TabPage, title As String)
        If title.Length > 28 Then
            tab.Text = title.Substring(0, 28) & "…"
        Else
            tab.Text = title
        End If
        TabControl1.Invalidate()
    End Sub

    Private Function GetDomainNameFromUrl(url As String) As String
        Dim uri As New Uri(url)
        Dim host As String = uri.Host

        If host.StartsWith("www.") Then
            host = host.Substring(4)
        End If

        Dim domainParts As String() = host.Split("."c)
        If domainParts.Length > 2 Then
            host = domainParts(domainParts.Length - 2)
        Else
            host = domainParts(0)
        End If

        Return host
    End Function

    Public Class DoubleBufferedTabControl
        Inherits TabControl

        Public Sub New()
            Me.SetStyle(ControlStyles.UserPaint Or
                    ControlStyles.AllPaintingInWmPaint Or
                    ControlStyles.OptimizedDoubleBuffer Or
                    ControlStyles.ResizeRedraw, True)
            Me.DoubleBuffered = True
        End Sub

        Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
            ' Prevent default background painting
            ' Do nothing — we already draw the background in DrawItem
        End Sub
    End Class

    Private Sub TabControl1_DrawItem(sender As Object, e As DrawItemEventArgs)
        Dim tabControl = CType(sender, TabControl)
        Dim g = e.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        Dim headerHeight = tabControl.ItemSize.Height + TabControl1.Padding.Y * 2 + 12
        Dim headerRect = New Rectangle(0, 0, tabControl.Width, headerHeight)

        Using headerBrush As New SolidBrush(Color.FromArgb(244, 246, 250))
            g.FillRectangle(headerBrush, headerRect)
        End Using

        Dim tabRect = tabControl.GetTabRect(e.Index)
        tabRect.Inflate(-6, -4)
        Dim isSelected = (e.Index = tabControl.SelectedIndex)
        Dim tabBackColor = If(isSelected, Color.White, Color.FromArgb(244, 246, 250))
        Dim textColor = If(isSelected, Color.FromArgb(32, 32, 32), Color.FromArgb(110, 116, 129))

        Using path As Drawing2D.GraphicsPath = RoundedRect(tabRect, If(isSelected, 10, 8))
            Using brush As New SolidBrush(tabBackColor)
                g.FillPath(brush, path)
            End Using
        End Using

        If isSelected Then
            Using accentPen As New Pen(Color.FromArgb(66, 133, 244), 2)
                g.DrawLine(accentPen, tabRect.Left + 12, tabRect.Bottom - 3, tabRect.Right - 12, tabRect.Bottom - 3)
            End Using
        End If

        Dim font As New Font("Segoe UI", 9.25F, FontStyle.SemiBold, GraphicsUnit.Point)
        Dim closeButtonSize = 16
        Dim closePadding = 14
        Dim textRect = New Rectangle(tabRect.Left + 16, tabRect.Top + 6, tabRect.Width - closeButtonSize - closePadding * 2, tabRect.Height - 12)

        TextRenderer.DrawText(g, tabControl.TabPages(e.Index).Text, font, textRect, textColor, TextFormatFlags.EndEllipsis Or TextFormatFlags.VerticalCenter)

        Dim closeRect = New Rectangle(tabRect.Right - closeButtonSize - closePadding, tabRect.Top + (tabRect.Height - closeButtonSize) \ 2, closeButtonSize, closeButtonSize)
        Dim closeBackColor = If(isSelected, Color.FromArgb(232, 236, 244), Color.FromArgb(238, 240, 244))
        Dim closeLineColor = If(isSelected, Color.FromArgb(110, 120, 140), Color.FromArgb(150, 150, 150))

        Using closePath As Drawing2D.GraphicsPath = RoundedRect(closeRect, 6)
            Using closeBrush As New SolidBrush(closeBackColor)
                g.FillPath(closeBrush, closePath)
            End Using
        End Using

        Using pen As New Pen(closeLineColor, 1.6F)
            g.DrawLine(pen, closeRect.Left + 4, closeRect.Top + 4, closeRect.Right - 4, closeRect.Bottom - 4)
            g.DrawLine(pen, closeRect.Right - 4, closeRect.Top + 4, closeRect.Left + 4, closeRect.Bottom - 4)
        End Using
    End Sub


    ' Helper function for rounded rectangles
    Private Function RoundedRect(rect As Rectangle, radius As Integer) As Drawing2D.GraphicsPath
        Dim path As New Drawing2D.GraphicsPath()
        path.AddArc(rect.Left, rect.Top, radius, radius, 180, 90)
        path.AddArc(rect.Right - radius, rect.Top, radius, radius, 270, 90)
        path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90)
        path.AddArc(rect.Left, rect.Bottom - radius, radius, radius, 90, 90)
        path.CloseFigure()
        Return path
    End Function



    Private Sub TabControl1_MouseDown(sender As Object, e As MouseEventArgs) Handles TabControl1.MouseDown
        Dim closeButtonSize = 16
        Dim closeButtonPadding = 14

        For i As Integer = 0 To TabControl1.TabPages.Count - 1
            Dim tabRect = TabControl1.GetTabRect(i)
            tabRect.Inflate(-6, -4)
            Dim closeButtonX = tabRect.Right - closeButtonSize - closeButtonPadding
            Dim closeButtonRect = New Rectangle(closeButtonX, tabRect.Top + (tabRect.Height - closeButtonSize) / 2, closeButtonSize, closeButtonSize)

            If closeButtonRect.Contains(e.Location) Then
                Dim tabPage = TabControl1.TabPages(i)
                Dim webView = TryCast(tabPage.Controls("WebView2"), Microsoft.Web.WebView2.WinForms.WebView2)
                If webView IsNot Nothing Then
                    If webView.CoreWebView2 Is Not Nothing Then
                        RemoveHandler webView.CoreWebView2.NewWindowRequested, AddressOf CoreWebView2_NewWindowRequested
                    End If
                    webView.Dispose()
                End If
                TabControl1.TabPages.Remove(tabPage)
                Exit For
            End If
        Next
    End Sub

    Friend Async Function OpenBookmarkAsync(url As String, Optional openInNewTab As Boolean = False) As Task
        If String.IsNullOrWhiteSpace(url) Then
            Return
        End If

        If openInNewTab OrElse TabControl1.SelectedTab Is Nothing Then
            Await AddNewTab(url)
            Return
        End If

        Dim currentWebView = GetCurrentWebView()
        If currentWebView Is Nothing Then
            Await AddNewTab(url)
            Return
        End If

        Await currentWebView.EnsureCoreWebView2Async(Nothing)
        If currentWebView.CoreWebView2 Is Nothing Then
            Return
        End If

        currentWebView.CoreWebView2.Navigate(url)
        UpdateUrlTextBox(TabControl1.SelectedTab, url)
        UpdateTabText(TabControl1.SelectedTab, url)
    End Function


    Private Sub OnNavigationCompleted(sender As Object, e As CoreWebView2NavigationCompletedEventArgs)
        Dim webView As Microsoft.Web.WebView2.WinForms.WebView2 = CType(sender, Microsoft.Web.WebView2.WinForms.WebView2)
        Dim url As String = webView.Source.ToString()
        Dim title As String = webView.CoreWebView2.DocumentTitle
        Dim visitDate As DateTime = DateTime.Now

        Dim historyItem As New HistoryItem(url, title, visitDate)
        historyList.Add(historyItem)

        BrowserHistoryManager.SaveHistory(historyList)
    End Sub


    Private Sub UpdateUrlTextBox(tab As TabPage, url As String)
        Dim textBoxUrl = CType(tab.Controls.Find("textBoxUrl", True).FirstOrDefault(), TextBox)
        If textBoxUrl IsNot Nothing Then
            textBoxUrl.Text = url
        End If
    End Sub

    Private Async Sub NewTabToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewTabToolStripMenuItem.Click
        Await AddNewTab()
    End Sub

    Private Sub ViewHistoryToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ViewHistoryToolStripMenuItem1.Click
        Dim historyForm As New HistoryForm(Me)
        historyForm.Show()
    End Sub


    Private Sub ViewBookmarksToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewBookmarksToolStripMenuItem.Click
        Dim bookmarkForm As New BookmarkForm(Me)
        bookmarkForm.Show(Me)
    End Sub

    Private Async Sub AddToBookmarksToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddToBookmarksToolStripMenuItem.Click
        Dim currentWebView = GetCurrentWebView()

        If currentWebView Is Nothing Then
            MessageBox.Show("There is no active tab available. Open a tab and try again.", "Bookmarks", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Await currentWebView.EnsureCoreWebView2Async(Nothing)

        If currentWebView.CoreWebView2 Is Nothing Then
            MessageBox.Show("The current tab is still initializing. Please wait a moment and try again.", "Bookmarks", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim sourceUri = currentWebView.Source
        Dim url As String = If(sourceUri?.AbsoluteUri, String.Empty)

        If String.IsNullOrWhiteSpace(url) Then
            MessageBox.Show("Unable to determine the current page address.", "Bookmarks", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim title As String = currentWebView.CoreWebView2.DocumentTitle
        If String.IsNullOrWhiteSpace(title) Then
            title = url
        End If

        Dim bookmarkName = Interaction.InputBox("Enter a name for this bookmark:", "Add Bookmark", title)
        If bookmarkName Is Nothing Then
            Return
        End If

        bookmarkName = bookmarkName.Trim()
        If bookmarkName.Length = 0 Then
            MessageBox.Show("Bookmark name cannot be empty.", "Bookmarks", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        BookmarkManager.SaveBookmark(url, bookmarkName)
        MessageBox.Show("Bookmark saved.", "Bookmarks", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub ViewCacheToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewCacheToolStripMenuItem.Click
        MessageBox.Show("Coming Soon.")
    End Sub

    Private Async Sub ViewDownloadsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewDownloadsToolStripMenuItem.Click
        Dim currentWebView = GetCurrentWebView()
        If currentWebView Is Nothing Then
            MessageBox.Show("Open a tab before viewing downloads.", "Downloads", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Await currentWebView.EnsureCoreWebView2Async(Nothing)

        If currentWebView.CoreWebView2 IsNot Nothing Then
            currentWebView.CoreWebView2.OpenDefaultDownloadDialog()
        Else
            MessageBox.Show("The active tab is still initializing.", "Downloads", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub AboutToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        About.Show()
    End Sub

    Private Async Sub DeleteCacheToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteCacheToolStripMenuItem.Click
        Dim currentWebView = GetCurrentWebView()
        If currentWebView Is Nothing Then
            MessageBox.Show("Open a tab before clearing the cache.", "Cache", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Await currentWebView.EnsureCoreWebView2Async(Nothing)

        If currentWebView.CoreWebView2 Is Nothing Then
            MessageBox.Show("The active tab is still initializing.", "Cache", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Await currentWebView.CoreWebView2.Profile.ClearBrowsingDataAsync()
        MessageBox.Show("Browser cache cleared.", "Cache", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub DeveloperToolsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeveloperToolsToolStripMenuItem.Click
        Dim currentWebView = GetCurrentWebView()
        If currentWebView?.CoreWebView2 IsNot Nothing Then
            currentWebView.CoreWebView2.OpenDevToolsWindow()
        Else
            MessageBox.Show("Developer tools are available once the active tab finishes loading.", "Developer Tools", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Public Sub ClearHistoryData()
        historyList.Clear()
        BrowserHistoryManager.ClearHistory()
    End Sub

    Private Sub ClearHistoryToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ClearHistoryToolStripMenuItem1.Click
        ClearHistoryData()
    End Sub

    Private Class ModernMenuColorTable
        Inherits ProfessionalColorTable

        Public Overrides ReadOnly Property MenuBorder As Color
            Get
                Return Color.FromArgb(224, 224, 224)
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemBorder As Color
            Get
                Return Color.FromArgb(214, 223, 247)
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemSelected As Color
            Get
                Return Color.FromArgb(232, 240, 254)
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemSelectedGradientBegin As Color
            Get
                Return MenuItemSelected
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemSelectedGradientEnd As Color
            Get
                Return MenuItemSelected
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemPressedGradientBegin As Color
            Get
                Return MenuItemSelected
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemPressedGradientEnd As Color
            Get
                Return MenuItemSelected
            End Get
        End Property

        Public Overrides ReadOnly Property ToolStripDropDownBackground As Color
            Get
                Return Color.White
            End Get
        End Property

        Public Overrides ReadOnly Property ImageMarginGradientBegin As Color
            Get
                Return Color.White
            End Get
        End Property

        Public Overrides ReadOnly Property ImageMarginGradientMiddle As Color
            Get
                Return Color.White
            End Get
        End Property

        Public Overrides ReadOnly Property ImageMarginGradientEnd As Color
            Get
                Return Color.White
            End Get
        End Property

        Public Overrides ReadOnly Property SeparatorDark As Color
            Get
                Return Color.FromArgb(224, 224, 224)
            End Get
        End Property

        Public Overrides ReadOnly Property SeparatorLight As Color
            Get
                Return Color.FromArgb(224, 224, 224)
            End Get
        End Property
    End Class
End Class
