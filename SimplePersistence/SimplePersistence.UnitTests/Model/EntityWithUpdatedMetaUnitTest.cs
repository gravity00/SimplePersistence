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
    using System;

    [TestClass]
    public class EntityWithUpdatedMetaUnitTest
    {
        [TestMethod, TestCategory("Model"), TestCategory("ObjectType")]
        public void ObjectType()
        {
            var entity01 = new MockEntity<long>();

            Assert.IsInstanceOfType(entity01, typeof (EntityWithUpdatedMeta<long>));
            Assert.IsInstanceOfType(entity01, typeof (IEntity<long>));
            Assert.IsInstanceOfType(entity01, typeof (IHaveUpdatedMeta<string>));

            var entity02 = new MockEntity<int, object>();

            Assert.IsInstanceOfType(entity02, typeof(IEntity<int>));
            Assert.IsInstanceOfType(entity02, typeof(IHaveUpdatedMeta<object>));
        }

        [TestMethod, TestCategory("Model"), TestCategory("Constructor")]
        public void ConstructorObjectInitializer()
        {
            var entity01 = new MockEntity<long>();

            Assert.AreEqual(0, entity01.Id);
            Assert.AreEqual(default(DateTime), entity01.UpdatedOn);
            Assert.AreEqual(default(string), entity01.UpdatedBy);

            entity01 = new MockEntity<long>
            {
                Id = TestId,
                UpdatedOn = TestUpdatedOn,
                UpdatedBy = TestUpdatedBy01
            };
            Assert.AreEqual(TestId, entity01.Id);
            Assert.AreEqual(TestUpdatedOn, entity01.UpdatedOn);
            Assert.AreEqual(TestUpdatedBy01, entity01.UpdatedBy);

            var entity02 = new MockEntity<long, object>();

            Assert.AreEqual(0, entity02.Id);
            Assert.AreEqual(default(DateTime), entity02.UpdatedOn);
            Assert.AreEqual(default(string), entity02.UpdatedBy);

            entity02 = new MockEntity<long, object>
                     {
                         Id = TestId,
                         UpdatedOn = TestUpdatedOn,
                         UpdatedBy = TestUpdatedBy02
                     };
            Assert.AreEqual(TestId, entity02.Id);
            Assert.AreEqual(TestUpdatedOn, entity02.UpdatedOn);
            Assert.AreEqual(TestUpdatedBy02, entity02.UpdatedBy);
        }

        #region Mock

        private class MockEntity<TIdentity> : EntityWithUpdatedMeta<TIdentity> { }

        private class MockEntity<TIdentity, TUpdatedBy> : EntityWithUpdatedMeta<TIdentity, TUpdatedBy> { }

        #endregion

        #region Constants

        private const long TestId = 1337;
        private static readonly DateTimeOffset TestUpdatedOn = DateTimeOffset.Now;
        private const string TestUpdatedBy01 = "john.doe";
        private static readonly object TestUpdatedBy02 = new object();

        #endregion
    }
}