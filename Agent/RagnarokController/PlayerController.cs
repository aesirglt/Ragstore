using System;
using System.Runtime.InteropServices;

namespace RagnarokController
{
    public class PlayerController
    {
        private readonly MemoryManager _memoryManager;
        private readonly IntPtr _playerBaseAddress;

        public PlayerController(MemoryManager memoryManager, IntPtr playerBaseAddress)
        {
            _memoryManager = memoryManager;
            _playerBaseAddress = playerBaseAddress;
        }

        public (int x, int y) GetCurrentPosition()
        {
            int x = _memoryManager.ReadMemory<int>(_playerBaseAddress + 0x99C000);
            int y = _memoryManager.ReadMemory<int>(_playerBaseAddress + 0x99C004);
            return (x, y);
        }

        public void MoveTo(int x, int y)
        {
            _memoryManager.WriteMemory(_playerBaseAddress + 0x99C000, x);
            _memoryManager.WriteMemory(_playerBaseAddress + 0x99C004, y);
        }
    }
} 