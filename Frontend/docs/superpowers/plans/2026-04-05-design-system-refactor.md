# Design System Refactor Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Introduce a centralized design system with CSS variable tokens, Tailwind integration, reusable UI primitives & composites, dark mode, and migrate all existing feature components to use the new system.

**Architecture:** CSS custom properties in `src/styles/theme.css` define all tokens (colors, radii, shadows). `tailwind.config.js` maps these to semantic Tailwind classes (`bg-primary`, `text-accent`). UI primitives (`src/components/ui/`) compose into higher-level composites (`src/components/`). Feature pages consume these — no hardcoded colors anywhere.

**Tech Stack:** React 18, TypeScript, Tailwind CSS 3, CSS custom properties, Inter font (Google Fonts)

**Spec:** `docs/superpowers/specs/2026-04-05-design-system-refactor.md`

**Note:** No test framework exists in this project. Each task ends with `npm run build` for TypeScript + Vite verification.

---

## File Structure

### New files to create:
- `src/styles/theme.css` — All CSS variable design tokens (light + dark)
- `src/components/ui/Button.tsx` — Button primitive
- `src/components/ui/Input.tsx` — Input primitive
- `src/components/ui/Card.tsx` — Card primitive
- `src/components/ui/Badge.tsx` — Badge primitive
- `src/components/ui/Modal.tsx` — Modal primitive
- `src/components/ui/Skeleton.tsx` — Skeleton primitive
- `src/components/ui/IconButton.tsx` — IconButton primitive
- `src/components/ui/index.ts` — Barrel export
- `src/components/ThemeToggle.tsx` — Dark mode toggle
- `src/components/StarRating.tsx` — Star rating display
- `src/components/PriceDisplay.tsx` — Formatted price
- `src/components/QuantitySelector.tsx` — +/- quantity control
- `src/components/OrderSummary.tsx` — Order summary panel
- `src/components/AddressCard.tsx` — Address selection card
- `src/components/ProductCard.tsx` — Moved from `src/app/components/product/`
- `src/app/features/home/ProductList.tsx` — Renamed from `ProductLİst.tsx` (fix typo)

### Files to modify:
- `tailwind.config.js` — Add semantic color/shadow/radius mappings
- `src/index.css` — Import Inter font + theme.css
- `src/app/layout/PageLayout.tsx` — Use theme tokens
- `src/app/layout/Header.tsx` — Use theme tokens + Button + Badge + ThemeToggle
- `src/app/features/home/Home.tsx` — Use Card + Button + theme tokens
- `src/app/features/home/Carousel.tsx` — Use Button + IconButton + theme tokens
- `src/app/features/home/ProductSection.tsx` — Use Card + Badge + PriceDisplay + theme tokens
- `src/app/features/catalog/Catalog.tsx` — Update ProductList import path
- `src/app/features/catalog/FilterForm.tsx` — Use Input + Button + theme tokens
- `src/app/features/catalog/ProductDetails.tsx` — Use Button + StarRating + PriceDisplay + theme tokens
- `src/app/features/cart/Cart.tsx` — Use Button + Badge + QuantitySelector + OrderSummary + Input
- `src/app/features/cart/CartSkeleton.tsx` — Use Skeleton + theme tokens
- `src/app/features/checkout/Checkout.tsx` — Use Button + Modal + Input + OrderSummary + AddressCard
- `src/app/features/checkout/CheckoutSkeleton.tsx` — Use Skeleton + theme tokens

### Files to delete:
- `src/app/components/product/ProductCard.tsx` — Moved to `src/components/ProductCard.tsx`
- `src/app/features/home/ProductLİst.tsx` — Replaced by `ProductList.tsx` (typo fix)

---

### Task 1: Theme Foundation

**Files:**
- Create: `src/styles/theme.css`
- Modify: `tailwind.config.js`
- Modify: `src/index.css`

- [ ] **Step 1: Create the theme CSS file with all design tokens**

Create `src/styles/theme.css`:

```css
:root {
  /* Primary - Indigo */
  --color-primary: #4f46e5;
  --color-primary-hover: #4338ca;
  --color-primary-light: #e0e7ff;
  --color-primary-foreground: #ffffff;

  /* Accent - Gold */
  --color-accent: #eab308;
  --color-accent-hover: #ca8a04;
  --color-accent-light: #fef9c3;
  --color-accent-foreground: #1e1b4b;

  /* Neutral */
  --color-background: #ffffff;
  --color-surface: #f8fafc;
  --color-surface-hover: #f1f5f9;
  --color-border: #e2e8f0;
  --color-text: #0f172a;
  --color-text-secondary: #64748b;
  --color-text-muted: #94a3b8;

  /* Semantic */
  --color-success: #059669;
  --color-success-light: #d1fae5;
  --color-error: #dc2626;
  --color-error-light: #fee2e2;
  --color-warning: #f59e0b;
  --color-warning-light: #fef3c7;

  /* Typography */
  --font-sans: "Inter", system-ui, sans-serif;

  /* Border Radius */
  --radius-sm: 0.375rem;
  --radius-md: 0.5rem;
  --radius-lg: 0.75rem;
  --radius-xl: 1rem;
  --radius-full: 9999px;

  /* Shadows */
  --shadow-sm: 0 1px 2px rgba(0, 0, 0, 0.05);
  --shadow-md: 0 4px 6px -1px rgba(0, 0, 0, 0.07),
    0 2px 4px -2px rgba(0, 0, 0, 0.05);
  --shadow-lg: 0 10px 15px -3px rgba(0, 0, 0, 0.08),
    0 4px 6px -4px rgba(0, 0, 0, 0.04);
}

.dark {
  --color-primary: #818cf8;
  --color-primary-hover: #6366f1;
  --color-primary-light: #1e1b4b;
  --color-primary-foreground: #ffffff;

  --color-accent: #facc15;
  --color-accent-hover: #eab308;
  --color-accent-light: #422006;
  --color-accent-foreground: #fefce8;

  --color-background: #0f172a;
  --color-surface: #1e293b;
  --color-surface-hover: #334155;
  --color-border: #334155;
  --color-text: #f1f5f9;
  --color-text-secondary: #94a3b8;
  --color-text-muted: #64748b;

  --color-success: #34d399;
  --color-success-light: #064e3b;
  --color-error: #f87171;
  --color-error-light: #450a0a;
  --color-warning: #fbbf24;
  --color-warning-light: #451a03;

  --shadow-sm: 0 1px 2px rgba(0, 0, 0, 0.2);
  --shadow-md: 0 4px 6px -1px rgba(0, 0, 0, 0.3),
    0 2px 4px -2px rgba(0, 0, 0, 0.2);
  --shadow-lg: 0 10px 15px -3px rgba(0, 0, 0, 0.35),
    0 4px 6px -4px rgba(0, 0, 0, 0.2);
}
```

- [ ] **Step 2: Update tailwind.config.js to map CSS variables to Tailwind classes**

Replace the entire contents of `tailwind.config.js` with:

```js
/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  darkMode: "class",
  theme: {
    extend: {
      fontFamily: {
        sans: ["var(--font-sans)"],
      },
      colors: {
        primary: {
          DEFAULT: "var(--color-primary)",
          hover: "var(--color-primary-hover)",
          light: "var(--color-primary-light)",
          foreground: "var(--color-primary-foreground)",
        },
        accent: {
          DEFAULT: "var(--color-accent)",
          hover: "var(--color-accent-hover)",
          light: "var(--color-accent-light)",
          foreground: "var(--color-accent-foreground)",
        },
        background: "var(--color-background)",
        surface: {
          DEFAULT: "var(--color-surface)",
          hover: "var(--color-surface-hover)",
        },
        border: "var(--color-border)",
        text: {
          DEFAULT: "var(--color-text)",
          secondary: "var(--color-text-secondary)",
          muted: "var(--color-text-muted)",
        },
        success: {
          DEFAULT: "var(--color-success)",
          light: "var(--color-success-light)",
        },
        error: {
          DEFAULT: "var(--color-error)",
          light: "var(--color-error-light)",
        },
        warning: {
          DEFAULT: "var(--color-warning)",
          light: "var(--color-warning-light)",
        },
      },
      borderRadius: {
        sm: "var(--radius-sm)",
        md: "var(--radius-md)",
        lg: "var(--radius-lg)",
        xl: "var(--radius-xl)",
      },
      boxShadow: {
        sm: "var(--shadow-sm)",
        md: "var(--shadow-md)",
        lg: "var(--shadow-lg)",
      },
    },
  },
  plugins: [],
};
```

- [ ] **Step 3: Update index.css to import Inter font and theme**

Replace the entire contents of `src/index.css` with:

```css
@import url("https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700&display=swap");
@import "./styles/theme.css";

@tailwind base;
@tailwind components;
@tailwind utilities;

@layer base {
  body {
    @apply bg-background text-text font-sans;
  }
}
```

- [ ] **Step 4: Verify build**

Run: `npm run build`
Expected: Build passes with zero errors.

- [ ] **Step 5: Commit**

```bash
git add src/styles/theme.css tailwind.config.js src/index.css
git commit -m "feat: add design system foundation with CSS variable tokens and Tailwind integration"
```

---

### Task 2: UI Primitive Components

**Files:**
- Create: `src/components/ui/Button.tsx`
- Create: `src/components/ui/Input.tsx`
- Create: `src/components/ui/Card.tsx`
- Create: `src/components/ui/Badge.tsx`
- Create: `src/components/ui/Modal.tsx`
- Create: `src/components/ui/Skeleton.tsx`
- Create: `src/components/ui/IconButton.tsx`
- Create: `src/components/ui/index.ts`

