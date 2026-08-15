import { forwardRef, InputHTMLAttributes } from "react";

interface InputProps extends InputHTMLAttributes<HTMLInputElement> {
  label?: string;
  error?: string;
}

const Input = forwardRef<HTMLInputElement, InputProps>(
  ({ label, error, className = "", id, ...props }, ref) => {
    const inputId = id || label?.toLowerCase().replace(/\s+/g, "-");

    return (
      <div className="w-full">
        {label && (
          <label
            htmlFor={inputId}
            className="block text-sm font-medium text-text mb-1"
          >
            {label}
          </label>
        )}
        <input
          ref={ref}
          id={inputId}
          className={`w-full border rounded-md px-4 py-2 bg-background text-text placeholder:text-text-muted focus:outline-none focus:ring-2 transition ${
            error
              ? "border-error focus:ring-error"
              : "border-border focus:ring-primary"
          } ${className}`}
          {...props}
        />
        {error && <p className="text-error text-sm mt-1">{error}</p>}
      </div>
    );
  },
);

Input.displayName = "Input";
export default Input;
