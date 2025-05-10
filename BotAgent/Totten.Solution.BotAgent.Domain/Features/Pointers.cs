namespace Totten.Solution.BotAgent.Domain.Features;
using System;

public static class Pointers
{
    public static nint CGameMode => 0x00;
    public static nint CGameObject => 0x00;
    public static nint CMouse => 0x00;
    public static nint CMousePointer => 0x00;
    public static nint CPc => 0x00;
    public static nint CPlayer => 0x00;
    public static nint CActor => 0x00;
    public static nint CWorld => 0x00;
    public static nint UIMerchantShopTitle => 0x00;
    public static nint PosY => 0xED8330;
    public static nint PosX => PosY - 4;
    public static nint Sp => 0xEEF640;
    public static nint SpMax => Sp + 4;
}
