using System;
using System.Collections.Generic;

namespace RagnarokController
{
    public class PlayerStats
    {
        private readonly MemoryManager _memoryManager;
        private readonly Dictionary<string, int> _offsets;
        private IntPtr _baseAddress;

        public PlayerStats(MemoryManager memoryManager, Dictionary<string, int> offsets)
        {
            _memoryManager = memoryManager ?? throw new ArgumentNullException(nameof(memoryManager));
            _offsets = offsets ?? throw new ArgumentNullException(nameof(offsets));
            
            // Encontra o endereço base do personagem
            var process = memoryManager.GetProcess();
            if (process?.MainModule == null)
            {
                throw new InvalidOperationException("Processo ou módulo principal não encontrado.");
            }
            _baseAddress = process.MainModule.BaseAddress;
        }

        public int GetHP()
        {
            if (!_offsets.ContainsKey("HP")) return 0;
            return _memoryManager.ReadMemory<int>(_baseAddress + _offsets["HP"]);
        }

        public int GetSP()
        {
            if (!_offsets.ContainsKey("SP")) return 0;
            return _memoryManager.ReadMemory<int>(_baseAddress + _offsets["SP"]);
        }

        public int GetBaseLevel()
        {
            if (!_offsets.ContainsKey("BaseLevel")) return 0;
            return _memoryManager.ReadMemory<int>(_baseAddress + _offsets["BaseLevel"]);
        }

        public int GetJobLevel()
        {
            if (!_offsets.ContainsKey("JobLevel")) return 0;
            return _memoryManager.ReadMemory<int>(_baseAddress + _offsets["JobLevel"]);
        }

        public int GetZeny()
        {
            if (!_offsets.ContainsKey("Zeny")) return 0;
            return _memoryManager.ReadMemory<int>(_baseAddress + _offsets["Zeny"]);
        }

        public int GetStatusPoint()
        {
            if (!_offsets.ContainsKey("StatusPoint")) return 0;
            return _memoryManager.ReadMemory<int>(_baseAddress + _offsets["StatusPoint"]);
        }

        public int GetSkillPoint()
        {
            if (!_offsets.ContainsKey("SkillPoint")) return 0;
            return _memoryManager.ReadMemory<int>(_baseAddress + _offsets["SkillPoint"]);
        }

        public void ShowAllStats()
        {
            Console.WriteLine("\n=== Estatísticas do Personagem ===");
            
            if (_offsets.ContainsKey("HP"))
            {
                int hp = GetHP();
                Console.WriteLine($"HP: {hp} (Offset: 0x{_offsets["HP"]:X})");
            }
            
            if (_offsets.ContainsKey("SP"))
            {
                int sp = GetSP();
                Console.WriteLine($"SP: {sp} (Offset: 0x{_offsets["SP"]:X})");
            }
            
            if (_offsets.ContainsKey("BaseLevel"))
            {
                int baseLevel = GetBaseLevel();
                Console.WriteLine($"Nível Base: {baseLevel} (Offset: 0x{_offsets["BaseLevel"]:X})");
            }
            
            if (_offsets.ContainsKey("JobLevel"))
            {
                int jobLevel = GetJobLevel();
                Console.WriteLine($"Nível de Classe: {jobLevel} (Offset: 0x{_offsets["JobLevel"]:X})");
            }
            
            if (_offsets.ContainsKey("Zeny"))
            {
                int zeny = GetZeny();
                Console.WriteLine($"Zeny: {zeny} (Offset: 0x{_offsets["Zeny"]:X})");
            }
            
            if (_offsets.ContainsKey("StatusPoint"))
            {
                int statusPoint = GetStatusPoint();
                Console.WriteLine($"Pontos de Status: {statusPoint} (Offset: 0x{_offsets["StatusPoint"]:X})");
            }
            
            if (_offsets.ContainsKey("SkillPoint"))
            {
                int skillPoint = GetSkillPoint();
                Console.WriteLine($"Pontos de Habilidade: {skillPoint} (Offset: 0x{_offsets["SkillPoint"]:X})");
            }
            
            Console.WriteLine("=================================");
        }
    }
} 