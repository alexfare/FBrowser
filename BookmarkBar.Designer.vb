<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BookmarkBar
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.BtnAddBookmark = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(965, 23)
        Me.FlowLayoutPanel1.TabIndex = 8
        '
        'BtnAddBookmark
        '
        Me.BtnAddBookmark.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnAddBookmark.Location = New System.Drawing.Point(974, 3)
        Me.BtnAddBookmark.Name = "BtnAddBookmark"
        Me.BtnAddBookmark.Size = New System.Drawing.Size(75, 23)
        Me.BtnAddBookmark.TabIndex = 9
        Me.BtnAddBookmark.Text = "Bookmark"
        Me.BtnAddBookmark.UseVisualStyleBackColor = True
        '
        'BookmarkBar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.BtnAddBookmark)
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Name = "BookmarkBar"
        Me.Size = New System.Drawing.Size(1056, 32)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents BtnAddBookmark As Button
End Class
