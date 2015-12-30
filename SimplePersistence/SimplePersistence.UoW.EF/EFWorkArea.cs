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
namespace SimplePersistence.UoW.EF
{
    using System;
    using System.Data.Entity;
    using JetBrains.Annotations;

    /// <summary>
    /// Represents a work area that can be used for aggregating
    /// UoW properties, specialized for the Entity Framework
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public abstract class EFWorkArea<TDbContext> : IEFWorkArea<TDbContext> where TDbContext : DbContext
    {
        private readonly TDbContext _context;

        /// <summary>
        /// Creates a new work area that will use the given database context
        /// </summary>
        /// <param name="context">The database context</param>
        /// <exception cref="ArgumentNullException">Thrown if the context is null</exception>
        protected EFWorkArea([NotNull] TDbContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// The Entity Framework database context
        /// </summary>
        public TDbContext Context { get { return _context; } }
    }

    /// <summary>
    /// Represents a work area that can be used for aggregating
    /// UoW properties, specialized for the Entity Framework
    /// </summary>
    public abstract class EFWorkArea : EFWorkArea<DbContext>, IEFWorkArea
    {
        /// <summary>
        /// Creates a new work area that will use the given database context
        /// </summary>
        /// <param name="context">The database context</param>
        protected EFWorkArea([NotNull] DbContext context)
            : base(context)
        {
        }
    }
}