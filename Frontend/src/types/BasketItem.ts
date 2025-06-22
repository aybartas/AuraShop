export interface BasketItem {
  productId: string;
  productName: string;
  imageUrl: string;
  quantity: number;
  price: number;
  discountedPrice?: number;
}
