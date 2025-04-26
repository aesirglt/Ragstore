using System;
using System.Collections.Generic;

namespace RagnarokController
{
    public class RagnarokAddresses
    {
        private readonly MemoryScanner _scanner;
        private Dictionary<string, IntPtr> _addresses;

        public RagnarokAddresses(MemoryScanner scanner)
        {
            _scanner = scanner;
            _addresses = new Dictionary<string, IntPtr>();
        }

        public void FindAddresses()
        {
            Console.WriteLine("Procurando endereços do Ragnarok Online...");

            // Procura o endereço base do jogador
            // Este é um exemplo de como encontrar o endereço base
            // Você precisará ajustar os padrões de acordo com sua versão do jogo
            byte[] playerBasePattern = new byte[] { 0x8B, 0x0D, 0x00, 0x00, 0x00, 0x00, 0x8B, 0x91 };
            string playerBaseMask = "xx????xx";
            IntPtr playerBase = _scanner.FindPattern(playerBasePattern, playerBaseMask);
            
            if (playerBase != IntPtr.Zero)
            {
                _addresses["PlayerBase"] = playerBase;
                Console.WriteLine($"Endereço base do jogador encontrado: 0x{playerBase.ToInt64():X}");
            }

            // Procura o endereço da posição X
            byte[] xPosPattern = new byte[] { 0x89, 0x86, 0x00, 0x00, 0x00, 0x00 };
            string xPosMask = "xx????";
            IntPtr xPos = _scanner.FindPattern(xPosPattern, xPosMask);
            
            if (xPos != IntPtr.Zero)
            {
                _addresses["PlayerX"] = xPos;
                Console.WriteLine($"Endereço da posição X encontrado: 0x{xPos.ToInt64():X}");
            }

            // Procura o endereço da posição Y
            byte[] yPosPattern = new byte[] { 0x89, 0x86, 0x04, 0x00, 0x00, 0x00 };
            string yPosMask = "xx????";
            IntPtr yPos = _scanner.FindPattern(yPosPattern, yPosMask);
            
            if (yPos != IntPtr.Zero)
            {
                _addresses["PlayerY"] = yPos;
                Console.WriteLine($"Endereço da posição Y encontrado: 0x{yPos.ToInt64():X}");
            }

            // Procura o endereço do diálogo de loja
            byte[] shopDialogPattern = new byte[] { 0x6A, 0x00, 0x68, 0x00, 0x00, 0x00, 0x00, 0xE8 };
            string shopDialogMask = "xxx????x";
            IntPtr shopDialog = _scanner.FindPattern(shopDialogPattern, shopDialogMask);
            
            if (shopDialog != IntPtr.Zero)
            {
                _addresses["ShopDialog"] = shopDialog;
                Console.WriteLine($"Endereço do diálogo de loja encontrado: 0x{shopDialog.ToInt64():X}");
            }
        }

        public IntPtr GetAddress(string name)
        {
            if (_addresses.TryGetValue(name, out IntPtr address))
            {
                return address;
            }
            return IntPtr.Zero;
        }

        public void SaveAddresses(string filePath)
        {
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath))
            {
                foreach (var pair in _addresses)
                {
                    writer.WriteLine($"{pair.Key}=0x{pair.Value.ToInt64():X}");
                }
            }
        }

        public void LoadAddresses(string filePath)
        {
            _addresses.Clear();
            using (System.IO.StreamReader reader = new System.IO.StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split('=');
                    if (parts.Length == 2)
                    {
                        string name = parts[0];
                        IntPtr address = new IntPtr(Convert.ToInt64(parts[1], 16));
                        _addresses[name] = address;
                    }
                }
            }
        }
    }
} 