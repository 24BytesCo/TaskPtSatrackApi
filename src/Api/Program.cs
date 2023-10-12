using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using System.Text.Json.Serialization;
using Tareas.Api.Middlewares;
using Tareas.Application;
using Tareas.Application.Features.Categories.Queries.GetCategoriesList;
using Tareas.Infrastructure;
using Tareas.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

//Agregamos y aseguramos que los servicios de infraestructura esten configurados correctamente
builder.Services.AddInfraestructureServiceRegistration(builder.Configuration);

//Agregamos y aseguramos que los servicios de Aplication esten configurados correctamente
builder.Services.AddApplicationServices(builder.Configuration);

// Configuración y registro del contexto de base de datos (TaskDbContext) en el contenedor de servicios.
builder.Services.AddDbContext<TaskDbContext>(options =>
{
    // Configuración de la cadena de conexión, obtenida desde la configuración de la aplicación.
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"), b =>
    {
        // Especificación de la configuración para SQL Server.
        // Se utiliza el nombre de la assembly que contiene el contexto de la base de datos para las migraciones.
        //PAra que imprima en consola todas las tareas que se esten realizando
        b.MigrationsAssembly(typeof(TaskDbContext).Assembly.FullName);
    });
});

// Registra los controladores y características de MediatR para el ensamblado que contiene la clase 'GetCategoriesListQuery'.
builder.Services.AddMediatR(typeof(GetCategoriesListQueryHandler).Assembly);


// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(r=> r.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de políticas CORS para permitir solicitudes desde cualquier origen, método y encabezado.
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", builder =>
        builder.AllowAnyOrigin()       // Permite solicitudes desde cualquier origen (dominio).
               .AllowAnyMethod()       // Permite cualquier método de solicitud (GET, POST, etc.).
               .AllowAnyHeader()       // Permite cualquier encabezado de solicitud.
        );
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();


//app.UseAuthorization();
app.UseCors("CorsPolicy");

app.MapControllers();

// Crea un ámbito de servicios para realizar tareas relacionadas con la base de datos.
using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;

    var loggerFactory = service.GetRequiredService<ILoggerFactory>();

    try
    {
        // Obtiene el contexto de la base de datos, necesario para las migraciones y la carga de datos iniciales.
        var context = service.GetRequiredService<TaskDbContext>();

        // Realiza migraciones pendientes en la base de datos de forma asíncrona.
        await context.Database.MigrateAsync();

        // Llama a un método para cargar datos iniciales en la base de datos.
        await TaskDbDataContext.LoadDataAsync(context, loggerFactory);
    }
    catch (Exception ex)
    {
        // En caso de error, registra el error en el sistema de registro.
        var logger = loggerFactory.CreateLogger<Program>();

        // Registra un mensaje de error junto con el mensaje de la excepción.
        logger.LogError(ex.Message, "Error en la migración");
    }
}


app.Run();
