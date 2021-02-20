using CodeGenOutput.API.Models;

namespace CodeGenOutput.API.Validation
{
    public interface IValidatorInitilizer
    {
        void Init();
    }
    
    public static class Validators
    {
        public static ProjectValidator ProjectValidator = new ProjectValidator();
        
        static Validators()
        {
            ProjectValidator.Init();
        }
    }
}
