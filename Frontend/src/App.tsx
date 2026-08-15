import { Route, Routes } from "react-router-dom";
import Header from "./app/layout/Header";
import Catalog from "./app/features/catalog/Catalog";
import ProductDetails from "./app/features/catalog/ProductDetails";
import Home from "./app/features/home/Home";
import Cart from "./app/features/cart/Cart";
import Checkout from "./app/features/checkout/Checkout";

function App() {
  return (
    <>
      <Header />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="catalog" element={<Catalog />} />
        <Route path="catalog/:id" element={<ProductDetails />} />
        <Route path="cart" element={<Cart />} />
        <Route path="checkout" element={<Checkout />} />
      </Routes>
    </>
  );
}

export default App;
