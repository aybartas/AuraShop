import React from "react";
import "tailwindcss/tailwind.css";
import { products } from "../../static";
import { ProductCard } from "../../components/product/ProductCard";

const sections = [
  { label: "Best Sellers", productList: products.slice(0, 4) },
  { label: "Amazing Discounts", productList: products.slice(4, 8) },
];

const CategoryProductsSection: React.FC = () => {
  return (
    <div className="space-y-12 mt-8">
      {sections?.map((section) => {
        return (
          <div key={section?.label} className="mt-8">
            <h2 className="text-3xl font-bold mb-8">{section.label}</h2>
            <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-8">
              {section?.productList?.map((product) => (
                <ProductCard {...product} />
              ))}
            </div>
          </div>
        );
      })}
    </div>
  );
};

export default CategoryProductsSection;
