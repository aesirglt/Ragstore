import { NextResponse } from 'next/server';
import { config } from '@/config/env';

export async function GET() {
  const response = await fetch(`${config.backendUrl}/auth/discord`);
  const data = await response.json();
  
  return NextResponse.redirect(data.url);
} 