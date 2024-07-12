using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly DbContext _dbContext;

        public RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public virtual T? GetById(int id)
        {
            return _dbContext.Set<T>().Find(new object[] { id });
        }

        public virtual List<T> List()
        {
            return _dbContext.Set<T>().ToList();
        }

        public virtual T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);

            SaveChanges();

            return entity;
        }

        public virtual void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);

            SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);

            SaveChanges();
        }

        public virtual void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
