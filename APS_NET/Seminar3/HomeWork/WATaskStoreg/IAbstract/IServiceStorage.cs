using WATaskStoreg.Models.DTO;

namespace WATaskStoreg.IAbstract
{
    public interface IServiceStorage
    {
        bool AddPosition(StorageDto category);
        IEnumerable<StorageDto> GetPosition();
        bool DeletPosition(StorageDto product);
    }
}
