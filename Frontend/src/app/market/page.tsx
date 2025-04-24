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

    const handleSearch = (term: string) => {
        setSearchTerm(term);
        setCurrentPage(1);
    };

    if (isLoading) return <div>Carregando...</div>;
    if (error) return <div>Erro ao carregar itens: {error.message}</div>;

    const items = data?.value || [];
    const totalCount = data?.['@odata.count'] || 0;

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
                <>
                    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4 mb-8">
                        {items.map((item) => (
                            <ItemCard key={item.id} item={item} />
                        ))}
                    </div>

                    <Pagination
                        currentPage={currentPage}
                        totalPages={Math.ceil(totalCount / pageSize)}
                        onPageChange={setCurrentPage}
                    />
                </>
            )}
        </div>
    );
} 