using System;
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
	}
}

