using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace RagnarokController
{
    public class CharacterAnalyzer
    {
        private readonly MemoryManager _memoryManager;
        private const string PDB_FILE = "HighPriest.pdb";
        
        // Offsets conhecidos
        private static readonly Dictionary<string, int> KNOWN_OFFSETS = new Dictionary<string, int>
        {
            // Níveis
            { "BaseLevel", 0xEEBA38 },
            { "JobLevel", 0xEEBA40 },
            
            // HP/SP
            { "CurrentHP", 0xEEF638 },
            { "MaxHP", 0xEEF63C },
            { "CurrentSP", 0xEEF640 },
            { "MaxSP", 0xEEF644 },
            
            // Atributos Base
            { "Strength", 0xEEBA6C },
            { "Vitality", 0xEEBA74 },
            { "Luck", 0xEEBA80 },
            
            // Status
            { "Attack", 0xEEBAA0 },
            { "Critical", 0xEEBACC },
            
            // Peso e Dinheiro
            { "CurrentWeight", 0xEEBAE8 },
            { "MaxWeight", 0xEEBAE4 },
            { "Zeny", 0xEEBAD8 },
            
            // Nome do personagem (string)
            { "CharacterName", 0xEF1C90 },

            // Posição (mantendo os anteriores por enquanto)
            { "PosX", 0xED832C },
            { "PosY", 0xED8330 }
        };

        // Tipos de dados para cada offset
        private static readonly Dictionary<string, Type> OFFSET_TYPES = new Dictionary<string, Type>
        {
            { "CharacterName", typeof(string) },
            // Todos os outros são int por padrão
        };

        public CharacterAnalyzer(MemoryManager memoryManager)
        {
            _memoryManager = memoryManager ?? throw new ArgumentNullException(nameof(memoryManager));
        }

        public Dictionary<string, int> AnalyzeCharacterStructure()
        {
            var offsets = new Dictionary<string, int>();
            
            // Primeiro, tenta encontrar os offsets usando o PDB
            if (File.Exists(PDB_FILE))
            {
                Console.WriteLine("Analisando arquivo PDB...");
                AnalyzePDB(offsets);
            }

            // Se não encontrou todos os offsets no PDB, usa os offsets conhecidos
            if (offsets.Count < KNOWN_OFFSETS.Count)
            {
                Console.WriteLine("Usando offsets conhecidos...");
                foreach (var kvp in KNOWN_OFFSETS)
                {
                    offsets[kvp.Key] = kvp.Value;
                    Console.WriteLine($"Offset de {kvp.Key} definido como: 0x{kvp.Value:X}");
                }
            }

            // Verifica os valores
            VerifyOffsets(offsets);

            return offsets;
        }

        private void VerifyOffsets(Dictionary<string, int> offsets)
        {
            var process = _memoryManager.GetProcess();
            if (process?.MainModule == null) return;

            IntPtr baseAddress = process.MainModule.BaseAddress;

            Console.WriteLine("\n=== Valores Atuais ===");
            foreach (var kvp in offsets)
            {
                try
                {
                    if (OFFSET_TYPES.TryGetValue(kvp.Key, out Type? type) && type == typeof(string))
                    {
                        // Lê string (nome do personagem)
                        byte[] buffer = new byte[24]; // Tamanho máximo do nome
                        _memoryManager.ReadProcessMemory(baseAddress + kvp.Value, buffer, buffer.Length, out int _);
                        string value = Encoding.ASCII.GetString(buffer).TrimEnd('\0');
                        Console.WriteLine($"{kvp.Key}: {value} (Offset: 0x{kvp.Value:X})");
                    }
                    else
                    {
                        // Lê valor numérico
                        int value = _memoryManager.ReadMemory<int>(baseAddress + kvp.Value);
                        Console.WriteLine($"{kvp.Key}: {value} (Offset: 0x{kvp.Value:X})");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao ler {kvp.Key} no offset 0x{kvp.Value:X}: {ex.Message}");
                }
            }
            Console.WriteLine("===================");
        }

        private void AnalyzePDB(Dictionary<string, int> offsets)
        {
            try
            {
                var pdbContent = File.ReadAllText(PDB_FILE);
                foreach (var kvp in KNOWN_OFFSETS)
                {
                    FindOffsetInPDB(pdbContent, kvp.Key, GetFieldName(kvp.Key), offsets);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao analisar PDB: {ex.Message}");
            }
        }

        private string GetFieldName(string statName)
        {
            // Mapeia nomes de estatísticas para nomes de campos no PDB
            switch (statName)
            {
                case "CurrentHP": return "m_nHP";
                case "MaxHP": return "m_nMaxHP";
                case "CurrentSP": return "m_nSP";
                case "MaxSP": return "m_nMaxSP";
                case "BaseLevel": return "m_nBaseLevel";
                case "JobLevel": return "m_nJobLevel";
                case "Strength": return "m_nStr";
                case "Vitality": return "m_nVit";
                case "Luck": return "m_nLuk";
                case "Attack": return "m_nAtk";
                case "Critical": return "m_nCritical";
                case "CurrentWeight": return "m_nWeight";
                case "MaxWeight": return "m_nMaxWeight";
                case "Zeny": return "m_nZeny";
                case "CharacterName": return "m_szName";
                case "PosX": return "m_nPosX";
                case "PosY": return "m_nPosY";
                default: return $"m_n{statName}";
            }
        }

        private void FindOffsetInPDB(string pdbContent, string statName, string fieldName, Dictionary<string, int> offsets)
        {
            var fieldIndex = pdbContent.IndexOf(fieldName);
            if (fieldIndex != -1)
            {
                var offsetStr = ExtractOffsetFromPDB(pdbContent, fieldIndex);
                if (int.TryParse(offsetStr, System.Globalization.NumberStyles.HexNumber, null, out int offset))
                {
                    offsets[statName] = offset;
                    Console.WriteLine($"Offset de {statName} encontrado no PDB: 0x{offset:X}");
                }
            }
        }

        private string ExtractOffsetFromPDB(string content, int fieldIndex)
        {
            var start = content.IndexOf("0x", fieldIndex);
            if (start != -1)
            {
                var end = content.IndexOfAny(new[] { ' ', '\n', '\r', '\t' }, start);
                if (end != -1)
                {
                    return content.Substring(start + 2, end - start - 2);
                }
            }
            return string.Empty;
        }
    }
} 