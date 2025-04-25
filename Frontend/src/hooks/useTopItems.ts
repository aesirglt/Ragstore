import { useQuery } from '@tanstack/react-query';
import { TopItemViewModel } from '@/types/api/viewmodels/TopItemViewModel';

export function useTopItems(server: string, itens: number[]) {
  return useQuery<TopItemViewModel[]>({
    queryKey: ['topItems', server],
    queryFn: async () => {
      // Construir a URL com múltiplos parâmetros itemId
      const params = new URLSearchParams();
      params.append('storeType', 'vending');
      itens.forEach(id => params.append('itemId', id.toString()));
      
      const response = await fetch(`/api/${server}/store-sumary?${params.toString()}`);
      if (!response.ok) {
        throw new Error('Erro ao buscar top items');
      }
      return response.json();
    },
  });
} 