using System.Drawing;
using System.Drawing.Imaging;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using System.IO;

namespace RagstoreAgent.App.Core;

public sealed class ShopDetector : IDisposable
{
    private readonly Rectangle _gameWindowArea;
    private readonly Mat? _tempMat;
    private bool _disposed;
    private readonly string _tempImagePath;

    public ShopDetector()
    {
        // Área aproximada da janela do jogo
        _gameWindowArea = new Rectangle(0, 0, 1280, 720);
        _tempMat = new Mat();
        _tempImagePath = Path.Combine(Path.GetTempPath(), "temp_capture.bmp");
    }

    private void SaveBitmapToTempFile(Bitmap bitmap)
    {
        bitmap.Save(_tempImagePath, ImageFormat.Bmp);
    }

    public async Task<List<Point>> DetectShops()
    {
        ArgumentNullException.ThrowIfNull(_tempMat);
        var shops = new List<Point>();

        try
        {
            using var bitmap = new Bitmap(_gameWindowArea.Width, _gameWindowArea.Height);
            using var graphics = Graphics.FromImage(bitmap);
            
            // Capturar a tela
            graphics.CopyFromScreen(_gameWindowArea.Location, Point.Empty, _gameWindowArea.Size);

            // Salvar bitmap em arquivo temporário
            SaveBitmapToTempFile(bitmap);

            // Carregar imagem diretamente com EmguCV
            using var mat = CvInvoke.Imread(_tempImagePath, ImreadModes.Color);
            using var bgrMat = new Mat();
            CvInvoke.CvtColor(mat, bgrMat, ColorConversion.Bgr2Gray);
            
            // Converter para HSV para melhor detecção de cores
            using var hsvMat = new Mat();
            CvInvoke.CvtColor(bgrMat, hsvMat, ColorConversion.Bgr2Hsv);
            
            // Detectar áreas brancas (fundo dos balões)
            using var whiteMask = new Mat();
            var lowerWhite = new ScalarArray(new MCvScalar(0, 0, 200));
            var upperWhite = new ScalarArray(new MCvScalar(180, 30, 255));
            CvInvoke.InRange(hsvMat, lowerWhite, upperWhite, whiteMask);

            // Aplicar operações morfológicas para limpar ruído
            using var kernel = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));
            CvInvoke.MorphologyEx(whiteMask, whiteMask, MorphOp.Open, kernel, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
            
            using var contours = new VectorOfVectorOfPoint();
            using var hierarchy = new Mat();
            CvInvoke.FindContours(whiteMask, contours, hierarchy, RetrType.List, ChainApproxMethod.ChainApproxSimple);

            // Processar cada contorno encontrado
            for (int i = 0; i < contours.Size; i++)
            {
                var contour = contours[i].ToArray();
                var area = CvInvoke.ContourArea(contours[i]);
                var boundingRect = CvInvoke.MinAreaRect(contours[i]);
                var rect = boundingRect.MinAreaRect();

                // Filtrar por tamanho e proporção do retângulo
                if (area > 500 && area < 5000 && 
                    rect.Width > rect.Height && 
                    rect.Width < 300)  // Evitar áreas muito grandes
                {
                    // Extrair região de interesse (ROI)
                    var roi = new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
                    using var roiMat = new Mat(whiteMask, roi);
                    using var grayRoi = new Mat();
                    CvInvoke.CvtColor(roiMat, grayRoi, ColorConversion.Bgr2Gray);
                    
                    var mean = CvInvoke.Mean(grayRoi);

                    // Se houver contraste suficiente (texto escuro em fundo claro)
                    if (mean.V0 > 127)
                    {
                        var centerX = (int)(rect.X + (rect.Width / 2));
                        var centerY = (int)(rect.Y + (rect.Height / 2));
                        shops.Add(new Point(centerX + _gameWindowArea.X, centerY + _gameWindowArea.Y));
                    }
                }
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
        ArgumentNullException.ThrowIfNull(_tempMat);

        try
        {
            using var bitmap = new Bitmap(_gameWindowArea.Width, _gameWindowArea.Height);
            using var graphics = Graphics.FromImage(bitmap);
            
            graphics.CopyFromScreen(_gameWindowArea.Location, Point.Empty, _gameWindowArea.Size);

            // Salvar bitmap em arquivo temporário
            SaveBitmapToTempFile(bitmap);

            // Carregar imagem diretamente com EmguCV
            using var mat = CvInvoke.Imread(_tempImagePath, ImreadModes.Color);
            using var grayMat = new Mat();
            CvInvoke.CvtColor(mat, grayMat, ColorConversion.Bgr2Gray);
            
            // Aplicar threshold para destacar texto
            using var thresholdMat = new Mat();
            CvInvoke.Threshold(grayMat, thresholdMat, 127, 255, ThresholdType.Binary);
            
            using var contours = new VectorOfVectorOfPoint();
            using var hierarchy = new Mat();
            CvInvoke.FindContours(thresholdMat, contours, hierarchy, RetrType.List, ChainApproxMethod.ChainApproxSimple);

            // Encontrar o botão cancel
            for (int i = 0; i < contours.Size; i++)
            {
                var contour = contours[i].ToArray();
                var boundingRect = CvInvoke.MinAreaRect(contours[i]);
                var rect = boundingRect.MinAreaRect();
                var area = rect.Width * rect.Height;

                // Filtrar por tamanho típico do botão cancel
                if (area > 1000 && area < 5000 && 
                    rect.Width > 40 && rect.Width < 100 &&
                    rect.Height > 15 && rect.Height < 40)
                {
                    var centerX = (int)(rect.X + (rect.Width / 2));
                    var centerY = (int)(rect.Y + (rect.Height / 2));
                    return new Point(centerX + _gameWindowArea.X, centerY + _gameWindowArea.Y);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao detectar botão cancel: {ex.Message}");
        }

        return null;
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _tempMat?.Dispose();
            if (File.Exists(_tempImagePath))
            {
                try
                {
                    File.Delete(_tempImagePath);
                }
                catch
                {
                    // Ignora erros ao tentar deletar o arquivo temporário
                }
            }
            _disposed = true;
        }
    }
} 