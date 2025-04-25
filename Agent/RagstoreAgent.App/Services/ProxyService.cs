using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;
using System.Text;

namespace RagstoreAgent.App.Services;

public class ProxyService
{
    private readonly ProxyServer _proxyServer;
    private readonly string _logPath;

    public ProxyService()
    {
        _proxyServer = new ProxyServer();
        _logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "shop_logs");
        Directory.CreateDirectory(_logPath);
    }

    public async Task StartAsync()
    {
        // Configurar o proxy
        var endpoint = new ExplicitProxyEndPoint(System.Net.IPAddress.Any, 8080, true);
        
        // Eventos para interceptar pacotes
        _proxyServer.BeforeRequest += OnRequest;
        _proxyServer.BeforeResponse += OnResponse;

        // Iniciar o proxy
        _proxyServer.AddEndPoint(endpoint);
        _proxyServer.Start();

        await Task.CompletedTask;
    }

    public async Task StopAsync()
    {
        _proxyServer.BeforeRequest -= OnRequest;
        _proxyServer.BeforeResponse -= OnResponse;
        _proxyServer.Stop();

        await Task.CompletedTask;
    }

    private async Task OnRequest(object sender, SessionEventArgs e)
    {
        var bodyBytes = await e.GetRequestBody();
        if (bodyBytes?.Length > 0)
        {
            // Verificar se é um pacote de loja
            if (IsShopPacket(bodyBytes))
            {
                var logFile = Path.Combine(_logPath, $"shop_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
                var packetInfo = FormatPacketInfo("Request", bodyBytes, e.HttpClient.Request);
                await File.WriteAllTextAsync(logFile, packetInfo);
            }
        }
    }

    private async Task OnResponse(object sender, SessionEventArgs e)
    {
        var bodyBytes = await e.GetResponseBody();
        if (bodyBytes?.Length > 0)
        {
            // Verificar se é um pacote de loja
            if (IsShopPacket(bodyBytes))
            {
                var logFile = Path.Combine(_logPath, $"shop_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
                var packetInfo = FormatPacketInfo("Response", bodyBytes, e.HttpClient.Response);
                await File.WriteAllTextAsync(logFile, packetInfo);
            }
        }
    }

    private string FormatPacketInfo(string type, byte[] data, object httpMessage)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"=== {type} Packet ===");
        sb.AppendLine($"Timestamp: {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
        sb.AppendLine($"Size: {data.Length} bytes");
        
        if (data.Length >= 2)
        {
            var opcode = BitConverter.ToUInt16(data, 0);
            sb.AppendLine($"Opcode: 0x{opcode:X4}");
        }

        sb.AppendLine("\nRaw Data (Hex):");
        sb.AppendLine(BitConverter.ToString(data));

        sb.AppendLine("\nASCII Interpretation:");
        sb.AppendLine(FormatAscii(data));

        return sb.ToString();
    }

    private string FormatAscii(byte[] data)
    {
        var sb = new StringBuilder();
        const int bytesPerLine = 16;
        
        for (int i = 0; i < data.Length; i += bytesPerLine)
        {
            // Endereço
            sb.AppendFormat("{0:X4}: ", i);
            
            // Bytes em hex
            for (int j = 0; j < bytesPerLine; j++)
            {
                if (i + j < data.Length)
                    sb.AppendFormat("{0:X2} ", data[i + j]);
                else
                    sb.Append("   ");
            }
            
            sb.Append(" | ");
            
            // Caracteres ASCII
            for (int j = 0; j < bytesPerLine && i + j < data.Length; j++)
            {
                var b = data[i + j];
                sb.Append(b >= 32 && b <= 126 ? (char)b : '.');
            }
            
            sb.AppendLine();
        }
        
        return sb.ToString();
    }

    private bool IsShopPacket(byte[] data)
    {
        // Implementar a lógica para identificar pacotes de loja
        // Isso vai depender do protocolo específico do Ragnarok
        // Por exemplo, procurar por headers ou signatures específicas
        
        // Este é um exemplo simplificado
        if (data.Length < 2) return false;
        
        // Verificar o opcode do pacote (você precisará ajustar isso)
        var opcode = BitConverter.ToUInt16(data, 0);
        return opcode == 0x00B2; // Exemplo de opcode para pacotes de loja
    }
} 