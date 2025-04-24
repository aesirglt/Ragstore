import { useQuery } from '@tanstack/react-query';
import { MarketItemViewModel } from '@/types/api/viewmodels/MarketItemViewModel';

export interface UseMarketItemsParams {
    server: string;
    page: number;
    pageSize: number;
    itemName?: string;
    category?: string;
    priceOrder?: string;
}

export const useMarketItems = (params: UseMarketItemsParams) => {
    const { server, page, pageSize, itemName, category, priceOrder } = params;
    const queryKey = ['marketItems', server, page, pageSize, itemName, category, priceOrder];

    const query = useQuery<MarketItemViewModel[], Error>({
        queryKey,
        queryFn: async () => {
            try {
                const url = new URL(`/api/${server}/stores-vending/items`, window.location.origin);
                url.searchParams.append('$skip', String((page - 1) * pageSize));
                url.searchParams.append('$top', String(pageSize));
                
                if (itemName) {
                    url.searchParams.append('$filter', `contains(itemName, '${itemName}')`);
                }
                
                if (category) {
                    url.searchParams.append('$filter', `category eq '${category}'`);
                }
                
                if (priceOrder) {
                    url.searchParams.append('$orderby', `price ${priceOrder}`);
                }

                console.log('Fetching from URL:', url.toString());
                const response = await fetch(url.toString());

                if (!response.ok) {
                    const errorText = await response.text();
                    console.error('API Error:', {
                        status: response.status,
                        statusText: response.statusText,
                        body: errorText
                    });
                    throw new Error(`Erro na API: ${response.status} ${response.statusText}`);
                }

                const data = await response.json();
                console.log('API Response:', data);
                return data;
            } catch (error) {
                console.error('Error in useMarketItems:', error);
                throw error;
            }
        },
        retry: 1,
        staleTime: 1000 * 60 * 5, // 5 minutes
        refetchOnWindowFocus: true,
        refetchOnMount: true,
        refetchOnReconnect: true
    });

    return {
        data: query.data || [],
        isLoading: query.isLoading,
        error: query.error
    };
};