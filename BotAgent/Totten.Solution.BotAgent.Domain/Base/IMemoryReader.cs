namespace Totten.Solution.BotAgent.Domain.Base;
using System;
using System.Text;

public interface IMemoryReader
{
    public int ReadInt32(IntPtr processHandle, IntPtr address);
    public long ReadInt64(IntPtr processHandle, IntPtr address);
    public float ReadFloat(IntPtr processHandle, IntPtr address);
    public double ReadDouble(IntPtr processHandle, IntPtr address);
    public bool ReadBool(IntPtr processHandle, IntPtr address);
    public byte ReadByte(IntPtr processHandle, IntPtr address);
    public string ReadString(IntPtr processHandle, IntPtr address, int maxLength, Encoding? encoding = null);
    public char[] ReadCharArray(IntPtr processHandle, IntPtr address, int length);
    public T ReadStruct<T>(IntPtr processHandle, IntPtr address) where T : struct;
    public byte[] ReadBytes(IntPtr processHandle, IntPtr address, int size);
}
