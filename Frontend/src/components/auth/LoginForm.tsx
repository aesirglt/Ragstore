import { Button, VStack, useToast } from '@chakra-ui/react';
import { FaGoogle, FaDiscord } from 'react-icons/fa';
import { useAuth } from '@/contexts/AuthContext';

export function LoginForm() {
  const { loading } = useAuth();
  const toast = useToast();

  const handleGoogleLogin = async () => {
    try {
      const response = await fetch('/api/auth/google');
      if (!response.ok) {
        throw new Error('Falha ao iniciar login com Google');
      }
      const data = await response.json();
      window.location.href = data.url;
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
      const response = await fetch('/api/auth/discord');
      if (!response.ok) {
        throw new Error('Falha ao iniciar login com Discord');
      }
      const data = await response.json();
      window.location.href = data.url;
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
    <VStack spacing={4} width="100%">
      <Button
        leftIcon={<FaGoogle />}
        colorScheme="red"
        width="100%"
        onClick={handleGoogleLogin}
        isLoading={loading}
      >
        Entrar com Google
      </Button>
      <Button
        leftIcon={<FaDiscord />}
        colorScheme="blue"
        width="100%"
        onClick={handleDiscordLogin}
        isLoading={loading}
      >
        Entrar com Discord
      </Button>
    </VStack>
  );
} 