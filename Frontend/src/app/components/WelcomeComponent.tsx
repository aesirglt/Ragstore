import {
  Card,
  CardBody,
  VStack,
  Box,
  Text,
  Heading,
  Button,
} from '@chakra-ui/react';
import { useRouter } from 'next/navigation';
import { useAuth } from '@/contexts/AuthContext';

export function WelcomeComponent() {
  const router = useRouter();
  const { user } = useAuth();

  return (
    <Card>
      <CardBody>
        <VStack spacing={4} align="start">
          <Box>
            <Text fontSize="sm" color="gray.500">Seja bem-vindo,</Text>
            <Heading size="lg">{user ? user.name : 'Aventureiro'}</Heading>
          </Box>
          <Text color="gray.600">
            {user 
              ? 'Seja VIP e desfrute de toda a capacidade de criar alertas de preço para muitos items com um limite maior que o usuário comum.'
              : 'Faça login para desfrutar da experiência completa, com acesso a todas as funcionalidades, possibilidade de receber alertas de preço e muito mais.'}
          </Text>
          <Button 
            colorScheme="orange" 
            size="sm" 
            onClick={() => router.push(user ? '/infos' : '/auth')}
          >
            {user ? 'Informações →' : 'Login →'}
          </Button>
        </VStack>
      </CardBody>
    </Card>
  );
} 