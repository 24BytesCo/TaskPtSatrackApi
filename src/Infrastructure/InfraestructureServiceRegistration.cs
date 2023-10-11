using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tareas.Application.Persistence;
using Tareas.Infrastructure.Repositories;

namespace Tareas.Infrastructure
{
    public static class InfraestructureServiceRegistration
    {
        // Este método de extensión se encarga de registrar los servicios de infraestructura en el contenedor de servicios.
        public static IServiceCollection AddInfraestructureServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            // Registra el servicio IUnitOfWork con su implementación UnitOfWork en el contenedor.
            // Este servicio se utiliza para gestionar transacciones y unidades de trabajo en la base de datos.
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Registra un servicio genérico para trabajar con repositorios de entidades en la base de datos.
            // IAsyncRepository<> representa un repositorio genérico, y RepositoryBase<> es su implementación concreta.
            // El uso de 'typeof' permite la generación de servicios para múltiples tipos de entidades.
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

            // Devuelve el contenedor de servicios actualizado con los servicios de infraestructura registrados.
            return services;
        }
    }
}
