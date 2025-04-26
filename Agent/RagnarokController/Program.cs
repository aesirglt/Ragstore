using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace RagnarokController
{
    class Program
    {
        private static MemoryManager _memoryManager = null!;
        private static PlayerController _playerController = null!;
        private static ShopManager _shopManager = null!;
        private static PlayerStats? _playerStats;
        private static CharacterAnalyzer _characterAnalyzer = null!;

        static void Main(string[] args)
        {
            Console.WriteLine("Inicializando Ragnarok Controller...");
            
            _memoryManager = new MemoryManager();
            _characterAnalyzer = new CharacterAnalyzer(_memoryManager);

            try
            {
                if (!_memoryManager.AttachToProcess())
                {
                    Console.WriteLine("Erro: Não foi possível encontrar o processo Ragexe.exe");
                    return;
                }

                // Inicializa os controladores com endereços base
                var process = _memoryManager.GetProcess();
                if (process?.MainModule == null)
                {
                    Console.WriteLine("Erro: Não foi possível obter o módulo principal do processo");
                    return;
                }

                IntPtr baseAddress = process.MainModule.BaseAddress;
                _playerController = new PlayerController(_memoryManager, baseAddress);
                _shopManager = new ShopManager(_memoryManager, baseAddress);

                Console.WriteLine("Conectado ao Ragnarok Online com sucesso!");
                ShowHelp();

                while (true)
                {
                    Console.Write("\nDigite um comando: ");
                    string? command = Console.ReadLine()?.ToLower();

                    switch (command)
                    {
                        case "help":
                            ShowHelp();
                            break;
                            
                        case "pos":
                            var pos = _playerController.GetCurrentPosition();
                            Console.WriteLine($"Posição atual: X={pos.x}, Y={pos.y}");
                            break;
                            
                        case "move":
                            Console.Write("Digite a coordenada X: ");
                            if (!int.TryParse(Console.ReadLine(), out int x))
                            {
                                Console.WriteLine("Coordenada X inválida");
                                break;
                            }
                            
                            Console.Write("Digite a coordenada Y: ");
                            if (!int.TryParse(Console.ReadLine(), out int y))
                            {
                                Console.WriteLine("Coordenada Y inválida");
                                break;
                            }
                            
                            _playerController.MoveTo(x, y);
                            break;
                            
                        case "shop":
                            Console.Write("Digite o ID da loja: ");
                            if (!int.TryParse(Console.ReadLine(), out int shopId))
                            {
                                Console.WriteLine("ID da loja inválido");
                                break;
                            }
                            
                            _shopManager.OpenShop(shopId);
                            var items = _shopManager.GetShopItems();
                            Console.WriteLine("\nItens disponíveis na loja:");
                            foreach (var item in items)
                            {
                                Console.WriteLine($"- Item ID: {item}");
                            }
                            break;
                            
                        case "scan":
                            var scanner = new MemoryScanner(_memoryManager);
                            var addresses = new RagnarokAddresses(scanner);
                            addresses.FindAddresses();
                            break;
                            
                        case "analyze":
                            var offsets = _characterAnalyzer.AnalyzeCharacterStructure();
                            if (offsets != null && offsets.Count > 0)
                            {
                                _playerStats = new PlayerStats(_memoryManager, offsets);
                                Console.WriteLine("Análise de estrutura do personagem concluída!");
                            }
                            break;
                            
                        case "stats":
                            if (_playerStats == null)
                            {
                                Console.WriteLine("PlayerStats não inicializado. Use o comando 'analyze' primeiro.");
                                break;
                            }
                            _playerStats.ShowAllStats();
                            break;
                            
                        case "exit":
                            _memoryManager.Detach();
                            return;
                            
                        default:
                            Console.WriteLine("Comando inválido. Digite 'help' para ver os comandos disponíveis.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
            finally
            {
                _memoryManager.Detach();
            }
        }

        static void ShowHelp()
        {
            Console.WriteLine("\nComandos disponíveis:");
            Console.WriteLine("help - Mostra esta ajuda");
            Console.WriteLine("pos - Mostra a posição atual do personagem");
            Console.WriteLine("move - Move o personagem para as coordenadas especificadas");
            Console.WriteLine("shop - Abre uma loja e lista seus itens");
            Console.WriteLine("scan - Procura por endereços de memória importantes");
            Console.WriteLine("analyze - Analisa a estrutura do personagem para encontrar offsets");
            Console.WriteLine("stats - Mostra todas as estatísticas do personagem");
            Console.WriteLine("exit - Sai do programa");
        }
    }
}
