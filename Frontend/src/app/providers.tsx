'use client';

import { ChakraProvider } from '@chakra-ui/react';
import { Toaster } from 'react-hot-toast';
import { Navbar } from './components/Navbar';

export function Providers({ children }: { children: React.ReactNode }) {
  return (
    <ChakraProvider>
      <Navbar />
      {children}
      <Toaster position="top-right" />
    </ChakraProvider>
  );
} 