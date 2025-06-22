import http from "../http";

export interface AddBasketItemCommand {
  productId: string;
  productName: string;
  price: number;
  quantity: number;
  imageUrl: string;
}

export interface UpdateBasketItemCommand {
  productId: string;
  quantity: number;
}

export const BasketService = {
  addItemToCart: (data: AddBasketItemCommand) =>
    http.post("/baskets/items", data),
  removeItemFromCart: (data: string) => http.delete(`/baskets/items/${data}`),
  updateCartItem: (data: UpdateBasketItemCommand) =>
    http.put(`/baskets/items/${data.productId}`, data),
  getBasket: () => http.get("/baskets/user"),
};
