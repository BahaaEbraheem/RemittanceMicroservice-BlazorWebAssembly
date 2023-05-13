using Tasky.CurrencyService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tasky.CurrencyService.Permissions;

public class CurrencyServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var currencyGroup = context.AddGroup(CurrencyServicePermissions.GroupName, L("Permission:CurrencyService"));
        var currencyPermission = currencyGroup.AddPermission(CurrencyServicePermissions.Currencies.Default, L("Permission:Currencies"));
        currencyPermission.AddChild(CurrencyServicePermissions.Currencies.Create, L("Permission:Create"));
        currencyPermission.AddChild(CurrencyServicePermissions.Currencies.Update, L("Permission:Update"));
        currencyPermission.AddChild(CurrencyServicePermissions.Currencies.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CurrencyServiceResource>(name);
    }
}
