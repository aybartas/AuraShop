import { ReactNode } from "react";

const variantClasses = {
  primary: "bg-primary-light text-primary",
  accent: "bg-accent-light text-accent-foreground",
  success: "bg-success-light text-success",
  error: "bg-error-light text-error",
  warning: "bg-warning-light text-warning",
} as const;

const sizeClasses = {
  sm: "px-2 py-0.5 text-xs",
  md: "px-2.5 py-1 text-sm",
} as const;

interface BadgeProps {
  variant?: keyof typeof variantClasses;
  size?: keyof typeof sizeClasses;
  children: ReactNode;
  className?: string;
}

export default function Badge({
  variant = "primary",
  size = "sm",
  children,
  className = "",
}: BadgeProps) {
  return (
    <span
      className={`inline-flex items-center font-semibold rounded-full ${variantClasses[variant]} ${sizeClasses[size]} ${className}`}
    >
      {children}
    </span>
  );
}
