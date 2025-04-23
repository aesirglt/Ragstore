import { ButtonHTMLAttributes } from "react";
import clsx from "clsx";

interface ButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
  variant?: "default" | "outline";
}

export function Button({ children, variant = "default", className, ...props }: ButtonProps) {
  return (
    <button
      {...props}
      className={clsx(
        "px-3 py-1 rounded text-sm",
        variant === "default" && "bg-orange-500 text-white hover:bg-orange-600",
        variant === "outline" && "border border-gray-300 hover:bg-gray-100",
        className
      )}
    >
      {children}
    </button>
  );
}
