import React from "react";
import PageLayout from "../../layout/PageLayout";
import FilterForm from "./FilterForm";
import ProductList from "../home/ProductLİst";

function Catalog() {
  return (
    <PageLayout>
      <div className="container mx-auto">
        <div className="grid grid-cols-4 gap-4">
          <div className="">
            <FilterForm />
          </div>
          <div className="col-span-3">
            <ProductList />
          </div>
        </div>
      </div>
    </PageLayout>
  );
}

export default Catalog;
