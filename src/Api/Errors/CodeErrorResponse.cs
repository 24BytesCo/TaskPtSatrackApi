using Newtonsoft.Json;

namespace Tareas.Api.Errors
{
    // La clase se utiliza para representar respuestas de error personalizadas en formato JSON.
    public class CodeErrorResponse
    {
        [JsonProperty("statuscode")]
        public int Statuscode { get; set; }  // Representa el código de estado HTTP para la respuesta.

        [JsonProperty("message")]
        public string[]? Message { get; set; }  // Representa un array de mensajes de error.

        public CodeErrorResponse(int statusCode, string[]? message = null)
        {
            Statuscode = statusCode;

            if (message is null)
            {
                Message = new string[0];  // Inicializa el array de mensajes de error como vacío.

                var text = GetDefaultMessageStatusCode(statusCode);  // Obtiene un mensaje predeterminado según el código de estado.

                Message[0] = text;  // Asigna el mensaje predeterminado al array de mensajes.
            }
            else
            {
                Message = message;  // Utiliza los mensajes proporcionados en lugar de los predeterminados.
            }
        }

        // El método se utiliza para obtener un mensaje predeterminado basado en el código de estado.
        private static string GetDefaultMessageStatusCode(int statusCode)
        {
            // Se utiliza una expresión switch para asignar un mensaje predeterminado según el código de estado.
            return statusCode switch
            {
                400 => "El Request enviado tiene errores",  // Mensaje para el código de estado 400 (Bad Request).
                404 => "No se encontró el recurso solicitado",  // Mensaje para el código de estado 404 (Not Found).
                500 => "Se produjeron errores en el servidor, revisar logs",  // Mensaje para el código de estado 500 (Internal Server Error).
                _ => string.Empty  // Un valor predeterminado de cadena vacía para otros códigos de estado.
            };
        }
    }
}
