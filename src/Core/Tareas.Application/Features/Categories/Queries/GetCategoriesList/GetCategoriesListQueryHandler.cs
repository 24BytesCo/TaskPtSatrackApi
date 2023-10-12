using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tareas.Application.Persistence;
using Tareas.Domain;

namespace Tareas.Application.Features.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, IReadOnlyList<Category>>
    {

        private IUnitOfWork _unitOfWork;

        public GetCategoriesListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<Category>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Category, object>>>();
            return await _unitOfWork.Repository<Category>().GetAsync(x=> x.Status == true, x=> x.OrderBy(r=> r.Name), includes, true);
        }
    }
}
