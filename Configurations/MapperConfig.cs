using AutoMapper;
using TravelAPI.Models;
using TravelAPI.DBContexts;
using TravelAPI.Controllers;
using TravelAPI.Services;
using TravelAPI.Dto;

namespace TravelAPI.Configurations
{
    public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<Activites, ActivitesDTO>();
        CreateMap<ActivityType, ActivityTypeDTO>();
        CreateMap<ActivityTypeStats, ActivityTypeStatsDTO>();
        CreateMap<Budget, BudgetDTO>();
        CreateMap<Comments, CommentsDTO>();
        CreateMap<Destination, DestinationDTO>();
        CreateMap<Destination, DestinationWithActivitiesDTO>()
                .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities));
        CreateMap<Destination, DestinationWithVoyagesDTO>()
            .ForMember(dest => dest.Voyages, opt => opt.MapFrom(src => src.Voyage));
        CreateMap<User, UserDTO>();
        CreateMap<User, UserWithVoyagesDTO>()
            .ForMember(dest => dest.Voyages, opt => opt.MapFrom(src => src.Voyage));
        CreateMap<User, UserWithCommentsDTO>()
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments));
        CreateMap<Voyage, VoyageDTO>();
    }
}
}