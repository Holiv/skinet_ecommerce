using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specification
{
	public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Products>
	{
		public ProductsWithTypesAndBrandsSpecification(string sort)
		{
			AddIncludes(x => x.ProductType);
			AddIncludes(x => x.ProductBrand);
			//AddOrderBy(x => x.Name);

			if(!string.IsNullOrEmpty(sort))
			{
				switch (sort) 
				{
					case "priceAsc":
						AddOrderBy(p => p.Price);
						break;
					case "priceDesc":
						AddOrderByDescending(p => p.Price);
						break;
					default:
						AddOrderBy(n => n.Name);
						break;
				}
			}
		}

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddIncludes(x => x.ProductType);
            AddIncludes(x => x.ProductBrand);
        }
    }
}

