import { Box, HStack, Select } from '@chakra-ui/react';

interface MarketFiltersProps {
  selectedCategory: string;
  selectedServer: string;
  storeType: string;
  onCategoryChange: (value: string) => void;
  onServerChange: (value: string) => void;
  onStoreTypeChange: (value: string) => void;
}

export const MarketFilters = ({
  selectedCategory,
  selectedServer,
  storeType,
  onCategoryChange,
  onServerChange,
  onStoreTypeChange,
}: MarketFiltersProps) => {
  return (
    <HStack spacing={1} wrap="wrap">
      <Box flex="1" minW="200px">
        <Select
          placeholder="Servidor"
          value={selectedServer}
          onChange={(e) => onServerChange(e.target.value)}
          bg="white"
        >
          <option value="brothor">Thor</option>
        </Select>
      </Box>
      <Box flex="1" minW="200px">
        <Select
          placeholder="Categoria"
          value={selectedCategory}
          onChange={(e) => onCategoryChange(e.target.value)}
          bg="white"
        >
          <option value="">Todas</option>
          <option value="weapon">Armas</option>
          <option value="armor">Armaduras</option>
          <option value="card">Cartas</option>
          <option value="potion">Poções</option>
          <option value="material">Materiais</option>
        </Select>
      </Box>

      <Box flex="1" minW="200px">
        <Select
          placeholder="Tipo de loja"
          value={storeType}
          onChange={(e) => onStoreTypeChange(e.target.value)}
          bg="white"
        >
          <option value="buy">Compra</option>
          <option value="sell">Venda</option>
        </Select>
      </Box>
    </HStack>
  );
}; 