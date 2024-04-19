namespace Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.ResponseModels;

using Totten.Solution.Ragstore.Domain.Bases;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Bases;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;

public class StoreItemResponseModel

{
    public int Id { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public string Map { get; set; } = string.Empty;
    public string StoreName { get; set; } = string.Empty;
    public string VendingType { get; set; } = string.Empty;
    public string CharacterName { get; set; } = string.Empty;

    public void SetData<TStore, TStoreItem>(
            string characterName,
            Store<TStore> store,
            StoreItem<TStoreItem> vendingStoreItem
        )
        where TStore : Entity<TStore, int>
        where TStoreItem : Entity<TStoreItem, int>
    {
        this.Id = vendingStoreItem.Id;
        this.Price = vendingStoreItem.Price;
        this.Quantity = vendingStoreItem.Quantity;
        this.Map = store.Location;
        this.StoreName = store.Name ?? "not informed";
        this.VendingType = nameof(TStore);
        this.CharacterName = characterName;
    }
}
