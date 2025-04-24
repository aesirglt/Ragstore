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
import { useUserMerchants, UserMerchant } from '../hooks/useUserMerchants';

// Dados zerados para quando houver erro
const emptyTopItems = [
  {
    id: "1",
    name: "---",
    lastPrice: 0,
    lowPrice: 0,
    highPrice: 0,
    volume: 0,
    percentageChange: 0,
    imageUrl: "/items/default.png"
  }
];

// Dados zerados para quando houver erro nos últimos pesquisados
const emptyLastSearchedItems = [
  {
    itemId: 0,
    itemName: "---",
    totalQuantity: 0,
    averagePrice: 0,
    image: "/items/default.png"
  }
];

// Dados zerados para quando houver erro nos marcadores
const emptyUserMerchants: UserMerchant[] = [];

export default function HomePage() {
  // TODO: Pegar o servidor selecionado do contexto ou estado global
  const selectedServer = "broTHOR";
  // TODO: Pegar o userId do contexto de autenticação
  const userId: string | undefined = undefined; // Temporariamente undefined para simular usuário deslogado

  const { data: topItems, isLoading: isLoadingTopItems } = useTopItems(selectedServer);
  const { data: lastSearchedItems, isLoading: isLoadingLastSearched } = useLastSearchedItems(selectedServer);
  const { data: userMerchants, isLoading: isLoadingMerchants } = useUserMerchants(userId ?? '');

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
                <LastSearchedItems items={lastSearchedItems || emptyLastSearchedItems} />
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
                  items={userMerchants || emptyUserMerchants}
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