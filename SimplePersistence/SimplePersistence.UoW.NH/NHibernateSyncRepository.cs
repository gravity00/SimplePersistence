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
namespace SimplePersistence.UoW.NH
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using NHibernate;
    using NHibernate.Linq;

    public abstract class NHibernateSyncRepository<TEntity, TKey> : ISyncRepository<TEntity, TKey>
        where TEntity : class
    {
        protected ISession Session { get; private set; }

        protected NHibernateSyncRepository(ISession session)
        {
            if (session == null) throw new ArgumentNullException("session");

            Session = session;
        }

        #region Query

        public virtual IQueryable<TEntity> Query()
        {
            return Session.Query<TEntity>();
        }

        public virtual IQueryable<TEntity> QueryFetching(params Expression<Func<TEntity, object>>[] propertiesToFetch)
        {
            return propertiesToFetch.Aggregate(Query(), (current, expression) => current.Fetch(expression));
        }

        #endregion

        #region GetById

        public virtual TEntity GetById(TKey id)
        {
            return Session.Get<TEntity>(id);
        }

        #endregion

        #region Add

        public virtual TEntity Add(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            Session.Save(entity);
            return entity;
        }

        public virtual IEnumerable<TEntity> Add(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            return entities.Select(Add).ToArray();
        }

        #endregion

        #region Update

        public virtual TEntity Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            Session.Update(entity);
            return entity;
        }

        public virtual IEnumerable<TEntity> Update(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            return entities.Select(Update).ToArray();
        }

        #endregion

        #region Delete

        public virtual TEntity Delete(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            Session.Delete(entity);
            return entity;
        }

        public virtual IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            return entities.Select(Delete).ToArray();
        }

        #endregion

        #region Total

        public virtual long Total()
        {
            return Session.QueryOver<TEntity>().RowCountInt64();
        }

        #endregion
    }
}
