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
        public static AddressValidator AddressValidator = new AddressValidator();
        public static ContactValidator ContactValidator = new ContactValidator();
        
        static Validators()
        {
            AccountValidator.Init();
            AccountContactValidator.Init();
            AddressValidator.Init();
            ContactValidator.Init();
        }
    }
}
