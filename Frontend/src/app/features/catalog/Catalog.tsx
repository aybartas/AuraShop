import PageLayout from "../../layout/PageLayout";
import FilterForm from "./FilterForm";
import ProductList from "../home/ProductList";
import { useEffect, useState } from "react";
import { Product } from "../../../types/Product";
import { CatalogService } from "../../../api/services/CatalogService";

function Catalog() {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    CatalogService.getProducts({})
      .then((res) => setProducts(res.data.data))
      .finally(() => setLoading(false));
  }, []);

  return (
    <PageLayout>
      <div className="grid grid-cols-4 gap-4">
        <div>
          <FilterForm />
        </div>
        <div className="col-span-3">
          <ProductList products={products} loading={loading} />
        </div>
      </div>
    </PageLayout>
  );
}

export default Catalog;
