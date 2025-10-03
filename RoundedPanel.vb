Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Public Class RoundedPanel
    Inherits Panel

    Private _cornerRadius As Integer = 14
    Private _borderColor As Color = Color.FromArgb(68, 68, 74)
    Private _borderThickness As Integer = 1

    Public Sub New()
        DoubleBuffered = True
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.ResizeRedraw Or ControlStyles.UserPaint Or ControlStyles.OptimizedDoubleBuffer, True)
        BackColor = Color.FromArgb(32, 32, 36)
    End Sub

    <Browsable(True)>
    Public Property CornerRadius As Integer
        Get
            Return _cornerRadius
        End Get
        Set(value As Integer)
            If value <> _cornerRadius Then
                _cornerRadius = Math.Max(0, value)
                Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    Public Property BorderColor As Color
        Get
            Return _borderColor
        End Get
        Set(value As Color)
            If value <> _borderColor Then
                _borderColor = value
                Invalidate()
            End If
        End Set
    End Property

    <Browsable(True)>
    Public Property BorderThickness As Integer
        Get
            Return _borderThickness
        End Get
        Set(value As Integer)
            Dim thickness = Math.Max(0, value)
            If thickness <> _borderThickness Then
                _borderThickness = thickness
                Invalidate()
            End If
        End Set
    End Property

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        Dim rect = New Rectangle(0, 0, Width - 1, Height - 1)

        Using path = CreateRoundedRectangle(rect, _cornerRadius)
            Using brush As New SolidBrush(BackColor)
                e.Graphics.FillPath(brush, path)
            End Using

            If _borderThickness > 0 Then
                Using pen As New Pen(_borderColor, _borderThickness)
                    e.Graphics.DrawPath(pen, path)
                End Using
            End If
        End Using

        MyBase.OnPaint(e)
    End Sub

    Protected Overrides Sub OnPaintBackground(pevent As PaintEventArgs)
        ' Avoid flicker by preventing default background paint
    End Sub

    Private Shared Function CreateRoundedRectangle(rect As Rectangle, radius As Integer) As GraphicsPath
        Dim path As New GraphicsPath()

        If radius <= 0 Then
            path.AddRectangle(rect)
            Return path
        End If

        Dim diameter = radius * 2

        path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90)
        path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90)
        path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90)
        path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90)
        path.CloseFigure()

        Return path
    End Function
End Class
