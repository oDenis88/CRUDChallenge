using System.Collections.Generic;
using System.Linq;
using ATS.Domain.Interfaces;
using ATS.Domain.Models;
using ATS.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace ATS.Infra.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }
        
        public virtual TEntity GetById(int id)
        {
            var query = _context.Set<TEntity>().Where(e => e.Id == id);

            if(query.Any())
                return query.FirstOrDefault();

            return null;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            var query = _context.Set<TEntity>();

            if(query.Any())
                return query.ToList();

            return new List<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAllFromSP(string spName)
        {
            return _context.Set<TEntity>().FromSqlRaw($"EXECUTE {spName}").ToList();
        }

        public virtual void Save(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public virtual async Task BulkSave(IEnumerable<TEntity> entities)
        {
            //Using the fastest way to save bulk data using EF
            _context.Set<TEntity>().AddRange(entities);
            await _context.SaveChangesAsync();
        }

        public virtual void Update(TEntity entity)
        {

            var ent = _context.Set<TEntity>().Where( p => p.Id == entity.Id).FirstOrDefault();

            if (ent != null)
            {
               _context.Entry(ent).CurrentValues.SetValues(entity);
               _context.SaveChanges();
            }

        }

        public virtual void Delete(int id)
        {
            var ent = _context.Set<TEntity>().Where(p => p.Id == id).FirstOrDefault();
            _context.Entry(ent).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}