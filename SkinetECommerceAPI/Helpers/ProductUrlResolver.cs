using System;
using AutoMapper;
using Core.Entities;
using SkinetECommerceAPI.DTOs;

namespace SkinetECommerceAPI.Helpers
{
	public class ProductUrlResolver : IValueResolver<Products, ProductToReturnDto, string>
	{
        private readonly IConfiguration config;

        public ProductUrlResolver(IConfiguration config)
        {
            this.config = config;
        }

        public string Resolve(Products source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return config["ApiUrl"] + source.PictureUrl;
            }

            return null;
        }
    }
}

