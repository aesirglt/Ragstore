'use client';

import {
  Container,
  Box,
  Heading,
  Text,
  VStack,
  SimpleGrid,
  Card,
  CardBody,
  Button,
  Avatar,
  Flex,
  Divider,
  Stack,
  Badge,
} from '@chakra-ui/react';
import { UserCallbacks } from '../components/UserCallbacks';
import { ProtectedRoute } from '@/components/auth/ProtectedRoute';

export default function ProfilePage() {
  return (
    <ProtectedRoute>
      <Container maxW="container.xl" py={8}>
        <VStack spacing={8} align="stretch">
          <SimpleGrid columns={{ base: 1, lg: 2 }} spacing={8}>
            {/* Coluna da esquerda - Informações do perfil */}
            <VStack align="stretch" spacing={6}>
              <Card>
                <CardBody>
                  <VStack spacing={6} align="start">
                    <Flex gap={4} align="center">
                      <Avatar size="xl" name="Meu amor" />
                      <Box>
                        <Heading size="lg">Meu amor</Heading>
                        <Text color="gray.500">Membro desde Abril 2024</Text>
                      </Box>
                    </Flex>
                    
                    <Divider />

                    <Stack spacing={4} width="100%">
                      <Box>
                        <Text fontWeight="bold" mb={2}>Email</Text>
                        <Text color="gray.600">usuario@email.com</Text>
                      </Box>

                      <Box>
                        <Text fontWeight="bold" mb={2}>Status da Conta</Text>
                        <Badge colorScheme="green">Ativo</Badge>
                      </Box>
                    </Stack>

                    <Button colorScheme="blue" size="sm">
                      Editar Perfil
                    </Button>
                  </VStack>
                </CardBody>
              </Card>
            </VStack>

            {/* Coluna da direita - Estatísticas e preferências */}
            <VStack align="stretch" spacing={6}>
              <Card>
                <CardBody>
                  <VStack spacing={4} align="start">
                    <Heading size="md">Estatísticas</Heading>
                    
                    <SimpleGrid columns={2} spacing={4} width="100%">
                      <Box>
                        <Text color="gray.500">Itens Marcados</Text>
                        <Text fontSize="2xl">0</Text>
                      </Box>
                      <Box>
                        <Text color="gray.500">Pesquisas</Text>
                        <Text fontSize="2xl">12</Text>
                      </Box>
                    </SimpleGrid>
                  </VStack>
                </CardBody>
              </Card>

              <Card>
                <CardBody>
                  <VStack spacing={4} align="start">
                    <Heading size="md">Preferências</Heading>
                    
                    <Stack spacing={4} width="100%">
                      <Box>
                        <Text fontWeight="bold" mb={2}>Notificações</Text>
                        <Text color="gray.600">Receber alertas de preço</Text>
                      </Box>

                      <Box>
                        <Text fontWeight="bold" mb={2}>Tema</Text>
                        <Text color="gray.600">Claro</Text>
                      </Box>
                    </Stack>
                  </VStack>
                </CardBody>
              </Card>
            </VStack>
          </SimpleGrid>
          <UserCallbacks />
        </VStack>
      </Container>
    </ProtectedRoute>
  );
} 