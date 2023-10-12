using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tareas.Application.Behaviors;
using Tareas.Application.Features.Categories.Commands.CreateCategory;
using Tareas.Application.Features.Tasks.Commands.CreateTask;
using Tareas.Application.Features.Tasks.Commands.UpdateTask;
using Tareas.Application.Mappings;

namespace Tareas.Application
{
    // Proporciona métodos de extensión para registrar servicios de la capa de aplicación en el
    // contenedor de servicios de ASP.NET Core.
    public static class ApplicationServiceRegistration
    {
        // Registra los servicios de la capa de aplicación en el contenedor de servicios.
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Configuración del mapeo de objetos utilizando AutoMapper.
            var mapperConfig = new MapperConfiguration(mapperConfiguration =>
            {
                // Se agrega el perfil de mapeo 'MappingProfile' que define cómo mapear entre objetos DTO y entidades de dominio.
                mapperConfiguration.AddProfile(new MappingProfile());
            });

            // Se crea una instancia de IMapper a partir de la configuración del mapeo.
            IMapper mapper = mapperConfig.CreateMapper();

            // Se registra la instancia de IMapper como un servicio Singleton en el contenedor de servicios.
            services.TryAddSingleton(mapper);

            // Se registran los siguientes comportamientos de MediatR como servicios transitorios en el contenedor de servicios:

            //// - 'UnhandledExceptionBehavior': Maneja excepciones no controladas en las solicitudes de MediatR.
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));

            services.AddTransient<IValidator<CreateCategoryCommand>, CreateCategoryCommandValidator>();
            services.AddTransient<IValidator<CreateTaskCommand>, CreateTaskCommandValidation>();
            services.AddTransient<IValidator<UpdateTaskCommand>, UpdateTaskCommandValidation>();


            // - 'ValidationBehavior': Realiza la validación de las solicitudes de MediatR usando FluentValidation.
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


            return services;  // Se devuelve el contenedor de servicios actualizado con los servicios de la capa de aplicación registrados.
        }
    }
}
