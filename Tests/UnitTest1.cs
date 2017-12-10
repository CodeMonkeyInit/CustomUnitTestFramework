using System;
using CustomTestFramework.Core.Attributes;
using CustomTestFramework.Core.Exceptions;
using static CustomTestFramework.Core.Assertions.Assertions;
using System.Diagnostics;

namespace Tests
{
    public class UnitTest2
    {
        [CustomTest]
        public void Test1()
        {
            AssertThrows<InvalidOperationException>(() => throw new FieldAccessException());
        }
    }

    public class UnitTest1
    {
        [BeforeAll]
        public void BeforeAll()
        {
            Debug.WriteLine("before all");
        }

        [AfterEach]
        public void After()
        {
            Debug.WriteLine("after");
        }

        [AfterAll]
        public void AfterAll()
        {
            Debug.WriteLine("after all");
        }

        [BeforeEach]
        public void Before()
        {
            Debug.WriteLine("before");
        }

        [CustomTest]
        public void Test()
        {
            AssertTrue(false);
        }

        [CustomTest]
        public void Method()
        {
            Console.WriteLine("Alright then");
        }
    }
}
