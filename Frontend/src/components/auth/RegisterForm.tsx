import {
  Box,
  Button,
  FormControl,
  FormLabel,
  Input,
  VStack,
  Text,
  Divider,
  useToast,
} from '@chakra-ui/react';
import { useState } from 'react';
import { useAuth } from '@/contexts/AuthContext';

export function RegisterForm() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const { register, getGoogleAuthUrl, getDiscordAuthUrl } = useAuth();
  const toast = useToast();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsLoading(true);

    if (password !== confirmPassword) {
      toast({
        title: 'Erro',
        description: 'As senhas não coincidem',
        status: 'error',
        duration: 3000,
        isClosable: true,
      });
      setIsLoading(false);
      return;
    }

    try {
      await register(email, password, confirmPassword);
    } catch (error) {
      toast({
        title: 'Erro',
        description: 'Falha no registro. Tente novamente.',
        status: 'error',
        duration: 3000,
        isClosable: true,
      });
    } finally {
      setIsLoading(false);
    }
  };

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
    <Box as="form" onSubmit={handleSubmit} width="100%" maxW="400px">
      <VStack spacing={4}>
        <FormControl isRequired>
          <FormLabel>Email</FormLabel>
          <Input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </FormControl>

        <FormControl isRequired>
          <FormLabel>Senha</FormLabel>
          <Input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </FormControl>

        <FormControl isRequired>
          <FormLabel>Confirmar Senha</FormLabel>
          <Input
            type="password"
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)}
          />
        </FormControl>

        <Button
          type="submit"
          colorScheme="blue"
          width="100%"
          isLoading={isLoading}
        >
          Registrar
        </Button>

        <Divider />

        <Text>Ou registre-se com</Text>

        <Button
          leftIcon={<img src="/google-icon.png" alt="Google" width="20" />}
          onClick={handleGoogleLogin}
          width="100%"
          variant="outline"
        >
          Google
        </Button>

        <Button
          leftIcon={<img src="/discord-icon.png" alt="Discord" width="20" />}
          onClick={handleDiscordLogin}
          width="100%"
          variant="outline"
          colorScheme="purple"
        >
          Discord
        </Button>
      </VStack>
    </Box>
  );
} 