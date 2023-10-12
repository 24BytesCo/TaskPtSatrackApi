using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tareas.Application.Features.Categories.Commands.CreateCategory;
using Tareas.Application.Features.Categories.Queries.GetCategoriesList;
using Tareas.Application.Features.Categories.Queries.Vms;
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
        [ProducesResponseType(typeof(IReadOnlyList<CategoryVm>), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<CategoryVm>>> GetCategoriesList() 
        {
            return Ok(await _mediator.Send(new GetCategoriesListQuery()));
        }

        [HttpPost("Create", Name = "CreateCategory")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<CategoryVm>> CreateCategory([FromBody] CreateCategoryCommand request)
        {
            return Ok(await _mediator.Send(request));
        }


    }
}
