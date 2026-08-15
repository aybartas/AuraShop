import { ReactNode } from "react";

interface LayoutProps {
  children: ReactNode;
}

export default function PageLayout({ children }: LayoutProps) {
  return (
    <div className="min-h-screen flex flex-col">
      <main className="flex-1 bg-surface">
        <div className="container mx-auto py-8">{children}</div>
      </main>

      <footer className="bg-gray-800 text-white py-4">
        <div className="container mx-auto text-center">
          <p>&copy; 2025 AuraShop. All Rights Reserved.</p>
        </div>
      </footer>
    </div>
  );
}
