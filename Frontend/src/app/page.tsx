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
  Grid,
  GridItem,
  Flex,
} from '@chakra-ui/react';
import { LastSearchedItems } from './components/LastSearchedItems';
import { PromotionBanner } from './components/PromotionBanner';
import { TopItemsSlider } from './components/TopItemsSlider';

// Dados de exemplo para o slider
const topItems = [
  {
    id: "1",
    name: "DKM / ZNY",
    lastPrice: 120000000,
    lowPrice: 110000000,
    highPrice: 130000000,
    volume: 20922000000,
    percentageChange: 0.8,
    imageUrl: "/items/1.png"
  },
  {
    id: "2",
    name: "CIN / ZNY",
    lastPrice: 1650000,
    lowPrice: 1599999,
    highPrice: 1800000,
    volume: 8215999500,
    percentageChange: -4.1,
    imageUrl: "/items/2.png"
  },
  {
    id: "3",
    name: "IST / ZNY",
    lastPrice: 850000,
    lowPrice: 850000,
    highPrice: 1500000,
    volume: 21290401478,
    percentageChange: -15.0,
    imageUrl: "/items/3.png"
  },
  {
    id: "4",
    name: "BSH / ZNY",
    lastPrice: 289999,
    lowPrice: 250000,
    highPrice: 290000,
    volume: 762617650,
    percentageChange: 16.0,
    imageUrl: "/items/4.png"
  },
  {
    id: "5",
    name: "RHA / ZNY",
    lastPrice: 300000,
    lowPrice: 280000,
    highPrice: 320000,
    volume: 543210987,
    percentageChange: 5.5,
    imageUrl: "/items/5.png"
  }
];

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
      <Grid
        templateColumns="repeat(2, 1fr)"
        templateRows="auto auto auto"
        gap={4}
      >
        {/* Primeira linha */}
        <GridItem>
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
        </GridItem>
        <GridItem>
          <Flex justify="center">
            <Box maxW="300px" w="100%">
              <TopItemsSlider items={topItems} />
            </Box>
          </Flex>
        </GridItem>

        {/* Segunda linha - Banner ocupando toda a largura */}
        <GridItem colSpan={2}>
          <PromotionBanner />
        </GridItem>

        {/* Terceira linha */}
        <GridItem>
          <Card>
            <CardBody>
              <LastSearchedItems items={mockLastSearchedItems} />
            </CardBody>
          </Card>
        </GridItem>
        <GridItem>
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
        </GridItem>
      </Grid>
    </Container>
  );
} 