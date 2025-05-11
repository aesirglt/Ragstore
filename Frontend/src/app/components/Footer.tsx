'use client';

import {
  Box,
  Container,
  Stack,
  Text,
  Link,
  useColorModeValue,
} from '@chakra-ui/react';

export function Footer() {
  const borderColor = useColorModeValue('gray.200', 'gray.700');
  const textColor = useColorModeValue('gray.600', 'gray.400');

  return (
    <Box
      borderTop="1px"
      borderColor={borderColor}
      py={4}
      width="100%"
      bg={useColorModeValue('white', 'gray.900')}
    >
      <Container maxW="container.xl">
        <Stack
          direction={{ base: 'column', md: 'row' }}
          spacing={4}
          justify="space-between"
          align="center"
        >
          <Text fontSize="sm" color={textColor}>
            © 2022 RagnaComercio. Ragnarok Database Licensed to RagnaComercio by v3zpr
          </Text>
          <Stack direction="row" spacing={6}>
            <Link
              href="#"
              fontSize="sm"
              color={textColor}
              _hover={{ color: 'orange.500' }}
            >
              Linkedin
            </Link>
            <Link
              href="#"
              fontSize="sm"
              color={textColor}
              _hover={{ color: 'orange.500' }}
            >
              Wiki
            </Link>
            <Link
              href="#"
              fontSize="sm"
              color={textColor}
              _hover={{ color: 'orange.500' }}
            >
              Doações
            </Link>
            <Link
              href="#"
              fontSize="sm"
              color={textColor}
              _hover={{ color: 'orange.500' }}
            >
              Discord
            </Link>
          </Stack>
        </Stack>
      </Container>
    </Box>
  );
} 