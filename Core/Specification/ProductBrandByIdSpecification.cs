using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specification
{
    public class ProductBrandByIdSpecification : BaseSpecification<ProductBrand>
    {
        public ProductBrandByIdSpecification(int id) : base(x => x.Id == id)
        {
        }
    }
}

