import { ShoppingCartIcon } from "@heroicons/react/24/outline";
import { Controller, useForm } from "react-hook-form";
import PageLayout from "../../layout/PageLayout";
import { Product } from "../../../types/Product";
import { useEffect, useState } from "react";
import { CatalogService } from "../../../api/services/CatalogService";
import { useParams } from "react-router-dom";
import { BasketService } from "../../../api/services/BasketService";
import { useBasket } from "../../../hooks/useBasket";

interface AddToCartForm {
  size: string;
  color: string;
}

interface ProductComment {
  user: string;
  date: string;
  rating: number;
  comment: string;
}

function ProductDetails() {
  const [product, setProduct] = useState<Product | null>(null);
  const [comments, setComments] = useState<ProductComment[]>([
    {
      user: "Jane Doe",
      date: new Date().toISOString(),
      rating: 4,
      comment: "Great product!",
    },
  ]);
  const [loading, setLoading] = useState<boolean>(false);
  const { refreshBasket } = useBasket();

  const { id } = useParams();

  useEffect(() => {
    if (id) {
      CatalogService.getProduct(id)
        .then((res) => {
          setProduct(res.data);
        })
        .catch((error) => {
          console.error("Failed to fetch product:", error);
        });
    }
  }, [id]);

  const {
    handleSubmit,
    control,
    formState: { errors },
    watch,
  } = useForm<AddToCartForm>({
    defaultValues: { size: "", color: "" },
    mode: "onChange",
  });

  const formData = watch();

  const onSubmit = async (formData: AddToCartForm) => {
    if (!product) return;

    const cartItem = {
      productId: product.id,
      productName: product.name,
      price: product.price,
      quantity: 1,
      imageUrl: product.images?.[0],
      size: formData.size,
      color: formData.color,
    };

    setLoading(true);

    BasketService.addItemToCart(cartItem)
      .then(() => {
        refreshBasket();
      })
      .catch((error) => {
        console.error("Failed to add item to cart:", error);
      })
      .finally(() => {
        setLoading(false);
      });
  };
  if (!product)
    return (
      <PageLayout>
        <p>Loading...</p>
      </PageLayout>
    );

  const { name, description, images, colors, sizes } = product;
  const image = images?.[0];
  const rating = 4.5; // You can derive or fetch this

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

            <div className="flex items-center mb-4">
              <span className="text-yellow-400 text-xl">
                {"★".repeat(Math.floor(rating))}
              </span>
              <span className="text-gray-400 text-xl">
                {"☆".repeat(5 - Math.floor(rating))}
              </span>
              <span className="text-sm text-gray-600 ml-2">({rating} / 5)</span>
            </div>

            <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
              {product.colors && product.colors.length > 0 && (
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Color: {formData.color}
                  </label>
                  <Controller
                    name="color"
                    control={control}
                    rules={{
                      required:
                        product?.colors && product.colors.length > 0
                          ? "Please select a color."
                          : false,
                    }}
                    render={({ field }) => (
                      <div className="flex gap-2 flex-wrap">
                        {colors?.map((colorObj) => (
                          <button
                            key={colorObj.hexCode}
                            type="button"
                            onClick={() => field.onChange(colorObj.name)}
                            className={`flex items-center px-4 py-2 min-w-[64px] rounded-md border text-sm font-medium transition-all duration-300 
                          ${
                            field.value === colorObj.name
                              ? "border-orange-500 text-orange-600 shadow-md ring-1 ring-orange-200"
                              : "border-gray-300 bg-gray-50 hover:border-gray-400"
                          }`}
                          >
                            <div
                              className="w-4 h-4 mr-2 rounded-full"
                              style={{ backgroundColor: colorObj.hexCode }}
                            ></div>
                            {colorObj.name}
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
              )}

              {product.sizes && product.sizes.length > 0 && (
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Size: {formData.size}
                  </label>
                  <Controller
                    name="size"
                    control={control}
                    rules={{
                      required:
                        product?.sizes && product.sizes.length > 0
                          ? "Please select a size."
                          : false,
                    }}
                    render={({ field }) => (
                      <div className="flex gap-2 flex-wrap">
                        {sizes?.map((size) => (
                          <button
                            key={size}
                            type="button"
                            onClick={() => field.onChange(size)}
                            className={`px-4 py-2 min-w-[64px] rounded-md border text-sm font-medium transition-all duration-300 
                          ${
                            field.value === size
                              ? "border-orange-500 text-orange-600 shadow-md ring-1 ring-orange-200"
                              : "border-gray-300 bg-gray-50 hover:border-gray-400"
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
              )}

              <div className="w-full justify-center flex pt-4">
                <button
                  type="submit"
                  disabled={loading}
                  className={`w-full sm:w-auto flex items-center justify-center gap-3 px-6 py-3 text-base font-semibold rounded-lg shadow-md transition duration-300
                    ${
                      loading
                        ? "bg-orange-300 cursor-not-allowed"
                        : "bg-orange-500 hover:bg-orange-600 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-orange-400 text-white"
                    }
                  `}
                >
                  {loading ? (
                    <svg
                      className="w-6 h-6 text-white animate-spin"
                      xmlns="http://www.w3.org/2000/svg"
                      fill="none"
                      viewBox="0 0 24 24"
                    >
                      <circle
                        className="opacity-25"
                        cx="12"
                        cy="12"
                        r="10"
                        stroke="currentColor"
                        strokeWidth="4"
                      />
                      <path
                        className="opacity-75"
                        fill="currentColor"
                        d="M4 12a8 8 0 018-8v4a4 4 0 00-4 4H4z"
                      />
                    </svg>
                  ) : (
                    <ShoppingCartIcon className="w-6 h-6 text-white" />
                  )}
                  {loading ? "Adding..." : "Add to Cart"}
                </button>
              </div>
            </form>
          </div>
        </div>

        {/* COMMENTS SECTION */}
        <div className="mt-6">
          <h2 className="text-xl font-bold mb-4">Comments</h2>
          <ul className="space-y-3">
            {comments.map((comment, index) => (
              <li
                key={index}
                className="bg-gray-100 p-4 rounded-lg shadow-sm flex items-start gap-4"
              >
                <div className="flex-shrink-0 w-10 h-10 bg-gray-300 rounded-full overflow-hidden">
                  <img
                    src="https://picsum.photos/300"
                    className="w-full h-full object-cover"
                    alt="User"
                  />
                </div>
                <div>
                  <div className="flex gap-2 items-center justify-between mb-1">
                    <span className="font-semibold text-gray-700">
                      {comment.user}
                    </span>
                    <span className="text-sm text-gray-500">
                      {new Date(comment.date).toLocaleDateString()}
                    </span>
                  </div>
                  <div className="flex items-center mb-1">
                    <span className="text-yellow-400">
                      {"★".repeat(comment.rating)}
                    </span>
                    <span className="text-gray-400">
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
