import {
  Box,
  Button,
  ButtonGroup,
  Select,
  Flex,
  Tag,
  TagLabel,
  TagCloseButton,
  Wrap,
  useColorModeValue,
  Text,
} from '@chakra-ui/react';
import { useState } from 'react';

interface MarketFiltersProps {
  selectedCategory: string[];
  storeType: string;
  onCategoryChange: (categories: string[]) => void;
  onStoreTypeChange: (type: string) => void;
}

export function MarketFilters({
  selectedCategory,
  storeType,
  onCategoryChange,
  onStoreTypeChange,
}: MarketFiltersProps) {
  const bgColor = useColorModeValue('white', 'gray.800');
  const borderColor = useColorModeValue('gray.200', 'gray.700');
  const activeBg = useColorModeValue('blue.50', 'blue.900');
  const activeColor = useColorModeValue('blue.600', 'blue.200');
  const selectBg = useColorModeValue('white', 'gray.700');
  const selectBorderColor = useColorModeValue('gray.200', 'gray.600');
  const [isOpen, setIsOpen] = useState(false);

  const handleCategoryChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const value = e.target.value;
    if (value && !selectedCategory.includes(value)) {
      onCategoryChange([...selectedCategory, value]);
    }
  };

  const removeCategory = (categoryToRemove: string) => {
    onCategoryChange(selectedCategory.filter(cat => cat !== categoryToRemove));
  };

  return (
    <Box
      bg={bgColor}
      p={4}
      borderRadius="md"
      borderWidth="1px"
      borderColor={borderColor}
      boxShadow="sm"
    >
      <Flex direction="column" gap={4} width="100%">
        <Flex gap={4} direction={{ base: 'column', md: 'row' }}>
          <Box flex="2">
            <Text fontSize="sm" fontWeight="medium" mb={2} color={useColorModeValue('gray.700', 'gray.300')}>
              Categorias
            </Text>
            <Select
              placeholder="Selecione as categorias"
              value=""
              onChange={handleCategoryChange}
              size="md"
              bg={selectBg}
              borderColor={selectBorderColor}
              _hover={{
                borderColor: 'blue.300',
              }}
              _focus={{
                borderColor: 'blue.500',
                boxShadow: '0 0 0 1px var(--chakra-colors-blue-500)',
              }}
            >
              <option value="Armas">Armas</option>
              <option value="Armaduras">Armaduras</option>
              <option value="Consumíveis">Consumíveis</option>
              <option value="Acessórios">Acessórios</option>
              <option value="Blueprints">Blueprints</option>
              <option value="Encantamentos">Encantamentos</option>
              <option value="Gemas">Gemas</option>
              <option value="Pets">Pets</option>
              <option value="Quest">Quest</option>
              <option value="Scrolls">Scrolls</option>
              <option value="Shadow">Shadow</option>
              <option value="Especial">Especial</option>
              <option value="Diversos">Diversos</option>
            </Select>
          </Box>

          <Box flex="1">
            <Text fontSize="sm" fontWeight="medium" mb={2} color={useColorModeValue('gray.700', 'gray.300')}>
              Tipo de Loja
            </Text>
            <ButtonGroup spacing={2} size="md">
              <Button
                onClick={() => onStoreTypeChange('Vending')}
                variant={storeType === 'Vending' ? 'solid' : 'outline'}
                colorScheme="blue"
                bg={storeType === 'Vending' ? activeBg : undefined}
                color={storeType === 'Vending' ? activeColor : undefined}
                _hover={{
                  bg: storeType === 'Vending' ? 'blue.100' : 'blue.50',
                  color: 'blue.600',
                }}
                _active={{
                  bg: storeType === 'Vending' ? 'blue.200' : 'blue.100',
                }}
              >
                Vendas
              </Button>
              <Button
                onClick={() => onStoreTypeChange('Buying')}
                variant={storeType === 'Buying' ? 'solid' : 'outline'}
                colorScheme="blue"
                bg={storeType === 'Buying' ? activeBg : undefined}
                color={storeType === 'Buying' ? activeColor : undefined}
                _hover={{
                  bg: storeType === 'Buying' ? 'blue.100' : 'blue.50',
                  color: 'blue.600',
                }}
                _active={{
                  bg: storeType === 'Buying' ? 'blue.200' : 'blue.100',
                }}
              >
                Compras
              </Button>
            </ButtonGroup>
          </Box>
        </Flex>

        {selectedCategory.length > 0 && (
          <Box>
            <Text fontSize="sm" fontWeight="medium" mb={2} color={useColorModeValue('gray.700', 'gray.300')}>
              Categorias Selecionadas
            </Text>
            <Wrap spacing={2}>
              {selectedCategory.map((category) => (
                <Tag
                  key={category}
                  size="md"
                  borderRadius="full"
                  variant="solid"
                  colorScheme="blue"
                  bg={useColorModeValue('blue.100', 'blue.900')}
                  color={useColorModeValue('blue.700', 'blue.200')}
                >
                  <TagLabel>{category}</TagLabel>
                  <TagCloseButton 
                    onClick={() => removeCategory(category)}
                    _hover={{
                      bg: useColorModeValue('blue.200', 'blue.800'),
                    }}
                  />
                </Tag>
              ))}
            </Wrap>
          </Box>
        )}
      </Flex>
    </Box>
  );
} 