using CodeGenOutput.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace CodeGenOutput.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class CodeGenOutputController : AbpController
    {
        protected CodeGenOutputController()
        {
            LocalizationResource = typeof(CodeGenOutputResource);
        }
    }
}