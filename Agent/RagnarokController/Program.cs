using System;
using System.Collections.Generic;

namespace RagnarokController
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Iniciando Controlador do Ragnarok Online...");

            var memoryManager = new MemoryManager();
            if (!memoryManager.AttachToProcess())
            {
                Console.WriteLine("Não foi possível conectar ao processo do Ragnarok Online.");
                Console.WriteLine("Certifique-se de que:");
                Console.WriteLine("1. O jogo está em execução");
                Console.WriteLine("2. Você está executando este programa como administrador");
                Console.WriteLine("3. O nome do processo é 'Ragexe'");
                return;
            }

            Console.WriteLine("Conectado ao Ragnarok Online com sucesso!");
            
            var scanner = new MemoryScanner(memoryManager);
            var addresses = new RagnarokAddresses(scanner);
            var analyzer = new CharacterAnalyzer(memoryManager);
            PlayerStats? playerStats = null;

            Console.WriteLine("\nDigite 'help' para ver os comandos disponíveis.");

            while (true)
            {
                try
                {
                    Console.Write("\n> ");
                    string? input = Console.ReadLine();
                    string command = input?.ToLower() ?? "help";

                    switch (command)
                    {
                        case "help":
                            ShowHelp();
                            break;

                        case "scan":
                            Console.WriteLine("\nIniciando busca por endereços...");
                            addresses.FindAddresses();
                            break;

                        case "analyze":
                            Console.WriteLine("\nIniciando análise da estrutura do personagem...");
                            try
                            {
                                var offsets = analyzer.AnalyzeCharacterStructure();
                                if (offsets.Count > 0)
                                {
                                    playerStats = new PlayerStats(memoryManager, offsets);
                                    Console.WriteLine("Análise concluída com sucesso!");
                                }
                                else
                                {
                                    Console.WriteLine("Nenhum offset encontrado durante a análise.");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Erro durante a análise: {ex.Message}");
                            }
                            break;

                        case "save":
                            Console.Write("Digite o caminho do arquivo para salvar: ");
                            string? savePath = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(savePath))
                            {
                                Console.WriteLine("Caminho inválido.");
                                break;
                            }
                            try
                            {
                                addresses.SaveAddresses(savePath);
                                Console.WriteLine("Endereços salvos com sucesso!");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Erro ao salvar endereços: {ex.Message}");
                            }
                            break;

                        case "load":
                            Console.Write("Digite o caminho do arquivo para carregar: ");
                            string? loadPath = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(loadPath))
                            {
                                Console.WriteLine("Caminho inválido.");
                                break;
                            }
                            try
                            {
                                addresses.LoadAddresses(loadPath);
                                Console.WriteLine("Endereços carregados com sucesso!");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Erro ao carregar endereços: {ex.Message}");
                            }
                            break;

                        case "stats":
                            if (playerStats == null)
                            {
                                Console.WriteLine("PlayerStats não inicializado. Use o comando 'analyze' primeiro.");
                            }
                            else
                            {
                                playerStats.ShowAllStats();
                            }
                            break;

                        case "exit":
                            Console.WriteLine("Desconectando do Ragnarok Online...");
                            memoryManager.Detach();
                            return;

                        default:
                            Console.WriteLine("Comando desconhecido. Digite 'help' para ver os comandos disponíveis.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nErro inesperado: {ex.Message}");
                    Console.WriteLine("O programa continuará executando. Digite 'exit' para sair.");
                }
            }
        }

        static void ShowHelp()
        {
            Console.WriteLine("\nComandos disponíveis:");
            Console.WriteLine("help     - Mostra esta lista de comandos");
            Console.WriteLine("scan     - Procura por endereços de memória importantes");
            Console.WriteLine("analyze  - Analisa a estrutura do personagem");
            Console.WriteLine("save     - Salva os endereços encontrados em um arquivo");
            Console.WriteLine("load     - Carrega endereços de um arquivo");
            Console.WriteLine("stats    - Mostra as estatísticas do personagem (requer analyze)");
            Console.WriteLine("exit     - Sai do programa");
        }
    }
}
