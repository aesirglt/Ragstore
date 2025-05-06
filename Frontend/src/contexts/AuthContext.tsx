import { createContext, useContext, useState, useEffect, ReactNode } from 'react';
import { config } from '@/config/env';

interface User {
  id: string;
  name: string;
  email: string;
  picture?: string;
}

interface AuthContextType {
  user: User | null;
  loading: boolean;
  signOut: () => void;
  isAuthenticated: boolean;
}

const AuthContext = createContext<AuthContextType>({} as AuthContextType);

export function AuthProvider({ children }: { children: ReactNode }) {
  const [user, setUser] = useState<User | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function loadUser() {
      console.log('[Auth] Iniciando carregamento do usuário...');
      try {
        const response = await fetch('/api/auth/me', {
          credentials: 'include',
        });
        console.log('[Auth] Resposta da API /api/auth/me:', response);
        if (response.ok) {
          const userData = await response.json();
          console.log('[Auth] Usuário carregado:', userData);
          setUser(userData);
        } else {
          console.warn('[Auth] Falha ao carregar usuário. Status:', response.status);
          setUser(null);
        }
      } catch (error) {
        console.error('[Auth] Erro ao carregar usuário:', error);
        setUser(null);
      } finally {
        setLoading(false);
        console.log('[Auth] Carregamento do usuário finalizado.');
      }
    }

    loadUser();
  }, []);

  const signOut = async () => {
    try {
      console.log('[Auth] Fazendo logout...');
      await fetch(`${config.backendUrl}/api/auth/signout`, {
        method: 'POST',
        credentials: 'include',
      });
      setUser(null);
      console.log('[Auth] Logout realizado com sucesso.');
    } catch (error) {
      console.error('[Auth] Erro ao fazer logout:', error);
    }
  };

  return (
    <AuthContext.Provider value={{ user, loading, signOut, isAuthenticated: !!user }}>
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth deve ser usado dentro de um AuthProvider');
  }
  return context;
} 