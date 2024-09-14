namespace Totten.Solution.Ragstore.ApplicationService.Features.Characters.Commands;

using FunctionalConcepts;
using FunctionalConcepts.Results;
using MediatR;

public class CharacterCreateCommand : IRequest<Result<Success>>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int AccountId { get; set; }
    public int JobId { get; set; }
    public int BaseLevel { get; set; }
    public int Sex { get; set; }
    public string Map { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string? PartyName { get; set; }
    public int? GuildId { get; set; }
    public string? GuildName { get; set; }
    public string? GuildPosition { get; set; }
    public int? TitleId { get; set; }
    public string? TitleName { get; set; }
    public int? HairId { get; set; }
    public int? HairColorId { get; set; }
    public int? ClothesColorId { get; set; }
    public int? WeaponId { get; set; }
    public int? ShieldId { get; set; }
    public int? HeadTopId { get; set; }
    public int? HeadMiddleId { get; set; }
    public int? HeadBottomId { get; set; }
    public int? RobeId { get; set; }
}
