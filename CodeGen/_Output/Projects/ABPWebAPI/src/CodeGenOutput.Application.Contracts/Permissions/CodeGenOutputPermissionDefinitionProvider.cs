using CodeGenOutput.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace CodeGenOutput.Permissions
{
    public class CodeGenOutputPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(CodeGenOutputPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(CodeGenOutputPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<CodeGenOutputResource>(name);
        }
    }
}
