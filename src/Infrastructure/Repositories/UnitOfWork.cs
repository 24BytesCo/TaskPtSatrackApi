using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tareas.Application.Persistence;
using Tareas.Infrastructure.Persistence;

namespace Tareas.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        // Usamos una Hashtable para almacenar los repositorios.
        private Hashtable? _repositories;

        // Mantenemos una referencia al contexto de la base de datos.
        private readonly TaskDbContext _context;

        public UnitOfWork(TaskDbContext taskDbContext)
        {
            this._context = taskDbContext;
        }

        public void Dispose()
        {
            // Al liberar recursos, aseguramos que el contexto de la base de datos se cierre adecuadamente.
            _context.Dispose();
        }

        // El método Repository<TEntity> proporciona un repositorio genérico para una entidad específica.
        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories is null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                //Creamos una instancia del repositorio específico para una entidad y la agrega al diccionario de repositorios.

                // Obtiene el tipo genérico de la clase RepositoryBase<>.
                var repositoryType = typeof(RepositoryBase<>);

                // Crea una instancia del repositorio específico para una entidad determinada.
                // Utiliza el tipo genérico RepositoryBase<> como plantilla y lo "rellena" con el tipo de entidad (TEntity).
                // También se pasa el contexto de la base de datos (_context) al constructor del repositorio específico.
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

                // Agrega la instancia del repositorio al diccionario de repositorios.
                // Esto permite un acceso eficiente a los repositorios específicos de las entidades según el tipo de entidad.
                // La clave del diccionario es el nombre de la entidad en forma de cadena.
                _repositories.Add(type, repositoryInstance);

            }

            // Devolvemos una instancia del repositorio genérico adecuado.
            return (IAsyncRepository<TEntity>)_repositories[type]!;
        }
    }
}
