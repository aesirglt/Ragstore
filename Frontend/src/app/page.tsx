"use client";
import React, { useState, useEffect } from 'react';
import { fetchMarketSummary, fetchRecentItems, RecentItem } from './api/market/route';
import MarketSummary from '@/components/market/MarketSummary';
import RecentItems from '@/components/market/RecentItems';

export default function Home() {
  const [recentItems, setRecentItems] = useState<RecentItem[]>([]);
  const [marketSummary, setMarketSummary] = useState({
    vending: { minValue: 0, currentMinValue: 0, currentMaxValue: 0, average: 0, storeNumbers: 0 },
    buying: { minValue: 0, currentMinValue: 0, currentMaxValue: 0, average: 0, storeNumbers: 0 }
  });
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const loadData = async () => {
      try {
        const itemsData = await fetchRecentItems();
        setRecentItems(itemsData);
        
        // For demo, just use the first item ID for market summary
        if (itemsData.length > 0) {
          const itemId = itemsData[0].id;
          const server = 'default'; // Replace with actual server value
          const vendingSummary = await fetchMarketSummary(server, itemId, 'vending');
          const buyingSummary = await fetchMarketSummary(server, itemId, 'buying');
          
          setMarketSummary({
            vending: vendingSummary,
            buying: buyingSummary
          });
        }
      } catch (error) {
        console.error('Failed to load home data:', error);
      } finally {
        setLoading(false);
      }
    };
    
    loadData();
  }, []);

  return (
      <div className="flex flex-col md:flex-row gap-6">
        <div className="flex-1">
          <div className="bg-white p-4 rounded shadow mb-6">
            <div className="mb-4">
              <h1 className="text-2xl font-medium text-gray-700">Seja bem-vindo, Lindinho</h1>
              <p className="text-sm text-gray-500">Entre em sua conta e tenha otimos beneficios.</p>
              <button className="mt-2 bg-orange-500 text-white px-3 py-1 rounded text-sm">
                Login →
              </button>
            </div>
          </div>
          
          <div className="bg-white p-4 rounded shadow mb-6">
            <div className="flex items-center text-orange-500 mb-3">
              <span className="mr-2">📢</span>
              <p className="text-sm">
                Você sabia que pode nos ajudar? Clique aqui...
              </p>
            </div>
          </div>
          
          <div className="bg-white p-4 rounded shadow">
            <div className="flex justify-between items-center mb-4">
              <h2 className="text-lg font-medium">Últimas Transações</h2>
              <a href="#" className="text-blue-500 text-sm">Ver mais</a>
            </div>
            <RecentItems items={recentItems} loading={loading} />
          </div>
        </div>
        
        <div className="md:w-80">
          <div className="bg-white p-4 rounded shadow mb-6">
            <MarketSummary data={marketSummary} loading={loading} />
          </div>
          
          <div className="bg-white p-4 rounded shadow">
            <h2 className="text-lg font-medium mb-4">Meus Mercadores</h2>
            {/* <MerchantsList /> */}
            <div className="mt-4 text-center">
              <button className="px-4 py-2 border border-yellow-500 text-yellow-500 rounded">
                Mercador « 
              </button>
            </div>
          </div>
        </div>
      </div>
  );
}