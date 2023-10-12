using System;
using System.Collections.Generic;
using System.Text;

namespace Tareas.Application.Exceptions
{
    // Esta clase NotFoundException se utiliza para manejar excepciones cuando una entidad no se encuentra en la base de datos.
    public class NotFoundException : ApplicationException
    {
        // Constructor de la clase que toma dos parámetros: 'nombre' y 'key'.
        // 'nombre' se refiere al nombre de la entidad que no se encontró, y 'key' es la clave o identificador de la entidad no encontrada.
        public NotFoundException(string nombre, object key) : base($"La entidad {nombre} {key} no ha sido encontrado")
        {
        }
    }
}
