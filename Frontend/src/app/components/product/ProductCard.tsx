import { useNavigate } from "react-router-dom";
import { Product } from "../../../types/Product";

interface ProductCardProps {
  product: Product;
}

export function ProductCard({ product }: ProductCardProps) {
  const navigate = useNavigate();

  const handleNavigate = () => {
    navigate(`/catalog/${product.id}`);
  };

  return (
    <div
      onClick={handleNavigate}
      className="bg-white rounded-lg shadow-md overflow-hidden relative hover:cursor-pointer"
    >
      <img
        src={product.images?.[0] || "https://via.placeholder.com/300x200"}
        alt={product.name}
        className="w-full h-48 object-cover"
      />

      <div className="p-4">
        <h3 className="text-lg font-semibold">{product.name}</h3>
        <p className="text-gray-500 text-sm mb-2 line-clamp-2">
          {product.description}
        </p>
        <span className="text-xl font-bold text-orange-600">
          ${product.price.toFixed(2)}
        </span>
      </div>
    </div>
  );
}
