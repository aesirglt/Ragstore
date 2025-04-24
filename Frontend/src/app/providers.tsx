'use client';

import { Box, ChakraProvider } from '@chakra-ui/react';
import { Toaster } from 'react-hot-toast';
import { Navbar } from './components/Navbar';
import { Footer } from './components/Footer';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { ReactNode } from 'react';

const queryClient = new QueryClient();

interface ProvidersProps {
  children: ReactNode;
}

export function Providers({ children }: ProvidersProps) {
  return (
    <ChakraProvider>
      <Navbar />
      <Box pb={16}>
        <QueryClientProvider client={queryClient}>
          {children}
        </QueryClientProvider>
      </Box>
      <Footer />
      <Toaster position="top-right" />
    </ChakraProvider>
  );
} 