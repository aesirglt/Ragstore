namespace Totten.Solution.BotAgent.Domain.Features.Sessions;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CSession
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0xED8330)]
    public byte[] Padding; 
    public int spMax;
}
