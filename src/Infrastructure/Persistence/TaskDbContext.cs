using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tareas.Domain;

namespace Tareas.Infrastructure.Persistence
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
            // Constructor que recibe opciones de DbContext.
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
