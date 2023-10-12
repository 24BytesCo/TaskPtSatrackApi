using Newtonsoft.Json;
using System.Net;
using Tareas.Api.Errors;
using Tareas.Application.Exceptions;

namespace Tareas.Api.Middlewares
{
    // Se utiliza como middleware para manejar excepciones en las solicitudes HTTP.
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;  // Representa el próximo middleware en la cadena de middleware.

        private readonly ILogger<ExceptionMiddleware> _logger;  // Proporciona registro de eventos.

        // Constructor de la clase ExceptionMiddleware.
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        // Se invoca para manejar excepciones en las solicitudes HTTP.
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);  // Invoca el siguiente middleware en la cadena.
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);  // Registra la excepción en el registro de eventos.
                context.Response.ContentType = "application/json";
                var statusCode = (int)HttpStatusCode.InternalServerError;
                var result = string.Empty;

                // Se manejan diferentes tipos de excepciones y se establecen códigos de estado y respuestas JSON apropiadas.
                switch (ex)
                {
                    case NotFoundException notFoundException:
                        statusCode = (int)HttpStatusCode.NotFound;  // Cambia el código de estado a 404 si es una NotFoundException.
                        break;

                    case FluentValidation.ValidationException validationException:
                        // Cambia el código de estado a 400 si es una ValidationException.
                        statusCode = (int)HttpStatusCode.BadRequest;

                        // Obtiene los mensajes de error generados por la validación.
                        var errors = validationException.Errors.Select(ers => ers.ErrorMessage).ToArray();

                        // Convierte los mensajes de error en una cadena JSON.
                        var details = JsonConvert.SerializeObject(errors);

                        // Crea una respuesta personalizada para las validaciones fallidas.
                        result = JsonConvert.SerializeObject(
                            new CodeErrorResponseException(statusCode, errors, details)
                        );
                        break;


                    case BadRequestException badRequestException:
                        statusCode = (int)HttpStatusCode.BadRequest;  // Cambia el código de estado a 400 si es una BadRequestException.
                        break;

                    default:
                        statusCode = (int)HttpStatusCode.InternalServerError;  // El código de estado predeterminado es 500 (Internal Server Error).
                        break;
                }

                // Si no se generó una respuesta JSON específica, se crea una respuesta de error genérica.
                if (string.IsNullOrEmpty(result))
                {
                    result = JsonConvert.SerializeObject(
                        new CodeErrorResponseException(statusCode, new string[] { ex.Message }, ex.StackTrace)
                    );
                }

                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsync(result);  // Envía la respuesta JSON al cliente.
            }
        }
    }
}
