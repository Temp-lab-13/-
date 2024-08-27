using AutoMapper;
using WATask2Storeg.Models;
using WATask2Storeg.Models.Dto;

namespace WATask2Storeg.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Storage, StoreDto>(MemberList.Destination).ReverseMap();
        }

    }
}
