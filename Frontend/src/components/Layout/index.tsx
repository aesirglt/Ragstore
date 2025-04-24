import { Box, Container, Flex, Select, Image, Button, Menu, MenuButton, MenuList, MenuItem } from '@chakra-ui/react'
import { ChevronDownIcon } from '@chakra-ui/icons'
import Link from 'next/link'
import { ReactNode } from 'react'

interface LayoutProps {
  children: ReactNode
}

export function Layout({ children }: LayoutProps) {
  return (
    <Box minH="100vh">
      <Box bg="brand.500" color="white" py={4} mb={8}>
        <Container maxW="container.xl">
          <Flex justify="space-between" align="center">
            <Flex align="center" gap={8}>
              <Link href="/">
                <Image src="/logo.png" alt="RagStore" h="40px" />
              </Link>
              <Link href="/">
                <Button variant="ghost" color="white" _hover={{ bg: 'brand.600' }}>
                  Home
                </Button>
              </Link>
              <Link href="/mercado">
                <Button variant="ghost" color="white" _hover={{ bg: 'brand.600' }}>
                  Mercado
                </Button>
              </Link>
            </Flex>

            <Flex align="center" gap={4}>
              <Select
                bg="white"
                color="gray.800"
                w="200px"
                placeholder="Selecione o servidor"
                defaultValue="bRO"
              >
                <option value="bRO">bRO Thor</option>
                <option value="brother">Brother</option>
              </Select>

              <Menu>
                <MenuButton
                  as={Button}
                  rightIcon={<ChevronDownIcon />}
                  variant="ghost"
                  color="white"
                  _hover={{ bg: 'brand.600' }}
                >
                  Minha Conta
                </MenuButton>
                <MenuList color="gray.800">
                  <MenuItem as={Link} href="/perfil">
                    Perfil
                  </MenuItem>
                  <MenuItem as={Link} href="/configuracoes">
                    Configurações
                  </MenuItem>
                </MenuList>
              </Menu>
            </Flex>
          </Flex>
        </Container>
      </Box>

      <Container maxW="container.xl">
        {children}
      </Container>
    </Box>
  )
} 