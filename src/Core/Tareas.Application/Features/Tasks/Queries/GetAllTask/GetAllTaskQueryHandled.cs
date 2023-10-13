using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tareas.Application.Features.Tasks.Queries.Vms;
using Tareas.Application.Persistence;
using Tareas.Domain;

namespace Tareas.Application.Features.Tasks.Queries.GetAllTask
{
    public class GetAllTaskQueryHandled : IRequestHandler<GetAllTaskQuery, IReadOnlyList<TaskVm>>
    {

        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GetAllTaskQueryHandled(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<TaskVm>> Handle(GetAllTaskQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<ScheduledTask, object>>>();

            includes.Add(r => r.Category!);

            var tareasBd = await _unitOfWork.Repository<ScheduledTask>()
                .GetAsync(null, x => x.OrderBy(r => r.Deadline), includes, true);

            return _mapper.Map<ReadOnlyCollection<TaskVm>>(tareasBd);
        }
    }
}
