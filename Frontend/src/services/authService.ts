import { LoginRequest, RegisterRequest, AuthResponse, ExternalLoginRequest } from '@/types/auth';

const API_URL = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5000';

export const authService = {
  async login(data: LoginRequest): Promise<AuthResponse> {
    const response = await fetch(`${API_URL}/identity/login`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      throw new Error('Falha no login');
    }

    return response.json();
  },

  async register(data: RegisterRequest): Promise<AuthResponse> {
    const response = await fetch(`${API_URL}/identity/register`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      throw new Error('Falha no registro');
    }

    return response.json();
  },

  async externalLogin(data: ExternalLoginRequest): Promise<AuthResponse> {
    const response = await fetch(`${API_URL}/api/auth/external-login`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      throw new Error('Falha no login externo');
    }

    return response.json();
  },

  async getGoogleAuthUrl(): Promise<string> {
    const response = await fetch(`${API_URL}/api/auth/google-url`);
    if (!response.ok) {
      throw new Error('Falha ao obter URL do Google');
    }
    const data = await response.json();
    return data.url;
  },

  async getDiscordAuthUrl(): Promise<string> {
    const response = await fetch(`${API_URL}/api/auth/discord-url`);
    if (!response.ok) {
      throw new Error('Falha ao obter URL do Discord');
    }
    const data = await response.json();
    return data.url;
  },
}; 