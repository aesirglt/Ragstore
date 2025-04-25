'use client';

import {
  Container,
  Heading,
  Text,
  VStack,
  Box,
  Card,
  CardBody,
  Button,
  Grid,
  GridItem,
  Flex,
  Spinner,
} from '@chakra-ui/react';
import { LastSearchedItems } from './components/LastSearchedItems';
import { PromotionBanner } from './components/PromotionBanner';
import { TopItemsSlider } from './components/TopItemsSlider';
import { UserMerchants } from './components/UserMerchants';
import { useTopItems } from '../hooks/useTopItems';
import { useLastSearchedItems } from '../hooks/useLastSearchedItems';
import { useUserMerchants } from '../hooks/useUserMerchants';
import { useEffect, useState } from 'react';
import { TopItemViewModel } from '@/types/api/viewmodels/TopItemViewModel';

// Dados zerados para quando houver erro
const emptyTopItems: TopItemViewModel[] = [
  {
    itemId: "1",
    itemName: "---",
    average: 0,
    currentMinValue: 0,
    currentMaxValue: 0,
    storeNumbers: 0,
    percentageChange: 0,
    imageUrl: "/items/default.png"
  }
];

// Dados zerados para quando houver erro nos últimos pesquisados
const emptyLastSearchedItems = [
  {
    itemId: 0,
    itemName: "---",
    quantity: 0,
    average: 0,
    image: "/items/default.png"
  }
];

export default function HomePage() {
  // TODO: Pegar o servidor selecionado do contexto ou estado global
  const selectedServer = "brothor";
  // TODO: Pegar o userId do contexto de autenticação
  const userId: string | undefined = undefined; // Temporariamente undefined para simular usuário deslogado

  const { data: lastSearchedItems, isLoading: isLoadingLastSearched } = useLastSearchedItems(selectedServer);
  const [lastSearchedItemIds, setLastSearchedItemIds] = useState<number[]>([]);
  const { data: topItems, isLoading: isLoadingTopItems } = useTopItems(selectedServer, lastSearchedItemIds);
  const { data: userMerchants, isLoading: isLoadingMerchants } = useUserMerchants(userId ?? '');

  useEffect(() => {
    if (lastSearchedItems && lastSearchedItems.length > 0) {
      setLastSearchedItemIds(lastSearchedItems.map(item => item.itemId));
    }
  }, [lastSearchedItems]);

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
                  <Heading size="lg">Meu bem</Heading>
                </Box>
                <Text color="gray.600">
                  Entre em sua conta e desfrute da experiência completa.
                </Text>
                <Button colorScheme="orange" size="sm">
                  Login →
                </Button>
              </VStack>
            </CardBody>
          </Card>
        </GridItem>
        <GridItem>
          <Flex justify="flex-end" pr={2}>
            <Box w="280px">
              {isLoadingTopItems ? (
                <Card>
                  <CardBody>
                    <Flex justify="center" align="center" h="200px">
                      <Spinner />
                    </Flex>
                  </CardBody>
                </Card>
              ) : (
                <TopItemsSlider items={topItems || emptyTopItems} />
              )}
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
              {isLoadingLastSearched ? (
                <Flex justify="center" align="center" h="200px">
                  <Spinner />
                </Flex>
              ) : (
                <LastSearchedItems items={lastSearchedItems || []} />
              )}
            </CardBody>
          </Card>
        </GridItem>
        <GridItem>
          <Card>
            <CardBody>
              {isLoadingMerchants ? (
                <Flex justify="center" align="center" h="200px">
                  <Spinner />
                </Flex>
              ) : (
                <UserMerchants 
                  items={userMerchants || []}
                  isLoading={isLoadingMerchants}
                  userId={userId}
                />
              )}
            </CardBody>
          </Card>
        </GridItem>
      </Grid>
    </Container>
  );
} 