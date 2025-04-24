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
} from '@chakra-ui/react';
import { useEffect, useState } from 'react';

interface Callback {
  id: string;
  itemId: string;
  itemName: string;
  targetPrice: number;
  isActive: boolean;
}

export function UserCallbacks() {
  const { isOpen, onOpen, onClose } = useDisclosure();
  const [callbacks, setCallbacks] = useState<Callback[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    // TODO: Implementar chamada à API
    setCallbacks([
      {
        id: '1',
        itemId: '123',
        itemName: 'Diário de Aventuras [HARDCORE]',
        targetPrice: 250000000,
        isActive: true,
      },
      {
        id: '2',
        itemId: '456',
        itemName: 'Instance Stone',
        targetPrice: 800000,
        isActive: true,
      }
    ]);
    setLoading(false);
  }, []);

  const handleAddCallback = (data: any) => {
    // TODO: Implementar chamada à API para criar novo callback
    onClose();
  };

  return (
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
                <Th>Status</Th>
                <Th>Ações</Th>
              </Tr>
            </Thead>
            <Tbody>
              {callbacks.map((callback) => (
                <Tr key={callback.id}>
                  <Td>{callback.itemName}</Td>
                  <Td isNumeric>{callback.targetPrice.toLocaleString()}z</Td>
                  <Td>
                    <Badge colorScheme={callback.isActive ? "green" : "red"}>
                      {callback.isActive ? "Ativo" : "Inativo"}
                    </Badge>
                  </Td>
                  <Td>
                    <Button
                      size="sm"
                      colorScheme="red"
                      variant="ghost"
                    >
                      Remover
                    </Button>
                  </Td>
                </Tr>
              ))}
            </Tbody>
          </Table>
        </VStack>

        {/* Modal para adicionar novo callback */}
        <Modal isOpen={isOpen} onClose={onClose}>
          <ModalOverlay />
          <ModalContent>
            <ModalHeader>Adicionar Notificação</ModalHeader>
            <ModalCloseButton />
            <ModalBody>
              <VStack spacing={4}>
                <FormControl>
                  <FormLabel>Item</FormLabel>
                  <Input placeholder="Pesquisar item..." />
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
  );
} 