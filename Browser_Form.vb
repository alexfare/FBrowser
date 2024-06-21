Imports Microsoft.Web.WebView2.Core

Public Class Browser_Form

    Private WithEvents bookmarkBar As BookmarkBar

    Private Async Sub Browser_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Clear any existing tabs
        TabControl1.TabPages.Clear()
        ' Initialize the first tab with default URL
        Await AddNewTab("https://sites.google.com/view/alexfare-com/home")

        ' Initialize Bookmark Bar
        'bookmarkBar = New BookmarkBar()
        'AddHandler bookmarkBar.BookmarkClicked, AddressOf OnBookmarkClicked
        'bookmarkBar.Dock = DockStyle.Bottom
        'Controls.Add(bookmarkBar)

        ' Set the tab size and draw mode
        TabControl1.DrawMode = TabDrawMode.OwnerDrawFixed
        TabControl1.ItemSize = New Size(75, 17) ' Adjust width and height as needed
        TabControl1.SizeMode = TabSizeMode.Fixed
        TextBox1.Focus()
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

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            e.Handled = True
            URLSearch()
        End If
    End Sub

    Private Async Sub BtnNewTab_Click(sender As Object, e As EventArgs) Handles BtnNewTab.Click
        Await AddNewTab()
    End Sub

    Private Sub URLSearch()
        Dim currentWebView = GetCurrentWebView()
        If currentWebView IsNot Nothing AndAlso currentWebView.CoreWebView2 IsNot Nothing Then
            Dim input As String = TextBox1.Text.Trim()
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
        Dim newWebView As New Microsoft.Web.WebView2.WinForms.WebView2 With {
            .Name = "WebView2",
            .Dock = DockStyle.Fill
        }

        ' Handle the NewWindowRequested event
        AddHandler newWebView.CoreWebView2InitializationCompleted, Async Sub(sender, args)
                                                                       If args.IsSuccess Then
                                                                           AddHandler newWebView.CoreWebView2.NewWindowRequested, AddressOf CoreWebView2_NewWindowRequested
                                                                           ' Navigate to the URL if provided
                                                                           If Not String.IsNullOrEmpty(url) Then
                                                                               newWebView.CoreWebView2.Navigate(url)
                                                                               UpdateTabText(newTab, url)
                                                                           End If
                                                                       End If
                                                                   End Sub

        newTab.Controls.Add(newWebView)
        TabControl1.TabPages.Add(newTab)
        TabControl1.SelectedTab = newTab

        Try
            Await newWebView.EnsureCoreWebView2Async(Nothing)
        Catch ex As Exception
            MessageBox.Show("Failed to initialize WebView2 runtime.", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Async Sub CoreWebView2_NewWindowRequested(sender As Object, e As CoreWebView2NewWindowRequestedEventArgs)
        e.Handled = True ' Prevent the default new window behavior
        Await AddNewTab()
        Dim newWebView = GetCurrentWebView()
        If newWebView IsNot Nothing Then
            newWebView.CoreWebView2.Navigate(e.Uri)
        End If
    End Sub

    Private Sub UpdateTabText(tab As TabPage, url As String)
        Dim domainName As String = GetDomainNameFromUrl(url)
        If domainName.Length > 8 Then
            domainName = domainName.Substring(0, 8) & "..."
        End If
        tab.Text = domainName
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

    Private Sub TabControl1_DrawItem(sender As Object, e As DrawItemEventArgs) Handles TabControl1.DrawItem
        Dim tabPage = TabControl1.TabPages(e.Index)
        Dim tabRect = TabControl1.GetTabRect(e.Index)
        Dim closeButtonSize = 10
        Dim closeButtonPadding = 10 ' Adjust the padding to ensure space between text and button

        ' Calculate the position for the close button based on the text width
        Dim textSize = TextRenderer.MeasureText(e.Graphics, tabPage.Text, tabPage.Font)
        Dim closeButtonX = tabRect.Left + textSize.Width + closeButtonPadding
        Dim closeButtonRect = New Rectangle(closeButtonX, tabRect.Top + (tabRect.Height - closeButtonSize) / 2, closeButtonSize, closeButtonSize)

        ' Draw the tab header text
        TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font, tabRect, tabPage.ForeColor, TextFormatFlags.Left)

        ' Draw the close button
        e.Graphics.DrawRectangle(Pens.Black, closeButtonRect)
        e.Graphics.DrawLine(Pens.Black, closeButtonRect.Left, closeButtonRect.Top, closeButtonRect.Right, closeButtonRect.Bottom)
        e.Graphics.DrawLine(Pens.Black, closeButtonRect.Right, closeButtonRect.Top, closeButtonRect.Left, closeButtonRect.Bottom)
    End Sub

    Private Sub TabControl1_MouseDown(sender As Object, e As MouseEventArgs) Handles TabControl1.MouseDown
        Dim closeButtonSize = 10
        Dim closeButtonPadding = 10 ' Adjust the padding to ensure space between text and button

        For i As Integer = 0 To TabControl1.TabPages.Count - 1
            Dim tabRect = TabControl1.GetTabRect(i)
            Dim textSize = TextRenderer.MeasureText(TabControl1.TabPages(i).Text, TabControl1.TabPages(i).Font)
            Dim closeButtonX = tabRect.Left + textSize.Width + closeButtonPadding
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
            TextBox1.Text = url
            UpdateTabText(TabControl1.SelectedTab, url)
        End If
    End Sub

End Class
