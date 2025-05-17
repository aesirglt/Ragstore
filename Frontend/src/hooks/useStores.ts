import { useQuery } from '@tanstack/react-query';
import { PageResult } from '@/types/api/responses/PageResult';

interface Store {
  id: number;
  accountId: number;
  characterId: number;
  characterName: string;
  name: string;
  location: string;
  itemPrice: number;
  quantity: number;
  storeType: string;
}

interface UseStoresParams {
  server: string;
  itemId: number;
  page: number;
  pageSize: number;
}

export function useStores({ server, itemId, page, pageSize }: UseStoresParams) {
  return useQuery<PageResult<Store>>({
    queryKey: ['stores', server, itemId, page],
    queryFn: async () => {
      const url = new URL(`/api/server/${server}/stores`, window.location.origin);
      url.searchParams.append('storeType', 'VendingStore');
      url.searchParams.append('$skip', String((page - 1) * pageSize));
      url.searchParams.append('$top', String(pageSize));
      url.searchParams.append('$filter', `itemId eq ${itemId}`);
      console.log('Fetching from URL:', url.toString());
      const response = await fetch(url.toString());

      if (!response.ok) {
        throw new Error('Erro ao buscar lojinhas');
      }
      var resp = await response.json();
      console.log(resp);

      return resp;
    },
    enabled: !!itemId,
  });
} 