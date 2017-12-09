#region Copyright Notice

// Copyright (C) 2017 Slice Labs Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium 
// without permission from Slice Labs Inc. is strictly prohibited
// Proprietary and confidential 

#endregion

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Slice.Core.Underwriter.Business.Interfaces
{
    public interface IBaseManager<T> : IDisposable, IRepositoryManager<T> where T : class, IBaseModel
    {
        T Add(T model);
        T Edit(T model);
        T Delete(T model);

        T AddIfNotExist(Expression<Func<T, bool>> select, Func<T> add);
        T AddOrEdit(Expression<Func<T, bool>> select, Func<T> add, Action<T> update);
        IEnumerable<T> Delete(Expression<Func<T, bool>> select);
        IEnumerable<T> AddEditOrDelete<TId>(Expression<Func<T, bool>> select, IEnumerable<TId> ids, Func<T> add, Action<T, TId> update);
        IEnumerable<T> AddEditOrDelete<TModel>(Expression<Func<T, bool>> select, IEnumerable<TModel> models, Action<T, TModel> update) where TModel : T, new();

        T Read(int id);
        IEnumerable<T> Read(IEnumerable<int> ids);
        IEnumerable<T> Read(Expression<Func<T, bool>> predicate);
        IEnumerable<T> Read();

        T Read(int id, params Action<T>[] includes);
        IEnumerable<T> Read(IEnumerable<int> ids, params Action<T>[] includes);
        IEnumerable<T> Read(Expression<Func<T, bool>> predicate, params Action<T>[] includes);
        IEnumerable<T> Read(params Action<T>[] includes);

        int Count();
        int Count(Expression<Func<T, bool>> predicate);
    }
}