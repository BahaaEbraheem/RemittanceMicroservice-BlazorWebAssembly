using Tasky.RemittanceService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tasky.RemittanceService.Permissions;

public class RemittanceServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var RemittanceServiceGroup = context.AddGroup(RemittanceServicePermissions.GroupName, L("Permission:RemittanceService"));
        var remittances = RemittanceServiceGroup.AddPermission(RemittanceServicePermissions.Remittances.Default, L("Permission:Remittances"));
        remittances.AddChild(RemittanceServicePermissions.Remittances.Create, L("Permission:Create"));
        remittances.AddChild(RemittanceServicePermissions.Remittances.Update, L("Permission:Edit"));
        remittances.AddChild(RemittanceServicePermissions.Remittances.Delete, L("Permission:Delete"));
        remittances.AddChild(RemittanceServicePermissions.Remittances.Ready, L("Permission:Ready"));
        remittances.AddChild(RemittanceServicePermissions.Remittances.Approved, L("Permission:Approved"));
        remittances.AddChild(RemittanceServicePermissions.Remittances.Released, L("Permission:Released"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<RemittanceServiceResource>(name);
    }
}
