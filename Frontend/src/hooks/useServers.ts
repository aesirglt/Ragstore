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
      const response = await fetch('/api/servers');
      if (!response.ok) {
        throw new Error('Falha ao buscar servidores');
      }
      return response.json();
    },
  });
} 