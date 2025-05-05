'use client';

import { Container, Box, VStack, Heading, Text, Flex } from '@chakra-ui/react';
import { LoginForm } from '@/components/auth/LoginForm';

export default function AuthPage() {
  return (
    <Flex minH="100vh" align="center" justify="center" bg="gray.50">
      <Container maxW="container.sm" py={8}>
        <VStack spacing={8}>
          <Box textAlign="center">
            <Heading>Bem-vindo ao Ragstore</Heading>
            <Text color="gray.500">Entre com um provedor para acessar sua conta</Text>
          </Box>
          <Box width="100%">
            <LoginForm />
          </Box>
        </VStack>
      </Container>
    </Flex>
  );
} 