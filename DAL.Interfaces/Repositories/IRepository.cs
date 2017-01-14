using System.Collections.Generic;

namespace DAL.Interfaces.Repositories
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(int id);

        void Create(TEntity entity);

        void Delete(int id);

        void Update(TEntity entity);
    }
}