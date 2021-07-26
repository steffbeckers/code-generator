using Volo.Abp.Settings;

namespace CodeGenOutput.Settings
{
    public class CodeGenOutputSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(CodeGenOutputSettings.MySetting1));
        }
    }
}
