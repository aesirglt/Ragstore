using System;
using System.Diagnostics;

namespace RagnarokController
{
    class Program
    {
        private static MemoryManager _memoryManager = null!;
        private static MovementManager _movementManager = null!;

        static void Main(string[] args)
        {
            Console.WriteLine("Inicializando Ragnarok Controller...");
            
            _memoryManager = new MemoryManager();

            try
            {
                if (!_memoryManager.AttachToProcess())
                {
                    Console.WriteLine("Erro: Não foi possível encontrar o processo Ragexe.exe");
                    return;
                }

                _movementManager = new MovementManager(_memoryManager);
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
                            var pos = _movementManager.GetCurrentPosition();
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
                            
                            Console.Write("Usar movimento suave? (s/n): ");
                            bool smoothMovement = Console.ReadLine()?.ToLower() == "s";
                            
                            if (smoothMovement)
                            {
                                _movementManager.WalkTo(x, y);
                            }
                            else
                            {
                                _movementManager.MoveTo(x, y);
                            }
                            break;
                            
                        case "stop":
                            if (!_movementManager.StopMoving())
                            {
                                Console.WriteLine("Falha ao parar o movimento do personagem.");
                            }
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
            Console.WriteLine("move - Move o personagem para as coordenadas especificadas (com opção de movimento suave)");
            Console.WriteLine("stop - Para o movimento do personagem");
            Console.WriteLine("exit - Sai do programa");
        }
    }
}
