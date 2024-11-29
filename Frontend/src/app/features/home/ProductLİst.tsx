import React, { FC } from "react";

interface Product {
  id: number;
  name: string;
  description: string;
  price: string;
  discount: string;
  image: string;
}

const ProductCard: FC<Product> = ({
  name,
  description,
  price,
  discount,
  image,
}) => {
  return (
    <div className="bg-white rounded-lg shadow-md overflow-hidden relative">
      <img src={image} alt={name} className="w-full h-48 object-cover" />
      {discount && (
        <div className="absolute top-2 right-2 bg-red-500 text-white px-3 py-1 rounded-full">
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
};

const ProductList: FC = () => {
  const products: Product[] = [
    {
      id: 1,
      name: "Discounted Product 1",
      description: "Product description goes here.",
      price: "$39.99",
      discount: "20%",
      image: "https://via.placeholder.com/300x300?text=Discounted+Product+1",
    },
    {
      id: 2,
      name: "Discounted Product 2",
      description: "Another great product.",
      price: "$49.99",
      discount: "15%",
      image: "https://via.placeholder.com/300x300?text=Discounted+Product+2",
    },
    {
      id: 3,
      name: "Discounted Product 3",
      description: "Yet another awesome product.",
      price: "$29.99",
      discount: "10%",
      image: "https://via.placeholder.com/300x300?text=Discounted+Product+3",
    },
    {
      id: 4,
      name: "Discounted Product 3",
      description: "Yet another awesome product.",
      price: "$29.99",
      discount: "10%",
      image: "https://via.placeholder.com/300x300?text=Discounted+Product+3",
    },
  ];

  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-4 gap-4">
      {products.map((product) => (
        <ProductCard key={product.id} {...product} />
      ))}
    </div>
  );
};

export default ProductList;
