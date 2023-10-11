using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tareas.Application.Persistence;
using Tareas.Infrastructure.Persistence;

namespace Tareas.Infrastructure.Repositories
{
    // Esta clase proporciona una implementación genérica de un repositorio asincrónico basado en Entity Framework Core.
    public class RepositoryBase<T> : IAsyncRepository<T> where T : class
    {
        protected readonly TaskDbContext _context;

        public RepositoryBase(TaskDbContext context)
        {
            _context = context;
        }

        // Agrega una entidad al contexto y guarda los cambios en la base de datos de forma asincrónica.
        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // Elimina una entidad del contexto y guarda los cambios en la base de datos de forma asincrónica.
        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        // Obtiene todas las entidades de tipo T de la base de datos de forma asincrónica.
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        // Obtiene entidades de tipo T que coinciden con un predicado y aplica filtros, inclusiones y seguimiento de entidades de forma asincrónica.
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy, string? includeString, bool disableTracking = true)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        // Obtiene entidades de tipo T que coinciden con un predicado y aplica filtros, inclusiones, ordenamiento y seguimiento de entidades de forma asincrónica.
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<Expression<Func<T, object>>>? includes = null, bool disableTracking = true)
        {
            var query = _context.Set<T>().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        // Obtiene una entidad de tipo T por su identificador único de forma asincrónica.
        public async Task<T> GetByIdAsync(Guid id)
        {
            return (await _context.Set<T>().FindAsync(id))!;
        }

        // Obtiene una entidad de tipo T que coincide con un predicado y aplica inclusiones y seguimiento de entidades de forma asincrónica.
        public async Task<T> GetEntityAsync(Expression<Func<T, bool>>? predicate, List<Expression<Func<T, object>>>? includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return (await query.FirstOrDefaultAsync(predicate))!;
        }

        // Actualiza una entidad en el contexto y guarda los cambios en la base de datos de forma asincrónica.
        public async Task<T> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
