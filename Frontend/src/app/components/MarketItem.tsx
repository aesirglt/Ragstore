import {
  Box,
  Image,
  Text,
  useColorModeValue,
} from '@chakra-ui/react';

interface MarketItemProps {
  itemId: number;
  itemName: string;
  image: string;
  category: string;
  price: number;
  quantity: number;
  onClick: (itemId: number) => void;
}

export function MarketItem({
  itemId,
  itemName,
  image,
  category,
  price,
  quantity,
  onClick,
}: MarketItemProps) {
  const bgColor = useColorModeValue('white', 'gray.800');
  const borderColor = useColorModeValue('gray.200', 'gray.700');
  const hoverBorderColor = useColorModeValue('blue.200', 'blue.500');
  const hoverBgColor = useColorModeValue('white', 'gray.700');
  const textColor = useColorModeValue('gray.600', 'gray.300');
  const priceColor = useColorModeValue('red.500', 'red.300');
  const imageBg = useColorModeValue('gray.50', 'gray.900');

  return (
    <Box
      bg={bgColor}
      p={2}
      borderRadius="md"
      boxShadow="sm"
      borderWidth="1px"
      borderColor={borderColor}
      transition="all 0.3s ease"
      position="relative"
      zIndex={1}
      _hover={{ 
        transform: 'translateY(-5px)',
        boxShadow: 'lg',
        borderColor: hoverBorderColor,
        bg: hoverBgColor,
        zIndex: 2
      }}
      cursor="pointer"
      maxW="135px"
      onClick={() => onClick(itemId)}
    >
      <Image 
        src={image} 
        alt={itemName}
        fallbackSrc="https://via.placeholder.com/100"
        borderRadius="md"
        mb={1}
        width="full"
        height="85px"
        objectFit="contain"
        bg={imageBg}
      />
      
      <Text fontSize="2xs" fontWeight="bold" mb={0.5} noOfLines={1}>{itemName}</Text>
      <Text color={textColor} fontSize="2xs" mb={0.5}>
        Categoria: {category}
      </Text>
      
      <Text fontWeight="semibold" fontSize="xs" color={priceColor} mb={0.5}>
        {price.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}
      </Text>
      <Text color={textColor} fontSize="2xs">
        Quantidade: {quantity}
      </Text>
    </Box>
  );
} 