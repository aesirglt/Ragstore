namespace Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.ResponseModels;
public class StoreItemValueSumaryResponseModel
{
    public double MinValue { get; set; }
    public double MaxValue { get; set; }
    public double CurrentMinValue { get; set; }
    public double CurrentMaxValue { get; set; }
    public double Average { get; set; }
    public long StoreNumbers { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}
