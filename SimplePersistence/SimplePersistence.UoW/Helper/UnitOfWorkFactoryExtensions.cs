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
namespace SimplePersistence.UoW.Helper
{
    using System;
    using Exceptions;

    /// <summary>
    /// Extension classes for the Unit of Work factory
    /// </summary>
    public static class UnitOfWorkFactoryExtensions
    {
        #region GetAndRelease

        #region T

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function, releases the UoW instance and returns the value
        /// </summary>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <typeparam name="TFactory">The <see cref="IUnitOfWorkFactory"/> type</typeparam>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The return type</typeparam>
        /// <exception cref="ArgumentNullException"/>
        public static T GetAndRelease<TFactory, TUoW, T>(this TFactory factory, Func<TUoW, T> toExecute)
            where TFactory : IUnitOfWorkFactory
            where TUoW : IUnitOfWork
        {
            if (factory == null) throw new ArgumentNullException("factory");
            if (toExecute == null) throw new ArgumentNullException("toExecute");

            var uow = factory.Get<TUoW>();
            try
            {
                return toExecute(uow);
            }
            finally
            {
                factory.Release(uow);
            }
        }

        #endregion

        #region Void

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the action, releases the UoW instance
        /// </summary>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <typeparam name="TFactory">The <see cref="IUnitOfWorkFactory"/> type</typeparam>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <exception cref="ArgumentNullException"/>
        public static void GetAndRelease<TFactory, TUoW>(this TFactory factory, Action<TUoW> toExecute)
            where TFactory : IUnitOfWorkFactory
            where TUoW : IUnitOfWork
        {
            if (factory == null) throw new ArgumentNullException("factory");
            if (toExecute == null) throw new ArgumentNullException("toExecute");

            var uow = factory.Get<TUoW>();
            try
            {
                toExecute(uow);
            }
            finally
            {
                factory.Release(uow);
            }
        }

        #endregion

        #endregion

        #region GetAndReleaseAfterExecuteAndCommit

        #region T

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function, releases the UoW instance and returns the value
        /// </summary>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <typeparam name="TFactory">The <see cref="IUnitOfWorkFactory"/> type</typeparam>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The return type</typeparam>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static T GetAndReleaseAfterExecuteAndCommit<TFactory, TUoW, T>(this TFactory factory, Func<TUoW, T> toExecute)
            where TFactory : IUnitOfWorkFactory
            where TUoW : IUnitOfWork
        {
            if (factory == null) throw new ArgumentNullException("factory");
            if (toExecute == null) throw new ArgumentNullException("toExecute");

            var uow = factory.Get<TUoW>();
            try
            {
                return uow.ExecuteAndCommit(toExecute);
            }
            finally
            {
                factory.Release(uow);
            }
        }

        #endregion

        #region Void

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the action, releases the UoW instance
        /// </summary>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <typeparam name="TFactory">The <see cref="IUnitOfWorkFactory"/> type</typeparam>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static void GetAndReleaseAfterExecuteAndCommit<TFactory, TUoW>(this TFactory factory, Action<TUoW> toExecute)
            where TFactory : IUnitOfWorkFactory
            where TUoW : IUnitOfWork
        {
            if (factory == null) throw new ArgumentNullException("factory");
            if (toExecute == null) throw new ArgumentNullException("toExecute");

            var uow = factory.Get<TUoW>();
            try
            {
                uow.ExecuteAndCommit(toExecute);
            }
            finally
            {
                factory.Release(uow);
            }
        }

        #endregion

        #endregion
    }
}
