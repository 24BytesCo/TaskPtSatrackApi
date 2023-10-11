using Microsoft.EntityFrameworkCore;
using Tareas.Infrastructure;
using Tareas.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

//Agregamos y aseguramos que los servicios de infraestructura esten configurados correctamente
builder.Services.AddInfraestructureServiceRegistration(builder.Configuration);

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

// Add services to the container.

builder.Services.AddControllers();

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

//app.UseAuthorization();
app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
