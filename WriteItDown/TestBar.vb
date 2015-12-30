Imports System.Windows.Forms
Imports System.Drawing
Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
<ToolboxBitmap(GetType(ProgressBar))> _
Public Class TestBar
    Inherits Control

    Public Sub New()
        MyBase.New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw, True)
        UpdateStyles()
        Me.SuspendLayout()
        Me.ResumeLayout(False)
    End Sub
#Region "properties"
    Dim max As Integer = 100
    <Category("Werte")>
    Public Property Maximum() As Integer
        Get
            Return max
        End Get
        Set(ByVal value As Integer)
            max = value
        End Set
    End Property
    Dim min As Integer = 0
    <Category("Werte")>
    Public Property Minimum() As Integer
        Get
            Return min
        End Get
        Set(ByVal value As Integer)
            min = value
        End Set
    End Property
    Dim val As Integer = 70
    <Category("Werte")>
    Public Property Value() As Integer
        Get
            Return val
        End Get
        Set(ByVal value As Integer)
            val = value
            Me.Invalidate()
        End Set
    End Property
    Dim balkencol As Color = Color.Green
    Public Property BalkenColor() As Color
        Get
            Return balkencol
        End Get
        Set(ByVal value As Color)
            balkencol = value
        End Set
    End Property
#End Region
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        InstallQuality(e)
        Drawbar(e)
    End Sub
    Public Sub Drawbar(ByVal e As System.Windows.Forms.PaintEventArgs)
        With e.Graphics
            Dim HintergrundHoehe As Integer = MyBase.ClientRectangle.Height
            'Die Höhe des Hintergrunds
            Dim HintergrundBreite As Integer = MyBase.ClientRectangle.Width
            'Die Breite des Hntergrunds
            Dim BalkenHoehe As Integer = CInt(MyBase.ClientRectangle.Height)
            'Die Höhe des Balken (entspricht der Höhe des Hintergrunds)
            Dim Differenz As Integer = Me.Maximum - Me.Minimum
            'Dies brauchen wir zum Berechnen der Breite des Balkens, die sich dann ändern wird
            Dim BalkenBreite As Integer = Width * (Value - Minimum) \ (Maximum - Minimum)
            'Die Value ändert sich dann immer und das Ganze wird neu berechnet, sodass der Balken länger wird
            Dim Hintergrundfarbe As Color = Color.White
            'Wir setzen die Hintergrundfarbe auf weiß. (benutzerdefiniert)
            Dim Balkenfarbe As Color = Me.BalkenColor
            'Wir setzen die Balkenfarbe auf grün (benutzerdefiniert)
            Dim hintergrundrect As New Rectangle(0, 0, HintergrundBreite, HintergrundHoehe)
            'Wir erstellen ein neues Rectangle mit der Location (0,0) und den Werten der oben deklarierten Variablen
            .FillRectangle(New SolidBrush(Hintergrundfarbe), hintergrundrect)
            'Wir füllen das Rectangle mit einer SolidBrush (festen Farbe)
            Dim balkenrect As New Rectangle(0, 0, BalkenBreite, BalkenHoehe)
            'Wir erstellen ein neues Rectangle für den Balken mit der Location (0,0) und den Werten der oben deklarierten Variablen
            .FillRectangle(New SolidBrush(Balkenfarbe), balkenrect)
            'Wir füllen das Rectangle wieder mit einer SolidBrush (festen Farbe)
        End With
    End Sub
    Public Sub InstallQuality(ByVal e As System.Windows.Forms.PaintEventArgs)
        With e.Graphics
            .SmoothingMode = SmoothingMode.HighQuality
            .TextRenderingHint = TextRenderingHint.AntiAlias
            .PixelOffsetMode = PixelOffsetMode.HighQuality
            .InterpolationMode = InterpolationMode.HighQualityBilinear
            .CompositingQuality = CompositingQuality.HighQuality
        End With
    End Sub
End Class
