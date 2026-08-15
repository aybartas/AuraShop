import Skeleton from "../../../components/ui/Skeleton";

export default function CartSkeleton() {
  return (
    <div className="max-w-6xl mx-auto p-6 grid md:grid-cols-3 gap-8">
      <div className="md:col-span-2 space-y-6">
        <Skeleton className="h-32 w-full" />
        <Skeleton className="h-32 w-full" />
        <Skeleton className="h-48 w-full" />
      </div>

      <div className="p-6 border border-border rounded-lg bg-background shadow-md space-y-6">
        <Skeleton className="h-6 w-1/2" />
        <div className="space-y-4">
          <div className="flex justify-between">
            <Skeleton className="h-4 w-24" />
            <Skeleton className="h-4 w-16" />
          </div>
          <div className="flex justify-between">
            <Skeleton className="h-4 w-24" />
            <Skeleton className="h-4 w-16" />
          </div>
          <div className="flex justify-between">
            <Skeleton className="h-4 w-24" />
            <Skeleton className="h-4 w-16" />
          </div>
          <div className="flex justify-between border-t border-border pt-4">
            <Skeleton className="h-5 w-24" />
            <Skeleton className="h-5 w-16" />
          </div>
        </div>
        <Skeleton className="h-12 w-full" />
      </div>
    </div>
  );
}
