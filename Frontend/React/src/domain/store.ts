import { ItemStore } from "./item";

export interface Store {
    id: number,
    name: string,
    location: string,
    merchant: string,
    items: ItemStore []
}