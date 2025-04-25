'use client';

import {
  Box,
  Link,
  Icon,
  Text,
  HStack,
  useColorModeValue,
} from '@chakra-ui/react';
import { FaGift } from 'react-icons/fa';

export function PromotionBanner() {
  const bgColor = useColorModeValue('white', 'gray.800');
  const borderColor = useColorModeValue('gray.200', 'gray.700');

  return (
    <Box
      p={4}
      bg={bgColor}
      borderRadius="md"
      border="1px"
      borderColor={borderColor}
      mb={6}
    >
      <HStack spacing={2}>
        <Icon as={FaGift} color="orange.400" />
        <Text>
          Você sabia que pode nos ajudar a manter o site online?{' '}
          <Link
            color="blue.500"
            href="#"
            display="inline-flex"
            alignItems="center"
          >
            Clique aqui para saber mais...
            <Icon as={FaGift} ml={1} fontSize="sm" />
          </Link>
        </Text>
      </HStack>
    </Box>
  );
} 