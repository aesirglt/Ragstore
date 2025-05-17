import { PageResult } from '@/types/api/responses/PageResult';
import { useQuery } from '@tanstack/react-query';

export interface ServerResume {
  id: string;
  name: string;
  displayName: string;
}

export function useServers() {
  return useQuery<PageResult<ServerResume>>({
    queryKey: ['servers'],
    queryFn: async () => {
      const url = new URL(`/api/Servers`, window.location.origin);
      const response = await fetch(url.toString(), {
        method: 'GET',
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json',
        },
      });
      
      if (!response.ok) {
        throw new Error(`Falha ao buscar servidores: ${response.status} ${response.statusText}`);
      }

      const contentType = response.headers.get('content-type');
      if (!contentType || !contentType.includes('application/json')) {
        throw new Error('Resposta inválida do servidor: esperado JSON');
      }

      return response.json();
    },
  });
} 