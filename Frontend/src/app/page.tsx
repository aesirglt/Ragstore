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
import { LastSearchedItems } from '@/components/LastSearchedItems';
import { PromotionBanner } from './components/PromotionBanner';
import { TopItemsSlider } from './components/TopItemsSlider';
import { useTopItems } from '../hooks/useTopItems';
import { useLastSearchedItems } from '../hooks/useLastSearchedItems';
import { useUserMerchants } from '../hooks/useUserMerchants';
import { useEffect, useState } from 'react';
import { TopItemViewModel } from '@/types/api/viewmodels/TopItemViewModel';
import { LastSearchedItemViewModel } from '@/types/api/viewmodels/LastSearchedItemViewModel';
import { useServer } from '@/contexts/ServerContext';
import { useRouter } from 'next/navigation';
import { WelcomeComponent } from './components/WelcomeComponent';
import { LoadingList } from '@/components/LoadingList';
import { PageResult } from '@/types/api/responses/PageResult';

// Dados zerados para quando houver erro
const emptyTopItems: TopItemViewModel[] = [{
    itemId: "1",
    itemName: "---",
    average: 0,
    currentMinValue: 0,
    currentMaxValue: 0,
    storeNumbers: 0,
    percentageChange: 0,
    imageUrl: "/items/default.png"
  }];

// Dados zerados para quando houver erro nos últimos pesquisados
const emptyLastSearchedItems: PageResult<LastSearchedItemViewModel> = {
  data: [{
    itemId: 0,
    itemName: "---",
    quantity: 0,
    average: 0,
    image: "/items/default.png"
  }],
  totalCount: 1
};

export default function HomePage() {
  // TODO: Pegar o servidor selecionado do contexto ou estado global
  const { currentServer } = useServer();
  // TODO: Pegar o userId do contexto de autenticação
  const userId: string | undefined = undefined; // Temporariamente undefined para simular usuário deslogado

  const { data: lastSearchedItems, isLoading: isLoadingLastSearched } = useLastSearchedItems(currentServer);
  const [lastSearchedItemIds, setLastSearchedItemIds] = useState<number[]>([]);
  const { data: topItems, isLoading: isLoadingTopItems } = useTopItems(
    currentServer, 
    lastSearchedItemIds.length > 0 ? lastSearchedItemIds : []
  );
  const { data: userMerchants, isLoading: isLoadingMerchants } = useUserMerchants(userId ?? '');

  const router = useRouter();

  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);

  // Atualiza os IDs quando lastSearchedItems mudar
  useEffect(() => {
    if (lastSearchedItems?.data && lastSearchedItems.data.length > 0) {
      setLastSearchedItemIds(lastSearchedItems.data.map(item => item.itemId));
    }
  }, [lastSearchedItems]);

  return (
    <Container maxW="container.xl" py={8}>
      <Grid
        templateColumns={{ base: "1fr", md: "1.5fr 1fr" }}
        templateRows="auto auto auto"
        gap={4}
      >
        {/* Primeira linha */}
        <GridItem>
          <WelcomeComponent />
        </GridItem>
        <GridItem>
          <Box />
        </GridItem>

        {/* Segunda linha - Banner ocupando toda a largura */}
        <GridItem colSpan={2}>
          <PromotionBanner />
        </GridItem>

        {/* Terceira linha */}
        <GridItem>
          <Card>
            <CardBody pr={2}>
              {isLoadingLastSearched ? (
                <LoadingList />
              ) : (
                <LastSearchedItems 
                  page={lastSearchedItems || emptyLastSearchedItems}
                />
              )}
            </CardBody>
          </Card>
        </GridItem>
        <GridItem>
          <Flex justify="flex-end">
            <Box w="280px">
              {isLoadingTopItems ? (
                <Card>
                  <CardBody>
                    <LoadingList />
                  </CardBody>
                </Card>
              ) : (
                <TopItemsSlider items={topItems || emptyTopItems} />
              )}
            </Box>
          </Flex>
        </GridItem>
      </Grid>
    </Container>
  );
} 