import { ItemResponse } from "@/types/api";
import { useQuery } from '@tanstack/react-query';

export function useMarketItems(server: string) {
    return useQuery<ItemResponse>({
      queryKey: ['useMarketItems', server],
      queryFn: async () => {
        const response = await fetch(`api/${server}/stores-vending/items`);
        if (!response.ok) {
          throw new Error('Erro ao buscar marcadores do usuário');
        }
        return response.json();
      },
      enabled: !!server,
    });
  }