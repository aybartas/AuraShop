import React, { useState } from "react";
import { useForm, Controller } from "react-hook-form";
import { ChevronDownIcon, ChevronUpIcon } from "@heroicons/react/24/solid";
interface FilterFormInputs {
  categories: string[];
  brands: string[];
  priceRange: { min: string; max: string };
}

const FilterForm: React.FC = () => {
  const { control, handleSubmit } = useForm<FilterFormInputs>();
  const [isCategoriesOpen, setIsCategoriesOpen] = useState(false);

  const onSubmit = (data: FilterFormInputs) => {
    console.log(data);
  };

  const categoriesOptions = ["Electronics", "Clothing", "Home Appliances"];
  const brandOptions = ["Samsung", "Nike", "LG"];

  return (
    <form
      className="flex flex-col gap-4 bg-white p-6 shadow-md rounded-xl"
      onSubmit={handleSubmit(onSubmit)}
    >
      <h2 className="text-lg font-semibold mb-4">Filter Products</h2>

      <div
        className="flex items-center cursor-pointer mb-1 gap-2"
        onClick={() => setIsCategoriesOpen(!isCategoriesOpen)}
      >
        <span className="text-sm text-gray-500">
          {isCategoriesOpen ? (
            <ChevronUpIcon className="h-3 w-3 text-gray-500" />
          ) : (
            <ChevronDownIcon className="h-3 w-3 text-gray-500" />
          )}
        </span>
        <label className="block text-sm font-medium text-gray-700">
          Categories
        </label>
      </div>
      {isCategoriesOpen && (
        <Controller
          name="categories"
          control={control}
          render={({ field }) => (
            <div className="flex flex-col gap-2">
              {categoriesOptions.map((category) => (
                <label
                  key={category}
                  className="inline-flex items-center gap-2"
                >
                  <input
                    type="checkbox"
                    value={category}
                    className="accent-orange-400"
                    checked={field.value?.includes(category) || false}
                    onChange={(e) => {
                      const value = field.value || [];
                      if (e.target.checked) {
                        field.onChange([...value, category]);
                      } else {
                        field.onChange(value.filter((v) => v !== category));
                      }
                    }}
                  />
                  {category}
                </label>
              ))}
            </div>
          )}
        />
      )}

      {/* Brands Filter */}
      <div className="">
        <label className="block text-sm font-medium text-gray-700 mb-1">
          Brands
        </label>
        <Controller
          name="brands"
          control={control}
          render={({ field }) => (
            <div className="flex flex-col gap-2">
              {brandOptions.map((brand) => (
                <label key={brand} className="inline-flex items-center gap-2">
                  <input
                    type="checkbox"
                    value={brand}
                    checked={field.value?.includes(brand) || false}
                    onChange={(e) => {
                      const value = field.value || [];
                      if (e.target.checked) {
                        field.onChange([...value, brand]);
                      } else {
                        field.onChange(value.filter((v) => v !== brand));
                      }
                    }}
                    className="form-checkbox"
                  />
                  {brand}
                </label>
              ))}
            </div>
          )}
        />
      </div>

      {/* Price Range Filter */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-1">
          Price Range
        </label>

        <div className="flex items-center gap-4">
          <Controller
            name="priceRange.min"
            control={control}
            render={({ field }) => (
              <input
                {...field}
                type="number"
                placeholder="Min"
                className="w-full border border-gray-300 rounded-md p-2 text-sm"
              />
            )}
          />
          <span className="text-gray-500">-</span>

          <Controller
            name="priceRange.max"
            control={control}
            render={({ field }) => (
              <input
                {...field}
                type="number"
                placeholder="Min"
                className="w-full border border-gray-300 rounded-md p-2 text-sm"
              />
            )}
          />
        </div>
      </div>

      {/* Submit Button */}
      <button
        type="submit"
        className="w-full bg-orange-500 hover:bg-orange-600 text-white py-2 px-4 rounded-lg text-sm"
      >
        Apply Filters
      </button>
    </form>
  );
};

export default FilterForm;
