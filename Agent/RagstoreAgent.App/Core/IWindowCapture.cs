using System.Drawing;

namespace RagstoreAgent.App.Core;

public interface IWindowCapture
{
    Bitmap? CaptureWindow();
    Point GetClientToScreen(Point clientPoint);
    bool IsPointInsideWindow(Point point);
} 