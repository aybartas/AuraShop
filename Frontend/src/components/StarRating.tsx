interface StarRatingProps {
  rating: number;
  maxStars?: number;
  size?: "sm" | "md" | "lg";
  showValue?: boolean;
}

const sizeClasses = {
  sm: "text-sm",
  md: "text-xl",
  lg: "text-2xl",
};

export default function StarRating({
  rating,
  maxStars = 5,
  size = "md",
  showValue = false,
}: StarRatingProps) {
  const filled = Math.floor(rating);
  const empty = maxStars - filled;

  return (
    <div className="inline-flex items-center gap-1">
      <span className={`text-accent ${sizeClasses[size]}`}>
        {"★".repeat(filled)}
      </span>
      <span className={`text-text-muted ${sizeClasses[size]}`}>
        {"☆".repeat(empty)}
      </span>
      {showValue && (
        <span className="text-sm text-text-secondary ml-1">
          ({rating} / {maxStars})
        </span>
      )}
    </div>
  );
}
