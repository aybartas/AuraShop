const CheckoutSkeleton = () => {
  return (
    <form>
      <div className="max-w-6xl mx-auto p-6 grid md:grid-cols-3 gap-8 animate-pulse">
        {/* Left column */}
        <div className="md:col-span-2 space-y-6">
          <div className="h-32 bg-gray-200 rounded-md" />{" "}
          {/* AddressSelection */}
          <div className="h-32 bg-gray-200 rounded-md" />{" "}
          {/* ShippingOptions */}
          <div className="h-48 bg-gray-200 rounded-md" /> {/* PaymentForm */}
        </div>

        {/* Right column: Order Summary */}
        <div className="p-6 border rounded-lg bg-white shadow-md space-y-6">
          <div className="h-6 w-1/2 bg-gray-300 rounded-md" /> {/* Heading */}
          <div className="space-y-4 text-base text-gray-700">
            <div className="flex justify-between">
              <div className="h-4 w-24 bg-gray-200 rounded-md" />
              <div className="h-4 w-16 bg-gray-200 rounded-md" />
            </div>
            <div className="flex justify-between">
              <div className="h-4 w-24 bg-gray-200 rounded-md" />
              <div className="h-4 w-16 bg-gray-200 rounded-md" />
            </div>
            <div className="flex justify-between">
              <div className="h-4 w-24 bg-gray-200 rounded-md" />
              <div className="h-4 w-16 bg-gray-200 rounded-md" />
            </div>
            <div className="flex justify-between border-t pt-4">
              <div className="h-5 w-24 bg-gray-300 rounded-md" />
              <div className="h-5 w-16 bg-gray-300 rounded-md" />
            </div>
          </div>
          <div className="h-12 bg-gray-300 rounded-md" /> {/* Button */}
        </div>
      </div>
    </form>
  );
};

export default CheckoutSkeleton;
