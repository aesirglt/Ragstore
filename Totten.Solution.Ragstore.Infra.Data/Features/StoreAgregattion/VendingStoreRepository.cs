namespace Totten.Solution.Ragstore.Infra.Data.Features.StoreAgregattion;

using MongoDB.Driver;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Vendings;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class VendingStoreRepository : IVendingStoreRepository
{
    private readonly IMongoCollection<VendingStore> _collection;
    public VendingStoreRepository(IMongoDatabase mongoDatabase, string collectionName)
    {
        _collection = mongoDatabase.GetCollection<VendingStore>(collectionName);
    }

    public Task<List<VendingStore>> GetAll()
    {
        return _collection.Find(_ => true).ToListAsync();
    }

    public Task<VendingStore> GetById(Guid id)
        => _collection.Find(x => x.Id == id).FirstAsync();

    public Task<List<VendingStore>> GetAllByFilter(Expression<Func<VendingStore, bool>> filter)
        => _collection.Find(filter).ToListAsync();

    public async Task<Unit> Save(VendingStore store)
    {
        try
        {
            await _collection.InsertOneAsync(store);
        }
        catch(Exception ex)
        {
            return new Unit();
        }
        return new Unit();
    }

    public async Task<Unit> SaveBatch(IQueryable<VendingStore> stores)
    {
        await _collection.InsertManyAsync(stores);
        return new Unit();
    }

    public async Task<Unit> Update(VendingStore entity)
    {
        await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        return new Unit();
    }

    public async Task<Unit> Remove(VendingStore entity)
    {
        await _collection.DeleteOneAsync(x => x.Id == entity.Id);
        return new Unit();
    }
}
