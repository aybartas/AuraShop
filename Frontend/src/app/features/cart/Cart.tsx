import { useEffect, useState } from "react";
import PageLayout from "../../layout/PageLayout";
import { MdDelete, MdAdd, MdRemove } from "react-icons/md";
import { useBasket } from "../../../hooks/useBasket";
import { useNavigate } from "react-router-dom";
import { BasketService } from "../../../api/services/BasketService";

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

  // Calculate subtotal
  const subtotal =
    basket?.basketItems?.reduce(
      (sum, item) => sum + item.price * item.quantity,
      0
    ) || 0;

  // Coupon discount
  const couponDiscountRate = basket?.discountRate || 0;
  const discountAmount = (subtotal * couponDiscountRate) / 100;

  // Shipping info
  const amountLeftForFreeShipping = Math.max(
    0,
    FREE_SHIPPING_THRESHOLD - subtotal
  );

  // Calculate total
  const total = subtotal + (basket?.shippingAmount || 0) - discountAmount;

  // Apply coupon handler
  const applyCoupon = () => {
    BasketService.applyDiscount({ couponCode: couponCode })
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

  // Remove coupon handler
  const removeCoupon = () => {
    BasketService.removeDiscount()
      .then(() => {
        setCouponError(null);
        setAppliedCoupon(null);
        refreshBasket();
      })
      .catch(() => {
        setCouponError("Error while removing basket");
      });
  };

  return (
    <PageLayout>
      <div className="max-w-6xl mx-auto p-6">
        <h2 className="text-3xl font-bold mb-8 text-gray-900">Shopping Cart</h2>

        <div className="grid md:grid-cols-3 gap-8">
          {/* Cart Items */}
          <div className="md:col-span-2 space-y-6">
            {!basket?.basketItems?.length ? (
              <div className="p-6 border bg-white shadow-md flex flex-col items-center justify-center space-y-6 rounded-lg">
                <h2 className="text-2xl font-semibold text-gray-800">
                  Your Cart is empty
                </h2>
                <p className="text-lg text-gray-600">
                  It seems you've not added any items yet.
                </p>

                <button
                  onClick={() => navigate(`/catalog`)}
                  className="bg-orange-500 hover:bg-orange-600 text-white py-2 px-4 rounded-lg text-sm"
                >
                  Start Shopping
                </button>
              </div>
            ) : (
              basket?.basketItems?.map((item) => (
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
                      <div className="flex items-center gap-2">
                        <button
                          aria-label={`Decrease quantity of ${item.productName}`}
                          onClick={() =>
                            BasketService.updateCartItem({
                              quantity: item.quantity - 1,
                              productId: item.productId,
                            }).then(refreshBasket)
                          }
                          disabled={item.quantity <= 1}
                          className="p-2 border rounded-md hover:bg-gray-100 transition"
                        >
                          <MdRemove size={20} />
                        </button>
                        <span className="w-8 text-center text-lg font-medium">
                          {item.quantity}
                        </span>
                        <button
                          aria-label={`Increase quantity of ${item.productName}`}
                          onClick={() =>
                            BasketService.updateCartItem({
                              quantity: item.quantity + 1,
                              productId: item.productId,
                            }).then(refreshBasket)
                          }
                          className="p-2 border rounded-md hover:bg-gray-100 transition"
                        >
                          <MdAdd size={20} />
                        </button>
                      </div>
                      <button
                        aria-label={`Remove ${item.productName} from cart`}
                        onClick={() =>
                          BasketService.removeItemFromCart(item.productId).then(
                            refreshBasket
                          )
                        }
                        className="flex items-center text-red-600 hover:text-red-800 "
                      >
                        <MdDelete size={28} />
                      </button>
                    </div>
                  </div>
                  <p className="text-xl font-semibold text-gray-800">
                    ${((item.price ?? item.price) * item.quantity).toFixed(2)}
                  </p>
                </div>
              ))
            )}
          </div>

          {/* Order Summary */}
          <div className="p-6 border rounded-lg bg-white shadow-md space-y-8">
            <h3 className="text-2xl font-semibold text-gray-900">
              Order Summary
            </h3>

            {/* Summary Items */}
            <div className="space-y-4 text-gray-700 text-base">
              <div className="flex justify-between font-semibold">
                <span>Subtotal</span>
                <span>${subtotal.toFixed(2)}</span>
              </div>

              <div className="flex flex-col gap-1">
                <div className="flex justify-between font-semibold">
                  <span>Shipping</span>
                  {basket?.basketItems?.length &&
                  basket.shippingAmount === 0 ? (
                    <span className="bg-green-100 text-green-800 px-2 py-1 rounded-full text-sm font-semibold">
                      Free Shipping
                    </span>
                  ) : (
                    <span>${basket?.shippingAmount?.toFixed(2)}</span>
                  )}
                </div>
                {subtotal < FREE_SHIPPING_THRESHOLD && (
                  <p className="text-xs text-gray-500 italic">
                    Add ${amountLeftForFreeShipping.toFixed(2)} more to qualify
                    for free shipping.
                  </p>
                )}
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

            {/* Coupon Section */}
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
                  className="w-full border border-gray-300 rounded-md px-4 py-2 focus:outline-none focus:ring-2 transition"
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
                    disabled={
                      !couponCode.trim() || !basket?.basketItems?.length
                    }
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

            <button
              onClick={() => navigate("/checkout")}
              className="w-full bg-orange-500 text-white py-3 rounded-md hover:bg-orange-600 transition disabled:opacity-50 text-lg font-semibold"
              disabled={!basket?.basketItems?.length}
            >
              Continue To Checkout
            </button>
          </div>
        </div>
      </div>
    </PageLayout>
  );
}

export default Cart;
