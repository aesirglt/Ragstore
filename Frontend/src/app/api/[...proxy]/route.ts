import { NextRequest, NextResponse } from 'next/server';

export async function GET(request: NextRequest) {
  const { searchParams } = new URL(request.url);
  const target = 'http://localhost:53766';
  const path = request.nextUrl.pathname.replace('/api', '');
  console.log(path);

  try {
    const url = new URL(path, target);
    searchParams.delete('target');
    url.search = searchParams.toString();

    const response = await fetch(url.toString(), {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        ...Object.fromEntries(request.headers.entries()),
      },
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

export async function POST(request: NextRequest) {
  const { searchParams } = new URL(request.url);
  const target = searchParams.get('target') || 'http://localhost:60378';
  const path = request.nextUrl.pathname.replace('/api', '');

  try {
    const url = new URL(path, target);
    searchParams.delete('target');
    url.search = searchParams.toString();

    const body = await request.json();
    const response = await fetch(url.toString(), {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
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