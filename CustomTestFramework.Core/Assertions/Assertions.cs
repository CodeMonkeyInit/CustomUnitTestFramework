using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomTestFramework.Core.Exceptions;

namespace CustomTestFramework.Core.Assertions
{
    public static class Assertions
    {
        public static void AssertTrue(bool value)
        {
            if (!value)
            {
                throw new AssertionFailedException($"Given {nameof(value)} is false");
            }
        }

        public static void AssertThrows<TException>(Action method)
            where TException : Exception
        {
            Type exceptionType = typeof(TException);

            var errorMessage = $"'{exceptionType.Name}' wasn't thrown";

            try
            {
                method();
            }
            catch (TException)
            {
                return;
            }
            catch (Exception e)
            {
                throw new AssertionFailedException($"{errorMessage}, but instead exception '{e.GetType().Name}' was thrown");
            }

            throw new AssertionFailedException(errorMessage);
        }
    }
}
