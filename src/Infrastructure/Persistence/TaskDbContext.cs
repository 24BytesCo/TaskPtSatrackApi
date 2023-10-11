using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tareas.Domain;
using Tareas.Domain.Common;

namespace Tareas.Infrastructure.Persistence
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }

        // Este método anula la funcionalidad predeterminada de Entity Framework Core para guardar cambios en la base de datos.
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Recorre todas las entidades rastreadas por el contexto de Entity Framework.
            foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
            {
                switch (entry.State)
                {
                    // Si la entidad es nueva (Added), establece la fecha de creación en la fecha y hora actual.
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;

                    // Si la entidad se está modificando (Modified), actualiza la fecha de última modificación a la fecha y hora actual.
                    case EntityState.Modified:
                        entry.Entity.LastUpdateDate = DateTime.Now;
                        break;

                    default:
                        break;
                }
            }

            // Llama al método SaveChangesAsync de la clase base para guardar los cambios en la base de datos.
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la relación entre ScheduledTask y Category.
            modelBuilder.Entity<Category>()
                .HasMany(category => category.ScheduledTasks)  // Una categoría tiene muchas tareas programadas.
                .WithOne(task => task.Category)  // Una tarea programada pertenece a una categoría.
                .HasForeignKey(task => task.CategoryId)  // Utiliza la propiedad "CategoryId" como clave foránea.

                // Establece la restricción de eliminación.
                // Evita eliminar las tareas asignadas a esa categoría.
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de ScheduledTask.
            modelBuilder.Entity<ScheduledTask>()
                .HasOne(task => task.Category)  // Una tarea programada pertenece a una categoría.
                .WithMany(category => category.ScheduledTasks)  // Una categoría tiene muchas tareas programadas.
                .HasForeignKey(task => task.CategoryId);  // Utiliza la propiedad "CategoryId" como clave foránea.

            // Llamamos al método base para mantener la funcionalidad de Entity Framework.
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ScheduledTask> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
