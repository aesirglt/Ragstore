using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace RagstoreAgent.App.Core;

public class WindowCapture : IWindowCapture
{
    private const int WINDOW_WIDTH = 1280;
    private const int WINDOW_HEIGHT = 720;
    private const int WINDOW_X = 0;
    private const int WINDOW_Y = 0;

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out POINT lpPoint);

    [StructLayout(LayoutKind.Sequential)]
    private struct POINT
    {
        public int X;
        public int Y;
    }

    public Bitmap? CaptureWindow()
    {
        try
        {
            var bitmap = new Bitmap(WINDOW_WIDTH, WINDOW_HEIGHT, PixelFormat.Format32bppArgb);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(WINDOW_X, WINDOW_Y, 0, 0, new Size(WINDOW_WIDTH, WINDOW_HEIGHT));
            }
            return bitmap;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao capturar janela: {ex.Message}");
            return null;
        }
    }

    public Point GetClientToScreen(Point clientPoint)
    {
        // Como a janela está fixa em (0,0), as coordenadas do cliente são as mesmas da tela
        return new Point(
            WINDOW_X + clientPoint.X,
            WINDOW_Y + clientPoint.Y
        );
    }

    public bool IsPointInsideWindow(Point point)
    {
        return point.X >= WINDOW_X && point.X < WINDOW_X + WINDOW_WIDTH &&
               point.Y >= WINDOW_Y && point.Y < WINDOW_Y + WINDOW_HEIGHT;
    }
} 