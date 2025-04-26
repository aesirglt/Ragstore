using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace RagnarokController
{
    public class PlayerController
    {
        private readonly MemoryManager _memoryManager;
        private readonly IntPtr _playerBaseAddress;
        private Dictionary<string, int> _offsets;

        public PlayerController(MemoryManager memoryManager, IntPtr playerBaseAddress)
        {
            _memoryManager = memoryManager ?? throw new ArgumentNullException(nameof(memoryManager));
            _playerBaseAddress = playerBaseAddress;
            _offsets = new Dictionary<string, int>();
        }

        public void UpdateOffsets(Dictionary<string, int> offsets)
        {
            if (offsets == null) throw new ArgumentNullException(nameof(offsets));
            _offsets = offsets;
        }

        public (int x, int y) GetCurrentPosition()
        {
            if (!_offsets.ContainsKey("PosX") || !_offsets.ContainsKey("PosY"))
            {
                Console.WriteLine("Offsets de posição não encontrados. Execute o comando 'analyze' primeiro.");
                return (0, 0);
            }

            int x = _memoryManager.ReadMemory<int>(_playerBaseAddress + _offsets["PosX"]);
            int y = _memoryManager.ReadMemory<int>(_playerBaseAddress + _offsets["PosY"]);
            return (x, y);
        }

        public void MoveTo(int x, int y)
        {
            if (!_offsets.ContainsKey("PosX") || !_offsets.ContainsKey("PosY"))
            {
                Console.WriteLine("Offsets de posição não encontrados. Execute o comando 'analyze' primeiro.");
                return;
            }

            _memoryManager.WriteMemory(_playerBaseAddress + _offsets["PosX"], x);
            _memoryManager.WriteMemory(_playerBaseAddress + _offsets["PosY"], y);
        }
    }
} 