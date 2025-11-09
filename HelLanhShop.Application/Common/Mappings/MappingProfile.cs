using AutoMapper;
using HelLanhShop.Application.Products.DTOs;
using HelLanhShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<Product, CreateProduct>();
            CreateMap<CreateProduct, Product>();
            CreateMap<Product, UpdateProduct>();
            CreateMap<UpdateProduct, Product>();
        }
    }
}
