import { ShoppingCartIcon } from "@heroicons/react/24/outline";
import { Product } from "../../models/Product";
import { Controller, useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import PageLayout from "../../layout/PageLayout";

interface AddToCartForm {
  size: string;
  color: string;
}

function ProductDetails() {
  const navigate = useNavigate();

  const product: Product = {
    id: 1,
    name: "Discounted Product 1",
    description: "Product description goes here.",
    sizes: ["S", "M", "L", "XL", "XXL"],
    colors: ["Red", "Blue", "Yellow"],
    price: "$39.99",
    discount: "20%",
    image: "https://via.placeholder.com/300x300?text=Discounted+Product+1",
    rating: 4.5,
    comments: [
      {
        user: "Alice Lee",
        rating: 5,
        comment: "Amazing product! Love it!",
        date: "2024-12-01",
      },
      {
        user: "Mark Twain",
        rating: 4,
        comment: "Good quality for the price.",
        date: "2024-11-30",
      },
    ],
  };

  const {
    name,
    description,
    price,
    discount,
    image,
    rating,
    comments,
    colors,
    sizes,
  } = product;

  const {
    handleSubmit,
    control,
    formState: { errors },
  } = useForm<AddToCartForm>();

  const onSubmit = (data: AddToCartForm) => {
    navigate("/basket");
    console.log("Product added to cart:", data);
  };
  return (
    <PageLayout>
      <div className="max-w-6xl mx-auto p-6 bg-white rounded-lg shadow-lg">
        <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
          <img
            src={image}
            alt={name}
            className="w-full h-96 object-cover rounded-lg"
          />

          <div className="flex flex-col">
            <h1 className="text-2xl font-bold mb-4">{name}</h1>
            <p className="text-gray-600 mb-4">{description}</p>

            <div className="flex items-center gap-2 mb-4">
              <span className="text-xl font-bold text-red-500">{price}</span>
              {discount && (
                <span className="bg-red-400 text-white px-3 py-1 rounded-full text-sm">
                  {discount} Off
                </span>
              )}
            </div>

            <div className="flex items-center mb-4">
              <span className="text-yellow-400 text-xl">
                {"★".repeat(Math.floor(rating || 5))}
              </span>
              <span className="text-gray-400 text-xl">
                {"☆".repeat(5 - Math.floor(rating || 5))}
              </span>
              <span className="text-sm text-gray-600 ml-2">({rating} / 5)</span>
            </div>

            <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">
                  Color:
                </label>
                <Controller
                  name="color"
                  control={control}
                  render={({ field }) => (
                    <div className="flex gap-2">
                      {colors.map((color) => (
                        <button
                          type="button"
                          onClick={() => field.onChange(color)}
                          className={`px-4 py-2 rounded-lg border ${
                            field.value === color
                              ? "bg-blue-500 text-white"
                              : "bg-gray-100"
                          }`}
                        >
                          {color}
                        </button>
                      ))}
                    </div>
                  )}
                />
                {errors.color && (
                  <p className="text-red-500 text-sm mt-1">
                    {errors.color.message}
                  </p>
                )}
              </div>

              <div>
                <label
                  htmlFor="size"
                  className="block text-sm font-medium text-gray-700 mb-1"
                >
                  Size:
                </label>
                <Controller
                  name="size"
                  control={control}
                  render={({ field }) => (
                    <div className="flex gap-2">
                      {sizes.map((size) => (
                        <button
                          key={size}
                          type="button"
                          onClick={() => field.onChange(size)}
                          className={`px-4 py-2 rounded-lg border ${
                            field.value === size
                              ? "bg-blue-500 text-white"
                              : "bg-gray-100"
                          }`}
                        >
                          {size}
                        </button>
                      ))}
                    </div>
                  )}
                />
                {errors.size && (
                  <p className="text-red-500 text-sm mt-1">
                    {errors.size.message}
                  </p>
                )}
              </div>

              <div className="flex justify-center">
                <button
                  type="submit"
                  className="flex items-center gap-2 bg-orange-500 text-white px-4 py-2 rounded-lg shadow hover:bg-orange-600"
                >
                  <ShoppingCartIcon className="w-5 h-5" /> Add to Cart
                </button>
              </div>
            </form>
          </div>
        </div>

        <div className="mt-6">
          <h2 className="text-xl font-bold mb-4">Comments</h2>
          <ul className="space-y-3">
            {comments?.map((comment, index) => (
              <li
                key={index}
                className="bg-gray-100 p-4 rounded-lg shadow-sm flex items-start gap-4"
              >
                <div className="flex-shrink-0 w-10 h-10 bg-gray-300 rounded-full flex items-center justify-center font-bold text-white">
                  <img
                    src="https://via.placeholder.com/40"
                    className="w-full h-full rounded-full object-cover"
                    alt="User"
                  />
                </div>
                <div>
                  <div className="flex items-center justify-between mb-1">
                    <span className="font-semibold text-gray-700">
                      {comment.user}
                    </span>
                    <span className="text-sm text-gray-500">
                      {new Date(comment.date).toLocaleDateString()}
                    </span>
                  </div>
                  <div className="flex items-center mb-1">
                    <span className="text-yellow-400 text-sm">
                      {"★".repeat(comment.rating)}
                    </span>
                    <span className="text-gray-400 text-sm">
                      {"☆".repeat(5 - comment.rating)}
                    </span>
                  </div>
                  <p className="text-gray-600 text-sm">{comment.comment}</p>
                </div>
              </li>
            ))}
          </ul>
        </div>
      </div>
    </PageLayout>
  );
}
export default ProductDetails;
