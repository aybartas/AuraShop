import React from "react";
import { Product } from "../../../types/Product";
import { ProductCard } from "../../components/product/ProductCard";

function ProductList() {
  const products: Product[] = [
    {
      id: 1,
      name: "Discounted Product 1",
      description: "Product description goes here.",
      price: "$39.99",
      discount: "20%",
      image: "https://picsum.photos/300",
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
      colors: [],
      sizes: [],
    },
    {
      id: 2,
      name: "Discounted Product 2",
      description: "Another great product.",
      price: "$49.99",
      discount: "15%",
      image: "https://picsum.photos/300",
      rating: 4.2,
      comments: [
        {
          user: "John Smith",
          rating: 5,
          comment: "Excellent craftsmanship!",
          date: "2024-11-29",
        },
      ],
      colors: [],
      sizes: [],
    },
    {
      id: 3,
      name: "Discounted Product 3",
      description: "Yet another awesome product.",
      price: "$29.99",
      discount: "10%",
      image: "https://picsum.photos/300",
      rating: 3.8,
      comments: [
        {
          user: "Sarah Connor",
          rating: 4,
          comment: "Met expectations, but could improve.",
          date: "2024-11-28",
        },
      ],
      colors: [],
      sizes: [],
    },
  ];
  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-4 gap-4">
      {products.map((product) => (
        <React.Fragment key={product.id}>
          <ProductCard {...product} />
        </React.Fragment>
      ))}
    </div>
  );
}

export default ProductList;
