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
