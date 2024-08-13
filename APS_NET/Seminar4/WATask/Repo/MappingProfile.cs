using AutoMapper;
using WATask.Models;
using WATask.Models.Dto;

namespace WATask.Repo
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product, ProductDto>(MemberList.Destination).ReverseMap();
            CreateMap<Category, CatalogDto>(MemberList.Destination).ReverseMap();
            CreateMap<Storage, StoreDto>(MemberList.Destination).ReverseMap();
        }

    }
}
