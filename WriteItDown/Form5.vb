Class Form5
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        If RadioButton1.Checked Then
            Form6.Show()
            Using dlg As New OpenFileDialog
                Form6.RichTextBox1.LoadFile("C:\Programme\WriteItDown\Tasten.wid")
            End Using

        ElseIf RadioButton2.Checked Then
            Form6.Show()
            Using dlg As New OpenFileDialog
                Form6.RichTextBox1.LoadFile("C:\Programme\WriteItDown\Allg.wid")
            End Using

        ElseIf RadioButton5.Checked Then
            Form6.Show()
            Using dlg As New OpenFileDialog
                Form6.RichTextBox1.LoadFile("C:\Programme\WriteItDown\err.wid")
            End Using

        ElseIf RadioButton6.Checked Then
            Form6.Show()
            Using dlg As New OpenFileDialog
                Form6.RichTextBox1.LoadFile("C:\Programme\WriteItDown\ged.wid")
            End Using

        End If
    End Sub
End Class