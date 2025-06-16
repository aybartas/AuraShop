import React, { ReactNode } from "react";
import "tailwindcss/tailwind.css";

interface LayoutProps {
  children: ReactNode;
}

const PageLayout: React.FC<LayoutProps> = ({ children }) => {
  return (
    <div className="min-h-screen flex flex-col">
      {/* Main content */}
      <main className="flex-1 bg-gray-50">
        <div className="container mx-auto py-8">{children}</div>
      </main>

      {/* Footer (optional) */}
      <footer className="bg-gray-800 text-white py-4">
        <div className="container mx-auto text-center">
          <p>&copy; 2025 AuraShop. All Rights Reserved.</p>
        </div>
      </footer>
    </div>
  );
};

export default PageLayout;
