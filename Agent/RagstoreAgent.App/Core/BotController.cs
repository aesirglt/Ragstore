using Emgu.CV;
using Emgu.CV.Structure;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace RagstoreAgent.App.Core;

public class BotController
{
    private readonly CancellationTokenSource _cts;
    private readonly ShopDetector _shopDetector;
    private bool _isRunning;
    private int _movementPattern;
    private readonly Random _random;

    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out POINT lpPoint);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr GetMessageExtraInfo();

    [DllImport("user32.dll")]
    private static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

    [DllImport("kernel32.dll")]
    private static extern uint GetCurrentThreadId();

    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [StructLayout(LayoutKind.Sequential)]
    private struct POINT
    {
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public uint mouseData;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct INPUT
    {
        public uint type;
        public MOUSEKEYBDHARDWAREINPUT Data;
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct MOUSEKEYBDHARDWAREINPUT
    {
        [FieldOffset(0)]
        public MOUSEINPUT mi;
    }

    private const int INPUT_MOUSE = 0;
    private const uint MOUSEEVENTF_MOVE = 0x0001;
    private const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
    private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
    private const uint MOUSEEVENTF_LEFTUP = 0x0004;
    private const uint MOUSEEVENTF_VIRTUALDESK = 0x4000;

    public BotController()
    {
        _cts = new CancellationTokenSource();
        _shopDetector = new ShopDetector();
        _random = new Random();
        _movementPattern = 0;
    }

    private bool AttachToForegroundWindow()
    {
        var foregroundWindow = GetForegroundWindow();
        if (foregroundWindow == IntPtr.Zero) return false;

        uint foregroundProcessId;
        var foregroundThreadId = GetWindowThreadProcessId(foregroundWindow, out foregroundProcessId);
        var currentThreadId = GetCurrentThreadId();

        if (foregroundThreadId == currentThreadId) return true;

        bool attached = AttachThreadInput(currentThreadId, foregroundThreadId, true);
        if (attached)
        {
            // Desanexar após um breve delay
            Task.Delay(100).ContinueWith(_ => 
                AttachThreadInput(currentThreadId, foregroundThreadId, false));
        }

        return attached;
    }

    private void SendMouseInput(int x, int y, uint flags)
    {
        var input = new INPUT
        {
            type = INPUT_MOUSE,
            Data = new MOUSEKEYBDHARDWAREINPUT
            {
                mi = new MOUSEINPUT
                {
                    dx = x,
                    dy = y,
                    mouseData = 0,
                    dwFlags = flags | MOUSEEVENTF_VIRTUALDESK,
                    time = 0,
                    dwExtraInfo = GetMessageExtraInfo()
                }
            }
        };

        SendInput(1, new INPUT[] { input }, Marshal.SizeOf(typeof(INPUT)));
    }

    private void MoveCursorTo(int x, int y)
    {
        // Converter para coordenadas normalizadas (0-65535)
        int normalizedX = (x * 65535) / Screen.PrimaryScreen.Bounds.Width;
        int normalizedY = (y * 65535) / Screen.PrimaryScreen.Bounds.Height;

        AttachToForegroundWindow();
        SendMouseInput(normalizedX, normalizedY, MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE);
    }

    private void LeftClick()
    {
        AttachToForegroundWindow();
        SendMouseInput(0, 0, MOUSEEVENTF_LEFTDOWN);
        Thread.Sleep(50 + _random.Next(30));
        SendMouseInput(0, 0, MOUSEEVENTF_LEFTUP);
    }

    public async Task StartAsync()
    {
        if (_isRunning) return;
        _isRunning = true;

        try
        {
            while (!_cts.Token.IsCancellationRequested)
            {
                // Tentar detectar e clicar nas lojas
                var foundShops = await ScanAndClickShops();
                
                // Se não encontrou lojas, mover o personagem
                if (!foundShops)
                {
                    await MoveCharacter();
                }
                
                // Pausa maior entre ações para ser mais natural
                await Task.Delay(1000 + _random.Next(500), _cts.Token);
            }
        }
        catch (OperationCanceledException)
        {
            // Ignorar exceção de cancelamento
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro no bot: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
    }

    public async Task StopAsync()
    {
        if (!_isRunning) return;
        
        _cts.Cancel();
        _isRunning = false;
        await Task.CompletedTask;
    }

    private async Task<bool> ScanAndClickShops()
    {
        var shops = await _shopDetector.DetectShops();
        
        if (shops.Count == 0) return false;

        foreach (var shop in shops)
        {
            if (_cts.Token.IsCancellationRequested) break;

            try
            {
                // Verificar se a loja está dentro da janela
                if (!_shopDetector._windowCapture.IsPointInsideWindow(shop))
                {
                    Console.WriteLine($"Loja detectada fora da janela: {shop}");
                    continue;
                }

                // Mover mouse para a loja com movimento mais natural
                POINT currentPos;
                GetCursorPos(out currentPos);
                var targetX = shop.X;
                var targetY = shop.Y;

                // Mais passos e curva natural
                var steps = 30 + _random.Next(20);
                for (int i = 1; i <= steps; i++)
                {
                    // Movimento em curva natural usando função seno
                    var progress = (double)i / steps;
                    var curve = Math.Sin(progress * Math.PI / 2);
                    
                    var x = (int)(currentPos.X + ((targetX - currentPos.X) * curve));
                    var y = (int)(currentPos.Y + ((targetY - currentPos.Y) * curve));
                    
                    // Adicionar pequena variação aleatória
                    x += _random.Next(-2, 3);
                    y += _random.Next(-2, 3);
                    
                    MoveCursorTo(x, y);
                    
                    // Delay variável para movimento mais natural
                    await Task.Delay(10 + _random.Next(15));
                }

                // Garantir que chegou no destino
                MoveCursorTo(targetX, targetY);
                await Task.Delay(200 + _random.Next(100));

                // Duplo clique com timing mais natural
                LeftClick();
                await Task.Delay(120 + _random.Next(50));
                LeftClick();

                // Aguardar a janela abrir
                await Task.Delay(1000 + _random.Next(500));

                // Tentar encontrar e clicar no botão cancel
                var cancelButton = await _shopDetector.DetectCancelButton();
                if (cancelButton.HasValue && _shopDetector._windowCapture.IsPointInsideWindow(cancelButton.Value))
                {
                    // Movimento mais natural do mouse até o botão cancel
                    GetCursorPos(out currentPos);
                    targetX = cancelButton.Value.X;
                    targetY = cancelButton.Value.Y;

                    steps = 20 + _random.Next(10);
                    for (int i = 1; i <= steps; i++)
                    {
                        var progress = (double)i / steps;
                        var curve = Math.Sin(progress * Math.PI / 2);
                        
                        var x = (int)(currentPos.X + ((targetX - currentPos.X) * curve));
                        var y = (int)(currentPos.Y + ((targetY - currentPos.Y) * curve));
                        
                        x += _random.Next(-1, 2);
                        y += _random.Next(-1, 2);
                        
                        MoveCursorTo(x, y);
                        await Task.Delay(8 + _random.Next(12));
                    }

                    MoveCursorTo(targetX, targetY);
                    await Task.Delay(100 + _random.Next(50));
                    
                    LeftClick();
                    await Task.Delay(500 + _random.Next(300));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao interagir com loja: {ex.Message}");
                continue;
            }
        }

        return true;
    }

    private async Task MoveCharacter()
    {
        try
        {
            // Padrões de movimento em espiral com duração variável
            var duration = 800 + _random.Next(400);
            
            switch (_movementPattern)
            {
                case 0: // Direita
                    SendKeys.SendWait("{RIGHT down}");
                    await Task.Delay(duration);
                    SendKeys.SendWait("{RIGHT up}");
                    break;
                case 1: // Baixo
                    SendKeys.SendWait("{DOWN down}");
                    await Task.Delay(duration);
                    SendKeys.SendWait("{DOWN up}");
                    break;
                case 2: // Esquerda
                    SendKeys.SendWait("{LEFT down}");
                    await Task.Delay(duration);
                    SendKeys.SendWait("{LEFT up}");
                    break;
                case 3: // Cima
                    SendKeys.SendWait("{UP down}");
                    await Task.Delay(duration);
                    SendKeys.SendWait("{UP up}");
                    break;
            }

            // Alternar padrão de movimento
            _movementPattern = (_movementPattern + 1) % 4;
            
            // Pausa maior entre movimentos
            await Task.Delay(500 + _random.Next(500));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao mover personagem: {ex.Message}");
        }
    }
} 