using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using WATask2.Interface;
using WATask2.Models;
using WATask2.Models.Dto;

namespace WATask2.Services
{
    public class ProductService : IProductService
    {

        private readonly ProductContext _productContext;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        public ProductService(IMapper mapper, IMemoryCache memoryCache, ProductContext productContext) 
        { 
            _mapper = mapper;
            _productContext = productContext;
            _memoryCache = memoryCache;
        } 
       
        public int AddProduct(ProductDto product)
        {
            using (_productContext)
            {
                var ent = _mapper.Map<Product>(product);
                _productContext.Products.Add(ent);
                _productContext.SaveChanges();
                _memoryCache.Remove("products");
                return ent.Id;
            }
        }

        public bool ChekProduct(int productId)
        {
            using (_productContext) 
            {
                if(_memoryCache.TryGetValue("products", out List<ProductDto> producs))
                {
                    return true;
                };
                bool result = _productContext.Products.Any(x => x.Id == productId);
                return result;
            }
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            using (_productContext)
            {
                if(_memoryCache.TryGetValue("products", out List<ProductDto> producs))
                {
                    return producs;
                }
                producs = _productContext.Products.Select(p => _mapper.Map<ProductDto>(p)).ToList();
                _memoryCache.Set("products", producs, TimeSpan.FromMinutes(30));   
                return producs;
            }
        }
    }
}
