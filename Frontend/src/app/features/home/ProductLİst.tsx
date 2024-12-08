import React, { useState } from "react";
import { Product } from "../../models/Product";
import { ProductCard } from "../../components/product/ProductCard";
import { products } from "../../static";

function ProductList() {
  const [sortBy, setSortBy] = useState<string>("name");

  const handleSort = (a: Product, b: Product): number => {
    switch (sortBy) {
      case "price":
        return (
          parseFloat(a.price.substring(1)) - parseFloat(b.price.substring(1))
        );
      case "rating":
        return (b.rating || 0) - (a.rating || 0);
      default:
        return a.name.localeCompare(b.name);
    }
  };

  return (
    <div className="flex flex-col gap-4 bg-white p-4 shadow-md rounded-xl">
      <div className="flex justify-end mb-4">
        <div className="relative">
          <label htmlFor="sort" className="sr-only">
            Sort By
          </label>
          <select
            id="sort"
            className="block w-full appearance-none bg-white border border-gray-300 text-gray-700 py-2 px-4 pr-8 rounded leading-tight focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
            value={sortBy}
            onChange={(e) => setSortBy(e.target.value)}
          >
            <option value="name">Name</option>
            <option value="price">Price</option>
            <option value="rating">Rating</option>
          </select>
          <div className="pointer-events-none absolute inset-y-0 right-0 flex items-center px-2 text-gray-700">
            <svg
              className="fill-current h-4 w-4"
              xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 20 20"
            >
              <path d="M5.516 7.548L10 12.032l4.484-4.484 1.032 1.032-5.516 5.516-5.516-5.516z" />
            </svg>
          </div>
        </div>
      </div>
      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-4 gap-4">
        {products.sort(handleSort).map((product) => (
          <React.Fragment key={product.id}>
            <ProductCard {...product} />
          </React.Fragment>
        ))}
      </div>
    </div>
  );
}

export default ProductList;
