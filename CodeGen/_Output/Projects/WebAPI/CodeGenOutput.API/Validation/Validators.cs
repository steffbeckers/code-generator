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
        public static ContactValidator ContactValidator = new ContactValidator();
        
        static Validators()
        {
            AccountValidator.Init();
            ContactValidator.Init();
        }
    }
}
