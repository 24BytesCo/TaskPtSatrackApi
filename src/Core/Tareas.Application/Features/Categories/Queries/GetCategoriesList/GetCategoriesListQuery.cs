using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tareas.Domain;

namespace Tareas.Application.Features.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQuery: IRequest<IReadOnlyList<Category>>
    {
    }
}
