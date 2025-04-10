Imports Microsoft.Web.WebView2.Core
Imports Microsoft.Web.WebView2.WinForms

Public Class Browser
    Private historyList As New List(Of HistoryItem)

    Private Async Sub Browser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === Load History and Clear Tabs ===
        historyList = BrowserHistoryManager.LoadHistory()
        TabControl1.TabPages.Clear()

        ' === Add First Tab ===
        Await AddNewTab("https://alexfare.com") ' TODO: Make homepage user-configurable

        ' === Configure TabControl Appearance ===
        With TabControl1
            .DrawMode = TabDrawMode.OwnerDrawFixed
            .ItemSize = New Size(145, 25)
            .Appearance = TabAppearance.Normal
            .SizeMode = TabSizeMode.Fixed
            .BackColor = Color.FromArgb(30, 30, 30)
            .Padding = New Point(10, 3)
        End With
        AddHandler TabControl1.DrawItem, AddressOf TabControl1_DrawItem

        ' === Initialize Default WebView2 Instance ===
        InitializeAsync()

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
        ' Dark theme for MenuStrip
        MenuStrip1.BackColor = Color.FromArgb(30, 30, 30)
        MenuStrip1.ForeColor = Color.White
        For Each item As ToolStripMenuItem In MenuStrip1.Items
            item.ForeColor = Color.White
            item.BackColor = Color.FromArgb(30, 30, 30)
        Next

        ' TabControl color fix
        TabControl1.BackColor = Color.FromArgb(40, 40, 40)
        TabControl1.ForeColor = Color.White
        TabControl1.Appearance = TabAppearance.FlatButtons
        TabControl1.ItemSize = New Size(180, 30)
        TabControl1.SizeMode = TabSizeMode.Fixed
        TabControl1.DrawMode = TabDrawMode.OwnerDrawFixed

        With TabControl1
            .Appearance = TabAppearance.Normal
            .DrawMode = TabDrawMode.OwnerDrawFixed
            .BackColor = Color.FromArgb(30, 30, 30)
            .ItemSize = New Size(180, 30)
            .SizeMode = TabSizeMode.Fixed
            .Padding = New Point(10, 3)
        End With

    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        GetCurrentWebView()?.GoBack()
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        GetCurrentWebView()?.Reload()
    End Sub

    Private Sub BtnForward_Click(sender As Object, e As EventArgs) Handles BtnForward.Click
        GetCurrentWebView()?.GoForward()
    End Sub

    Private Sub BtnGo_Click(sender As Object, e As EventArgs) Handles BtnGo.Click
        URLSearch()
    End Sub

    Private Sub TxtURL_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtURL.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            e.Handled = True
            URLSearch()
        End If
    End Sub

    Private Sub TxtURL_Enter(sender As Object, e As EventArgs) Handles TxtURL.Enter
        TxtURL.SelectAll()
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
        Dim newTab As New TabPage("New Tab")
        newTab.BackColor = Color.FromArgb(20, 20, 20)

        Dim navPanel As New Panel With {
        .Dock = DockStyle.Top,
        .Height = 45,
        .BackColor = Color.FromArgb(30, 30, 30)
    }

        Dim btnBack As New Button With {.Text = "←", .Width = 40, .Left = 10, .Top = 7, .FlatStyle = FlatStyle.Flat, .ForeColor = Color.White, .BackColor = Color.FromArgb(60, 60, 60)}
        Dim btnForward As New Button With {.Text = "→", .Width = 40, .Left = 60, .Top = 7, .FlatStyle = FlatStyle.Flat, .ForeColor = Color.White, .BackColor = Color.FromArgb(60, 60, 60)}
        Dim btnRefresh As New Button With {.Text = "⟳", .Width = 40, .Left = 110, .Top = 7, .FlatStyle = FlatStyle.Flat, .ForeColor = Color.White, .BackColor = Color.FromArgb(60, 60, 60)}
        Dim textBoxUrl As New TextBox With {.Left = 160, .Top = 10, .Width = 500, .Height = 25, .Name = "textBoxUrl", .BackColor = Color.FromArgb(45, 45, 45), .ForeColor = Color.White, .BorderStyle = BorderStyle.FixedSingle}
        Dim btnGo As New Button With {.Text = "Go", .Left = 670, .Top = 7, .Width = 60, .BackColor = Color.FromArgb(66, 133, 244), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}

        ' Remove button borders
        For Each btn In {btnBack, btnForward, btnRefresh, btnGo}
            btn.FlatAppearance.BorderSize = 0
        Next

        navPanel.Controls.AddRange({btnBack, btnForward, btnRefresh, textBoxUrl, btnGo})

        Dim webView As New WebView2 With {
        .Name = "WebView2",
        .Dock = DockStyle.Fill
    }

        newTab.Controls.Add(webView)
        newTab.Controls.Add(navPanel)

        TabControl1.TabPages.Add(newTab)
        TabControl1.SelectedTab = newTab

        ' Event handlers
        AddHandler btnBack.Click, Sub() If webView.CoreWebView2 IsNot Nothing Then webView.CoreWebView2.GoBack()
        AddHandler btnForward.Click, Sub() If webView.CoreWebView2 IsNot Nothing Then webView.CoreWebView2.GoForward()
        AddHandler btnRefresh.Click, Sub() If webView.CoreWebView2 IsNot Nothing Then webView.CoreWebView2.Reload()
        AddHandler btnGo.Click, Sub() If webView.CoreWebView2 IsNot Nothing Then NavigateToUrl(webView, textBoxUrl.Text)
        AddHandler textBoxUrl.KeyPress, Sub(sender, e)
                                            If e.KeyChar = Convert.ToChar(Keys.Enter) Then
                                                e.Handled = True
                                                If webView.CoreWebView2 IsNot Nothing Then
                                                    NavigateToUrl(webView, textBoxUrl.Text)
                                                End If
                                            End If
                                        End Sub

        AddHandler webView.NavigationStarting, Sub(sender, e) UpdateUrlTextBox(newTab, e.Uri)
        AddHandler webView.NavigationCompleted, Sub(sender, e)
                                                    UpdateUrlTextBox(newTab, webView.Source.ToString())
                                                    OnNavigationCompleted(webView, e)
                                                End Sub

        AddHandler webView.CoreWebView2InitializationCompleted, Sub(sender, args)
                                                                    If args.IsSuccess Then
                                                                        AddHandler webView.CoreWebView2.DocumentTitleChanged,
                                                                        Sub() UpdateTabTitle(newTab, webView.CoreWebView2.DocumentTitle)
                                                                        If Not String.IsNullOrEmpty(url) Then
                                                                            NavigateToUrl(webView, url)
                                                                            UpdateTabText(newTab, url)
                                                                        End If
                                                                    End If
                                                                End Sub

        Try
            Await webView.EnsureCoreWebView2Async(Nothing)
        Catch ex As Exception
            MessageBox.Show("Failed to initialize WebView2.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        textBoxUrl.Focus()
    End Function


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
        If domainName.Length > 25 Then
            domainName = domainName.Substring(0, 25)
        End If
        tab.Text = domainName.PadRight(25)
        TabControl1.Invalidate()
    End Sub

    Private Sub UpdateTabTitle(tab As TabPage, title As String)
        If title.Length > 25 Then
            tab.Text = title.Substring(0, 25).PadRight(25)
        Else
            tab.Text = title.PadRight(25)
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

        ' Paint the entire tab strip background
        Using bgBrush As New SolidBrush(Color.FromArgb(30, 30, 30))
            g.FillRectangle(bgBrush, tabControl.GetTabRect(e.Index))
        End Using

        Dim tabPage = tabControl.TabPages(e.Index)
        Dim tabRect = tabControl.GetTabRect(e.Index)
        Dim isSelected = (e.Index = tabControl.SelectedIndex)

        ' Styling
        Dim bgColor = If(isSelected, Color.FromArgb(50, 50, 50), Color.FromArgb(30, 30, 30))
        Dim textColor = Color.White
        Dim closeButtonColor = Color.FromArgb(80, 80, 80)

        ' Tab background
        Using b As New SolidBrush(bgColor)
            g.FillRectangle(b, tabRect)
        End Using

        ' Tab text
        Dim font = New Font("Segoe UI", 9, FontStyle.Bold)
        Dim padding = 12
        Dim closeButtonSize = 14
        Dim closePadding = 6

        Dim textWidth = tabRect.Width - closeButtonSize - padding * 2
        Dim textRect = New Rectangle(tabRect.X + padding, tabRect.Y + 6, textWidth, tabRect.Height - 6)

        TextRenderer.DrawText(g, tabPage.Text.Trim(), font, textRect, textColor, TextFormatFlags.Left Or TextFormatFlags.VerticalCenter)

        ' Close button
        Dim closeX = tabRect.Right - closeButtonSize - closePadding
        Dim closeY = tabRect.Top + (tabRect.Height - closeButtonSize) \ 2
        Dim closeRect = New Rectangle(closeX, closeY, closeButtonSize, closeButtonSize)

        Using path As Drawing2D.GraphicsPath = RoundedRect(closeRect, 4)
            Using b As New SolidBrush(closeButtonColor)
                g.FillPath(b, path)
            End Using
        End Using

        Using pen As New Pen(Color.White, 1.5)
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
        Dim closeButtonSize = 10
        Dim closeButtonPadding = 7 'Adjust the padding to ensure space between text and button

        For i As Integer = 0 To TabControl1.TabPages.Count - 1
            Dim tabRect = TabControl1.GetTabRect(i)
            Dim closeButtonX = tabRect.Right - closeButtonSize - closeButtonPadding
            Dim closeButtonRect = New Rectangle(closeButtonX, tabRect.Top + (tabRect.Height - closeButtonSize) / 2, closeButtonSize, closeButtonSize)

            If closeButtonRect.Contains(e.Location) Then
                TabControl1.TabPages.RemoveAt(i)
                Exit For
            End If
        Next
    End Sub

    Private Sub OnBookmarkClicked(url As String)
        Dim currentWebView = GetCurrentWebView()
        If currentWebView IsNot Nothing Then
            currentWebView.CoreWebView2.Navigate(url)
            UpdateUrlTextBox(TabControl1.SelectedTab, url)
            UpdateTabText(TabControl1.SelectedTab, url)
        End If
    End Sub

    Private Async Sub InitializeAsync()
        Await WebView2.EnsureCoreWebView2Async(Nothing)
        AddHandler WebView2.NavigationCompleted, AddressOf OnNavigationCompleted
        WebView2.Source = New Uri("https://www.example.com")
    End Sub

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
        MessageBox.Show("Coming Soon.")
    End Sub

    Private Sub AddToBookmarksToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddToBookmarksToolStripMenuItem.Click
        MessageBox.Show("Coming Soon.")
    End Sub

    Private Sub ViewCacheToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewCacheToolStripMenuItem.Click
        MessageBox.Show("Coming Soon.")
    End Sub

    Private Sub ViewDownloadsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewDownloadsToolStripMenuItem.Click
        If WebView2.CoreWebView2 IsNot Nothing Then
            WebView2.CoreWebView2.OpenDefaultDownloadDialog()
        Else
            MessageBox.Show("WebView2 is not initialized.")
        End If
    End Sub

    Private Sub AboutToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        About.Show()
    End Sub

    Private Async Sub DeleteCacheToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteCacheToolStripMenuItem.Click
        Await WebView2.EnsureCoreWebView2Async()

        ' Clear cache and other browsing data
        Await WebView2.CoreWebView2.Profile.ClearBrowsingDataAsync()
        Console.WriteLine("WebView2 cache cleared.")
    End Sub

    Private Sub DeveloperToolsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeveloperToolsToolStripMenuItem.Click
        WebView2.CoreWebView2.OpenDevToolsWindow()
    End Sub

    Private Sub ClearHistoryToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ClearHistoryToolStripMenuItem1.Click
        BrowserHistoryManager.ClearHistory()
    End Sub
End Class