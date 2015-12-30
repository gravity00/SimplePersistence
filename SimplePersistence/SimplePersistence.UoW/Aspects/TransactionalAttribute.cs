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
namespace SimplePersistence.UoW.Aspects
{
    using System;
    using Properties;
    using JetBrains.Annotations;

    /// <summary>
    /// Indicates that this method or class should be executed inside a transaction
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class TransactionalAttribute : Attribute
    {
        /// <summary>
        /// The transaction type. By default will be assigned as <see cref="TransactionType"/>.Required
        /// </summary>
        public TransactionType TransactionType { get; set; }

        /// <summary>
        /// The transaction max duration. By default will be assigned as 1 minute
        /// </summary>
        public int TransactionDurationLimit { get; set; }

        /// <summary>
        /// If a System.Transactions.TransactionScope should be used. By default will be set to false
        /// </summary>
        public bool UseGlobalTransaction { get; set; }

        /// <summary>
        /// The context type for which this annotation will be used
        /// </summary>
        public Type UnitOfWorkType { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWorkType"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public TransactionalAttribute([NotNull] Type unitOfWorkType)
        {
            if (unitOfWorkType == null)
                throw new ArgumentNullException("unitOfWorkType");
            if (!typeof(IUnitOfWork).IsAssignableFrom(unitOfWorkType))
                throw new ArgumentException(Resources.TransactionalAttributeInvalidUoWType, "unitOfWorkType");

            UnitOfWorkType = unitOfWorkType;
            TransactionDurationLimit = 1;
            TransactionType = TransactionType.Required;
            UseGlobalTransaction = false;
        }
    }
}