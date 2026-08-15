import { useNavigate } from "react-router-dom";
import { Product } from "../types/Product";
import Card from "./ui/Card";
import PriceDisplay from "./PriceDisplay";

interface ProductCardProps {
  product: Product;
}

export default function ProductCard({ product }: ProductCardProps) {
  const navigate = useNavigate();

  return (
    <Card hoverable onClick={() => navigate(`/catalog/${product.id}`)}>
      <img
        src={product.images?.[0] || "https://via.placeholder.com/300x200"}
        alt={product.name}
        className="w-full h-48 object-cover"
      />
      <div className="p-4">
        <h3 className="text-lg font-semibold text-text">{product.name}</h3>
        <p className="text-text-secondary text-sm mb-2 line-clamp-2">
          {product.description}
        </p>
        <PriceDisplay amount={product.price} />
      </div>
    </Card>
  );
}
