using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Helper
{
    public class UrlValueResolver : IValueResolver<Product, ProductForListDto, string>
    {
        private readonly IConfiguration _config;
        public UrlValueResolver(IConfiguration config)
        {
            _config = config;

        }

        public string Resolve(
            Product source,
            ProductForListDto destination,
            string destMember,
            ResolutionContext context)
        {
            return _config["ApiUrl"] + source.PictureUrl;
        }
    }
}