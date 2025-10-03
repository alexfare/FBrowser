Imports System.Drawing.Drawing2D

Public Class GradientPanel
    Inherits Panel

    Private _gradientColor1 As Color = Color.FromArgb(52, 52, 58)
    Private _gradientColor2 As Color = Color.FromArgb(32, 32, 36)
    Private _gradientMode As LinearGradientMode = LinearGradientMode.Horizontal

    Public Sub New()
        DoubleBuffered = True
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.ResizeRedraw Or ControlStyles.UserPaint Or ControlStyles.OptimizedDoubleBuffer, True)
    End Sub

    Public Property GradientColor1 As Color
        Get
            Return _gradientColor1
        End Get
        Set(value As Color)
            If _gradientColor1 <> value Then
                _gradientColor1 = value
                Invalidate()
            End If
        End Set
    End Property

    Public Property GradientColor2 As Color
        Get
            Return _gradientColor2
        End Get
        Set(value As Color)
            If _gradientColor2 <> value Then
                _gradientColor2 = value
                Invalidate()
            End If
        End Set
    End Property

    Public Property GradientMode As LinearGradientMode
        Get
            Return _gradientMode
        End Get
        Set(value As LinearGradientMode)
            If _gradientMode <> value Then
                _gradientMode = value
                Invalidate()
            End If
        End Set
    End Property

    Protected Overrides Sub OnPaintBackground(pevent As PaintEventArgs)
        If Width <= 0 OrElse Height <= 0 Then
            MyBase.OnPaintBackground(pevent)
            Return
        End If

        pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias

        Using brush As New LinearGradientBrush(ClientRectangle, _gradientColor1, _gradientColor2, _gradientMode)
            pevent.Graphics.FillRectangle(brush, ClientRectangle)
        End Using
    End Sub
End Class
