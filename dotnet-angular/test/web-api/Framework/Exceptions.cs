using System;

namespace Test.API.Framework.Exceptions
{
    public class LoginFailedException : Exception
    {
        public LoginFailedException(string message) : base(message) {}
    }
}
