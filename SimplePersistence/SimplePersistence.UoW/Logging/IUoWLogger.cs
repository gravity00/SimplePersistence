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
namespace SimplePersistence.UoW.Logging
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The logger interface used by UoW classes
    /// </summary>
    public interface IUoWLogger
    {
        /// <summary>
        /// Writes a debug message
        /// </summary>
        /// <param name="msg">The message to write</param>
        /// <param name="e">The exception or null if none</param>
        void Debug(string msg, Exception e = null);

        /// <summary>
        /// Writes a debug message
        /// </summary>
        /// <param name="msg">The message to write</param>
        /// <param name="e">The exception or null if none</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The task to be awaited</returns>
        Task DebugAsync(string msg, Exception e = null, CancellationToken ct = default (CancellationToken));

        /// <summary>
        /// Writes an information message
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="e"></param>
        void Info(string msg, Exception e = null);

        /// <summary>
        /// Writes an information message
        /// </summary>
        /// <param name="msg">The message to write</param>
        /// <param name="e">The exception or null if none</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The task to be awaited</returns>
        Task InfoAsync(string msg, Exception e = null, CancellationToken ct = default (CancellationToken));

        /// <summary>
        /// Writes a warning message
        /// </summary>
        /// <param name="msg">The message to write</param>
        /// <param name="e">The exception or null if none</param>
        void Warn(string msg, Exception e = null);

        /// <summary>
        /// Writes a warning message
        /// </summary>
        /// <param name="msg">The message to write</param>
        /// <param name="e">The exception or null if none</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The task to be awaited</returns>
        Task WarnAsync(string msg, Exception e = null, CancellationToken ct = default (CancellationToken));

        /// <summary>
        /// Writes an error message
        /// </summary>
        /// <param name="msg">The message to write</param>
        /// <param name="e">The exception or null if none</param>
        void Error(string msg, Exception e = null);

        /// <summary>
        /// Writes an error message
        /// </summary>
        /// <param name="msg">The message to write</param>
        /// <param name="e">The exception or null if none</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The task to be awaited</returns>
        Task ErrorAsync(string msg, Exception e = null, CancellationToken ct = default (CancellationToken));

        /// <summary>
        /// Writes a fatal message
        /// </summary>
        /// <param name="msg">The message to write</param>
        /// <param name="e">The exception or null if none</param>
        void Fatal(string msg, Exception e = null);

        /// <summary>
        /// Writes a fatal message
        /// </summary>
        /// <param name="msg">The message to write</param>
        /// <param name="e">The exception or null if none</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The task to be awaited</returns>
        Task FatalAsync(string msg, Exception e = null, CancellationToken ct = default (CancellationToken));
    }
}
