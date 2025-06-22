import { createContext, ReactNode, useEffect, useState } from "react";
import { Basket } from "../types/Basket";
import { BasketService } from "../api/services/BasketService";

interface BasketContextProps {
  basket: Basket | null;
  refreshBasket: () => void;
}

export const BasketContext = createContext<BasketContextProps | undefined>(
  undefined
);

export const BasketProvider: React.FC<{ children: ReactNode }> = ({
  children,
}) => {
  const [basket, setBasket] = useState<Basket | null>(null);

  const refreshBasket = async () => {
    try {
      const response = await BasketService.getBasket();
      const data = response.data;
      setBasket(data);
    } catch (error) {
      console.error("Failed to refresh basket:", error);
    }
  };

  useEffect(() => {
    refreshBasket();
  }, []);

  return (
    <BasketContext.Provider value={{ basket, refreshBasket }}>
      {children}
    </BasketContext.Provider>
  );
};
