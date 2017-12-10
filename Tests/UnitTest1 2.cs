using System;
using CustomTestFramework.Core.Attributes;
using CustomTestFramework.Core.Exceptions;

namespace Tests
{
    public class UnitTest2
    {
        [CustomTest]
        public void Test1()
        {
            throw new AssertionFailedException("fdsfsd");
        }
    }

    public class UnitTest1
    {
        [CustomTest]
        public void Test()
        {
            throw new AssertionFailedException("Something went wrong");
        }

        [CustomTest]
        public void Method()
        {
            Console.WriteLine("Alright then");
        }
    }
}