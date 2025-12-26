import http from "../http";

type CreateOrderRequest = {};
type CreateOrderResponse = {};

export const OrderService = {
  createOrder: (request: CreateOrderRequest) =>
    http.post<CreateOrderResponse>("/orders", request),
};
