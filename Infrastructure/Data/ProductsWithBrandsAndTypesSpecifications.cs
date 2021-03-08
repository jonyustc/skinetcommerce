using System;
using System.Linq.Expressions;
using Core.Entities;
using Core.Specifications;

namespace Infrastructure.Data
{
    public class ProductsWithBrandsAndTypesSpecifications : Specification<Product>
    {
        public ProductsWithBrandsAndTypesSpecifications()
        {
            AddIncludes(p=>p.ProductBrand);
            AddIncludes(p=>p.ProductType);
        }

        public ProductsWithBrandsAndTypesSpecifications(int id) : base(p=>p.Id == id)
        {
            AddIncludes(p=>p.ProductBrand);
            AddIncludes(p=>p.ProductType);
        }
    }
}