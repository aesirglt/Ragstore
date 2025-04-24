import axios from 'axios';
import { toast } from 'react-hot-toast';

const api = axios.create({
  baseURL: '/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

// Interceptor para requisições
api.interceptors.request.use(
  (config) => {
    // Adiciona o parâmetro target apenas para requisições que não são para o proxy
    if (!config.url?.includes('/api/[...proxy]')) {
      config.params = {
        ...config.params,
        target: process.env.NEXT_PUBLIC_API_URL || 'http://localhost:60378',
      };
    }
    return config;
  },
  (error) => {
    console.error('Erro na requisição:', error);
    return Promise.reject(error);
  }
);

// Interceptor para respostas
api.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    if (error.response) {
      // Erros do servidor
      const errorMessage = error.response.data?.error || error.response.data?.message || 'Erro desconhecido';
      
      switch (error.response.status) {
        case 401:
          toast.error('Não autorizado. Por favor, faça login novamente.');
          // Aqui você pode redirecionar para a página de login
          // router.push('/login');
          break;
        case 403:
          toast.error('Acesso negado.');
          break;
        case 404:
          toast.error('Recurso não encontrado.');
          break;
        case 500:
          toast.error(`Erro interno do servidor: ${errorMessage}`);
          break;
        default:
          toast.error(`Erro: ${errorMessage}`);
      }
    } else if (error.request) {
      // Erros de rede
      toast.error('Erro de conexão. Verifique sua internet.');
    } else {
      // Erros na configuração da requisição
      toast.error('Erro ao configurar a requisição.');
    }
    return Promise.reject(error);
  }
);

export default api; 