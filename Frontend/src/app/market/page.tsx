'use client';

import { useState, useEffect } from 'react';
import { useMarketItems } from '@/hooks/useMarketItems';
import { Pagination } from '@/components/ui/Pagination';
import { ItemCard } from '@/components/ui/ItemCard';
import { SearchInput } from '@/components/ui/SearchInput';

export default function MarketPage() {
    const [currentPage, setCurrentPage] = useState(1);
    const [searchTerm, setSearchTerm] = useState('');
    const pageSize = 20;
    const server = 'brothor';

    useEffect(() => {
        console.log('MarketPage - useEffect - valores:', {
            server,
            currentPage,
            pageSize,
            searchTerm
        });
    }, [server, currentPage, pageSize, searchTerm]);

    const marketItemsParams = {
        server,
        page: currentPage,
        pageSize,
        itemName: searchTerm
    };

    console.log('MarketPage - antes de chamar useMarketItems:', marketItemsParams);

    const { data: items, isLoading, error } = useMarketItems(marketItemsParams);

    console.log('Market page render:', { 
        items, 
        isLoading, 
        error,
        itemsType: typeof items,
        isItemsArray: Array.isArray(items),
        itemsLength: items?.length
    });

    const handleSearch = (term: string) => {
        console.log('Search term changed:', term);
        setSearchTerm(term);
        setCurrentPage(1);
    };

    console.log('MarketPage - antes das condições:', { isLoading, error });

    if (isLoading) {
        console.log('Loading items...');
        return <div>Carregando...</div>;
    }

    console.log('MarketPage - após verificação de loading');

    if (error) {
        console.error('Error loading items:', error);
        return <div>Erro ao carregar itens: {error.message}</div>;
    }

    console.log('MarketPage - após verificação de erro');
    console.log('Page state:', { items, currentPage, searchTerm });

    return (
        <div className="container mx-auto px-4 py-8">
            <h1 className="text-3xl font-bold mb-8">Mercado</h1>
            
            <div className="mb-6">
                <SearchInput 
                    value={searchTerm}
                    onChange={handleSearch}
                    placeholder="Buscar itens..."
                />
            </div>

            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4 mb-8">
                {items.map((item) => (
                    <ItemCard key={item.itemId} item={item} />
                ))}
            </div>

            <Pagination
                currentPage={currentPage}
                totalPages={Math.ceil(items.length / pageSize)}
                onPageChange={setCurrentPage}
            />
        </div>
    );
} 