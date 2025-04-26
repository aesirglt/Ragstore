using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace RagnarokController
{
    public class MovementManager
    {
        private const int PLAYER_STRUCT_OFFSET = 0x4F020;
        private const int POSITION_X_OFFSET = 0xED832C;
        private const int POSITION_Y_OFFSET = 0xED8330;
        private const int MOVE_FUNCTION_OFFSET = 0x70B31F;

        private readonly MemoryManager _memoryManager;

        public MovementManager(MemoryManager memoryManager)
        {
            _memoryManager = memoryManager ?? throw new ArgumentNullException(nameof(memoryManager));
        }

        private IntPtr GetPlayerPointer()
        {
            try
            {
                var process = _memoryManager.GetProcess();
                if (process?.MainModule == null) return IntPtr.Zero;

                var baseAddress = process.MainModule.BaseAddress;
                
                // Primeiro encontra a estrutura base (ebx no assembly)
                var baseStruct = _memoryManager.ReadMemory<IntPtr>(IntPtr.Add(baseAddress, 0xC5F7D0));
                if (baseStruct == IntPtr.Zero)
                {
                    Console.WriteLine("Estrutura base não encontrada");
                    return IntPtr.Zero;
                }

                Console.WriteLine($"Estrutura base encontrada em: 0x{baseStruct.ToInt64():X}");

                // Agora lê o ponteiro do jogador usando o offset encontrado
                var playerPtr = _memoryManager.ReadMemory<IntPtr>(IntPtr.Add(baseStruct, PLAYER_STRUCT_OFFSET));
                Console.WriteLine($"Ponteiro do jogador: 0x{playerPtr.ToInt64():X}");

                return playerPtr;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter ponteiro do jogador: {ex.Message}");
                return IntPtr.Zero;
            }
        }

        public (int x, int y) GetCurrentPosition()
        {
            try
            {
                var process = _memoryManager.GetProcess();
                if (process?.MainModule == null)
                {
                    Console.WriteLine("Processo não encontrado.");
                    return (0, 0);
                }

                var baseAddress = process.MainModule.BaseAddress;

                // Lê diretamente dos offsets absolutos que sabemos que funcionam
                int x = _memoryManager.ReadMemory<int>(IntPtr.Add(baseAddress, POSITION_X_OFFSET));
                int y = _memoryManager.ReadMemory<int>(IntPtr.Add(baseAddress, POSITION_Y_OFFSET));

                Console.WriteLine($"Lendo X em: 0x{(baseAddress.ToInt64() + POSITION_X_OFFSET):X} = {x}");
                Console.WriteLine($"Lendo Y em: 0x{(baseAddress.ToInt64() + POSITION_Y_OFFSET):X} = {y}");

                return (x, y);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao ler posição atual: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return (0, 0);
            }
        }

        public bool MoveTo(int x, int y)
        {
            try
            {
                var process = _memoryManager.GetProcess();
                if (process?.MainModule == null)
                {
                    Console.WriteLine("Processo não encontrado.");
                    return false;
                }

                var baseAddress = process.MainModule.BaseAddress;

                // Escreve diretamente nos endereços de posição
                _memoryManager.WriteMemory<int>(IntPtr.Add(baseAddress, POSITION_X_OFFSET), x);
                _memoryManager.WriteMemory<int>(IntPtr.Add(baseAddress, POSITION_Y_OFFSET), y);

                Console.WriteLine($"Movendo para X={x}, Y={y}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao mover o personagem: {ex.Message}");
                return false;
            }
        }

        public bool WalkTo(int x, int y)
        {
            try
            {
                var currentPos = GetCurrentPosition();
                Console.WriteLine($"Posição atual: X={currentPos.x}, Y={currentPos.y}");
                Console.WriteLine($"Destino: X={x}, Y={y}");

                // Calcula a distância total
                int dx = Math.Abs(x - currentPos.x);
                int dy = Math.Abs(y - currentPos.y);
                double distance = Math.Sqrt(dx * dx + dy * dy);
                
                // Se a distância for muito pequena, move diretamente
                if (distance < 5)
                {
                    Console.WriteLine("Distância muito pequena, movendo diretamente");
                    return MoveTo(x, y);
                }
                
                const int stepSize = 5;
                var steps = Math.Max(1, Math.Max(dx, dy) / stepSize);

                Console.WriteLine($"Distância: {distance:F2}, Caminhando em {steps} passos");

                for (int i = 0; i <= steps; i++)
                {
                    var stepX = currentPos.x + ((x - currentPos.x) * i / steps);
                    var stepY = currentPos.y + ((y - currentPos.y) * i / steps);
                    
                    Console.WriteLine($"Passo {i}/{steps}: Movendo para X={stepX}, Y={stepY}");
                    
                    if (!MoveTo(stepX, stepY))
                    {
                        Console.WriteLine("Falha ao executar passo do movimento");
                        return false;
                    }
                    
                    Thread.Sleep(100); // Aguarda um pouco entre cada passo
                }

                Console.WriteLine("Movimento concluído com sucesso!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao caminhar: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return false;
            }
        }

        public bool StopMoving()
        {
            try
            {
                var currentPos = GetCurrentPosition();
                return MoveTo(currentPos.x, currentPos.y);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao parar o movimento: {ex.Message}");
                return false;
            }
        }
    }
} 