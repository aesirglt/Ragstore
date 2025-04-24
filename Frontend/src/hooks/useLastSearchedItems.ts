import { useQuery } from '@tanstack/react-query';
import { LastSearchedItemViewModel } from '@/types/api/viewmodels/LastSearchedItemViewModel';
import { GetLastSearchedItemsRequest } from '@/types/api/requests/GetLastSearchedItemsRequest';

export function useLastSearchedItems(server: string) {
  return useQuery<LastSearchedItemViewModel[]>({
    queryKey: ['lastSearchedItems', server],
    queryFn: async () => {
      const request: GetLastSearchedItemsRequest = { server };
      const response = await fetch(`/${request.server}/items-top-search`);
      if (!response.ok) {
        throw new Error('Erro ao buscar últimos itens pesquisados');
      }
      return response.json();
    },
  });
} 