using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System.Text.RegularExpressions;
using WATask.Models;
using WATask.Models.Abstract;
using WATask.Models.Dto;

namespace WATask.Repo
{
    public class ProductRepo : IProductRepo
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public ProductRepo(IMapper mapper, IMemoryCache cache) 
        {
            _mapper = mapper;
            _cache = cache;
        }
        public int AddCatalog(CatalogDto catalogDto)
        {
            
            using (var context = new ProductContext())
            {
                var enCatalog = context.Categories.FirstOrDefault(x => x.Name.ToLower() == catalogDto.Name.ToLower());
                if (enCatalog == null)
                {
                    enCatalog = _mapper.Map<Category>(catalogDto);
                    context.Categories.Add(enCatalog);
                    context.SaveChanges();
                    _cache.Remove("categories");
                }
                return enCatalog.Id;
            }
        }

       

        public int AddProduct(ProductDto product)
        {
            using (var context = new ProductContext())
            {
                var enProduct = context.Products.FirstOrDefault(x => x.Name.ToLower() == product.Name.ToLower());
                if (enProduct == null)
                {
                    enProduct = _mapper.Map<Product>(product);
                    context.Products.Add(enProduct);
                    context.SaveChanges();
                    _cache.Remove("products");
                }
                return enProduct.Id;
            }
        }

        public IEnumerable<CatalogDto> GetCategories()
        {
            if (_cache.TryGetValue("categories", out List<CatalogDto> list))
            {
                return list;
            } else 
            {
                using (var context = new ProductContext())
                {
                    var list2 = context.Categories.Select(x => _mapper.Map<CatalogDto>(x)).ToList();
                    _cache.Set("categories", list2, TimeSpan.FromMinutes(30));
                    return list2;
                }
            }
            
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            if (_cache.TryGetValue("products", out List<ProductDto> list))
            {
                return list;
            }
            else
            { 
                using (var context = new ProductContext())
                {
                    var list2 = context.Categories.Select(x => _mapper.Map<ProductDto>(x)).ToList();
                    _cache.Set("products", list2, TimeSpan.FromMinutes(30));
                    return list2;
                }
            }
        }
    }
}
