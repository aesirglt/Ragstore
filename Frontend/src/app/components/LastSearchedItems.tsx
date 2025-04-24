'use client';

import {
  Box,
  Table,
  Thead,
  Tbody,
  Tr,
  Th,
  Td,
  Flex,
  Image,
  Text,
  Link,
  Heading,
} from '@chakra-ui/react';

interface SearchedItem {
  itemId: number;
  itemName: string;
  totalQuantity: number;
  averagePrice: number;
  image: string;
}

interface LastSearchedItemsProps {
  items: SearchedItem[];
}

export function LastSearchedItems({ items = [] }: LastSearchedItemsProps) {
  return (
    <Box w="100%" overflowX="auto">
      <Flex justify="space-between" align="center" mb={4}>
        <Heading size="md">Últimos Itens Pesquisados</Heading>
        <Link color="orange.400" href="/mercado">Ver mais</Link>
      </Flex>

      <Table variant="simple">
        <Thead>
          <Tr>
            <Th>Item</Th>
            <Th isNumeric>Quantidade</Th>
            <Th isNumeric>Média</Th>
          </Tr>
        </Thead>
        <Tbody>
          {items.map((item) => (
            <Tr key={item.itemId}>
              <Td>
                <Flex align="center" gap={2}>
                  <Image
                    src={item.image}
                    alt={item.itemName}
                    boxSize="32px"
                    objectFit="contain"
                  />
                  <Text>{item.itemName}</Text>
                </Flex>
              </Td>
              <Td isNumeric>{item.totalQuantity}x</Td>
              <Td isNumeric>{item.averagePrice.toLocaleString()}z</Td>
            </Tr>
          ))}
        </Tbody>
      </Table>
    </Box>
  );
} 