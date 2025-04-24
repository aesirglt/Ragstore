'use client';

import { Container, Heading, Text, VStack } from '@chakra-ui/react';

export default function ConfiguracoesPage() {
  return (
    <Container maxW="container.xl" py={8}>
      <VStack spacing={6} align="stretch">
        <Heading as="h1" size="xl">Configurações</Heading>
        <Text>Em breve você poderá configurar suas preferências aqui.</Text>
      </VStack>
    </Container>
  );
} 