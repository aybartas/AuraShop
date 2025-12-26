import { BasketItem } from "./BasketItem";

export interface Basket {
  basketItems: BasketItem[];
  coupon?: string | null;
  discountRate?: number | null;
  shippingAmount: number;
  subtotal: number;
  totalPrice: number;
}
