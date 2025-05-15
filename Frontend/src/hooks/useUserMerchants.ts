import { useQuery } from '@tanstack/react-query';
import { UserMerchantViewModel } from '@/types/api/viewmodels/UserMerchantViewModel';
import { GetUserMerchantsRequest } from '@/types/api/requests/GetUserMerchantsRequest';
import { PageResult } from '@/types/api/responses/PageResult';

export function useUserMerchants(userId: string) {
  return useQuery<PageResult<UserMerchantViewModel>>({
    queryKey: ['userMerchants', userId],
    queryFn: async () => {
      const request: GetUserMerchantsRequest = { userId };
      const response = await fetch(`/user/${request.userId}/merchants`);
      if (!response.ok) {
        throw new Error('Erro ao buscar marcadores do usuário');
      }
      return response.json();
    },
    enabled: !!userId,
  });
}