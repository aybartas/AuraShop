import { Address } from "../types/Address";

interface AddressCardProps {
  address: Address & { id: string; title: string };
  selected: boolean;
  onSelect: () => void;
}

export default function AddressCard({
  address,
  selected,
  onSelect,
}: AddressCardProps) {
  return (
    <div
      onClick={onSelect}
      className={`p-4 border rounded-lg cursor-pointer min-w-[220px] transition ${
        selected
          ? "border-primary bg-primary-light shadow"
          : "border-border hover:border-text-muted bg-background"
      }`}
    >
      <h5 className="font-medium text-primary">{address.title}</h5>
      <p className="text-sm text-text-secondary">
        {address.street}, {address.city}, {address.state}, {address.zipCode},{" "}
        {address.country}
      </p>
    </div>
  );
}
