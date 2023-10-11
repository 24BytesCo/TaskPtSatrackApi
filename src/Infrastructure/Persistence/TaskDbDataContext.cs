using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tareas.Domain;

namespace Tareas.Infrastructure.Persistence
{
    public class TaskDbDataContext
    {
        // Este método se encarga de cargar datos iniciales en la base de datos, como categorías.
        public static async Task LoadDataAsync(
            TaskDbContext context,
            ILoggerFactory loggerFactory
        )
        {
            try
            {
                // Verifica si la tabla de categorías está vacía antes de cargar datos.
                if (context.Categories != null && !context.Categories.Any())
                {
                    // Crea instancias de categorías.
                    Category categoryEstudio = new Category() { Name = "Estudio" };
                    Category categoryDeporte = new Category() { Name = "Deporte" };
                    Category categoryOcio = new Category() { Name = "Ocio" };

                    // Agrega las categorías al contexto y las guarda en la base de datos.
                    await context.AddRangeAsync(categoryDeporte, categoryEstudio, categoryOcio);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Si se produce una excepción, registra el error en el sistema de registro.
                var logger = loggerFactory.CreateLogger<TaskDbContext>();
                logger.LogError(ex.Message);
            }
        }
    }
}
