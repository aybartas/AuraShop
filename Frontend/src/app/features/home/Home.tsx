import Carousel from "./Carousel";
import PageLayout from "../../layout/PageLayout";
import ProductSection from "./ProductSection";
import Button from "../../../components/ui/Button";

function Home() {
  return (
    <PageLayout>
      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
        <div className="col-span-1 sm:col-span-2 lg:col-span-2">
          <Carousel />
        </div>

        <div className="flex flex-col gap-4 sm:grid sm:grid-cols-2 lg:grid-cols-1">
          <div className="relative bg-background rounded-lg shadow-md overflow-hidden">
            <img
              src="https://picsum.photos/300"
              alt="Product 2"
              className="w-full h-48 object-cover"
            />
            <div className="absolute inset-0 flex flex-col items-center justify-center bg-black/50">
              <h3 className="text-lg font-semibold text-white mb-2">
                Product 2
              </h3>
              <p className="text-white mb-4">Product description goes here.</p>
              <Button variant="primary" size="md">
                Shop Now
              </Button>
            </div>
          </div>

          <div className="relative bg-background rounded-lg shadow-md overflow-hidden">
            <img
              src="https://picsum.photos/300"
              alt="Product 3"
              className="w-full h-48 object-cover"
            />
            <div className="absolute inset-0 flex flex-col items-center justify-center bg-black/50">
              <h3 className="text-lg font-semibold text-white mb-2">
                Product 3
              </h3>
              <p className="text-white mb-4">Product description goes here.</p>
              <Button variant="primary" size="md">
                Shop Now
              </Button>
            </div>
          </div>
        </div>
      </div>

      <ProductSection />
    </PageLayout>
  );
}

export default Home;
