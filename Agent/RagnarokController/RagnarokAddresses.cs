using System;
using System.Collections.Generic;
using System.IO;

namespace RagnarokController
{
    public class RagnarokAddresses
    {
        private readonly MemoryScanner _scanner;
        private readonly Dictionary<string, IntPtr> _addresses;

        public RagnarokAddresses(MemoryScanner scanner)
        {
            _scanner = scanner ?? throw new ArgumentNullException(nameof(scanner));
            _addresses = new Dictionary<string, IntPtr>();
        }

        public void FindAddresses()
        {
            Console.WriteLine("Iniciando busca por endereços...");
            
            // Procura pelo endereço base do personagem
            var playerBase = _scanner.FindPattern(new byte[] { 0x55, 0x8B, 0xEC, 0x83, 0xEC, 0x10 }, "xxxxxx");
            if (playerBase != IntPtr.Zero)
            {
                _addresses["PlayerBase"] = playerBase;
                Console.WriteLine($"Endereço base do personagem encontrado: 0x{playerBase.ToString("X")}");
            }
            else
            {
                Console.WriteLine("Endereço base do personagem não encontrado.");
            }

            // Procura pelo endereço base da loja
            var shopBase = _scanner.FindPattern(new byte[] { 0x55, 0x8B, 0xEC, 0x83, 0xEC, 0x14 }, "xxxxxx");
            if (shopBase != IntPtr.Zero)
            {
                _addresses["ShopBase"] = shopBase;
                Console.WriteLine($"Endereço base da loja encontrado: 0x{shopBase.ToString("X")}");
            }
            else
            {
                Console.WriteLine("Endereço base da loja não encontrado.");
            }

            // Procura pelo endereço do diálogo da loja
            var shopDialog = _scanner.FindPattern(new byte[] { 0x8B, 0x45, 0x08, 0x8B, 0x80, 0x00, 0x00, 0x00, 0x00, 0x89, 0x45, 0xFC }, "xxxx????xxxx");
            if (shopDialog != IntPtr.Zero)
            {
                _addresses["ShopDialog"] = shopDialog + 5;
                Console.WriteLine($"Endereço do diálogo da loja encontrado: 0x{(shopDialog + 5).ToString("X")}");
            }
            else
            {
                Console.WriteLine("Endereço do diálogo da loja não encontrado.");
            }

            Console.WriteLine($"\nBusca concluída. {_addresses.Count} endereços encontrados.");
        }

        public IntPtr GetAddress(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Nome do endereço não pode ser nulo ou vazio.", nameof(name));
            }

            return _addresses.TryGetValue(name, out IntPtr address) ? address : IntPtr.Zero;
        }

        public void SaveAddresses(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("Caminho do arquivo não pode ser nulo ou vazio.", nameof(filePath));
            }

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var pair in _addresses)
                {
                    writer.WriteLine($"{pair.Key}={pair.Value.ToInt64():X}");
                }
            }
        }

        public void LoadAddresses(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("Caminho do arquivo não pode ser nulo ou vazio.", nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Arquivo não encontrado.", filePath);
            }

            _addresses.Clear();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] parts = line.Split('=');
                    if (parts.Length == 2 && 
                        !string.IsNullOrWhiteSpace(parts[0]) && 
                        long.TryParse(parts[1], System.Globalization.NumberStyles.HexNumber, null, out long address))
                    {
                        _addresses[parts[0]] = new IntPtr(address);
                    }
                    else
                    {
                        Console.WriteLine($"Aviso: Linha inválida ignorada: {line}");
                    }
                }
            }

            Console.WriteLine($"Carregados {_addresses.Count} endereços do arquivo.");
        }
    }
} 