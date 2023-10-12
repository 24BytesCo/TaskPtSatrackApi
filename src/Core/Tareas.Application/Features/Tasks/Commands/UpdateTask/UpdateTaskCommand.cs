using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tareas.Application.Features.Tasks.Queries.Vms;

namespace Tareas.Application.Features.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommand : IRequest<TaskVm>
    {
        public Guid TaskId { get; set; }
        
        public DateTime Deadline { get; set; }

        public bool Status { get; set; }
    }
}
