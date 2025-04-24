import {
  Table,
  Thead,
  Tbody,
  Tr,
  Th,
  Td,
  Box,
  Heading,
  Flex,
  Image,
  Link,
  Text
} from '@chakra-ui/react';
import { useEffect, useState } from 'react';
import { getLastSearchedItems } from '@/services/api';

interface Transaction {
  itemId: string;
  itemName: string;
  quantity: number;
  unitPrice: number;
  totalPrice: number;
  timestamp: string;
  image: string;
}

export function LastTransactions() {
  const [transactions, setTransactions] = useState<Transaction[]>([]);

  useEffect(() => {
    async function loadTransactions() {
      try {
        const response = await getLastSearchedItems();
        setTransactions(response.data);
      } catch (error) {
        console.error('Erro ao carregar transações:', error);
      }
    }

    loadTransactions();
  }, []);

  return (
    <Box w="100%" overflowX="auto">
      <Flex justify="space-between" align="center" mb={4}>
        <Heading size="md">Últimas Transações</Heading>
        <Link color="orange.400" href="/mercado">Ver mais</Link>
      </Flex>

      <Table variant="simple">
        <Thead>
          <Tr>
            <Th>Item</Th>
            <Th>Quantidade</Th>
            <Th>Valor Unitário</Th>
            <Th>Total</Th>
            <Th>Horário</Th>
          </Tr>
        </Thead>
        <Tbody>
          {transactions.map((transaction, index) => (
            <Tr key={index}>
              <Td>
                <Flex align="center" gap={2}>
                  <Image
                    src={transaction.image}
                    alt={transaction.itemName}
                    boxSize="32px"
                    objectFit="contain"
                  />
                  <Text>{transaction.itemName}</Text>
                </Flex>
              </Td>
              <Td>{transaction.quantity}x</Td>
              <Td>{transaction.unitPrice.toLocaleString()}z</Td>
              <Td>{transaction.totalPrice.toLocaleString()}z</Td>
              <Td>{new Date(transaction.timestamp).toLocaleTimeString()}</Td>
            </Tr>
          ))}
        </Tbody>
      </Table>
    </Box>
  );
} 