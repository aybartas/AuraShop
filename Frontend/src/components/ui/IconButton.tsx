import { forwardRef, ButtonHTMLAttributes, ReactNode } from "react";

const variantClasses = {
  default: "text-text-secondary hover:bg-surface",
  primary: "text-primary hover:bg-primary-light",
  danger: "text-error hover:bg-error-light",
} as const;

const sizeClasses = {
  sm: "p-1.5",
  md: "p-2",
  lg: "p-3",
} as const;

interface IconButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
  icon: ReactNode;
  variant?: keyof typeof variantClasses;
  size?: keyof typeof sizeClasses;
  "aria-label": string;
}

const IconButton = forwardRef<HTMLButtonElement, IconButtonProps>(
  ({ icon, variant = "default", size = "md", className = "", ...props }, ref) => {
    return (
      <button
        ref={ref}
        className={`inline-flex items-center justify-center rounded-full transition-colors duration-200 focus:outline-none focus:ring-2 focus:ring-primary focus:ring-offset-2 disabled:opacity-50 disabled:cursor-not-allowed ${variantClasses[variant]} ${sizeClasses[size]} ${className}`}
        {...props}
      >
        {icon}
      </button>
    );
  },
);

IconButton.displayName = "IconButton";
export default IconButton;
