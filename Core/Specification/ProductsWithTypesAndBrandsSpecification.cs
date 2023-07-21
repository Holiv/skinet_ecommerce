﻿using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specification
{
	public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Products>
	{
		public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams) 
			: base(x => 
			(!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
			(!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
			)
		{
			AddIncludes(x => x.ProductType);
			AddIncludes(x => x.ProductBrand);
			AddOrderBy(x => x.Name);
			ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

			if (!string.IsNullOrEmpty(productParams.Sort))
			{
				switch (productParams.Sort) 
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

