import { useQuery } from '@tanstack/react-query';
import { LastSearchedItemViewModel } from '@/types/api/viewmodels/LastSearchedItemViewModel';

export function useLastSearchedItems(server: string) {
  return useQuery<LastSearchedItemViewModel[]>({
    queryKey: ['lastSearchedItems', server],
    queryFn: async () => {
      const response = await fetch(`/api/${server}/store-sumary/searched-items`);
      if (!response.ok) {
        throw new Error('Erro ao buscar últimos itens pesquisados');
      }
      return response.json();
    },
  });
} 