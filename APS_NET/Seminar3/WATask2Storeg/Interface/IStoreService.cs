using WATask2Storeg.Models.Dto;

namespace WATask2Storeg.Interface
{
    public interface IStoreService
    {
        int AddPosition(StoreDto store);
        IEnumerable<StoreDto> GetPosition();
    }
}
