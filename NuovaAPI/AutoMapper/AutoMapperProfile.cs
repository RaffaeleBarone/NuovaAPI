using AutoMapper;
using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;

namespace NuovaAPI.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ClienteDTO, Cliente>(); 
            //CreateMap<ClienteDTO, Cliente>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Cognome));    MAPPATURA ATTRIBUTI CON NOME DIVERSO, COMMENTATO PERCHÈ ORA NON SERVE
            CreateMap<OrdineProdottoDTO, OrdineProdotto>();
        }
    }
}
