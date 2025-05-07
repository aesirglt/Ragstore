namespace Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Bases;
using Totten.Solution.RagnaComercio.Domain.Bases;

public interface IStoreRepository<TStoreItem> : IRepository<TStoreItem, int>
    where TStoreItem : StoreItem<TStoreItem>
{

}
