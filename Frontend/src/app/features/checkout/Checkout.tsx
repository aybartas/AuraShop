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
