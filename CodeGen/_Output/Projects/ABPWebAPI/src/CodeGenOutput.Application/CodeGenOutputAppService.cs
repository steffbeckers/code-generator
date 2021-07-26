using System;
using System.Collections.Generic;
using System.Text;
using CodeGenOutput.Localization;
using Volo.Abp.Application.Services;

namespace CodeGenOutput
{
    /* Inherit your application services from this class.
     */
    public abstract class CodeGenOutputAppService : ApplicationService
    {
        protected CodeGenOutputAppService()
        {
            LocalizationResource = typeof(CodeGenOutputResource);
        }
    }
}
