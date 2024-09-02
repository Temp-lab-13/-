using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using WATaskStoreg.IAbstract;
using WATaskStoreg.Models;
using WATaskStoreg.Models.Context;
using WATaskStoreg.Models.DTO;

namespace WATaskStoreg.Service
{
    public class ServiceStorage : IServiceStorage
    {

        private readonly IMapper mapper;
        private IMemoryCache memoryCache;
        private readonly StoregeContext context;

        public ServiceStorage(IMapper mapper, IMemoryCache memoryCache, StoregeContext context)
        {
            this.mapper = mapper;
            this.memoryCache = memoryCache;
            this.context = context;
        }

        public bool AddPosition(StorageDto product)
        {
            if (!context.Storages.Any(x => x.Name.Equals(product.Name)) && !context.Storages.Any(x => x.productId == product.productId))
            {
                var entity = mapper.Map<Storage>(product);
                context.Storages.Add(entity);
                context.SaveChanges();
                memoryCache.Remove("Storeg");
                return true;
            }
            return false;
        }

        public bool DeletPosition(StorageDto product)
        {
            if (context.Storages.Any(x => x.Name.Equals(product.Name)))                                 // Проверяем, есть ли такая позиция.
            {
                var entity = context.Storages.Where(x => x.Name.Equals(product.Name)).FirstOrDefault(); // Получаем позицию.
                context.Storages.Remove(entity);                                                        // Удаляем.
                context.SaveChanges();                                                                  // Сохраняем изменения.
                memoryCache.Remove("Storeg");                                                           // Удаляем устаревший кэш.
                return true;
            }
            return false;
        }

        public IEnumerable<StorageDto> GetPosition()
        {
            if (memoryCache.TryGetValue("Storeg", out List<StorageDto> categoriesCash))
            {
                return categoriesCash;
            }

            var categorys = context.Storages.Select(x => mapper.Map<StorageDto>(x)).ToList();
            memoryCache.Set("Storeg", categorys, TimeSpan.FromMinutes(30));
            return categorys;
        }
    }
}
