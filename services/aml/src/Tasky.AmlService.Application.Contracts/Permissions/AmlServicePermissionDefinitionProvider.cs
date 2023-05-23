using Tasky.AmlService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tasky.AmlService.Permissions;

public class AmlServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {

        var AmlServiceGroup = context.AddGroup(AmlServicePermissions.GroupName, L("Permission:AmlService"));
        var remittances = AmlServiceGroup.AddPermission(AmlServicePermissions.AmlRemittances.Default, L("Permission:AmlRemittance"));
        remittances.AddChild(AmlServicePermissions.AmlRemittances.Check, L("Permission:Check"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AmlServiceResource>(name);
    }
}
