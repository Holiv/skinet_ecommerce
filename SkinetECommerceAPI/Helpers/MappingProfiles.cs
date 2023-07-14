using System;
using System.Linq.Expressions;
using AutoMapper;
using Core.Entities;
using SkinetECommerceAPI.DTOs;

namespace SkinetECommerceAPI.Helpers
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<Products, ProductToReturnDto>()
				.ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
				.ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name));

        }
	}
}