- [ ] **Step 1: Create Button component**

Create `src/components/ui/Button.tsx`:

```tsx
import { forwardRef, ButtonHTMLAttributes, ReactNode } from "react";

const variantClasses = {
  primary:
    "bg-primary text-primary-foreground hover:bg-primary-hover focus:ring-primary",
  accent:
    "bg-accent text-accent-foreground hover:bg-accent-hover focus:ring-accent",
  outline:
    "border border-border bg-transparent text-text hover:bg-surface focus:ring-primary",
  ghost: "bg-transparent text-text-secondary hover:bg-surface focus:ring-primary",
  danger: "bg-error text-white hover:bg-red-700 focus:ring-error",
  success: "bg-success text-white hover:bg-emerald-700 focus:ring-success",
} as const;

const sizeClasses = {
  sm: "px-3 py-1.5 text-sm",
  md: "px-4 py-2 text-sm",
  lg: "px-6 py-3 text-base",
} as const;

interface ButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
  variant?: keyof typeof variantClasses;
  size?: keyof typeof sizeClasses;
  loading?: boolean;
  fullWidth?: boolean;
  children: ReactNode;
}

const Button = forwardRef<HTMLButtonElement, ButtonProps>(
  (
    {
      variant = "primary",
      size = "md",
      loading = false,
      fullWidth = false,
      disabled,
      className = "",
      children,
      ...props
    },
    ref,
  ) => {
    return (
      <button
        ref={ref}
        disabled={disabled || loading}
        className={`inline-flex items-center justify-center gap-2 font-medium rounded-lg transition-colors duration-200 focus:outline-none focus:ring-2 focus:ring-offset-2 disabled:opacity-50 disabled:cursor-not-allowed ${variantClasses[variant]} ${sizeClasses[size]} ${fullWidth ? "w-full" : ""} ${className}`}
        {...props}
      >
        {loading && (
          <svg
            className="w-4 h-4 animate-spin"
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
          >
            <circle
              className="opacity-25"
              cx="12"
              cy="12"
              r="10"
              stroke="currentColor"
              strokeWidth="4"
            />
            <path
              className="opacity-75"
              fill="currentColor"
              d="M4 12a8 8 0 018-8v4a4 4 0 00-4 4H4z"
            />
          </svg>
        )}
        {children}
      </button>
    );
  },
);

Button.displayName = "Button";
export default Button;
```

- [ ] **Step 2: Create Input component**

Create `src/components/ui/Input.tsx`:

```tsx
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
        {error && (
          <p className="text-error text-sm mt-1">{error}</p>
        )}
      </div>
    );
  },
);

Input.displayName = "Input";
export default Input;
```

- [ ] **Step 3: Create Card component**

Create `src/components/ui/Card.tsx`:

```tsx
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
```

- [ ] **Step 4: Create Badge component**

Create `src/components/ui/Badge.tsx`:

```tsx
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
```

- [ ] **Step 5: Create Modal component**

Create `src/components/ui/Modal.tsx`:

```tsx
import { ReactNode, useEffect } from "react";

interface ModalProps {
  open: boolean;
  onClose: () => void;
  title: string;
  children: ReactNode;
  size?: "sm" | "md" | "lg";
}

const sizeClasses = {
  sm: "max-w-sm",
  md: "max-w-md",
  lg: "max-w-lg",
};

export default function Modal({
  open,
  onClose,
  title,
  children,
  size = "md",
}: ModalProps) {
  useEffect(() => {
    if (!open) return;
    const handleEsc = (e: KeyboardEvent) => {
      if (e.key === "Escape") onClose();
    };
    document.addEventListener("keydown", handleEsc);
    return () => document.removeEventListener("keydown", handleEsc);
  }, [open, onClose]);

  if (!open) return null;

  return (
    <div
      className="fixed inset-0 z-50 flex items-center justify-center bg-black/30 backdrop-blur-sm"
      onClick={onClose}
    >
      <div
        className={`bg-background rounded-xl shadow-lg p-6 w-full ${sizeClasses[size]} relative`}
        onClick={(e) => e.stopPropagation()}
      >
        <div className="flex items-center justify-between mb-4">
          <h4 className="text-lg font-semibold text-text">{title}</h4>
          <button
            onClick={onClose}
            className="text-text-muted hover:text-text text-xl leading-none"
            aria-label="Close"
          >
            &times;
          </button>
        </div>
        {children}
      </div>
    </div>
  );
}
```

- [ ] **Step 6: Create Skeleton component**

Create `src/components/ui/Skeleton.tsx`:

```tsx
interface SkeletonProps {
  className?: string;
  variant?: "text" | "circle" | "rect";
  width?: string;
  height?: string;
}

export default function Skeleton({
  className = "",
  variant = "rect",
  width,
  height,
}: SkeletonProps) {
  const variantClasses = {
    text: "rounded h-4",
    circle: "rounded-full",
    rect: "rounded-md",
  };

  return (
    <div
      className={`animate-pulse bg-surface-hover ${variantClasses[variant]} ${className}`}
      style={{ width, height }}
    />
  );
}
```

- [ ] **Step 7: Create IconButton component**

Create `src/components/ui/IconButton.tsx`:

```tsx
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
  (
    {
      icon,
      variant = "default",
      size = "md",
      className = "",
      ...props
    },
    ref,
  ) => {
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
```

- [ ] **Step 8: Create barrel export**

Create `src/components/ui/index.ts`:

```ts
export { default as Button } from "./Button";
export { default as Input } from "./Input";
export { default as Card } from "./Card";
export { default as Badge } from "./Badge";
export { default as Modal } from "./Modal";
export { default as Skeleton } from "./Skeleton";
export { default as IconButton } from "./IconButton";
```

- [ ] **Step 9: Verify build**

Run: `npm run build`
Expected: Build passes with zero errors.

- [ ] **Step 10: Commit**

```bash
git add src/components/ui/
git commit -m "feat: add UI primitive components (Button, Input, Card, Badge, Modal, Skeleton, IconButton)"
```

---

### Task 3: Composite Components

**Files:**
- Create: `src/components/ThemeToggle.tsx`
- Create: `src/components/StarRating.tsx`
- Create: `src/components/PriceDisplay.tsx`
- Create: `src/components/QuantitySelector.tsx`
- Create: `src/components/OrderSummary.tsx`
- Create: `src/components/AddressCard.tsx`

- [ ] **Step 1: Create ThemeToggle component**

Create `src/components/ThemeToggle.tsx`:

```tsx
import { useEffect, useState } from "react";
import { SunIcon, MoonIcon } from "@heroicons/react/24/outline";

export default function ThemeToggle() {
  const [dark, setDark] = useState(() => {
    if (typeof window === "undefined") return false;
    return (
      localStorage.getItem("theme") === "dark" ||
      (!localStorage.getItem("theme") &&
        window.matchMedia("(prefers-color-scheme: dark)").matches)
    );
  });

  useEffect(() => {
    const root = document.documentElement;
    if (dark) {
      root.classList.add("dark");
      localStorage.setItem("theme", "dark");
    } else {
      root.classList.remove("dark");
      localStorage.setItem("theme", "light");
    }
  }, [dark]);

  return (
    <button
      onClick={() => setDark(!dark)}
      className="p-2 rounded-full text-text-secondary hover:bg-surface transition-colors"
      aria-label={dark ? "Switch to light mode" : "Switch to dark mode"}
    >
      {dark ? (
        <SunIcon className="h-5 w-5" />
      ) : (
        <MoonIcon className="h-5 w-5" />
      )}
    </button>
  );
}
```

- [ ] **Step 2: Create StarRating component**

Create `src/components/StarRating.tsx`:

```tsx
interface StarRatingProps {
  rating: number;
  maxStars?: number;
  size?: "sm" | "md" | "lg";
  showValue?: boolean;
}

const sizeClasses = {
  sm: "text-sm",
  md: "text-xl",
  lg: "text-2xl",
};

export default function StarRating({
  rating,
  maxStars = 5,
  size = "md",
  showValue = false,
}: StarRatingProps) {
  const filled = Math.floor(rating);
  const empty = maxStars - filled;

  return (
    <div className="inline-flex items-center gap-1">
      <span className={`text-accent ${sizeClasses[size]}`}>
        {"★".repeat(filled)}
      </span>
      <span className={`text-text-muted ${sizeClasses[size]}`}>
        {"☆".repeat(empty)}
      </span>
      {showValue && (
        <span className="text-sm text-text-secondary ml-1">
          ({rating} / {maxStars})
        </span>
      )}
    </div>
  );
}
```

- [ ] **Step 3: Create PriceDisplay component**

Create `src/components/PriceDisplay.tsx`:

```tsx
interface PriceDisplayProps {
  amount: number;
  originalAmount?: number;
  currency?: string;
  size?: "sm" | "md" | "lg";
}

const sizeClasses = {
  sm: "text-base",
  md: "text-xl",
  lg: "text-2xl",
};

export default function PriceDisplay({
  amount,
  originalAmount,
  currency = "$",
  size = "md",
}: PriceDisplayProps) {
  return (
    <div className="inline-flex items-center gap-2">
      <span className={`font-bold text-accent ${sizeClasses[size]}`}>
        {currency}
        {amount.toFixed(2)}
      </span>
      {originalAmount && originalAmount > amount && (
        <span className="text-text-muted line-through text-sm">
          {currency}
          {originalAmount.toFixed(2)}
        </span>
      )}
    </div>
  );
}
```

