using System;

namespace Smartwyre.DeveloperTest.Exceptions
{
    public class NoAccountException : Exception
    {
        public NoAccountException(string message) : base(message)
        {

        }
    }
}
