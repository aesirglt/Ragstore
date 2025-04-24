'use client';

import { useEffect, useState } from 'react';
import { Container, Heading, SimpleGrid, Box, Text, Spinner, Alert, AlertIcon, Image, Flex, Button } from '@chakra-ui/react';
import { ItemViewModel, ItemResponse } from '@/types/api';
import apiService from '@/services/api';
import { useMarketItems } from '@/hooks/useMarketItems';

const ITEMS_PER_PAGE = 12;

export default function MercadoPage() {
  const [items, setItems] = useState<ItemViewModel[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const { data: useMarkets, isLoading: isLoadingMarkets } = useMarketItems('brothor');

  useEffect(() => {
    const fetchItems = async () => {
      try {
        setLoading(true);
        setError(null);
        const response = useMarkets;
        //const response = await getItems({ 
        //  page: currentPage, 
        //  pageSize: ITEMS_PER_PAGE 
        //}) as ItemResponse;
        
        if (response && response.value) {
          setItems(response.value);
          setTotalPages(Math.ceil((response['@odata.count'] || 0) / ITEMS_PER_PAGE));
        } else {
          setError('Nenhum item encontrado');
        }
      } catch (error: any) {
        console.error('Erro ao buscar itens:', error);
        setError(error.response?.data?.error || 'Erro ao carregar os itens');
      } finally {
        setLoading(false);
      }
    };

    fetchItems();
  }, [currentPage]);

  const handlePageChange = (newPage: number) => {
    setCurrentPage(newPage);
  };

  if (loading) {
    return (
      <Container maxW="container.xl" py={8} centerContent>
        <Spinner size="xl" />
      </Container>
    );
  }

  if (error) {
    return (
      <Container maxW="container.xl" py={8}>
        <Alert status="error">
          <AlertIcon />
          {error}
        </Alert>
      </Container>
    );
  }

  if (items.length === 0) {
    return (
      <Container maxW="container.xl" py={8}>
        <Alert status="info">
          <AlertIcon />
          Nenhum item encontrado
        </Alert>
      </Container>
    );
  }

  return (
    <Container maxW="container.xl" py={8}>
      <Heading as="h1" mb={6}>Mercado</Heading>
      
      <SimpleGrid columns={{ base: 1, md: 2, lg: 3 }} spacing={6}>
        {items.map((item) => (
          <Box
            key={item.itemId}
            p={5}
            shadow="md"
            borderWidth="1px"
            borderRadius="lg"
          >
            <Image 
              src={item.image} 
              alt={item.itemName}
              fallbackSrc="https://via.placeholder.com/150"
              borderRadius="md"
              mb={4}
            />
            
            <Heading size="md">{item.itemName}</Heading>
            <Text mt={2}>Categoria: {item.category}</Text>
            
            <Box mt={4}>
              <Text>Preço: {item.price.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}</Text>
              <Text>Quantidade: {item.quantity}</Text>
            </Box>
          </Box>
        ))}
      </SimpleGrid>

      <Flex justify="center" mt={8} gap={2}>
        <Button
          onClick={() => handlePageChange(currentPage - 1)}
          isDisabled={currentPage === 1}
        >
          Anterior
        </Button>
        <Text>
          Página {currentPage} de {totalPages}
        </Text>
        <Button
          onClick={() => handlePageChange(currentPage + 1)}
          isDisabled={currentPage === totalPages}
        >
          Próxima
        </Button>
      </Flex>
    </Container>
  );
} 