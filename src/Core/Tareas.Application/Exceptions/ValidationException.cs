using FluentValidation.Results; // Importamos el espacio de nombres necesario.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tareas.Application.Exceptions
{
    // Esta clase ValidationException se utiliza para representar excepciones relacionadas con errores de validación.
    public class ValidationException : ApplicationException
    {
        // La propiedad Errors almacena los errores de validación como un diccionario donde la clave es el nombre de la propiedad y el valor es un array de mensajes de error.
        public IDictionary<string, string[]> Errors { get; }

        // Constructor predeterminado que establece un mensaje predeterminado.
        public ValidationException() : base("Se presentaron uno o más errores de validación")
        {
            // Inicializa la propiedad Errors como un nuevo diccionario vacío.
            Errors = new Dictionary<string, string[]>();
        }

        // Constructor sobrecargado que toma una lista de fallos de validación (ValidationFailure) como argumento.
        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            // Transforma la lista de fallos en un diccionario donde las claves son los nombres de las propiedades y los valores son arrays de mensajes de error.
            Errors = failures
                .GroupBy(failure => failure.PropertyName, failure => failure.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }
    }
}
