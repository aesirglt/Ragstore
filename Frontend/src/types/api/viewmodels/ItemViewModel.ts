import { StoreDetailViewModel } from "./StoreDetailViewModel";

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