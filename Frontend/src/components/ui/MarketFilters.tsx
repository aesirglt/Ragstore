import { Box, Select, Flex, Tag, TagLabel, TagCloseButton, Wrap } from '@chakra-ui/react';
import { useState } from 'react';

interface MarketFiltersProps {
  selectedCategory: string[];
  storeType: string;
  onCategoryChange: (value: string[]) => void;
  onStoreTypeChange: (value: string) => void;
}

export const MarketFilters: React.FC<MarketFiltersProps> = ({
  selectedCategory,
  storeType,
  onCategoryChange,
  onStoreTypeChange,
}) => {
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

        <Box flex="1" minW="200px">
          <Select
            placeholder="Tipo de loja"
            value={storeType}
            onChange={(e) => onStoreTypeChange(e.target.value)}
            size="md"
          >
            <option value="Vending">Venda</option>
            <option value="Buying">Compra</option>
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
  );
}; 