namespace Totten.Solution.Ragstore.Infra.Data.Features.Stores;

using MongoDB.Driver;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.Domain.Features.Stores;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class StoreRepository : IStoreRepository
{
    private readonly IMongoCollection<Store> _collection;
    public StoreRepository(IMongoDatabase mongoDatabase, string collectionName)
    {
        _collection = mongoDatabase.GetCollection<Store>(collectionName);
    }

    public Task<Store> GetById(Guid id)
    {
        return _collection.Find(x => x.Id == id).FirstAsync();
    }

    public Task<List<Store>> GetAll()
    {
        return _collection.Find(_ => true).ToListAsync();
    }

    public async Task<Unit> Save(Store store)
    {
        await _collection.InsertOneAsync(store);
        return new Unit();
    }
    public async Task<Unit> SaveBatch(IQueryable<Store> stores)
    {
        await _collection.InsertManyAsync(stores);
        return new Unit();
    }

    public async Task UpdateAsync(Guid id, Store store) =>
        await _collection.ReplaceOneAsync(x => x.Id == id, store);

    public async Task RemoveAsync(Guid id) =>
        await _collection.DeleteOneAsync(x => x.Id == id);
}
