'use client';

import { useEffect, useState } from 'react';
import { Container, Box, Text, Spinner, Alert, AlertIcon, Image, VStack, SimpleGrid } from '@chakra-ui/react';
import { useMarketItems, UseMarketItemsParams } from '@/hooks/useMarketItems';
import { SearchBar } from '@/components/ui/SearchBar';
import { MarketFilters } from '@/components/ui/MarketFilters';
import { MarketPagination } from '@/components/ui/MarketPagination';
import { StoreListModal } from '../components/StoreListModal';
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
      <Box height="100vh" display="flex" flexDirection="column">
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
      <Box height="100vh" display="flex" flexDirection="column">
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
      <Box height="100vh" display="flex" flexDirection="column">
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
                <Box
                  key={item.itemId}
                  bg="white"
                  p={2}
                  borderRadius="md"
                  boxShadow="sm"
                  borderWidth="1px"
                  borderColor="gray.200"
                  transition="all 0.3s ease"
                  position="relative"
                  zIndex={1}
                  _hover={{ 
                    transform: 'translateY(-5px)',
                    boxShadow: 'lg',
                    borderColor: 'blue.200',
                    zIndex: 2
                  }}
                  cursor="pointer"
                  maxW="135px"
                  onClick={() => handleItemClick(item.itemId)}
                >
                  <Image 
                    src={item.image} 
                    alt={item.itemName}
                    fallbackSrc="https://via.placeholder.com/100"
                    borderRadius="md"
                    mb={1}
                    width="full"
                    height="85px"
                    objectFit="contain"
                    bg="gray.50"
                  />
                  
                  <Text fontSize="2xs" fontWeight="bold" mb={0.5} noOfLines={1}>{item.itemName}</Text>
                  <Text color="gray.600" fontSize="2xs" mb={0.5}>
                    Categoria: {item.category}
                  </Text>
                  
                  <Text fontWeight="semibold" fontSize="xs" color="red.500" mb={0.5}>
                    {item.price.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}
                  </Text>
                  <Text color="gray.600" fontSize="2xs">
                    Quantidade: {item.quantity}
                  </Text>
                </Box>
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