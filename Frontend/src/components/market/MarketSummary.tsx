import React from 'react';

// Interface para definir a estrutura dos dados para cada operação (compra/venda)
interface MarketData {
  minValue: number;
  currentMinValue: number;
  currentMaxValue: number;
  average: number;
  storeNumbers: number;
}

// Interface para as props do componente
interface MarketSummaryProps {
  data: {
    vending: MarketData;
    buying: MarketData;
  };
  loading: boolean;
}

// Componente para mostrar os detalhes de cada operação (compra/venda)
const MarketOperation: React.FC<{ title: string; data: MarketData }> = ({ title, data }) => {
  return (
    <div className="rounded-lg bg-gray-50 p-4 shadow-sm">
      <h3 className="text-lg font-medium text-gray-900 mb-2">{title}</h3>
      <div className="grid grid-cols-2 gap-2">
        <div className="bg-white p-3 rounded shadow-sm">
          <p className="text-sm text-gray-500">Valor Mínimo</p>
          <p className="font-semibold">{data.minValue.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}</p>
        </div>
        <div className="bg-white p-3 rounded shadow-sm">
          <p className="text-sm text-gray-500">Mínimo Atual</p>
          <p className="font-semibold">{data.currentMinValue.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}</p>
        </div>
        <div className="bg-white p-3 rounded shadow-sm">
          <p className="text-sm text-gray-500">Máximo Atual</p>
          <p className="font-semibold">{data.currentMaxValue.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}</p>
        </div>
        <div className="bg-white p-3 rounded shadow-sm">
          <p className="text-sm text-gray-500">Média</p>
          <p className="font-semibold">{data.average.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}</p>
        </div>
        <div className="bg-white p-3 rounded shadow-sm col-span-2">
          <p className="text-sm text-gray-500">Número de Lojas</p>
          <p className="font-semibold text-center">{data.storeNumbers}</p>
        </div>
      </div>
    </div>
  );
};

// Componente principal MarketSummary
const MarketSummary: React.FC<MarketSummaryProps> = ({ data, loading }) => {
  if (loading) {
    return (
      <div className="p-6 bg-white rounded-lg shadow">
        <div className="flex justify-center items-center h-40">
          <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-500"></div>
        </div>
      </div>
    );
  }

  return (
    <div className="bg-white rounded-lg shadow p-6">
      <h2 className="text-xl font-bold text-gray-800 mb-4">Resumo do Mercado</h2>
      <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
        <MarketOperation title="Vendendo" data={data.vending} />
        <MarketOperation title="Comprando" data={data.buying} />
      </div>
    </div>
  );
};

export default MarketSummary;