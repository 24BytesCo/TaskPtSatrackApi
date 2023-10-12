using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tareas.Application.Features.Tasks.Commands.CreateTask;

namespace Tareas.Application.Features.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommandValidation : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidation() 
        {
            RuleFor(r => r.Status)
                .NotNull().WithMessage("Debes enviar el estado de la tarea");

            RuleFor(r => r.TaskId)
                 .NotNull().WithMessage("Debes enviar el id de la tarea");

        }
    }
}
