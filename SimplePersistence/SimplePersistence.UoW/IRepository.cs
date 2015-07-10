#region License
// The MIT License (MIT)
// Copyright (c) 2015 João Simões
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
#endregion
namespace SimplePersistence.UoW
{
    using System;

    /// <summary>
    /// Repository interface that will be used to query and manipulate
    /// persisted entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The entity id type</typeparam>
    public interface IRepository<TEntity, in TId> : IAsyncRepository<TEntity, TId>, ISyncRepository<TEntity, TId> 
        where TEntity : class 
        where TId : IEquatable<TId>
    {

    }

    /// <summary>
    /// Repository interface that will be used to query and manipulate
    /// persisted entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId01">The entity id first type</typeparam>
    /// <typeparam name="TId02">The entity id second type</typeparam>
    public interface IRepository<TEntity, in TId01, in TId02> : IAsyncRepository<TEntity, TId01, TId02>, ISyncRepository<TEntity, TId01, TId02>
        where TEntity : class
        where TId01 : IEquatable<TId01>
        where TId02 : IEquatable<TId02>
    {

    }

    /// <summary>
    /// Repository interface that will be used to query and manipulate
    /// persisted entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId01">The entity id first type</typeparam>
    /// <typeparam name="TId02">The entity id second type</typeparam>
    /// <typeparam name="TId03">The entity id third type</typeparam>
    public interface IRepository<TEntity, in TId01, in TId02, in TId03> : IAsyncRepository<TEntity, TId01, TId02, TId03>, ISyncRepository<TEntity, TId01, TId02, TId03>
        where TEntity : class
        where TId01 : IEquatable<TId01>
        where TId02 : IEquatable<TId02>
        where TId03 : IEquatable<TId03>
    {

    }

    /// <summary>
    /// Repository interface that will be used to query and manipulate
    /// persisted entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface IRepository<TEntity> : IAsyncRepository<TEntity>, ISyncRepository<TEntity>
        where TEntity : class
    {

    }
}
