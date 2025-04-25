import { useQuery } from '@tanstack/react-query';

interface Store {
  id: number;
  accountId: number;
  characterId: number;
  name: string;
  characterName: string;
  map: string;
  location: string;
}

interface UseStoresParams {
  server: string;
  itemId: number;
  page: number;
  pageSize: number;
}

export function useStores({ server, itemId, page, pageSize }: UseStoresParams) {
  return useQuery<Store[]>({
    queryKey: ['stores', server, itemId, page],
    queryFn: async () => {
      const response = await fetch(
        `/api/${server}/stores?itemId=${itemId}$top=${pageSize}&$skip=${(page - 1) * pageSize}`
      );
      if (!response.ok) {
        throw new Error('Erro ao buscar lojinhas');
      }
      return response.json();
    },
    enabled: !!itemId,
  });
} 