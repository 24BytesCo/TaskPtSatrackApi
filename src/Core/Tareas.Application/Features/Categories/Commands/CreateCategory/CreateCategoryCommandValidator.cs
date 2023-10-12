using FluentValidation;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tareas.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("El nombre de la categoría es obligatorio")
                .MaximumLength(50).WithMessage("El nombre no puede tener más de 50 caracteres");

        }
    }
}
