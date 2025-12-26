import {
  createContext,
  ReactNode,
  useContext,
  useEffect,
  useState,
} from "react";
import { Basket } from "../types/Basket";
import { BasketService } from "../api/services/BasketService";
import { AuthContext } from "./AuthContext";
import { useAuth } from "../hooks/useAuth";

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
  const { user } = useAuth();

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
  }, [user]);

  return (
    <BasketContext.Provider
      value={{ basket, refreshBasket, setBasket, loading }}
    >
      {children}
    </BasketContext.Provider>
  );
};
