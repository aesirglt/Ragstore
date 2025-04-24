import axios from 'axios';
import { 
  StoreResponse, 
  ItemResponse, 
  ServerResponse, 
  AgentResponse, 
  HistoryResponse,
  GetStoreByIdRequest,
  GetItemByIdRequest,
  GetStoresByItemIdRequest,
  GetHistoryRequest,
  PaginationParams
} from '../types/api'

const api = axios.create({
    baseURL: process.env.NEXT_PUBLIC_API_URL
});

export const apiService = {
  // Servidores
  getServers: async (): Promise<ServerResponse> => {
    const response = await api.get('/brothor/servers')
    return response.data
  },

  // Lojas
  getStoreById: async ({ server, id }: GetStoreByIdRequest): Promise<StoreResponse> => {
    const response = await api.get(`/brothor/stores-vending/${id}`)
    return response.data
  },

  // Itens
  getItemById: async ({ server, id }: GetItemByIdRequest): Promise<ItemResponse> => {
    const response = await api.get(`/brothor/stores-vending/items/${id}`)
    return response.data
  },

  getStoresByItemId: async ({ server, itemId }: GetStoresByItemIdRequest): Promise<StoreResponse> => {
    const response = await api.get(`/brothor/stores-vending/items/${itemId}/stores`)
    return response.data
  },

  // Lista de itens
  getItems: async ({ page, pageSize }: PaginationParams): Promise<ItemResponse> => {
    const response = await api.get('/brothor/stores-vending/items', {
      params: {
        $skip: (page - 1) * pageSize,
        $top: pageSize,
        $count: true,
        $orderby: 'ItemName'
      }
    })
    return response.data
  },

  // Agentes
  getAgents: async (server: string): Promise<AgentResponse> => {
    const response = await api.get(`/brothor/agents`)
    return response.data
  },

  // Histórico
  getHistory: async ({ server, itemId }: GetHistoryRequest): Promise<HistoryResponse> => {
    const response = await api.get(`/brothor/${server}/items/${itemId}/history`)
    return response.data
  },

  getLastSearchedItems: async () => {
    return await api.get('/api/items/last-searched', {
      params: {
        $top: 10,
        $orderby: 'timestamp desc'
      }
    })
  },

  getItemsByName: async (itemName?: string) => {
    return await api.get('/api/items', {
      params: {
        $filter: itemName ? `contains(tolower(ItemName), tolower('${itemName}'))` : undefined,
        $orderby: 'ItemName'
      }
    })
  },
}

export default api; 