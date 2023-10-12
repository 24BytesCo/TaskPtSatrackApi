using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tareas.Application.Features.Categories.Queries.Vms;
using Tareas.Application.Features.Tasks.Queries.Vms;

namespace Tareas.Application.Features.Tasks.Commands.CreateTask
{
    public class CreateTaskCommand : IRequest<TaskVm>
    {
        public string? Title { get; set; }
        
        public string? Description { get; set; }

        public DateTime Deadline { get; set; }

        public Guid CategoryId { get; set; }

    }
}
