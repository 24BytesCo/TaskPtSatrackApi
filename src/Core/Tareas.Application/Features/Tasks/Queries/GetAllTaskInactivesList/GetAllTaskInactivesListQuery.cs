using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tareas.Application.Features.Tasks.Queries.Vms;

namespace Tareas.Application.Features.Tasks.Queries.GetAllTaskInactivesList
{
    public class GetAllTaskInactivesListQuery : IRequest<IReadOnlyList<TaskVm>>
    {
    }
}
