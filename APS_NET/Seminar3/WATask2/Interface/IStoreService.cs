using WATask2.Models.Dto;

namespace WATask2.Interface
{
    public interface IStoreService
    {
        public int AddStore(StoreDto store);
        public IEnumerable<StoreDto> GetStore();
    }
}
