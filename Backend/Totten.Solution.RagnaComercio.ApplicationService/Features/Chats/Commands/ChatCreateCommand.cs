namespace Totten.Solution.RagnaComercio.ApplicationService.Features.Chats.Commands;

using FunctionalConcepts;
using FunctionalConcepts.Results;
using MediatR;

public class ChatCreateCommand : IRequest<Result<Success>>
{
    public int AccountId { get; set; }
    public int CharacterId { get; set; }
    public int Limit { get; set; }
    public int IsPublic { get; set; }
    public string Map { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int QuantityUsers { get; set; }
    public virtual List<EquipmentDto> EquipmentItems { get; set; } = [];

    public class EquipmentDto
    {
        public class ItemCardDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        public class ItemOptionDto
        {
            public int Id { get; set; }
            public int Val { get; set; }
            public int Param { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        public int AccountId { get; set; }
        public int CharacterId { get; set; }
        public int ItemId { get; set; }
        public int ChatId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int Type { get; set; }
        public int Refine { get; set; }
        public int? EnchantGrade { get; set; }
        public int IsIdentified { get; set; }
        public int IsDamaged { get; set; }
        public int? Location { get; set; }
        public int? SpriteId { get; set; }
        public int Slots { get; set; }

        public virtual ItemCardDto[] InfoCards { get; set; } = [];
        public virtual ItemOptionDto[] InfoOptions { get; set; } = [];

        public int? CrafterId { get; set; }
        public string? CrafterName { get; set; }
    }
}
