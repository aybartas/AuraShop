import React from "react";
import { Product } from "../../../types/Product";
import { ProductCard } from "../../components/product/ProductCard";

interface Props {
  products: Product[];
  loading: boolean;
}

const SkeletonCard = () => (
  <div className="animate-pulse bg-white shadow-md rounded-md p-4 space-y-4">
    <div className="bg-gray-300 h-40 w-full rounded-md" />
    <div className="h-4 bg-gray-300 rounded w-3/4" />
    <div className="h-4 bg-gray-300 rounded w-1/2" />
    <div className="h-4 bg-gray-200 rounded w-1/3" />
  </div>
);
const skeletonCount = 8;

function ProductList({ products, loading }: Props) {
  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-4 gap-4">
      {loading
        ? Array.from({ length: skeletonCount }).map((_, index) => (
            <SkeletonCard key={index} />
          ))
        : products.map((product) => (
            <React.Fragment key={product.id}>
              <ProductCard product={product} />
            </React.Fragment>
          ))}
    </div>
  );
}

export default ProductList;
