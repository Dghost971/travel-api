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
        CreateMap<Activites, ActivitesDTO>().ForMember(dest => dest.ActivityType, opt => opt.MapFrom(src => src.ActivityType)).ReverseMap();
        
        CreateMap<ActivityType, ActivityTypeDTO>().ReverseMap();
        CreateMap<ActivityTypeStats, ActivityTypeStatsDTO>().ReverseMap();
        CreateMap<Budget, BudgetDTO>().ReverseMap();
        CreateMap<Comments, CommentsDTO>().ReverseMap();
        CreateMap<Destination, DestinationDTO>().ReverseMap();
        CreateMap<Destination, DestinationWithActivitiesDTO>()
                .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities)).ReverseMap();
        CreateMap<Destination, DestinationWithVoyagesDTO>()
            .ForMember(dest => dest.Voyages, opt => opt.MapFrom(src => src.Voyage)).ReverseMap();
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<User, UserWithVoyagesDTO>()
            .ForMember(dest => dest.Voyages, opt => opt.MapFrom(src => src.Voyage)).ReverseMap();
        CreateMap<User, UserWithCommentsDTO>()
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments));
        CreateMap<Voyage, VoyageDTO>().ReverseMap();
    }
}
}