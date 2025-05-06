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
import { Pagination } from './ui/Pagination';

interface LastSearchedItemsProps {
  items: LastSearchedItemViewModel[];
  currentPage?: number;
  totalPages?: number;
  onPageChange?: (page: number) => void;
}

export function LastSearchedItems({ 
  items = [], 
  currentPage = 1,
  totalPages = 1,
  onPageChange 
}: LastSearchedItemsProps) {
  const textColor = useColorModeValue('gray.700', 'gray.300');
  const linkColor = useColorModeValue('orange.400', 'orange.300');

  return (
    <Box w="100%" overflowX="auto">
      <Flex direction="column" gap={4}>
        <Flex justify="space-between" align="center">
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

        {onPageChange && (
          <Pagination
            currentPage={currentPage}
            totalPages={totalPages}
            onPageChange={onPageChange}
            variant="inline"
          />
        )}
      </Flex>
    </Box>
  );
} 