import { ItemResponse } from '@/types/api/responses/ItemResponse';
import { GetMarketItemsRequest } from '@/types/api/requests/GetMarketItemsRequest';
import { useQuery } from '@tanstack/react-query';

export function useMarketItems({ server, page, pageSize, itemName }: GetMarketItemsRequest) {
    return useQuery<ItemResponse>({
        queryKey: ['useMarketItems', server, page, pageSize, itemName],
        queryFn: async () => {
            try {
                const skip = (page - 1) * pageSize;
                const top = pageSize;
                const filter = itemName ? `&$filter=contains(itemName,'${itemName}')` : '';
                const url = `http://localhost:53766/${server}/stores-vending/items?$skip=${skip}&$top=${top}${filter}`;
                
                console.log('Fetching items from:', url);
                
                const response = await fetch(url, {
                    method: 'GET',
                    headers: {
                        'accept': 'application/json;odata.metadata=minimal;odata.streaming=true',
                        'Content-Type': 'application/json'
                    }
                });
                
                console.log('Response status:', response.status);
                
                if (!response.ok) {
                    const errorText = await response.text();
                    console.error('Error response:', errorText);
                    throw new Error(`Erro ao buscar itens do mercado: ${response.status} ${errorText}`);
                }
                
                const data = await response.json();
                console.log('Response data:', data);
                return data;
            } catch (error) {
                console.error('Error in useMarketItems:', error);
                throw error;
            }
        },
        enabled: !!server,
    });
}