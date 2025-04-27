namespace StoreAgent.WinApp.Analyzers;

using StoreAgent.WinApp.Infra;
using System;
using System.Collections.Generic;
public class ExecutableAnalyzer
{
    private readonly string _oldExePath;
    private readonly string _newExePath;
    private readonly MemoryScanner _scanner;

    // Estruturas importantes baseadas no PDB antigo
    private struct FunctionInfo
    {
        public string Name;
        public IntPtr Address;
        public byte[] Pattern;
        public int Size;
    }

    private List<FunctionInfo> _oldFunctions = new List<FunctionInfo>();
    private Dictionary<string, IntPtr> _newAddresses = new Dictionary<string, IntPtr>();

    public ExecutableAnalyzer(string oldExePath, string newExePath, MemoryScanner scanner)
    {
        _oldExePath = oldExePath;
        _newExePath = newExePath;
        _scanner = scanner;
    }

    public void AnalyzeExecutables()
    {
        Console.WriteLine("Analisando executáveis...");
        Console.WriteLine($"Executável antigo: {_oldExePath}");
        Console.WriteLine($"Executável atual: {_newExePath}");

        // Lê os primeiros bytes de cada função importante do executável antigo
        using (var fs = new FileStream(_oldExePath, FileMode.Open, FileAccess.Read))
        {
            // Procura por padrões conhecidos no executável antigo
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, (int)fs.Length);

            // Procura SendPacket
            FindFunctionInOldExe(buffer, "SendPacket", new byte[]
            {
                    0x55,                   // push ebp
                    0x8B, 0xEC,            // mov ebp, esp
                    0x83, 0xEC, 0x08,      // sub esp, 8
                    0x53,                   // push ebx
                    0x56,                   // push esi
                    0x57                    // push edi
            });

            // Procura instanceR
            FindFunctionInOldExe(buffer, "instanceR", new byte[]
            {
                    0x55,                   // push ebp
                    0x8B, 0xEC,            // mov ebp, esp
                    0x83, 0xEC, 0x08,      // sub esp, 8
                    0xA1                    // mov eax, [g_ragConnection]
            });
        }

        // Agora procura padrões similares no executável atual em memória
        foreach (var oldFunc in _oldFunctions)
        {
            Console.WriteLine($"\nProcurando função {oldFunc.Name} no executável atual...");
            Console.WriteLine($"Padrão encontrado no executável antigo em: 0x{oldFunc.Address.ToInt64():X}");
            Console.WriteLine($"Tamanho do padrão: {oldFunc.Pattern.Length} bytes");

            // Cria uma máscara onde todos os bytes são importantes
            string mask = new string('x', oldFunc.Pattern.Length);

            // Procura o padrão no executável atual
            IntPtr newAddress = _scanner.FindPattern(oldFunc.Pattern, mask);

            if (newAddress != IntPtr.Zero)
            {
                _newAddresses[oldFunc.Name] = newAddress;
                Console.WriteLine($"Encontrado no executável atual em: 0x{newAddress.ToInt64():X}");

                // Lê alguns bytes ao redor do endereço encontrado para verificação
                try
                {
                    byte[] surrounding = _scanner.GetMemoryManager().ReadMemory(newAddress, oldFunc.Pattern.Length + 10);
                    Console.WriteLine("Bytes encontrados:");
                    for (int i = 0; i < surrounding.Length; i++)
                    {
                        Console.Write($"{surrounding[i]:X2} ");
                        if (( i + 1 ) % 16 == 0) Console.WriteLine();
                    }
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao ler memória: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Função {oldFunc.Name} não encontrada no executável atual.");
            }
        }
    }

    private void FindFunctionInOldExe(byte[] buffer, string funcName, byte[] pattern)
    {
        for (int i = 0; i < buffer.Length - pattern.Length; i++)
        {
            bool found = true;
            for (int j = 0; j < pattern.Length; j++)
            {
                if (buffer[i + j] != pattern[j])
                {
                    found = false;
                    break;
                }
            }

            if (found)
            {
                var info = new FunctionInfo
                {
                    Name = funcName,
                    Address = new IntPtr(i),
                    Pattern = pattern,
                    Size = pattern.Length
                };
                _oldFunctions.Add(info);
                Console.WriteLine($"Função {funcName} encontrada no executável antigo em: 0x{i:X}");

                // Mostra os bytes encontrados
                Console.WriteLine("Bytes encontrados:");
                for (int k = 0; k < pattern.Length; k++)
                {
                    Console.Write($"{buffer[i + k]:X2} ");
                }
                Console.WriteLine("\n");

                break;
            }
        }
    }

    public Dictionary<string, IntPtr> GetNewAddresses()
    {
        return _newAddresses;
    }
}