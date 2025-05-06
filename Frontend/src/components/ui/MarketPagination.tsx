import {
  Box,
  Button,
  ButtonGroup,
  useColorModeValue,
} from '@chakra-ui/react';

interface MarketPaginationProps {
  currentPage: number;
  totalPages: number;
  onPageChange: (page: number) => void;
}

export function MarketPagination({
  currentPage,
  totalPages,
  onPageChange,
}: MarketPaginationProps) {
  const bgColor = useColorModeValue('white', 'gray.800');
  const borderColor = useColorModeValue('gray.200', 'gray.700');
  const activeBg = useColorModeValue('blue.50', 'blue.900');
  const activeColor = useColorModeValue('blue.600', 'blue.200');

  return (
    <Box
      position="fixed"
      bottom={0}
      left={0}
      right={0}
      bg={bgColor}
      borderTop="1px"
      borderColor={borderColor}
      p={4}
      zIndex={10}
    >
      <ButtonGroup spacing={2} justifyContent="center" width="100%">
        <Button
          onClick={() => onPageChange(currentPage - 1)}
          isDisabled={currentPage === 1}
          size="sm"
        >
          Anterior
        </Button>
        <Button
          onClick={() => onPageChange(currentPage + 1)}
          isDisabled={currentPage === totalPages}
          size="sm"
        >
          Próxima
        </Button>
      </ButtonGroup>
    </Box>
  );
} 