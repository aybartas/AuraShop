import React from "react";
import { Product } from "../../../types/Product";
import ProductCard from "../../../components/ProductCard";
import Skeleton from "../../../components/ui/Skeleton";

interface Props {
  products: Product[];
  loading: boolean;
}

function ProductList({ products, loading }: Props) {
  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-4 gap-4">
      {loading
        ? Array.from({ length: 8 }).map((_, index) => (
            <div key={index} className="bg-background shadow-md rounded-md p-4 space-y-4">
              <Skeleton className="h-40 w-full" />
              <Skeleton className="h-4 w-3/4" />
              <Skeleton className="h-4 w-1/2" />
              <Skeleton className="h-4 w-1/3" />
            </div>
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
