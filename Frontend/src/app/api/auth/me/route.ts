import { NextRequest, NextResponse } from 'next/server';
import { config } from '@/config/env';

export async function GET(req: NextRequest) {
  // Repassar cookies de autenticação
  const cookie = req.headers.get('cookie');

  const response = await fetch(`${config.backendUrl}/profile`, {
    headers: {
      'cookie': cookie || '',
    },
    credentials: 'include',
  });

  const data = await response.json();
  return NextResponse.json(data, { status: response.status });
} 