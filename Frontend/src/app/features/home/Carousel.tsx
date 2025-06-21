import React, { useEffect, useState } from "react";
import {
  ShoppingCartIcon,
  ArrowLeftIcon,
  ArrowRightIcon,
} from "@heroicons/react/24/outline";
import "tailwindcss/tailwind.css";

interface Slide {
  id: number;
  category: string;
  description: string;
  image: string;
}

const Carousel: React.FC = () => {
  const [currentIndex, setCurrentIndex] = useState<number>(0);

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

  const nextSlide = () => {
    setCurrentIndex((prevIndex) => (prevIndex + 1) % slides.length);
  };

  const prevSlide = () => {
    setCurrentIndex(
      (prevIndex) => (prevIndex - 1 + slides.length) % slides.length
    );
  };

  useEffect(() => {
    const interval = setInterval(nextSlide, 5000);
    return () => clearInterval(interval); 
  }, []);

  return (
    <div className="h-full relative w-full max-w-6xl mx-auto overflow-hidden">
      <div
        className={`h-full relative flex items-center justify-center w-full bg-cover bg-center transition-all duration-1000 ease-in-out rounded-lg`}
        style={{
          backgroundImage: `url(${slides[currentIndex].image})`,
          opacity: 1,
        }}
      >
        <div className="absolute inset-0 bg-black bg-opacity-50 rounded-lg"></div>
        <div className="relative text-center text-white">
          <h2 className="text-2xl font-bold mb-2">
            {slides[currentIndex].category}
          </h2>
          <p className="mb-4">{slides[currentIndex].description}</p>
          <div className="flex justify-center">
            <button className="flex items-center px-4 py-2 bg-blue-500 hover:bg-blue-600 text-white font-semibold rounded-lg">
              <ShoppingCartIcon className="h-5 w-5 mr-2" /> Shop Now
            </button>
          </div>
        </div>
      </div>

      <button
        onClick={prevSlide}
        className="flex items-center absolute top-1/2 left-4 w-12 transform -translate-y-1/2 bg-gray-800 text-white p-2 rounded-full focus:outline-none hover:bg-gray-700"
      >
        <ArrowLeftIcon />
      </button>

      <button
        onClick={nextSlide}
        className="flex items-center absolute top-1/2 right-4 w-12 transform -translate-y-1/2 bg-gray-800 text-white p-2 rounded-full focus:outline-none hover:bg-gray-700"
      >
        <ArrowRightIcon />
      </button>
    </div>
  );
};

export default Carousel;
