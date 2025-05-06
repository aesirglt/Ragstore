import { createContext, useContext, useState, useEffect, ReactNode } from 'react';
import { config } from '@/config/env';
import { User, AuthContextType } from '@/types/auth';

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

  const logout = async () => {
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

  const login = async (email: string, password: string) => {
    try {
      const response = await fetch('/api/auth/login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ email, password }),
        credentials: 'include',
      });

      if (!response.ok) {
        throw new Error('Falha no login');
      }

      const userData = await response.json();
      setUser(userData);
    } catch (error) {
      console.error('[Auth] Erro no login:', error);
      throw error;
    }
  };

  const updateUser = async (updatedUser: User) => {
    try {
      const response = await fetch('/api/auth/update', {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(updatedUser),
        credentials: 'include',
      });

      if (!response.ok) {
        throw new Error('Falha ao atualizar usuário');
      }

      const userData = await response.json();
      setUser(userData);
    } catch (error) {
      console.error('[Auth] Erro ao atualizar usuário:', error);
      throw error;
    }
  };

  return (
    <AuthContext.Provider value={{ 
      user, 
      loading, 
      isAuthenticated: !!user,
      login,
      logout,
      updateUser,
      signOut: logout
    }}>
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