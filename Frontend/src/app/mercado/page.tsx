'use client';

import { useEffect, useState, useRef } from 'react';
import { Container, Heading, SimpleGrid, Box, Text, Spinner, Alert, AlertIcon, Image, Flex, Button, Input, InputGroup, InputLeftElement, Select, VStack, HStack } from '@chakra-ui/react';
import { useMarketItems, UseMarketItemsParams } from '@/hooks/useMarketItems';
import { SearchIcon } from '@chakra-ui/icons';

const ITEMS_PER_PAGE = 12;

export default function MercadoPage() {
  const searchInputRef = useRef<HTMLInputElement>(null);
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
  const [selectedCategory, setSelectedCategory] = useState('');
  const [selectedServer, setSelectedServer] = useState('');
  const [priceOrder, setPriceOrder] = useState('');

  // Efeito para debounce da pesquisa
  useEffect(() => {
    const timer = setTimeout(() => {
      setDebouncedSearchTerm(searchTerm);
      // Salva o termo de busca no localStorage
      if (typeof window !== 'undefined') {
        localStorage.setItem('marketSearchTerm', searchTerm);
      }
      // Foca no input após a pesquisa
      if (searchInputRef.current) {
        searchInputRef.current.focus();
      }
    }, 1000);

    return () => clearTimeout(timer);
  }, [searchTerm]);

  // Efeito para focar no input quando o componente é montado
  useEffect(() => {
    if (searchInputRef.current) {
      searchInputRef.current.focus();
    }
  }, []);

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
    priceOrder: priceOrder
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
      <Box height="100vh" display="flex" flexDirection="column">
        <Container maxW="container.xl" py={4}>
          <VStack spacing={4} align="stretch">
            <Box>
              <InputGroup>
                <InputLeftElement pointerEvents="none">
                  <SearchIcon color="gray.400" />
                </InputLeftElement>
                <Input
                  ref={searchInputRef}
                  placeholder="Pesquisar itens..."
                  value={searchTerm}
                  onChange={(e) => setSearchTerm(e.target.value)}
                  size="md"
                  variant="filled"
                  bg="white"
                  _hover={{ bg: 'gray.50' }}
                  _focus={{ bg: 'white', borderColor: 'blue.500' }}
                  autoFocus
                />
              </InputGroup>
            </Box>

            <HStack spacing={1} wrap="wrap">
              <Box flex="1" minW="200px">
                <Select
                  placeholder="Categoria"
                  value={selectedCategory}
                  onChange={(e) => setSelectedCategory(e.target.value)}
                  bg="white"
                >
                  <option value="">Todas</option>
                  <option value="weapon">Armas</option>
                  <option value="armor">Armaduras</option>
                  <option value="card">Cartas</option>
                  <option value="potion">Poções</option>
                  <option value="material">Materiais</option>
                </Select>
              </Box>

              <Box flex="1" minW="200px">
                <Select
                  placeholder="Servidor"
                  value={selectedServer}
                  onChange={(e) => setSelectedServer(e.target.value)}
                  bg="white"
                >
                  <option value="">Todos</option>
                  <option value="thor">Thor</option>
                  <option value="odin">Odin</option>
                  <option value="loki">Loki</option>
                  <option value="freya">Freya</option>
                </Select>
              </Box>

              <Box flex="1" minW="200px">
                <Select
                  placeholder="Ordenar por preço"
                  value={priceOrder}
                  onChange={(e) => setPriceOrder(e.target.value)}
                  bg="white"
                >
                  <option value="">Padrão</option>
                  <option value="asc">Menor Preço</option>
                  <option value="desc">Maior Preço</option>
                </Select>
              </Box>
            </HStack>

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
            <Box>
              <InputGroup>
                <InputLeftElement pointerEvents="none">
                  <SearchIcon color="gray.400" />
                </InputLeftElement>
                <Input
                  ref={searchInputRef}
                  placeholder="Pesquisar itens..."
                  value={searchTerm}
                  onChange={(e) => setSearchTerm(e.target.value)}
                  size="md"
                  variant="filled"
                  bg="white"
                  _hover={{ bg: 'gray.50' }}
                  _focus={{ bg: 'white', borderColor: 'blue.500' }}
                  autoFocus
                />
              </InputGroup>
            </Box>

            <HStack spacing={1} wrap="wrap">
              <Box flex="1" minW="200px">
                <Select
                  placeholder="Categoria"
                  value={selectedCategory}
                  onChange={(e) => setSelectedCategory(e.target.value)}
                  bg="white"
                >
                  <option value="">Todas</option>
                  <option value="weapon">Armas</option>
                  <option value="armor">Armaduras</option>
                  <option value="card">Cartas</option>
                  <option value="potion">Poções</option>
                  <option value="material">Materiais</option>
                </Select>
              </Box>

              <Box flex="1" minW="200px">
                <Select
                  placeholder="Servidor"
                  value={selectedServer}
                  onChange={(e) => setSelectedServer(e.target.value)}
                  bg="white"
                >
                  <option value="">Todos</option>
                  <option value="thor">Thor</option>
                  <option value="odin">Odin</option>
                  <option value="loki">Loki</option>
                  <option value="freya">Freya</option>
                </Select>
              </Box>

              <Box flex="1" minW="200px">
                <Select
                  placeholder="Ordenar por preço"
                  value={priceOrder}
                  onChange={(e) => setPriceOrder(e.target.value)}
                  bg="white"
                >
                  <option value="">Padrão</option>
                  <option value="asc">Menor Preço</option>
                  <option value="desc">Maior Preço</option>
                </Select>
              </Box>
            </HStack>

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
            <InputGroup>
              <InputLeftElement pointerEvents="none">
                <SearchIcon color="gray.400" />
              </InputLeftElement>
              <Input
                ref={searchInputRef}
                placeholder="Pesquisar itens..."
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
                size="md"
                variant="filled"
                bg="white"
                _hover={{ bg: 'gray.50' }}
                _focus={{ bg: 'white', borderColor: 'blue.500' }}
                autoFocus
              />
            </InputGroup>
          </Box>

          <HStack spacing={1} wrap="wrap">
            <Box flex="1" minW="200px">
              <Select
                placeholder="Categoria"
                value={selectedCategory}
                onChange={(e) => setSelectedCategory(e.target.value)}
                bg="white"
              >
                <option value="">Todas</option>
                <option value="weapon">Armas</option>
                <option value="armor">Armaduras</option>
                <option value="card">Cartas</option>
                <option value="potion">Poções</option>
                <option value="material">Materiais</option>
              </Select>
            </Box>

            <Box flex="1" minW="200px">
              <Select
                placeholder="Servidor"
                value={selectedServer}
                onChange={(e) => setSelectedServer(e.target.value)}
                bg="white"
              >
                <option value="">Todos</option>
                <option value="thor">Thor</option>
                <option value="odin">Odin</option>
                <option value="loki">Loki</option>
                <option value="freya">Freya</option>
              </Select>
            </Box>

            <Box flex="1" minW="200px">
              <Select
                placeholder="Ordenar por preço"
                value={priceOrder}
                onChange={(e) => setPriceOrder(e.target.value)}
                bg="white"
              >
                <option value="">Padrão</option>
                <option value="asc">Menor Preço</option>
                <option value="desc">Maior Preço</option>
              </Select>
            </Box>
          </HStack>

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