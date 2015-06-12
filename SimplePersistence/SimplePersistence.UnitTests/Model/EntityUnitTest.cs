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
namespace SimplePersistence.UnitTests.Model
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SimplePersistence.Model;

    [TestClass]
    public class EntityUnitTest
    {
        [TestMethod, TestCategory("Model"), TestCategory("ObjectType")]
        public void ObjectType()
        {
            var entity01 = new MockEntity<long>();

            Assert.IsInstanceOfType(entity01, typeof (Entity<long>));
            Assert.IsInstanceOfType(entity01, typeof (IEntity<long>));

            var entity02 = new MockEntity<int>();

            Assert.IsInstanceOfType(entity02, typeof (IEntity<int>));
        }

        [TestMethod, TestCategory("Model"), TestCategory("Constructor")]
        public void ConstructorObjectInitializer()
        {
            var entity = new MockEntity<long>();

            Assert.AreEqual(0, entity.Id);

            const long id = 1337;
            entity = new MockEntity<long>
            {
                Id = id
            };
            Assert.AreEqual(id, entity.Id);
        }

        #region Mock

        private class MockEntity<TIdentity> : Entity<TIdentity> { }

        #endregion
    }
}
