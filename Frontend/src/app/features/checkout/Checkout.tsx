// React Checkout Page using Stripe PaymentElement (Custom Checkout Form)
import { useEffect, useState } from "react";
import {
  useForm,
  FormProvider,
  useFormContext,
  Controller,
} from "react-hook-form";
import { useBasket } from "../../../hooks/useBasket";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { loadStripe } from "@stripe/stripe-js";
import {
  Elements,
  PaymentElement,
  useStripe,
  useElements,
} from "@stripe/react-stripe-js";

const stripePromise = loadStripe(import.meta.env.STRIPE_PK);

const savedAddresses = [
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

const shippingOptions = [
  { id: "std", name: "Standard Shipping", price: 0 },
  { id: "exp", name: "Express Shipping", price: 29.99 },
];

function AddressSelection() {
  const { control, watch } = useFormContext();
  const selectedAddress = watch("addressId");
  return (
    <div className="space-y-4">
      <h4 className="text-lg font-semibold">Select Delivery Address</h4>
      <div className="grid gap-3">
        <Controller
          name="addressId"
          control={control}
          render={({ field }) => (
            <>
              {savedAddresses.map((addr) => (
                <div
                  key={addr.id}
                  onClick={() => field.onChange(addr.id)}
                  className={`p-4 border rounded cursor-pointer ${
                    field.value === addr.id
                      ? "border-orange-500 bg-orange-50"
                      : "hover:border-gray-400"
                  }`}
                >
                  <h5 className="font-medium">{addr.title}</h5>
                  <p className="text-sm text-gray-600">{`${addr.street}, ${addr.city}, ${addr.state}, ${addr.zipCode}, ${addr.country}`}</p>
                </div>
              ))}
            </>
          )}
        />
        <div className="mt-4 space-y-2">
          <label className="text-sm">Add New Address</label>
          <Controller
            name="newAddress.street"
            control={control}
            render={({ field }) => (
              <input
                {...field}
                className="w-full border px-3 py-2 rounded"
                placeholder="Street"
              />
            )}
          />
          <Controller
            name="newAddress.city"
            control={control}
            render={({ field }) => (
              <input
                {...field}
                className="w-full border px-3 py-2 rounded"
                placeholder="City"
              />
            )}
          />
          <Controller
            name="newAddress.state"
            control={control}
            render={({ field }) => (
              <input
                {...field}
                className="w-full border px-3 py-2 rounded"
                placeholder="State"
              />
            )}
          />
          <Controller
            name="newAddress.zipCode"
            control={control}
            render={({ field }) => (
              <input
                {...field}
                className="w-full border px-3 py-2 rounded"
                placeholder="Zip Code"
              />
            )}
          />
          <Controller
            name="newAddress.country"
            control={control}
            render={({ field }) => (
              <input
                {...field}
                className="w-full border px-3 py-2 rounded"
                placeholder="Country"
              />
            )}
          />
          <Controller
            name="saveAddress"
            control={control}
            render={({ field }) => (
              <label className="inline-flex items-center mt-2">
                <input
                  type="checkbox"
                  {...field}
                  checked={field.value || false}
                  className="mr-2"
                />{" "}
                Save this address
              </label>
            )}
          />
        </div>
      </div>
    </div>
  );
}

function ShippingOptions() {
  const { control } = useFormContext();
  return (
    <div className="space-y-4 mt-8">
      <h4 className="text-lg font-semibold">Shipping Method</h4>
      <Controller
        name="shippingOptionId"
        control={control}
        render={({ field }) => (
          <>
            {shippingOptions.map((opt) => (
              <div
                key={opt.id}
                onClick={() => field.onChange(opt.id)}
                className={`p-4 border rounded cursor-pointer flex justify-between ${
                  field.value === opt.id
                    ? "border-orange-500 bg-orange-50"
                    : "hover:border-gray-400"
                }`}
              >
                <span>{opt.name}</span>
                <span>
                  {opt.price === 0 ? "Free" : `$${opt.price.toFixed(2)}`}
                </span>
              </div>
            ))}
          </>
        )}
      />
    </div>
  );
}

function PaymentForm() {
  const { control } = useFormContext();
  return (
    <div className="space-y-4 mt-8">
      <h4 className="text-lg font-semibold">Email & Payment</h4>
      <Controller
        name="email"
        control={control}
        rules={{ required: "Email is required" }}
        render={({ field }) => (
          <input
            {...field}
            type="email"
            className="w-full border px-3 py-2 rounded"
            placeholder="you@example.com"
          />
        )}
      />
      <div className="border px-3 py-4 rounded bg-white shadow-sm">
        <PaymentElement />
      </div>
    </div>
  );
}

function CheckoutContent({ clientSecret }: { clientSecret: string }) {
  const methods = useForm();
  const stripe = useStripe();
  const elements = useElements();
  const navigate = useNavigate();
  const { basket } = useBasket();
  const subtotal =
    basket?.basketItems?.reduce(
      (sum, item) => sum + item.price * item.quantity,
      0
    ) || 0;
  const discountAmount = (subtotal * (basket?.discountRate || 0)) / 100;
  const shippingCost =
    shippingOptions.find((opt) => opt.id === methods.watch("shippingOptionId"))
      ?.price || 0;
  const total = subtotal + shippingCost - discountAmount;

  const onSubmit = async (formData: any) => {
    if (!stripe || !elements) return;

    const result = await stripe.confirmPayment({
      elements,
      confirmParams: {
        return_url: `${window.location.origin}/order-success`,
        payment_method_data: {
          billing_details: {
            email: formData.email,
          },
        },
      },
      redirect: "if_required",
    });

    if (result.error) return alert(result.error.message);

    await axios.post("/api/orders", {
      couponCode: basket?.coupon || null,
      shippingOrderAddress: {
        addressId: formData.addressId || null,
        ...formData.newAddress,
      },
      saveShippingAddress: formData.saveAddress,
      items: basket?.basketItems.map((item) => ({
        productId: item.productId,
        productName: item.productName,
        unitPrice: item.price,
        quantity: item.quantity,
      })),
    });

    navigate("/order-success");
  };

  return (
    <FormProvider {...methods}>
      <form onSubmit={methods.handleSubmit(onSubmit)}>
        <div className="max-w-6xl mx-auto p-6 grid md:grid-cols-3 gap-8">
          <div className="md:col-span-2 space-y-6">
            <AddressSelection />
            <ShippingOptions />
            <PaymentForm />
          </div>

          <div className="p-6 border rounded-lg bg-white shadow-md space-y-6">
            <h3 className="text-2xl font-semibold text-gray-900">
              Order Summary
            </h3>
            <div className="space-y-2 text-gray-700 text-base">
              <div className="flex justify-between font-semibold">
                <span>Subtotal</span>
                <span>${subtotal.toFixed(2)}</span>
              </div>
              {discountAmount > 0 && (
                <div className="flex justify-between text-green-700 font-semibold">
                  <span>Discount</span>
                  <span>- ${discountAmount.toFixed(2)}</span>
                </div>
              )}
              <div className="flex justify-between font-semibold">
                <span>Shipping</span>
                <span>
                  {shippingCost === 0 ? "Free" : `$${shippingCost.toFixed(2)}`}
                </span>
              </div>
              <div className="flex justify-between border-t pt-4 text-lg font-bold text-gray-900">
                <span>Total</span>
                <span>${total.toFixed(2)}</span>
              </div>
            </div>
            <button
              type="submit"
              className="w-full bg-orange-500 text-white py-3 rounded-md hover:bg-orange-600 transition disabled:opacity-50 text-lg font-semibold"
            >
              Complete Order
            </button>
          </div>
        </div>
      </form>
    </FormProvider>
  );
}

export default function CheckoutPage() {
  const [clientSecret, setClientSecret] = useState<string | null>(null);

  useEffect(() => {
    async function fetchClientSecret() {
      const res = await axios.post("/api/payment/create-payment-intent");
      setClientSecret(res.data.clientSecret);
    }
    fetchClientSecret();
  }, []);

  if (!clientSecret)
    return <div className="p-10 text-center">Loading payment...</div>;

  return (
    <Elements stripe={stripePromise} options={{ clientSecret }}>
      <CheckoutContent clientSecret={clientSecret} />
    </Elements>
  );
}
