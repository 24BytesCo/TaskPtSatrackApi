using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tareas.Application.Features.Categories.Queries.GetCategoriesList;
using Tareas.Application.Features.Categories.Queries.Vms;
using Tareas.Application.Features.Tasks.Queries.Vms;
using Tareas.Application.Persistence;
using Tareas.Domain;

namespace Tareas.Application.Features.Tasks.Queries.GetAllTaskActivesList
{
    public class GetAllTaskActivesListQueryHandler : IRequestHandler<GetAllTaskActivesListQuery, IReadOnlyList<TaskVm>>
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GetAllTaskActivesListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<TaskVm>> Handle(GetAllTaskActivesListQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<ScheduledTask, object>>>();

            includes.Add(r => r.Category!);

            var tareasBd = await _unitOfWork.Repository<ScheduledTask>()
                .GetAsync(x => x.Status == true, x => x.OrderBy(r => r.Title), includes, true);

            return _mapper.Map<ReadOnlyCollection<TaskVm>>(tareasBd);
        }
    }
}
