using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product,ProductForListDto>()
                .ForMember(d=>d.ProductBrand,o=>o.MapFrom(s=>s.ProductBrand.Name))
                .ForMember(d=>d.ProductType,o=>o.MapFrom(s=>s.ProductType.Name))
                .ForMember(d=>d.PictureUrl,o=>o.MapFrom<UrlValueResolver>());
        }
    }
}