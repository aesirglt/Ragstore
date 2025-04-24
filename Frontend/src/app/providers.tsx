'use client';

import { Box, ChakraProvider } from '@chakra-ui/react';
import { Toaster } from 'react-hot-toast';
import { Navbar } from './components/Navbar';
import { Footer } from './components/Footer';

export function Providers({ children }: { children: React.ReactNode }) {
  return (
    <ChakraProvider>
      <Navbar />
      <Box pb={16}>
        {children}
      </Box>
      <Footer />
      <Toaster position="top-right" />
    </ChakraProvider>
  );
} 