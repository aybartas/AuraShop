import { useState } from "react";
import PageLayout from "../../layout/PageLayout";
import { MdDelete, MdAdd, MdRemove } from "react-icons/md";

interface BasketItem {
  productId: string;
  productName: string;
  imageUrl: string;
  quantity: number;
  price: number;
  discountedPrice?: number;
}

interface Basket {
  basketItems: BasketItem[];
  discountRate?: number;
  shippingAmount: number;
}

const initialBasket: Basket = {
  shippingAmount: 19.99,
  basketItems: [
    {
      productId: "1",
      productName: "Wireless Headphones",
      imageUrl: "https://picsum.photos/300",
      quantity: 2,
      price: 99.99,
      discountedPrice: 89.99,
    },
    {
      productId: "2",
      productName: "Smart Watch",
      imageUrl: "https://picsum.photos/300",
      quantity: 1,
      price: 199.99,
    },
  ],
};

const VALID_COUPONS: Record<string, number> = {
  SAVE10: 10,
  SAVE20: 20,
  FREESHIP: 0,
};

function Cart() {
  const [basket, setBasket] = useState<Basket>(initialBasket);
  const [couponCode, setCouponCode] = useState("");
  const [appliedCoupon, setAppliedCoupon] = useState<string | null>(null);
  const [couponError, setCouponError] = useState<string | null>(null);

  // Quantity handlers
  const handleQuantityChange = (productId: string, delta: number) => {
    setBasket((prev) => ({
      ...prev,
      basketItems: prev.basketItems
        .map((item) =>
          item.productId === productId
            ? { ...item, quantity: Math.max(1, item.quantity + delta) }
            : item
        )
        .filter((item) => item.quantity > 0),
    }));
  };

  // Remove item
  const handleRemove = (productId: string) => {
    setBasket((prev) => ({
      ...prev,
      basketItems: prev.basketItems.filter(
        (item) => item.productId !== productId
      ),
    }));
  };

  // Calculate totals
  const subtotal = basket.basketItems.reduce(
    (sum, item) => sum + (item.discountedPrice ?? item.price) * item.quantity,
    0
  );

  // Calculate discount based on coupon
  const couponDiscountRate = appliedCoupon ? VALID_COUPONS[appliedCoupon] : 0;
  const discountAmount = (subtotal * couponDiscountRate) / 100;
  const total = subtotal + basket.shippingAmount - discountAmount;

  // Apply coupon handler
  const applyCoupon = () => {
    const code = couponCode.trim().toUpperCase();
    if (!code) {
      setCouponError("Please enter a coupon code.");
      setAppliedCoupon(null);
      return;
    }
    if (!(code in VALID_COUPONS)) {
      setCouponError("Invalid coupon code.");
      setAppliedCoupon(null);
      return;
    }
    setCouponError(null);
    setAppliedCoupon(code);
  };

  // Remove applied coupon
  const removeCoupon = () => {
    setAppliedCoupon(null);
    setCouponCode("");
    setCouponError(null);
  };

  return (
    <PageLayout>
      <div className="max-w-6xl mx-auto p-6">
        <h2 className="text-3xl font-bold mb-8 text-gray-900">Shopping Cart</h2>

        <div className="grid md:grid-cols-3 gap-8">
          {/* Cart Items */}
          <div className="md:col-span-2 space-y-6">
            {basket.basketItems.length === 0 ? (
              <p className="text-gray-500 text-center text-lg">
                Your cart is empty.
              </p>
            ) : (
              basket.basketItems.map((item) => (
                <div
                  key={item.productId}
                  className="flex items-center gap-6 p-5 border rounded-lg bg-white shadow-md hover:shadow-lg transition-shadow"
                >
                  <img
                    src={item.imageUrl}
                    alt={item.productName}
                    className="w-24 h-24 object-cover rounded-lg flex-shrink-0"
                  />
                  <div className="flex-1">
                    <h3 className="text-xl font-semibold text-gray-800">
                      {item.productName}
                    </h3>
                    <p className="text-sm text-gray-500 mt-1">
                      ${(item.discountedPrice ?? item.price).toFixed(2)} / unit
                    </p>

                    <div className="mt-3 flex items-center gap-3">
                      <button
                        aria-label={`Decrease quantity of ${item.productName}`}
                        onClick={() => handleQuantityChange(item.productId, -1)}
                        className="p-2 border rounded-md hover:bg-gray-100 transition"
                      >
                        <MdRemove size={20} />
                      </button>
                      <span className="w-8 text-center text-lg font-medium">
                        {item.quantity}
                      </span>
                      <button
                        aria-label={`Increase quantity of ${item.productName}`}
                        onClick={() => handleQuantityChange(item.productId, 1)}
                        className="p-2 border rounded-md hover:bg-gray-100 transition"
                      >
                        <MdAdd size={20} />
                      </button>
                    </div>
                  </div>

                  <div className="flex flex-col items-end">
                    <p className="text-xl font-semibold text-gray-900">
                      $
                      {(
                        (item.discountedPrice ?? item.price) * item.quantity
                      ).toFixed(2)}
                    </p>
                    <button
                      aria-label={`Remove ${item.productName} from cart`}
                      onClick={() => handleRemove(item.productId)}
                      className="text-red-600 hover:text-red-800 mt-3"
                    >
                      <MdDelete size={24} />
                    </button>
                  </div>
                </div>
              ))
            )}
          </div>

          {/* Order Summary */}
          <div className="p-6 border rounded-lg bg-white shadow-md space-y-8 h-fit">
            <h3 className="text-2xl font-semibold text-gray-900">
              Order Summary
            </h3>

            {/* Summary Items */}
            <div className="space-y-4 text-gray-700 text-base">
              <div className="flex justify-between">
                <span>Subtotal</span>
                <span>${subtotal.toFixed(2)}</span>
              </div>
              <div className="flex justify-between">
                <span>Shipping</span>
                <span>${basket.shippingAmount.toFixed(2)}</span>
              </div>
              {discountAmount > 0 && (
                <div className="flex justify-between text-green-700 font-semibold">
                  <span>Discount</span>
                  <span>- ${discountAmount.toFixed(2)}</span>
                </div>
              )}
              <div className="flex justify-between border-t pt-4 text-lg font-bold text-gray-900">
                <span>Total</span>
                <span>${total.toFixed(2)}</span>
              </div>
            </div>

            {/* Coupon Input */}
            <div className="space-y-2">
              <label
                htmlFor="coupon"
                className="font-medium text-gray-700 block"
              >
                Have a coupon?
              </label>
              <div className="flex flex-col sm:flex-row gap-3">
                <input
                  id="coupon"
                  type="text"
                  className="w-full border border-gray-300 rounded-md px-4 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500 transition"
                  placeholder="Enter coupon code"
                  value={couponCode}
                  onChange={(e) => setCouponCode(e.target.value)}
                  disabled={!!appliedCoupon}
                  aria-invalid={!!couponError}
                  aria-describedby="coupon-error"
                />
                {!appliedCoupon ? (
                  <button
                    onClick={applyCoupon}
                    className="bg-green-600 text-white px-6 py-2 rounded-md hover:bg-green-700 transition disabled:opacity-50"
                    disabled={!couponCode.trim()}
                  >
                    Apply
                  </button>
                ) : (
                  <button
                    onClick={removeCoupon}
                    className="bg-red-500 text-white px-6 py-2 rounded-md hover:bg-red-600 transition"
                  >
                    Remove
                  </button>
                )}
              </div>
              {couponError && (
                <p
                  id="coupon-error"
                  className="text-sm text-red-600 mt-1"
                  role="alert"
                >
                  {couponError}
                </p>
              )}
              {appliedCoupon && !couponError && (
                <p className="text-sm text-green-600 font-medium mt-1">
                  Coupon "<span className="uppercase">{appliedCoupon}</span>"
                  applied: {couponDiscountRate}% off
                </p>
              )}
            </div>

            {/* Checkout Button */}
            <button
              className="w-full bg-orange-500 text-white py-3 rounded-md hover:bg-orange-600 transition disabled:opacity-50 text-lg font-semibold"
              disabled={basket.basketItems.length === 0}
              aria-disabled={basket.basketItems.length === 0}
            >
              Complete Order
            </button>
          </div>
        </div>
      </div>
    </PageLayout>
  );
}

export default Cart;
