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
namespace SimplePersistence.Model
{
    using System;

    /// <summary>
    /// Metadata information about the entity creation
    /// </summary>
    /// <typeparam name="TCreatedBy">The identifier or entity type</typeparam>
    public interface IHaveLocalCreatedMeta<TCreatedBy>
    {
        /// <summary>
        /// The <see cref="DateTime"/> when it was created
        /// </summary>
        DateTime CreatedOn { get; set; }

        /// <summary>
        /// The identifier (or entity) which first created this entity
        /// </summary>
        TCreatedBy CreatedBy { get; set; }
    }

    /// <summary>
    /// Metadata information about the entity creation, using a <see cref="string"/>
    /// as an identifier for the <see cref="IHaveLocalCreatedMeta{T}.CreatedBy"/>
    /// </summary>
    public interface IHaveLocalCreatedMeta : IHaveLocalCreatedMeta<string>
    {
        
    }
}
