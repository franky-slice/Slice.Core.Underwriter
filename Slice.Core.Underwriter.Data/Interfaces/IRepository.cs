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

namespace Slice.Core.Underwriter.Data.Interfaces
{
    public interface IWeatherRepository<T> : IRepository<T> where T : class
    {
    }

    public interface IRepository<T> where T : class
    {
        void Dispose();

        #region Sync

        T Add(T t);

        int Count();

        void Delete(T entity);

        T Find(Expression<Func<T, bool>> match);

        ICollection<T> FindAll(Expression<Func<T, bool>> match);

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        T Get(int id);

        IQueryable<T> GetAll();

        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);

        void Save();

        T Update(object key, T entity);

        #endregion

        #region Async

        Task<T> AddAsync(T t);

        Task<int> CountAsync();

        Task<int> DeleteAsync(T entity);

        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);

        Task<T> FindAsync(Expression<Func<T, bool>> match);

        Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate);

        Task<ICollection<T>> GetAllAsync();

        Task<T> GetAsync(int id);

        Task<int> SaveAsync();

        Task<T> UpdateAsync(object key, T t);

        #endregion
    }
}