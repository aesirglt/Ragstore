import { FaUserCircle } from "react-icons/fa";

export default function Profile() {
  return (
    <div className="flex items-center gap-2">
      <FaUserCircle size="2xl" />
      <span className="text-sm">Minha Conta</span>
    </div>
  );
}
