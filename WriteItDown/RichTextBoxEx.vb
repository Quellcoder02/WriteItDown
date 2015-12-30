Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Drawing.Printing

' An extension to RichTextBox suitable for printing and more
Public Class RichTextBoxEx

    Inherits RichTextBox

    ' Konstante zum De(Aktivieren) der Bilschirmaktualisierung
    ' Fenstersperrung ein-/ausschalten
    Private Const WM_SETREDRAW As Integer = &HB
    Private iLastFindPos As Integer

    <StructLayout(LayoutKind.Sequential)> _
    Private Structure STRUCT_RECT
        Public left As Int32
        Public top As Int32
        Public right As Int32
        Public bottom As Int32
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Private Structure STRUCT_CHARRANGE
        Public cpMin As Int32
        Public cpMax As Int32
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Private Structure STRUCT_FORMATRANGE
        Public hdc As IntPtr
        Public hdcTarget As IntPtr
        Public rc As STRUCT_RECT
        Public rcPage As STRUCT_RECT
        Public chrg As STRUCT_CHARRANGE
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Private Structure STRUCT_CHARFORMAT
        Public cbSize As Integer
        Public dwMask As UInt32
        Public dwEffects As UInt32
        Public yHeight As Int32
        Public yOffset As Int32
        Public crTextColor As Int32
        Public bCharSet As Byte
        Public bPitchAndFamily As Byte
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=32)> _
        Public szFaceName() As Char
    End Structure

    <DllImport("user32.dll")> _
    Private Shared Function SendMessage( _
    ByVal hWnd As IntPtr, _
    ByVal msg As Int32, _
    ByVal wParam As Int32, _
    ByVal lParam As IntPtr) As Int32
    End Function

    ' Windows Message defines
    Private Const WM_USER As Int32 = &H400&
    Private Const EM_FORMATRANGE As Int32 = WM_USER + 57
    Private Const EM_GETCHARFORMAT As Int32 = WM_USER + 58
    Private Const EM_SETCHARFORMAT As Int32 = WM_USER + 68

    ' Defines for EM_GETCHARFORMAT/EM_SETCHARFORMAT
    Private SCF_SELECTION As Int32 = &H1&
    Private SCF_WORD As Int32 = &H2&
    Private SCF_ALL As Int32 = &H4&

    ' Defines for STRUCT_CHARFORMAT member dwMask
    ' (Long because UInt32 is not an intrinsic type)
    Private Const CFM_BOLD As Long = &H1&
    Private Const CFM_ITALIC As Long = &H2&
    Private Const CFM_UNDERLINE As Long = &H4&
    Private Const CFM_STRIKEOUT As Long = &H8&
    Private Const CFM_PROTECTED As Long = &H10&
    Private Const CFM_LINK As Long = &H20&
    Private Const CFM_SIZE As Long = &H80000000&
    Private Const CFM_COLOR As Long = &H40000000&
    Private Const CFM_FACE As Long = &H20000000&
    Private Const CFM_OFFSET As Long = &H10000000&
    Private Const CFM_CHARSET As Long = &H8000000&

    ' Defines for STRUCT_CHARFORMAT member dwEffects
    Private Const CFE_BOLD As Long = &H1&
    Private Const CFE_ITALIC As Long = &H2&
    Private Const CFE_UNDERLINE As Long = &H4&
    Private Const CFE_STRIKEOUT As Long = &H8&
    Private Const CFE_PROTECTED As Long = &H10&
    Private Const CFE_LINK As Long = &H20&
    Private Const CFE_AUTOCOLOR As Long = &H40000000&

    ' Calculate or render the contents of our RichTextBox for printing
    ' 
    ' Parameters:
    ' "measureOnly": If true, only the calculation is performed,
    '   otherwise the text is rendered as well
    ' "e": The PrintPageEventArgs object from the PrintPage event
    ' "charFrom": Index of first character to be printed
    ' "charTo": Index of last character to be printed
    ' 
    ' Return value:
    ' (Index of last character that fitted on the page) + 1
    Public Function FormatRange(ByVal measureOnly As Boolean, _
      ByVal e As PrintPageEventArgs, _
      ByVal charFrom As Integer, _
      ByVal charTo As Integer) As Integer

        ' Specify which characters to print
        Dim cr As STRUCT_CHARRANGE
        cr.cpMin = charFrom
        cr.cpMax = charTo

        ' Specify the area inside page margins
        Dim rc As STRUCT_RECT
        With e.MarginBounds
            rc.top = HundredthInchToTwips(.Top)
            rc.bottom = HundredthInchToTwips(.Bottom)
            rc.left = HundredthInchToTwips(.Left)
            rc.right = HundredthInchToTwips(.Right)
        End With

        ' Specify the page area
        Dim rcPage As STRUCT_RECT
        With e.PageBounds
            rcPage.top = HundredthInchToTwips(.Top)
            rcPage.bottom = HundredthInchToTwips(.Bottom)
            rcPage.left = HundredthInchToTwips(.Left)
            rcPage.right = HundredthInchToTwips(.Right)
        End With

        ' Get device context of output device
        Dim hdc As IntPtr
        hdc = e.Graphics.GetHdc()

        ' Fill in the FORMATRANGE structure
        Dim fr As STRUCT_FORMATRANGE
        fr.chrg = cr
        fr.hdc = hdc
        fr.hdcTarget = hdc
        fr.rc = rc
        fr.rcPage = rcPage

        ' Non-Zero wParam means render, Zero means measure
        Dim wParam As Int32
        If measureOnly Then
            wParam = 0
        Else
            wParam = 1
        End If

        ' Allocate memory for the FORMATRANGE struct and
        ' copy the contents of our struct to this memory
        Dim lParam As IntPtr
        lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fr))
        Marshal.StructureToPtr(fr, lParam, False)

        ' Send the actual Win32 message
        Dim res As Integer
        res = SendMessage(Handle, EM_FORMATRANGE, wParam, lParam)

        ' Free allocated memory
        Marshal.FreeCoTaskMem(lParam)

        ' and release the device context
        e.Graphics.ReleaseHdc(hdc)

        Return res
    End Function

    ' Convert between 1/100 inch (unit used by the .NET framework)
    ' and twips (1/1440 inch, used by Win32 API calls)
    '
    ' Parameter "n": Value in 1/100 inch
    ' Return value: Value in twips
    Private Function HundredthInchToTwips(ByVal n As Integer) As Int32
        Return Convert.ToInt32(n * 14.4)
    End Function

    ' Free cached data from rich edit control after printing
    Public Sub FormatRangeDone()
        Dim lParam As New IntPtr(0)
        SendMessage(Handle, EM_FORMATRANGE, 0, lParam)
    End Sub

    ''' <summary>
    ''' Setzt die Schriftart für den selektierten Text
    ''' </summary>
    ''' <param name="face">Name der Schriftart</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetSelectionFont(ByVal face As String) As Boolean
        Dim cf As New STRUCT_CHARFORMAT()
        cf.cbSize = Marshal.SizeOf(cf)
        cf.dwMask = Convert.ToUInt32(CFM_FACE)

        ' ReDim face name to relevant size
        ReDim cf.szFaceName(32)
        face.CopyTo(0, cf.szFaceName, 0, Math.Min(31, face.Length))

        Dim lParam As IntPtr
        lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf))
        Marshal.StructureToPtr(cf, lParam, False)

        Dim res As Integer
        res = SendMessage(Handle, EM_SETCHARFORMAT, SCF_SELECTION, lParam)
        Return (res = 0)
    End Function

    ''' <summary>
    ''' Setzt die Schriftgrösse des selektierten Textees
    ''' </summary>
    ''' <param name="size">Schriftgrösse</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetSelectionSize(ByVal size As Integer) As Boolean
        Dim cf As New STRUCT_CHARFORMAT()
        cf.cbSize = Marshal.SizeOf(cf)
        cf.dwMask = Convert.ToUInt32(CFM_SIZE)
        ' yHeight is in 1/20 pt
        cf.yHeight = size * 20

        Dim lParam As IntPtr
        lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf))
        Marshal.StructureToPtr(cf, lParam, False)

        Dim res As Integer
        res = SendMessage(Handle, EM_SETCHARFORMAT, SCF_SELECTION, lParam)
        Return (res = 0)
    End Function

    ''' <summary>
    ''' Setzt Fettdruck für selektierten Text oder hebt diesen auf
    ''' </summary>
    ''' <param name="bold">True = Fettdruck; False = normal</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetSelectionBold(ByVal bold As Boolean) As Boolean
        If (bold) Then
            Return SetSelectionStyle(CFM_BOLD, CFE_BOLD)
        Else
            Return SetSelectionStyle(CFM_BOLD, 0)
        End If
    End Function

    ''' <summary>
    ''' Kurssiv-Schrift für selektierten Text setzen oder aufheben
    ''' </summary>
    ''' <param name="italic">True = kursiv; False = nicht kursiv</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetSelectionItalic(ByVal italic As Boolean) As Boolean
        If (italic) Then
            Return SetSelectionStyle(CFM_ITALIC, CFE_ITALIC)
        Else
            Return SetSelectionStyle(CFM_ITALIC, 0)
        End If
    End Function

    ''' <summary>
    ''' Unterstreicht den selektierten Text oder hebt diese auf
    ''' </summary>
    ''' <param name="underlined">True = unterstrichen; 
    ''' False = nicht unterstrichen</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetSelectionUnderlined( _
      ByVal underlined As Boolean) As Boolean

        If (underlined) Then
            Return SetSelectionStyle(CFM_UNDERLINE, CFE_UNDERLINE)
        Else
            Return SetSelectionStyle(CFM_UNDERLINE, 0)
        End If
    End Function

    Private Function SetSelectionStyle(ByVal mask As Int32, _
      ByVal effect As Int32) As Boolean

        Dim cf As New STRUCT_CHARFORMAT()
        cf.cbSize = Marshal.SizeOf(cf)
        cf.dwMask = Convert.ToUInt32(mask)
        cf.dwEffects = Convert.ToUInt32(effect)

        Dim lParam As IntPtr
        lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf))
        Marshal.StructureToPtr(cf, lParam, False)

        Dim res As Integer
        res = SendMessage(Handle, EM_SETCHARFORMAT, SCF_SELECTION, lParam)
        Return (res = 0)
    End Function

    ''' <summary>
    ''' Pausiert die Bildschirmaktualisierung 
    ''' (Um die Bildschirmaktualisierung fortzusetzen, 
    ''' muss ResumeRefresh aufgerufen werden)
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub StopRefresh()
        ' Das Neuzeichnen von Elementen in unterbinden
        Call SendMessage(Handle, WM_SETREDRAW, 0, 0)
    End Sub

    ''' <summary>
    ''' Aktiviert die Bildschrimaktualisierung wieder 
    ''' (wenn diese zuvor über StopRefresh pausiert wurde)
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ResumeRefresh()
        ' Das Neuzeichnen von Elementen wieder zulassen
        Call SendMessage(Me.Handle, WM_SETREDRAW, 1, 0)
        Refresh()
    End Sub

    ''' <summary>
    ''' Hebt gefundene Zeichenketten oder Wörter mit einer 
    ''' Hintergrundfarbe hervor (nicht bei langen Texten empfohlen)
    ''' </summary>
    ''' <param name="FindWhat">Zu markierender Text oder Wort</param>
    ''' <param name="BackColor">Hintergrundfarbe die gesetzt werden soll</param>
    ''' <param name="MatchCase">Gibt an ob Gross-/Kleinschreibung 
    ''' beachtet werden soll</param>
    ''' <param name="MatchWholeWord">Wenn True, wird nur nach 
    ''' ganzen Wörtern gesucht</param>
    ''' <param name="MaxFind">Begrenzt die Anzahl der zu markierenden 
    ''' Fundstellen auf die angegebene Anzahl, wenn der Wert nicht 0 ist</param>
    ''' <remarks></remarks>
    Public Sub Highlight(ByVal FindWhat As String, _
      ByVal BackColor As Color, _
      ByVal MatchCase As Boolean, _
      ByVal MatchWholeWord As Boolean, _
      Optional ByVal MaxFind As Integer = 0)

        ' Neuzeichnen unterbinden
        StopRefresh()
        SuspendLayout()

        Dim SelStart As Integer = SelectionStart
        Dim SelLength As Integer = SelectionLength

        Dim StartFrom As Integer = 0
        Dim Length As Integer = FindWhat.Length
        Dim Finds As RichTextBoxFinds
        Dim i As Integer = 0

        ' Flags für die Suche setzen
        If MatchCase Then Finds = Finds Or RichTextBoxFinds.MatchCase
        If MatchWholeWord Then Finds = Finds Or RichTextBoxFinds.WholeWord

        ' Kompletten Text durchsuchen
        While Find(FindWhat, StartFrom, Finds) > -1
            If MaxFind > 0 And i = MaxFind Then Exit While
            SelectionBackColor = BackColor
            ' Start auf Fundstelle setzen
            StartFrom = SelectionStart + SelectionLength
            i += 1
        End While

        SelectionStart = SelStart
        SelectionLength = SelLength
        ResumeLayout()

        ' Neuzeichnen aktivieren
        ResumeRefresh()
    End Sub

    ''' <summary>
    ''' Löscht die Hintergundfarbe für den markierten Bereich 
    ''' oder für das komplette Dokument
    ''' </summary>
    ''' <param name="ClearAll">Wenn der Wert True ist, wird die 
    ''' Hintergrundfarbe für das komplette Dokument gelöscht 
    ''' (nicht nur Selektion)</param>
    ''' <remarks></remarks>
    Public Sub ClearBackColor(Optional ByVal ClearAll As Boolean = True)
        ' Neuzeichnen unterbinden
        StopRefresh()
        Me.SuspendLayout()
        Dim SelStart As Integer = SelectionStart
        Dim SelLength As Integer = SelectionLength

        If ClearAll Then SelectAll()
        SelectionBackColor = BackColor

        SelectionStart = SelStart
        SelectionLength = SelLength
        ResumeLayout()

        ' Neuzeichnen aktivieren
        ResumeRefresh()
    End Sub

    ''' <summary>
    ''' Markiert den gefundenen Text (Setzt den Fokus auf die Fundstelle) 
    ''' </summary>
    ''' <param name="FindWhat">Zeichenkette oder Wort das 
    ''' gesucht werden soll</param>
    ''' <param name="MatchCase">Gibt an ob Gross-/Kleinschreibung 
    ''' berücksichtigt werden soll (False=Ignorieren)</param>
    ''' <param name="MatchWholeWord">Wenn der Wert True ist, wird nur 
    ''' nach genzen Wörtern gesucht</param>
    ''' <param name="StartFrom">Gibt die Position an ab der gesucht 
    ''' werden soll. Wenn nichts übergeben wird, wird ab der aktuellen 
    ''' Selektion gesucht</param>
    ''' <param name="bFindBackwords">Wenn der Wert True ist, wird von 
    ''' unten nach oben anstatt von Oben nach Unten gesucht</param>
    ''' <remarks>Wenn das Ende des Dokuments erreicht ist, wird wieder 
    ''' vom Anfang gesucht</remarks>
    Public Sub SelectText(ByVal FindWhat As String, _
      ByVal MatchCase As Boolean, _
      ByVal MatchWholeWord As Boolean, _
      Optional ByVal StartFrom As Integer = -1, _
      Optional ByVal bFindBackwords As Boolean = False)

        Dim Length As Integer = FindWhat.Length
        Dim Finds As RichTextBoxFinds
        Dim iFindPos As Integer

        ' Flags für die Suche setzen
        If MatchCase Then Finds = Finds Or RichTextBoxFinds.MatchCase
        If MatchWholeWord Then Finds = Finds Or RichTextBoxFinds.WholeWord

        ' Startposition festlegen
        If bFindBackwords = False Then
            StartFrom = SelectionStart + SelectionLength
        Else
            StartFrom = SelectionStart - 1
        End If

        If bFindBackwords = False Then
            iFindPos = Find(FindWhat, StartFrom, Finds)
        Else
            iFindPos = Find(FindWhat, 0, StartFrom, _
              Finds Or RichTextBoxFinds.Reverse)
        End If

        ' Wenn Ende ereicht ist, dann von Anfang an wieder suchen
        If iFindPos < 0 And StartFrom > 0 Then
            StartFrom = 0
            iFindPos = Find(FindWhat, StartFrom, Finds)
        End If

        ' Fundstelle markieren
        If iFindPos >= 0 Then
            Me.Select()

            If bFindBackwords = False Then
                Me.Select(Find(FindWhat, StartFrom, Finds), Length)
            Else
                Me.Select(Find(FindWhat, 0, StartFrom, _
                  Finds Or RichTextBoxFinds.Reverse), Length)
            End If
        End If
    End Sub
End Class