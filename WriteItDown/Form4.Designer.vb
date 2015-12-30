<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form4
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form4))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.SpeichernUnterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DruckenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DruckervorschauToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FormatToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SchriftartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SchriftfarbeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HintergrundfarbeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NormaleAnwendungToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.RichTextBoxEx1 = New WriteItDown.RichTextBoxEx()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SpeichernUnterToolStripMenuItem, Me.DruckenToolStripMenuItem, Me.DruckervorschauToolStripMenuItem, Me.FormatToolStripMenuItem, Me.NormaleAnwendungToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(484, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'SpeichernUnterToolStripMenuItem
        '
        Me.SpeichernUnterToolStripMenuItem.Name = "SpeichernUnterToolStripMenuItem"
        Me.SpeichernUnterToolStripMenuItem.Size = New System.Drawing.Size(112, 20)
        Me.SpeichernUnterToolStripMenuItem.Text = "Speichern Unter..."
        '
        'DruckenToolStripMenuItem
        '
        Me.DruckenToolStripMenuItem.Name = "DruckenToolStripMenuItem"
        Me.DruckenToolStripMenuItem.Size = New System.Drawing.Size(63, 20)
        Me.DruckenToolStripMenuItem.Text = "Drucken"
        '
        'DruckervorschauToolStripMenuItem
        '
        Me.DruckervorschauToolStripMenuItem.Name = "DruckervorschauToolStripMenuItem"
        Me.DruckervorschauToolStripMenuItem.Size = New System.Drawing.Size(108, 20)
        Me.DruckervorschauToolStripMenuItem.Text = "Druckervorschau"
        '
        'FormatToolStripMenuItem
        '
        Me.FormatToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SchriftartToolStripMenuItem, Me.SchriftfarbeToolStripMenuItem, Me.HintergrundfarbeToolStripMenuItem})
        Me.FormatToolStripMenuItem.Name = "FormatToolStripMenuItem"
        Me.FormatToolStripMenuItem.Size = New System.Drawing.Size(57, 20)
        Me.FormatToolStripMenuItem.Text = "Format"
        '
        'SchriftartToolStripMenuItem
        '
        Me.SchriftartToolStripMenuItem.Name = "SchriftartToolStripMenuItem"
        Me.SchriftartToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.SchriftartToolStripMenuItem.Text = "Schriftart"
        '
        'SchriftfarbeToolStripMenuItem
        '
        Me.SchriftfarbeToolStripMenuItem.Name = "SchriftfarbeToolStripMenuItem"
        Me.SchriftfarbeToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.SchriftfarbeToolStripMenuItem.Text = "Schriftfarbe"
        '
        'HintergrundfarbeToolStripMenuItem
        '
        Me.HintergrundfarbeToolStripMenuItem.Name = "HintergrundfarbeToolStripMenuItem"
        Me.HintergrundfarbeToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.HintergrundfarbeToolStripMenuItem.Text = "Hintergrundfarbe"
        '
        'NormaleAnwendungToolStripMenuItem
        '
        Me.NormaleAnwendungToolStripMenuItem.Name = "NormaleAnwendungToolStripMenuItem"
        Me.NormaleAnwendungToolStripMenuItem.Size = New System.Drawing.Size(133, 20)
        Me.NormaleAnwendungToolStripMenuItem.Text = "Normale Anwendung"
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'PrintDocument1
        '
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Document = Me.PrintDocument1
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'RichTextBoxEx1
        '
        Me.RichTextBoxEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBoxEx1.Location = New System.Drawing.Point(0, 24)
        Me.RichTextBoxEx1.Name = "RichTextBoxEx1"
        Me.RichTextBoxEx1.Size = New System.Drawing.Size(484, 307)
        Me.RichTextBoxEx1.TabIndex = 2
        Me.RichTextBoxEx1.Text = ""
        '
        'Form4
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 331)
        Me.Controls.Add(Me.RichTextBoxEx1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximumSize = New System.Drawing.Size(500, 369)
        Me.MinimumSize = New System.Drawing.Size(500, 369)
        Me.Name = "Form4"
        Me.Text = "Write It Down!"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents SpeichernUnterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DruckenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DruckervorschauToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RichTextBoxEx1 As WriteItDown.RichTextBoxEx
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents NormaleAnwendungToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FormatToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SchriftartToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SchriftfarbeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HintergrundfarbeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
