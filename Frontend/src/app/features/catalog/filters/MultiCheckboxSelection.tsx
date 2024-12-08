import { ChevronDownIcon, ChevronUpIcon } from "@heroicons/react/24/outline";
import { useState } from "react";
import { Control, Controller, FieldValues, Path } from "react-hook-form";

interface Props<T extends FieldValues> {
  options: string[];
  label: string;
  name: Path<T>;
  control: Control<T>;
}

function MultiCheckboxSelection<T extends FieldValues>({
  options,
  label,
  name,
  control,
}: Props<T>) {
  const [isOpen, setIsOpen] = useState(true);

  return (
    <div>
      <div
        className="flex items-center cursor-pointer mb-1 gap-2"
        onClick={() => setIsOpen((prev) => !prev)}
      >
        <span className="text-sm text-gray-500">
          {isOpen ? (
            <ChevronUpIcon className="h-3 w-3 text-gray-500" />
          ) : (
            <ChevronDownIcon className="h-3 w-3 text-gray-500" />
          )}
        </span>
        <label className="block text-sm font-medium text-gray-700">
          {label}
        </label>
      </div>

      {isOpen && (
        <Controller
          name={name}
          control={control}
          render={({ field }) => (
            <div className="flex flex-col gap-2">
              {options.map((option) => (
                <label key={option} className="inline-flex items-center gap-2">
                  <input
                    type="checkbox"
                    value={option}
                    className="accent-orange-400"
                    checked={field.value?.includes(option) || false}
                    onChange={(e) => {
                      const value = field.value || [];
                      if (e.target.checked) {
                        // Add the selected option to the array
                        field.onChange([...value, option]);
                      } else {
                        // Remove the unchecked option from the array
                        field.onChange(
                          value.filter((v: string) => v !== option)
                        );
                      }
                    }}
                  />
                  {option}
                </label>
              ))}
            </div>
          )}
        />
      )}
    </div>
  );
}

export default MultiCheckboxSelection;
