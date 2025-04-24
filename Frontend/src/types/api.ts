// Tipos base
export interface BaseResponse {
  success: boolean;
  message?: string;
}

// Tipos para Lojas
export interface StoreDetailViewModel {
  id: number;
  name: string;
  description?: string;
  items: StoreItemViewModel[];
  createdAt: string;
  updatedAt: string;
}

export interface StoreItemViewModel {
  itemId: number;
  price: number;
  quantity: number;
  storeId: number;
  createdAt: string;
  updatedAt: string;
}

// Tipos para Itens
export interface ItemViewModel {
  id: number;
  itemId: number;
  itemName: string;
  name: string;
  description?: string;
  type: string;
  weight: number;
  price: number;
  quantity: number;
  image: string;
  category: string;
  stores: StoreDetailViewModel[];
}

// Tipos para Servidores
export interface ServerViewModel {
  id: number;
  name: string;
  description?: string;
  status: string;
  createdAt: string;
  updatedAt: string;
}

// Tipos para Agentes
export interface AgentViewModel {
  id: number;
  name: string;
  description?: string;
  status: string;
  createdAt: string;
  updatedAt: string;
}

// Tipos para Histórico
export interface HistoryViewModel {
  id: number;
  itemId: number;
  storeId: number;
  price: number;
  quantity: number;
  createdAt: string;
}

// Tipos para Requisições
export interface GetStoreByIdRequest {
  server: string;
  id: number;
}

export interface GetItemByIdRequest {
  server: string;
  id: number;
}

export interface GetStoresByItemIdRequest {
  server: string;
  itemId: number;
}

export interface GetHistoryRequest {
  server: string;
  itemId: number;
}

// Tipos para Respostas
export interface StoreResponse extends BaseResponse {
  data?: StoreDetailViewModel;
}

export interface ItemResponse extends BaseResponse {
  '@odata.count'?: number;
  value?: ItemViewModel[];
  data?: ItemViewModel;
}

export interface ServerResponse extends BaseResponse {
  data?: ServerViewModel[];
}

export interface AgentResponse extends BaseResponse {
  data?: AgentViewModel[];
}

export interface HistoryResponse extends BaseResponse {
  data?: HistoryViewModel[];
}

export interface PaginationParams {
  page: number;
  pageSize: number;
} 