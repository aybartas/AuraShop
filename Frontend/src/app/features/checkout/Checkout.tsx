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
import { loadStripe } from "@stripe/stripe-js";
import {
  Elements,
  PaymentElement,
  useStripe,
  useElements,
} from "@stripe/react-stripe-js";
import { PaymentService } from "../../../api/services/PaymentService";
import CheckoutSkeleton from "./CheckoutSkeleton";

const stripePromise = loadStripe(import.meta.env.VITE_STRIPE_PK);

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

  // Modal form state
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
      <h4 className="text-lg font-semibold">Select Delivery Address</h4>
      <div className="flex flex-wrap gap-4 w-full">
        <Controller
          name="addressId"
          control={control}
          render={({ field }) => (
            <>
              {addresses.map((addr) => (
                <div
                  key={addr.id}
                  onClick={() => field.onChange(addr.id)}
                  className={`p-4 border rounded-lg cursor-pointer min-w-[220px] transition
                    ${
                      field.value === addr.id
                        ? "border-orange-500 bg-orange-50 shadow"
                        : "hover:border-gray-400 bg-white"
                    }`}
                >
                  <h5 className="font-medium text-orange-600">{addr.title}</h5>
                  <p className="text-sm text-gray-600">{`${addr.street}, ${addr.city}, ${addr.state}, ${addr.zipCode}, ${addr.country}`}</p>
                </div>
              ))}
            </>
          )}
        />
        <button
          type="button"
          onClick={handleAddAddress}
          className="flex flex-col items-center justify-center p-4 border-2 border-dashed rounded-lg min-w-[220px] text-orange-500 hover:border-orange-400 hover:bg-orange-50 transition"
        >
          <span className="text-3xl mb-1">+</span>
          <span className="font-semibold">Add New Address</span>
        </button>
      </div>

      {/* Modal */}
      {showModal && (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-black bg-opacity-30">
          <div className="bg-white rounded-lg shadow-lg p-6 w-full max-w-md relative">
            <button
              className="absolute top-2 right-2 text-gray-400 hover:text-gray-700 text-xl"
              onClick={() => setShowModal(false)}
              type="button"
              aria-label="Close"
            >
              ×
            </button>
            <h4 className="text-lg font-semibold mb-4">Add New Address</h4>
            <div className="space-y-3">
              <input
                name="title"
                value={modalForm.title}
                onChange={handleModalChange}
                className="w-full border px-3 py-2 rounded"
                placeholder="Title (e.g. Home, Work)"
              />
              <input
                name="street"
                value={modalForm.street}
                onChange={handleModalChange}
                className="w-full border px-3 py-2 rounded"
                placeholder="Street"
              />
              <input
                name="city"
                value={modalForm.city}
                onChange={handleModalChange}
                className="w-full border px-3 py-2 rounded"
                placeholder="City"
              />
              <input
                name="state"
                value={modalForm.state}
                onChange={handleModalChange}
                className="w-full border px-3 py-2 rounded"
                placeholder="State"
              />
              <input
                name="zipCode"
                value={modalForm.zipCode}
                onChange={handleModalChange}
                className="w-full border px-3 py-2 rounded"
                placeholder="Zip Code"
              />
              <input
                name="country"
                value={modalForm.country}
                onChange={handleModalChange}
                className="w-full border px-3 py-2 rounded"
                placeholder="Country"
              />
            </div>
            <div className="flex justify-end gap-2 mt-6">
              <button
                type="button"
                className="px-4 py-2 rounded bg-gray-100 hover:bg-gray-200"
                onClick={() => setShowModal(false)}
              >
                Cancel
              </button>
              <button
                type="button"
                className="px-4 py-2 rounded bg-orange-500 text-white hover:bg-orange-600"
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
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}

function PaymentForm() {
  return <PaymentElement />;
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
      0
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

    // 2. Confirm order with backend (do not pass basket items or prices)
    // await PaymentService.createOrder({
    //   paymentIntentId,
    //   shippingOrderAddress: {
    //     addressId: formData.addressId || null,
    //     ...formData.newAddress,
    //   },
    //   saveShippingAddress: formData.saveAddress,
    // });

    navigate("/success");
  };

  return (
    <FormProvider {...methods}>
      <form onSubmit={methods.handleSubmit(onSubmit)}>
        <div className="max-w-6xl mx-auto p-6 grid md:grid-cols-3 gap-8">
          <div className="md:col-span-2 space-y-6">
            <AddressSelection />
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
      const res = await PaymentService.createPaymentIntent();
      setClientSecret(res.data.clientSecret);
    }
    fetchClientSecret();
  }, []);

  if (!clientSecret) return <CheckoutSkeleton />;

  return (
    <Elements stripe={stripePromise} options={{ clientSecret }}>
      <CheckoutContent />
    </Elements>
  );
}
