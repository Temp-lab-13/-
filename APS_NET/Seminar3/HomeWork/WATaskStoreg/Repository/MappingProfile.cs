using AutoMapper;
using WATaskStoreg.Models;
using WATaskStoreg.Models.DTO;

namespace WATaskStoreg.Repository
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Storage, StorageDto>(MemberList.Destination).ReverseMap();
        }
    }
}
