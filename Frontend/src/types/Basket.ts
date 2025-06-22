import { BasketItem } from "./BasketItem";

export interface Basket {
  basketItems: BasketItem[];
  coupon?: string;
  discountRate?: number;
  shippingAmount: number;
  hasDiscount: boolean;
  totalPrice: number;
  totalDiscountedPrice?: number;
}
