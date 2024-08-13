using AutoMapper;
using PochtaServers.Models.EssenceModel;
using PochtaServers.Models.EssenceModel.Dto;

namespace PochtaServers.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<ClientDto, Client>().ReverseMap();
            CreateMap<MessageDto, Message>().ReverseMap();
        }
    }
}
