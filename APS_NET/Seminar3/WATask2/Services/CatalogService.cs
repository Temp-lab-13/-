using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using WATask2.Interface;
using WATask2.Models;
using WATask2.Models.Dto;

namespace WATask2.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly ProductContext _productContext;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        public CatalogService(IMapper mapper, IMemoryCache memoryCache, ProductContext productContext)
        {
            _mapper = mapper;
            _productContext = productContext;
            _memoryCache = memoryCache;
        }
        public int AddCatalog(CatalogDto catalog)
        {
            using (_productContext)
            {
                var ent = _mapper.Map<Category>(catalog);
                _productContext.Categories.Add(ent);
                _productContext.SaveChanges();
                _memoryCache.Remove("cataloges");
                return ent.Id;
            }
        }

        public IEnumerable<CatalogDto> GetCatalog()
        {
            using (var _productContext = new ProductContext())
            {
                if (_memoryCache.TryGetValue("cataloges", out List<CatalogDto> category))
                {
                    return category;
                }
                category = _productContext.Categories.Select(p => _mapper.Map<CatalogDto>(p)).ToList();
                _memoryCache.Set("cataloges", category, TimeSpan.FromMinutes(30));
                return category;
            }
        }
    }
}
