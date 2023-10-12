using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tareas.Application.Features.Categories.Commands.CreateCategory;
using Tareas.Application.Features.Categories.Queries.Vms;
using Tareas.Application.Features.Tasks.Commands.CreateTask;
using Tareas.Application.Features.Tasks.Commands.UpdateTask;
using Tareas.Application.Features.Tasks.Queries.Vms;
using Tareas.Domain;

namespace Tareas.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryVm>()
                .ForMember(r => r.CategoryId, x => x.MapFrom(t => t.Id))
                .ForMember(r => r.Name, x => x.MapFrom(t => t.Name))
                .ForMember(r => r.Status, x => x.MapFrom(t => t.Status))
                .ReverseMap();

            CreateMap<ScheduledTask, TaskVm>()
                .ForMember(r => r.TaskId, x => x.MapFrom(t => t.Id))
                .ForMember(r => r.Title, x => x.MapFrom(t => t.Title))
                .ForMember(r => r.Description, x => x.MapFrom(t => t.Description))
                .ForMember(r => r.Deadline, x => x.MapFrom(t => t.Deadline))
                .ForMember(r => r.CreatedDate, x => x.MapFrom(t => t.CreatedDate))
                .ForMember(r => r.LastUpdateDate, x => x.MapFrom(t => t.LastUpdateDate))
                .ForMember(r => r.Category, x => x.MapFrom(t => t.Category))
                .ForMember(r => r.Status, x => x.MapFrom(t => t.Status));

            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<CreateTaskCommand, ScheduledTask>();
            CreateMap<UpdateTaskCommand, ScheduledTask>();
        }
    }
}
