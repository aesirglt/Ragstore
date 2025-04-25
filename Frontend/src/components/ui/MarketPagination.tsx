import { Box, Flex, Button, Text, Container } from '@chakra-ui/react';

interface MarketPaginationProps {
  currentPage: number;
  totalPages: number;
  onPageChange: (page: number) => void;
}

export const MarketPagination = ({ currentPage, totalPages, onPageChange }: MarketPaginationProps) => {
  return (
    <Box 
      position="fixed"
      bottom="48px"
      left="0"
      right="0"
      bg="white" 
      py={3}
      borderTop="1px" 
      borderColor="gray.200"
      boxShadow="0 -2px 10px rgba(0,0,0,0.05)"
    >
      <Container maxW="container.xl">
        <Flex justify="center" gap={4} align="center">
          <Button
            size="sm"
            colorScheme="blue"
            variant="outline"
            onClick={() => onPageChange(currentPage - 1)}
            isDisabled={currentPage === 1}
          >
            Anterior
          </Button>
          <Text fontSize="sm">
            Página {currentPage} de {totalPages}
          </Text>
          <Button
            size="sm"
            colorScheme="blue"
            variant="outline"
            onClick={() => onPageChange(currentPage + 1)}
            isDisabled={currentPage === totalPages}
          >
            Próxima
          </Button>
        </Flex>
      </Container>
    </Box>
  );
}; 