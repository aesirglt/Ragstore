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
    >
      <ButtonGroup spacing={2} size="sm">
        <Button
          onClick={() => onStoreTypeChange('Vending')}
          variant={storeType === 'Vending' ? 'solid' : 'outline'}
          colorScheme="blue"
          bg={storeType === 'Vending' ? activeBg : undefined}
          color={storeType === 'Vending' ? activeColor : undefined}
        >
          Vendas
        </Button>
        <Button
          onClick={() => onStoreTypeChange('Buying')}
          variant={storeType === 'Buying' ? 'solid' : 'outline'}
          colorScheme="blue"
          bg={storeType === 'Buying' ? activeBg : undefined}
          color={storeType === 'Buying' ? activeColor : undefined}
        >
          Compras
        </Button>
      </ButtonGroup>

      <Flex direction="column" gap={4} width="100%">
        <Flex gap={4} wrap="wrap">
          <Box flex="1" minW="200px">
            <Select
              placeholder="Selecione as categorias"
              value=""
              onChange={handleCategoryChange}
              size="md"
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
        </Flex>

        {selectedCategory.length > 0 && (
          <Wrap spacing={2}>
            {selectedCategory.map((category) => (
              <Tag
                key={category}
                size="md"
                borderRadius="full"
                variant="solid"
                colorScheme="blue"
              >
                <TagLabel>{category}</TagLabel>
                <TagCloseButton onClick={() => removeCategory(category)} />
              </Tag>
            ))}
          </Wrap>
        )}
      </Flex>
    </Box>
  );
} 