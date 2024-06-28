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
        btnClearHistory = New Button()
        History = New Label()
        SuspendLayout()
        ' 
        ' ListBox1
        ' 
        ListBox1.FormattingEnabled = True
        ListBox1.ItemHeight = 15
        ListBox1.Location = New Point(12, 41)
        ListBox1.Name = "ListBox1"
        ListBox1.Size = New Size(401, 394)
        ListBox1.TabIndex = 0
        ' 
        ' btnClearHistory
        ' 
        btnClearHistory.Location = New Point(327, 13)
        btnClearHistory.Name = "btnClearHistory"
        btnClearHistory.Size = New Size(86, 23)
        btnClearHistory.TabIndex = 1
        btnClearHistory.Text = "Clear History"
        btnClearHistory.UseVisualStyleBackColor = True
        ' 
        ' History
        ' 
        History.AutoSize = True
        History.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        History.Location = New Point(12, 9)
        History.Name = "History"
        History.Size = New Size(78, 25)
        History.TabIndex = 2
        History.Text = "History"
        ' 
        ' HistoryForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(425, 450)
        Controls.Add(History)
        Controls.Add(btnClearHistory)
        Controls.Add(ListBox1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "HistoryForm"
        Text = "HistoryForm"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents btnClearHistory As Button
    Friend WithEvents History As Label
End Class
