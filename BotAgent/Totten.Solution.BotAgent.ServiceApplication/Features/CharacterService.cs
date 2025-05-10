namespace Totten.Solution.BotAgent.ServiceApplication.Features;
using System.Diagnostics;
using Totten.Solution.BotAgent.Domain.Base;
using Totten.Solution.BotAgent.Domain.Features;
using Totten.Solution.BotAgent.Domain.Features.Characters;

public class CharacterService(IMemoryReader memoryReader) : ICharacterService
{
    public CPlayer GetCharacter(Process process)
        => process.MainModule is not null
            ? memoryReader.ReadStruct<CPlayer>(process.Handle, process.MainModule.BaseAddress + Pointers.CPlayer)
            : new CPlayer();

    public int GetX(Process process)
        => memoryReader.ReadInt32(process.Handle, process.MainModule.BaseAddress + Pointers.PosX);
    public int GetY(Process process)
        => memoryReader.ReadInt32(process.Handle, process.MainModule.BaseAddress + Pointers.PosY);
    public int GetSp(Process process)
        => memoryReader.ReadInt32(process.Handle, process.MainModule.BaseAddress + Pointers.Sp);
    public int GetSpMax(Process process)
        => memoryReader.ReadInt32(process.Handle, process.MainModule.BaseAddress + Pointers.SpMax);
}
