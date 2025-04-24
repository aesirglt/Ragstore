'use client';

import { useEffect, useState } from 'react';
import { Container, Heading, SimpleGrid, Box, Text, Spinner, Alert, AlertIcon, Image, Flex, Button, Input, InputGroup, InputLeftElement } from '@chakra-ui/react';
import { useMarketItems, UseMarketItemsParams } from '@/hooks/useMarketItems';
import { SearchIcon } from '@chakra-ui/icons';

const ITEMS_PER_PAGE = 12;

export default function MercadoPage() {
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [searchTerm, setSearchTerm] = useState(() => {
    // Recupera o termo de busca do localStorage ao inicializar
    if (typeof window !== 'undefined') {
      return localStorage.getItem('marketSearchTerm') || '';
    }
    return '';
  });
  const [debouncedSearchTerm, setDebouncedSearchTerm] = useState('');

  // Efeito para debounce da pesquisa
  useEffect(() => {
    const timer = setTimeout(() => {
      setDebouncedSearchTerm(searchTerm);
      // Salva o termo de busca no localStorage
      if (typeof window !== 'undefined') {
        localStorage.setItem('marketSearchTerm', searchTerm);
      }
    }, 500);

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
    server: 'brothor',
    page: currentPage,
    pageSize: ITEMS_PER_PAGE,
    itemName: debouncedSearchTerm
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
    setCurrentPage(1); // Reset para primeira página ao buscar
  };

  if (isLoading) {
    return (
      <Box height="100vh" display="flex" alignItems="center" justifyContent="center">
        <Spinner size="xl" />
      </Box>
    );
  }

  if (error) {
    return (
      <Box height="100vh" display="flex" alignItems="center" justifyContent="center">
        <Alert status="error">
          <AlertIcon />
          {error.message}
        </Alert>
      </Box>
    );
  }

  if (!items || items.length === 0) {
    return (
      <Box height="100vh" display="flex" flexDirection="column">
        <Container maxW="container.xl" py={4}>
          <Box mb={4}>
            <InputGroup>
              <InputLeftElement pointerEvents="none">
                <SearchIcon color="gray.400" />
              </InputLeftElement>
              <Input
                placeholder="Pesquisar itens..."
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
                size="md"
                variant="filled"
                bg="white"
                _hover={{ bg: 'gray.50' }}
                _focus={{ bg: 'white', borderColor: 'blue.500' }}
              />
            </InputGroup>
          </Box>
          <Alert status="info" mt={4}>
            <AlertIcon />
            Nenhum item encontrado
          </Alert>
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
        <Box mb={4}>
          <InputGroup>
            <InputLeftElement pointerEvents="none">
              <SearchIcon color="gray.400" />
            </InputLeftElement>
            <Input
              placeholder="Pesquisar itens..."
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
              size="md"
              variant="filled"
              bg="white"
              _hover={{ bg: 'gray.50' }}
              _focus={{ bg: 'white', borderColor: 'blue.500' }}
            />
          </InputGroup>
        </Box>
        
        <Box 
          flex="1" 
          overflow="auto" 
          position="relative"
          mx={-4}
          px={4}
          pb={16}
        >
          <SimpleGrid 
            columns={{ base: 1, sm: 2, md: 3, lg: 5 }} 
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
                _hover={{ boxShadow: 'md' }}
                transition="all 0.2s"
                maxW="200px"
                fontSize="sm"
              >
                <Image 
                  src={item.image} 
                  alt={item.itemName}
                  fallbackSrc="https://via.placeholder.com/100"
                  borderRadius="md"
                  mb={2}
                  width="full"
                  height="auto"
                  maxH="100px"
                  objectFit="contain"
                />
                
                <Heading size="sm" mb={1}>{item.itemName}</Heading>
                <Text color="gray.600" fontSize="xs" mb={1}>
                  Categoria: {item.category}
                </Text>
                
                <Text fontWeight="semibold" fontSize="sm">
                  Preço: {item.price.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}
                </Text>
                <Text color="gray.600" fontSize="xs">
                  Quantidade: {item.quantity}
                </Text>
              </Box>
            ))}
          </SimpleGrid>
        </Box>
      </Container>

      <Box 
        position="fixed"
        bottom="48px"
        left="0"
        right="0"
        bg="white" 
        py={3}
        borderTop="1px" 
        borderColor="gray.200"
        boxShadow="0 -2px 10px rgba(0,0,0,0.05)"
      >
        <Container maxW="container.xl">
          <Flex justify="center" gap={4} align="center">
            <Button
              size="sm"
              colorScheme="blue"
              variant="outline"
              onClick={() => handlePageChange(currentPage - 1)}
              isDisabled={currentPage === 1}
            >
              Anterior
            </Button>
            <Text fontSize="sm">
              Página {currentPage} de {totalPages}
            </Text>
            <Button
              size="sm"
              colorScheme="blue"
              variant="outline"
              onClick={() => handlePageChange(currentPage + 1)}
              isDisabled={currentPage === totalPages}
            >
              Próxima
            </Button>
          </Flex>
        </Container>
      </Box>
    </Box>
  );
} 