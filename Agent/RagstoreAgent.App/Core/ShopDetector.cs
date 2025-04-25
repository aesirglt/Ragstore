using System.Drawing;
using System.Drawing.Imaging;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System.Runtime.InteropServices;
using Point = System.Drawing.Point;

namespace RagstoreAgent.App.Core;

public class ShopDetector
{
    private readonly bool _debugMode;
    private readonly string _debugPath;
    public readonly IWindowCapture _windowCapture;

    public ShopDetector(bool debugMode = false)
    {
        _windowCapture = new WindowCapture();
        _debugMode = debugMode;
        _debugPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "debug");
        
        if (_debugMode && !Directory.Exists(_debugPath))
        {
            Directory.CreateDirectory(_debugPath);
        }
    }

    private Mat BitmapToMat(Bitmap bitmap)
    {
        if (bitmap.PixelFormat != PixelFormat.Format24bppRgb)
        {
            var temp = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format24bppRgb);
            using (var g = Graphics.FromImage(temp))
            {
                g.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
            }
            bitmap.Dispose();
            bitmap = temp;
        }

        var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
        var bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
        
        var mat = new Mat(bitmap.Height, bitmap.Width, DepthType.Cv8U, 3, bitmapData.Scan0, bitmapData.Stride);
        var result = mat.Clone();
        
        bitmap.UnlockBits(bitmapData);
        return result;
    }

    public async Task<List<Point>> DetectShops()
    {
        var shops = new List<Point>();
        
        try
        {
            using var bitmap = _windowCapture.CaptureWindow();
            if (bitmap == null) return shops;

            using var mat = BitmapToMat(bitmap);
            using var gray = new Mat();
            using var blur = new Mat();
            using var edges = new Mat();
            using var dilated = new Mat();
            
            // Converter para escala de cinza
            CvInvoke.CvtColor(mat, gray, ColorConversion.Bgr2Gray);
            
            // Aplicar blur gaussiano para reduzir ruído
            CvInvoke.GaussianBlur(gray, blur, new Size(5, 5), 1.5);
            
            // Detectar bordas com Canny
            CvInvoke.Canny(blur, edges, 50, 150);
            
            // Dilatar bordas para conectar regiões próximas
            var kernel = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));
            CvInvoke.Dilate(edges, dilated, kernel, new Point(-1, -1), 2, BorderType.Default, new MCvScalar(1));

            using var contours = new VectorOfVectorOfPoint();
            CvInvoke.FindContours(dilated, contours, null, RetrType.External, ChainApproxMethod.ChainApproxSimple);

            // Filtrar e processar contornos
            for (int i = 0; i < contours.Size; i++)
            {
                using var contour = contours[i];
                var area = CvInvoke.ContourArea(contour);
                
                // Filtrar por área - balões de fala típicos têm área entre 1000 e 5000 pixels
                if (area < 1000 || area > 5000) continue;

                var boundingRect = CvInvoke.MinAreaRect(contour);
                var rect = boundingRect.MinAreaRect();
                
                // Filtrar por proporções do retângulo
                var aspectRatio = (double)rect.Width / rect.Height;
                if (aspectRatio < 1.5 || aspectRatio > 4) continue;
                
                // Verificar se é um balão de fala analisando a distribuição de pixels
                var roiRect = Rectangle.Round(rect);
                if (roiRect.Width <= 0 || roiRect.Height <= 0) continue;
                
                using var roiMat = new Mat(edges, roiRect);
                using var roiMask = new Mat();
                CvInvoke.Threshold(roiMat, roiMask, 128, 255, ThresholdType.Binary);
                
                var nonZero = CvInvoke.CountNonZero(roiMask);
                var density = nonZero / (double)(rect.Width * rect.Height);
                
                // Balões de fala típicos têm densidade de pixels de borda entre 0.1 e 0.3
                if (density < 0.1 || density > 0.3) continue;

                // Calcular o ponto central do balão
                var centerX = (int)(rect.X + rect.Width / 2);
                var centerY = (int)(rect.Y + rect.Height / 2);
                var clientPoint = new Point(centerX, centerY);
                var screenPoint = _windowCapture.GetClientToScreen(clientPoint);
                shops.Add(screenPoint);

                if (_debugMode)
                {
                    // Desenhar retângulo e centro para debug
                    var box = boundingRect.GetVertices();
                    for (int j = 0; j < 4; j++)
                    {
                        var pt1 = Point.Round(box[j]);
                        var pt2 = Point.Round(box[(j + 1) % 4]);
                        CvInvoke.Line(mat, pt1, pt2, new MCvScalar(0, 255, 0), 2);
                    }
                    CvInvoke.Circle(mat, new Point(centerX, centerY), 3, new MCvScalar(0, 0, 255), -1);
                }
            }

            if (_debugMode)
            {
                // Salvar imagens de debug
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                CvInvoke.Imwrite(Path.Combine(_debugPath, $"debug_shops_{timestamp}.png"), mat);
                CvInvoke.Imwrite(Path.Combine(_debugPath, $"debug_edges_{timestamp}.png"), edges);
                CvInvoke.Imwrite(Path.Combine(_debugPath, $"debug_dilated_{timestamp}.png"), dilated);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao detectar lojas: {ex.Message}");
        }

        return shops;
    }

    public async Task<Point?> DetectCancelButton()
    {
        try
        {
            using var bitmap = _windowCapture.CaptureWindow();
            if (bitmap == null) return null;

            using var mat = BitmapToMat(bitmap);
            using var hsv = new Mat();
            using var mask = new Mat();
            
            // Converter para HSV
            CvInvoke.CvtColor(mat, hsv, ColorConversion.Bgr2Hsv);
            
            // Filtrar cor vermelha (botão cancel)
            using var lowerRed = new ScalarArray(new MCvScalar(0, 100, 100));
            using var upperRed = new ScalarArray(new MCvScalar(10, 255, 255));
            CvInvoke.InRange(hsv, lowerRed, upperRed, mask);
            
            using var contours = new VectorOfVectorOfPoint();
            CvInvoke.FindContours(mask, contours, null, RetrType.External, ChainApproxMethod.ChainApproxSimple);

            double maxArea = 0;
            Point? buttonCenter = null;

            for (int i = 0; i < contours.Size; i++)
            {
                using var contour = contours[i];
                var area = CvInvoke.ContourArea(contour);
                
                // Filtrar por área - botão cancel típico tem área entre 500 e 2000 pixels
                if (area < 500 || area > 2000) continue;
                
                if (area > maxArea)
                {
                    maxArea = area;
                    var boundingRect = CvInvoke.MinAreaRect(contour);
                    var rect = boundingRect.MinAreaRect();
                    var clientPoint = new Point(
                        (int)(rect.X + rect.Width / 2),
                        (int)(rect.Y + rect.Height / 2)
                    );
                    buttonCenter = _windowCapture.GetClientToScreen(clientPoint);
                }
            }

            if (_debugMode && buttonCenter.HasValue)
            {
                CvInvoke.Circle(mat, buttonCenter.Value, 5, new MCvScalar(0, 255, 0), -1);
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                CvInvoke.Imwrite(Path.Combine(_debugPath, $"debug_cancel_{timestamp}.png"), mat);
                CvInvoke.Imwrite(Path.Combine(_debugPath, $"debug_cancel_mask_{timestamp}.png"), mask);
            }

            return buttonCenter;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao detectar botão cancel: {ex.Message}");
            return null;
        }
    }
} 
