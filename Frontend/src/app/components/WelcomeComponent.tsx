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
            <Heading size="lg">{user ? user.name : 'Meu bem'}</Heading>
          </Box>
          <Text color="gray.600">
            {user 
              ? 'Veja aqui tudo sobre como ajudar o site e a experiência VIP'
              : 'Entre em sua conta e desfrute da experiência completa.'}
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