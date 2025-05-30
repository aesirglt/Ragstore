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
  name: string;
  email: string;
  avatarUrl?: string;
  memberSince: string;
  isActive: boolean;
  callbacksCount: number;
  searchCount: number;
  receivePriceAlerts: boolean;
}

export interface ExternalLoginRequest {
  provider: 'google' | 'discord';
  token: string;
}

export interface CallbackResumeViewModel {
  id: string;
  server: string;
  itemId: number;
  itemPrice: number;
  storeType: string;
  itemUrl: string;
}

export interface AuthContextType {
  user: User | null;
  isAuthenticated: boolean;
  loading: boolean;
  login: (email: string, password: string) => Promise<void>;
  logout: () => Promise<void>;
  updateUser: (user: User) => Promise<void>;
  signOut: () => void;
} 