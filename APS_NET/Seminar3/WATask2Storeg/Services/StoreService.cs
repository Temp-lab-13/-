using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using WATask2Storeg.Interface;
using WATask2Storeg.Models;
using WATask2Storeg.Models.Dto;
using WATask2Storeg.WebClient;

namespace WATask2Storeg.Services
{
    public class StoreService : IStoreService
    {
        private readonly StoregContext _productContext;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        public StoreService(IMapper mapper, IMemoryCache memoryCache, StoregContext productContext)
        {
            _mapper = mapper;
            _productContext = productContext;
            _memoryCache = memoryCache;
        }
        public int AddPosition(StoreDto store)
        {
            using (_productContext)
            {
                var exist = new StoregClient().ExistProduct(store.productId);
                bool c = exist.Result;
                if (c)
                {
                    var ent = _mapper.Map<Storage>(store);
                    _productContext.Storages.Add(ent);
                    _productContext.SaveChanges();
                    _memoryCache.Remove("storage");
                    return ent.Id;
                }
                return 0;
            }
        }

        public IEnumerable<StoreDto> GetPosition()
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
