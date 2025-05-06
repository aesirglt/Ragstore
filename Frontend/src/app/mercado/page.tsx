'use client';

import { useEffect, useState } from 'react';
import { 
  Container, 
  Box, 
  Text, 
  Spinner, 
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

const ITEMS_PER_PAGE = 12;

export default function MercadoPage() {
  const { currentServer } = useServer();
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [searchTerm, setSearchTerm] = useState('');
  const [debouncedSearchTerm, setDebouncedSearchTerm] = useState('');
  const [selectedCategories, setSelectedCategories] = useState<string[]>([]);
  const [storeType, setStoreType] = useState('Vending');
  const [selectedItemId, setSelectedItemId] = useState<number | null>(null);
  const [isStoreModalOpen, setIsStoreModalOpen] = useState(false);
  const [selectedSort, setSelectedSort] = useState<string>('price_asc');
  const [itemsPerPage] = useState(20);

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

  const marketParams: UseMarketItemsParams = {
    server: currentServer,
    page: currentPage,
    pageSize: ITEMS_PER_PAGE,
    itemName: debouncedSearchTerm,
    category: selectedCategories.join(','),
    storeType: storeType
  };

  const { data: items = [], isLoading, error } = useMarketItems(marketParams);

  useEffect(() => {
    if (items && items.length > 0) {
      setTotalPages(Math.ceil(items.length / ITEMS_PER_PAGE));
    }
  }, [items]);

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
              <Spinner size="xl" />
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

  if (!items || items.length === 0) {
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
      height="100vh" 
      display="flex" 
      flexDirection="column" 
      position="relative"
      overflow="hidden"
      bg={containerBg}
    >
      <Container maxW="container.xl" py={4} height="full">
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
            overflow="auto" 
            position="relative"
            pb={16}
            pt={4}
          >
            <SimpleGrid 
              columns={{ base: 1, sm: 2, md: 3, lg: 4, xl: 5 }} 
              spacing={3}
            >
              {items.map((item) => (
                <MarketItem
                  key={item.itemId}
                  itemId={item.itemId}
                  itemName={item.itemName}
                  image={item.image}
                  category={item.category}
                  price={item.price}
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
        server={currentServer}
      />
    </Box>
  );
} 