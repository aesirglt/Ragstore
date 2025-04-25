'use client';

import {
  Box,
  Card,
  CardBody,
  Text,
  HStack,
  VStack,
  Image,
  Flex,
  IconButton,
} from '@chakra-ui/react';
import { ChevronLeftIcon, ChevronRightIcon } from '@chakra-ui/icons';
import { useState } from 'react';
import { TopItemViewModel } from '@/types/api/viewmodels/TopItemViewModel';

interface TopItemsSliderProps {
  items: TopItemViewModel[];
}

export function TopItemsSlider({ items }: TopItemsSliderProps) {
  const [currentIndex, setCurrentIndex] = useState(0);

  const nextSlide = () => {
    setCurrentIndex((prev) => (prev + 1) % items.length);
  };

  const prevSlide = () => {
    setCurrentIndex((prev) => (prev - 1 + items.length) % items.length);
  };

  const currentItem = items[currentIndex];

  return (
    <Card size="sm">
      <CardBody py={2} px={2}>
        <Flex justify="space-between" align="center" gap={1}>
          <IconButton
            aria-label="Previous item"
            icon={<ChevronLeftIcon />}
            onClick={prevSlide}
            variant="ghost"
            size="sm"
            p={0}
          />

          <VStack spacing={1} flex={1} align="stretch">
            {/* Cabeçalho com nome do item e imagem */}
            <HStack spacing={1}>
              <Image
                src={currentItem?.imageUrl ?? "/items/default.png"}
                alt={currentItem?.itemName ?? "Item padrão"}
                boxSize="16px"
                objectFit="contain"
              />
              <Text fontSize="xs" fontWeight="bold">{currentItem?.itemName?? "Item padrão"}</Text>
            </HStack>

            {/* Preço mais recente e Variação */}
            <HStack justify="space-between" fontSize="xs">
              <Text color="green.500" fontWeight="semibold">
                Last {currentItem?.average ?? "0"}z
              </Text>
              <Text
                color={currentItem?.percentageChange >= 0 ? "green.500" : "red.500"}
                fontWeight="semibold"
              >
                {currentItem?.percentageChange >= 0 ? "+" : ""}{currentItem?.percentageChange?.toFixed(1)}%
              </Text>
            </HStack>

            {/* Período */}
            <HStack spacing={2} justify="center" fontSize="xs">
              <Text color="gray.500">24h</Text>
              <Text color="gray.500">7d</Text>
            </HStack>

            {/* Preços baixo e alto */}
            <HStack justify="space-between" fontSize="xs">
              <Box>
                <Text color="gray.500" fontSize="2xs">Low</Text>
                <Text color="red.500">{currentItem?.currentMinValue?.toLocaleString()}z</Text>
              </Box>
              <Box>
                <Text color="gray.500" fontSize="2xs">High</Text>
                <Text color="green.500">{currentItem?.currentMaxValue?.toLocaleString()}z</Text>
              </Box>
            </HStack>

            {/* Volume e Ver Mais */}
            <HStack justify="space-between" fontSize="xs">
              <Box>
                <Text color="gray.500" fontSize="2xs">Vol</Text>
                <Text>{currentItem?.storeNumbers?.toLocaleString()}z</Text>
              </Box>
              <Text color="blue.500" cursor="pointer" fontSize="2xs">Ver Mais</Text>
            </HStack>
          </VStack>

          <IconButton
            aria-label="Next item"
            icon={<ChevronRightIcon />}
            onClick={nextSlide}
            variant="ghost"
            size="sm"
            p={0}
          />
        </Flex>
      </CardBody>
    </Card>
  );
} 