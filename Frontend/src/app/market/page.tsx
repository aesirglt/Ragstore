"use client";
import { useEffect, useState } from 'react';
import Head from 'next/head';

interface Item {
  id: number;
  storeId: number;
  itemName: string;
  price: number;
  quantity: number;
  image: string;
  category: string;
}

export default function MarketPage() {
  const [items, setItems] = useState<Item[]>([]);
  const [filters, setFilters] = useState({
    search: '',
    category: '',
    priceOrder: '',
    server: '',
  });

  useEffect(() => {
    const query = new URLSearchParams();
    if (filters.search) query.append('search', filters.search);
    if (filters.category) query.append('category', filters.category);
    if (filters.priceOrder) query.append('priceOrder', filters.priceOrder);
    if (filters.server) query.append('server', filters.server);

    fetch(`/api/market/items?${query.toString()}`)
      .then(res => res.json())
      .then((data: Item[]) => {
        setItems(data);
      });
  }, [filters]);

  const formatPrice = (price: number) => price.toLocaleString('pt-BR');

  return (
    <>
      <Head>
        <title>Mercado</title>
      </Head>
      <header className="text-white p-2 shadow-md">
        <div className="container mx-auto">
          <h1 className="text-xl font-bold">Mercado</h1>
        </div>
      </header>
      <main className="container mx-auto p-4">
        <div className="flex gap-2 mb-4">
          <input
            type="text"
            placeholder="Pesquisar itens..."
            className="flex-1 p-3 border border-gray-300 rounded"
            onChange={e => setFilters({ ...filters, search: e.target.value })}
            onKeyUp={e => e.key === 'Enter' && setFilters({ ...filters })}
          />
          <button
            className="p-3 px-6 bg-blue-500 text-white rounded hover:bg-blue-600"
            onClick={() => setFilters({ ...filters })}
          >
            Buscar
          </button>
        </div>
        <div className="flex flex-wrap gap-4 mb-4">
          <div className="flex flex-col gap-1">
            <label htmlFor="category">Categoria</label>
            <select
              id="category"
              className="p-2 border border-gray-300 rounded"
              onChange={e => setFilters({ ...filters, category: e.target.value })}
            >
              <option value="">Todas</option>
              <option value="weapon">Armas</option>
              <option value="armor">Armaduras</option>
              <option value="card">Cartas</option>
              <option value="potion">Poções</option>
              <option value="material">Materiais</option>
            </select>
          </div>
          <div className="flex flex-col gap-1">
            <label htmlFor="server">Servidor</label>
            <select
              id="server"
              className="p-2 border border-gray-300 rounded"
              onChange={e => setFilters({ ...filters, server: e.target.value })}
            >
              <option value="thor">Latam</option>
            </select>
          </div>
        </div>
        <div className="grid grid-cols-[repeat(auto-fill,minmax(280px,1fr))] gap-4">
          {items.length === 0 ? (
            <p className="col-span-full text-center py-10">Nenhum item encontrado.</p>
          ) : (
            items.map(item => (
              <div key={item.id} className="bg-white rounded-lg shadow hover:shadow-lg hover:-translate-y-1 transition p-2">
                <div className="w-full h-44 bg-gray-200 flex items-center justify-center">
                  <img src="/api/placeholder/200/180" alt={item.itemName} width={100} height={100} />
                </div>
                <div className="p-3">
                  <h3 className="font-bold mb-1">{item.itemName}</h3>
                  <p className="text-red-500 font-semibold text-lg mb-1">{formatPrice(item.price)} zeny</p>
                  <p className="text-sm text-gray-500">Quantidade: {item.quantity}</p>
                </div>
              </div>
            ))
          )}
        </div>
      </main>
    </>
  );
}