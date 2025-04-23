import { NextRequest, NextResponse } from 'next/server';

const mockItems = [
  {
    id: 1,
    itemName: 'Espada Sagrada',
    price: 500000,
    quantity: 2,
    image: '',
    category: 'weapon',
  },
  {
    id: 2,
    itemName: 'Armadura do Herói',
    price: 1200000,
    quantity: 1,
    image: '',
    category: 'armor',
  },
  {
    id: 3,
    itemName: 'Carta MvP',
    price: 50000000,
    quantity: 1,
    image: '',
    category: 'card',
  },
];

export async function GET(req: NextRequest) {
  const searchParams = req.nextUrl.searchParams;
  const server = searchParams.get('server') || 'thor';
  const queryString = searchParams.toString();

  const search = searchParams.get('search')?.toLowerCase() || '';
  const category = searchParams.get('category') || '';
  const priceOrder = searchParams.get('priceOrder') || '';

  const url = `https://localhost:62242/${server}/stores-buying/items?${queryString}`;

  let items = [...mockItems];

  if (search) {
    items = items.filter(item => item.itemName.toLowerCase().includes(search));
  }

  if (category) {
    items = items.filter(item => item.category === category);
  }

  if (priceOrder === 'asc') {
    items.sort((a, b) => a.price - b.price);
  } else if (priceOrder === 'desc') {
    items.sort((a, b) => b.price - a.price);
  }

  try {
    const res = await fetch(url);
    const data = await res.json();
    return NextResponse.json(data);
  } catch (error) {
    console.error('Erro ao buscar dados:', error);
    return NextResponse.json(items, { status: 500 });
  }
}
