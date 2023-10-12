using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tareas.Application.Features.Categories.Queries.GetCategoriesList;
using Tareas.Domain;

namespace Tareas.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController : ControllerBase
    {
        private IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("List", Name = "GetCategoriesList")]
        [ProducesResponseType(typeof(IReadOnlyList<Category>), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetCategoriesList() 
        {
            return Ok(await _mediator.Send(new GetCategoriesListQuery()));
        }


    }
}
