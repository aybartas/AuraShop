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
