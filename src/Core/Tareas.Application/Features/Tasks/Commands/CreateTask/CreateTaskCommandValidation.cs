using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tareas.Application.Features.Categories.Commands.CreateCategory;

namespace Tareas.Application.Features.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandValidation : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidation()
        {
            RuleFor(r => r.Title)
                .NotEmpty().WithMessage("El Título de la Tarea es obligatorio")
                .MaximumLength(100).WithMessage("El Título de la Tarea no puede tener más de 100 caracteres");

            RuleFor(r => r.Deadline)
                .NotNull().WithMessage("La fecha limite es obligatoria");
        }
    }
}
