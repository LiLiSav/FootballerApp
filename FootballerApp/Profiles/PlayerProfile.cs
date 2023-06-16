using AutoMapper;
using FootballerApp.Models;
using FootballerApp.Models.DTOs.Incoming;
using FootballerApp.Models.DTOs.Outcoming;

namespace FootballerApp.Profiles
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile() 
        {
            // Source first then mapped to the destination
            // generate the values automatically and don't expect it to come from the createPlayer
            // can manipulate any of the data here, e.g. ToUpper(), ToLower(), ToString(), StringConcat, etc.
            CreateMap<PlayerCreationDto, Player>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.DateAdded, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.DateUpdated, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName.ToUpper()))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName.ToUpper()))
                .ForMember(dest => dest.AllIrelands, opt => opt.MapFrom(src => src.AllIrelands))
                .ForMember(dest => dest.PlayerNumber, opt => opt.MapFrom(src => src.Number));

            CreateMap<Player, PlayerDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NumberFullName, opt => opt.MapFrom(src => $"{src.PlayerNumber} - {src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.AllIrelands, opt => opt.MapFrom(src => src.AllIrelands));

        }
    }
}