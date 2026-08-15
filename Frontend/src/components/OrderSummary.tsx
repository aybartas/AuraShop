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
