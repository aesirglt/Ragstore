import { BaseResponse } from "./BaseResponse";

export interface ServerResponse extends BaseResponse {
    data?: ServerViewModel[];
  }

  // Tipos para Servidores
export interface ServerViewModel {
    id: number;
    name: string;
}