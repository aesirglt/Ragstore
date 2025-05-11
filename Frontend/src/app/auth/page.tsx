'use client';

import { Container, Box, VStack, Heading, Text, Flex } from '@chakra-ui/react';
import { LoginForm } from '@/components/auth/LoginForm';
import { useAuth } from '@/contexts/AuthContext';
import { useRouter } from 'next/navigation';
import { useEffect } from 'react';

export default function AuthPage() {
  const { isAuthenticated } = useAuth();
  const router = useRouter();

  useEffect(() => {
    if (isAuthenticated) {
      router.push('/');
    }
  }, [isAuthenticated, router]);

  return (
    <Flex minH="100vh" align="center" justify="center" bg="gray.50">
      <Container maxW="container.sm" py={8}>
        <VStack spacing={8}>
          <Box textAlign="center">
            <Heading>Bem-vindo ao RagnaComercio</Heading>
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