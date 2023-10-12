using Newtonsoft.Json;

namespace Tareas.Api.Errors
{
    // Se extiende la clase CodeErrorResponse y se utiliza para representar respuestas de error personalizadas en formato JSON que incluyen detalles adicionales.
    public class CodeErrorResponseException : CodeErrorResponse
    {
        [JsonProperty(PropertyName = "details")]
        public string? Details { get; set; }  // Representa detalles técnicos adicionales relacionados con el error.

        public CodeErrorResponseException(int statusCode, string[]? message = null, string? details = null)
            : base(statusCode, message)
        {
            Details = details;  // Asigna los detalles adicionales proporcionados al objeto de respuesta de error.
        }
    }
}
