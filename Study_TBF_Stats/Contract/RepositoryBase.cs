using Microsoft.EntityFrameworkCore;
using Study_TBF_Stats.Contract.IContract;
using Study_TBF_Stats.Models;
using System.Linq.Expressions;

namespace Study_TBF_Stats.Contract
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class

    {
        protected StudyTbfSupContext _context;
        public RepositoryBase(StudyTbfSupContext context)
        {
            _context = context;
        }
        public IQueryable<T> FindAll(bool trackChanges)
        {
            return !trackChanges ? _context.Set<T>().AsNoTracking() : _context.Set<T>();
        }
        public IQueryable<T> FindByCondtion(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return !trackChanges ? _context.Set<T>().Where(expression).AsNoTracking() : _context.Set<T>().Where(expression);
        }
        public void Create(T entity) => _context.Add(entity);
        public void Delete(T entity) => _context.Remove(entity);
        public void Update(T entity) => _context.Set<T>().Update(entity);

    }
}
