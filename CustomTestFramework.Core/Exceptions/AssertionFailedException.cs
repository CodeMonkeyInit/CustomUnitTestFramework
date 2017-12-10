using System;

namespace CustomTestFramework.Core.Exceptions
{
    public class AssertionFailedException : Exception
    {
        public AssertionFailedException(string message) : base(message)
        {

        }

        public AssertionFailedException()
        {
            
        }
    }

    
}