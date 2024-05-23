namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.ResponseModels;
public class StoreItemValueSumaryResponseModel
{
    public double MinValue { get; set; }
    public double CurrentMinValue { get; set; }
    public double CurrentMaxValue { get; set; }
    public double Average { get; set; }
    public int StoreNumbers { get; set; }
}
