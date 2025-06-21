export interface ProductColor {
  name: string;
  hexCode: string;
}

export interface Product {
  id: string;
  name: string;
  price: number;
  description: string;
  category: string;
  images: string[];
  colors: ProductColor[];
  sizes: string[];
}
