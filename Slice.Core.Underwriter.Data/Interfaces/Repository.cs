#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Slice.Core.Underwriter.Data.Interfaces
{
    public class WeatherRepository<T> : Repository<T>, IWeatherRepository<T> where T : class
    {
        public WeatherRepository(WeatherContext context) : base(context)
        {
        }
    }

    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;

        private bool _disposed;

        protected Repository(DbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        #region Sync

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public virtual T Get(Guid id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual T Add(T t)
        {
            _context.Set<T>().Add(t);
            _context.SaveChanges();
            return t;
        }

        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().SingleOrDefault(match);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().Where(match).ToList();
        }

        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public virtual T Update(object key, T entity)
        {
            if (entity == null)
            {
                return null;
            }

            var exist = _context.Set<T>().Find(key);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }
            return exist;
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            var query = _context.Set<T>().Where(predicate);
            return query;
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            var queryable = GetAll();
            foreach (var includeProperty in includeProperties)
            {
                queryable = queryable.Include(includeProperty);
            }

            return queryable;
        }

        #endregion

        #region Async

        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            try
            {
                return await _context.Set<T>().ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                //_logger.LogError($"Error: {e.Message}");
                throw;
            }
        }

        public virtual async Task<T> GetAsync(Guid id)
        {
            try
            {
                return await _context.Set<T>().FindAsync(id).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                //_logger.LogError($"Error: {e.Message}");
                throw;
            }
        }

        public virtual async Task<T> AddAsync(T t)
        {
            try
            {
                _context.Set<T>().Add(t);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return t;
            }
            catch (Exception e)
            {
                //_logger.LogError($"Error: {e.Message}");
                throw;
            }
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            try
            {
                return await _context.Set<T>().SingleOrDefaultAsync(match).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                //_logger.LogError($"Error: {e.Message}");
                throw;
            }
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            try
            {
                return await _context.Set<T>().Where(match).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                //_logger.LogError($"Error: {e.Message}");
                throw;
            }
        }

        public virtual async Task<int> DeleteAsync(T entity)
        {
            try
            {
                _context.Set<T>().Remove(entity);
                return await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
               // _logger.LogError($"Error: {e.Message}");
                throw;
            }
        }

        public virtual async Task<T> UpdateAsync(object key, T t)
        {
            try
            {
                if (t == null)
                {
                    return null;
                }

                var exist = await _context.Set<T>().FindAsync(key).ConfigureAwait(false);
                if (exist != null)
                {
                    _context.Entry(exist).CurrentValues.SetValues(t);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return exist;
            }
            catch (Exception e)
            {
                //_logger.LogError($"Error: {e.Message}");
                throw;
            }
        }

        public async Task<int> CountAsync()
        {
            try
            {
                return await _context.Set<T>().CountAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                //_logger.LogError($"Error: {e.Message}");
                throw;
            }
        }

        public virtual async Task<int> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                //_logger.LogError($"Error: {e.Message}");
                throw;
            }
        }

        public virtual async Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _context.Set<T>().Where(predicate).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                //_logger.LogError($"Error: {e.Message}");
                throw;
            }
        }

        #endregion
    }
}