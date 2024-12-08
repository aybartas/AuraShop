import Carousel from "./Carousel";
import PageLayout from "../../layout/PageLayout";
import CategoryProductsSection from "./ProductSection";

function HomePage() {
  return (
    <PageLayout>
      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
        <div className="col-span-1 sm:col-span-2 lg:col-span-2">
          <Carousel />
        </div>

        <div className="flex flex-col gap-4 sm:grid sm:grid-cols-2 lg:grid-cols-1">
          <div className="relative bg-white rounded-lg shadow-md overflow-hidden">
            <img
              src="https://via.placeholder.com/300x300?text=Product+2"
              alt="Product 2"
              className="w-full h-48 object-cover"
            />

            <div className="absolute inset-0 flex flex-col items-center justify-center bg-black bg-opacity-50">
              <h3 className="text-lg font-semibold text-white mb-2">
                Product 2
              </h3>
              <p className="text-white mb-4">Product description goes here.</p>
              <button className="px-4 py-2 bg-blue-500 text-white font-semibold rounded-lg hover:bg-blue-600">
                Shop Now
              </button>
            </div>
          </div>

          <div className="relative bg-white rounded-lg shadow-md overflow-hidden">
            <img
              src="https://via.placeholder.com/300x300?text=Product+2"
              alt="Product 2"
              className="w-full h-48 object-cover"
            />

            <div className="absolute inset-0 flex flex-col items-center justify-center bg-black bg-opacity-50">
              <h3 className="text-lg font-semibold text-white mb-2">
                Product 2
              </h3>
              <p className="text-white mb-4">Product description goes here.</p>
              <button className="px-4 py-2 bg-blue-500 text-white font-semibold rounded-lg hover:bg-blue-600">
                Shop Now
              </button>
            </div>
          </div>
        </div>
      </div>

      <CategoryProductsSection />
    </PageLayout>
  );
}

export default HomePage;
