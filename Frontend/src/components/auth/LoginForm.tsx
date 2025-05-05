import {
  Box,
  Button,
  VStack,
  Text,
  useToast,
  Icon,
} from '@chakra-ui/react';
import { FaGoogle, FaDiscord } from 'react-icons/fa';
import { useAuth } from '@/contexts/AuthContext';

export function LoginForm() {
  const { getGoogleAuthUrl, getDiscordAuthUrl } = useAuth();
  const toast = useToast();

  const handleGoogleLogin = async () => {
    try {
      const url = await getGoogleAuthUrl();
      window.location.href = url;
    } catch (error) {
      toast({
        title: 'Erro',
        description: 'Falha ao iniciar login com Google',
        status: 'error',
        duration: 3000,
        isClosable: true,
      });
    }
  };

  const handleDiscordLogin = async () => {
    try {
      const url = await getDiscordAuthUrl();
      window.location.href = url;
    } catch (error) {
      toast({
        title: 'Erro',
        description: 'Falha ao iniciar login com Discord',
        status: 'error',
        duration: 3000,
        isClosable: true,
      });
    }
  };

  return (
    <Box width="100%" maxW="400px" mx="auto">
      <VStack spacing={4} align="center" width="100%">
        <Text textAlign="center" width="100%">Entre com um dos provedores abaixo:</Text>
        <Button
          leftIcon={<Icon as={FaGoogle} boxSize={5} />}
          onClick={handleGoogleLogin}
          width="100%"
          variant="outline"
          colorScheme="gray"
        >
          Entrar com Google
        </Button>
        <Button
          leftIcon={<Icon as={FaDiscord} boxSize={5} />}
          onClick={handleDiscordLogin}
          width="100%"
          variant="outline"
          colorScheme="purple"
        >
          Entrar com Discord
        </Button>
      </VStack>
    </Box>
  );
} 