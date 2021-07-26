using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace CodeGenOutput
{
    [Dependency(ReplaceServices = true)]
    public class CodeGenOutputBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "CodeGenOutput";
    }
}
