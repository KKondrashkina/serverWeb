using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ShopUnitOfWork
{
    public abstract class BaseEfRepository<T> : IRepository<T> where T : class
    {
        protected DbContext db;
        protected DbSet<T> dbSet;

        protected BaseEfRepository(DbContext db)
        {
            this.db = db;
            dbSet = db.Set<T>();
        }

        public virtual IDbContextTransaction BeginTransaction()
        {
            return db.Database.BeginTransaction();
        }

        public virtual void Delete(T entity)
        {
            if (db.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
        }

        public virtual T[] GetAll()
        {
            return dbSet.ToArray();
        }

        public virtual T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void AddRange(List<T> entities)
        {
            dbSet.AddRange(entities);
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
        }
    }
}
