<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HistoryForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HistoryForm))
        ListBox1 = New ListBox()
        History = New Label()
        Panel1 = New Panel()
        MenuStrip1 = New MenuStrip()
        FileToolStripMenuItem = New ToolStripMenuItem()
        CloseToolStripMenuItem = New ToolStripMenuItem()
        ClearHistoryToolStripMenuItem = New ToolStripMenuItem()
        Panel1.SuspendLayout()
        MenuStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ListBox1
        ' 
        ListBox1.Dock = DockStyle.Fill
        ListBox1.FormattingEnabled = True
        ListBox1.HorizontalScrollbar = True
        ListBox1.ItemHeight = 15
        ListBox1.Location = New Point(0, 0)
        ListBox1.Name = "ListBox1"
        ListBox1.Size = New Size(530, 475)
        ListBox1.TabIndex = 0
        ' 
        ' History
        ' 
        History.AutoSize = True
        History.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        History.Location = New Point(12, 24)
        History.Name = "History"
        History.Size = New Size(78, 25)
        History.TabIndex = 2
        History.Text = "History"
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(ListBox1)
        Panel1.Location = New Point(12, 52)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(530, 475)
        Panel1.TabIndex = 3
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.Items.AddRange(New ToolStripItem() {FileToolStripMenuItem, ClearHistoryToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(554, 24)
        MenuStrip1.TabIndex = 4
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' FileToolStripMenuItem
        ' 
        FileToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {CloseToolStripMenuItem})
        FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        FileToolStripMenuItem.Size = New Size(37, 20)
        FileToolStripMenuItem.Text = "File"
        ' 
        ' CloseToolStripMenuItem
        ' 
        CloseToolStripMenuItem.Name = "CloseToolStripMenuItem"
        CloseToolStripMenuItem.Size = New Size(180, 22)
        CloseToolStripMenuItem.Text = "Close"
        ' 
        ' ClearHistoryToolStripMenuItem
        ' 
        ClearHistoryToolStripMenuItem.Name = "ClearHistoryToolStripMenuItem"
        ClearHistoryToolStripMenuItem.Size = New Size(87, 20)
        ClearHistoryToolStripMenuItem.Text = "Clear History"
        ' 
        ' HistoryForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(554, 541)
        Controls.Add(Panel1)
        Controls.Add(History)
        Controls.Add(MenuStrip1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MainMenuStrip = MenuStrip1
        Name = "HistoryForm"
        Text = "HistoryForm"
        Panel1.ResumeLayout(False)
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents History As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClearHistoryToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CloseToolStripMenuItem As ToolStripMenuItem
End Class
