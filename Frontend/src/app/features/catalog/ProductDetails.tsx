import { Controller, useForm } from "react-hook-form";
import PageLayout from "../../layout/PageLayout";
import { Product } from "../../../types/Product";
import { useEffect, useState } from "react";
import { CatalogService } from "../../../api/services/CatalogService";
import { useParams } from "react-router-dom";
import { useAuth } from "../../../contexts/AuthContext";
import { BasketService } from "../../../api/services/BasketService";
import { useBasket } from "../../../hooks/useBasket";
import Button from "../../../components/ui/Button";
import Card from "../../../components/ui/Card";
import StarRating from "../../../components/StarRating";
import PriceDisplay from "../../../components/PriceDisplay";

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
  const { keycloak } = useAuth();
  const [product, setProduct] = useState<Product | null>(null);
  const [comments] = useState<ProductComment[]>([
    {
      user: "Jane Doe",
      date: new Date().toISOString(),
      rating: 4,
      comment: "Great product!",
    },
  ]);
  const [loading, setLoading] = useState(false);
  const { refreshBasket } = useBasket();
  const { id } = useParams();

  useEffect(() => {
    if (id) {
      CatalogService.getProduct(id)
        .then((res) => setProduct(res.data))
        .catch((error) => console.error("Failed to fetch product:", error));
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

    if (!keycloak?.authenticated) {
      keycloak?.login();
      return;
    }

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
      .then(() => refreshBasket())
      .catch((error) => console.error("Failed to add item to cart:", error))
      .finally(() => setLoading(false));
  };

  if (!product)
    return (
      <PageLayout>
        <p className="text-text-secondary">Loading...</p>
      </PageLayout>
    );

  const { name, description, images, colors, sizes } = product;
  const image = images?.[0];
  const rating = 4.5;

  return (
    <PageLayout>
      <Card padding="lg" className="max-w-6xl mx-auto">
        <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
          <img
            src={image}
            alt={name}
            className="w-full h-96 object-cover rounded-lg"
          />

          <div className="flex flex-col">
            <h1 className="text-2xl font-bold text-text mb-2">{name}</h1>
            <PriceDisplay amount={product.price} size="lg" />
            <p className="text-text-secondary my-4">{description}</p>

            <div className="mb-4">
              <StarRating rating={rating} showValue />
            </div>

            <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
              {colors && colors.length > 0 && (
                <div>
                  <label className="block text-sm font-medium text-text mb-1">
                     Color: {formData.color}
                  </label>
                  <Controller
                    name="color"
                    control={control}
                    rules={{
                      required:
                        colors.length > 0 ? "Please select a color." : false,
                    }}
                    render={({ field }) => (
                      <div className="flex gap-2 flex-wrap">
                        {colors.map((colorObj) => (
                          <button
                            key={colorObj.hexCode}
                            type="button"
                            onClick={() => field.onChange(colorObj.name)}
                            className={`flex items-center px-4 py-2 min-w-[64px] rounded-md border text-sm font-medium transition-all duration-300 ${
                              field.value === colorObj.name
                                ? "border-primary text-primary shadow-md ring-1 ring-primary-light"
                                : "border-border bg-surface hover:border-text-muted"
                            }`}
                          >
                            <div
                              className="w-4 h-4 mr-2 rounded-full"
                              style={{ backgroundColor: colorObj.hexCode }}
                            />
                            {colorObj.name}
                          </button>
                        ))}
                      </div>
                    )}
                  />
                  {errors.color && (
                    <p className="text-error text-sm mt-1">
                      {errors.color.message}
                    </p>
                  )}
                </div>
              )}

              {sizes && sizes.length > 0 && (
                <div>
                  <label className="block text-sm font-medium text-text mb-1">
                    Size: {formData.size}
                  </label>
                  <Controller
                    name="size"
                    control={control}
                    rules={{
                      required:
                        sizes.length > 0 ? "Please select a size." : false,
                    }}
                    render={({ field }) => (
                      <div className="flex gap-2 flex-wrap">
                        {sizes.map((size) => (
                          <button
                            key={size}
                            type="button"
                            onClick={() => field.onChange(size)}
                            className={`px-4 py-2 min-w-[64px] rounded-md border text-sm font-medium transition-all duration-300 ${
                              field.value === size
                                ? "border-primary text-primary shadow-md ring-1 ring-primary-light"
                                : "border-border bg-surface hover:border-text-muted"
                            }`}
                          >
                            {size}
                          </button>
                        ))}
                      </div>
                    )}
                  />
                  {errors.size && (
                    <p className="text-error text-sm mt-1">
                      {errors.size.message}
                    </p>
                  )}
                </div>
              )}

              <div className="w-full justify-center flex pt-4">
                <Button
                  type="submit"
                  variant="primary"
                  size="lg"
                  loading={loading}
                  className="w-full sm:w-auto"
                >
                  {loading ? "Adding..." : "Add to Cart"}
                </Button>
              </div>
            </form>
          </div>
        </div>

        {/* Comments Section */}
        <div className="mt-6">
          <h2 className="text-xl font-bold text-text mb-4">Comments</h2>
          <ul className="space-y-3">
            {comments.map((comment, index) => (
              <li
                key={index}
                className="bg-surface p-4 rounded-lg shadow-sm flex items-start gap-4"
              >
                <div className="flex-shrink-0 w-10 h-10 bg-surface-hover rounded-full overflow-hidden">
                  <img
                    src="https://picsum.photos/300"
                    className="w-full h-full object-cover"
                    alt="User"
                  />
                </div>
                <div>
                  <div className="flex gap-2 items-center justify-between mb-1">
                    <span className="font-semibold text-text">
                      {comment.user}
                    </span>
                    <span className="text-sm text-text-muted">
                      {new Date(comment.date).toLocaleDateString()}
                    </span>
                  </div>
                  <StarRating rating={comment.rating} size="sm" />
                  <p className="text-text-secondary text-sm mt-1">
                    {comment.comment}
                  </p>
                </div>
              </li>
            ))}
          </ul>
        </div>
      </Card>
    </PageLayout>
  );
}

export default ProductDetails;
