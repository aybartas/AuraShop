import { Product } from "../../types/Product";
import http from "../http";

interface GetProductsRequest {}

interface GetProductsResponse {
  data: Product[];
}

export const CatalogService = {
  getProducts: (data: GetProductsRequest) =>
    http.get<GetProductsResponse>("/products", data),
  getProduct: (id: string) => http.get<Product>(`/products/${id}`),
};
