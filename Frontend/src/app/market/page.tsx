'use client';

import { useState } from 'react';
import { useMarketItems } from '@/hooks/useMarketItems';
import { Pagination } from '@/components/ui/Pagination';
import { ItemCard } from '@/components/ui/ItemCard';
import { SearchInput } from '@/components/ui/SearchInput';

export default function MarketPage() {
    const [currentPage, setCurrentPage] = useState(1);
    const [searchTerm, setSearchTerm] = useState('');
    const pageSize = 20;
    const server = 'brothor';

    const { data, isLoading, error } = useMarketItems({
        server,
        page: currentPage,
        pageSize,
        itemName: searchTerm
    });

    console.log('Market page render:', { data, isLoading, error });

    const handleSearch = (term: string) => {
        console.log('Search term changed:', term);
        setSearchTerm(term);
        setCurrentPage(1);
    };

    if (isLoading) {
        console.log('Loading items...');
        return <div>Carregando...</div>;
    }
    if (error) {
        console.error('Error loading items:', error);
        return <div>Erro ao carregar itens: {error.message}</div>;
    }

    // Ajuste para lidar com a resposta do OData
    const items = Array.isArray(data) ? data : data?.value || [];
    const totalCount = data?.['@odata.count'] || items.length;

    console.log('Page state:', { items, totalCount, currentPage, searchTerm });

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

            {items.length === 0 ? (
                <div className="text-center text-gray-500 py-8">
                    Nenhum item encontrado
                </div>
            ) : (
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4 mb-8">
                    {items.map((item) => (
                        <ItemCard key={item.id} item={item} />
                    ))}
                </div>
            )}

            {totalCount > 0 && (
                <Pagination
                    currentPage={currentPage}
                    totalPages={Math.ceil(totalCount / pageSize)}
                    onPageChange={setCurrentPage}
                />
            )}
        </div>
    );
} 