using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RagnarokController
{
    public class MemoryManager
    {
        private Process _process;
        private IntPtr _processHandle;
        private const string PROCESS_NAME = "Ragexe";

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesWritten);

        private const int PROCESS_VM_READ = 0x0010;
        private const int PROCESS_VM_WRITE = 0x0020;
        private const int PROCESS_VM_OPERATION = 0x0008;

        public Process GetProcess()
        {
            return _process;
        }

        public bool AttachToProcess()
        {
            Process[] processes = Process.GetProcessesByName(PROCESS_NAME);
            if (processes.Length == 0)
                return false;

            _process = processes[0];
            _processHandle = OpenProcess(PROCESS_VM_READ | PROCESS_VM_WRITE | PROCESS_VM_OPERATION, false, _process.Id);
            
            return _processHandle != IntPtr.Zero;
         }

        public byte[] ReadMemory(IntPtr address, int size)
        {
            byte[] buffer = new byte[size];
            ReadProcessMemory(_processHandle, address, buffer, size, out _);
            return buffer;
        }

        public int ReadMemoryInt(IntPtr address)
        {
            byte[] buffer = new byte[4];
            ReadProcessMemory(_processHandle, address, buffer, 4, out _);
            return BitConverter.ToInt32(buffer, 0);
        }

        public void WriteMemory(IntPtr address, byte[] data)
        {
            WriteProcessMemory(_processHandle, address, data, data.Length, out _);
        }

        public void Detach()
        {
            if (_processHandle != IntPtr.Zero)
            {
                _processHandle = IntPtr.Zero;
            }
            _process = null;
        }
    }
} 