using System;
using System.Runtime.InteropServices;

namespace RagnarokController
{
    public class PlayerController
    {
        private readonly MemoryManager _memoryManager;
        private readonly PacketManager _packetManager;
        
        // Endereços fixos encontrados pelo usuário
        private static readonly IntPtr PLAYER_X_OFFSET = (IntPtr)0x012D832C;
        private static readonly IntPtr PLAYER_Y_OFFSET = (IntPtr)0x012D8330;

        public PlayerController(MemoryManager memoryManager)
        {
            _memoryManager = memoryManager;
            _packetManager = new PacketManager(memoryManager);
        }

        public void MoveTo(int x, int y)
        {
            // Envia o pacote de movimento
            _packetManager.SendMovePacket(x, y);

            // Atualiza a posição na memória
            byte[] xBytes = BitConverter.GetBytes(x);
            _memoryManager.WriteMemory(PLAYER_X_OFFSET, xBytes);

            byte[] yBytes = BitConverter.GetBytes(y);
            _memoryManager.WriteMemory(PLAYER_Y_OFFSET, yBytes);
        }

        public (int X, int Y) GetCurrentPosition()
        {
            // Lê a posição X atual
            byte[] xBytes = _memoryManager.ReadMemory(PLAYER_X_OFFSET, 4);
            int x = BitConverter.ToInt32(xBytes, 0);

            // Lê a posição Y atual
            byte[] yBytes = _memoryManager.ReadMemory(PLAYER_Y_OFFSET, 4);
            int y = BitConverter.ToInt32(yBytes, 0);

            return (x, y);
        }
    }
} 