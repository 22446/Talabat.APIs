﻿using AutoMapper;
using Talabat.APIs.DTO;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helper
{
    public class PictureResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public PictureResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _configuration["BaseUrl"] + source.PictureUrl;
            }
            return string.Empty;
        }
    }
}
