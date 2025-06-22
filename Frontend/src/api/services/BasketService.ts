import http from "../http";

export interface AddBasketItemCommand {
  productId: string;
  productName: string;
  price: number;
  quantity: number;
  imageUrl: string;
}

export const BasketService = {
  addItemToCart: (data: AddBasketItemCommand) =>
    http.post("/baskets/item", data),
  getBasket: () => http.get("/baskets/user"),
};
