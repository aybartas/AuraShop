import { AxiosResponse } from "axios";
import http from "../http";

type CreatePaymentIntentResponse = {
  clientSecret: string;
};
type GetPaymentIntentResponse = {
  id: string;
  status: string;
};

export const PaymentService: {
  createPaymentIntent: () => Promise<
    AxiosResponse<CreatePaymentIntentResponse>
  >;
  getPaymentIntent: (
    id: string
  ) => Promise<AxiosResponse<GetPaymentIntentResponse>>;
} = {
  createPaymentIntent: () =>
    http.post<CreatePaymentIntentResponse>("/payments/payment-intents", {}),
  getPaymentIntent: (id: string) =>
    http.get<GetPaymentIntentResponse>(`/payments/payment-intents/${id}`),
};
