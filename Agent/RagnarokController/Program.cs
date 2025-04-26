using System;

namespace RagnarokController
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando Controlador do Ragnarok Online...");
            
            var memoryManager = new MemoryManager();

            if (!memoryManager.AttachToProcess())
            {
                Console.WriteLine("Erro: Não foi possível encontrar o processo do Ragnarok Online.");
                Console.WriteLine("Certifique-se de que o jogo está rodando.");
                return;
            } 

            var scanner = new MemoryScanner(memoryManager);
            var addresses = new RagnarokAddresses(scanner);
            var playerController = new PlayerController(memoryManager);
            var shopManager = new ShopManager(memoryManager);
            PlayerStats playerStats = null;

            Console.WriteLine("Conectado ao Ragnarok Online com sucesso!");
            Console.WriteLine("Digite 'help' para ver os comandos disponíveis.");

            while (true)
            {
                Console.Write("> ");
                string command = Console.ReadLine().ToLower();

                switch (command)
                {
                    case "help":
                        ShowHelp();
                        break;

                    case "scan":
                        addresses.FindAddresses();
                        // Tenta inicializar o PlayerStats se ainda não foi inicializado
                        if (playerStats == null)
                        {
                            IntPtr playerBaseAddress = addresses.GetAddress("PlayerBase");
                            if (playerBaseAddress != IntPtr.Zero)
                            {
                                playerStats = new PlayerStats(memoryManager, playerBaseAddress);
                                Console.WriteLine("PlayerStats inicializado com sucesso!");
                            }
                        }
                        break;

                    case "save":
                        Console.Write("Digite o caminho do arquivo para salvar: ");
                        string savePath = Console.ReadLine();
                        addresses.SaveAddresses(savePath);
                        Console.WriteLine("Endereços salvos com sucesso!");
                        break;

                    case "load":
                        Console.Write("Digite o caminho do arquivo para carregar: ");
                        string loadPath = Console.ReadLine();
                        addresses.LoadAddresses(loadPath);
                        Console.WriteLine("Endereços carregados com sucesso!");
                        break;

                    case "pos":
                        var (x, y) = playerController.GetCurrentPosition();
                        Console.WriteLine($"Posição atual: X={x}, Y={y}");
                        break;

                    case "move":
                        Console.Write("Digite a coordenada X: ");
                        if (int.TryParse(Console.ReadLine(), out int newX))
                        {
                            Console.Write("Digite a coordenada Y: ");
                            if (int.TryParse(Console.ReadLine(), out int newY))
                            {
                                playerController.MoveTo(newX, newY);
                                Console.WriteLine($"Movendo para X={newX}, Y={newY}");
                            }
                        }
                        break;

                    case "shop":
                        Console.Write("Digite o ID da loja: ");
                        if (int.TryParse(Console.ReadLine(), out int shopId))
                        {
                            shopManager.OpenShop(shopId);
                            var items = shopManager.GetShopItems(shopId);
                            Console.WriteLine($"Itens disponíveis na loja {shopId}:");
                            foreach (var item in items)
                            {
                                Console.WriteLine($"- Item ID: {item}");
                            }
                        }
                        break;

                    case "stats":
                        if (playerStats == null)
                        {
                            Console.WriteLine("PlayerStats não inicializado. Use o comando 'scan' primeiro.");
                        }
                        else
                        {
                            playerStats.ShowAllStats();
                        }
                        break;

                    case "exit":
                        memoryManager.Detach();
                        return;

                    default:
                        Console.WriteLine("Comando desconhecido. Digite 'help' para ver os comandos disponíveis.");
                        break;
                }
            }
        }

        static void ShowHelp()
        {
            Console.WriteLine("Comandos disponíveis:");
            Console.WriteLine("scan - Procura por endereços de memória importantes");
            Console.WriteLine("save - Salva os endereços encontrados em um arquivo");
            Console.WriteLine("load - Carrega endereços de um arquivo");
            Console.WriteLine("pos - Mostra a posição atual do personagem");
            Console.WriteLine("move - Move o personagem para uma nova posição");
            Console.WriteLine("shop - Abre uma loja específica");
            Console.WriteLine("stats - Mostra as estatísticas do personagem (requer scan)");
            Console.WriteLine("exit - Sai do programa");
        }
    }
}
