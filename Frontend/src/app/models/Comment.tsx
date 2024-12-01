export interface Comment {
  user: string;
  rating: number;
  comment: string;
  date: string;
}

export interface Basket {
  userId: string;
  discountCode?: number;
  discountRate?: number;
  totalQuantity?: number;
  totalPrice?: number;
  basketItems?: BasketItem[];
}

export interface BasketItem {
  productId: string;
  productName?: string;
  productImage?: string;
  color: string;
  size: string;
  quantity?: number;
  price?: number;
}
