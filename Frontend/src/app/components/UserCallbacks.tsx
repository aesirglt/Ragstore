'use client';

import {
  Box,
  Card,
  CardBody,
  Heading,
  VStack,
  Table,
  Thead,
  Tbody,
  Tr,
  Th,
  Td,
  Button,
  Flex,
  Badge,
  useDisclosure,
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalBody,
  ModalCloseButton,
  FormControl,
  FormLabel,
  Input,
  NumberInput,
  NumberInputField,
  ModalFooter,
  useToast,
  Text,
  HStack,
  IconButton,
} from '@chakra-ui/react';
import { useEffect, useState } from 'react';
import { useAuth } from '@/contexts/AuthContext';
import { useServer } from '@/contexts/ServerContext';
import { CallbackResumeViewModel } from '@/types/auth';
import { ProtectedRoute } from '@/components/auth/ProtectedRoute';
import { DeleteIcon } from '@chakra-ui/icons';

interface UserCallbacksProps {
  onRemoveCallback: (callbackId: string) => Promise<void>;
}

export function UserCallbacks({ onRemoveCallback }: UserCallbacksProps) {
  const { isAuthenticated, user } = useAuth();
  const { isOpen, onOpen, onClose } = useDisclosure();
  const [callbacks, setCallbacks] = useState<CallbackResumeViewModel[]>([]);
  const [loading, setLoading] = useState(true);
  const { currentServer } = useServer();
  const toast = useToast();

  useEffect(() => {
    if (isAuthenticated && user) {
      fetchCallbacks();
    }
  }, [isAuthenticated, user, currentServer]);

  const fetchCallbacks = async () => {
    try {
      const response = await fetch(`/api/${currentServer}/callbacks-user`);
      if (!response.ok) {
        throw new Error('Falha ao buscar callbacks');
      }
      const data = await response.json();
      setCallbacks(data);
    } catch (error) {
      toast({
        title: 'Erro',
        description: 'Falha ao carregar notificações',
        status: 'error',
        duration: 3000,
        isClosable: true,
      });
    } finally {
      setLoading(false);
    }
  };

  const handleAddCallback = async (data: any) => {
    try {
      const response = await fetch(`/api/${currentServer}/callbacks`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
      });

      if (!response.ok) {
        throw new Error('Falha ao adicionar callback');
      }

      await fetchCallbacks();
      toast({
        title: 'Sucesso',
        description: 'Notificação adicionada com sucesso',
        status: 'success',
        duration: 3000,
        isClosable: true,
      });
      onClose();
    } catch (error) {
      toast({
        title: 'Erro',
        description: 'Falha ao adicionar notificação',
        status: 'error',
        duration: 3000,
        isClosable: true,
      });
    }
  };

  if (!isAuthenticated) {
    return (
      <Card>
        <CardBody>
          <VStack spacing={4} align="stretch">
            <Heading size="md">Notificações de Preço</Heading>
            <Text>Faça login para gerenciar suas notificações de preço</Text>
          </VStack>
        </CardBody>
      </Card>
    );
  }

  return (
    <ProtectedRoute>
      <Card>
        <CardBody>
          <VStack spacing={4} align="stretch">
            <Flex justify="space-between" align="center">
              <Heading size="md">Notificações de Preço</Heading>
              <Button colorScheme="blue" size="sm" onClick={onOpen}>
                + Adicionar
              </Button>
            </Flex>

            <Table variant="simple">
              <Thead>
                <Tr>
                  <Th>Item</Th>
                  <Th isNumeric>Preço Alvo</Th>
                  <Th>Tipo</Th>
                  <Th>Ações</Th>
                </Tr>
              </Thead>
              <Tbody>
                {callbacks.map((callback) => (
                  <Tr key={`${callback.itemId}-${callback.server}`}>
                    <Td>{callback.itemId}</Td>
                    <Td isNumeric>{callback.itemPrice.toLocaleString()}z</Td>
                    <Td>
                      <Badge colorScheme={callback.storeType === 'vending' ? 'green' : 'blue'}>
                        {callback.storeType === 'vending' ? 'Venda' : 'Compra'}
                      </Badge>
                    </Td>
                    <Td>
                      <Button
                        size="sm"
                        colorScheme="red"
                        variant="ghost"
                        onClick={async () => {
                          try {
                            const response = await fetch(`/api/${currentServer}/callbacks/${callback.itemId}`, {
                              method: 'DELETE',
                            });

                            if (!response.ok) {
                              throw new Error('Falha ao remover callback');
                            }

                            await fetchCallbacks();
                            toast({
                              title: 'Sucesso',
                              description: 'Notificação removida com sucesso',
                              status: 'success',
                              duration: 3000,
                              isClosable: true,
                            });
                          } catch (error) {
                            toast({
                              title: 'Erro',
                              description: 'Falha ao remover notificação',
                              status: 'error',
                              duration: 3000,
                              isClosable: true,
                            });
                          }
                        }}
                      >
                        Remover
                      </Button>
                    </Td>
                  </Tr>
                ))}
              </Tbody>
            </Table>
          </VStack>

          <Modal isOpen={isOpen} onClose={onClose}>
            <ModalOverlay />
            <ModalContent>
              <ModalHeader>Adicionar Notificação</ModalHeader>
              <ModalCloseButton />
              <ModalBody>
                <VStack spacing={4}>
                  <FormControl>
                    <FormLabel>Item ID</FormLabel>
                    <NumberInput min={0}>
                      <NumberInputField placeholder="Digite o ID do item..." />
                    </NumberInput>
                  </FormControl>
                  <FormControl>
                    <FormLabel>Preço Alvo</FormLabel>
                    <NumberInput min={0}>
                      <NumberInputField placeholder="Digite o preço alvo..." />
                    </NumberInput>
                  </FormControl>
                </VStack>
              </ModalBody>

              <ModalFooter>
                <Button variant="ghost" mr={3} onClick={onClose}>
                  Cancelar
                </Button>
                <Button colorScheme="blue" onClick={() => handleAddCallback({})}>
                  Adicionar
                </Button>
              </ModalFooter>
            </ModalContent>
          </Modal>
        </CardBody>
      </Card>
    </ProtectedRoute>
  );
} 