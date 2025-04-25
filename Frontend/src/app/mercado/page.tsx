'use client';

import { useEffect, useState, useRef } from 'react';
import { Container, Box, Text, Spinner, Alert, AlertIcon, Image, Flex, Button, VStack, SimpleGrid } from '@chakra-ui/react';
import { useMarketItems, UseMarketItemsParams } from '@/hooks/useMarketItems';
import { SearchBar } from '@/components/ui/SearchBar';
import { MarketFilters } from '@/components/ui/MarketFilters';
import { MarketPagination } from '@/components/ui/MarketPagination';

const ITEMS_PER_PAGE = 12;

export default function MercadoPage() {
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [searchTerm, setSearchTerm] = useState(() => {
    if (typeof window !== 'undefined') {
      return localStorage.getItem('marketSearchTerm') || '';
    }
    return '';
  });
  const [debouncedSearchTerm, setDebouncedSearchTerm] = useState('');
  const [selectedCategory, setSelectedCategory] = useState('');
  const [selectedServer, setSelectedServer] = useState('');
  const [storeType, setStoreType] = useState('');

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

  const marketParams: UseMarketItemsParams = {
    server: selectedServer || 'brothor',
    page: currentPage,
    pageSize: ITEMS_PER_PAGE,
    itemName: debouncedSearchTerm,
    category: selectedCategory,
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

  if (isLoading) {
    return (
      <Box height="100vh" display="flex" flexDirection="column">
        <Container maxW="container.xl" py={4}>
          <VStack spacing={4} align="stretch">
            <SearchBar value={searchTerm} onChange={handleSearch} />
            <MarketFilters
              selectedCategory={selectedCategory}
              selectedServer={selectedServer}
              storeType={storeType}
              onCategoryChange={setSelectedCategory}
              onServerChange={setSelectedServer}
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
              selectedCategory={selectedCategory}
              selectedServer={selectedServer}
              storeType={storeType}
              onCategoryChange={setSelectedCategory}
              onServerChange={setSelectedServer}
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
              selectedCategory={selectedCategory}
              selectedServer={selectedServer}
              storeType={storeType}
              onCategoryChange={setSelectedCategory}
              onServerChange={setSelectedServer}
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
          <SearchBar value={searchTerm} onChange={handleSearch} />
          <MarketFilters
            selectedCategory={selectedCategory}
            selectedServer={selectedServer}
            storeType={storeType}
            onCategoryChange={setSelectedCategory}
            onServerChange={setSelectedServer}
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
    </Box>
  );
} 