using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tareas.Application.Features.Categories.Commands.CreateCategory;
using Tareas.Application.Features.Categories.Queries.GetCategoriesList;
using Tareas.Application.Features.Categories.Queries.Vms;
using Tareas.Application.Features.Tasks.Commands.CreateTask;
using Tareas.Application.Features.Tasks.Commands.UpdateTask;
using Tareas.Application.Features.Tasks.Queries.GetAllTaskActivesList;
using Tareas.Application.Features.Tasks.Queries.Vms;
using Tareas.Domain;

namespace Tareas.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TaskController : ControllerBase
    {
        private IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("List", Name = "GetAllTaskActives")]
        [ProducesResponseType(typeof(IReadOnlyList<TaskVm>), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<TaskVm>>> GetAllTaskActivesList() 
        {
            return Ok(await _mediator.Send(new GetAllTaskActivesListQuery()));
        }

        [HttpPost("Create", Name = "CreateTask")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<CategoryVm>> CreateTask([FromBody] CreateTaskCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("Update", Name = "UpdateTask")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<CategoryVm>> UpdateTask([FromBody] UpdateTaskCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

    }
}