- [ ] **Step 4: Create QuantitySelector component**

Create `src/components/QuantitySelector.tsx`:

```tsx
import { MdAdd, MdRemove } from "react-icons/md";
import IconButton from "./ui/IconButton";

interface QuantitySelectorProps {
  value: number;
  onIncrement: () => void;
  onDecrement: () => void;
  min?: number;
  max?: number;
}

export default function QuantitySelector({
  value,
  onIncrement,
  onDecrement,
  min = 1,
  max = 99,
}: QuantitySelectorProps) {
  return (
    <div className="flex items-center gap-2">
      <IconButton
        icon={<MdRemove size={20} />}
        aria-label="Decrease quantity"
        onClick={onDecrement}
        disabled={value <= min}
        className="border border-border"
      />
      <span className="w-8 text-center text-lg font-medium text-text">
        {value}
      </span>
      <IconButton
        icon={<MdAdd size={20} />}
        aria-label="Increase quantity"
        onClick={onIncrement}
        disabled={value >= max}
        className="border border-border"
      />
    </div>
  );
}
```

- [ ] **Step 5: Create OrderSummary component**

Create `src/components/OrderSummary.tsx`:

```tsx
import Badge from "./ui/Badge";

interface OrderSummaryProps {
  subtotal: number;
  shipping: number;
  discount?: number;
  discountRate?: number;
  total: number;
  currency?: string;
}

export default function OrderSummary({
  subtotal,
  shipping,
  discount = 0,
  discountRate,
  total,
  currency = "$",
}: OrderSummaryProps) {
  return (
    <div className="space-y-4 text-text text-base">
      <div className="flex justify-between font-semibold">
        <span>Subtotal</span>
        <span>
          {currency}
          {subtotal.toFixed(2)}
        </span>
      </div>

      <div className="flex flex-col gap-1">
        <div className="flex justify-between font-semibold">
          <span>Shipping</span>
          {shipping === 0 ? (
            <Badge variant="success" size="md">
              Free Shipping
            </Badge>
          ) : (
            <span>
              {currency}
              {shipping.toFixed(2)}
            </span>
          )}
        </div>
      </div>

      {discount > 0 && (
        <div className="flex justify-between text-success font-semibold">
          <span>Discount{discountRate ? ` (${discountRate}%)` : ""}</span>
          <span>
            - {currency}
            {discount.toFixed(2)}
          </span>
        </div>
      )}

      <div className="flex justify-between border-t border-border pt-4 text-lg font-bold">
        <span>Total</span>
        <span>
          {currency}
          {total.toFixed(2)}
        </span>
      </div>
    </div>
  );
}
```

- [ ] **Step 6: Create AddressCard component**

Create `src/components/AddressCard.tsx`:

```tsx
import { Address } from "../types/Address";

interface AddressCardProps {
  address: Address & { id: string; title: string };
  selected: boolean;
  onSelect: () => void;
}

export default function AddressCard({
  address,
  selected,
  onSelect,
}: AddressCardProps) {
  return (
    <div
      onClick={onSelect}
      className={`p-4 border rounded-lg cursor-pointer min-w-[220px] transition ${
        selected
          ? "border-primary bg-primary-light shadow"
          : "border-border hover:border-text-muted bg-background"
      }`}
    >
      <h5 className="font-medium text-primary">{address.title}</h5>
      <p className="text-sm text-text-secondary">
        {address.street}, {address.city}, {address.state}, {address.zipCode},{" "}
        {address.country}
      </p>
    </div>
  );
}
```

- [ ] **Step 7: Verify build**

Run: `npm run build`
Expected: Build passes with zero errors.

- [ ] **Step 8: Commit**

```bash
git add src/components/ThemeToggle.tsx src/components/StarRating.tsx src/components/PriceDisplay.tsx src/components/QuantitySelector.tsx src/components/OrderSummary.tsx src/components/AddressCard.tsx
git commit -m "feat: add composite components (ThemeToggle, StarRating, PriceDisplay, QuantitySelector, OrderSummary, AddressCard)"
```

---

### Task 4: Migrate ProductCard and ProductList

**Files:**
- Create: `src/components/ProductCard.tsx`
- Create: `src/app/features/home/ProductList.tsx`
- Delete: `src/app/components/product/ProductCard.tsx`
- Delete: `src/app/features/home/ProductLİst.tsx`
- Modify: `src/app/features/catalog/Catalog.tsx:3`
- Modify: `src/app/features/home/ProductList.tsx` (new file, references new ProductCard)

- [ ] **Step 1: Create new ProductCard at src/components/ProductCard.tsx**

```tsx
import { useNavigate } from "react-router-dom";
import { Product } from "../types/Product";
import Card from "./ui/Card";
import PriceDisplay from "./PriceDisplay";

interface ProductCardProps {
  product: Product;
}

export default function ProductCard({ product }: ProductCardProps) {
  const navigate = useNavigate();

  return (
    <Card
      hoverable
      onClick={() => navigate(`/catalog/${product.id}`)}
    >
      <img
        src={product.images?.[0] || "https://via.placeholder.com/300x200"}
        alt={product.name}
        className="w-full h-48 object-cover"
      />
      <div className="p-4">
        <h3 className="text-lg font-semibold text-text">{product.name}</h3>
        <p className="text-text-secondary text-sm mb-2 line-clamp-2">
          {product.description}
        </p>
        <PriceDisplay amount={product.price} />
      </div>
    </Card>
  );
}
```

- [ ] **Step 2: Create fixed ProductList (rename from ProductLİst)**

Create `src/app/features/home/ProductList.tsx`:

```tsx
import React from "react";
import { Product } from "../../../types/Product";
import ProductCard from "../../../components/ProductCard";
import Skeleton from "../../../components/ui/Skeleton";

interface Props {
  products: Product[];
  loading: boolean;
}

function ProductList({ products, loading }: Props) {
  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-4 gap-4">
      {loading
        ? Array.from({ length: 8 }).map((_, index) => (
            <div key={index} className="bg-background shadow-md rounded-md p-4 space-y-4">
              <Skeleton className="h-40 w-full" />
              <Skeleton className="h-4 w-3/4" />
              <Skeleton className="h-4 w-1/2" />
              <Skeleton className="h-4 w-1/3" />
            </div>
          ))
        : products.map((product) => (
            <React.Fragment key={product.id}>
              <ProductCard product={product} />
            </React.Fragment>
          ))}
    </div>
  );
}

export default ProductList;
```

- [ ] **Step 3: Delete old files**

```bash
rm "src/app/components/product/ProductCard.tsx"
rm "src/app/features/home/ProductLİst.tsx"
```

After deleting, if `src/app/components/product/` is empty, also remove it:

```bash
rmdir "src/app/components/product"
rmdir "src/app/components"
```

- [ ] **Step 4: Update Catalog.tsx import**

In `src/app/features/catalog/Catalog.tsx`, change line 3 from:

```tsx
import ProductList from "../home/ProductLİst";
```

to:

```tsx
import ProductList from "../home/ProductList";
```

- [ ] **Step 5: Verify build**

Run: `npm run build`
Expected: Build passes with zero errors.

- [ ] **Step 6: Commit**

```bash
git add -A
git commit -m "refactor: move ProductCard to components, fix ProductList filename typo"
```

---

### Task 5: Migrate Layout Components (PageLayout + Header)

**Files:**
- Modify: `src/app/layout/PageLayout.tsx`
- Modify: `src/app/layout/Header.tsx`

- [ ] **Step 1: Migrate PageLayout.tsx**

Replace the entire contents of `src/app/layout/PageLayout.tsx` with:

```tsx
import { ReactNode } from "react";

interface LayoutProps {
  children: ReactNode;
}

export default function PageLayout({ children }: LayoutProps) {
  return (
    <div className="min-h-screen flex flex-col">
      <main className="flex-1 bg-surface">
        <div className="container mx-auto py-8">{children}</div>
      </main>

      <footer className="bg-gray-800 text-white py-4">
        <div className="container mx-auto text-center">
          <p>&copy; 2025 AuraShop. All Rights Reserved.</p>
        </div>
      </footer>
    </div>
  );
}
```

- [ ] **Step 2: Migrate Header.tsx**

Replace the entire contents of `src/app/layout/Header.tsx` with:

