import Link from "next/link";
import { FaUserCircle } from "react-icons/fa";

export default function Header() {
  return (
    <header className="bg-blue-400 text-white py-4 px-8 flex justify-between items-center">
      <div className="text-2xl font-bold">
        <span className="text-yellow-400">Rag</span>Store
      </div>
      <nav className="flex gap-6 text-white">
        <Link href="/" passHref className="hover:underline font-bold border-b-2 border-orange-400">
          Home
        </Link>
        <Link href="/market" passHref className="hover:underline font-bold border-b-2 border-orange-400">
          Mercado
        </Link>
      </nav>
      <div className="flex items-center gap-4">
        <FaUserCircle />
        <span>Minha Conta</span>
        <label className="flex items-center gap-2">
          <span>Modo Escuro</span>
          <input type="checkbox" className="toggle" />
        </label>
      </div>
    </header>
  );
}
