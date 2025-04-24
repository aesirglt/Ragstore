import { StoreItemViewModel } from "./StoreItemViewModel";

// Tipos para Lojas
export interface StoreDetailViewModel {
    id: number;
    name: string;
    description?: string;
    items: StoreItemViewModel[];
    createdAt: string;
    updatedAt: string;
  }