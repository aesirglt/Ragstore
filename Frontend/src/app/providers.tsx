'use client';

import { Box, ChakraProvider } from '@chakra-ui/react';
import { Toaster } from 'react-hot-toast';
import { Navbar } from './components/Navbar';
import { Footer } from './components/Footer';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { ReactNode } from 'react';
import { ServerProvider } from '../contexts/ServerContext';
import { AuthProvider } from '@/contexts/AuthContext';

const queryClient = new QueryClient();

interface ProvidersProps {
  children: ReactNode;
}

export function Providers({ children }: ProvidersProps) {
  return (
    <QueryClientProvider client={queryClient}>
      <ChakraProvider>
        <AuthProvider>
          <ServerProvider>
            <Box height="100vh" display="flex" flexDirection="column" overflow="hidden">
              <Navbar />
              <Box flex="1" height="100%" overflow="hidden">
                {children}
              </Box>
              <Footer />
            </Box>
            <Toaster position="top-right" />
          </ServerProvider>
        </AuthProvider>
      </ChakraProvider>
    </QueryClientProvider>
  );
} 