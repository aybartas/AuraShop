# AuraShop Frontend Design System Refactor

## Context

The AuraShop frontend has no centralized design system. Colors are scattered Tailwind defaults (orange-500 here, blue-600 there), there are no reusable UI components (every button/input/card is inline JSX), and changing the look-and-feel requires touching every file. This refactor introduces a professional, maintainable design system with a centralized theme, reusable components, and dark mode — making the entire UI controllable from a single source of truth.

## Decisions

- **Palette**: Indigo Luxe — primary `#4f46e5`, accent gold `#eab308`
- **Typography**: Inter (Google Fonts)
- **Approach**: CSS custom properties as tokens, wired into Tailwind config
- **Scope**: UI primitives + composite components
- **Dark mode**: Built in from day one via `.dark` class toggle

---

## 1. Design Token Layer

### `src/styles/theme.css`

All design tokens as CSS custom properties. This is the **single source of truth** for the entire app's visual identity.

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
  --font-sans: 'Inter', system-ui, sans-serif;

  /* Border Radius */
  --radius-sm: 0.375rem;
  --radius-md: 0.5rem;
  --radius-lg: 0.75rem;
  --radius-xl: 1rem;
  --radius-full: 9999px;

  /* Shadows */
  --shadow-sm: 0 1px 2px rgba(0, 0, 0, 0.05);
  --shadow-md: 0 4px 6px -1px rgba(0, 0, 0, 0.07), 0 2px 4px -2px rgba(0, 0, 0, 0.05);
  --shadow-lg: 0 10px 15px -3px rgba(0, 0, 0, 0.08), 0 4px 6px -4px rgba(0, 0, 0, 0.04);
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
  --shadow-md: 0 4px 6px -1px rgba(0, 0, 0, 0.3), 0 2px 4px -2px rgba(0, 0, 0, 0.2);
  --shadow-lg: 0 10px 15px -3px rgba(0, 0, 0, 0.35), 0 4px 6px -4px rgba(0, 0, 0, 0.2);
}
```

### `tailwind.config.js` — Theme Extension

Maps CSS variables to Tailwind classes so components use `bg-primary`, `text-accent`, etc.

```js
export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  darkMode: "class",
  theme: {
    extend: {
      fontFamily: {
        sans: ['var(--font-sans)'],
      },
      colors: {
        primary: {
          DEFAULT: 'var(--color-primary)',
          hover: 'var(--color-primary-hover)',
          light: 'var(--color-primary-light)',
          foreground: 'var(--color-primary-foreground)',
        },
        accent: {
          DEFAULT: 'var(--color-accent)',
          hover: 'var(--color-accent-hover)',
          light: 'var(--color-accent-light)',
          foreground: 'var(--color-accent-foreground)',
        },
        background: 'var(--color-background)',
        surface: {
          DEFAULT: 'var(--color-surface)',
          hover: 'var(--color-surface-hover)',
        },
        border: 'var(--color-border)',
        text: {
          DEFAULT: 'var(--color-text)',
          secondary: 'var(--color-text-secondary)',
          muted: 'var(--color-text-muted)',
        },
        success: {
          DEFAULT: 'var(--color-success)',
          light: 'var(--color-success-light)',
        },
        error: {
          DEFAULT: 'var(--color-error)',
          light: 'var(--color-error-light)',
        },
        warning: {
          DEFAULT: 'var(--color-warning)',
          light: 'var(--color-warning-light)',
        },
      },
      borderRadius: {
        sm: 'var(--radius-sm)',
        md: 'var(--radius-md)',
        lg: 'var(--radius-lg)',
        xl: 'var(--radius-xl)',
      },
      boxShadow: {
        sm: 'var(--shadow-sm)',
        md: 'var(--shadow-md)',
        lg: 'var(--shadow-lg)',
      },
    },
  },
  plugins: [],
};
```

### `src/index.css` — Import Order

```css
@import url('https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700&display=swap');
@import './styles/theme.css';
@tailwind base;
@tailwind components;
@tailwind utilities;
```

---

## 2. UI Primitive Components (`src/components/ui/`)

Small, single-responsibility components. Each uses only theme tokens — never hardcoded colors.

### Button

```
Props: variant ('primary' | 'accent' | 'outline' | 'ghost' | 'danger'), size ('sm' | 'md' | 'lg'), loading, disabled, fullWidth, children
```

- `primary`: `bg-primary text-primary-foreground hover:bg-primary-hover`
- `accent`: `bg-accent text-accent-foreground hover:bg-accent-hover`
- `outline`: `border border-border text-text hover:bg-surface`
- `ghost`: `text-text-secondary hover:bg-surface`
- `danger`: `bg-error text-white hover:bg-red-700`
- Loading state: shows spinner SVG, disables click
- Renders `<button>` element, forwards all native button props

### Input

```
Props: label, error, className, all native input props
```

- Renders label + input + error message
- Base: `border border-border rounded-md bg-background text-text focus:ring-2 focus:ring-primary`
- Error state: `border-error focus:ring-error`
- Works with react-hook-form via `register()` or `Controller`

### Card

```
Props: className, children, hoverable, padding ('none' | 'sm' | 'md' | 'lg')
```

- Base: `bg-background rounded-lg shadow-md border border-border`
- Hoverable: adds `hover:shadow-lg transition-shadow`

### Badge

```
Props: variant ('primary' | 'accent' | 'success' | 'error' | 'warning'), size ('sm' | 'md'), children
```

- Small inline labels for cart count, discount %, free shipping
- Each variant maps to its token pair: `bg-{variant}-light text-{variant}`

### Modal

```
Props: open, onClose, title, children, size ('sm' | 'md' | 'lg')
```

- Portal-rendered overlay + dialog
- Overlay: `bg-black/30 backdrop-blur-sm`
- Dialog: `bg-background rounded-xl shadow-lg`
- Close on overlay click + Escape key
- Extracts the pattern currently inline in Checkout.tsx

### Skeleton

```
Props: className, variant ('text' | 'circle' | 'rect'), width, height
```

- `animate-pulse bg-surface-hover rounded`
- Replaces duplicated skeleton patterns in CartSkeleton, CheckoutSkeleton, ProductList

### IconButton

```
Props: icon (React element), variant, size, aria-label, all native button props
```

- Circular button for actions (quantity +/-, delete, carousel arrows)
- `rounded-full p-2 hover:bg-surface transition-colors`

---

## 3. Composite Components (`src/components/`)

Higher-level components that compose UI primitives. These eliminate the most repeated patterns.

### ProductCard (`src/components/ProductCard.tsx`)

Move from `src/app/components/product/ProductCard.tsx`. Uses Card, Badge internally.
- Product image with fallback
- Name (truncated), description (line-clamp-2)
- Price in `text-accent font-bold`
- Click navigates to `/catalog/:id`

### QuantitySelector (`src/components/QuantitySelector.tsx`)

Extract from Cart.tsx. Uses IconButton.
- Props: `value`, `onIncrement`, `onDecrement`, `min`, `max`
- `-` button, value display, `+` button

### PriceDisplay (`src/components/PriceDisplay.tsx`)

- Props: `amount`, `currency`, `size`, `discount?`
- Formats price consistently with `text-accent`
- Optional strikethrough original price + discount price

### StarRating (`src/components/StarRating.tsx`)

Extract from ProductDetails.tsx.
- Props: `rating`, `maxStars`, `size`
- Renders filled/empty/half stars with `text-accent` (gold)
- Replaces hardcoded "★★★★☆" strings

### OrderSummary (`src/components/OrderSummary.tsx`)

Extract duplicated pattern from Cart.tsx and Checkout.tsx.
- Props: `subtotal`, `shipping`, `discount?`, `discountRate?`, `total`
- Line items with labels and values
- Uses Badge for "Free Shipping"

### AddressCard (`src/components/AddressCard.tsx`)

Extract from Checkout.tsx.
- Props: `address`, `selected`, `onSelect`
- Card with selection state (`border-primary` when selected)

### ThemeToggle (`src/components/ThemeToggle.tsx`)

- Toggles `.dark` class on `<html>`
- Persists preference to `localStorage`
- Sun/moon icon swap
- Goes in Header

---

## 4. File Structure (After Refactor)

```
src/
├── styles/
│   └── theme.css                    # Design tokens (CSS variables)
├── components/
│   ├── ui/                          # Primitives
│   │   ├── Button.tsx
│   │   ├── Input.tsx
│   │   ├── Card.tsx
│   │   ├── Badge.tsx
│   │   ├── Modal.tsx
│   │   ├── Skeleton.tsx
│   │   ├── IconButton.tsx
│   │   └── index.ts                 # Barrel export
│   ├── ProductCard.tsx              # Composite
│   ├── QuantitySelector.tsx
│   ├── PriceDisplay.tsx
│   ├── StarRating.tsx
│   ├── OrderSummary.tsx
│   ├── AddressCard.tsx
│   └── ThemeToggle.tsx
├── app/
│   ├── features/                    # Unchanged structure
│   │   ├── home/
│   │   ├── catalog/
│   │   ├── cart/
│   │   └── checkout/
│   └── layout/
│       ├── Header.tsx               # + ThemeToggle added
│       ├── PageLayout.tsx           # Uses theme tokens
│       └── ProtectedRoute.tsx       # Unchanged
├── api/                             # Unchanged
├── contexts/                        # Unchanged
├── hooks/                           # Unchanged
├── types/                           # Unchanged
├── index.css                        # Imports theme.css + Inter font
└── App.tsx                          # Unchanged routing
```

### Key structural changes:
- `src/app/components/product/ProductCard.tsx` → `src/components/ProductCard.tsx` (flattened)
- `src/app/features/home/ProductLİst.tsx` → `src/app/features/home/ProductList.tsx` (fix Turkish İ typo)
- New `src/styles/` directory for theme
- New `src/components/ui/` directory for primitives
- New `src/components/` for composites

---

## 5. Migration Strategy for Feature Components

Each feature file gets updated to use the new components and theme tokens:

### Cart.tsx
- Inline buttons → `<Button variant="primary">`, `<Button variant="danger">`, `<Button variant="outline">`
- Quantity controls → `<QuantitySelector />`
- Order summary section → `<OrderSummary />`
- Badge for free shipping → `<Badge variant="success">`
- All `bg-orange-500`, `bg-red-500`, `bg-green-600` → semantic variants

### Checkout.tsx
- Address cards → `<AddressCard />`
- Address modal → `<Modal>` + `<Input>` components
- Order summary → `<OrderSummary />`
- Buttons → `<Button>`

### ProductDetails.tsx
- Star rating → `<StarRating />`
- Add to Cart → `<Button variant="primary" loading={loading}>`
- Color/size selectors → styled with `border-primary` / `ring-primary` tokens
- Price → `<PriceDisplay />`

### Catalog.tsx + FilterForm.tsx
- Filter inputs → `<Input>`
- Apply button → `<Button variant="primary">`
- Product grid unchanged (already uses ProductCard)

### Header.tsx
- All `text-blue-600` → `text-primary`
- Cart badge → `<Badge variant="accent">`
- Login/Register/Logout buttons → `<Button variant="outline">` / `<Button variant="ghost">`
- Add `<ThemeToggle />` to nav

### Home.tsx + Carousel.tsx + ProductSection.tsx
- `bg-blue-500` buttons → `<Button variant="primary">`
- Discount badges → `<Badge variant="error">`
- Cards → `<Card hoverable>`
- Hardcoded product sections → use `<ProductCard>` with theme tokens

### PageLayout.tsx
- `bg-gray-100` → `bg-surface`
- Footer text → `text-text-secondary`

---

## 6. Verification Plan

1. **Build check**: `npm run build` — TypeScript + Vite must pass with zero errors
2. **Lint check**: `npm run lint` — no ESLint violations
3. **Visual verification**: `npm run dev` and manually check:
   - Home page: carousel, product sections, cards use indigo/gold palette
   - Catalog: filters, product grid, product details all themed
   - Cart: quantity controls, order summary, coupon section all themed
   - Checkout: address selection, payment, order summary all themed
   - Header: navigation, auth buttons, cart badge, theme toggle
4. **Dark mode**: Click theme toggle and verify all pages render correctly in dark
5. **Theme change test**: Modify `--color-primary` in theme.css to a different color, confirm entire app updates
