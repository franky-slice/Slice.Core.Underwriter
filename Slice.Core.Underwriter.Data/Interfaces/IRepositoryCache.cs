#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Slice.Core.Underwriter.Data.Interfaces
{
    public interface IRepositoryCache
    {
        T Get<T>(Guid id) where T : class, IBaseModel;
        bool Set<T>(T model) where T : class, IBaseModel;

        T GetOrAdd<T>(Guid id, Func<T> loader) where T : class, IBaseModel;
        T GetOrAdd<T>(Expression<Func<T, bool>> predicate, Func<T> loader, bool addToLists = true);

        IEnumerable<T> GetOrAdd<T>(IEnumerable<Guid> ids, Func<IEnumerable<T>> loader, bool addToLists = true);
        IEnumerable<T> GetOrAdd<T>(Expression<Func<T, bool>> predicate, Func<IEnumerable<T>> loader, bool addToLists = true);
        IEnumerable<T> GetOrAdd<T>(int offset, int length, Func<IEnumerable<T>> loader, bool addToLists = true);
        IEnumerable<T> GetOrAdd<T>(int offset, int length, Expression<Func<T, bool>> predicate, Func<IEnumerable<T>> loader, bool addToLists = true);
        IEnumerable<T> GetOrAdd<T>(Func<IEnumerable<T>> loader, bool addToLists = true);

        int GetOrAddCount<T>(Expression<Func<T, bool>> predicate, Func<int> loader, bool addToLists = true);

        T GetOrAdd<T>(Guid id, IRepository<T> repo) where T : class, IBaseModel;

        IEnumerable<T> GetOrAdd<T>(IEnumerable<Guid> ids, IRepository<T> repo, bool addToLists = true) where T : class, IBaseModel;
        IEnumerable<T> GetOrAdd<T>(Expression<Func<T, bool>> predicate, IRepository<T> repo, bool addToLists = true) where T : class;
        IEnumerable<T> GetOrAdd<T>(int offset, int length, IRepository<T> repo, bool addToLists = true) where T : class, IBaseModel;
        IEnumerable<T> GetOrAdd<T, S>(int offset, int length, Expression<Func<T, S>> orderBy, IRepository<T> repo, bool addToLists = true) where T : class;
        IEnumerable<T> GetOrAdd<T>(int offset, int length, Expression<Func<T, bool>> predicate, IRepository<T> repo, bool addToLists = true) where T : class, IBaseModel;
        IEnumerable<T> GetOrAdd<T, S>(int offset, int length, Expression<Func<T, bool>> predicate, Expression<Func<T, S>> orderBy, IRepository<T> repo, bool addToLists = true) where T : class;
        IEnumerable<T> GetOrAdd<T>(IRepository<T> repo, bool addToLists = true) where T : class;
        int GetOrAddCount<T>(Expression<Func<T, bool>> predicate, IRepository<T> repo, bool addToLists = true) where T : class;

        void Clear<T>();
        void Clear<T>(T model) where T : class, IBaseModel;
        void Clear<T>(IEnumerable<Guid> ids);
        void Clear<T>(Expression<Func<T, bool>> predicate);
        void Clear<T>(int offset, int length);
        void Clear<T>(int offset, int length, Expression<Func<T, bool>> predicate);
        void ClearCount<T>(Expression<Func<T, bool>> predicate);
        void ClearLists<T>();
    }
}