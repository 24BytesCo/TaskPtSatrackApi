using Microsoft.EntityFrameworkCore;
using Tareas.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n y registro del contexto de base de datos (TaskDbContext) en el contenedor de servicios.
builder.Services.AddDbContext<TaskDbContext>(options =>
{
    // Configuraci�n de la cadena de conexi�n, obtenida desde la configuraci�n de la aplicaci�n.
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"), b =>
    {
        // Especificaci�n de la configuraci�n para SQL Server.
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
