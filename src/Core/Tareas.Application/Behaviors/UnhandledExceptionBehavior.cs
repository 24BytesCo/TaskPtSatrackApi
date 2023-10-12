using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tareas.Application.Behaviors
{
    // La clase UnhandledExceptionBehavior es un comportamiento para gestionar excepciones no manejadas
    // que pueden ocurrir durante el procesamiento de solicitudes de la aplicación.
    public class UnhandledExceptionBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                // Procesa la solicitud y pasa al siguiente comportamiento.
                return await next();
            }
            catch (Exception ex)
            {
                // Registra la excepción no manejada en el sistema de registro.
                var requestName = typeof(TRequest).Name;
                _logger.LogError(ex, "Application Request: Se produjo una excepción para la solicitud {Name} {@Request}", requestName, request);

                // Lanza una nueva excepción para indicar que la solicitud de la aplicación tiene errores.
                throw new Exception("Application Request con errores");
            }
        }
    }
}
