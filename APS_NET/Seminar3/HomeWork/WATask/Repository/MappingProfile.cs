using AutoMapper;
using WATask.Models;
using WATask.Models.DTO;

namespace WATask.Repository
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product, ProductDto>(MemberList.Destination).ReverseMap();
            CreateMap<Category, CategoryDto>(MemberList.Destination).ReverseMap();
            CreateMap<Storage, StorageDto>(MemberList.Destination).ReverseMap();    //  Магазин не стал випиливать из этого сервеса, просто стобы не возиться с переделкой базы данных.
                                                                                    //  По сути, всё что его касается, нигде не используется.
        }

    }
}
