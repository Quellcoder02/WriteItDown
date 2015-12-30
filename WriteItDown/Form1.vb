Imports System
Imports System.IO
Imports System.Collections
Public Class Form1
    ' Variablen für Druckfunktion
    Private m_nFirstCharOnPage As Integer ' Variable für Druck
    Private iPage As Short
    Private iPages As Short
    Private initPrint As Boolean = False
    Private PrintRtb As New RichTextBoxPrint

    Private Sub SpeichernUnterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpeichernUnterToolStripMenuItem.Click
        If Not RichTextBoxEx1.Text = Nothing Then
            Using dlg As New SaveFileDialog
                dlg.Filter = "WriteItDown|*.wid"
                If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
                    RichTextBoxEx1.SaveFile(dlg.FileName)
                End If
            End Using
        Else
                If RichTextBoxEx1.Text = Nothing Then
                End If
                MessageBox.Show("Bitte Text eingeben!", "Speichern", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
    End Sub

    Private Sub LadenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LadenToolStripMenuItem.Click
        Using dlg As New OpenFileDialog
            dlg.Filter = "WriteItDown|*.wid|Alle Dateien|*.*"
            If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
                If dlg.FileName.ToLower.EndsWith(".wid") Then
                    RichTextBoxEx1.LoadFile(dlg.FileName, RichTextBoxStreamType.RichText)
                Else
                    RichTextBoxEx1.LoadFile(dlg.FileName, RichTextBoxStreamType.PlainText)
                End If
            End If
        End Using
    End Sub

    Private Sub SchriftartToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SchriftartToolStripMenuItem1.Click
        Using dlg As New FontDialog
            If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
                RichTextBoxEx1.SelectionFont = dlg.Font
            End If
        End Using
    End Sub

    Private Sub SchriftfarbeToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SchriftfarbeToolStripMenuItem1.Click
        Using dlg As New ColorDialog
            If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
                RichTextBoxEx1.SelectionColor = dlg.Color
            End If
        End Using
    End Sub

    Private Sub HintergrungfarbeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HintergrungfarbeToolStripMenuItem.Click
        Using dlg As New ColorDialog
            If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
                RichTextBoxEx1.BackColor = dlg.Color
            End If
        End Using
    End Sub

    Private Sub DruckenToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DruckenToolStripMenuItem1.Click
        If Form2.TextBox1.Text = "Nein" Then
            If RichTextBoxEx1.Text = Nothing Then
                MessageBox.Show("Bitte Text eingeben!", "Drucken", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If

        If Form2.TextBox1.Text = "Ja" Then
            If RichTextBoxEx1.Text = Nothing Then
                MessageBox.Show("Please Enter Text!", "Print", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Else
            Using dlg As New PrintDialog
                dlg.Document = Me.PrintDocument1
                If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Me.PrintDocument1.Print()
                End If
            End Using
        End If
    End Sub

    Private Sub DruckervorschauToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DruckervorschauToolStripMenuItem.Click
            If RichTextBoxEx1.Text = Nothing Then
                MessageBox.Show("Bitte Text eingeben!", "Drucken", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            PrintPreviewDialog1.ShowDialog()
        End If
    End Sub

    Private Sub PrintDocument1_BeginPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles PrintDocument1.BeginPrint
        ' Start at the beginning of the text
        m_nFirstCharOnPage = 0
        iPage = 0
        Me.PrintRtb.printDoc_BeginPrint(sender, e)
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        If initPrint = True Then
            iPages += 1
        End If
        iPage += 1

        ' Code für Kopfzeile (Seitenanzahl)
        Dim oPen6 As New Pen(Color.Black, 1.2)
        Dim oFont1 As New Font("Arial", 10, FontStyle.Bold)
        Me.PrintRtb.printDoc_PrintPage(sender, e)

        With e.MarginBounds
            e.Graphics.DrawLine(oPen6, .X, .Y - 15, e.PageBounds.Width - .X - (e.PageBounds.Right - .Right), .Y - 15)

            If initPrint = True Then
                e.Graphics.DrawString("Seite: " & iPage & "/?", oFont1, Brushes.Black, .X, .Y - 35)
            Else
                e.Graphics.DrawString("Seite: " & iPage & "/" & iPages, oFont1, Brushes.Black, .X, .Y - 35)
            End If
        End With

        With RichTextBoxEx1
            m_nFirstCharOnPage = .FormatRange(False, e, m_nFirstCharOnPage, .TextLength)
            ' Prüfen ob noch Seiten gedruckt werden müssen
            e.HasMorePages = (m_nFirstCharOnPage < .TextLength)
        End With
    End Sub

    Private Sub Form1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.D Then
            Dialog1.Show()
        End If

        If e.Control AndAlso e.KeyCode = Keys.P Then
            PrintPreviewDialog1.Show()
        End If

        If e.Control AndAlso e.KeyCode = Keys.S Then
            If Not RichTextBoxEx1.Text = Nothing Then
                Using dlg As New SaveFileDialog
                    dlg.Filter = "WriteItDown|*.wid"
                    If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
                        RichTextBoxEx1.SaveFile(dlg.FileName)
                    End If
                End Using
            Else
                If RichTextBoxEx1.Text = Nothing Then
                End If
                MessageBox.Show("Bitte Text eingeben!", "Speichern", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If
    End Sub

    Private Sub PrintDocument1_EndPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles PrintDocument1.EndPrint
        ' Variablen für Druck zurücksezen
        RichTextBoxEx1.FormatRangeDone()
        initPrint = False
        Me.PrintRtb.printDoc_EndPrint(sender, e)

        ' In PrintPreviewDialog.Text die Anzahl Gesamtseiten ausgeben
        PrintPreviewDialog1.Text = "Druck-Vorschau (Anzahl Seiten: " & iPages & ")"
    End Sub

    Private Sub RichTextBoxEx1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RichTextBoxEx1.TextChanged
        RichTextBoxEx1.Text.Split(New String() {" ", vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)
        ToolStripStatusLabel4.Text = RichTextBoxEx1.Text.Split(New String() {" ", vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries).Length
    End Sub

    Private Sub HifeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HifeToolStripMenuItem.Click
        Form5.Show()
    End Sub

    Private Sub MIniAnwendungToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MiniAnwendungToolStripMenuItem.Click
        Form4.RichTextBoxEx1.Text = Me.RichTextBoxEx1.Text
        Form4.Show()
        Me.Hide()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    Private Sub Panels_Click(sender As Object, e As EventArgs) Handles Panel1.Click, Panel2.Click, Panel3.Click
        RichTextBoxEx1.Select()
        RichTextBoxEx1.Focus()
    End Sub
End Class