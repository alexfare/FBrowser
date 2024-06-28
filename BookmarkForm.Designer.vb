<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BookmarkForm
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
        BookmarkList = New ListBox()
        History = New Label()
        SuspendLayout()
        ' 
        ' BookmarkList
        ' 
        BookmarkList.FormattingEnabled = True
        BookmarkList.ItemHeight = 15
        BookmarkList.Location = New Point(12, 37)
        BookmarkList.Name = "BookmarkList"
        BookmarkList.Size = New Size(317, 409)
        BookmarkList.TabIndex = 0
        ' 
        ' History
        ' 
        History.AutoSize = True
        History.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        History.Location = New Point(12, 9)
        History.Name = "History"
        History.Size = New Size(113, 25)
        History.TabIndex = 3
        History.Text = "Bookmarks"
        ' 
        ' BookmarkForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(340, 450)
        Controls.Add(History)
        Controls.Add(BookmarkList)
        Name = "BookmarkForm"
        Text = "BookmarkForm"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents BookmarkList As ListBox
    Friend WithEvents History As Label
End Class
