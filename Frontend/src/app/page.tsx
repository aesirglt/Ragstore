'use client';

import {
  Container,
  Heading,
  Text,
  VStack,
  Box,
  Card,
  CardBody,
  SimpleGrid,
  Flex,
  Button,
} from '@chakra-ui/react';
import { LastTransactions } from './components/LastTransactions';

export default function HomePage() {
  return (
    <Container maxW="container.xl" py={8}>
      <SimpleGrid columns={{ base: 1, lg: 2 }} spacing={8}>
        {/* Coluna da esquerda */}
        <VStack align="stretch" spacing={6}>
          <Card>
            <CardBody>
              <VStack spacing={4} align="start">
                <Box>
                  <Text fontSize="sm" color="gray.500">Seja bem-vindo,</Text>
                  <Heading size="lg">Lindinho</Heading>
                </Box>
                <Text color="gray.600">
                  Entre em sua conta e desfrute da experiência completa de nossa DB.
                </Text>
                <Button colorScheme="orange" size="sm">
                  Login →
                </Button>
              </VStack>
            </CardBody>
          </Card>

          <Card>
            <CardBody>
              <LastTransactions />
            </CardBody>
          </Card>
        </VStack>

        {/* Coluna da direita */}
        <VStack align="stretch" spacing={6}>
          <Card>
            <CardBody>
              <VStack spacing={4} align="start">
                <Flex justify="space-between" w="100%" align="center">
                  <Box>
                    <Text fontSize="sm" color="gray.500">IST / ZEN</Text>
                    <Text fontSize="lg" fontWeight="bold">Last 500.000z</Text>
                  </Box>
                  <Box textAlign="right">
                    <Text fontSize="sm" color="gray.500">24h</Text>
                    <Text fontSize="lg" fontWeight="bold" color="green.500">+1.500.000z</Text>
                  </Box>
                </Flex>
                <Box w="100%">
                  <Text fontSize="sm" color="gray.500">Vol</Text>
                  <Text fontSize="lg">22.255.901.824z</Text>
                </Box>
                <Box w="100%">
                  <Text fontSize="sm" color="red.500">-15.0% ▼</Text>
                  <Text fontSize="sm" color="gray.500">Ver Mais</Text>
                </Box>
              </VStack>
            </CardBody>
          </Card>

          <Card>
            <CardBody>
              <VStack spacing={4} align="start">
                <Heading size="md">Meus Marcadores</Heading>
                <Box p={8} w="100%" textAlign="center">
                  <Text color="gray.500">Nenhum item marcado</Text>
                </Box>
                <Button colorScheme="blue" size="sm" alignSelf="center">
                  + Marcador
                </Button>
              </VStack>
            </CardBody>
          </Card>
        </VStack>
      </SimpleGrid>
    </Container>
  );
} 