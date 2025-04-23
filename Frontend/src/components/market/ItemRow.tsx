import { Item } from "@/types/item";
import { Button } from "@/components/ui/button";

export default function ItemRow({ item, preco, quantidade, localizacao, vendedor }: Item) {
  return (
    <div className="grid grid-cols-6 items-center px-4 py-2 border-b hover:bg-gray-50">
      <div className="flex items-center gap-2">
        <img src="/placeholder.png" alt={item} className="w-6 h-6" />
        {item}
      </div>
      <div>{preco}</div>
      <div>{quantidade}</div>
      <div>{localizacao}</div>
      <div>{vendedor}</div>
      <div>
        <Button variant="outline">Filtrar</Button>
      </div>
    </div>
  );
}