```tsx
import logo from "../../assets/logo.svg";
import { useState } from "react";
import { ShoppingCartIcon } from "@heroicons/react/24/outline";
import { NavLink } from "react-router-dom";
import { useAuth } from "../../contexts/AuthContext";
import { useBasket } from "../../hooks/useBasket";
import { Button, Badge } from "../../components/ui";
import ThemeToggle from "../../components/ThemeToggle";

interface CategoryLink {
  name: string;
  url?: string;
  subcategories?: string[];
}
const categories: CategoryLink[] = [{ name: "Catalog", url: "/catalog" }];

export default function Header() {
  const { keycloak } = useAuth();
  const { basket } = useBasket();

  const [isOpen, setIsOpen] = useState(false);
  const [activeCategory, setActiveCategory] = useState<number | null>(null);

  const handleMouseEnter = (index: number) => setActiveCategory(index);
  const handleMouseLeave = (index: number) => {
    if (activeCategory === index) setActiveCategory(null);
  };

  return (
    <nav className="bg-background shadow-md sticky top-0 z-50 border-b border-border">
      <div className="container mx-auto px-4 py-3 flex items-center justify-between">
        <NavLink to="/" className="flex items-center space-x-2">
          <img src={logo} alt="AuraShop" className="h-8 w-8" />
          <span className="font-bold text-xl text-text">AuraShop</span>
        </NavLink>

        <div className="hidden md:flex items-center space-x-8">
          {categories.map((category, idx) => (
            <div
              key={idx}
              className="relative"
              onMouseEnter={() => handleMouseEnter(idx)}
              onMouseLeave={() => handleMouseLeave(idx)}
            >
              <NavLink
                to={category.url || "#"}
                className={({ isActive }) =>
                  `px-2 py-1 text-text-secondary hover:text-primary transition-colors ${
                    isActive ? "text-primary font-semibold" : ""
                  }`
                }
              >
                {category.name}
              </NavLink>

              {category.subcategories && activeCategory === idx && (
                <div className="absolute top-full left-0 mt-1 bg-background border border-border rounded-lg shadow-md min-w-[150px]">
                  {category.subcategories.map((sub, subIdx) => (
                    <NavLink
                      key={subIdx}
                      to="#"
                      className="block px-4 py-2 text-text-secondary hover:bg-primary-light hover:text-primary"
                    >
                      {sub}
                    </NavLink>
                  ))}
                </div>
              )}
            </div>
          ))}
        </div>

        <div className="hidden md:flex flex-1 max-w-md mx-6">
          <input
            type="search"
            placeholder="Search anything..."
            className="w-full border border-border rounded-lg px-4 py-2 bg-background text-text placeholder:text-text-muted focus:outline-none focus:ring-2 focus:ring-primary"
          />
        </div>

        <div className="hidden md:flex items-center space-x-4">
          <ThemeToggle />

          {keycloak?.authenticated ? (
            <>
              <span className="text-text-secondary">
                Hello,{" "}
                {keycloak.tokenParsed?.preferred_username ||
                  keycloak.tokenParsed?.email}
              </span>
              <Button
                variant="ghost"
                size="sm"
                onClick={() => keycloak.logout()}
              >
                Logout
              </Button>
            </>
          ) : (
            <>
              <Button
                variant="ghost"
                size="sm"
                onClick={() => keycloak?.login()}
              >
                Login
              </Button>
              <Button
                variant="outline"
                size="sm"
                onClick={() => keycloak?.register()}
              >
                Register
              </Button>
            </>
          )}

          <NavLink
            to="/cart"
            className="relative flex items-center text-text-secondary hover:text-primary transition-colors"
          >
            <ShoppingCartIcon className="h-5 w-5 mr-1" />
            Cart
            {basket?.basketItems && basket.basketItems.length > 0 && (
              <Badge
                variant="accent"
                size="sm"
                className="absolute -top-2 -right-6"
              >
                {basket.basketItems.length}
              </Badge>
            )}
          </NavLink>
        </div>

        {/* Mobile Hamburger */}
        <button
          className="md:hidden text-text focus:outline-none"
          aria-label="Toggle menu"
          onClick={() => setIsOpen(!isOpen)}
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            className="h-6 w-6"
            fill="none"
            viewBox="0 0 24 24"
            stroke="currentColor"
            strokeWidth={2}
          >
            {isOpen ? (
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="M6 18L18 6M6 6l12 12"
              />
            ) : (
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="M4 6h16M4 12h16M4 18h16"
              />
            )}
          </svg>
        </button>
      </div>

      {/* Mobile Menu */}
      {isOpen && (
        <div className="md:hidden bg-background border-t border-border shadow-lg px-4 pb-4">
          <div className="pt-2 space-y-1">
            {categories.map((category, idx) => (
              <div key={idx}>
                <button
                  className="w-full flex justify-between items-center py-2 text-text-secondary hover:text-primary focus:outline-none"
                  onClick={() =>
                    setActiveCategory(activeCategory === idx ? null : idx)
                  }
                >
                  {category.name}
                  <svg
                    className={`h-4 w-4 transform transition-transform ${
                      activeCategory === idx ? "rotate-180" : "rotate-0"
                    }`}
                    fill="none"
                    stroke="currentColor"
                    strokeWidth={2}
                    viewBox="0 0 24 24"
                    xmlns="http://www.w3.org/2000/svg"
                  >
                    <path
                      strokeLinecap="round"
                      strokeLinejoin="round"
                      d="M19 9l-7 7-7-7"
                    ></path>
                  </svg>
                </button>
                {activeCategory === idx && category.subcategories && (
                  <div className="pl-4 space-y-1">
                    {category.subcategories.map((sub, subIdx) => (
                      <NavLink
                        key={subIdx}
                        to="#"
                        className="block py-1 text-text-secondary hover:text-primary"
                      >
                        {sub}
                      </NavLink>
                    ))}
                  </div>
                )}
              </div>
            ))}
          </div>

          <div className="mt-4">
            <input
              type="search"
              placeholder="Search anything..."
              className="w-full border border-border rounded-lg px-4 py-2 bg-background text-text placeholder:text-text-muted focus:outline-none focus:ring-2 focus:ring-primary"
            />
          </div>

          <div className="mt-4 flex items-center justify-between">
            <ThemeToggle />
          </div>

          <div className="mt-4 space-y-2">
            {keycloak?.authenticated ? (
              <>
                <span className="block text-text-secondary">
                  Hello,{" "}
                  {keycloak.tokenParsed?.preferred_username ||
                    keycloak.tokenParsed?.email}
                </span>
                <Button
                  variant="ghost"
                  size="sm"
                  fullWidth
                  onClick={() => keycloak.logout()}
                >
                  Logout
                </Button>
                <Button
                  variant="ghost"
                  size="sm"
                  fullWidth
                  onClick={() =>
                    window.open(keycloak.createAccountUrl(), "_blank")
                  }
                >
                  Profile
                </Button>
              </>
            ) : (
              <>
                <Button
                  variant="ghost"
                  size="sm"
                  fullWidth
                  onClick={() => keycloak?.login()}
                >
                  Login
                </Button>
                <Button
                  variant="outline"
                  size="sm"
                  fullWidth
                  onClick={() => keycloak?.register()}
                >
                  Register
                </Button>
              </>
            )}
            <NavLink
              to="/cart"
              className="flex items-center text-text-secondary hover:text-primary transition-colors"
            >
              <ShoppingCartIcon className="h-5 w-5 mr-2" />
              Cart
            </NavLink>
          </div>
        </div>
      )}
    </nav>
  );
}
```

- [ ] **Step 3: Verify build**

Run: `npm run build`
Expected: Build passes with zero errors.

- [ ] **Step 4: Commit**

```bash
git add src/app/layout/PageLayout.tsx src/app/layout/Header.tsx
git commit -m "refactor: migrate PageLayout and Header to design system tokens"
```

---

### Task 6: Migrate Home Page (Home, Carousel, ProductSection)

**Files:**
- Modify: `src/app/features/home/Home.tsx`
- Modify: `src/app/features/home/Carousel.tsx`
- Modify: `src/app/features/home/ProductSection.tsx`

- [ ] **Step 1: Migrate Home.tsx**

Replace the entire contents of `src/app/features/home/Home.tsx` with:

```tsx
import Carousel from "./Carousel";
import PageLayout from "../../layout/PageLayout";
import ProductSection from "./ProductSection";
import Button from "../../../components/ui/Button";

function Home() {
  return (
    <PageLayout>
      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
        <div className="col-span-1 sm:col-span-2 lg:col-span-2">
          <Carousel />
        </div>

        <div className="flex flex-col gap-4 sm:grid sm:grid-cols-2 lg:grid-cols-1">
          <div className="relative bg-background rounded-lg shadow-md overflow-hidden">
            <img
              src="https://picsum.photos/300"
              alt="Product 2"
              className="w-full h-48 object-cover"
            />
            <div className="absolute inset-0 flex flex-col items-center justify-center bg-black/50">
              <h3 className="text-lg font-semibold text-white mb-2">
                Product 2
              </h3>
              <p className="text-white mb-4">Product description goes here.</p>
              <Button variant="primary" size="md">
                Shop Now
              </Button>
            </div>
          </div>

          <div className="relative bg-background rounded-lg shadow-md overflow-hidden">
            <img
              src="https://picsum.photos/300"
              alt="Product 3"
              className="w-full h-48 object-cover"
            />
            <div className="absolute inset-0 flex flex-col items-center justify-center bg-black/50">
              <h3 className="text-lg font-semibold text-white mb-2">
                Product 3
              </h3>
              <p className="text-white mb-4">Product description goes here.</p>
              <Button variant="primary" size="md">
                Shop Now
              </Button>
            </div>
          </div>
        </div>
      </div>

      <ProductSection />
    </PageLayout>
  );
}

export default Home;
```

- [ ] **Step 2: Migrate Carousel.tsx**

Replace the entire contents of `src/app/features/home/Carousel.tsx` with:

