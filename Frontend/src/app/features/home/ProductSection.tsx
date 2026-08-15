import Card from "../../../components/ui/Card";
import Badge from "../../../components/ui/Badge";
import PriceDisplay from "../../../components/PriceDisplay";

const bestSellers = [
  { name: "Product 1", description: "Product description goes here.", price: 49.99, image: "https://picsum.photos/300" },
  { name: "Product 2", description: "Product description goes here.", price: 59.99, image: "https://picsum.photos/300" },
  { name: "Product 3", description: "Product description goes here.", price: 39.99, image: "https://picsum.photos/300" },
  { name: "Product 4", description: "Product description goes here.", price: 69.99, image: "https://picsum.photos/300" },
];

const discountedProducts = [
  { name: "Discounted Product 1", description: "Product description goes here.", price: 39.99, originalPrice: 49.99, discount: 20, image: "https://picsum.photos/300" },
  { name: "Discounted Product 2", description: "Product description goes here.", price: 49.99, originalPrice: 58.82, discount: 15, image: "https://picsum.photos/300" },
  { name: "Discounted Product 3", description: "Product description goes here.", price: 29.99, originalPrice: 42.84, discount: 30, image: "https://picsum.photos/300" },
  { name: "Discounted Product 4", description: "Product description goes here.", price: 59.99, originalPrice: 66.66, discount: 10, image: "https://picsum.photos/300" },
];

export default function ProductSection() {
  return (
    <div className="space-y-12">
      <div className="mt-8">
        <h2 className="text-3xl font-bold text-text mb-8">Best Sellers</h2>
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-8">
          {bestSellers.map((product) => (
            <Card key={product.name} hoverable>
              <img
                src={product.image}
                alt={product.name}
                className="w-full h-48 object-cover"
              />
              <div className="p-4">
                <h3 className="text-lg font-semibold text-text">
                  {product.name}
                </h3>
                <p className="text-text-secondary mb-2">
                  {product.description}
                </p>
                <PriceDisplay amount={product.price} />
              </div>
            </Card>
          ))}
        </div>
      </div>

      <div className="py-12">
        <h2 className="text-3xl font-bold text-text mb-8">
          Amazing Discounts
        </h2>
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-8">
          {discountedProducts.map((product) => (
            <Card key={product.name} hoverable className="relative">
              <img
                src={product.image}
                alt={product.name}
                className="w-full h-48 object-cover"
              />
              <Badge
                variant="error"
                size="md"
                className="absolute top-2 right-2"
              >
                {product.discount}% Off
              </Badge>
              <div className="p-4">
                <h3 className="text-lg font-semibold text-text">
                  {product.name}
                </h3>
                <p className="text-text-secondary mb-2">
                  {product.description}
                </p>
                <PriceDisplay
                  amount={product.price}
                  originalAmount={product.originalPrice}
                />
              </div>
            </Card>
          ))}
        </div>
      </div>
    </div>
  );
}
