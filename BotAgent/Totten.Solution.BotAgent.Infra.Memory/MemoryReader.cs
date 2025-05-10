namespace Totten.Solution.BotAgent.Infra.Memory;
using System;
using System.Runtime.InteropServices;
using System.Text;
using Totten.Solution.BotAgent.Domain.Base;

public class MemoryReader : IMemoryReader
{
    public int ReadInt32(IntPtr processHandle, IntPtr address)
        => BitConverter.ToInt32(ReadBytes(processHandle, address, 4), 0);

    public long ReadInt64(IntPtr processHandle, IntPtr address)
        => BitConverter.ToInt64(ReadBytes(processHandle, address, 8), 0);

    public float ReadFloat(IntPtr processHandle, IntPtr address)
        => BitConverter.ToSingle(ReadBytes(processHandle, address, 4), 0);

    public double ReadDouble(IntPtr processHandle, IntPtr address)
        => BitConverter.ToDouble(ReadBytes(processHandle, address, 8), 0);

    public bool ReadBool(IntPtr processHandle, IntPtr address)
        => BitConverter.ToBoolean(ReadBytes(processHandle, address, 1), 0);

    public byte ReadByte(IntPtr processHandle, IntPtr address)
        => ReadBytes(processHandle, address, 1)[0];

    public string ReadString(IntPtr processHandle, IntPtr address, int maxLength, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;
        byte[] buffer = ReadBytes(processHandle, address, maxLength);
        int nullIndex = Array.IndexOf(buffer, (byte)0);
        if (nullIndex >= 0)
            Array.Resize(ref buffer, nullIndex);
        return encoding.GetString(buffer);
    }

    public char[] ReadCharArray(IntPtr processHandle, IntPtr address, int length)
    {
        byte[] bytes = ReadBytes(processHandle, address, length * 2);
        char[] chars = new char[length];
        Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
        return chars;
    }

    public T ReadStruct<T>(IntPtr processHandle, IntPtr address) where T : struct
    {
        int size = Marshal.SizeOf<T>();
        byte[] buffer = ReadBytes(processHandle, address, size);
        GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
        T result = Marshal.PtrToStructure<T>(handle.AddrOfPinnedObject());
        handle.Free();
        return result;
    }

    public byte[] ReadBytes(IntPtr processHandle, IntPtr address, int size)
    {
        byte[] buffer = new byte[size];
        ReadProcessMemory(processHandle, address, buffer, size, out _);
        return buffer;
    }

    #region WinAPI

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(ProcessAccessFlags access, bool inheritHandle, int processId);

    [DllImport("kernel32.dll")]
    private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, int size, out IntPtr bytesRead);

    [Flags]
    public enum ProcessAccessFlags : uint
    {
        All = 0x1F0FFF
    }

    #endregion
}
