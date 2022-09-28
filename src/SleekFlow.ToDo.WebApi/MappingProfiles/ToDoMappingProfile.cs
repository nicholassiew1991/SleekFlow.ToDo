using AutoMapper;
using SleekFlow.ToDo.WebApi.DataAccess.Entities;
using SleekFlow.ToDo.WebApi.Models;

namespace SleekFlow.ToDo.WebApi.MappingProfiles;

public class ToDoMappingProfile : Profile
{
    public ToDoMappingProfile()
    {
        this.CreateMap<ToDoEntity, ToDoModel>()
            .ForMember(x => x.Id, x => x.MapFrom(src => src.Id))
            .ForMember(x => x.Name, x => x.MapFrom(src => src.Name))
            .ForMember(x => x.DueDate, x => x.MapFrom(src => DateTimeOffset.FromUnixTimeMilliseconds(src.DueDate)))
            .ForMember(x => x.Status, x => x.MapFrom(src => src.Status));
    
        this.CreateMap<ToDoModel, ToDoEntity>()
            .ForMember(x => x.Id, x => x.MapFrom(src => src.Id))
            .ForMember(x => x.Name, x => x.MapFrom(src => src.Name))
            .ForMember(x => x.DueDate, x => x.MapFrom(src => src.DueDate.ToUnixTimeMilliseconds()))
            .ForMember(x => x.Status, x => x.MapFrom(src => src.Status));
    }
}