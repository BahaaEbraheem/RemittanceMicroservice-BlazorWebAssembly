using Tasky.CurrencyService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tasky.CurrencyService.Permissions;

public class CurrencyServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var currencyGroup = context.AddGroup(CurrencyServicePermissions.GroupName, L("CurrencyService"));
        var currencyPermission = currencyGroup.AddPermission(CurrencyServicePermissions.Currencies.Default, L("CurrencyService:Default"));
        currencyPermission.AddChild(CurrencyServicePermissions.Currencies.Create, L("CurrencyService:Create"));
        currencyPermission.AddChild(CurrencyServicePermissions.Currencies.Update, L("CurrencyService:Update"));
        currencyPermission.AddChild(CurrencyServicePermissions.Currencies.Delete, L("CurrencyService:Delete"));

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CurrencyServiceResource>(name);
    }
}
