'use client';

import {
  Box,
  Text,
  VStack,
  Flex,
  Image,
  Card,
  CardBody,
} from '@chakra-ui/react';

interface ItemValueSummary {
  minValue: number;
  currentMinValue: number;
  currentMaxValue: number;
  average: number;
  storeNumbers: number;
}

interface ItemValueSummaryProps {
  data: ItemValueSummary;
}

export function ItemValueSummary({ data }: ItemValueSummaryProps) {
  const percentageChange = ((data.currentMinValue - data.minValue) / data.minValue) * 100;
  const isPositiveChange = percentageChange > 0;

  return (
    <Card>
      <CardBody>
        <VStack spacing={4} align="start">
          <Flex justify="space-between" w="100%" align="center">
            <Box>
              <Flex align="center" gap={2}>
                <Image src="/coin.svg" alt="Coin" boxSize="24px" />
                <Text fontSize="sm" color="gray.500">ZNY</Text>
              </Flex>
              <Text fontSize="lg" fontWeight="bold">
                Min {data.currentMinValue.toLocaleString()}z
              </Text>
            </Box>
            <Box textAlign="right">
              <Text fontSize="sm" color="gray.500">{data.storeNumbers} lojas</Text>
              <Flex gap={4}>
                <Box>
                  <Text fontSize="sm" color="gray.500">Low</Text>
                  <Text fontSize="sm">{data.minValue.toLocaleString()}z</Text>
                </Box>
                <Box>
                  <Text fontSize="sm" color="gray.500">High</Text>
                  <Text fontSize="sm">{data.currentMaxValue.toLocaleString()}z</Text>
                </Box>
              </Flex>
            </Box>
          </Flex>
          <Box w="100%">
            <Text fontSize="sm" color="gray.500">Média</Text>
            <Text fontSize="lg">{data.average.toLocaleString()}z</Text>
          </Box>
          <Flex justify="space-between" w="100%" align="center">
            <Text 
              fontSize="sm" 
              color={isPositiveChange ? "green.500" : "red.500"}
            >
              {isPositiveChange ? "+" : ""}{percentageChange.toFixed(1)}% {isPositiveChange ? "↗" : "↘"}
            </Text>
            <Text fontSize="sm" color="blue.500" cursor="pointer">
              Ver Mais
            </Text>
          </Flex>
        </VStack>
      </CardBody>
    </Card>
  );
} 