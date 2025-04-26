using System;

namespace RagnarokController
{
    public class PlayerStats
    {
        private readonly MemoryManager _memoryManager;
        private readonly IntPtr _baseAddress;

        // Offsets baseados no executável antigo
        private const int HP_OFFSET = 0x23DC;
        private const int SP_OFFSET = 0x240C;
        private const int MAX_HP_OFFSET = 0x237C;
        private const int MAX_SP_OFFSET = 0x23AC;
        private const int JOB_LEVEL_OFFSET = 0x136C;
        private const int BASE_LEVEL_OFFSET = 0x1368;
        private const int WEIGHT_OFFSET = 0x141C;
        private const int MAX_WEIGHT_OFFSET = 0x1410;

        public PlayerStats(MemoryManager memoryManager, IntPtr baseAddress)
        {
            _memoryManager = memoryManager;
            _baseAddress = baseAddress;
        }

        public int GetHP()
        {
            return _memoryManager.ReadMemoryInt(_baseAddress + HP_OFFSET);
        }

        public int GetMaxHP()
        {
            return _memoryManager.ReadMemoryInt(_baseAddress + MAX_HP_OFFSET);
        }

        public int GetSP()
        {
            return _memoryManager.ReadMemoryInt(_baseAddress + SP_OFFSET);
        }

        public int GetMaxSP()
        {
            return _memoryManager.ReadMemoryInt(_baseAddress + MAX_SP_OFFSET);
        }

        public int GetJobLevel()
        {
            return _memoryManager.ReadMemoryInt(_baseAddress + JOB_LEVEL_OFFSET);
        }

        public int GetBaseLevel()
        {
            return _memoryManager.ReadMemoryInt(_baseAddress + BASE_LEVEL_OFFSET);
        }

        public int GetWeight()
        {
            return _memoryManager.ReadMemoryInt(_baseAddress + WEIGHT_OFFSET);
        }

        public int GetMaxWeight()
        {
            return _memoryManager.ReadMemoryInt(_baseAddress + MAX_WEIGHT_OFFSET);
        }

        public void ShowAllStats()
        {
            Console.WriteLine($"=== Status do Personagem ===");
            Console.WriteLine($"HP: {GetHP()}/{GetMaxHP()}");
            Console.WriteLine($"SP: {GetSP()}/{GetMaxSP()}");
            Console.WriteLine($"Nível Base: {GetBaseLevel()}");
            Console.WriteLine($"Nível Classe: {GetJobLevel()}");
            Console.WriteLine($"Peso: {GetWeight()}/{GetMaxWeight()}");
            Console.WriteLine($"========================");
        }
    }
} 