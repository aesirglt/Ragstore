import { useQuery } from '@tanstack/react-query';
import { LastSearchedItemViewModel } from '@/types/api/viewmodels/LastSearchedItemViewModel';
import { PageResult } from '@/types/api/responses/PageResult';

export function useLastSearchedItems(server: string) {
  return useQuery<PageResult<LastSearchedItemViewModel>>({
    queryKey: ['lastSearchedItems', server],
    queryFn: async () => {
      const response = await fetch(`/api/server/${server}/store-summary/searched-items`);
      if (!response.ok) {
        throw new Error('Erro ao buscar últimos itens pesquisados');
      }
      return response.json();
    },
  });
} 