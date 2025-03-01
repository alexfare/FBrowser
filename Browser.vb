Imports Microsoft.Web.WebView2.Core
Imports Microsoft.Web.WebView2.WinForms

Public Class Browser
    Private historyList As New List(Of HistoryItem)

    Private Async Sub Browser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        historyList = BrowserHistoryManager.LoadHistory()
        TabControl1.TabPages.Clear()
        Await AddNewTab("https://alexfare.com") 'Allow user to change this in the future

        'Set the tab size and draw mode
        TabControl1.DrawMode = TabDrawMode.OwnerDrawFixed
        TabControl1.ItemSize = New Size(145, 25) ' Set a fixed size for tabs
        AddHandler TabControl1.DrawItem, AddressOf TabControl1_DrawItem

        InitializeAsync()

        'Set focus to the URL TextBox of the current tab
        Dim currentTab = TabControl1.SelectedTab
        Dim textBoxUrl = CType(currentTab.Controls.Find("textBoxUrl", True).FirstOrDefault(), TextBox)
        If textBoxUrl IsNot Nothing Then
            textBoxUrl.Focus()
        End If

        'get version
        Dim getVersion As String = My.Settings.Version
        Me.Text = "FBrowser - " + getVersion
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

    'Highlight the text when TxtURL receives focus
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

    Private Async Function AddNewTab(Optional url As String = "") As Task
        Dim newTab As New TabPage("New Tab")

        Dim navPanel As New Panel With {
            .Dock = DockStyle.Top,
            .Height = 30
        }

        Dim btnBack As New Button With {.Text = "Back", .Width = 60, .Left = 10}
        Dim btnRefresh As New Button With {.Text = "Refresh", .Width = 60, .Left = 80}
        Dim btnForward As New Button With {.Text = "Forward", .Width = 60, .Left = 150}
        Dim btnGo As New Button With {.Text = "Go", .Width = 60, .Anchor = AnchorStyles.Top Or AnchorStyles.Right}
        Dim initialLeftPosition As Integer = 220
        Dim rightPadding As Integer = 10
        Dim textBoxUrlWidth As Integer = navPanel.Width - (initialLeftPosition + btnGo.Width + rightPadding)

        Dim textBoxUrl As New TextBox With {.Left = initialLeftPosition, .Name = "textBoxUrl", .Width = textBoxUrlWidth, .Anchor = AnchorStyles.Left Or AnchorStyles.Right}

        btnGo.Left = textBoxUrl.Left + textBoxUrl.Width + 10

        navPanel.Controls.Add(btnBack)
        navPanel.Controls.Add(btnRefresh)
        navPanel.Controls.Add(btnForward)
        navPanel.Controls.Add(textBoxUrl)
        navPanel.Controls.Add(btnGo)

        Dim newWebView As New Microsoft.Web.WebView2.WinForms.WebView2 With {
            .Name = "WebView2",
            .Dock = DockStyle.Fill
        }

        newTab.Controls.Add(newWebView)
        newTab.Controls.Add(navPanel)

        TabControl1.TabPages.Add(newTab)
        TabControl1.SelectedTab = newTab

        AddHandler btnBack.Click, Sub(sender, e)
                                      If newWebView.CoreWebView2 IsNot Nothing Then
                                          newWebView.CoreWebView2.GoBack()
                                      End If
                                  End Sub

        AddHandler btnRefresh.Click, Sub(sender, e)
                                         If newWebView.CoreWebView2 IsNot Nothing Then
                                             newWebView.CoreWebView2.Reload()
                                         End If
                                     End Sub

        AddHandler btnForward.Click, Sub(sender, e)
                                         If newWebView.CoreWebView2 IsNot Nothing Then
                                             newWebView.CoreWebView2.GoForward()
                                         End If
                                     End Sub

        AddHandler btnGo.Click, Sub(sender, e)
                                    If newWebView.CoreWebView2 IsNot Nothing Then
                                        NavigateToUrl(newWebView, textBoxUrl.Text)
                                    End If
                                End Sub

        AddHandler textBoxUrl.KeyPress, Sub(sender, e)
                                            If e.KeyChar = Convert.ToChar(Keys.Enter) Then
                                                e.Handled = True
                                                If newWebView.CoreWebView2 IsNot Nothing Then
                                                    NavigateToUrl(newWebView, textBoxUrl.Text)
                                                End If
                                            End If
                                        End Sub

        AddHandler newWebView.CoreWebView2InitializationCompleted, Sub(sender, args)
                                                                       If args.IsSuccess Then
                                                                           AddHandler newWebView.CoreWebView2.NewWindowRequested, AddressOf CoreWebView2_NewWindowRequested
                                                                           If Not String.IsNullOrEmpty(url) Then
                                                                               NavigateToUrl(newWebView, url)
                                                                               UpdateTabText(newTab, url)
                                                                           End If
                                                                       End If
                                                                   End Sub

        AddHandler newWebView.NavigationStarting, Sub(sender, e)
                                                      UpdateUrlTextBox(newTab, e.Uri)
                                                  End Sub
        AddHandler newWebView.NavigationCompleted, Sub(sender, e)
                                                       UpdateUrlTextBox(newTab, newWebView.Source.ToString())
                                                       OnNavigationCompleted(newWebView, e)
                                                   End Sub
        AddHandler newWebView.CoreWebView2InitializationCompleted, Sub(sender, args)
                                                                       If args.IsSuccess Then
                                                                           AddHandler newWebView.CoreWebView2.DocumentTitleChanged, Sub()
                                                                                                                                        UpdateTabTitle(newTab, newWebView.CoreWebView2.DocumentTitle)
                                                                                                                                    End Sub
                                                                       End If
                                                                   End Sub

        Try
            Await newWebView.EnsureCoreWebView2Async(Nothing)
        Catch ex As Exception
            MessageBox.Show("Failed to initialize WebView2 runtime.", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        textBoxUrl.Focus()

        AddHandler navPanel.Resize, Sub(sender, e)
                                        textBoxUrl.Width = navPanel.Width - (initialLeftPosition + btnGo.Width + rightPadding + 10)
                                        btnGo.Left = textBoxUrl.Left + textBoxUrl.Width + 10
                                    End Sub
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

    Private Sub TabControl1_DrawItem(sender As Object, e As DrawItemEventArgs)
        Dim tabPage = TabControl1.TabPages(e.Index)
        Dim tabRect = TabControl1.GetTabRect(e.Index)
        Dim closeButtonSize = 10
        Dim closeButtonPadding = 7 'Adjust the padding to ensure space between text and button

        Dim closeButtonX = tabRect.Right - closeButtonSize - closeButtonPadding
        Dim closeButtonRect = New Rectangle(closeButtonX, tabRect.Top + (tabRect.Height - closeButtonSize) / 2, closeButtonSize, closeButtonSize)

        Dim textHeight = TextRenderer.MeasureText(tabPage.Text, tabPage.Font).Height
        Dim textY = tabRect.Top + (tabRect.Height - textHeight) / 2

        Dim textRect = New Rectangle(tabRect.X + closeButtonPadding, textY, tabRect.Width - closeButtonSize - closeButtonPadding * 2, tabRect.Height)
        TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font, textRect, tabPage.ForeColor, TextFormatFlags.Left)

        e.Graphics.DrawRectangle(Pens.Black, closeButtonRect)
        e.Graphics.DrawLine(Pens.Black, closeButtonRect.Left, closeButtonRect.Top, closeButtonRect.Right, closeButtonRect.Bottom)
        e.Graphics.DrawLine(Pens.Black, closeButtonRect.Right, closeButtonRect.Top, closeButtonRect.Left, closeButtonRect.Bottom)
    End Sub

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
        Dim historyForm As New HistoryForm()
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

    Private Sub DeleteCacheToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteCacheToolStripMenuItem.Click
        MessageBox.Show("Coming Soon.")
    End Sub
End Class