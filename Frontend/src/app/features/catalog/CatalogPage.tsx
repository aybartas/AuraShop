import PageLayout from "../../layout/PageLayout";
import FilterForm from "./FilterForm";
import ProductList from "../home/ProductLİst";

function CatalogPage() {
  return (
    <PageLayout>
      <div className="grid grid-cols-4 gap-4">
        <FilterForm />

        <div className="col-span-3">
          <ProductList />
        </div>
      </div>
    </PageLayout>
  );
}

export default CatalogPage;
