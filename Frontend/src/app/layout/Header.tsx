import logo from "../../assets/logo.svg";
import { useState } from "react";
import { ShoppingCartIcon } from "@heroicons/react/24/outline";
import { NavLink } from "react-router-dom";
import { useAuth } from "../../hooks/useAuth";

interface CategoryLink {
  name: string;
  url?: string;
  subcategories?: string[];
}
const categories: CategoryLink[] = [{ name: "Catalog", url: "/catalog" }];

export default function Header() {
  const { user, logout } = useAuth();

  const [isOpen, setIsOpen] = useState(false);
  const [activeCategory, setActiveCategory] = useState<number | null>(null);

  const handleMouseEnter = (index: number) => setActiveCategory(index);
  const handleMouseLeave = (index: number) => {
    if (activeCategory === index) setActiveCategory(null);
  };

  return (
    <nav className="bg-white shadow-md sticky top-0 z-50">
      <div className="container mx-auto px-4 py-3 flex items-center justify-between">
        <NavLink to="/" className="flex items-center space-x-2">
          <img src={logo} alt="AuraShop" className="h-8 w-8" />
          <span className="font-bold text-xl text-gray-800">AuraShop</span>
        </NavLink>

        <div className="hidden md:flex items-center space-x-8">
          {categories.map((category, idx) => (
            <div
              key={idx}
              className="relative"
              onMouseEnter={() => handleMouseEnter(idx)}
              onMouseLeave={() => handleMouseLeave(idx)}
            >
              <NavLink
                to={category.url || "#"}
                className={({ isActive }) =>
                  `px-2 py-1 text-gray-700 hover:text-blue-600 ${
                    isActive ? "text-blue-600 font-semibold" : ""
                  }`
                }
              >
                {category.name}
              </NavLink>

              {/* Subcategories Dropdown */}
              {category.subcategories && activeCategory === idx && (
                <div className="absolute top-full left-0 mt-1 bg-white border border-gray-200 rounded shadow-md min-w-[150px]">
                  {category.subcategories.map((sub, subIdx) => (
                    <NavLink
                      key={subIdx}
                      to="#"
                      className="block px-4 py-2 text-gray-600 hover:bg-blue-50 hover:text-blue-600"
                    >
                      {sub}
                    </NavLink>
                  ))}
                </div>
              )}
            </div>
          ))}
        </div>

        <div className="hidden md:flex flex-1 max-w-md mx-6">
          <input
            type="search"
            placeholder="Search anything..."
            className="w-full border border-gray-300 rounded-lg px-4 py-2 text-gray-700 focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
        </div>

        <div className="hidden md:flex items-center space-x-6">
          {user ? (
            <>
              <span className="text-gray-700">Hello, {user.email}</span>
              <button
                onClick={logout}
                className="text-gray-700 hover:text-red-600"
              >
                Logout
              </button>
            </>
          ) : (
            <>
              <NavLink
                to="/login"
                className="text-gray-700 hover:text-blue-600 transition"
              >
                Login
              </NavLink>
            </>
          )}
          <NavLink
            to="/cart"
            className="flex items-center text-gray-700 hover:text-blue-600"
          >
            <ShoppingCartIcon className="h-5 w-5 mr-1" />
            Cart
          </NavLink>
        </div>

        {/* Mobile Hamburger */}
        <button
          className="md:hidden text-gray-700 focus:outline-none"
          aria-label="Toggle menu"
          onClick={() => setIsOpen(!isOpen)}
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            className="h-6 w-6"
            fill="none"
            viewBox="0 0 24 24"
            stroke="currentColor"
            strokeWidth={2}
          >
            {isOpen ? (
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="M6 18L18 6M6 6l12 12"
              />
            ) : (
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="M4 6h16M4 12h16M4 18h16"
              />
            )}
          </svg>
        </button>
      </div>

      {/* Mobile Menu */}
      {isOpen && (
        <div className="md:hidden bg-white border-t border-gray-200 shadow-lg px-4 pb-4">
          <div className="pt-2 space-y-1">
            {categories.map((category, idx) => (
              <div key={idx}>
                <button
                  className="w-full flex justify-between items-center py-2 text-gray-700 hover:text-blue-600 focus:outline-none"
                  onClick={() =>
                    setActiveCategory(activeCategory === idx ? null : idx)
                  }
                >
                  {category.name}
                  <svg
                    className={`h-4 w-4 transform transition-transform ${
                      activeCategory === idx ? "rotate-180" : "rotate-0"
                    }`}
                    fill="none"
                    stroke="currentColor"
                    strokeWidth={2}
                    viewBox="0 0 24 24"
                    xmlns="http://www.w3.org/2000/svg"
                  >
                    <path
                      strokeLinecap="round"
                      strokeLinejoin="round"
                      d="M19 9l-7 7-7-7"
                    ></path>
                  </svg>
                </button>
                {activeCategory === idx && category.subcategories && (
                  <div className="pl-4 space-y-1">
                    {category.subcategories.map((sub, subIdx) => (
                      <NavLink
                        key={subIdx}
                        to="#"
                        className="block py-1 text-gray-600 hover:text-blue-600"
                      >
                        {sub}
                      </NavLink>
                    ))}
                  </div>
                )}
              </div>
            ))}
          </div>

          <div className="mt-4">
            <input
              type="search"
              placeholder="Search anything..."
              className="w-full border border-gray-300 rounded-lg px-4 py-2 text-gray-700 focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
          </div>

          <div className="mt-4 space-y-2">
            {user ? (
              <>
                <span className="block text-gray-700">Hello, {user.email}</span>
                <button
                  onClick={logout}
                  className="w-full text-left text-gray-700 hover:text-red-600"
                >
                  Logout
                </button>
              </>
            ) : (
              <>
                <NavLink
                  to="/login"
                  className="block text-gray-700 hover:text-blue-600"
                >
                  Login
                </NavLink>
                <NavLink
                  to="/signup"
                  className="block text-gray-700 hover:text-blue-600"
                >
                  Sign Up
                </NavLink>
              </>
            )}
            <NavLink
              to="/cart"
              className="flex items-center text-gray-700 hover:text-blue-600"
            >
              <ShoppingCartIcon className="h-5 w-5 mr-2" />
              Cart
            </NavLink>
          </div>
        </div>
      )}
    </nav>
  );
}
