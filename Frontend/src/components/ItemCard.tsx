import { ItemViewModel } from '@/types/api/viewmodels/ItemViewModel';

interface ItemCardProps {
    item: ItemViewModel;
}

export function ItemCard({ item }: ItemCardProps) {
    return (
        <div className="bg-white rounded-lg shadow-md p-4">
            <div className="flex items-center gap-4">
                <img 
                    src={item.image} 
                    alt={item.itemName}
                    className="w-16 h-16 object-cover rounded"
                />
                <div>
                    <h3 className="font-semibold">{item.itemName}</h3>
                    <p className="text-gray-600">Preço: {item.price.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}</p>
                    <p className="text-gray-600">Quantidade: {item.quantity}</p>
                    <p className="text-gray-600">Categoria: {item.category}</p>
                </div>
            </div>
        </div>
    );
} 