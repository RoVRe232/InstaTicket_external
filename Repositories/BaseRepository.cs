using InstaTicket.Context;
using InstaTicket.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InstaTicket.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class, new()
    {
        protected readonly InstaAppContext dbContext;

        public BaseRepository(InstaAppContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<T> GetQuery(Expression<Func<T, bool>> expression)
        {
            return dbContext.Set<T>().Where(expression);
        }

        public T Add(T itemToAdd)
        {
            var entity = dbContext.Add<T>(itemToAdd);
            dbContext.SaveChanges();
            return entity.Entity;
        }

        public T GetById(Guid Id)
        {
            return dbContext.Set<T>()
                            .Find(Id);
        }

        public T GetById(int Id)
        {
            return dbContext.Set<T>()
                .Find(Id);
        }

        public T GetById(string Id)
        {
            return dbContext.Set<T>()
                .Find(Id);
        }

        public bool Delete(T itemToDelete)
        {
            dbContext.Remove<T>(itemToDelete);
            dbContext.SaveChanges();
            return true;

        }

        public IEnumerable<T> GetAll()
        {
            return dbContext.Set<T>()
                            .AsEnumerable();
        }

        public T Update(T itemToUpdate)
        {
            var entity = dbContext.Update<T>(itemToUpdate);
            dbContext.SaveChanges();
            return entity.Entity;
        }

    }
}
