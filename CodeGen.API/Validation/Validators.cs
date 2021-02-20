using CodeGen.API.Models;

namespace CodeGen.API.Validation
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