```tsx
import { useEffect, useState } from "react";
import {
  ShoppingCartIcon,
  ArrowLeftIcon,
  ArrowRightIcon,
} from "@heroicons/react/24/outline";
import Button from "../../../components/ui/Button";
import IconButton from "../../../components/ui/IconButton";

interface Slide {
  id: number;
  category: string;
  description: string;
  image: string;
}

const slides: Slide[] = [
  {
    id: 1,
    category: "Electronics",
    description: "Latest gadgets and electronics.",
    image:
      "https://www.eurokidsindia.com/blog/wp-content/uploads/2023/12/names-of-electronic-devices-in-english-870x570.jpg",
  },
  {
    id: 2,
    category: "Fashion",
    description: "Trendy clothing and accessories.",
    image:
      "https://media.istockphoto.com/id/1398610798/tr/foto%C4%9Fraf/young-woman-in-linen-shirt-shorts-and-high-heels-pointing-to-the-side-and-talking.jpg?s=612x612&w=0&k=20&c=dmIBAa6CCMNbP9PXTO5L1mlHspqmfRcf5yFhImOcB1c=",
  },
  {
    id: 3,
    category: "Home Decor",
    description: "Beautiful decor for your home.",
    image:
      "https://cdn.decorilla.com/online-decorating/wp-content/uploads/2023/01/Minimalist-home-decor-The-Spruce.jpg?width=900",
  },
];

export default function Carousel() {
  const [currentIndex, setCurrentIndex] = useState(0);

  const nextSlide = () => {
    setCurrentIndex((prev) => (prev + 1) % slides.length);
  };

  const prevSlide = () => {
    setCurrentIndex((prev) => (prev - 1 + slides.length) % slides.length);
  };

  useEffect(() => {
    const interval = setInterval(nextSlide, 5000);
    return () => clearInterval(interval);
  }, []);

  return (
    <div className="h-full relative w-full max-w-6xl mx-auto overflow-hidden">
      <div
        className="h-full relative flex items-center justify-center w-full bg-cover bg-center transition-all duration-1000 ease-in-out rounded-lg"
        style={{ backgroundImage: `url(${slides[currentIndex].image})` }}
      >
        <div className="absolute inset-0 bg-black/50 rounded-lg" />
        <div className="relative text-center text-white">
          <h2 className="text-2xl font-bold mb-2">
            {slides[currentIndex].category}
          </h2>
          <p className="mb-4">{slides[currentIndex].description}</p>
          <div className="flex justify-center">
            <Button variant="primary" size="md">
              <ShoppingCartIcon className="h-5 w-5" />
              Shop Now
            </Button>
          </div>
        </div>
      </div>

      <div className="absolute top-1/2 left-4 -translate-y-1/2">
        <IconButton
          icon={<ArrowLeftIcon className="h-5 w-5" />}
          aria-label="Previous slide"
          onClick={prevSlide}
          className="bg-gray-800 text-white hover:bg-gray-700"
        />
      </div>

      <div className="absolute top-1/2 right-4 -translate-y-1/2">
        <IconButton
          icon={<ArrowRightIcon className="h-5 w-5" />}
          aria-label="Next slide"
          onClick={nextSlide}
          className="bg-gray-800 text-white hover:bg-gray-700"
        />
      </div>
    </div>
  );
}
```

- [ ] **Step 3: Migrate ProductSection.tsx**

Replace the entire contents of `src/app/features/home/ProductSection.tsx` with:

```tsx
import Card from "../../../components/ui/Card";
import Badge from "../../../components/ui/Badge";
import PriceDisplay from "../../../components/PriceDisplay";

const bestSellers = [
  { name: "Product 1", description: "Product description goes here.", price: 49.99, image: "https://picsum.photos/300" },
  { name: "Product 2", description: "Product description goes here.", price: 59.99, image: "https://picsum.photos/300" },
  { name: "Product 3", description: "Product description goes here.", price: 39.99, image: "https://picsum.photos/300" },
  { name: "Product 4", description: "Product description goes here.", price: 69.99, image: "https://picsum.photos/300" },
];

const discountedProducts = [
  { name: "Discounted Product 1", description: "Product description goes here.", price: 39.99, originalPrice: 49.99, discount: 20, image: "https://picsum.photos/300" },
  { name: "Discounted Product 2", description: "Product description goes here.", price: 49.99, originalPrice: 58.82, discount: 15, image: "https://picsum.photos/300" },
  { name: "Discounted Product 3", description: "Product description goes here.", price: 29.99, originalPrice: 42.84, discount: 30, image: "https://picsum.photos/300" },
  { name: "Discounted Product 4", description: "Product description goes here.", price: 59.99, originalPrice: 66.66, discount: 10, image: "https://picsum.photos/300" },
];

export default function ProductSection() {
  return (
    <div className="space-y-12">
      <div className="mt-8">
        <h2 className="text-3xl font-bold text-text mb-8">Best Sellers</h2>
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-8">
          {bestSellers.map((product) => (
            <Card key={product.name} hoverable>
              <img
                src={product.image}
                alt={product.name}
                className="w-full h-48 object-cover"
              />
              <div className="p-4">
                <h3 className="text-lg font-semibold text-text">
                  {product.name}
                </h3>
                <p className="text-text-secondary mb-2">
                  {product.description}
                </p>
                <PriceDisplay amount={product.price} />
              </div>
            </Card>
          ))}
        </div>
      </div>

      <div className="py-12">
        <h2 className="text-3xl font-bold text-text mb-8">
          Amazing Discounts
        </h2>
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-8">
          {discountedProducts.map((product) => (
            <Card key={product.name} hoverable className="relative">
              <img
                src={product.image}
                alt={product.name}
                className="w-full h-48 object-cover"
              />
              <Badge
                variant="error"
                size="md"
                className="absolute top-2 right-2"
              >
                {product.discount}% Off
              </Badge>
              <div className="p-4">
                <h3 className="text-lg font-semibold text-text">
                  {product.name}
                </h3>
                <p className="text-text-secondary mb-2">
                  {product.description}
                </p>
                <PriceDisplay
                  amount={product.price}
                  originalAmount={product.originalPrice}
                />
              </div>
            </Card>
          ))}
        </div>
      </div>
    </div>
  );
}
```

- [ ] **Step 4: Verify build**

Run: `npm run build`
Expected: Build passes with zero errors.

- [ ] **Step 5: Commit**

```bash
git add src/app/features/home/
git commit -m "refactor: migrate Home, Carousel, ProductSection to design system"
```

---

### Task 7: Migrate Catalog (Catalog, FilterForm, ProductDetails)

**Files:**
- Modify: `src/app/features/catalog/Catalog.tsx`
- Modify: `src/app/features/catalog/FilterForm.tsx`
- Modify: `src/app/features/catalog/ProductDetails.tsx`

- [ ] **Step 1: Migrate Catalog.tsx**

Replace the entire contents of `src/app/features/catalog/Catalog.tsx` with:

```tsx
import PageLayout from "../../layout/PageLayout";
import FilterForm from "./FilterForm";
import ProductList from "../home/ProductList";
import { useEffect, useState } from "react";
import { Product } from "../../../types/Product";
import { CatalogService } from "../../../api/services/CatalogService";

function Catalog() {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    CatalogService.getProducts({})
      .then((res) => setProducts(res.data.data))
      .finally(() => setLoading(false));
  }, []);

  return (
    <PageLayout>
      <div className="grid grid-cols-4 gap-4">
        <div>
          <FilterForm />
        </div>
        <div className="col-span-3">
          <ProductList products={products} loading={loading} />
        </div>
      </div>
    </PageLayout>
  );
}

export default Catalog;
```

- [ ] **Step 2: Migrate FilterForm.tsx**

Replace the entire contents of `src/app/features/catalog/FilterForm.tsx` with:

```tsx
import { useState } from "react";
import { useForm, Controller } from "react-hook-form";
import { ChevronDownIcon, ChevronUpIcon } from "@heroicons/react/24/solid";
import { Button, Input } from "../../../components/ui";

interface FilterFormInputs {
  categories: string[];
  brands: string[];
  priceRange: { min: string; max: string };
}

const categoriesOptions = ["Electronics", "Clothing", "Home Appliances"];
const brandOptions = ["Samsung", "Nike", "LG"];

export default function FilterForm() {
  const { control, handleSubmit } = useForm<FilterFormInputs>();
  const [isCategoriesOpen, setIsCategoriesOpen] = useState(false);

  const onSubmit = (data: FilterFormInputs) => {
    console.log(data);
  };

  return (
    <form
      className="flex flex-col gap-4 bg-background p-6 shadow-md rounded-xl border border-border"
      onSubmit={handleSubmit(onSubmit)}
    >
      <h2 className="text-lg font-semibold text-text mb-4">Filter Products</h2>

      {/* Categories Filter */}
      <div
        className="flex items-center cursor-pointer mb-1 gap-2"
        onClick={() => setIsCategoriesOpen(!isCategoriesOpen)}
      >
        <span className="text-sm text-text-muted">
          {isCategoriesOpen ? (
            <ChevronUpIcon className="h-3 w-3" />
          ) : (
            <ChevronDownIcon className="h-3 w-3" />
          )}
        </span>
        <label className="block text-sm font-medium text-text">
          Categories
        </label>
      </div>
      {isCategoriesOpen && (
        <Controller
          name="categories"
          control={control}
          render={({ field }) => (
            <div className="flex flex-col gap-2">
              {categoriesOptions.map((category) => (
                <label
                  key={category}
                  className="inline-flex items-center gap-2 text-text-secondary"
                >
                  <input
                    type="checkbox"
                    value={category}
                    checked={field.value?.includes(category) || false}
                    onChange={(e) => {
                      const value = field.value || [];
                      if (e.target.checked) {
                        field.onChange([...value, category]);
                      } else {
                        field.onChange(value.filter((v) => v !== category));
                      }
                    }}
                    className="accent-primary"
                  />
                  {category}
                </label>
              ))}
            </div>
          )}
        />
      )}

      {/* Brands Filter */}
      <div>
        <label className="block text-sm font-medium text-text mb-1">
          Brands
        </label>
        <Controller
          name="brands"
          control={control}
          render={({ field }) => (
            <div className="flex flex-col gap-2">
              {brandOptions.map((brand) => (
                <label
                  key={brand}
                  className="inline-flex items-center gap-2 text-text-secondary"
                >
                  <input
                    type="checkbox"
                    value={brand}
                    checked={field.value?.includes(brand) || false}
                    onChange={(e) => {
                      const value = field.value || [];
                      if (e.target.checked) {
                        field.onChange([...value, brand]);
                      } else {
                        field.onChange(value.filter((v) => v !== brand));
                      }
                    }}
                    className="accent-primary"
                  />
                  {brand}
                </label>
              ))}
            </div>
          )}
        />
      </div>

      {/* Price Range Filter */}
      <div>
        <label className="block text-sm font-medium text-text mb-1">
          Price Range
        </label>
        <div className="flex items-center gap-4">
          <Controller
            name="priceRange.min"
            control={control}
            render={({ field }) => (
              <Input {...field} type="number" placeholder="Min" />
            )}
          />
          <span className="text-text-muted">-</span>
          <Controller
            name="priceRange.max"
            control={control}
            render={({ field }) => (
              <Input {...field} type="number" placeholder="Max" />
            )}
          />
        </div>
      </div>

      <Button type="submit" variant="primary" fullWidth>
        Apply Filters
      </Button>
    </form>
  );
}
```

