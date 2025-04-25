import { useQuery } from '@tanstack/react-query';
import { TopItemViewModel } from '@/types/api/viewmodels/TopItemViewModel';
import { GetTopItemsRequest } from '@/types/api/requests/GetTopItemsRequest';

export function useTopItems(server: string) {
  return useQuery<TopItemViewModel[]>({
    queryKey: ['topItems', server],
    queryFn: async () => {
      const request: GetTopItemsRequest = { server };
      const response = await fetch(`/api/${request.server}/items-top`);
      if (!response.ok) {
        throw new Error('Erro ao buscar top items');
      }
      return response.json();
    },
  });
} 