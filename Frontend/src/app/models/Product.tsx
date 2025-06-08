import { Comment } from "./Comment";

export interface Product {
  id: number;
  name: string;
  colors: string[];
  sizes: string[];
  description: string;
  price: string;
  discount: string;
  image: string;
  rating?: number;
  comments?: Comment[];
}