- [ ] **Step 3: Migrate ProductDetails.tsx**

Replace the entire contents of `src/app/features/catalog/ProductDetails.tsx` with:

```tsx
import { Controller, useForm } from "react-hook-form";
import PageLayout from "../../layout/PageLayout";
import { Product } from "../../../types/Product";
import { useEffect, useState } from "react";
import { CatalogService } from "../../../api/services/CatalogService";
import { useParams } from "react-router-dom";
import { useAuth } from "../../../contexts/AuthContext";
import { BasketService } from "../../../api/services/BasketService";
import { useBasket } from "../../../hooks/useBasket";
import Button from "../../../components/ui/Button";
import Card from "../../../components/ui/Card";
import StarRating from "../../../components/StarRating";
import PriceDisplay from "../../../components/PriceDisplay";

interface AddToCartForm {
  size: string;
  color: string;
}

interface ProductComment {
  user: string;
  date: string;
  rating: number;
  comment: string;
}

function ProductDetails() {
  const { keycloak } = useAuth();
  const [product, setProduct] = useState<Product | null>(null);
  const [comments] = useState<ProductComment[]>([
    {
      user: "Jane Doe",
      date: new Date().toISOString(),
      rating: 4,
      comment: "Great product!",
    },
  ]);
  const [loading, setLoading] = useState(false);
  const { refreshBasket } = useBasket();
  const { id } = useParams();

  useEffect(() => {
    if (id) {
      CatalogService.getProduct(id)
        .then((res) => setProduct(res.data))
        .catch((error) => console.error("Failed to fetch product:", error));
    }
  }, [id]);

  const {
    handleSubmit,
    control,
    formState: { errors },
    watch,
  } = useForm<AddToCartForm>({
    defaultValues: { size: "", color: "" },
    mode: "onChange",
  });

  const formData = watch();

  const onSubmit = async (formData: AddToCartForm) => {
    if (!product) return;

    if (!keycloak?.authenticated) {
      keycloak?.login();
      return;
    }

    const cartItem = {
      productId: product.id,
      productName: product.name,
      price: product.price,
      quantity: 1,
      imageUrl: product.images?.[0],
      size: formData.size,
      color: formData.color,
    };

    setLoading(true);
    BasketService.addItemToCart(cartItem)
      .then(() => refreshBasket())
      .catch((error) => console.error("Failed to add item to cart:", error))
      .finally(() => setLoading(false));
  };

  if (!product)
    return (
      <PageLayout>
        <p className="text-text-secondary">Loading...</p>
      </PageLayout>
    );

  const { name, description, images, colors, sizes } = product;
  const image = images?.[0];
  const rating = 4.5;

  return (
    <PageLayout>
      <Card padding="lg" className="max-w-6xl mx-auto">
        <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
          <img
            src={image}
            alt={name}
            className="w-full h-96 object-cover rounded-lg"
          />

          <div className="flex flex-col">
            <h1 className="text-2xl font-bold text-text mb-2">{name}</h1>
            <PriceDisplay amount={product.price} size="lg" />
            <p className="text-text-secondary my-4">{description}</p>

            <div className="mb-4">
              <StarRating rating={rating} showValue />
            </div>

            <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
              {colors && colors.length > 0 && (
                <div>
                  <label className="block text-sm font-medium text-text mb-1">
                    Color: {formData.color}
                  </label>
                  <Controller
                    name="color"
                    control={control}
                    rules={{
                      required:
                        colors.length > 0 ? "Please select a color." : false,
                    }}
                    render={({ field }) => (
                      <div className="flex gap-2 flex-wrap">
                        {colors.map((colorObj) => (
                          <button
                            key={colorObj.hexCode}
                            type="button"
                            onClick={() => field.onChange(colorObj.name)}
                            className={`flex items-center px-4 py-2 min-w-[64px] rounded-md border text-sm font-medium transition-all duration-300 ${
                              field.value === colorObj.name
                                ? "border-primary text-primary shadow-md ring-1 ring-primary-light"
                                : "border-border bg-surface hover:border-text-muted"
                            }`}
                          >
                            <div
                              className="w-4 h-4 mr-2 rounded-full"
                              style={{ backgroundColor: colorObj.hexCode }}
                            />
                            {colorObj.name}
                          </button>
                        ))}
                      </div>
                    )}
                  />
                  {errors.color && (
                    <p className="text-error text-sm mt-1">
                      {errors.color.message}
                    </p>
                  )}
                </div>
              )}

              {sizes && sizes.length > 0 && (
                <div>
                  <label className="block text-sm font-medium text-text mb-1">
                    Size: {formData.size}
                  </label>
                  <Controller
                    name="size"
                    control={control}
                    rules={{
                      required:
                        sizes.length > 0 ? "Please select a size." : false,
                    }}
                    render={({ field }) => (
                      <div className="flex gap-2 flex-wrap">
                        {sizes.map((size) => (
                          <button
                            key={size}
                            type="button"
                            onClick={() => field.onChange(size)}
                            className={`px-4 py-2 min-w-[64px] rounded-md border text-sm font-medium transition-all duration-300 ${
                              field.value === size
                                ? "border-primary text-primary shadow-md ring-1 ring-primary-light"
                                : "border-border bg-surface hover:border-text-muted"
                            }`}
                          >
                            {size}
                          </button>
                        ))}
                      </div>
                    )}
                  />
                  {errors.size && (
                    <p className="text-error text-sm mt-1">
                      {errors.size.message}
                    </p>
                  )}
                </div>
              )}

              <div className="w-full justify-center flex pt-4">
                <Button
                  type="submit"
                  variant="primary"
                  size="lg"
                  loading={loading}
                  className="w-full sm:w-auto"
                >
                  {loading ? "Adding..." : "Add to Cart"}
                </Button>
              </div>
            </form>
          </div>
        </div>

        {/* Comments Section */}
        <div className="mt-6">
          <h2 className="text-xl font-bold text-text mb-4">Comments</h2>
          <ul className="space-y-3">
            {comments.map((comment, index) => (
              <li
                key={index}
                className="bg-surface p-4 rounded-lg shadow-sm flex items-start gap-4"
              >
                <div className="flex-shrink-0 w-10 h-10 bg-surface-hover rounded-full overflow-hidden">
                  <img
                    src="https://picsum.photos/300"
                    className="w-full h-full object-cover"
                    alt="User"
                  />
                </div>
                <div>
                  <div className="flex gap-2 items-center justify-between mb-1">
                    <span className="font-semibold text-text">
                      {comment.user}
                    </span>
                    <span className="text-sm text-text-muted">
                      {new Date(comment.date).toLocaleDateString()}
                    </span>
                  </div>
                  <StarRating rating={comment.rating} size="sm" />
                  <p className="text-text-secondary text-sm mt-1">
                    {comment.comment}
                  </p>
                </div>
              </li>
            ))}
          </ul>
        </div>
      </Card>
    </PageLayout>
  );
}

export default ProductDetails;
```

- [ ] **Step 4: Verify build**

Run: `npm run build`
Expected: Build passes with zero errors.

- [ ] **Step 5: Commit**

```bash
git add src/app/features/catalog/
git commit -m "refactor: migrate Catalog, FilterForm, ProductDetails to design system"
```

---

### Task 8: Migrate Cart

**Files:**
- Modify: `src/app/features/cart/Cart.tsx`
- Modify: `src/app/features/cart/CartSkeleton.tsx`

- [ ] **Step 1: Migrate Cart.tsx**

Replace the entire contents of `src/app/features/cart/Cart.tsx` with:

