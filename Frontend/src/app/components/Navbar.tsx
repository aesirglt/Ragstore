'use client';

import {
  Box,
  Container,
  Flex,
  Link,
  Stack,
  Menu,
  MenuButton,
  MenuList,
  MenuItem,
  Button,
  Icon,
  useColorModeValue,
  Switch,
  useColorMode,
} from '@chakra-ui/react';
import NextLink from 'next/link';
import { usePathname } from 'next/navigation';
import { FaUser } from 'react-icons/fa';
import { MdDarkMode } from 'react-icons/md';
import { ServerSelector } from '@/components/ServerSelector';
import { useAuth } from '@/contexts/AuthContext';

export function Navbar() {
  const pathname = usePathname();
  const bgColor = useColorModeValue('blue.50', 'blue.900');
  const borderColor = useColorModeValue('blue.100', 'blue.700');
  const { isAuthenticated } = useAuth();
  const { colorMode, toggleColorMode } = useColorMode();

  const isActive = (path: string) => pathname === path;

  const links = [
    { href: '/', label: 'Home' },
    { href: '/mercado', label: 'Mercado' },
  ];

  return (
    <Box bg={bgColor} borderBottom="1px" borderColor={borderColor}>
      <Container maxW="container.xl">
        <Flex h={16} alignItems="center" justifyContent="space-between">
          <Stack direction="row" spacing={8}>
            {links.map(({ href, label }) => (
              <Link
                key={href}
                as={NextLink}
                href={href}
                fontSize="md"
                fontWeight={isActive(href) ? "bold" : "normal"}
                color={isActive(href) ? "blue.600" : "gray.600"}
                _hover={{
                  textDecoration: 'none',
                  color: 'blue.500',
                }}
              >
                {label}
              </Link>
            ))}
          </Stack>
          <ServerSelector />
          {isAuthenticated && (
            <Button
              as={NextLink}
              href="/perfil"
              leftIcon={<Icon as={FaUser} />}
              colorScheme="blue"
              variant="ghost"
            >
              Minha Conta
            </Button>
          )}
        </Flex>
      </Container>
    </Box>
  );
} 