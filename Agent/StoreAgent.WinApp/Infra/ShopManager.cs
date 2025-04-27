namespace StoreAgent.WinApp.Infra;
using System;
using System.Collections.Generic;

public class ShopManager
{
    private readonly MemoryManager _memoryManager;
    private readonly IntPtr _shopBaseAddress;

    public ShopManager(MemoryManager memoryManager, IntPtr shopBaseAddress)
    {
        _memoryManager = memoryManager;
        _shopBaseAddress = shopBaseAddress;
    }

    public void OpenShop(int shopId)
    {
        _memoryManager.WriteMemory(_shopBaseAddress + 0x99C008, shopId);
    }

    public List<int> GetShopItems()
    {
        List<int> items = new List<int>();
        int itemCount = _memoryManager.ReadMemory<int>(_shopBaseAddress + 0x99C00C);

        for (int i = 0; i < itemCount; i++)
        {
            int itemId = _memoryManager.ReadMemory<int>(_shopBaseAddress + 0x99C010 + ( i * 4 ));
            if (itemId != 0)
            {
                items.Add(itemId);
            }
        }

        return items;
    }

    public void CloseShop()
    {
        _memoryManager.WriteMemory(_shopBaseAddress + 0x99C008, 0);
    }
}
