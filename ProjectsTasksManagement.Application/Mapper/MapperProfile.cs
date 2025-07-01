using AutoMapper;
using ProjectsTasksManagement.Application.DTOs.Response;
using ProjectsTasksManagement.Domain.Entities;

namespace ProjectsTasksManagement.Application.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Project, ProjectDTO>()
            .ForMember(d => d.Tasks, s => s.MapFrom(c => c.Tasks));

        CreateMap<Task, TaskDTO>().ReverseMap();
    }
}
