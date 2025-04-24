import React, { useState, useEffect } from 'react';
import { useRouter } from 'next/router';
import Link from 'next/link';
import { fetchStoreDetails } from '@/app/api/market/route';

interface StoreItem {
  id: string;
  name: string;
  icon: string | null;
  price: number;
  quantity: number;
}

interface Store {
  id: string;
  name: string;
  owner: string;
  location: string;
  items: StoreItem[];
}

export default function StoreDetail() {
  const router = useRouter();
  const { id } = router.query;

  const [store, setStore] = useState<Store | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const loadData = async () => {
      if (!id || typeof id !== 'string') return;

      try {
        const storeData = await fetchStoreDetails(id);
        setStore(storeData);
      } catch (error) {
        console.error('Failed to load store details:', error);
      } finally {
        setLoading(false);
      }
    };

    loadData();
  }, [id]);

  if (loading) {
    return (
    <div className="bg-white rounded shadow p-6">
        <p>Carregando detalhes da loja...</p>
    </div>
    );
  }

  if (!store) {
    return (
    <div className="bg-white rounded shadow p-6">
        <p>Loja não encontrada.</p>
        <Link href="/market">
        <button className="mt-4 bg-blue-500 text-white px-4 py-2 rounded">
            Voltar para o Mercado
        </button>
        </Link>
    </div>
    );
  }

  return (
      <div className="bg-white rounded shadow p-6">
        <div className="flex items-center mb-6">
          <div className="ml-4">
            <h1 className="text-2xl font-medium text-gray-700">{store.name}</h1>
            <p className="text-sm text-gray-500">Proprietário: {store.owner}</p>
            <p className="text-sm text-gray-500">Localização: {store.location}</p>
          </div>
        </div>

        <div className="mb-6">
          <h2 className="text-lg font-medium mb-4">Itens à venda</h2>

          <div className="overflow-x-auto">
            <table className="min-w-full divide-y divide-gray-200">
              <thead>
                <tr>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Item
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Preço
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Quantidade
                  </th>
                  <th></th>
                </tr>
              </thead>
              <tbody className="bg-white divide-y divide-gray-200">
                {store.items.length === 0 ? (
                  <tr>
                    <td colSpan={4} className="px-6 py-4 text-center">Esta loja não tem itens à venda no momento</td>
                  </tr>
                ) : (
                  store.items.map((item) => (
                    <tr key={item.id} className="hover:bg-gray-50">
                      <td className="px-6 py-4 whitespace-nowrap">
                        <Link href={`/market/item/${item.id}`}>
                          <div className="flex items-center cursor-pointer">
                            <div className="flex-shrink-0 h-10 w-10 bg-gray-200 rounded-full flex items-center justify-center">
                              {item.icon ? (
                                <img src={item.icon} alt={item.name} className="h-8 w-8" />
                              ) : (
                                <span>🔍</span>
                              )}
                            </div>
                            <div className="ml-4">
                              <div className="text-sm font-medium text-gray-900">{item.name}</div>
                            </div>
                          </div>
                        </Link>
                      </td>
                      <td className="px-6 py-4 whitespace-nowrap">
                        <div className="text-sm text-gray-900">{item.price.toLocaleString()} z</div>
                      </td>
                      <td className="px-6 py-4 whitespace-nowrap">
                        <div className="text-sm text-gray-900">{item.quantity}</div>
                      </td>
                      <td className="px-6 py-4 whitespace-nowrap text-right">
                        <span className="text-yellow-500">▼</span>
                      </td>
                    </tr>
                  ))
                )}
              </tbody>
            </table>
          </div>
        </div>

        <div className="flex justify-between items-center">
          <Link href="/market">
            <button className="bg-gray-200 text-gray-700 px-4 py-2 rounded">
              Voltar para o Mercado
            </button>
          </Link>
        </div>
      </div>
  );
}
