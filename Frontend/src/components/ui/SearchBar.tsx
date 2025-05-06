import {
  Input,
  InputGroup,
  InputLeftElement,
  useColorModeValue,
} from '@chakra-ui/react';
import { SearchIcon } from '@chakra-ui/icons';
import { useRef, useEffect } from 'react';

interface SearchBarProps {
  value: string;
  onChange: (value: string) => void;
  placeholder?: string;
}

export function SearchBar({ value, onChange, placeholder = "Pesquisar itens..." }: SearchBarProps) {
  const searchInputRef = useRef<HTMLInputElement>(null);
  const bgColor = useColorModeValue('white', 'gray.800');
  const borderColor = useColorModeValue('gray.200', 'gray.700');
  const placeholderColor = useColorModeValue('gray.500', 'gray.400');
  const iconColor = useColorModeValue('gray.500', 'gray.400');

  useEffect(() => {
    if (searchInputRef.current) {
      searchInputRef.current.focus();
    }
  }, []);

  return (
    <InputGroup size="lg">
      <InputLeftElement pointerEvents="none">
        <SearchIcon color={iconColor} />
      </InputLeftElement>
      <Input
        ref={searchInputRef}
        placeholder={placeholder}
        value={value}
        onChange={(e) => onChange(e.target.value)}
        bg={bgColor}
        borderColor={borderColor}
        _hover={{
          borderColor: 'blue.300',
        }}
        _focus={{
          borderColor: 'blue.500',
          boxShadow: '0 0 0 1px var(--chakra-colors-blue-500)',
        }}
        _placeholder={{
          color: placeholderColor,
        }}
        autoFocus
      />
    </InputGroup>
  );
} 