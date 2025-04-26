using System;
using System.Collections.Generic;

namespace RagnarokController
{
    public class ShopManager
    {
        private readonly MemoryManager _memoryManager;
        private const int SHOP_DIALOG_OFFSET = 0x99C008; // Endereço base para diálogos de loja
        private const int SHOP_ITEMS_OFFSET = 0x99C00C; // Endereço base para itens da loja

        public ShopManager(MemoryManager memoryManager)
        {
            _memoryManager = memoryManager;
        }

        public void OpenShop(int shopId)
        {
            // Simula a abertura de uma loja
            byte[] shopIdBytes = BitConverter.GetBytes(shopId);
            _memoryManager.WriteMemory(new IntPtr(SHOP_DIALOG_OFFSET), shopIdBytes);
        }

        public List<int> GetShopItems(int shopId)
        {
            List<int> items = new List<int>();
            
            // Lê os itens da loja
            byte[] itemsData = _memoryManager.ReadMemory(new IntPtr(SHOP_ITEMS_OFFSET), 100); // Ajuste o tamanho conforme necessário
            
            for (int i = 0; i < itemsData.Length; i += 4)
            {
                if (i + 4 <= itemsData.Length)
                {
                    int itemId = BitConverter.ToInt32(itemsData, i);
                    if (itemId != 0)
                    {
                        items.Add(itemId);
                    }
                }
            }

            return items;
        }

        public void CloseShop()
        {
            // Fecha a loja atual
            byte[] closeBytes = BitConverter.GetBytes(0);
            _memoryManager.WriteMemory(new IntPtr(SHOP_DIALOG_OFFSET), closeBytes);
        }
    }
} 