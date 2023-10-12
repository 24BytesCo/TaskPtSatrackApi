using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tareas.Application.Features.Categories.Queries.Vms;
using Tareas.Application.Persistence;
using Tareas.Domain;

namespace Tareas.Application.Features.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, IReadOnlyList<CategoryVm>>
    {

        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GetCategoriesListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CategoryVm>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Category, object>>>();

            var categoiesDb = await _unitOfWork.Repository<Category>()
                .GetAsync(x => x.Status == true, x => x.OrderBy(r => r.Name), includes, true);

            return _mapper.Map<ReadOnlyCollection<CategoryVm>>(categoiesDb);
        }
    }
}
