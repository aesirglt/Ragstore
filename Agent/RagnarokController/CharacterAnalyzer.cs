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

            // Se não encontrou todos os offsets no PDB, procura na memória
            if (offsets.Count < 8) // Temos 8 estatísticas para encontrar
            {
                Console.WriteLine("Procurando offsets na memória...");
                FindOffsetsInMemory(offsets);
            }

            return offsets;
        }

        private void AnalyzePDB(Dictionary<string, int> offsets)
        {
            try
            {
                // Lê o arquivo PDB
                var pdbContent = File.ReadAllText(PDB_FILE);
                
                // Procura por padrões específicos no PDB
                FindOffsetInPDB(pdbContent, "HP", "m_nHP", offsets);
                FindOffsetInPDB(pdbContent, "SP", "m_nSP", offsets);
                FindOffsetInPDB(pdbContent, "BaseLevel", "m_nBaseLevel", offsets);
                FindOffsetInPDB(pdbContent, "JobLevel", "m_nJobLevel", offsets);
                FindOffsetInPDB(pdbContent, "Zeny", "m_nZeny", offsets);
                FindOffsetInPDB(pdbContent, "StatusPoint", "m_nStatusPoint", offsets);
                FindOffsetInPDB(pdbContent, "SkillPoint", "m_nSkillPoint", offsets);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao analisar PDB: {ex.Message}");
            }
        }

        private void FindOffsetInPDB(string pdbContent, string statName, string fieldName, Dictionary<string, int> offsets)
        {
            // Procura pelo campo no PDB
            var fieldIndex = pdbContent.IndexOf(fieldName);
            if (fieldIndex != -1)
            {
                // Tenta extrair o offset
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
            // Procura pelo próximo número hexadecimal após o campo
            var start = content.IndexOf("0x", fieldIndex);
            if (start != -1)
            {
                var end = content.IndexOfAny(new[] { ' ', '\n', '\r', '\t' }, start);
                if (end != -1)
                {
                    return content.Substring(start, end - start);
                }
            }
            return string.Empty;
        }

        private void FindOffsetsInMemory(Dictionary<string, int> offsets)
        {
            // Procura por valores conhecidos na memória
            var process = _memoryManager.GetProcess();
            if (process?.MainModule == null) return;

            IntPtr baseAddress = process.MainModule.BaseAddress;
            int size = process.MainModule.ModuleMemorySize;

            // Procura por valores específicos
            FindValueInMemory(baseAddress, size, 99, "BaseLevel", offsets);
            FindValueInMemory(baseAddress, size, 50, "JobLevel", offsets);
            FindValueInMemory(baseAddress, size, 1000, "HP", offsets);
            FindValueInMemory(baseAddress, size, 500, "SP", offsets);
            FindValueInMemory(baseAddress, size, 1000000, "Zeny", offsets);
            FindValueInMemory(baseAddress, size, 0, "StatusPoint", offsets);
            FindValueInMemory(baseAddress, size, 0, "SkillPoint", offsets);
        }

        private void FindValueInMemory(IntPtr baseAddress, int size, int targetValue, string statName, Dictionary<string, int> offsets)
        {
            if (offsets.ContainsKey(statName)) return;

            byte[] targetBytes = BitConverter.GetBytes(targetValue);
            int chunkSize = 0x1000;

            for (IntPtr address = baseAddress; address.ToInt64() < baseAddress.ToInt64() + size; address += chunkSize)
            {
                try
                {
                    byte[] buffer = _memoryManager.ReadMemory(address, chunkSize);
                    for (int i = 0; i < buffer.Length - 4; i++)
                    {
                        if (BitConverter.ToInt32(buffer, i) == targetValue)
                        {
                            int offset = (int)(address.ToInt64() - baseAddress.ToInt64() + i);
                            offsets[statName] = offset;
                            Console.WriteLine($"Offset de {statName} encontrado na memória: 0x{offset:X}");
                            return;
                        }
                    }
                }
                catch
                {
                    // Ignora erros de leitura de memória
                }
            }
        }
    }
} 