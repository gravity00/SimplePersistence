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
    /// <summary>
    /// Metadata information about the entity version
    /// </summary>
    /// <example>
    /// For Entity Framework you should use IHaveVersion&lt;byte[]&gt; 
    /// (<see cref="IHaveVersionAsByteArray"/> may be used instead)
    /// <code>
    /// public class Person : IEntity&lt;long&gt;, IHaveVersion&lt;byte[]&gt; {
    /// 
    ///     public long Id { get; set; }
    ///     public string Name { get; set; }
    ///     public byte[] Version { get; set; }
    /// 
    /// }
    /// </code>
    /// </example>
    /// <typeparam name="TVersion">
    /// The version property type.
    /// </typeparam>
    public interface IHaveVersion<TVersion>
    {
        /// <summary>
        /// The entity version
        /// </summary>
        TVersion Version { get; set; }
    }
}