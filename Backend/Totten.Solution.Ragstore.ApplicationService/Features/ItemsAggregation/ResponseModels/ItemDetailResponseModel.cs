namespace Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.ResponseModels;
public record ItemDetailResponseModel
{
    public int Id { get; set; }
    public string AegisName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Setname { get; set; } = string.Empty;
    public string ItemUrlImage { get; set; } = string.Empty;

    public int? Slots { get; set; }
    public int? ItemTypeId { get; set; }
    public int? ItemSubTypeId { get; set; }
    public int? Attack { get; set; }
    public int? Defense { get; set; }
    public double Weight { get; set; }
    public int? RequiredLevel { get; set; }
    public int? LimitLevel { get; set; }
    public int? ItemLevel { get; set; }
    public int? Equip { get; set; }
    public bool? HasScript { get; set; }
}
