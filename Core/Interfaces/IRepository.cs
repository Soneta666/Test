

namespace Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity?> GetById(object id);

        Task<TEntity?> Insert(TEntity entity);

        Task Delete(object id);

        Task Update(TEntity entityToUpdate);

        Task Save();
    }
}