import React from "react";
import { useForm, Controller } from "react-hook-form";
import MultiCheckboxSelection from "./filters/MultiCheckboxSelection";
interface FilterFormInputs {
  categories: string[];
  brands: string[];
  priceRange: { min: string; max: string };
}

const FilterForm: React.FC = () => {
  const { control, handleSubmit } = useForm<FilterFormInputs>();

  const onSubmit = (data: FilterFormInputs) => {
    alert(JSON.stringify(data));
  };

  const categoriesOptions = ["Electronics", "Clothing", "Home Appliances"];
  const brandOptions = ["Samsung", "Nike", "LG"];

  return (
    <form
      className="flex flex-col gap-4 bg-white p-6 shadow-md rounded-xl"
      onSubmit={handleSubmit(onSubmit)}
    >
      <h2 className="text-lg font-semibold mb-4">Filter Products</h2>

      <MultiCheckboxSelection
        options={categoriesOptions}
        control={control}
        name="categories"
        label="Categories"
      />

      <MultiCheckboxSelection
        options={brandOptions}
        control={control}
        name="brands"
        label="Brand"
      />

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
