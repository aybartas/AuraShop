import { useEffect, useState } from "react";
import {
  ShoppingCartIcon,
  ArrowLeftIcon,
  ArrowRightIcon,
} from "@heroicons/react/24/outline";
import Button from "../../../components/ui/Button";
import IconButton from "../../../components/ui/IconButton";

interface Slide {
  id: number;
  category: string;
  description: string;
  image: string;
}

const slides: Slide[] = [
  {
    id: 1,
    category: "Electronics",
    description: "Latest gadgets and electronics.",
    image:
      "https://www.eurokidsindia.com/blog/wp-content/uploads/2023/12/names-of-electronic-devices-in-english-870x570.jpg",
  },
  {
    id: 2,
    category: "Fashion",
    description: "Trendy clothing and accessories.",
    image:
      "https://media.istockphoto.com/id/1398610798/tr/foto%C4%9Fraf/young-woman-in-linen-shirt-shorts-and-high-heels-pointing-to-the-side-and-talking.jpg?s=612x612&w=0&k=20&c=dmIBAa6CCMNbP9PXTO5L1mlHspqmfRcf5yFhImOcB1c=",
  },
  {
    id: 3,
    category: "Home Decor",
    description: "Beautiful decor for your home.",
    image:
      "https://cdn.decorilla.com/online-decorating/wp-content/uploads/2023/01/Minimalist-home-decor-The-Spruce.jpg?width=900",
  },
];

export default function Carousel() {
  const [currentIndex, setCurrentIndex] = useState(0);

  const nextSlide = () => {
    setCurrentIndex((prev) => (prev + 1) % slides.length);
  };

  const prevSlide = () => {
    setCurrentIndex((prev) => (prev - 1 + slides.length) % slides.length);
  };

  useEffect(() => {
    const interval = setInterval(nextSlide, 5000);
    return () => clearInterval(interval);
  }, []);

  return (
    <div className="h-full relative w-full max-w-6xl mx-auto overflow-hidden">
      <div
        className="h-full relative flex items-center justify-center w-full bg-cover bg-center transition-all duration-1000 ease-in-out rounded-lg"
        style={{ backgroundImage: `url(${slides[currentIndex].image})` }}
      >
        <div className="absolute inset-0 bg-black/50 rounded-lg" />
        <div className="relative text-center text-white">
          <h2 className="text-2xl font-bold mb-2">
            {slides[currentIndex].category}
          </h2>
          <p className="mb-4">{slides[currentIndex].description}</p>
          <div className="flex justify-center">
            <Button variant="primary" size="md">
              <ShoppingCartIcon className="h-5 w-5" />
              Shop Now
            </Button>
          </div>
        </div>
      </div>

      <div className="absolute top-1/2 left-4 -translate-y-1/2">
        <IconButton
          icon={<ArrowLeftIcon className="h-5 w-5" />}
          aria-label="Previous slide"
          onClick={prevSlide}
          className="bg-gray-800 text-white hover:bg-gray-700"
        />
      </div>

      <div className="absolute top-1/2 right-4 -translate-y-1/2">
        <IconButton
          icon={<ArrowRightIcon className="h-5 w-5" />}
          aria-label="Next slide"
          onClick={nextSlide}
          className="bg-gray-800 text-white hover:bg-gray-700"
        />
      </div>
    </div>
  );
}
