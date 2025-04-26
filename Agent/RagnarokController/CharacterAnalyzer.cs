using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace RagnarokController
{
    public class CharacterAnalyzer
    {
        private readonly MemoryManager _memoryManager;
        private readonly MemoryScanner _scanner;

        public CharacterAnalyzer(MemoryManager memoryManager)
        {
            _memoryManager = memoryManager;
            _scanner = new MemoryScanner(memoryManager);
        }

        public Dictionary<string, IntPtr> AnalyzeCharacterStructure()
        {
            Console.WriteLine("Iniciando análise da estrutura do personagem...");
            var offsets = new Dictionary<string, IntPtr>();

            // Padrões mais genéricos para busca
            var patterns = new Dictionary<string, (byte[] pattern, string mask)>
            {
                { "HP", (new byte[] { 0x8B, 0x45, 0x00, 0x8B, 0x80 }, "xx?xx") },
                { "SP", (new byte[] { 0x8B, 0x45, 0x00, 0x8B, 0x88 }, "xx?xx") },
                { "BaseLevel", (new byte[] { 0x8B, 0x45, 0x00, 0x8B, 0x90 }, "xx?xx") },
                { "JobLevel", (new byte[] { 0x8B, 0x45, 0x00, 0x8B, 0x98 }, "xx?xx") },
                { "Weight", (new byte[] { 0x8B, 0x45, 0x00, 0x8B, 0xA0 }, "xx?xx") },
                { "MaxWeight", (new byte[] { 0x8B, 0x45, 0x00, 0x8B, 0xA8 }, "xx?xx") },
                { "Zeny", (new byte[] { 0x8B, 0x45, 0x00, 0x8B, 0xB0 }, "xx?xx") },
                { "BaseExp", (new byte[] { 0x8B, 0x45, 0x00, 0x8B, 0xB8 }, "xx?xx") },
                { "JobExp", (new byte[] { 0x8B, 0x45, 0x00, 0x8B, 0xC0 }, "xx?xx") },
                { "StatusPoint", (new byte[] { 0x8B, 0x45, 0x00, 0x8B, 0xC8 }, "xx?xx") },
                { "SkillPoint", (new byte[] { 0x8B, 0x45, 0x00, 0x8B, 0xD0 }, "xx?xx") }
            };

            // Procura por cada padrão
            foreach (var pattern in patterns)
            {
                Console.WriteLine($"Procurando offset para {pattern.Key}...");
                var offset = _scanner.FindPattern(pattern.Value.pattern, pattern.Value.mask);
                
                if (offset != IntPtr.Zero)
                {
                    offsets[pattern.Key] = offset;
                    Console.WriteLine($"Offset de {pattern.Key} encontrado: 0x{offset.ToString("X")}");

                    // Tenta ler o valor para verificar se o offset está correto
                    try
                    {
                        int value = _memoryManager.ReadMemory<int>(offset);
                        Console.WriteLine($"Valor atual de {pattern.Key}: {value}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao ler valor de {pattern.Key}: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine($"Não foi possível encontrar o offset para {pattern.Key}");
                }
            }

            if (offsets.Count == 0)
            {
                Console.WriteLine("Aviso: Nenhum offset foi encontrado. Verifique se o processo do Ragnarok está em execução e se você tem permissões adequadas.");
            }
            else
            {
                Console.WriteLine($"Análise concluída. {offsets.Count} offsets encontrados.");
            }

            // Procura pelos offsets de SP
            var spOffset = _scanner.FindPattern(new byte[] { 0x8B, 0x45, 0x08, 0x8B, 0x80, 0x00, 0x00, 0x00, 0x00, 0x89, 0x45, 0xF8 }, "xxxx????xxxx");
            if (spOffset != IntPtr.Zero)
            {
                offsets["SP"] = spOffset + 5;
                Console.WriteLine("Offset de SP encontrado: 0x" + (spOffset + 5).ToString("X"));
            }

            // Procura pelos offsets de nível base
            var baseLevelOffset = _scanner.FindPattern(new byte[] { 0x8B, 0x45, 0x08, 0x8B, 0x80, 0x00, 0x00, 0x00, 0x00, 0x89, 0x45, 0xF4 }, "xxxx????xxxx");
            if (baseLevelOffset != IntPtr.Zero)
            {
                offsets["BaseLevel"] = baseLevelOffset + 5;
                Console.WriteLine("Offset de nível base encontrado: 0x" + (baseLevelOffset + 5).ToString("X"));
            }

            // Procura pelos offsets de nível de classe
            var jobLevelOffset = _scanner.FindPattern(new byte[] { 0x8B, 0x45, 0x08, 0x8B, 0x80, 0x00, 0x00, 0x00, 0x00, 0x89, 0x45, 0xF0 }, "xxxx????xxxx");
            if (jobLevelOffset != IntPtr.Zero)
            {
                offsets["JobLevel"] = jobLevelOffset + 5;
                Console.WriteLine("Offset de nível de classe encontrado: 0x" + (jobLevelOffset + 5).ToString("X"));
            }

            // Procura pelos offsets de peso
            var weightOffset = _scanner.FindPattern(new byte[] { 0x8B, 0x45, 0x08, 0x8B, 0x80, 0x00, 0x00, 0x00, 0x00, 0x89, 0x45, 0xEC }, "xxxx????xxxx");
            if (weightOffset != IntPtr.Zero)
            {
                offsets["Weight"] = weightOffset + 5;
                Console.WriteLine("Offset de peso encontrado: 0x" + (weightOffset + 5).ToString("X"));
            }

            // Procura pelos offsets de zeny
            var zenyOffset = _scanner.FindPattern(new byte[] { 0x8B, 0x45, 0x08, 0x8B, 0x80, 0x00, 0x00, 0x00, 0x00, 0x89, 0x45, 0xE8 }, "xxxx????xxxx");
            if (zenyOffset != IntPtr.Zero)
            {
                offsets["Zeny"] = zenyOffset + 5;
                Console.WriteLine("Offset de zeny encontrado: 0x" + (zenyOffset + 5).ToString("X"));
            }

            // Procura pelos offsets de experiência
            var expOffset = _scanner.FindPattern(new byte[] { 0x8B, 0x45, 0x08, 0x8B, 0x80, 0x00, 0x00, 0x00, 0x00, 0x89, 0x45, 0xE4 }, "xxxx????xxxx");
            if (expOffset != IntPtr.Zero)
            {
                offsets["BaseExp"] = expOffset + 5;
                Console.WriteLine("Offset de experiência base encontrado: 0x" + (expOffset + 5).ToString("X"));
            }

            // Procura pelos offsets de pontos de status
            var statusPointOffset = _scanner.FindPattern(new byte[] { 0x8B, 0x45, 0x08, 0x8B, 0x80, 0x00, 0x00, 0x00, 0x00, 0x89, 0x45, 0xE0 }, "xxxx????xxxx");
            if (statusPointOffset != IntPtr.Zero)
            {
                offsets["StatusPoint"] = statusPointOffset + 5;
                Console.WriteLine("Offset de pontos de status encontrado: 0x" + (statusPointOffset + 5).ToString("X"));
            }

            // Procura pelos offsets de pontos de habilidade
            var skillPointOffset = _scanner.FindPattern(new byte[] { 0x8B, 0x45, 0x08, 0x8B, 0x80, 0x00, 0x00, 0x00, 0x00, 0x89, 0x45, 0xDC }, "xxxx????xxxx");
            if (skillPointOffset != IntPtr.Zero)
            {
                offsets["SkillPoint"] = skillPointOffset + 5;
                Console.WriteLine("Offset de pontos de habilidade encontrado: 0x" + (skillPointOffset + 5).ToString("X"));
            }

            return offsets;
        }
    }
} 