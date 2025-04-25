import {
  VStack,
  Heading,
  Box,
  Text,
  Button,
  List,
  ListItem,
  HStack,
  Link,
} from '@chakra-ui/react';
import { UserMerchantViewModel } from '@/types/api/viewmodels/UserMerchantViewModel';

interface UserMerchantsProps {
  items: UserMerchantViewModel[];
  isLoading: boolean;
  userId?: string;
}

export function UserMerchants({ items, isLoading, userId }: UserMerchantsProps) {
  if (!userId) {
    return (
      <VStack spacing={4} align="start">
        <Heading size="md">Meus Mercadores</Heading>
        <Box p={8} w="100%" textAlign="center">
          <Text color="gray.500">Faça login para ver seus Mercadores</Text>
        </Box>
      </VStack>
    );
  }

  if (items.length === 0) {
    return (
      <VStack spacing={4} align="start">
        <Heading size="md">Meus Mercadores</Heading>
        <Box p={8} w="100%" textAlign="center">
          <Text color="gray.500">Nenhum item marcado</Text>
        </Box>
        <Button colorScheme="blue" size="sm" alignSelf="center">
          + Marcador
        </Button>
      </VStack>
    );
  }

  return (
    <VStack spacing={4} align="start" w="100%">
      <Heading size="md">Meus Mercadores</Heading>
      <List spacing={3} w="100%">
        {items.map((merchant) => (
          <ListItem key={merchant.accountId}>
            <HStack fontSize="sm" color="gray.600" spacing={4}>
              <Link href="#" color="blue.500">
                {merchant.accountId}
              </Link>
              <Text>{merchant.nome}</Text>
              <Text color="gray.500">{merchant.localizacao}</Text>
            </HStack>
          </ListItem>
        ))}
      </List>
      <Button colorScheme="blue" size="sm" alignSelf="center">
        + Marcador
      </Button>
    </VStack>
  );
} 