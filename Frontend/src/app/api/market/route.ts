// services/marketService.ts
export interface MarketItem {
  id: string;
  name: string;
  icon: string | null;
  price?: number;
  quantity?: number;
  location?: string;
  seller?: string;
  sellerId?: string;
}

export interface StoreItem extends MarketItem {}

export interface Store {
  id: string;
  name: string;
  owner: string;
  location: string;
  items: StoreItem[];
}

export interface MarketSummary {
  minValue: number;
  currentMinValue: number;
  currentMaxValue: number;
  average: number;
  storeNumbers: number;
}

export interface RecentItem {
  id: string;
  name: string;
  quantity: number;
  unitPrice: number;
  total: number;
  time: string;
}

// Mock Data
const MOCK_ITEMS: MarketItem[] = [
  { id: '1', name: 'Livro da Toração', icon: null },
  { id: '2', name: 'Bol Shadow', icon: null },
  { id: '3', name: 'Instance Stone', icon: null },
  { id: '4', name: 'Antique Gatling Gun [1]', icon: null },
  { id: '5', name: '+13 Gatling Primordial [2]', icon: null },
  { id: '6', name: '+11 do Sacrifício Dracônico Quepe do General [1]', icon: null },
  { id: '7', name: 'Ehredecon Perfeito', icon: null },
  { id: '8', name: 'Ehredecon Enriquecido', icon: null },
  { id: '9', name: 'Ehredecon', icon: null },
  { id: '10', name: 'Escudo de Aegis Domini [INFINITO]', icon: null }
];

const MOCK_MARKET_ITEMS: { items: MarketItem[]; totalCount: number } = {
  items: [
    { id: '1', name: 'Livro da Toração', icon: null, price: 100000, quantity: 69, location: 'Comodo 191, 152', seller: 'LG12', sellerId: 'LG12' },
    { id: '2', name: 'Bol Shadow', icon: null, price: 200000000, quantity: 9, location: 'Comodo 191, 152', seller: 'LG12', sellerId: 'LG12' },
    { id: '3', name: 'Instance Stone', icon: null, price: 1100000, quantity: 261, location: 'Yuno 145, 177', seller: 'Kolulu', sellerId: 'Kolulu' },
    { id: '4', name: 'Antique Gatling Gun [1]', icon: null, price: 500000000, quantity: 1, location: 'Malaya 286, 210', seller: 'DrogDealer', sellerId: 'DrogDealer' },
    { id: '5', name: '+13 Gatling Primordial [2]', icon: null, price: 899999999, quantity: 1, location: 'Malaya 286, 210', seller: 'DrogDealer', sellerId: 'DrogDealer' },
    { id: '6', name: '+11 do Sacrifício Dracônico Quepe do General [1]', icon: null, price: 999999999, quantity: 1, location: 'Malaya 286, 210', seller: 'DrogDealer', sellerId: 'DrogDealer' },
    { id: '7', name: 'Ehredecon Perfeito', icon: null, price: 2500000, quantity: 31, location: 'PrtLn 126, 62', seller: 'MeiaCinza', sellerId: 'MeiaCinza' },
    { id: '8', name: 'Ehredecon Enriquecido', icon: null, price: 4800000, quantity: 37, location: 'PrtLn 126, 62', seller: 'MeiaCinza', sellerId: 'MeiaCinza' },
    { id: '9', name: 'Ehredecon', icon: null, price: 90000, quantity: 57, location: 'PrtLn 126, 62', seller: 'MeiaCinza', sellerId: 'MeiaCinza' },
    { id: '10', name: 'Escudo de Aegis Domini [INFINITO]', icon: null, price: 35500000000, quantity: 2, location: 'Ayothaya 206, 188', seller: 'Gordon 9', sellerId: 'Gordon9' }
  ],
  totalCount: 14684
};

