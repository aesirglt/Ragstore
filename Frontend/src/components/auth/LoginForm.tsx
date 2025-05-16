import { Button, VStack, useToast } from '@chakra-ui/react';
import { FaGoogle, FaDiscord } from 'react-icons/fa';
import { useAuth } from '@/contexts/AuthContext';

export function LoginForm() {
  const { loading } = useAuth();
  const toast = useToast();

  const handleGoogleLogin = () => {
    const redirect = encodeURIComponent(window.location.href);
    window.location.href = `/api/auth/google?redirect=${redirect}`;
  };

  const handleDiscordLogin = () => {
    const redirect = encodeURIComponent(window.location.href);
    window.location.href = `/api/auth/discord?redirect=${redirect}`;
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