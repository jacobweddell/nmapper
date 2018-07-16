using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectMapper.Tests.TestObjects;
using System.Collections.Generic;

namespace ObjectMapper.Tests
{
    [TestClass]
    public class ObjectMapperTests
    {
        private readonly ObjectMapper _objectMapper;
        public ObjectMapperTests()
        {
            _objectMapper = new ObjectMapper();
        }
        [TestMethod]
        public void Map_SimpleValidObject_NewbjectReturned()
        {
            var testObject1 = new TestObject1()
            {
                Property1 = "hello",
                Property2 = "world",
                Property3 = 42
            };

            var testObject2 = _objectMapper.Map<TestObject2>(testObject1);

            Assert.AreEqual(nameof(TestObject2), testObject2.GetType().Name);
            Assert.AreEqual(testObject1.Property1, testObject2.Property1);
            Assert.AreEqual(testObject1.Property2, testObject2.Property2);
            Assert.AreEqual(testObject1.Property3, testObject2.Property3);
        }

        [TestMethod]
        public void Map_ComplexValidObject_NewbjectReturned()
        {
            var testObject1 = new TestObject1()
            {
                Property1 = "hello",
                Property2 = "world",
                Property3 = 42
            };
            var testObject3 = new TestObject3()
            {
                Property1 = 1,
                Property2 = testObject1
            };

            var testObject4 = _objectMapper.Map<TestObject4>(testObject3);

            Assert.AreEqual(nameof(TestObject4), testObject4.GetType().Name);
            Assert.AreEqual(testObject3.Property1, testObject4.Property1);

            Assert.IsNotNull(testObject3.Property2);
            Assert.AreEqual(testObject3.Property2.Property1, testObject4.Property2.Property1);
            Assert.AreEqual(testObject3.Property2.Property2, testObject4.Property2.Property2);
            Assert.AreEqual(testObject3.Property2.Property3, testObject4.Property2.Property3);
        }

        [TestMethod]
        public void Map_ComplexValidObjectOriginalChanges_NewbjectReturned()
        {
            var testObject1 = new TestObject1()
            {
                Property1 = "hello",
                Property2 = "world",
                Property3 = 42
            };
            var testObject3 = new TestObject3()
            {
                Property1 = 1,
                Property2 = testObject1
            };

            var testObject4 = _objectMapper.Map<TestObject4>(testObject3);

            testObject1.Property1 = "goodbye";

            Assert.AreEqual(nameof(TestObject4), testObject4.GetType().Name);
            Assert.AreEqual(testObject3.Property1, testObject4.Property1);

            Assert.IsNotNull(testObject3.Property2);
            Assert.AreEqual(testObject3.Property2.Property1, testObject4.Property2.Property1);
            Assert.AreEqual(testObject3.Property2.Property2, testObject4.Property2.Property2);
            Assert.AreEqual(testObject3.Property2.Property3, testObject4.Property2.Property3);
        }

        [TestMethod]
        public void Map_ValidObjectDifferentNames_NewbjectReturned()
        {
            var person1 = new Person1()
            {
                FirstName = "John",
                LastName = "Doe"
            };
            var mapping = new Dictionary<string, string>()
           {
               { nameof(Person1.FirstName), nameof(Person2.GivenName) },
               { nameof(Person1.LastName), nameof(Person2.FamilyName) }
           };

            var person2 = _objectMapper.Map<Person2>(person1, mapping);

            Assert.AreEqual(nameof(Person2), person2.GetType().Name);
            Assert.AreEqual(person1.FirstName, person2.GivenName);
            Assert.AreEqual(person1.LastName, person2.FamilyName);
        }

        [TestMethod]
        public void Map_SimpleValidObjectWithAttributes_NewbjectReturned()
        {
            var testObject5 = new TestObject5()
            {
                MyPropertyA = "hello",
                MyPropertyB = "world",
                MyPropertyC = 42
            };

            var testObject2 = _objectMapper.Map<TestObject1>(testObject5);

            Assert.AreEqual(nameof(TestObject1), testObject2.GetType().Name);
            Assert.AreEqual(testObject5.MyPropertyA, testObject2.Property1);
            Assert.AreEqual(testObject5.MyPropertyB, testObject2.Property2);
            Assert.AreEqual(testObject5.MyPropertyC, testObject2.Property3);
        }
    }
}
