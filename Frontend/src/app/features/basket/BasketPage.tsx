import { useForm, useFieldArray, Controller } from "react-hook-form";
import { PlusIcon, MinusIcon, TrashIcon } from "@heroicons/react/24/solid";
import PageLayout from "../../layout/PageLayout";

export interface BasketForm {
  userId: string;
  discountCode?: string;
  discountRate?: number;
  totalQuantity: number;
  totalPrice?: number;
  basketItems: BasketItemForm[];
}

export interface BasketItemForm {
  productId: string;
  productName: string;
  productImage: string;
  color: string;
  size: string;
  quantity: number;
  price: number;
}

const sampleBasket: BasketForm = {
  userId: "123",
  discountCode: "DISCOUNT60",
  discountRate: 0.1,
  totalQuantity: 150,
  basketItems: [
    {
      productId: "1",
      productImage: "https://via.placeholder.com/300x300?text=Product",
      color: "Red",
      size: "S",
      productName: "Product A",
      quantity: 2,
      price: 50,
    },
    {
      productId: "2",
      productImage: "https://via.placeholder.com/300x300?text=Product",
      color: "Blue",
      size: "M",
      productName: "Product B",
      quantity: 1,
      price: 100,
    },
  ],
};

function BasketPage() {
  const { control, handleSubmit, watch } = useForm<BasketForm>({
    defaultValues: sampleBasket,
  });

  const { fields, remove, update } = useFieldArray({
    control,
    name: "basketItems",
  });

  const basketData = watch();

  const calculateSummary = () => {
    const subtotal =
      basketData.basketItems?.reduce(
        (sum, item) => sum + (item.quantity || 0) * (item.price || 0),
        0
      ) || 0;
    const discount = subtotal * (basketData.discountRate || 0);
    const freight = 20;
    const total = subtotal - discount + freight;
    return { subtotal, discount, freight, total };
  };

  const summary = calculateSummary();

  const onSubmit = (data: BasketForm) => {
    console.log("Checkout Data", data);
  };

  return (
    <PageLayout>
      <h1 className="text-2xl font-bold mb-6">Shopping Cart</h1>
      <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
        <div className="flex flex-row gap-4">
          <div className="basis-3/4">
            <div className="bg-white p-6 rounded-lg shadow-md">
              {fields.map((field, index) => (
                <div
                  key={field.id}
                  className="flex items-center justify-between border-b pb-4 mb-4 gap-8"
                >
                  <div>
                    <img
                      src={field.productImage}
                      alt={field.productName}
                      className="h-24 w-24 object-contain"
                    />
                  </div>

                  <div className="flex-1 px-4">
                    <h2 className="text-lg font-semibold">
                      {field.productName}
                    </h2>
                    <p className="text-sm text-gray-500">
                      Color: {field.color}, Size: {field.size}
                    </p>
                    <div className="flex items-center space-x-2 mt-4">
                      <div className="flex items-center space-x-2 bg-gray-100"></div>
                      <button
                        type="button"
                        onClick={() =>
                          update(index, {
                            ...field,
                            quantity: Math.max((field.quantity || 1) - 1, 1),
                          })
                        }
                        className="text-gray-500 hover:text-black"
                      >
                        <MinusIcon className="h-5 w-5" />
                      </button>

                      <Controller
                        name={`basketItems.${index}.quantity`}
                        control={control}
                        defaultValue={1}
                        render={({ field, fieldState }) => (
                          <div>
                            <input
                              {...field}
                              type="number"
                              min="1"
                              className="p-2 border border-gray-300 rounded-md w-16"
                              onChange={(e) =>
                                field.onChange(Number(e.target.value))
                              }
                            />
                            {fieldState.error && (
                              <span className="text-red-500 text-sm">
                                {fieldState.error?.message}
                              </span>
                            )}
                          </div>
                        )}
                        rules={{
                          required: "Quantity is required",
                          min: {
                            value: 1,
                            message: "Quantity must be at least 1",
                          },
                        }}
                      />

                      <button
                        type="button"
                        onClick={() =>
                          update(index, {
                            ...field,
                            quantity: (field.quantity || 1) + 1,
                          })
                        }
                        className="text-gray-500 hover:text-black"
                      >
                        <PlusIcon className="h-5 w-5" />
                      </button>
                    </div>
                  </div>

                  <div className="text-lg font-semibold">
                    ${(field.price * field.quantity).toFixed(2)}
                  </div>
                  <button
                    type="button"
                    onClick={() => remove(index)}
                    className="text-orange-500 hover:text-orange-700"
                  >
                    <TrashIcon className="h-5 w-5" />
                  </button>
                </div>
              ))}
            </div>
          </div>
          <div className="basis-1/4">
            <div className="bg-white p-6 rounded-lg shadow-md">
              <h2 className="text-xl font-semibold mb-4">Cart Summary</h2>
              <div className="space-y-2">
                <div className="flex justify-between">
                  <span>Subtotal:</span>
                  <span>${summary.subtotal.toFixed(2)}</span>
                </div>
                <div className="flex justify-between">
                  <span>Discount:</span>
                  <span>-${summary.discount.toFixed(2)}</span>
                </div>
                <div className="flex justify-between">
                  <span>Freight:</span>
                  <span>${summary.freight.toFixed(2)}</span>
                </div>
                <div className="flex justify-between font-bold">
                  <span>Total:</span>
                  <span>${summary.total.toFixed(2)}</span>
                </div>
              </div>
              <div className="flex justify-center px-8 mt-16">
                <button
                  type="submit"
                  className="w-full bg-orange-500 text-white py-2 px-4 rounded hover:bg-orange-600"
                >
                  Complete Shopping
                </button>
              </div>
            </div>
          </div>
        </div>
      </form>
    </PageLayout>
  );
}

export default BasketPage;
