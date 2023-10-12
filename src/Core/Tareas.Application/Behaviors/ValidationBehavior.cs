using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tareas.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        // Este método se ejecutará antes de la manipulación de la solicitud por el controlador.
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                // Crea un contexto de validación para la solicitud actual.
                var context = new ValidationContext<TRequest>(request);

                // Ejecuta todas las validaciones en paralelo.
                var validationResult = await Task.WhenAll(_validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

                // Obtiene todas las fallas de las validaciones.
                var failures = validationResult.SelectMany(result => result.Errors).Where(error => error != null).ToList();

                // Si hay fallas en la validación, lanza una excepción de validación.
                if (failures.Any())
                {
                    throw new ValidationException(failures);
                }
            }

            // Continúa con el siguiente manejador en la cadena de responsabilidad.
            return await next();
        }
    }
}
