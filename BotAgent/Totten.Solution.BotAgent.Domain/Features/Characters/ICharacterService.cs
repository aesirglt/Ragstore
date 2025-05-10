namespace Totten.Solution.BotAgent.Domain.Features.Characters;
using System.Diagnostics;

public interface ICharacterService
{
    CPlayer GetCharacter(Process process);
    int GetSp(Process process);
    int GetSpMax(Process process);
    int GetX(Process process);
    int GetY(Process process);
}
