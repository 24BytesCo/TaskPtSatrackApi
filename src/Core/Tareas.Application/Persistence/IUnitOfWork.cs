using System;
using System.Threading.Tasks;

namespace Tareas.Application.Persistence
{
    // La interfaz IUnitOfWork se utiliza para representar un patrón de unidad de trabajo.
    // Este patrón se utiliza para agrupar múltiples operaciones de base de datos en una única transacción.
    public interface IUnitOfWork : IDisposable
    {
        // El método Repository<TEntity> se utiliza para obtener un repositorio para una entidad específica.
        // Este repositorio se utiliza para realizar operaciones de lectura y escritura en la entidad.
        IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : class;

        // El método Complete() se utiliza para confirmar y finalizar la transacción de la unidad de trabajo.
        // Devuelve el número de cambios guardados en la base de datos como resultado de la transacción.
        Task<int> Complete();
    }
}
