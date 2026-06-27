using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
public class AksoAnimatedHeaderPanel : Panel
{
    private readonly Timer _timer;
    private float _phase;

    public AksoAnimatedHeaderPanel()
    {
        DoubleBuffered = true;
        ResizeRedraw = true;

        _timer = new Timer { Interval = 35 };
        _timer.Tick += (s, e) =>
        {
            _phase += 0.018f;
            if (_phase > Math.PI * 2) _phase = 0;
            Invalidate();
        };
        _timer.Start();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        int shift = (int)(Math.Sin(_phase) * 45);
        Color left = Color.FromArgb(15, 23, 42);
        Color middle = Color.FromArgb(Math.Max(0, 37 + shift / 3), Math.Max(0, 99 + shift / 4), 235);
        Color right = Color.FromArgb(14, 165, Math.Max(160, 233 + shift / 5));

        using (LinearGradientBrush brush = new LinearGradientBrush(ClientRectangle, left, right, LinearGradientMode.ForwardDiagonal))
        using (GraphicsPath path = new GraphicsPath())
        {
            ColorBlend blend = new ColorBlend
            {
                Positions = new[] { 0f, 0.55f, 1f },
                Colors = new[] { left, middle, right }
            };
            brush.InterpolationColors = blend;
            e.Graphics.FillRectangle(brush, ClientRectangle);
        }

        using (Pen pen = new Pen(Color.FromArgb(120, 147, 197, 253), 1))
            e.Graphics.DrawLine(pen, 0, Height - 1, Width, Height - 1);

        base.OnPaint(e);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
            _timer?.Dispose();

        base.Dispose(disposing);
    }
}
