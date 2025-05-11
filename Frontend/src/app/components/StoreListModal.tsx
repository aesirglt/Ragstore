'use client';

import {
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalBody,
  ModalCloseButton,
  Table,
  Thead,
  Tbody,
  Tr,
  Th,
  Td,
  Text,
  Box,
  Button,
  HStack,
  IconButton,
  useToast,
} from '@chakra-ui/react';
import { useState } from 'react';
import { useStores } from '@/hooks/useStores';
import { CopyIcon } from '@chakra-ui/icons';
import { LoadingList } from '@/components/LoadingList';

interface StoreListModalProps {
  isOpen: boolean;
  onClose: () => void;
  itemId: number;
  server: string;
}

export function StoreListModal({ isOpen, onClose, itemId, server }: StoreListModalProps) {
  const [currentPage, setCurrentPage] = useState(1);
  const pageSize = 10;
  const toast = useToast();

  const { data, isLoading, error } = useStores({
    server,
    itemId,
    page: currentPage,
    pageSize,
  });

  const handleNextPage = () => {
    setCurrentPage((prev) => prev + 1);
  };

  const handlePrevPage = () => {
    setCurrentPage((prev) => Math.max(prev - 1, 1));
  };

  const handleCopyNavi = (location: string) => {
    const naviCommand = `/navi ${location}`;
    navigator.clipboard.writeText(naviCommand);
    toast({
      title: "Comando copiado!",
      status: "success",
      duration: 2000,
      isClosable: true,
    });
  };

  return (
    <Modal isOpen={isOpen} onClose={onClose} size="full" isCentered>
      <ModalOverlay />
      <ModalContent maxW="fit-content" mx="auto" minH="300px" my="auto" borderRadius="lg">
        <ModalHeader>Lojinhas com o Item</ModalHeader>
        <ModalCloseButton />
        <ModalBody pb={6} overflowY="auto" display="flex" flexDirection="column" flex="1">
          {isLoading ? (
            <Box display="flex" justifyContent="center" py={4}>
              <LoadingList />
            </Box>
          ) : error ? (
            <Text color="red.500">Erro ao carregar lojinhas</Text>
          ) : data && data.length > 0 ? (
            <>
              <Box overflowX="auto" flex="1">
                <Table variant="simple" size="sm">
                  <Thead>
                    <Tr>
                      <Th whiteSpace="nowrap">Nome da Loja</Th>
                      <Th whiteSpace="nowrap">Personagem</Th>
                      <Th whiteSpace="nowrap">Preço do item</Th>
                      <Th whiteSpace="nowrap">Quantidade</Th>
                      <Th whiteSpace="nowrap">Localização</Th>
                    </Tr>
                  </Thead>
                  <Tbody>
                    {data.map((store) => (
                      <Tr key={store.id}>
                        <Td maxW="200px" overflow="hidden" textOverflow="ellipsis" whiteSpace="nowrap">{store.name}</Td>
                        <Td maxW="200px" overflow="hidden" textOverflow="ellipsis" whiteSpace="nowrap">{store.characterName}</Td>
                        <Td maxW="150px" overflow="hidden" textOverflow="ellipsis" whiteSpace="nowrap">{store.itemPrice}z</Td>
                        <Td maxW="200px" overflow="hidden" textOverflow="ellipsis" whiteSpace="nowrap">{store.quantity}</Td>
                        <Td maxW="250px" overflow="hidden" textOverflow="ellipsis" whiteSpace="nowrap">
                          <HStack spacing={1}>
                            <Text>/navi {store.location}</Text>
                            <IconButton
                              aria-label="Copiar comando"
                              icon={<CopyIcon />}
                              size="xs"
                              variant="ghost"
                              onClick={() => handleCopyNavi(store.location)}
                            />
                          </HStack>
                        </Td>
                      </Tr>
                    ))}
                  </Tbody>
                </Table>
              </Box>
              <HStack justify="flex-end" mt={4}>
                <Button
                  size="sm"
                  onClick={handlePrevPage}
                  isDisabled={currentPage === 1}
                >
                  Anterior
                </Button>
                <Button
                  size="sm"
                  onClick={handleNextPage}
                  isDisabled={data.length < pageSize}
                >
                  Próxima
                </Button>
              </HStack>
            </>
          ) : (
            <Text>Nenhuma lojinha encontrada</Text>
          )}
        </ModalBody>
      </ModalContent>
    </Modal>
  );
} 