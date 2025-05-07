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
  Checkbox,
  useColorMode,
  Input,
  useToast,
} from '@chakra-ui/react';
import { UserCallbacks } from '../components/UserCallbacks';
import { ProtectedRoute } from '@/components/auth/ProtectedRoute';
import { useAuth } from '@/contexts/AuthContext';
import { useState } from 'react';

export default function ProfilePage() {
  const { user, updateUser } = useAuth();
  const { colorMode, toggleColorMode } = useColorMode();
  const [isEditingAvatar, setIsEditingAvatar] = useState(false);
  const toast = useToast();

  const handleAvatarChange = async (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0];
    if (file) {
      toast({
        title: 'Avatar atualizado',
        status: 'success',
        duration: 3000,
      });
    }
  };

  const handleNotificationToggle = async () => {
    if (user) {
      await updateUser({
        ...user,
        receivePriceAlerts: !user.receivePriceAlerts
      });
    }
  };

  const handleRemoveCallback = async (callbackId: string) => {
    // TODO: Implementar remoção do callback
    toast({
      title: 'Callback removido',
      status: 'success',
      duration: 3000,
    });
  };

  if (!user) return null;

  return (
    <ProtectedRoute>
      <Container maxW="container.xl" py={8} height="100%">
        <Box height="100%" display="flex" flexDirection="column">
          <VStack spacing={8} align="stretch" flex="1" minHeight={0}>
            <SimpleGrid columns={{ base: 1, lg: 2 }} spacing={8}>
              {/* Coluna da esquerda - Informações do perfil */}
              <VStack align="stretch" spacing={6}>
                <Card>
                  <CardBody>
                    <VStack spacing={6} align="start">
                      <Flex gap={4} align="center">
                        <Box position="relative">
                          <Avatar size="xl" name={user.name} src={user.avatarUrl} />
                          <Input
                            type="file"
                            accept="image/*"
                            position="absolute"
                            top="0"
                            left="0"
                            width="100%"
                            height="100%"
                            opacity="0"
                            cursor="pointer"
                            onChange={handleAvatarChange}
                          />
                        </Box>
                        <Box>
                          <Heading size="lg">{user.name}</Heading>
                          <Text color="gray.500">Membro desde {user.memberSince}</Text>
                        </Box>
                      </Flex>
                      
                      <Divider />

                      <Stack spacing={4} width="100%">
                        <Box>
                          <Text fontWeight="bold" mb={2}>Email</Text>
                          <Text color="gray.600">{user.email}</Text>
                        </Box>

                        <Box>
                          <Text fontWeight="bold" mb={2}>Status da Conta</Text>
                          <Badge colorScheme={user.isActive ? "green" : "red"}>
                            {user.isActive ? "Ativo" : "Inativo"}
                          </Badge>
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
                          <Text color="gray.500">Callbacks</Text>
                          <Text fontSize="2xl">{user.callbacksCount}</Text>
                        </Box>
                        <Box>
                          <Text color="gray.500">Pesquisas</Text>
                          <Text fontSize="2xl">{user.searchCount}</Text>
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
                          <Checkbox
                            isChecked={user.receivePriceAlerts}
                            onChange={handleNotificationToggle}
                          >
                            Receber alertas de preço
                          </Checkbox>
                        </Box>

                        <Box>
                          <Text fontWeight="bold" mb={2}>Tema</Text>
                          <Button
                            size="sm"
                            onClick={toggleColorMode}
                          >
                            {colorMode === 'light' ? '🌙 Modo Escuro' : '☀️ Modo Claro'}
                          </Button>
                        </Box>
                      </Stack>
                    </VStack>
                  </CardBody>
                </Card>
              </VStack>
            </SimpleGrid>
            <Box flex="1" minHeight={0}>
              <UserCallbacks />
            </Box>
          </VStack>
        </Box>
      </Container>
    </ProtectedRoute>
  );
} 