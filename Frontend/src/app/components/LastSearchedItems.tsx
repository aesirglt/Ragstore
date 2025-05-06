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
  useColorModeValue,
} from '@chakra-ui/react';
import { LastSearchedItemViewModel } from '@/types/api/viewmodels/LastSearchedItemViewModel';

interface LastSearchedItemsProps {
  items: LastSearchedItemViewModel[];
}

export function LastSearchedItems({ items = [] }: LastSearchedItemsProps) {
  const textColor = useColorModeValue('gray.700', 'gray.300');
  const linkColor = useColorModeValue('orange.400', 'orange.300');

  return (
    <Box w="100%" overflowX="auto">
      <Flex justify="space-between" align="center" mb={4}>
        <Heading size="md" color={textColor}>Últimos itens visualizados</Heading>
        <Link color={linkColor} href="/mercado">Ver mais</Link>
      </Flex>

      <Table variant="simple">
        <Thead>
          <Tr>
            <Th color={textColor}>Item</Th>
            <Th isNumeric color={textColor}>Quantidade</Th>
            <Th isNumeric color={textColor}>Média</Th>
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
                  <Text color={textColor}>{item.itemName}</Text>
                </Flex>
              </Td>
              <Td isNumeric color={textColor}>{item.quantity}x</Td>
              <Td isNumeric color={textColor}>{item.average}z</Td>
            </Tr>
          ))}
        </Tbody>
      </Table>
    </Box>
  );
} 