using Emgu.CV;
using Emgu.CV.Structure;
using WindowsInput;
using WindowsInput.Native;

namespace RagstoreAgent.App.Core;

public class BotController
{
    private readonly InputSimulator _input;
    private readonly CancellationTokenSource _cts;
    private readonly ShopDetector _shopDetector;
    private bool _isRunning;

    public BotController()
    {
        _input = new InputSimulator();
        _cts = new CancellationTokenSource();
        _shopDetector = new ShopDetector();
    }

    public async Task StartAsync()
    {
        if (_isRunning) return;
        _isRunning = true;

        try
        {
            while (!_cts.Token.IsCancellationRequested)
            {
                await ScanAndClickShops();
                await MoveCharacter();
                await Task.Delay(1000, _cts.Token);
            }
        }
        catch (OperationCanceledException)
        {
            // Ignorar exceção de cancelamento
        }
    }

    public async Task StopAsync()
    {
        if (!_isRunning) return;
        
        _cts.Cancel();
        _isRunning = false;
        await Task.CompletedTask;
    }

    private async Task ScanAndClickShops()
    {
        var shops = await _shopDetector.DetectShops();
        
        foreach (var shop in shops)
        {
            if (_cts.Token.IsCancellationRequested) break;

            // Mover mouse para a loja
            _input.Mouse.MoveMouseTo(shop.X, shop.Y);
            await Task.Delay(100);

            // Duplo clique
            _input.Mouse.LeftButtonClick();
            await Task.Delay(50);
            _input.Mouse.LeftButtonClick();

            // Aguardar a janela abrir
            await Task.Delay(500);

            // Clicar no botão cancel
            var cancelButton = await _shopDetector.DetectCancelButton();
            if (cancelButton.HasValue)
            {
                _input.Mouse.MoveMouseTo(cancelButton.Value.X, cancelButton.Value.Y);
                await Task.Delay(100);
                _input.Mouse.LeftButtonClick();
            }

            await Task.Delay(200);
        }
    }

    private async Task MoveCharacter()
    {
        // Implementar lógica de movimento do personagem
        // Por exemplo, mover em um padrão em espiral ou zigzag
        await Task.Delay(100);
        _input.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
        await Task.Delay(1000);
        _input.Keyboard.KeyPress(VirtualKeyCode.DOWN);
        await Task.Delay(1000);
    }
} 