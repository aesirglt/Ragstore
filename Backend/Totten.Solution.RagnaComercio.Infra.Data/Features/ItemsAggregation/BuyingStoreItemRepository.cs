﻿namespace Totten.Solution.RagnaComercio.Infra.Data.Features.ItemsAggregation;

using FunctionalConcepts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.RagnaComercio.Infra.Data.Bases;
using Totten.Solution.RagnaComercio.Infra.Data.Contexts.StoreServerContext;
using Item = string;
using StoreId = int;

public class BuyingStoreItemRepository(ServerStoreContext context)
    : RepositoryBase<BuyingStoreItem, int>(context), IBuyingStoreItemRepository
{
    public async Task<Success> DeleteAll(StoreId id)
    {
        var stores = await _context
            .Set<BuyingStoreItem>()
            .Where(x => x.StoreId == id)
            .AsNoTracking()
            .ToArrayAsync();

        _context.RemoveRange(stores);

        await _context.SaveChangesAsync();

        return default;
    }

    public IQueryable<BuyingStoreItem> GetAllByCharacterId(int id)
        => _context
            .Set<BuyingStoreItem>()
           .Where(item => item.CharacterId == id)
           .AsNoTracking();

    public IQueryable<BuyingStoreItem> GetAllByItemName(Item name)
        => _context
            .Set<BuyingStoreItem>()
           .Where(item => item.Name != null && item.Name.Contains(name))
           .AsNoTracking();
}