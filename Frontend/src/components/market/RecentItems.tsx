import React from 'react';

export interface RecentItem {
  id: string;
  name: string;
  quantity: number;
  unitPrice: number;
  total: number;
  time: string;
}

interface RecentItemsProps {
  items: RecentItem[];
  loading: boolean;
}

const RecentItems: React.FC<RecentItemsProps> = ({ items, loading }) => {
  if (loading) {
    return (
      <div className="bg-white rounded-lg shadow p-6">
        <h2 className="text-xl font-bold text-gray-800 mb-4">Itens Recentes</h2>
        <div className="flex justify-center items-center h-40">
          <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-500"></div>
        </div>
      </div>
    );
  }

  return (
    <div className="bg-white rounded-lg shadow p-6">
      <h2 className="text-xl font-bold text-gray-800 mb-4">Itens Recentes</h2>
      
      {items.length === 0 ? (
        <div className="text-center py-6">
          <p className="text-gray-500">Nenhum item recente encontrado</p>
        </div>
      ) : (
        <div className="overflow-x-auto">
          <table className="min-w-full divide-y divide-gray-200">
            <thead className="bg-gray-50">
              <tr>
                <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Nome
                </th>
                <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Quantidade
                </th>
                <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Preço Unitário
                </th>
                <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Total
                </th>
                <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Horário
                </th>
              </tr>
            </thead>
            <tbody className="bg-white divide-y divide-gray-200">
              {items.map((item) => (
                <tr key={item.id} className="hover:bg-gray-50">
                  <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">
                    {item.name}
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                    {item.quantity}
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                    {item.unitPrice.toLocaleString('pt-BR', { minimumFractionDigits: 2 })} ƶ
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                    {item.total.toLocaleString('pt-BR', { minimumFractionDigits: 2 })} ƶ
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                    {item.time}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
      
      {items.length > 0 && (
        <div className="mt-4 text-right">
          <p className="text-sm text-gray-500">
            Total de itens: <span className="font-medium">{items.length}</span>
          </p>
        </div>
      )}
    </div>
  );
};

export default RecentItems;