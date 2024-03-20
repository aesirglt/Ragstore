namespace Totten.Solution.Ragstore.Domain.Features.Chats;

using Totten.Solution.Ragstore.Domain.Bases;
using Totten.Solution.Ragstore.Domain.Features.Accounts;
using Totten.Solution.Ragstore.Domain.Features.Characters;
using Totten.Solution.Ragstore.Domain.Features.Items;

public class Chat : Entity<Chat>
{
    public int AccountId { get; set; }
    public int CharacterId { get; set; }
    public int Users { get; set; }
    public int Limit { get; set; }
    public int IsPublic { get; set; }
    public string Map { get; set; }
    public string Location { get; set; }
    public Account Account { get; set; }
    public Character character { get; set; }
    public List<EquipmentItem> EquipmentItems { get; set; }

}