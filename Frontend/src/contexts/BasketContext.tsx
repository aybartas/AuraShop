import { createContext, ReactNode, useEffect, useState } from "react";
import { Basket } from "../types/Basket";
import { BasketService } from "../api/services/BasketService";

interface BasketContextProps {
  basket: Basket | null;
  refreshBasket: () => void;
  setBasket: React.Dispatch<React.SetStateAction<Basket | null>>;
  loading?: boolean;
}

export const BasketContext = createContext<BasketContextProps | undefined>(
  undefined
);

export const BasketProvider: React.FC<{ children: ReactNode }> = ({
  children,
}) => {
  const [basket, setBasket] = useState<Basket | null>(null);
  const [loading, setLoading] = useState<boolean>(false);

  const refreshBasket = async () => {
    try {
      setLoading(true);
      const response = await BasketService.getBasket();
      const data = response.data;
      setBasket(data);
    } catch (error) {
      console.error("Failed to refresh basket:", error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    refreshBasket();
  }, []);

  return (
    <BasketContext.Provider
      value={{ basket, refreshBasket, setBasket, loading }}
    >
      {children}
    </BasketContext.Provider>
  );
};
