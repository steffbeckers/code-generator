using CodeGenOutput.API.Models;

namespace CodeGenOutput.API.Validation
{
    public interface IValidatorInitilizer
    {
        void Init();
    }
    
    public static class Validators
    {
        public static AccountValidator AccountValidator = new AccountValidator();
        public static AccountContactValidator AccountContactValidator = new AccountContactValidator();
        public static ContactValidator ContactValidator = new ContactValidator();
        public static AddressValidator AddressValidator = new AddressValidator();
        
        static Validators()
        {
            AccountContactValidator.Init();
            AccountValidator.Init();
            ContactValidator.Init();
            AddressValidator.Init();
        }
    }
}
