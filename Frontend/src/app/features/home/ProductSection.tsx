import React from "react";
import "tailwindcss/tailwind.css";

const ProductSection: React.FC = () => {
  return (
    <div className="space-y-12">
      {/* Best Sellers Section */}
      <div className="mt-8">
        <h2 className="text-3xl font-bold mb-8">Best Sellers</h2>
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-8">
          {/* Product Card 1 */}
          <div className="bg-white rounded-lg shadow-md overflow-hidden">
            <img
              src="https://via.placeholder.com/300x300?text=Product+1"
              alt="Product 1"
              className="w-full h-48 object-cover"
            />
            <div className="p-4">
              <h3 className="text-lg font-semibold">Product 1</h3>
              <p className="text-gray-500 mb-2">
                Product description goes here.
              </p>
              <span className="text-xl font-bold">$49.99</span>
            </div>
          </div>
          {/* Product Card 2 */}
          <div className="bg-white rounded-lg shadow-md overflow-hidden">
            <img
              src="https://via.placeholder.com/300x300?text=Product+2"
              alt="Product 2"
              className="w-full h-48 object-cover"
            />
            <div className="p-4">
              <h3 className="text-lg font-semibold">Product 2</h3>
              <p className="text-gray-500 mb-2">
                Product description goes here.
              </p>
              <span className="text-xl font-bold">$59.99</span>
            </div>
          </div>
          {/* Product Card 3 */}
          <div className="bg-white rounded-lg shadow-md overflow-hidden">
            <img
              src="https://via.placeholder.com/300x300?text=Product+3"
              alt="Product 3"
              className="w-full h-48 object-cover"
            />
            <div className="p-4">
              <h3 className="text-lg font-semibold">Product 3</h3>
              <p className="text-gray-500 mb-2">
                Product description goes here.
              </p>
              <span className="text-xl font-bold">$39.99</span>
            </div>
          </div>
          {/* Product Card 4 */}
          <div className="bg-white rounded-lg shadow-md overflow-hidden">
            <img
              src="https://via.placeholder.com/300x300?text=Product+4"
              alt="Product 4"
              className="w-full h-48 object-cover"
            />
            <div className="p-4">
              <h3 className="text-lg font-semibold">Product 4</h3>
              <p className="text-gray-500 mb-2">
                Product description goes here.
              </p>
              <span className="text-xl font-bold">$69.99</span>
            </div>
          </div>
        </div>
      </div>

      {/* Amazing Discounts Section */}
      <div className="py-12">
        <h2 className="text-3xl font-bold mb-8">Amazing Discounts</h2>
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-8">
          <div className="bg-white rounded-lg shadow-md overflow-hidden relative">
            <img
              src="https://via.placeholder.com/300x300?text=Discounted+Product+1"
              alt="Discounted Product 1"
              className="w-full h-48 object-cover"
            />
            <div className="absolute top-2 right-2 bg-red-500 text-white px-3 py-1 rounded-full">
              20% Off
            </div>
            <div className="p-4">
              <h3 className="text-lg font-semibold">Discounted Product 1</h3>
              <p className="text-gray-500 mb-2">
                Product description goes here.
              </p>
              <span className="text-xl font-bold text-red-500">$39.99</span>
            </div>
          </div>
          {/* Product Card 2 with Discount */}
          <div className="bg-white rounded-lg shadow-md overflow-hidden relative">
            <img
              src="https://via.placeholder.com/300x300?text=Discounted+Product+2"
              alt="Discounted Product 2"
              className="w-full h-48 object-cover"
            />
            <div className="absolute top-2 right-2 bg-red-500 text-white px-3 py-1 rounded-full">
              15% Off
            </div>
            <div className="p-4">
              <h3 className="text-lg font-semibold">Discounted Product 2</h3>
              <p className="text-gray-500 mb-2">
                Product description goes here.
              </p>
              <span className="text-xl font-bold text-red-500">$49.99</span>
            </div>
          </div>
          {/* Product Card 3 with Discount */}
          <div className="bg-white rounded-lg shadow-md overflow-hidden relative">
            <img
              src="https://via.placeholder.com/300x300?text=Discounted+Product+3"
              alt="Discounted Product 3"
              className="w-full h-48 object-cover"
            />
            <div className="absolute top-2 right-2 bg-red-500 text-white px-3 py-1 rounded-full">
              30% Off
            </div>
            <div className="p-4">
              <h3 className="text-lg font-semibold">Discounted Product 3</h3>
              <p className="text-gray-500 mb-2">
                Product description goes here.
              </p>
              <span className="text-xl font-bold text-red-500">$29.99</span>
            </div>
          </div>
          {/* Product Card 4 with Discount */}
          <div className="bg-white rounded-lg shadow-md overflow-hidden relative">
            <img
              src="https://via.placeholder.com/300x300?text=Discounted+Product+4"
              alt="Discounted Product 4"
              className="w-full h-48 object-cover"
            />
            <div className="absolute top-2 right-2 bg-red-500 text-white px-3 py-1 rounded-full">
              10% Off
            </div>
            <div className="p-4">
              <h3 className="text-lg font-semibold">Discounted Product 4</h3>
              <p className="text-gray-500 mb-2">
                Product description goes here.
              </p>
              <span className="text-xl font-bold text-red-500">$59.99</span>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ProductSection;
