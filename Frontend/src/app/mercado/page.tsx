'use client';

import { useEffect, useState, useRef } from 'react';
import { 
  Container, 
  Box, 
  Alert, 
  AlertIcon, 
  VStack, 
  SimpleGrid,
  useColorModeValue,
} from '@chakra-ui/react';
import { useMarketItems, UseMarketItemsParams } from '@/hooks/useMarketItems';
import { SearchBar } from '@/components/ui/SearchBar';
import { MarketFilters } from '@/components/ui/MarketFilters';
import { MarketPagination } from '@/components/ui/MarketPagination';
import { StoreListModal } from '../components/StoreListModal';
import { MarketItem } from '../components/MarketItem';
import { useServer } from '../../contexts/ServerContext';
import { LoadingList } from '@/components/LoadingList';
import { MarketItemViewModel } from '@/types/api/viewmodels/MarketItemViewModel';
import { PageResult } from '@/types/api/responses/PageResult';

export default function MercadoPage() {
  const { currentServer } = useServer();
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [searchTerm, setSearchTerm] = useState('');
  const [debouncedSearchTerm, setDebouncedSearchTerm] = useState('');
  const [selectedCategories, setSelectedCategories] = useState<string[]>([]);
  const [storeType, setStoreType] = useState('VendingStore');
  const [selectedItemId, setSelectedItemId] = useState<number | null>(null);
  const [isStoreModalOpen, setIsStoreModalOpen] = useState(false);
  const [selectedSort, setSelectedSort] = useState<string>('price_asc');
  const containerRef = useRef<HTMLDivElement>(null);

  // Dinamicamente calcula colunas e linhas
  const [gridConfig, setGridConfig] = useState({ columns: 10, rows: 3, itemsPerPage: 10 });

  const bgColor = useColorModeValue('white', 'gray.900');
  const containerBg = useColorModeValue('gray.50', 'gray.800');

  // Efeito para debounce da pesquisa
  useEffect(() => {
    const timer = setTimeout(() => {
      setDebouncedSearchTerm(searchTerm);
      if (typeof window !== 'undefined') {
        localStorage.setItem('marketSearchTerm', searchTerm);
      }
    }, 1000);

    return () => clearTimeout(timer);
  }, [searchTerm]);

  // Efeito para limpar o localStorage quando o componente é desmontado
  useEffect(() => {
    return () => {
      if (typeof window !== 'undefined') {
        localStorage.removeItem('marketSearchTerm');
      }
    };
  }, []);

  // Efeito para resetar a página quando os filtros mudarem
  useEffect(() => {
    setCurrentPage(1);
  }, [selectedCategories, storeType]);

  useEffect(() => {
    function updateGridConfig() {
      if (!containerRef.current) return;
      const width = containerRef.current.offsetWidth;
      const height = window.innerHeight - containerRef.current.getBoundingClientRect().top - 160; // 160px para filtros, paginação e rodapé
      const itemWidth = 180; // largura estimada do card
      const itemHeight = 140; // altura estimada do card
      const columns = Math.max(1, Math.floor(width / itemWidth));
      const rows = Math.max(1, Math.floor(height / itemHeight));
      setGridConfig({ columns, rows, itemsPerPage: columns * rows });
    }
    updateGridConfig();
    window.addEventListener('resize', updateGridConfig);
    return () => window.removeEventListener('resize', updateGridConfig);
  }, []);

  const ITEMS_PER_PAGE = gridConfig.itemsPerPage;

  const marketParams: UseMarketItemsParams = {
    serverId: currentServer?.id ?? '',
    page: currentPage,
    pageSize: ITEMS_PER_PAGE,
    itemName: debouncedSearchTerm,
    category: selectedCategories.join(','),
    storeType: storeType
  };

  const { data: items = { data: [], totalCount: 0 } as PageResult<MarketItemViewModel>, isLoading, error } = useMarketItems(marketParams);

  useEffect(() => {
    if (items?.totalCount) {
      setTotalPages(Math.ceil(items.totalCount / ITEMS_PER_PAGE));
    }
  }, [items, ITEMS_PER_PAGE]);

  const handlePageChange = (newPage: number) => {
    setCurrentPage(newPage);
  };

  const handleSearch = (term: string) => {
    setSearchTerm(term);
    setCurrentPage(1);
  };

  const handleItemClick = (itemId: number) => {
    setSelectedItemId(itemId);
    setIsStoreModalOpen(true);
  };

  if (isLoading) {
    return (
      <Box height="100vh" display="flex" flexDirection="column" bg={containerBg}>
        <Container maxW="container.xl" py={4}>
          <VStack spacing={4} align="stretch">
            <SearchBar value={searchTerm} onChange={handleSearch} />
            <MarketFilters
              selectedCategory={selectedCategories}
              storeType={storeType}
              onCategoryChange={setSelectedCategories}
              onStoreTypeChange={setStoreType}
            />
            <Box 
              flex="1" 
              display="flex" 
              alignItems="center" 
              justifyContent="center"
              minH="400px"
            >
              <LoadingList />
            </Box>
          </VStack>
        </Container>
      </Box>
    );
  }

  if (error) {
    return (
      <Box height="100vh" display="flex" flexDirection="column" bg={containerBg}>
        <Container maxW="container.xl" py={4}>
          <VStack spacing={4} align="stretch">
            <SearchBar value={searchTerm} onChange={handleSearch} />
            <MarketFilters
              selectedCategory={selectedCategories}
              storeType={storeType}
              onCategoryChange={setSelectedCategories}
              onStoreTypeChange={setStoreType}
            />
            <Alert status="error" mt={4}>
              <AlertIcon />
              {error.message}
            </Alert>
          </VStack>
        </Container>
      </Box>
    );
  }

  if (!items?.data || items.data.length === 0) {
    return (
      <Box height="100vh" display="flex" flexDirection="column" bg={containerBg}>
        <Container maxW="container.xl" py={4}>
          <VStack spacing={4} align="stretch">
            <SearchBar value={searchTerm} onChange={handleSearch} />
            <MarketFilters
              selectedCategory={selectedCategories}
              storeType={storeType}
              onCategoryChange={setSelectedCategories}
              onStoreTypeChange={setStoreType}
            />
            <Alert status="info" mt={4}>
              <AlertIcon />
              Nenhum item encontrado
            </Alert>
          </VStack>
        </Container>
      </Box>
    );
  }

  return (
    <Box 
      minH="100vh" 
      display="flex" 
      flexDirection="column" 
      bg={containerBg}
    >
      <Container maxW="container.xl" py={4} flexShrink={0} ref={containerRef}>
        <VStack spacing={4} align="stretch">
          <Box>
            <SearchBar value={searchTerm} onChange={handleSearch} />
          </Box>
          
          <MarketFilters
            selectedCategory={selectedCategories}
            storeType={storeType}
            onCategoryChange={setSelectedCategories}
            onStoreTypeChange={setStoreType}
          />

          <Box 
            flex="1" 
            position="relative"
          >
            <SimpleGrid 
              columns={gridConfig.columns}
              spacing={3}
            >
              {items.data.map((item: MarketItemViewModel) => (
                <MarketItem
                  key={item.itemId}
                  itemId={item.itemId}
                  itemName={item.itemName}
                  image={item.image}
                  category={item.category}
                  quantity={item.quantity}
                  onClick={handleItemClick}
                />
              ))}
            </SimpleGrid>
          </Box>
        </VStack>
      </Container>
      <MarketPagination
        currentPage={currentPage}
        totalPages={totalPages}
        onPageChange={handlePageChange}
      />
      <StoreListModal
        isOpen={isStoreModalOpen}
        onClose={() => setIsStoreModalOpen(false)}
        itemId={selectedItemId || 0}
        serverId={currentServer?.id ?? ''}
      />
    </Box>
  );
} 