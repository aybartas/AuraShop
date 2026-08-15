import { useState } from "react";
import { useForm, Controller } from "react-hook-form";
import { ChevronDownIcon, ChevronUpIcon } from "@heroicons/react/24/solid";
import { Button, Input } from "../../../components/ui";

interface FilterFormInputs {
  categories: string[];
  brands: string[];
  priceRange: { min: string; max: string };
}

const categoriesOptions = ["Electronics", "Clothing", "Home Appliances"];
const brandOptions = ["Samsung", "Nike", "LG"];

export default function FilterForm() {
  const { control, handleSubmit } = useForm<FilterFormInputs>();
  const [isCategoriesOpen, setIsCategoriesOpen] = useState(false);

  const onSubmit = (data: FilterFormInputs) => {
    console.log(data);
  };

  return (
    <form
      className="flex flex-col gap-4 bg-background p-6 shadow-md rounded-xl border border-border"
      onSubmit={handleSubmit(onSubmit)}
    >
      <h2 className="text-lg font-semibold text-text mb-4">Filter Products</h2>

      {/* Categories Filter */}
      <div
        className="flex items-center cursor-pointer mb-1 gap-2"
        onClick={() => setIsCategoriesOpen(!isCategoriesOpen)}
      >
        <span className="text-sm text-text-muted">
          {isCategoriesOpen ? (
            <ChevronUpIcon className="h-3 w-3" />
          ) : (
            <ChevronDownIcon className="h-3 w-3" />
          )}
        </span>
        <label className="block text-sm font-medium text-text">
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
                  className="inline-flex items-center gap-2 text-text-secondary"
                >
                  <input
                    type="checkbox"
                    value={category}
                    checked={field.value?.includes(category) || false}
                    onChange={(e) => {
                      const value = field.value || [];
                      if (e.target.checked) {
                        field.onChange([...value, category]);
                      } else {
                        field.onChange(value.filter((v) => v !== category));
                      }
                    }}
                    className="accent-primary"
                  />
                  {category}
                </label>
              ))}
            </div>
          )}
        />
      )}

      {/* Brands Filter */}
      <div>
        <label className="block text-sm font-medium text-text mb-1">
          Brands
        </label>
        <Controller
          name="brands"
          control={control}
          render={({ field }) => (
            <div className="flex flex-col gap-2">
              {brandOptions.map((brand) => (
                <label
                  key={brand}
                  className="inline-flex items-center gap-2 text-text-secondary"
                >
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
                    className="accent-primary"
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
        <label className="block text-sm font-medium text-text mb-1">
          Price Range
        </label>
        <div className="flex items-center gap-4">
          <Controller
            name="priceRange.min"
            control={control}
            render={({ field }) => (
              <Input {...field} type="number" placeholder="Min" />
            )}
          />
          <span className="text-text-muted">-</span>
          <Controller
            name="priceRange.max"
            control={control}
            render={({ field }) => (
              <Input {...field} type="number" placeholder="Max" />
            )}
          />
        </div>
      </div>

      <Button type="submit" variant="primary" fullWidth>
        Apply Filters
      </Button>
    </form>
  );
}
