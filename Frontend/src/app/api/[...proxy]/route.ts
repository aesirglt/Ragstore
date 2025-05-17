import { NextRequest, NextResponse } from 'next/server';
import { config } from '@/config/env';

export async function GET(request: NextRequest) {
  const { searchParams } = new URL(request.url);
  const target = config.backendUrl;
  const path = request.nextUrl.pathname.replace('/api', '');
  
  try {
    const url = new URL(path, target);
    searchParams.delete('target');
    url.search = searchParams.toString();

    const cookie = request.headers.get('cookie');

    const response = await fetch(url.toString(), {
      method: 'GET',
      headers: {
        'accept': 'application/json;odata.metadata=minimal;odata.streaming=true',
        ...(cookie ? { 'cookie': cookie } : {})
      }
    });
    if (!response.ok) {
      const errorData = await response.json().catch(() => ({}));
      throw new Error(`HTTP error! status: ${response.status}`, { cause: errorData });
    }

    const data = await response.json();
    return NextResponse.json(data);
  } catch (error: any) {
    console.error('Proxy error:', {
      message: error.message,
      cause: error.cause,
      stack: error.stack
    });
    return NextResponse.json(
      { 
        error: 'Erro ao fazer proxy da requisição', 
        details: error.message,
        cause: error.cause
      },
      { status: 500 }
    );
  }
}

export async function POST(request: NextRequest) {
  const { searchParams } = new URL(request.url);
  const target = config.backendUrl;
  const path = request.nextUrl.pathname.replace('/api', '');

  try {
    const url = new URL(path, target);
    searchParams.delete('target');
    url.search = searchParams.toString();

    const body = await request.json();
    // Repassar cookies do request original
    const cookie = request.headers.get('cookie');
    const response = await fetch(url.toString(), {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        ...(cookie ? { 'cookie': cookie } : {}),
        ...Object.fromEntries(request.headers.entries()),
      },
      body: JSON.stringify(body),
    });

    if (!response.ok) {
      const errorData = await response.json().catch(() => ({}));
      throw new Error(`HTTP error! status: ${response.status}`, { cause: errorData });
    }

    const data = await response.json();
    return NextResponse.json(data);
  } catch (error: any) {
    console.error('Proxy error:', error);
    return NextResponse.json(
      { 
        error: 'Erro ao fazer proxy da requisição', 
        details: error.message,
        cause: error.cause
      },
      { status: 500 }
    );
  }
} 