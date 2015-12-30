Imports System
Imports System.IO
Imports System.Collections
Public Class Form4

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
            If Form2.TextBox1.Text = "Nein" Then
                If RichTextBoxEx1.Text = Nothing Then
                End If
                MessageBox.Show("Bitte Text eingeben!", "Speichern", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

            If Form2.TextBox1.Text = "Ja" Then
                If RichTextBoxEx1.Text = Nothing Then
                    MessageBox.Show("Please Enter Text!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If
        End If
    End Sub

    Private Sub DruckenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DruckenToolStripMenuItem.Click
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
            PrintPreviewDialog1.ShowDialog()
        End If
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
    Private Sub PrintDocument1_EndPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles PrintDocument1.EndPrint
        ' Variablen für Druck zurücksezen
        RichTextBoxEx1.FormatRangeDone()
        initPrint = False
        Me.PrintRtb.printDoc_EndPrint(sender, e)

        ' In PrintPreviewDialog.Text die Anzahl Gesamtseiten ausgeben
        PrintPreviewDialog1.Text = "Druck-Vorschau (Anzahl Seiten: " & iPages & ")"
    End Sub

    Private Sub NormaleAnwendungToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NormaleAnwendungToolStripMenuItem.Click
        Form1.RichTextBoxEx1.Text = Me.RichTextBoxEx1.Text
        Form1.Show()
        Me.Close()
    End Sub

    Private Sub SchriftartToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SchriftartToolStripMenuItem.Click
        Using dlg As New FontDialog
            If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
                RichTextBoxEx1.SelectionFont = dlg.Font
            End If
        End Using
    End Sub

    Private Sub SchriftfarbeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SchriftfarbeToolStripMenuItem.Click
        Using dlg As New ColorDialog
            If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
                RichTextBoxEx1.SelectionColor = dlg.Color
            End If
        End Using
    End Sub

    Private Sub HintergrundfarbeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HintergrundfarbeToolStripMenuItem.Click
        Using dlg As New ColorDialog
            If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
                RichTextBoxEx1.BackColor = dlg.Color
            End If
        End Using
    End Sub
End Class