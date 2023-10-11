using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Tareas.Application.Persistence
{
    // Interfaz genérica para repositorios asíncronos.
    public interface IAsyncRepository<T> where T : class
    {
        // Recupera todos los elementos de tipo T de forma asíncrona.
        Task<IReadOnlyList<T>> GetAllAsync();

        // Recupera elementos de tipo T que cumplen con un predicado y opcionalmente realiza un ordenamiento y carga relacionada.
        Task<IReadOnlyList<T>> GetAsync(
            Expression<Func<T, bool>>? predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
            string? includeString,
            bool disableTracking = true
        );

        // Recupera elementos de tipo T que cumplen con un predicado y opcionalmente realiza un ordenamiento, carga relacionada y tracking.
        Task<IReadOnlyList<T>> GetAsync(
            Expression<Func<T, bool>>? predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<Expression<Func<T, object>>>? includes = null,
            bool disableTracking = true
        );

        // Recupera un elemento de tipo T que cumple con un predicado y opcionalmente realiza una carga relacionada y tracking.
        Task<T> GetEntityAsync(
            Expression<Func<T, bool>>? predicate,
            List<Expression<Func<T, object>>>? includes = null,
            bool disableTracking = true
        );

        // Recupera un elemento de tipo T por su ID de forma asíncrona.
        Task<T> GetByIdAsync(int id);

        // Agrega un elemento de tipo T de forma asíncrona.
        Task<T> AddAsync(T entity);

        // Actualiza un elemento de tipo T de forma asíncrona.
        Task<T> UpdateAsync(T entity);

        // Elimina un elemento de tipo T de forma asíncrona.
        Task DeleteAsync(T entity);

    }
}
