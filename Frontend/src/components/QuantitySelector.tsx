import { MdAdd, MdRemove } from "react-icons/md";
import IconButton from "./ui/IconButton";

interface QuantitySelectorProps {
  value: number;
  onIncrement: () => void;
  onDecrement: () => void;
  min?: number;
  max?: number;
}

export default function QuantitySelector({
  value,
  onIncrement,
  onDecrement,
  min = 1,
  max = 99,
}: QuantitySelectorProps) {
  return (
    <div className="flex items-center gap-2">
      <IconButton
        icon={<MdRemove size={20} />}
        aria-label="Decrease quantity"
        onClick={onDecrement}
        disabled={value <= min}
        className="border border-border"
      />
      <span className="w-8 text-center text-lg font-medium text-text">
        {value}
      </span>
      <IconButton
        icon={<MdAdd size={20} />}
        aria-label="Increase quantity"
        onClick={onIncrement}
        disabled={value >= max}
        className="border border-border"
      />
    </div>
  );
}
