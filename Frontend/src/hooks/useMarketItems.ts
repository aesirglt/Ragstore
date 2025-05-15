import { useQuery } from '@tanstack/react-query';
import { MarketItemViewModel } from '@/types/api/viewmodels/MarketItemViewModel';
import { PageResult } from '@/types/api/responses/PageResult';

export interface UseMarketItemsParams {
    server: string;
    page: number;
    pageSize: number;
    itemName?: string;
    category?: string;
    storeType?: string;
}

export const useMarketItems = (params: UseMarketItemsParams) => {
    const { server, page, pageSize, itemName, category, storeType } = params;
    const queryKey = ['marketItems', server, page, pageSize, itemName, category, storeType];

    const query = useQuery<PageResult<MarketItemViewModel>, Error>({
        queryKey,
        queryFn: async () => {
            try {
                const url = new URL(`/api/${server}/store-items`, window.location.origin);
                
                url.searchParams.append('$skip', String((page - 1) * pageSize));
                url.searchParams.append('$top', String(pageSize));
                url.searchParams.append('storeType', storeType ?? 'VendingStore');
                
                const filters = [];

                if (itemName) {
                    filters.push(`contains(itemName, '${itemName}')`);
                }
                
                if (category) {
                    const categories = category.split(',');
                    if (categories.length > 0) {
                        const categoryFilter = categories.map(cat => `category eq '${cat}'`).join(' or ');
                        filters.push(categoryFilter);
                    }
                }

                if (filters.length > 0) {
                    url.searchParams.append('$filter', filters.join(' and '));
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
        data: query.data || { data: [], totalCount: 0 },
        isLoading: query.isLoading,
        error: query.error
    };
};