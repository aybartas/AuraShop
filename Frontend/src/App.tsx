import { Route, Routes } from "react-router-dom";
import Header from "./app/layout/Header";
import Catalog from "./app/features/catalog/Catalog";
import ProductDetails from "./app/features/catalog/ProductDetails";
import Home from "./app/features/home/Home";
import Login from "./app/features/login/Login";
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
        <Route path="login" element={<Login />} />
        <Route path="cart" element={<Cart />} />
        <Route path="checkout" element={<Checkout />} />

        {/* <Route path="catalog/:id" element={<ProductDetail />} />

        <Route element={<RequireAuth />}>
          <Route path="checkout" element={<CheckOutPage />} />
          <Route path="orders" element={<Orders />} />
          <Route path="orders/:id" element={<OrderDetail />} />
        </Route>

        <Route element={<RequireAdmin />}>
          <Route path="inventory" element={<Inventory />} />
        </Route>
        <Route path="about" element={<AboutPage />} />
        <Route path="basket" element={<BasketPage />} />
        <Route path="login" element={<Login />} />
        <Route path="register" element={<Register />} />
        <Route path="server-error" element={<ServerError />} />
        <Route path="not-found" element={<NotFound />} />
        <Route path="*" element={<Navigate replace to="/not-found" />} /> */}
      </Routes>
    </>
  );
}

export default App;
