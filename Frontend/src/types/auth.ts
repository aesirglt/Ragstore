export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  email: string;
  password: string;
  confirmPassword: string;
}

export interface AuthResponse {
  token: string;
  expiresIn: number;
  user: User;
}

export interface User {
  id: string;
  email: string;
  name: string;
  avatar?: string;
}

export interface ExternalLoginRequest {
  provider: 'google' | 'discord';
  token: string;
}

export interface CallbackResumeViewModel {
  server: string;
  itemId: number;
  itemPrice: number;
  storeType: string;
} 