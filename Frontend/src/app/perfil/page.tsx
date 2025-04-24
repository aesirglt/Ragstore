'use client';

import { Container, Heading, Text, VStack } from '@chakra-ui/react';

export default function PerfilPage() {
  return (
    <Container maxW="container.xl" py={8}>
      <VStack spacing={6} align="stretch">
        <Heading as="h1" size="xl">Meu Perfil</Heading>
        <Text>Em breve você poderá editar seu perfil aqui.</Text>
      </VStack>
    </Container>
  );
} 