using System;

namespace Test.API.Framework.Exceptions
{
    # region Authentication

    public class LoginFailedException : Exception
    {
        public LoginFailedException(string message) : base(message) {}
    }

    public class RegistrationFailedException : Exception
    {
        public RegistrationFailedException(string message) : base(message) {}
    }

    #endregion
}
