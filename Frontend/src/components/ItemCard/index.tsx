import { Box, Text, Image } from '@chakra-ui/react'
import { motion } from 'framer-motion'

const MotionBox = motion(Box)

interface ItemCardProps {
  name: string
  seller: string
  price: number
  quantity: number
  image: string
  onClick: () => void
}

export function ItemCard({ name, seller, price, quantity, image, onClick }: ItemCardProps) {
  return (
    <MotionBox
      as="article"
      bg="white"
      borderRadius="md"
      overflow="hidden"
      cursor="pointer"
      onClick={onClick}
      whileHover={{ scale: 1.05 }}
      transition={{ duration: 0.2 }}
      boxShadow="sm"
      width="200px" // Tamanho base reduzido
    >
      <Box
        bg="gray.50"
        p={2}
        display="flex"
        alignItems="center"
        justifyContent="center"
        height="140px" // Altura reduzida
      >
        <Image
          src={image}
          alt={name}
          height="100px" // Tamanho da imagem reduzido
          objectFit="contain"
          fallbackSrc="https://via.placeholder.com/100"
        />
      </Box>

      <Box p={3}>
        <Text fontWeight="bold" fontSize="sm" mb={1} noOfLines={1}>
          {name}
        </Text>
        <Text color="gray.600" fontSize="xs" mb={1}>
          Vendedor: {seller}
        </Text>
        <Text color="red.500" fontWeight="bold" fontSize="md" mb={1}>
          {price.toLocaleString('pt-BR')} z
        </Text>
        <Text color="gray.500" fontSize="xs">
          Quantidade: {quantity}
        </Text>
      </Box>
    </MotionBox>
  )
} 