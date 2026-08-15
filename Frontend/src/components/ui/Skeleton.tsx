interface SkeletonProps {
  className?: string;
  variant?: "text" | "circle" | "rect";
  width?: string;
  height?: string;
}

const variantClasses = {
  text: "rounded h-4",
  circle: "rounded-full",
  rect: "rounded-md",
};

export default function Skeleton({
  className = "",
  variant = "rect",
  width,
  height,
}: SkeletonProps) {
  return (
    <div
      className={`animate-pulse bg-surface-hover ${variantClasses[variant]} ${className}`}
      style={{ width, height }}
    />
  );
}
