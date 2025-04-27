namespace StoreAgent.WinApp.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class MemoryScanner
{
    private readonly MemoryManager _memoryManager;
    private const int SCAN_CHUNK_SIZE = 0x1000; // Tamanho do chunk para escanear

    public MemoryScanner(MemoryManager memoryManager)
    {
        _memoryManager = memoryManager ?? throw new ArgumentNullException(nameof(memoryManager));
    }

    public MemoryManager GetMemoryManager()
    {
        return _memoryManager;
    }

    public IntPtr FindPattern(byte[] pattern, string mask)
    {
        try
        {
            Console.WriteLine($"Iniciando busca por padrão de {pattern.Length} bytes...");

            var process = _memoryManager.GetProcess();
            if (process?.MainModule == null)
            {
                throw new InvalidOperationException("Processo ou módulo principal não encontrado.");
            }

            IntPtr baseAddress = process.MainModule.BaseAddress;
            int size = process.MainModule.ModuleMemorySize;

            Console.WriteLine($"Endereço base: 0x{baseAddress.ToString("X")}");
            Console.WriteLine($"Tamanho do módulo: 0x{size.ToString("X")} bytes");

            int chunksScanned = 0;
            int totalChunks = size / SCAN_CHUNK_SIZE;

            for (IntPtr address = baseAddress; address.ToInt64() < baseAddress.ToInt64() + size; address += SCAN_CHUNK_SIZE)
            {
                chunksScanned++;
                if (chunksScanned % 100 == 0)
                {
                    Console.WriteLine($"Progresso: {( chunksScanned * 100.0 / totalChunks ):F2}% ({chunksScanned}/{totalChunks} chunks)");
                }

                byte[] buffer = new byte[SCAN_CHUNK_SIZE];
                int bytesRead;

                if (_memoryManager.ReadProcessMemory(address, buffer, SCAN_CHUNK_SIZE, out bytesRead))
                {
                    for (int i = 0; i < bytesRead - pattern.Length; i++)
                    {
                        bool found = true;
                        for (int j = 0; j < pattern.Length; j++)
                        {
                            if (mask[j] == 'x' && buffer[i + j] != pattern[j])
                            {
                                found = false;
                                break;
                            }
                        }

                        if (found)
                        {
                            IntPtr foundAddress = address + i;
                            Console.WriteLine($"Padrão encontrado em: 0x{foundAddress.ToString("X")}");

                            // Verifica se o endereço é válido tentando ler um valor
                            try
                            {
                                byte[] testBuffer = new byte[4];
                                int testBytesRead;
                                if (_memoryManager.ReadProcessMemory(foundAddress, testBuffer, 4, out testBytesRead))
                                {
                                    Console.WriteLine("Endereço validado com sucesso!");
                                    return foundAddress;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Erro ao validar endereço: {ex.Message}");
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Padrão não encontrado.");
            return IntPtr.Zero;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro durante a busca por padrão: {ex.Message}");
            return IntPtr.Zero;
        }
    }

    public IntPtr FindValue(int value)
    {
        Console.WriteLine($"Procurando valor: {value} (0x{value:X})");
        byte[] bytes = BitConverter.GetBytes(value);
        return FindPattern(bytes, "xxxx");
    }

    public IntPtr FindString(string str)
    {
        Console.WriteLine($"Procurando string: {str}");
        byte[] bytes = System.Text.Encoding.ASCII.GetBytes(str);
        return FindPattern(bytes, new string('x', bytes.Length));
    }
}
