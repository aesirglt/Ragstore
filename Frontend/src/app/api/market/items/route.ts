import { NextRequest, NextResponse } from 'next/server';

const mockItems = [
  {
    itemId: 1,
    itemName: 'Espada Sagrada',
    price: 500000,
    quantity: 2,
    image: '',
    category: 'weapon',
  },
  {
    itemId: 2,
    itemName: 'Armadura do Herói',
    price: 1200000,
    quantity: 1,
    image: '',
    category: 'armor',
  },
  {
    itemId: 3,
    itemName: 'Carta MvP',
    price: 50000000,
    quantity: 1,
    image: '',
    category: 'card',
  },
  {
    itemId: 4,
    itemName: 'Carta MvP',
    price: 50000000,
    quantity: 1,
    image: '',
    category: 'card',
  },
  {
    itemId: 5,
    itemName: 'Carta MvP',
    price: 50000000,
    quantity: 1,
    image: '',
    category: 'card',
  },
  {
    itemId: 6,
    itemName: 'Carta MvP',
    price: 50000000,
    quantity: 1,
    image: '',
    category: 'card',
  },
  {
    itemId: 7,
    itemName: 'Carta MvP',
    price: 50000000,
    quantity: 1,
    image: '',
    category: 'card',
  },
  {
    itemId: 8,
    itemName: 'Carta MvP',
    price: 50000000,
    quantity: 1,
    image: '',
    category: 'card',
  },
  {
    itemId: 9,
    itemName: 'Carta MvP',
    price: 50000000,
    quantity: 1,
    image: '',
    category: 'card',
  },
  {
    itemId: 10,
    itemName: 'Carta MvP',
    price: 50000000,
    quantity: 1,
    image: '',
    category: 'card',
  }
];

export async function GET(req: NextRequest) {
  const { searchParams } = new URL(req.url);
  const page = parseInt(searchParams.get('page') || '1');
  const limit = parseInt(searchParams.get('limit') || '16');
  const search = searchParams.get('search') || '';
  const category = searchParams.get('category') || '';
  const priceOrder = searchParams.get('priceOrder') || '';
  const server = searchParams.get('server') || '';

  const url = `http://localhost:60378/${server}/stores-vending/items`;
  console.log(url)
  let items = [...mockItems];

  try {
    const res = await fetch(url, {
      method: 'GET',
      headers: {
        'Accept': 'application/json;odata.metadata=minimal;odata.streaming=true',
      },
    });
    const data = await res.json();
    console.log(data)

    return NextResponse.json(data);
  } catch (error) {
    return NextResponse.json(items);
  }
}
