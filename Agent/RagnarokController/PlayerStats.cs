using System;
using System.Collections.Generic;

namespace RagnarokController
{
    public class PlayerStats
    {
        private readonly MemoryManager _memoryManager;
        private readonly Dictionary<string, IntPtr> _offsets;

        public PlayerStats(MemoryManager memoryManager, Dictionary<string, IntPtr> offsets)
        {
            _memoryManager = memoryManager;
            _offsets = offsets;
            
            if (_offsets == null || _offsets.Count == 0)
            {
                throw new ArgumentException("Nenhum offset fornecido para PlayerStats");
            }

            Console.WriteLine($"PlayerStats inicializado com {_offsets.Count} offsets:");
            foreach (var offset in _offsets)
            {
                Console.WriteLine($"- {offset.Key}: 0x{offset.Value.ToString("X")}");
            }
        }

        private int ReadStat(string statName)
        {
            if (!_offsets.TryGetValue(statName, out IntPtr offset))
            {
                Console.WriteLine($"Aviso: Offset para {statName} não encontrado");
                return 0;
            }

            try
            {
                int value = _memoryManager.ReadMemory<int>(offset);
                Console.WriteLine($"Leitura de {statName}: {value} (Offset: 0x{offset.ToString("X")})");
                return value;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao ler {statName}: {ex.Message}");
                return 0;
            }
        }

        public int GetHP() => ReadStat("HP");
        public int GetMaxHP() => ReadStat("MaxHP");
        public int GetSP() => ReadStat("SP");
        public int GetMaxSP() => ReadStat("MaxSP");
        public int GetBaseLevel() => ReadStat("BaseLevel");
        public int GetJobLevel() => ReadStat("JobLevel");
        public int GetWeight() => ReadStat("Weight");
        public int GetMaxWeight() => ReadStat("MaxWeight");
        public int GetZeny() => ReadStat("Zeny");
        public int GetBaseExp() => ReadStat("BaseExp");
        public int GetJobExp() => ReadStat("JobExp");
        public int GetStatusPoint() => ReadStat("StatusPoint");
        public int GetSkillPoint() => ReadStat("SkillPoint");

        public void ShowAllStats()
        {
            Console.WriteLine("\n=== Estatísticas do Personagem ===");
            
            var stats = new Dictionary<string, (string Name, Func<int> Getter)>
            {
                { "HP", ("HP", GetHP) },
                { "MaxHP", ("HP Máximo", GetMaxHP) },
                { "SP", ("SP", GetSP) },
                { "MaxSP", ("SP Máximo", GetMaxSP) },
                { "BaseLevel", ("Nível Base", GetBaseLevel) },
                { "JobLevel", ("Nível de Classe", GetJobLevel) },
                { "Weight", ("Peso", GetWeight) },
                { "MaxWeight", ("Peso Máximo", GetMaxWeight) },
                { "Zeny", ("Zeny", GetZeny) },
                { "BaseExp", ("Experiência Base", GetBaseExp) },
                { "JobExp", ("Experiência de Classe", GetJobExp) },
                { "StatusPoint", ("Pontos de Status", GetStatusPoint) },
                { "SkillPoint", ("Pontos de Habilidade", GetSkillPoint) }
            };

            foreach (var stat in stats)
            {
                if (_offsets.ContainsKey(stat.Key))
                {
                    try
                    {
                        int value = stat.Value.Getter();
                        Console.WriteLine($"{stat.Value.Name}: {value}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{stat.Value.Name}: Erro ao ler ({ex.Message})");
                    }
                }
            }

            Console.WriteLine("=================================\n");
        }
    }
} 