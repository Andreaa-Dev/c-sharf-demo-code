using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using user.src.Entity;
using static user.src.DTO.CategoryDTO;
using static user.src.DTO.OrderDetailDTO;
using static user.src.DTO.OrderDTO;
using static user.src.DTO.ProductDTO;
using static user.src.DTO.UserDTO;

namespace user.src.Utils
{
  public class MapperProfile : Profile
  {
    public MapperProfile()
    {
      CreateMap<Category, CategoryReadDto>();
      CreateMap<Category, CategoryReadDtoPro>();

      CreateMap<CategoryCreateDto, Category>();
      CreateMap<CategoryUpdateDto, Category>()
      .ForAllMembers(opts => opts.Condition((src, dest, srcProperty) => srcProperty != null));

      CreateMap<User, UserReadDto>();
      CreateMap<UserCreateDto, User>();
      CreateMap<UserUpdateDto, User>()
    .ForAllMembers(opts => opts.Condition((src, dest, srcProperty) => srcProperty != null));

      // only non-null values will be mapped
      // add other logic after 

      CreateMap<Product, ProductReadDto>();
      CreateMap<ProductCreateDto, Product>();
      CreateMap<ProductUpdateDto, Product>()
                 .ForAllMembers(opts => opts.Condition((src, dest, srcProperty) => srcProperty != null));

      // OrderDetail mappings
      CreateMap<OrderDetail, OrderDetailReadDto>();
      CreateMap<OrderDetailCreateDto, OrderDetail>();

      // Order mappings
      CreateMap<Order, OrderReadDto>();
      CreateMap<OrderCreateDto, Order>()
          .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));
    }
  }
}