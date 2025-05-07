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
  NumberInput,
  NumberInputField,
  ModalFooter,
  useToast,
  Text,
} from '@chakra-ui/react';
import { useEffect, useState, useRef } from 'react';
import { useAuth } from '@/contexts/AuthContext';
import { useServer } from '@/contexts/ServerContext';
import { CallbackResumeViewModel } from '@/types/auth';
import { ProtectedRoute } from '@/components/auth/ProtectedRoute';
import { Pagination } from '@/components/ui/Pagination';
import { LoadingList } from '@/components/LoadingList';

export function UserCallbacks() {
  const { isAuthenticated, user } = useAuth();
  const { isOpen, onOpen, onClose } = useDisclosure();
  const [callbacks, setCallbacks] = useState<CallbackResumeViewModel[]>([]);
  const [loading, setLoading] = useState(true);
  const { currentServer } = useServer();
  const toast = useToast();
  const [currentPage, setCurrentPage] = useState(1);
  const [rowsPerPage, setRowsPerPage] = useState(2); // valor inicial
  const cardRef = useRef<HTMLDivElement>(null);
  const [newItemId, setNewItemId] = useState<number | undefined>();
  const [newItemPrice, setNewItemPrice] = useState<number | undefined>();

  useEffect(() => {
    function updateRowsPerPageByCard() {
      if (!cardRef.current) return;
      const cardHeight = cardRef.current.offsetHeight;
      const controlsHeight = 180; // valor ajustado para controles
      const rowHeight = 48; // altura estimada de uma linha da tabela
      const availableHeight = cardHeight - controlsHeight;
      // Limitar entre 1 e 5 linhas por página
      const possibleRows = Math.max(1, Math.min(5, Math.floor(availableHeight / rowHeight)));
      setRowsPerPage(possibleRows);
    }
    updateRowsPerPageByCard();
    const resizeObserver = new (window as any).ResizeObserver(updateRowsPerPageByCard);
    if (cardRef.current) {
      resizeObserver.observe(cardRef.current);
    }
    window.addEventListener('resize', updateRowsPerPageByCard);
    return () => {
      if (cardRef.current) resizeObserver.disconnect();
      window.removeEventListener('resize', updateRowsPerPageByCard);
    };
  }, [callbacks.length]);

  useEffect(() => {
    if (isAuthenticated && user) {
      fetchCallbacks();
    }
  }, [isAuthenticated, user, currentServer]);

  const totalPages = Math.ceil(callbacks.length / rowsPerPage);
  const paginatedCallbacks = callbacks.slice((currentPage - 1) * rowsPerPage, currentPage * rowsPerPage);

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

  const handleAddCallback = async () => {
    if (!newItemId || !newItemPrice) {
      toast({
        title: 'Preencha todos os campos',
        status: 'warning',
        duration: 3000,
        isClosable: true,
      });
      return;
    }
    try {
      const response = await fetch(`/api/${currentServer}/callbacks-items`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          itemId: newItemId,
          itemPrice: newItemPrice,
        }),
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
      setNewItemId(undefined);
      setNewItemPrice(undefined);
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

  const handleRemoveCallback = async (callbackId: string) => {
    try {
      const response = await fetch(`/api/${currentServer}/callbacks-items/${callbackId}`, {
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

  if (loading) {
    return (
      <Card>
        <CardBody>
          <LoadingList />
        </CardBody>
      </Card>
    );
  }

  return (
    <ProtectedRoute>
      <Card ref={cardRef} height="100%" minH={0}>
        <CardBody>
          <VStack spacing={4} align="stretch" flex="1" minHeight={0}>
            <Flex justify="space-between" align="center">
              <Heading size="md">Notificações de Preço</Heading>
              <Button colorScheme="blue" size="sm" onClick={onOpen}>
                + Adicionar
              </Button>
            </Flex>
            <Box flex="1" minH={0} display="flex" flexDirection="column">
              <Box flex="1" minH={0}>
                <Table variant="simple" width="100%">
                  <Thead>
                    <Tr>
                      <Th>Item</Th>
                      <Th isNumeric>Preço Alvo</Th>
                      <Th>Tipo</Th>
                      <Th>Ações</Th>
                    </Tr>
                  </Thead>
                  <Tbody>
                    {paginatedCallbacks.map((callback) => (
                      <Tr key={`${callback.itemId}-${callback.server}`}>
                        <Td>{callback.itemId}</Td>
                        <Td isNumeric>{callback.itemPrice.toLocaleString()}z</Td>
                        <Td>
                          <Badge colorScheme={callback.storeType === 'VendingStore' ? 'green' : 'blue'}>
                            {callback.storeType === 'BuyingStore' ? 'Compra' : 'Venda'}
                          </Badge>
                        </Td>
                        <Td>
                          <Button
                            size="sm"
                            colorScheme="red"
                            variant="ghost"
                            onClick={async () => {
                              try {
                                await handleRemoveCallback(callback.id);
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
              </Box>
              {/* Paginação */}
              {totalPages > 1 && (
                <Pagination
                  currentPage={currentPage}
                  totalPages={totalPages}
                  onPageChange={setCurrentPage}
                />
              )}
            </Box>
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
                    <NumberInput min={0} value={newItemId ?? ''} onChange={(_, v) => setNewItemId(Number.isNaN(v) ? undefined : v)}>
                      <NumberInputField placeholder="Digite o ID do item..." />
                    </NumberInput>
                  </FormControl>
                  <FormControl>
                    <FormLabel>Preço Alvo</FormLabel>
                    <NumberInput min={0} value={newItemPrice ?? ''} onChange={(_, v) => setNewItemPrice(Number.isNaN(v) ? undefined : v)}>
                      <NumberInputField placeholder="Digite o preço alvo..." />
                    </NumberInput>
                  </FormControl>
                </VStack>
              </ModalBody>
              <ModalFooter>
                <Button variant="ghost" mr={3} onClick={onClose}>
                  Cancelar
                </Button>
                <Button colorScheme="blue" onClick={handleAddCallback}>
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