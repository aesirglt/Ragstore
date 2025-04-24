'use client';

import {
  Container,
  Heading,
  Text,
  VStack,
  Box,
  Card,
  CardBody,
  SimpleGrid,
  Button,
} from '@chakra-ui/react';
import { LastSearchedItems } from './components/LastSearchedItems';
import { PromotionBanner } from './components/PromotionBanner';
import { ItemValueSummary } from './components/ItemValueSummary';

// Dados de exemplo - serão substituídos pelos dados da API
const mockSummaryData = {
  minValue: 1599999,
  currentMinValue: 1650000,
  currentMaxValue: 1800000,
  average: 8215999500,
  storeNumbers: 7
};

// Dados de exemplo para os últimos itens pesquisados
const mockLastSearchedItems = [
  {
    itemId: 1,
    itemName: "Diário de Aventuras [HARDCORE]",
    totalQuantity: 527,
    averagePrice: 250000000,
    image: "/items/1.png"
  },
  {
    itemId: 2,
    itemName: "Fragmentos de Ametista",
    totalQuantity: 670,
    averagePrice: 300000,
    image: "/items/2.png"
  },
  {
    itemId: 3,
    itemName: "Instance Stone",
    totalQuantity: 150,
    averagePrice: 850000,
    image: "/items/3.png"
  },
  {
    itemId: 4,
    itemName: "Red Herb Activator",
    totalQuantity: 30,
    averagePrice: 300000,
    image: "/items/4.png"
  }
];

export default function HomePage() {
  return (
    <Container maxW="container.xl" py={8}>
      <SimpleGrid columns={{ base: 1, lg: 2 }} spacing={8}>
        {/* Coluna da esquerda */}
        <VStack align="stretch" spacing={6}>
          <Card>
            <CardBody>
              <VStack spacing={4} align="start">
                <Box>
                  <Text fontSize="sm" color="gray.500">Seja bem-vindo,</Text>
                  <Heading size="lg">Meu amor</Heading>
                </Box>
                <Text color="gray.600">
                  Entre em sua conta e desfrute da experiência completa de nossa DB.
                </Text>
                <Button colorScheme="orange" size="sm">
                  Login →
                </Button>
              </VStack>
            </CardBody>
          </Card>

          <PromotionBanner />

          <Card>
            <CardBody>
              <LastSearchedItems items={mockLastSearchedItems} />
            </CardBody>
          </Card>
        </VStack>

        {/* Coluna da direita */}
        <VStack align="stretch" spacing={6}>
          <ItemValueSummary data={mockSummaryData} />

          <Card>
            <CardBody>
              <VStack spacing={4} align="start">
                <Heading size="md">Meus Marcadores</Heading>
                <Box p={8} w="100%" textAlign="center">
                  <Text color="gray.500">Nenhum item marcado</Text>
                </Box>
                <Button colorScheme="blue" size="sm" alignSelf="center">
                  + Marcador
                </Button>
              </VStack>
            </CardBody>
          </Card>
        </VStack>
      </SimpleGrid>
    </Container>
  );
} 