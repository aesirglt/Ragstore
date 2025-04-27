namespace StoreAgent.WinApp.Infra;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class MemoryManager
{
    private Process? _process;
    private IntPtr _processHandle;
    private const string PROCESS_NAME = "Ragexe";
    private const uint PROCESS_ALL_ACCESS = 0x001F0FFF;

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

    [DllImport("kernel32.dll")]
    private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesWritten);

    [DllImport("kernel32.dll")]
    private static extern bool CloseHandle(IntPtr hObject);

    public Process? GetProcess()
    {
        return _process;
    }

    public bool AttachToProcess()
    {
        Process[] processes = Process.GetProcessesByName(PROCESS_NAME);
        if (processes.Length == 0)
        {
            Console.WriteLine("Processo Ragexe.exe não encontrado.");
            return false;
        }

        _process = processes[0];
        _processHandle = OpenProcess(PROCESS_ALL_ACCESS, false, _process.Id);

        if (_processHandle == IntPtr.Zero)
        {
            Console.WriteLine("Falha ao abrir o processo.");
            _process = null;
            return false;
        }

        return true;
    }

    public T ReadMemory<T>(IntPtr address) where T : struct
    {
        int size = Marshal.SizeOf<T>();
        byte[] buffer = new byte[size];
        int bytesRead;

        if (ReadProcessMemory(_processHandle, address, buffer, size, out bytesRead) && bytesRead == size)
        {
            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                return Marshal.PtrToStructure<T>(handle.AddrOfPinnedObject());
            }
            finally
            {
                handle.Free();
            }
        }

        return default(T);
    }

    public byte[] ReadMemory(IntPtr address, int size)
    {
        byte[] buffer = new byte[size];
        int bytesRead;

        if (ReadProcessMemory(_processHandle, address, buffer, size, out bytesRead) && bytesRead == size)
        {
            return buffer;
        }

        throw new Exception($"Falha ao ler memória no endereço 0x{address.ToString("X")}");
    }

    public bool WriteMemory<T>(IntPtr address, T value) where T : struct
    {
        int size = Marshal.SizeOf<T>();
        byte[] buffer = new byte[size];
        int bytesWritten;

        GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
        try
        {
            Marshal.StructureToPtr(value, handle.AddrOfPinnedObject(), false);
            return WriteProcessMemory(_processHandle, address, buffer, size, out bytesWritten) && bytesWritten == size;
        }
        finally
        {
            handle.Free();
        }
    }

    public bool ReadProcessMemory(IntPtr address, byte[] buffer, int size, out int bytesRead)
    {
        return ReadProcessMemory(_processHandle, address, buffer, size, out bytesRead);
    }

    public void Detach()
    {
        if (_processHandle != IntPtr.Zero)
        {
            CloseHandle(_processHandle);
            _processHandle = IntPtr.Zero;
        }
        _process = null;
    }
}
