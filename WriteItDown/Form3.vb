Public Class Form3

    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Timer1.Start()
        TestBar1.BalkenColor = Color.MediumAquamarine
    End Sub
    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
        Form2.NumericUpDown1.Value += 1
        TestBar1.Value = Form2.NumericUpDown1.Value
        Label1.Text = Form2.NumericUpDown1.Value
        If Form2.NumericUpDown1.Value = 30 Then
            Timer1.Interval = 10
        End If
        If Form2.NumericUpDown1.Value = 100 Then
            Timer1.Stop()
            Timer1.Interval = 100
            Form2.NumericUpDown1.Value = 0
            Form1.Show()
            Me.Close()
        End If
    End Sub
    Private Sub Form1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.U Then
            Timer1.Stop()
            Timer1.Interval = 100
            Form2.NumericUpDown1.Value = 0
            Form1.Show()
            Me.Close()
        End If
    End Sub
End Class