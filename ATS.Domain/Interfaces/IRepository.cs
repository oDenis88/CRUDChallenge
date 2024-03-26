using System.Collections.Generic;

namespace ATS.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        void Save(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
        IEnumerable<TEntity> GetAllFromSP(string spName);
        Task BulkSave(IEnumerable<TEntity> entities);

    }
}