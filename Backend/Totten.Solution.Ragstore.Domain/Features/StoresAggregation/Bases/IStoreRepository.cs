namespace Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Bases;
using Totten.Solution.Ragstore.Domain.Bases;

public interface IStoreRepository<TStoreItem> : IRepository<TStoreItem, int>
    where TStoreItem : StoreItem<TStoreItem>
{

}
