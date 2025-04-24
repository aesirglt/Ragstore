import { BaseResponse } from "./BaseResponse";

export interface HistoryResponse extends BaseResponse {
    data?: HistoryViewModel[];
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
  
  // Ti