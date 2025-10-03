Imports Microsoft.Web.WebView2.Core
Imports Microsoft.Web.WebView2.WinForms

Public Class Browser
    Private historyList As New List(Of HistoryItem)

    Private ReadOnly PrimaryBackground As Color = Color.FromArgb(18, 18, 20)
    Private ReadOnly SecondaryBackground As Color = Color.FromArgb(32, 32, 36)
    Private ReadOnly ToolbarGradientStart As Color = Color.FromArgb(58, 58, 66)
    Private ReadOnly ToolbarGradientEnd As Color = Color.FromArgb(34, 34, 38)
    Private ReadOnly MutedButtonColor As Color = Color.FromArgb(48, 48, 54)
    Private ReadOnly MutedButtonHover As Color = Color.FromArgb(70, 70, 78)
    Private ReadOnly AccentColor As Color = Color.FromArgb(99, 102, 241)
    Private ReadOnly AccentHoverColor As Color = Color.FromArgb(129, 140, 248)
    Private ReadOnly ControlTextColor As Color = Color.WhiteSmoke

    Private Async Sub Browser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        historyList = BrowserHistoryManager.LoadHistory()
        TabControl1.TabPages.Clear()

        SetupLayout()
        AddHandler TabControl1.DrawItem, AddressOf TabControl1_DrawItem

        Await AddNewTab("https://alexfare.com") ' TODO: Make homepage user-configurable

        InitializeAsync()

        Dim currentTab = TabControl1.SelectedTab
        Dim textBoxUrl = TryCast(currentTab?.Controls.Find("textBoxUrl", True).FirstOrDefault(), TextBox)
        textBoxUrl?.Focus()

        Me.Text = $"FBrowser - {My.Settings.Version}"
    End Sub

    Private Sub SetupLayout()
        BackColor = PrimaryBackground
        DoubleBuffered = True

        NavigationBar.GradientColor1 = ToolbarGradientStart
        NavigationBar.GradientColor2 = ToolbarGradientEnd
        NavigationBar.GradientMode = Drawing2D.LinearGradientMode.Horizontal

        UrlContainer.BackColor = SecondaryBackground
        UrlContainer.BorderColor = Color.FromArgb(72, 72, 78)
        UrlContainer.BorderThickness = 1
        UrlContainer.CornerRadius = 18

        TxtURL.BackColor = SecondaryBackground
        TxtURL.ForeColor = ControlTextColor
        TxtURL.Font = New Font("Segoe UI", 11.0F, FontStyle.Regular)

        LblBrand.ForeColor = ControlTextColor

        StyleNavigationButton(BtnBack)
        StyleNavigationButton(BtnForward)
        StyleNavigationButton(BtnRefresh)
        StyleNavigationButton(BtnGo, True)

        TabControl1.BackColor = PrimaryBackground
        TabControl1.ForeColor = ControlTextColor
        TabControl1.Appearance = TabAppearance.Normal
        TabControl1.DrawMode = TabDrawMode.OwnerDrawFixed
        TabControl1.ItemSize = New Size(190, 42)
        TabControl1.SizeMode = TabSizeMode.Fixed
        TabControl1.Padding = New Point(24, 6)
        TabControl1.Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Regular)

        MenuStrip1.BackColor = SecondaryBackground
        MenuStrip1.ForeColor = ControlTextColor
        MenuStrip1.Font = New Font("Segoe UI", 10.0F, FontStyle.Regular)
        MenuStrip1.RenderMode = ToolStripRenderMode.Professional
        MenuStrip1.Renderer = New ToolStripProfessionalRenderer(New ModernColorTable(SecondaryBackground, AccentColor, PrimaryBackground))
        StyleMenuItems()
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
        Dim navHeight = If(NavigationBar IsNot Nothing AndAlso Not NavigationBar.IsDisposed AndAlso NavigationBar.Height > 0, NavigationBar.Height, 84)
        Dim navPadding = If(NavigationBar IsNot Nothing AndAlso Not NavigationBar.IsDisposed, NavigationBar.Padding, New Padding(20, 14, 20, 14))
        Dim navButtonSize = If(BtnBack IsNot Nothing AndAlso Not BtnBack.IsDisposed, BtnBack.Size, New Size(48, 56))
        Dim navButtonFont = If(BtnBack IsNot Nothing AndAlso Not BtnBack.IsDisposed, BtnBack.Font, New Font("Segoe UI Symbol", 12.0F, FontStyle.Bold))
        Dim navButtonForwardFont = If(BtnForward IsNot Nothing AndAlso Not BtnForward.IsDisposed, BtnForward.Font, navButtonFont)
        Dim navButtonRefreshFont = If(BtnRefresh IsNot Nothing AndAlso Not BtnRefresh.IsDisposed, BtnRefresh.Font, navButtonFont)
        Dim urlCorner = If(UrlContainer IsNot Nothing AndAlso Not UrlContainer.IsDisposed, UrlContainer.CornerRadius, 18)
        Dim urlBorderColor = If(UrlContainer IsNot Nothing AndAlso Not UrlContainer.IsDisposed, UrlContainer.BorderColor, Color.FromArgb(72, 72, 78))
        Dim urlBorderThickness = If(UrlContainer IsNot Nothing AndAlso Not UrlContainer.IsDisposed, UrlContainer.BorderThickness, 1)
        Dim urlPadding = If(UrlContainer IsNot Nothing AndAlso Not UrlContainer.IsDisposed, UrlContainer.Padding, New Padding(18, 10, 18, 10))
        Dim textFont = If(TxtURL IsNot Nothing AndAlso Not TxtURL.IsDisposed, TxtURL.Font, New Font("Segoe UI", 11.0F, FontStyle.Regular))
        Dim goButtonFont = If(BtnGo IsNot Nothing AndAlso Not BtnGo.IsDisposed, BtnGo.Font, New Font("Segoe UI Semibold", 11.0F, FontStyle.Bold))
        Dim goButtonSize = If(BtnGo IsNot Nothing AndAlso Not BtnGo.IsDisposed, BtnGo.Size, New Size(58, 56))

        Dim newTab As New TabPage("New Tab") With {
            .BackColor = PrimaryBackground,
            .ForeColor = ControlTextColor,
            .Padding = New Padding(0),
            .Font = TabControl1.Font
        }

        Dim webView As New WebView2 With {
            .Name = "WebView2",
            .Dock = DockStyle.Fill,
            .DefaultBackgroundColor = PrimaryBackground
        }

        Dim navBar As New GradientPanel With {
            .Dock = DockStyle.Top,
            .Height = navHeight,
            .GradientColor1 = ToolbarGradientStart,
            .GradientColor2 = ToolbarGradientEnd,
            .GradientMode = Drawing2D.LinearGradientMode.Horizontal,
            .Padding = navPadding
        }

        Dim navLayout As New TableLayoutPanel With {
            .ColumnCount = 5,
            .Dock = DockStyle.Fill,
            .Margin = New Padding(0),
            .RowCount = 1,
            .GrowStyle = TableLayoutPanelGrowStyle.FixedSize
        }
        navLayout.ColumnStyles.Add(New ColumnStyle())
        navLayout.ColumnStyles.Add(New ColumnStyle())
        navLayout.ColumnStyles.Add(New ColumnStyle())
        navLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        navLayout.ColumnStyles.Add(New ColumnStyle())
        navLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))

        Dim btnBack As New Button With {
            .Text = "←",
            .Font = navButtonFont,
            .Margin = New Padding(0, 0, 10, 0),
            .Size = navButtonSize,
            .Anchor = AnchorStyles.None
        }
        btnBack.MinimumSize = btnBack.Size

        Dim btnForward As New Button With {
            .Text = "→",
            .Font = navButtonForwardFont,
            .Margin = New Padding(0, 0, 10, 0),
            .Size = navButtonSize,
            .Anchor = AnchorStyles.None
        }
        btnForward.MinimumSize = btnForward.Size

        Dim btnRefresh As New Button With {
            .Text = "⟳",
            .Font = navButtonRefreshFont,
            .Margin = New Padding(0, 0, 16, 0),
            .Size = navButtonSize,
            .Anchor = AnchorStyles.None
        }
        btnRefresh.MinimumSize = btnRefresh.Size

        Dim urlContainer As New RoundedPanel With {
            .CornerRadius = urlCorner,
            .BorderColor = urlBorderColor,
            .BorderThickness = urlBorderThickness,
            .BackColor = SecondaryBackground,
            .Margin = New Padding(16, 0, 16, 0),
            .Padding = urlPadding,
            .Dock = DockStyle.Fill
        }

        Dim textBoxUrl As New TextBox With {
            .Name = "textBoxUrl",
            .BorderStyle = BorderStyle.None,
            .BackColor = SecondaryBackground,
            .ForeColor = ControlTextColor,
            .Font = textFont,
            .Dock = DockStyle.Fill,
            .Margin = New Padding(0)
        }
        urlContainer.Controls.Add(textBoxUrl)

        Dim btnGo As New Button With {
            .Text = "Go",
            .Font = goButtonFont,
            .Margin = New Padding(0),
            .Size = goButtonSize,
            .Anchor = AnchorStyles.None
        }
        btnGo.MinimumSize = btnGo.Size

        navLayout.Controls.Add(btnBack, 0, 0)
        navLayout.Controls.Add(btnForward, 1, 0)
        navLayout.Controls.Add(btnRefresh, 2, 0)
        navLayout.Controls.Add(urlContainer, 3, 0)
        navLayout.Controls.Add(btnGo, 4, 0)

        navBar.Controls.Add(navLayout)

        newTab.Controls.Add(webView)
        newTab.Controls.Add(navBar)
        navBar.BringToFront()

        TabControl1.TabPages.Add(newTab)
        TabControl1.SelectedTab = newTab

        StyleNavigationButton(btnBack)
        StyleNavigationButton(btnForward)
        StyleNavigationButton(btnRefresh)
        StyleNavigationButton(btnGo, True)

        AddHandler btnBack.Click,
            Sub()
                If webView.CoreWebView2 IsNot Nothing Then
                    webView.CoreWebView2.GoBack()
                End If
            End Sub

        AddHandler btnForward.Click,
            Sub()
                If webView.CoreWebView2 IsNot Nothing Then
                    webView.CoreWebView2.GoForward()
                End If
            End Sub

        AddHandler btnRefresh.Click,
            Sub()
                If webView.CoreWebView2 IsNot Nothing Then
                    webView.CoreWebView2.Reload()
                End If
            End Sub

        AddHandler btnGo.Click,
            Sub()
                If webView.CoreWebView2 IsNot Nothing Then
                    NavigateToUrl(webView, textBoxUrl.Text)
                End If
            End Sub

        AddHandler textBoxUrl.KeyPress,
            Sub(sender, e)
                If e.KeyChar = Convert.ToChar(Keys.Enter) Then
                    e.Handled = True
                    If webView.CoreWebView2 Is Not Nothing Then
                        NavigateToUrl(webView, textBoxUrl.Text)
                    End If
                End If
            End Sub

        AddHandler webView.NavigationStarting, Sub(sender, e) UpdateUrlTextBox(newTab, e.Uri)
        AddHandler webView.NavigationCompleted,
            Sub(sender, e)
                UpdateUrlTextBox(newTab, webView.Source.ToString())
                OnNavigationCompleted(webView, e)
            End Sub

        AddHandler webView.CoreWebView2InitializationCompleted,
            Sub(sender, args)
                If args.IsSuccess Then
                    RemoveHandler webView.CoreWebView2.NewWindowRequested, AddressOf CoreWebView2_NewWindowRequested
                    AddHandler webView.CoreWebView2.NewWindowRequested, AddressOf CoreWebView2_NewWindowRequested
                    AddHandler webView.CoreWebView2.DocumentTitleChanged,
                        Sub()
                            UpdateTabTitle(newTab, webView.CoreWebView2.DocumentTitle)
                        End Sub

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

        If e.Index = 0 Then
            Using stripBrush As New SolidBrush(SecondaryBackground)
                g.FillRectangle(stripBrush, New Rectangle(Point.Empty, New Size(tabControl.Width, tabControl.ItemSize.Height + 24)))
            End Using
        End If

        Dim tabRect = tabControl.GetTabRect(e.Index)
        Dim isSelected = (e.Index = tabControl.SelectedIndex)
        Dim tabBounds = Rectangle.Inflate(tabRect, -6, -6)

        Using path = RoundedRect(tabBounds, 16)
            If isSelected Then
                Using brush As New Drawing2D.LinearGradientBrush(tabBounds, AccentColor, Lighten(AccentColor, 25), Drawing2D.LinearGradientMode.Vertical)
                    g.FillPath(brush, path)
                End Using
            Else
                Using brush As New SolidBrush(Color.FromArgb(48, 48, 58))
                    g.FillPath(brush, path)
                End Using
            End If

            Dim borderColor = If(isSelected, Color.FromArgb(180, AccentColor), Color.FromArgb(70, 70, 80))
            Using borderPen As New Pen(borderColor, If(isSelected, 1.6F, 1.0F))
                g.DrawPath(borderPen, path)
            End Using
        End Using

        Dim closeButtonSize = 16
        Dim textRect = New Rectangle(tabBounds.X + 18, tabBounds.Y + 10, tabBounds.Width - closeButtonSize - 34, tabBounds.Height - 20)
        Dim textColor = If(isSelected, Color.White, ControlTextColor)

        TextRenderer.DrawText(g, tabControl.TabPages(e.Index).Text.Trim(), New Font("Segoe UI Semibold", 9.5F), textRect, textColor, TextFormatFlags.EndEllipsis)

        Dim closeRect = New Rectangle(tabBounds.Right - closeButtonSize - 12, tabBounds.Top + (tabBounds.Height - closeButtonSize) \ 2, closeButtonSize, closeButtonSize)

        Using closePath = RoundedRect(closeRect, 6)
            Using closeBrush As New SolidBrush(If(isSelected, Color.FromArgb(230, Color.White), Color.FromArgb(110, 110, 120)))
                g.FillPath(closeBrush, closePath)
            End Using
        End Using

        Using pen As New Pen(If(isSelected, Color.FromArgb(70, 70, 80), Color.FromArgb(30, 30, 34)), 1.4F)
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

    Private Function Lighten(color As Color, amount As Integer) As Color
        Dim r = Math.Min(255, color.R + amount)
        Dim g = Math.Min(255, color.G + amount)
        Dim b = Math.Min(255, color.B + amount)
        Return Color.FromArgb(r, g, b)
    End Function



    Private Sub TabControl1_MouseDown(sender As Object, e As MouseEventArgs) Handles TabControl1.MouseDown
        Dim closeButtonSize = 16
        Dim closeButtonPadding = 12

        For i As Integer = 0 To TabControl1.TabPages.Count - 1
            Dim tabRect = Rectangle.Inflate(TabControl1.GetTabRect(i), -6, -6)
            Dim closeButtonX = tabRect.Right - closeButtonSize - closeButtonPadding
            Dim closeButtonRect = New Rectangle(closeButtonX, tabRect.Top + (tabRect.Height - closeButtonSize) / 2, closeButtonSize, closeButtonSize)

            If closeButtonRect.Contains(e.Location) Then
                Dim tabPage = TabControl1.TabPages(i)
                Dim webView = TryCast(tabPage.Controls("WebView2"), Microsoft.Web.WebView2.WinForms.WebView2)
                If webView IsNot Nothing Then
                    If webView.CoreWebView2 IsNot Nothing Then
                        RemoveHandler webView.CoreWebView2.NewWindowRequested, AddressOf CoreWebView2_NewWindowRequested
                    End If
                    webView.Dispose()
                End If
                TabControl1.TabPages.Remove(tabPage)
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
        If WebView2.CoreWebView2 IsNot Nothing Then
            RemoveHandler WebView2.CoreWebView2.NewWindowRequested, AddressOf CoreWebView2_NewWindowRequested
            AddHandler WebView2.CoreWebView2.NewWindowRequested, AddressOf CoreWebView2_NewWindowRequested
        End If
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

    Private Async Sub ViewDownloadsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewDownloadsToolStripMenuItem.Click
        Dim currentWebView = GetCurrentWebView()

        If currentWebView Is Nothing Then
            MessageBox.Show("There is no active tab available. Open a tab and try again.")
            Return
        End If

        Await currentWebView.EnsureCoreWebView2Async()

        If currentWebView.CoreWebView2 Is Nothing Then
            MessageBox.Show("The current tab is still initializing. Please wait a moment and try again.")
            Return
        End If

        currentWebView.CoreWebView2.OpenDefaultDownloadDialog()
    End Sub

    Private Sub AboutToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        About.Show()
    End Sub

    Private Async Sub DeleteCacheToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteCacheToolStripMenuItem.Click
        Dim currentWebView = GetCurrentWebView()

        If currentWebView Is Nothing Then
            MessageBox.Show("There is no active tab available. Open a tab and try again.")
            Return
        End If

        Await currentWebView.EnsureCoreWebView2Async()

        If currentWebView.CoreWebView2 Is Nothing Then
            MessageBox.Show("The current tab is still initializing. Please wait a moment and try again.")
            Return
        End If

        ' Clear cache and other browsing data
        Await currentWebView.CoreWebView2.Profile.ClearBrowsingDataAsync()
        Console.WriteLine("WebView2 cache cleared.")
    End Sub

    Private Sub StyleNavigationButton(button As Button, Optional isAccent As Boolean = False)
        Dim normalColor = If(isAccent, AccentColor, MutedButtonColor)
        Dim hoverColor = If(isAccent, AccentHoverColor, MutedButtonHover)

        button.FlatStyle = FlatStyle.Flat
        button.FlatAppearance.BorderSize = 0
        button.FlatAppearance.MouseOverBackColor = hoverColor
        button.FlatAppearance.MouseDownBackColor = hoverColor
        button.BackColor = normalColor
        button.ForeColor = ControlTextColor
        button.Cursor = Cursors.Hand

        AttachHoverEffect(button, normalColor, hoverColor)
    End Sub

    Private Sub AttachHoverEffect(button As Button, normalColor As Color, hoverColor As Color)
        AddHandler button.MouseEnter, Sub(sender, _) CType(sender, Button).BackColor = hoverColor
        AddHandler button.MouseLeave, Sub(sender, _) CType(sender, Button).BackColor = normalColor
    End Sub

    Private Sub StyleMenuItems()
        For Each item As ToolStripItem In MenuStrip1.Items
            Dim menuItem = TryCast(item, ToolStripMenuItem)
            If menuItem IsNot Nothing Then
                menuItem.ForeColor = ControlTextColor
                menuItem.BackColor = SecondaryBackground
                menuItem.Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold)
                menuItem.Margin = New Padding(4, 0, 4, 0)

                Dim dropDown = TryCast(menuItem.DropDown, ToolStripDropDownMenu)
                If dropDown IsNot Nothing Then
                    dropDown.BackColor = PrimaryBackground
                    dropDown.ForeColor = ControlTextColor
                    dropDown.ShowImageMargin = False
                End If

                StyleDropDownMenuItems(menuItem.DropDownItems)
            End If
        Next
    End Sub

    Private Sub StyleDropDownMenuItems(items As ToolStripItemCollection)
        For Each entry As ToolStripItem In items
            If TypeOf entry Is ToolStripSeparator Then
                Continue For
            End If

            entry.BackColor = PrimaryBackground
            entry.ForeColor = ControlTextColor
            entry.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular)
            entry.Padding = New Padding(12, 6, 12, 6)
        Next
    End Sub

    Private Class ModernColorTable
        Inherits ProfessionalColorTable

        Private ReadOnly _menuBackColor As Color
        Private ReadOnly _accentColor As Color
        Private ReadOnly _dropDownBackColor As Color

        Public Sub New(menuBackColor As Color, accentColor As Color, dropDownBackColor As Color)
            _menuBackColor = menuBackColor
            _accentColor = accentColor
            _dropDownBackColor = dropDownBackColor
        End Sub

        Public Overrides ReadOnly Property ToolStripBorder As Color
            Get
                Return _menuBackColor
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemBorder As Color
            Get
                Return _accentColor
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemSelected As Color
            Get
                Return Color.FromArgb(80, _accentColor)
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemSelectedGradientBegin As Color
            Get
                Return Color.FromArgb(100, _accentColor)
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemSelectedGradientEnd As Color
            Get
                Return Color.FromArgb(60, _accentColor)
            End Get
        End Property

        Public Overrides ReadOnly Property MenuStripGradientBegin As Color
            Get
                Return _menuBackColor
            End Get
        End Property

        Public Overrides ReadOnly Property MenuStripGradientEnd As Color
            Get
                Return _menuBackColor
            End Get
        End Property

        Public Overrides ReadOnly Property ImageMarginGradientBegin As Color
            Get
                Return _dropDownBackColor
            End Get
        End Property

        Public Overrides ReadOnly Property ImageMarginGradientMiddle As Color
            Get
                Return _dropDownBackColor
            End Get
        End Property

        Public Overrides ReadOnly Property ImageMarginGradientEnd As Color
            Get
                Return _dropDownBackColor
            End Get
        End Property

        Public Overrides ReadOnly Property ButtonSelectedBorder As Color
            Get
                Return _accentColor
            End Get
        End Property
    End Class

    Private Async Sub DeveloperToolsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeveloperToolsToolStripMenuItem.Click
        Dim currentWebView = GetCurrentWebView()

        If currentWebView Is Nothing Then
            MessageBox.Show("There is no active tab available. Open a tab and try again.")
            Return
        End If

        Await currentWebView.EnsureCoreWebView2Async()

        If currentWebView.CoreWebView2 Is Nothing Then
            MessageBox.Show("The current tab is still initializing. Please wait a moment and try again.")
            Return
        End If

        currentWebView.CoreWebView2.OpenDevToolsWindow()
    End Sub

    Private Sub ClearHistoryToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ClearHistoryToolStripMenuItem1.Click
        BrowserHistoryManager.ClearHistory()
    End Sub
End Class
