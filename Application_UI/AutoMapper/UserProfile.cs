using AutoMapper;
using BusinessLogic.ViewModels;
using DataAccess.Task;

namespace Application_UI.AutoMapper
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<Tasks, TaskViewModel>().ReverseMap();
        }
    }
}