```tsx
import { useEffect, useState } from "react";
import PageLayout from "../../layout/PageLayout";
import { MdDelete } from "react-icons/md";
import { useBasket } from "../../../hooks/useBasket";
import { useNavigate } from "react-router-dom";
import { BasketService } from "../../../api/services/BasketService";
import CartSkeleton from "./CartSkeleton";
import { Button, Input, Card, IconButton } from "../../../components/ui";
import QuantitySelector from "../../../components/QuantitySelector";
import OrderSummary from "../../../components/OrderSummary";

const FREE_SHIPPING_THRESHOLD = 500;

function Cart() {
  const [appliedCoupon, setAppliedCoupon] = useState<string | null>(null);
  const [couponError, setCouponError] = useState<string | null>(null);
  const navigate = useNavigate();
  const { basket, refreshBasket } = useBasket();
  const [couponCode, setCouponCode] = useState("");

  useEffect(() => {
    setCouponCode(basket?.coupon || "");
    setAppliedCoupon(basket?.coupon || "");
  }, [basket?.coupon]);

  const applyCoupon = () => {
    BasketService.applyDiscount({ couponCode })
      .then(() => {
        setCouponError(null);
        refreshBasket();
        setAppliedCoupon(couponCode);
      })
      .catch((error) => {
        const message =
          error?.response?.status === 400 && error?.response?.data?.detail
            ? error.response.data.detail
            : "Error occured while applying coupon";
        setCouponError(message);
      });
  };

  const removeCoupon = () => {
    BasketService.removeDiscount()
      .then(() => {
        setCouponError(null);
        setAppliedCoupon(null);
        refreshBasket();
      })
      .catch(() => setCouponError("Error while removing basket"));
  };

  if (!basket) return <CartSkeleton />;

  const couponDiscountRate = basket?.discountRate || 0;
  const discountAmount = (basket.subtotal * couponDiscountRate) / 100;
  const amountLeftForFreeShipping = Math.max(
    0,
    FREE_SHIPPING_THRESHOLD - basket.subtotal,
  );
  const total =
    basket.subtotal + (basket?.shippingAmount || 0) - discountAmount;

  return (
    <PageLayout>
      <div className="max-w-6xl mx-auto p-6">
        <h2 className="text-3xl font-bold mb-8 text-text">Shopping Cart</h2>

        <div className="grid md:grid-cols-3 gap-8">
          {/* Cart Items */}
          <div className="md:col-span-2 space-y-6">
            {!basket?.basketItems?.length ? (
              <Card padding="lg" className="flex flex-col items-center justify-center space-y-6">
                <h2 className="text-2xl font-semibold text-text">
                  Your Cart is empty
                </h2>
                <p className="text-lg text-text-secondary">
                  It seems you've not added any items yet.
                </p>
                <Button
                  variant="primary"
                  onClick={() => navigate("/catalog")}
                >
                  Start Shopping
                </Button>
              </Card>
            ) : (
              basket.basketItems.map((item) => (
                <Card
                  key={item.productId}
                  padding="md"
                  hoverable
                  className="flex items-center gap-6"
                >
                  <img
                    src={item.imageUrl}
                    alt={item.productName}
                    className="w-24 h-24 object-cover rounded-lg flex-shrink-0"
                  />
                  <div className="flex-1">
                    <h3 className="text-xl font-semibold text-text">
                      {item.productName}
                    </h3>
                    <p className="text-sm text-text-secondary mt-1">
                      ${item.price.toFixed(2)} / unit
                    </p>
                    <div className="mt-3 flex items-center gap-3">
                      <QuantitySelector
                        value={item.quantity}
                        onDecrement={() =>
                          BasketService.updateCartItem({
                            quantity: item.quantity - 1,
                            productId: item.productId,
                          }).then(refreshBasket)
                        }
                        onIncrement={() =>
                          BasketService.updateCartItem({
                            quantity: item.quantity + 1,
                            productId: item.productId,
                          }).then(refreshBasket)
                        }
                      />
                      <IconButton
                        icon={<MdDelete size={28} />}
                        variant="danger"
                        aria-label={`Remove ${item.productName} from cart`}
                        onClick={() =>
                          BasketService.removeItemFromCart(
                            item.productId,
                          ).then(refreshBasket)
                        }
                      />
                    </div>
                  </div>
                  <p className="text-xl font-semibold text-text">
                    ${(item.price * item.quantity).toFixed(2)}
                  </p>
                </Card>
              ))
            )}
          </div>

          {/* Order Summary */}
          <Card padding="lg" className="space-y-8">
            <h3 className="text-2xl font-semibold text-text">
              Order Summary
            </h3>

            <OrderSummary
              subtotal={basket.subtotal}
              shipping={basket.shippingAmount || 0}
              discount={discountAmount}
              discountRate={couponDiscountRate}
              total={total}
            />

            {basket.subtotal < FREE_SHIPPING_THRESHOLD &&
              basket.basketItems.length > 0 && (
                <p className="text-xs text-text-muted italic">
                  Add ${amountLeftForFreeShipping.toFixed(2)} more to qualify
                  for free shipping.
                </p>
              )}

            {/* Coupon Section */}
            <div className="space-y-2">
              <label
                htmlFor="coupon"
                className="font-medium text-text block"
              >
                Have a coupon?
              </label>
              <div className="flex flex-col sm:flex-row gap-3">
                <Input
                  id="coupon"
                  type="text"
                  placeholder="Enter coupon code"
                  value={couponCode}
                  onChange={(e) => setCouponCode(e.target.value)}
                  disabled={!!appliedCoupon}
                  aria-invalid={!!couponError}
                  aria-describedby="coupon-error"
                />
                {!appliedCoupon ? (
                  <Button
                    variant="success"
                    onClick={applyCoupon}
                    disabled={
                      !couponCode.trim() || !basket?.basketItems?.length
                    }
                  >
                    Apply
                  </Button>
                ) : (
                  <Button variant="danger" onClick={removeCoupon}>
                    Remove
                  </Button>
                )}
              </div>
              {couponError && (
                <p
                  id="coupon-error"
                  className="text-sm text-error mt-1"
                  role="alert"
                >
                  {couponError}
                </p>
              )}
              {appliedCoupon && !couponError && (
                <p className="text-sm text-success font-medium mt-1">
                  Coupon "<span className="uppercase">{appliedCoupon}</span>"
                  applied: {couponDiscountRate}% off
                </p>
              )}
            </div>

            <Button
              variant="primary"
              size="lg"
              fullWidth
              onClick={() => navigate("/checkout")}
              disabled={!basket?.basketItems?.length}
            >
              Continue To Checkout
            </Button>
          </Card>
        </div>
      </div>
    </PageLayout>
  );
}

export default Cart;
```

- [ ] **Step 2: Migrate CartSkeleton.tsx**

Replace the entire contents of `src/app/features/cart/CartSkeleton.tsx` with:

```tsx
import Skeleton from "../../../components/ui/Skeleton";

export default function CartSkeleton() {
  return (
    <div className="max-w-6xl mx-auto p-6 grid md:grid-cols-3 gap-8">
      <div className="md:col-span-2 space-y-6">
        <Skeleton className="h-32 w-full" />
        <Skeleton className="h-32 w-full" />
        <Skeleton className="h-48 w-full" />
      </div>

      <div className="p-6 border border-border rounded-lg bg-background shadow-md space-y-6">
        <Skeleton className="h-6 w-1/2" />
        <div className="space-y-4">
          <div className="flex justify-between">
            <Skeleton className="h-4 w-24" />
            <Skeleton className="h-4 w-16" />
          </div>
          <div className="flex justify-between">
            <Skeleton className="h-4 w-24" />
            <Skeleton className="h-4 w-16" />
          </div>
          <div className="flex justify-between">
            <Skeleton className="h-4 w-24" />
            <Skeleton className="h-4 w-16" />
          </div>
          <div className="flex justify-between border-t border-border pt-4">
            <Skeleton className="h-5 w-24" />
            <Skeleton className="h-5 w-16" />
          </div>
        </div>
        <Skeleton className="h-12 w-full" />
      </div>
    </div>
  );
}
```

- [ ] **Step 3: Verify build**

Run: `npm run build`
Expected: Build passes with zero errors.

- [ ] **Step 4: Commit**

```bash
git add src/app/features/cart/
git commit -m "refactor: migrate Cart and CartSkeleton to design system"
```

---

### Task 9: Migrate Checkout

**Files:**
- Modify: `src/app/features/checkout/Checkout.tsx`
- Modify: `src/app/features/checkout/CheckoutSkeleton.tsx`

- [ ] **Step 1: Migrate Checkout.tsx**

Replace the entire contents of `src/app/features/checkout/Checkout.tsx` with:

