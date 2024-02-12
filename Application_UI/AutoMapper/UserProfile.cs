using AutoMapper;
using BusinessLogic.ViewModels;
using DataAccess.Task;

namespace Application_UI.AutoMapper
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<Tasks, TaskViewModel>()
                .ForMember(destination => destination.PriorityLevel, h => h.MapFrom(source => source.PriorityLevel))
                .ForMember(destination => destination.StatusTask, h => h.MapFrom(source => source.StatusTask))
                .ForMember(destination => destination.Date, h => h.MapFrom(source => source.Date))
                .ForMember(destination => destination.Description, h => h.MapFrom(source => source.Description))
                .ForMember(destination => destination.Title, h => h.MapFrom(source => source.Title))
                //.ForMember(destination => destination.userId, h => h.MapFrom(source => source.userId))
                .ReverseMap(); ;
        }
    }
}
