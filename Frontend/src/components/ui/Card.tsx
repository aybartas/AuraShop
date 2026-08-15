import { HTMLAttributes, ReactNode } from "react";

const paddingClasses = {
  none: "",
  sm: "p-3",
  md: "p-4",
  lg: "p-6",
} as const;

interface CardProps extends HTMLAttributes<HTMLDivElement> {
  children: ReactNode;
  hoverable?: boolean;
  padding?: keyof typeof paddingClasses;
}

export default function Card({
  children,
  hoverable = false,
  padding = "none",
  className = "",
  ...props
}: CardProps) {
  return (
    <div
      className={`bg-background rounded-lg shadow-md border border-border overflow-hidden ${paddingClasses[padding]} ${
        hoverable ? "hover:shadow-lg transition-shadow cursor-pointer" : ""
      } ${className}`}
      {...props}
    >
      {children}
    </div>
  );
}