```tsx
import { useEffect, useState } from "react";
import {
  useForm,
  FormProvider,
  useFormContext,
  Controller,
} from "react-hook-form";
import { useBasket } from "../../../hooks/useBasket";
import { useNavigate } from "react-router-dom";
import { useStripe, useElements } from "@stripe/react-stripe-js";
import { PaymentService } from "../../../api/services/PaymentService";
import CheckoutSkeleton from "./CheckoutSkeleton";
import { Button, Input, Modal, Card } from "../../../components/ui";
import AddressCard from "../../../components/AddressCard";
import OrderSummary from "../../../components/OrderSummary";

const initialAddresses = [
  {
    id: "1",
    title: "Home",
    street: "Eryaman Mah",
    city: "Etimesgut",
    state: "Ankara",
    zipCode: "06824",
    country: "Turkey",
  },
  {
    id: "2",
    title: "Work",
    street: "Cyberpark",
    city: "Bilkent",
    state: "Ankara",
    zipCode: "06800",
    country: "Turkey",
  },
];

function AddressSelection() {
  const { control, setValue } = useFormContext();
  const [addresses, setAddresses] = useState(initialAddresses);
  const [showModal, setShowModal] = useState(false);
  const [modalForm, setModalForm] = useState({
    title: "",
    street: "",
    city: "",
    state: "",
    zipCode: "",
    country: "",
  });

  const handleAddAddress = () => {
    setShowModal(true);
    setModalForm({
      title: "",
      street: "",
      city: "",
      state: "",
      zipCode: "",
      country: "",
    });
  };

  const handleModalChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setModalForm({ ...modalForm, [e.target.name]: e.target.value });
  };

  const handleModalSave = () => {
    const newId = (Math.random() * 100000).toFixed(0);
    const newAddr = { ...modalForm, id: newId };
    setAddresses((prev) => [...prev, newAddr]);
    setValue("addressId", newId, { shouldValidate: true });
    setShowModal(false);
  };

  return (
    <div className="space-y-4">
      <h4 className="text-lg font-semibold text-text">
        Select Delivery Address
      </h4>
      <div className="flex flex-wrap gap-4 w-full">
        <Controller
          name="addressId"
          control={control}
          render={({ field }) => (
            <>
              {addresses.map((addr) => (
                <AddressCard
                  key={addr.id}
                  address={addr}
                  selected={field.value === addr.id}
                  onSelect={() => field.onChange(addr.id)}
                />
              ))}
            </>
          )}
        />
        <button
          type="button"
          onClick={handleAddAddress}
          className="flex flex-col items-center justify-center p-4 border-2 border-dashed border-border rounded-lg min-w-[220px] text-primary hover:border-primary hover:bg-primary-light transition"
        >
          <span className="text-3xl mb-1">+</span>
          <span className="font-semibold">Add New Address</span>
        </button>
      </div>

      <Modal
        open={showModal}
        onClose={() => setShowModal(false)}
        title="Add New Address"
      >
        <div className="space-y-3">
          <Input
            name="title"
            value={modalForm.title}
            onChange={handleModalChange}
            placeholder="Title (e.g. Home, Work)"
          />
          <Input
            name="street"
            value={modalForm.street}
            onChange={handleModalChange}
            placeholder="Street"
          />
          <Input
            name="city"
            value={modalForm.city}
            onChange={handleModalChange}
            placeholder="City"
          />
          <Input
            name="state"
            value={modalForm.state}
            onChange={handleModalChange}
            placeholder="State"
          />
          <Input
            name="zipCode"
            value={modalForm.zipCode}
            onChange={handleModalChange}
            placeholder="Zip Code"
          />
          <Input
            name="country"
            value={modalForm.country}
            onChange={handleModalChange}
            placeholder="Country"
          />
        </div>
        <div className="flex justify-end gap-2 mt-6">
          <Button variant="outline" onClick={() => setShowModal(false)}>
            Cancel
          </Button>
          <Button
            variant="primary"
            onClick={handleModalSave}
            disabled={
              !modalForm.title ||
              !modalForm.street ||
              !modalForm.city ||
              !modalForm.state ||
              !modalForm.zipCode ||
              !modalForm.country
            }
          >
            Save Address
          </Button>
        </div>
      </Modal>
    </div>
  );
}

function CheckoutContent() {
  const methods = useForm();
  const stripe = useStripe();
  const elements = useElements();
  const navigate = useNavigate();
  const { basket } = useBasket();

  const subtotal =
    basket?.basketItems?.reduce(
      (sum, item) => sum + item.price * item.quantity,
      0,
    ) || 0;
  const discountAmount = (subtotal * (basket?.discountRate || 0)) / 100;
  const shippingCost = basket?.shippingAmount || 0;
  const total = subtotal + shippingCost - discountAmount;

  const onSubmit = async (formData: any) => {
    if (!stripe || !elements) return;

    const result = await stripe.confirmPayment({
      elements,
      confirmParams: {
        return_url: window.location.origin + "/success",
        payment_method_data: {
          billing_details: { email: formData.email },
        },
      },
      redirect: "if_required",
    });

    if (result.error) {
      alert(result.error.message);
      return;
    }

    const paymentIntentId = result.paymentIntent?.id;
    if (!paymentIntentId) {
      alert("Payment confirmation failed.");
      return;
    }

    navigate("/success");
  };

  return (
    <FormProvider {...methods}>
      <form onSubmit={methods.handleSubmit(onSubmit)}>
        <div className="max-w-6xl mx-auto p-6 grid md:grid-cols-3 gap-8">
          <div className="md:col-span-2 space-y-6">
            <AddressSelection />
          </div>

          <Card padding="lg" className="space-y-6">
            <h3 className="text-2xl font-semibold text-text">
              Order Summary
            </h3>
            <OrderSummary
              subtotal={subtotal}
              shipping={shippingCost}
              discount={discountAmount}
              discountRate={basket?.discountRate || 0}
              total={total}
            />
            <Button
              type="submit"
              variant="primary"
              size="lg"
              fullWidth
              disabled={!basket?.basketItems?.length}
            >
              Complete Order
            </Button>
          </Card>
        </div>
      </form>
    </FormProvider>
  );
}

export default function CheckoutPage() {
  const [clientSecret, setClientSecret] = useState<string | null>(null);

  useEffect(() => {
    async function fetchClientSecret() {
      const res = await PaymentService.createPaymentIntent();
      setClientSecret(res.data.clientSecret);
    }
    fetchClientSecret();
  }, []);

  if (!clientSecret) return <CheckoutSkeleton />;

  return <CheckoutContent />;
}
```

- [ ] **Step 2: Migrate CheckoutSkeleton.tsx**

Replace the entire contents of `src/app/features/checkout/CheckoutSkeleton.tsx` with:

```tsx
import Skeleton from "../../../components/ui/Skeleton";

export default function CheckoutSkeleton() {
  return (
    <div className="max-w-6xl mx-auto p-6 grid md:grid-cols-3 gap-8">
      <div className="md:col-span-2 space-y-6">
        <Skeleton className="h-32 w-full" />
        <Skeleton className="h-32 w-full" />
        <Skeleton className="h-48 w-full" />
      </div>

      <div className="p-6 border border-border rounded-lg bg-background shadow-md space-y-6">
        <Skeleton className="h-6 w-1/2" />
        <div className="space-y-4">
          <div className="flex justify-between">
            <Skeleton className="h-4 w-24" />
            <Skeleton className="h-4 w-16" />
          </div>
          <div className="flex justify-between">
            <Skeleton className="h-4 w-24" />
            <Skeleton className="h-4 w-16" />
          </div>
          <div className="flex justify-between">
            <Skeleton className="h-4 w-24" />
            <Skeleton className="h-4 w-16" />
          </div>
          <div className="flex justify-between border-t border-border pt-4">
            <Skeleton className="h-5 w-24" />
            <Skeleton className="h-5 w-16" />
          </div>
        </div>
        <Skeleton className="h-12 w-full" />
      </div>
    </div>
  );
}
```

- [ ] **Step 3: Verify build**

Run: `npm run build`
Expected: Build passes with zero errors.

- [ ] **Step 4: Commit**

```bash
git add src/app/features/checkout/
git commit -m "refactor: migrate Checkout and CheckoutSkeleton to design system"
```

---

### Task 10: Cleanup and Final Verification

**Files:**
- Delete: `src/App.css` (empty file)
- Verify: all files

- [ ] **Step 1: Delete empty App.css**

```bash
rm src/App.css
```

- [ ] **Step 2: Verify no remaining hardcoded color references**

Search for old color patterns that should be replaced:

```bash
grep -rn "bg-orange\|bg-blue-5\|text-blue-6\|text-orange\|bg-red-5\|text-red-5\|text-gray-7\|bg-gray-1\|bg-gray-2\|bg-gray-3\|text-gray-5\|text-gray-9\|text-gray-8\|text-gray-6\|border-gray" src/ --include="*.tsx" --include="*.ts"
```

Expected: Only `bg-gray-800` in PageLayout footer and Carousel arrows (these are intentionally dark, not theme-semantic).

- [ ] **Step 3: Full build verification**

Run: `npm run build`
Expected: Build passes with zero errors.

- [ ] **Step 4: Lint check**

Run: `npm run lint`
Expected: No ESLint violations (or only pre-existing ones).

- [ ] **Step 5: Visual verification**

Run: `npm run dev`

Check each page manually:
1. **Home** — Carousel uses indigo buttons, product sections use Card + Badge + PriceDisplay
2. **Catalog** — Filter form uses Input + Button, product grid uses new ProductCard
3. **Product Details** — StarRating, PriceDisplay, color/size selectors use `border-primary`
4. **Cart** — QuantitySelector, OrderSummary, coupon Input, Button variants
5. **Checkout** — AddressCard with `border-primary`, Modal, OrderSummary
6. **Header** — ThemeToggle, Button for auth, Badge for cart count
7. **Dark mode** — Click ThemeToggle, verify all pages render correctly

- [ ] **Step 6: Theme change test**

In `src/styles/theme.css`, temporarily change `--color-primary: #4f46e5` to `--color-primary: #dc2626` (red). Confirm the entire app turns red. Revert the change.

- [ ] **Step 7: Final commit**

```bash
git add -A
git commit -m "chore: cleanup empty App.css, complete design system migration"
```
