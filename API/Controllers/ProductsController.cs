using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interface;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _brandsRepo;
        private readonly IGenericRepository<ProductType> _typesRepo;
        private readonly IMapper _mapper;

        public ProductsController(
            IGenericRepository<Product> productsRepo,
            IGenericRepository<ProductBrand> brandsRepo,
            IGenericRepository<ProductType> typesRepo,
            IMapper mapper)
        {
            _typesRepo = typesRepo;
            _mapper = mapper;
            _brandsRepo = brandsRepo;
            _productsRepo = productsRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
        {
            var spec = new ProductsWithBrandsAndTypesSpecifications();

            var products = await _productsRepo.ListAsync(spec);


            // if we not use automapper
            // var productsDto = products.Select(x=>new ProductForListDto{
            //     Id = x.Id,
            //     Name= x.Name,
            //     Description = x.Description,
            //     PictureUrl = x.PictureUrl,
            //     ProductBrand = x.ProductBrand.Name,
            //     ProductType = x.ProductType.Name
            // }).ToList();



            // using automapper
            var productsDto = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductForListDto>>(products);

            return Ok(productsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var spec = new ProductsWithBrandsAndTypesSpecifications(id);

            var product = await _productsRepo.GetEntityBySpec(spec);

            // var productDto = new ProductForListDto
            // {
            //     Id = product.Id,
            //     Name = product.Name,
            //     Price = product.Price,
            //     Description = product.Description,
            //     PictureUrl = product.PictureUrl,
            //     ProductBrand = product.ProductBrand.Name,
            //     ProductType = product.ProductType.Name
            // };

            var productDto = _mapper.Map<Product,ProductForListDto>(product);

            return Ok(productDto);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var productBrands = await _brandsRepo.ListAllAsync();
            return Ok(productBrands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var productTypes = await _typesRepo.ListAllAsync();
            return Ok(productTypes);
        }
    }
}