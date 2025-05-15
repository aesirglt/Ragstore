import { useQuery } from '@tanstack/react-query';
import { TopItemViewModel } from '@/types/api/viewmodels/TopItemViewModel';
import { PageResult } from '@/types/api/responses/PageResult';

export function useTopItems(server: string, itemIds: number[]) {
  return useQuery<PageResult<TopItemViewModel>>({
    queryKey: ['topItems', server, itemIds],
    queryFn: async () => {
      if (itemIds.length === 0) {
        return { data: [], totalCount: 0 };
      }

      // Busca cada item individualmente
      const items = await Promise.all(
        itemIds.map(async (itemId) => {
          const response = await fetch(`/api/${server}/store-summary/${itemId}`);
          if (!response.ok) {
            throw new Error(`Erro ao buscar item ${itemId}`);
          }
          
          const data = await response.json();
          
          // Transforma os dados no formato esperado
          return {
            itemId: data.itemId?.toString() || "0",
            itemName: data.itemName || "Item padrão",
            average: data.average || 0,
            currentMinValue: data.currentMinValue || 0,
            currentMaxValue: data.currentMaxValue || 0,
            storeNumbers: data.storeNumbers || 0,
            percentageChange: data.percentageChange || 0,
            imageUrl: data.imageUrl || "/items/default.png"
          } as TopItemViewModel;
        })
      );

      return {
        data: items,
        totalCount: items.length
      };
    },
    enabled: itemIds.length > 0,
  });
} 