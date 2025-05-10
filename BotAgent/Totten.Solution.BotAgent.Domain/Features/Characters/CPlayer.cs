namespace Totten.Solution.BotAgent.Domain.Features.Characters;

using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CPlayer
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0xEEBA6C)]
    public byte[] Padding; // até o offset da força
    public char[] name;
    public char[] map;
    public char[] className;
    public int posY;
    public int posX;
    public int hp;
    public int hpMax;
    public int sp;
    public int spMax;
    public int level;
    public int classLevel;
    public int zeny;
    public int weight;
}
