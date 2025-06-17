import { useNavigate } from "react-router-dom";
import { Product } from "../../../types/Product";

export function ProductCard({
  image,
  discount,
  name,
  description,
  price,
  id,
}: Product) {
  const navigate = useNavigate();

  const handleNavigate = () => {
    navigate(`/catalog/${id}`);
  };
  return (
    <div
      onClick={handleNavigate}
      className="bg-white rounded-lg shadow-md overflow-hidden relative hover:cursor-pointer"
    >
      <img src={image} alt={name} className="w-full h-48 object-cover" />
      {discount && (
        <div className="absolute top-2 right-2 bg-red-400 text-white px-3 py-1 rounded-full">
          {discount} Off
        </div>
      )}
      <div className="p-4">
        <h3 className="text-lg font-semibold">{name}</h3>
        <p className="text-gray-500 mb-2">{description}</p>
        <span className="text-xl font-bold text-red-500">{price}</span>
      </div>
    </div>
  );
}
