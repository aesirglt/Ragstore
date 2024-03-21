namespace Totten.Solution.Ragstore.Infra.Data.Features.StoreAgregattion;

using MongoDB.Driver;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Vendings;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class StoreRepository : IStoreRepository
{
    private readonly IMongoCollection<VendingStore> _collection;
    public StoreRepository(IMongoDatabase mongoDatabase, string collectionName)
    {
        _collection = mongoDatabase.GetCollection<VendingStore>(collectionName);
    }

    public Task<VendingStore> GetById(Guid id)
    {
        return _collection.Find(x => x.Id == id).FirstAsync();
    }

    public Task<List<VendingStore>> GetAll()
    {
        return _collection.Find(_ => true).ToListAsync();
    }

    public async Task<Unit> Save(VendingStore store)
    {
        await _collection.InsertOneAsync(store);
        return new Unit();
    }
    public async Task<Unit> SaveBatch(IQueryable<VendingStore> stores)
    {
        await _collection.InsertManyAsync(stores);
        return new Unit();
    }

    public async Task UpdateAsync(Guid id, VendingStore store) =>
        await _collection.ReplaceOneAsync(x => x.Id == id, store);

    public async Task RemoveAsync(Guid id) =>
        await _collection.DeleteOneAsync(x => x.Id == id);
}
