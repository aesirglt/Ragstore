import { useQuery } from '@tanstack/react-query';

interface MarketItem {
    id: number;
    name: string;
    price: number;
    quantity: number;
    seller: string;
}

interface UseMarketItemsParams {
    server: string;
    page: number;
    pageSize: number;
    itemName?: string;
}

export const useMarketItems = ({ server, page, pageSize, itemName }: UseMarketItemsParams) => {
    console.log('useMarketItems called with:', { server, page, pageSize, itemName });

    const queryKey = ['marketItems', server, page, pageSize, itemName];
    console.log('Query key:', queryKey);

    return useQuery<MarketItem[], Error>({
        queryKey,
        queryFn: async () => {
            try {
                const url = new URL(`/api/${server}/stores-vending/items`, window.location.origin);
                url.searchParams.append('$skip', String((page - 1) * pageSize));
                url.searchParams.append('$top', String(pageSize));
                if (itemName) {
                    url.searchParams.append('$filter', `contains(name, '${itemName}')`);
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
                return data.value || [];
            } catch (error) {
                console.error('Error in useMarketItems:', error);
                throw error;
            }
        },
        retry: 1,
        staleTime: 1000 * 60 * 5, // 5 minutes
    });
};