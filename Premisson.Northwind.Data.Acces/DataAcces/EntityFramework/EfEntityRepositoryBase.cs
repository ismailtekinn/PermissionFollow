using Microsoft.EntityFrameworkCore;
using Premisson.Northwind.Entities;
using System;
using System.Collections.Generic;
using Premisson.Northwind.Data.Acces.Concreate.EntityFramework;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Premisson.Northwind.DataAcces;

namespace Premisson.Northwinds.DataAcces.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity> : IEntityRepository<TEntity>
         where TEntity : class, IEntity, new()
    {

        private readonly NorthwindContext _context;

        public EfEntityRepositoryBase(NorthwindContext context)
        {
            _context = context;
        }
        public void Add(TEntity entity)
        {
           
                var addedEntity = _context.Entry(entity);
                addedEntity.State = EntityState.Added;
            
        }


        public TEntity Get(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().Where(filter);

            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            return query.SingleOrDefault();
        }

        public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity,bool>>filter = null)
        {
            IQueryable<TEntity> query = filter == null
                ? _context.Set<TEntity>()
                : _context.Set<TEntity>().Where(filter);
            return query;
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] include)
        {
            IQueryable<TEntity> query = filter == null
                    ? _context.Set<TEntity>()
                    : _context.Set<TEntity>().Where(filter);

            if (include != null)
                query = include.Aggregate(query, (current, include) => current.Include(include));

            return query.ToList();
        }

        public void Update(TEntity entity)
        {
            
                var updatedEntity = _context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
               
        }

        public void Delete(TEntity entity)
        {
                var deletedEntity = _context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
        }

        public bool Complate()
        {
            var transaction = _context.Database.BeginTransaction();
            bool isSuccess = _context.SaveChanges() > 0;
            if (isSuccess)
            {
                transaction.Commit();
            }
            else
            {
                transaction.Rollback();
            }
            return isSuccess;
        }
    }
}
