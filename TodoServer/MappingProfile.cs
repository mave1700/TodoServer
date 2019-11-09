using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace TodoServer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<Task, TaskDto>();

            CreateMap<UserForCreationDto, User>();

            CreateMap<UserForUpdateDto, User>();

            CreateMap<TaskForCreationDto, Task>();

            CreateMap<TaskForUpdateDto, Task>();

        }
    }
}
