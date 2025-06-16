import logo from "../../assets/logo.svg";
import { useState } from "react";
import { ShoppingCartIcon } from "@heroicons/react/24/outline";
import { NavLink } from "react-router-dom";

interface CategoryLink {
  name: string;
  url?: string;
  subcategories?: string[];
}
const categories: CategoryLink[] = [
  { name: "Catalog", url: "/catalog" },
  { name: "Category 1", subcategories: ["Sub 1", "Sub 2", "Sub 3"] },
  { name: "Category 1", subcategories: ["Sub 1", "Sub 2", "Sub 3"] },
  { name: "Category 2", subcategories: ["Sub A", "Sub B"] },
  { name: "Category 3", subcategories: ["Sub X"] },
];

export default function Header() {
  const [isOpen, setIsOpen] = useState(false);
  const [activeCategory, setActiveCategory] = useState<number | null>(null);

  const handleMouseEnter = (enterIndex: number) => {
    setActiveCategory(enterIndex);
  };

  const handleMouseLeave = (leaveIndex: number) => {
    if (activeCategory === leaveIndex) {
      setActiveCategory(null);
    }
  };
  return (
    <nav className="bg-white shadow-lg sticky top-0 z-50">
      <div className="container mx-auto px-4 py-2 flex items-center justify-between">
        <NavLink className="flex items-center space-x-2" to={"/"}>
          <img src={logo} alt="Logo" className="h-8 w-8" />;
          <span className="text-xl font-bold text-gray-800">AuraShop</span>
        </NavLink>

        {/* Categories */}
        <div className="hidden md:flex items-center space-x-4 relative">
          {categories.map((category, index) => (
            <div
              key={index}
              className="group relative"
              onMouseEnter={() => handleMouseEnter(index)}
              onMouseLeave={() => handleMouseLeave(index)}
            >
              <NavLink to={category?.url || ""}>
                <button className="text-gray-700 hover:text-blue-500">
                  {category.name}
                </button>
              </NavLink>

              {activeCategory === index && (
                <div className="absolute left-0 mt-2 w-48 bg-white shadow-lg rounded-lg">
                  {category?.subcategories?.map((sub, idx) => (
                    <a
                      key={idx}
                      href="#"
                      className="block px-4 py-2 text-gray-700 hover:bg-gray-100"
                    >
                      {sub}
                    </a>
                  ))}
                </div>
              )}
            </div>
          ))}
        </div>

        <div className="hidden md:flex items-center">
          <input
            type="text"
            placeholder="Search anything..."
            className="border border-gray-300 rounded-lg px-4 py-2 focus:ring-2 focus:ring-blue-500"
          />
        </div>

        {/* Account Section */}
        <div className="hidden md:flex items-center space-x-4">
          <NavLink to={"/login"}>
            <button className="text-gray-700 hover:text-blue-500">
              Login / Sign Up
            </button>
          </NavLink>
        </div>

        <div className="hidden md:flex items-center space-x-4">
          <button className="text-gray-700 hover:text-blue-500">Account</button>
          <button className="flex items-center text-gray-700 hover:text-blue-500">
            <ShoppingCartIcon className="h-5 w-5 mr-2" />
            Cart
          </button>
        </div>

        {/* Mobile Menu Toggle */}
        <button
          className="md:hidden text-gray-700"
          onClick={() => setIsOpen(!isOpen)}
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            strokeWidth="1.5"
            stroke="currentColor"
            className="w-6 h-6"
          >
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="M3.75 6.75h16.5m-16.5 5.25h16.5m-16.5 5.25h16.5"
            />
          </svg>
        </button>
      </div>

      {isOpen && (
        <div className="md:hidden bg-white shadow-lg py-2 px-4">
          <div className="space-y-2">
            {categories.map((category, index) => (
              <div key={index} className="relative">
                <button
                  className="w-full text-left text-gray-700 hover:text-blue-500"
                  onClick={() =>
                    setActiveCategory(activeCategory === index ? null : index)
                  }
                >
                  {category.name}
                </button>
                {activeCategory === index && (
                  <div className="mt-2 pl-4 space-y-1">
                    {category?.subcategories?.map((sub, idx) => (
                      <a
                        key={idx}
                        href="#"
                        className="block text-gray-700 hover:text-blue-500"
                      >
                        {sub}
                      </a>
                    ))}
                  </div>
                )}
              </div>
            ))}
          </div>
          <div className="mt-4">
            <input
              type="text"
              placeholder="Search..."
              className="w-full border border-gray-300 rounded-lg px-4 py-2 focus:ring-2 focus:ring-blue-500"
            />
          </div>
          <div className="mt-4 flex flex-col space-y-2">
            <button className="text-gray-700 hover:text-blue-500">Login</button>
            <button className="text-gray-700 hover:text-blue-500">
              Sign Up
            </button>
            <button className="text-gray-700 hover:text-blue-500">
              <ShoppingCartIcon className="h-5 w-5 mr-2" />
              Cart
            </button>
          </div>
        </div>
      )}
    </nav>
  );
}
