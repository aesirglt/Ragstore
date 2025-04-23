import ItemRow from "./ItemRow";
import { Item } from "@/types/item";

interface Props {
  items: Item[];
}

export default function ItemList({ items }: Props) {
  return (
    <div className="bg-white rounded shadow overflow-hidden">
      <div className="grid grid-cols-6 px-4 py-2 bg-gray-100 font-bold text-sm">
        <div>Item</div>
        <div>Preço</div>
        <div>Quantidade</div>
        <div>Localização</div>
        <div>Vendedor</div>
        <div>Ação</div>
      </div>
      {items.map((item, idx) => (
        <ItemRow key={idx} {...item} />
      ))}
    </div>
  );
}
