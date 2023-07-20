using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specification
{
	public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Products>
	{
		public ProductsWithTypesAndBrandsSpecification()
		{
			AddIncludes(x => x.ProductType);
			AddIncludes(x => x.ProductBrand);
		}

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddIncludes(x => x.ProductType);
            AddIncludes(x => x.ProductBrand);
        }
    }
}

