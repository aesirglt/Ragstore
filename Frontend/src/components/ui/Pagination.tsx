import {
  Box,
  Button,
  ButtonGroup,
} from '@chakra-ui/react';

interface PaginationProps {
  currentPage: number;
  totalPages: number;
  onPageChange: (page: number) => void;
  variant?: 'fixed' | 'inline';
}

export function Pagination({
  currentPage,
  totalPages,
  onPageChange,
  variant = 'inline'
}: PaginationProps) {

  const buttonGroup = (
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
  );

  return (
    <Box mt={4} bg="transparent" border="none" boxShadow="none">
      {buttonGroup}
    </Box>
  );
} 