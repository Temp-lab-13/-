using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using WATask2.Interface;
using WATask2.Models;
using WATask2.Models.Dto;

namespace WATask2.Services
{
    public class StoreService : IStoreService
    {
        private readonly ProductContext _productContext;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        public StoreService(IMapper mapper, IMemoryCache memoryCache, ProductContext productContext)
        {
            _mapper = mapper;
            _productContext = productContext;
            _memoryCache = memoryCache;
        }
        public int AddStore(StoreDto store)
        {
            using (_productContext)
            {
                var ent = _mapper.Map<Storage>(store);
                _productContext.Storages.Add(ent);
                _productContext.SaveChanges();
                _memoryCache.Remove("storage");
                return ent.Id;
            }
        }

        public IEnumerable<StoreDto> GetStore()
        {
            using (_productContext)
            {
                if (_memoryCache.TryGetValue("storage", out List<StoreDto> store))
                {
                    return store;
                }
                store = _productContext.Storages.Select(p => _mapper.Map<StoreDto>(p)).ToList();
                _memoryCache.Set("storage", store, TimeSpan.FromMinutes(30));
                return store;
            }
        }
    }
}
