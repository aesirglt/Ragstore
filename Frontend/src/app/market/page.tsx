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
  
  // Adicionar estados para paginação
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [itemsPerPage] = useState(16); // Aumentei para 16 já que os cards serão menores
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    const fetchItems = async () => {
      setLoading(true);
      const query = new URLSearchParams();
      if (filters.search) query.append('search', filters.search);
      if (filters.category) query.append('category', filters.category);
      if (filters.priceOrder) query.append('priceOrder', filters.priceOrder);
      if (filters.server) query.append('server', filters.server);
      query.append('page', currentPage.toString());
      query.append('limit', itemsPerPage.toString());

      try {
        const res = await fetch(`/api/market/items?${query.toString()}`);
        console.error(res);

        const data = await res.json();
        
        if (data.items && Array.isArray(data.items)) {
          setItems(data.items);
          setTotalPages(Math.ceil(data.totalCount / itemsPerPage));
        } else {
          setItems(data);
          setTotalPages(data.length < itemsPerPage ? 1 : currentPage + 1);
        }
      } catch (error) {
        console.error("Erro ao buscar itens:", error);
        setItems([]);
      } finally {
        setLoading(false);
      }
    };

    fetchItems();
  }, [filters, currentPage, itemsPerPage]);

  const formatPrice = (price: number) => price.toLocaleString('pt-BR');

  // Funções para navegação de páginas
  const goToPage = (page: number) => {
    window.scrollTo(0, 0);
    setCurrentPage(page);
  };

  const nextPage = () => {
    if (currentPage < totalPages) {
      goToPage(currentPage + 1);
    }
  };

  const prevPage = () => {
    if (currentPage > 1) {
      goToPage(currentPage - 1);
    }
  };

  // Gerar array de páginas para o componente de paginação
  const getPageNumbers = () => {
    const pages = [];
    const maxVisiblePages = 5;
    
    if (totalPages <= maxVisiblePages) {
      // Mostrar todas as páginas se houver menos que o máximo visível
      for (let i = 1; i <= totalPages; i++) {
        pages.push(i);
      }
    } else {
      // Mostrar páginas com ellipsis
      if (currentPage <= 3) {
        // Caso esteja nas primeiras páginas
        for (let i = 1; i <= 4; i++) {
          pages.push(i);
        }
        pages.push("...");
        pages.push(totalPages);
      } else if (currentPage >= totalPages - 2) {
        // Caso esteja nas últimas páginas
        pages.push(1);
        pages.push("...");
        for (let i = totalPages - 3; i <= totalPages; i++) {
          pages.push(i);
        }
      } else {
        // Caso esteja no meio
        pages.push(1);
        pages.push("...");
        pages.push(currentPage - 1);
        pages.push(currentPage);
        pages.push(currentPage + 1);
        pages.push("...");
        pages.push(totalPages);
      }
    }
    
    return pages;
  };

  return (
    <>
      <Head>
        <title>Mercado</title>
      </Head>
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
            onClick={() => {
              setCurrentPage(1); // Resetar para a primeira página ao buscar
              setFilters({ ...filters });
            }}
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
              onChange={e => {
                setCurrentPage(1); // Resetar para a primeira página ao mudar filtro
                setFilters({ ...filters, category: e.target.value });
              }}
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
              onChange={e => {
                setCurrentPage(1); // Resetar para a primeira página ao mudar servidor
                setFilters({ ...filters, server: e.target.value });
              }}
            >
              <option value="thor">Latam</option>
            </select>
          </div>
        </div>
        
        {/* Indicador de carregamento */}
        {loading && (
          <div className="text-center py-10">
            <p>Carregando...</p>
          </div>
        )}
        
        {/* Lista de itens - MODIFICADO AQUI PARA CARDS MENORES */}
        <div className="grid grid-cols-[repeat(auto-fill,minmax(140px,1fr))] gap-3">
          {!loading && items.length === 0 ? (
            <p className="col-span-full text-center py-10">Nenhum item encontrado.</p>
          ) : (
            items.map(item => (
              <div key={item.id} className="bg-white rounded-lg shadow hover:shadow-lg hover:-translate-y-1 transition p-1">
                <div className="w-full h-24 bg-gray-200 flex items-center justify-center">
                  <img src="/api/placeholder/100/90" alt={item.itemName} width={50} height={50} />
                </div>
                <div className="p-2">
                  <h3 className="font-bold text-sm mb-1 truncate">{item.itemName}</h3>
                  <p className="text-red-500 font-semibold text-xs mb-1">{formatPrice(item.price)} zeny</p>
                  <p className="text-xs text-gray-500">Qtd: {item.quantity}</p>
                </div>
              </div>
            ))
          )}
        </div>
        
        {/* Componente de paginação */}
        {!loading && totalPages > 1 && (
          <div className="flex justify-center items-center mt-8 mb-4">
            <nav className="flex items-center gap-1">
              <button 
                onClick={prevPage} 
                disabled={currentPage === 1}
                className={`px-3 py-1 rounded ${currentPage === 1 ? 'bg-gray-200 text-gray-500 cursor-not-allowed' : 'bg-blue-500 text-white hover:bg-blue-600'}`}
              >
                Anterior
              </button>
              
              {getPageNumbers().map((page, index) => (
                page === "..." ? (
                  <span key={`ellipsis-${index}`} className="px-3 py-1">...</span>
                ) : (
                  <button
                    key={`page-${page}`}
                    onClick={() => typeof page === 'number' && goToPage(page)}
                    className={`px-3 py-1 rounded ${currentPage === page ? 'bg-blue-600 text-white' : 'bg-gray-200 hover:bg-gray-300'}`}
                  >
                    {page}
                  </button>
                )
              ))}
              
              <button 
                onClick={nextPage} 
                disabled={currentPage === totalPages}
                className={`px-3 py-1 rounded ${currentPage === totalPages ? 'bg-gray-200 text-gray-500 cursor-not-allowed' : 'bg-blue-500 text-white hover:bg-blue-600'}`}
              >
                Próxima
              </button>
            </nav>
          </div>
        )}
      </main>
    </>
  );
}