const MOCK_STORES: Store[] = [
  {
    id: 'LG12',
    name: 'LG12 Store',
    owner: 'LG12',
    location: 'Comodo 191, 152',
    items: [
      { id: '1', name: 'Livro da Toração', icon: null, price: 100000, quantity: 69 },
      { id: '2', name: 'Bol Shadow', icon: null, price: 200000000, quantity: 9 }
    ]
  },
  {
    id: 'Kolulu',
    name: 'Kolulu Shop',
    owner: 'Kolulu',
    location: 'Yuno 145, 177',
    items: [
      { id: '3', name: 'Instance Stone', icon: null, price: 1100000, quantity: 261 }
    ]
  },
  {
    id: 'DrogDealer',
    name: 'DrogDealer Weapons',
    owner: 'DrogDealer',
    location: 'Malaya 286, 210',
    items: [
      { id: '4', name: 'Antique Gatling Gun [1]', icon: null, price: 500000000, quantity: 1 },
      { id: '5', name: '+13 Gatling Primordial [2]', icon: null, price: 899999999, quantity: 1 },
      { id: '6', name: '+11 do Sacrifício Dracônico Quepe do General [1]', icon: null, price: 999999999, quantity: 1 }
    ]
  },
  {
    id: 'MeiaCinza',
    name: 'MeiaCinza Potions',
    owner: 'MeiaCinza',
    location: 'PrtLn 126, 62',
    items: [
      { id: '7', name: 'Ehredecon Perfeito', icon: null, price: 2500000, quantity: 31 },
      { id: '8', name: 'Ehredecon Enriquecido', icon: null, price: 4800000, quantity: 37 },
      { id: '9', name: 'Ehredecon', icon: null, price: 90000, quantity: 57 }
    ]
  },
  {
    id: 'Gordon9',
    name: 'Gordon 9 Equipment',
    owner: 'Gordon 9',
    location: 'Ayothaya 206, 188',
    items: [
      { id: '10', name: 'Escudo de Aegis Domini [INFINITO]', icon: null, price: 35500000000, quantity: 2 }
    ]
  }
];

const MOCK_RECENT_ITEMS: RecentItem[] = [
  { id: '1', name: 'Etherium Perfeito', quantity: 22, unitPrice: 52000000, total: 1144000000, time: '16:47' },
  { id: '2', name: 'Etherium Perfeito', quantity: 27, unitPrice: 52000000, total: 1404000000, time: '16:47' },
  { id: '3', name: 'Etherium Perfeito', quantity: 5, unitPrice: 50000000, total: 250000000, time: '16:44' },
  { id: '4', name: 'Etherium Perfeito', quantity: 4, unitPrice: 49999999, total: 199999996, time: '16:43' }
];

// Funções
export const fetchMarketItems = async (filters: Record<string, any> = {}) => {
  try {
    const query = new URLSearchParams(filters).toString();
    const response = await fetch(`/market/items?${query}`);
    return await response.json();
  } catch (error) {
    console.error('Error fetching market items:', error);
    return MOCK_MARKET_ITEMS;
  }
};

export const fetchItemDetails = async (itemId: string) => {
  try {
    const response = await fetch(`/market/items/${itemId}`);
    return await response.json();
  } catch (error) {
    console.error(`Error fetching item details for ID ${itemId}:`, error);
    return MOCK_ITEMS.find(item => item.id === itemId) || null;
  }
};

export const fetchStoresSellingItem = async (itemId: string) => {
  try {
    const response = await fetch(`/market/items/${itemId}/stores`);
    return await response.json();
  } catch (error) {
    console.error(`Error fetching stores for item ID ${itemId}:`, error);
    return MOCK_STORES.filter(store => store.items.some(item => item.id === itemId));
  }
};

export const fetchStoreDetails = async (storeId: string) => {
  try {
    const response = await fetch(`/market/stores/${storeId}`);
    return await response.json();
  } catch (error) {
    console.error(`Error fetching store details for ID ${storeId}:`, error);
    return MOCK_STORES.find(store => store.id === storeId) || null;
  }
};

export const fetchRecentItems = async (limit = 10): Promise<RecentItem[]> => {
  try {
    const query = new URLSearchParams({ limit: limit.toString() }).toString();
    const response = await fetch(`/market/recent?${query}`);
    return await response.json();
  } catch (error) {
    console.error('Error fetching recent items:', error);
    return MOCK_RECENT_ITEMS.slice(0, limit);
  }
};

export const fetchItemsList = async (): Promise<MarketItem[]> => {
  try {
    const response = await fetch(`/market/items-list`);
    return await response.json();
  } catch (error) {
    console.error('Error fetching items list:', error);
    return MOCK_ITEMS;
  }
};

export const fetchMarketSummary = async (
  server: string,
  itemId: string,
  type: 'vending' | 'buying' = 'vending'
): Promise<MarketSummary> => {
  try {
    const endpoint = type === 'vending' ? 'vending-store-sumary' : 'buying-store-sumary';
    const response = await fetch(`/${server}/${endpoint}/${itemId}`);
    return await response.json();
  } catch (error) {
    console.error(`Error fetching ${type} market summary for item ${itemId}:`, error);
    return {
      minValue: 0,
      currentMinValue: 0,
      currentMaxValue: 0,
      average: 0,
      storeNumbers: 0
    };
  }
};
