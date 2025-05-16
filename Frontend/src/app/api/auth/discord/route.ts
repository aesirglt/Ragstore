import { NextRequest, NextResponse } from 'next/server';
import { config } from '@/config/env';

export async function GET(req: NextRequest) {
  const { searchParams } = new URL(req.url);
  const redirect = searchParams.get('redirect') || '/';
  return NextResponse.redirect(`${config.backendUrl}/auth/discord?redirect=${encodeURIComponent(redirect)}`);
} 