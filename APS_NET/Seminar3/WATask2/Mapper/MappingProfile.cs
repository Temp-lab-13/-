using AutoMapper;
using WATask2.Models;
using WATask2.Models.Dto;

namespace WATask2.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product, ProductDto>(MemberList.Destination).ReverseMap();
            CreateMap<Category, CatalogDto>(MemberList.Destination).ReverseMap();
            //CreateMap<Storage, StoreDto>(MemberList.Destination).ReverseMap();
        }

    }
}
