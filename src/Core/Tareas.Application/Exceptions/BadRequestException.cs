using System;
using System.Collections.Generic;
using System.Text;

namespace Tareas.Application.Exceptions
{
    // Esta clase se utiliza para manejar excepciones relacionadas con solicitudes incorrectas.
    public class BadRequestException : ApplicationException
    {
        // Constructor de la clase que toma un parámetro 'mensaje'.
        // 'mensaje' representa un mensaje descriptivo que se muestra al lanzar la excepción.
        public BadRequestException(string mensaje) : base(mensaje)
        {
        }
    }
}

