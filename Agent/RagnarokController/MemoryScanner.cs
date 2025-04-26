using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RagnarokController
{
    public class MemoryScanner
    {
        private readonly MemoryManager _memoryManager;
        private const int SCAN_CHUNK_SIZE = 0x1000; // Tamanho do chunk para escanear

        public MemoryScanner(MemoryManager memoryManager)
        {
            _memoryManager = memoryManager;
        }

        public MemoryManager GetMemoryManager()
        {
            return _memoryManager;
        }

        public IntPtr FindPattern(byte[] pattern, string mask)
        {
            // Obtém o módulo principal do processo
            ProcessModule mainModule = _memoryManager.GetProcess().MainModule;
            IntPtr baseAddress = mainModule.BaseAddress;
            int size = mainModule.ModuleMemorySize;

            // Escaneia a memória em chunks
            for (IntPtr currentAddress = baseAddress; 
                 currentAddress.ToInt64() < baseAddress.ToInt64() + size; 
                 currentAddress = IntPtr.Add(currentAddress, SCAN_CHUNK_SIZE))
            {
                try
                {
                    byte[] buffer = _memoryManager.ReadMemory(currentAddress, SCAN_CHUNK_SIZE);
                    
                    for (int i = 0; i < buffer.Length - pattern.Length; i++)
                    {
                        bool found = true;
                        for (int j = 0; j < pattern.Length; j++)
                        {
                            if (mask[j] == 'x' && buffer[i + j] != pattern[j])
                            {
                                found = false;
                                break;
                            }
                        }

                        if (found)
                        {
                            return IntPtr.Add(currentAddress, i);
                        }
                    }
                }
                catch
                {
                    // Ignora erros de leitura de memória
                    continue;
                }
            }

            return IntPtr.Zero;
        }

        public IntPtr FindValue(int value)
        {
            byte[] valueBytes = BitConverter.GetBytes(value);
            return FindPattern(valueBytes, "xxxx");
        }

        public IntPtr FindString(string str)
        {
            byte[] strBytes = System.Text.Encoding.ASCII.GetBytes(str);
            string mask = new string('x', strBytes.Length);
            return FindPattern(strBytes, mask);
        }
    }
} 