using Tasky.CustomerService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tasky.CustomerService.Permissions;

public class CustomerServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
      //  var myGroup = context.AddGroup(CustomerServicePermissions.GroupName, L("Permission:CustomerService"));


        var CustomerServiceGroup = context.AddGroup(CustomerServicePermissions.GroupName, L("Permission:CustomerService"));

        var customers = CustomerServiceGroup.AddPermission(CustomerServicePermissions.Customers.Default, L("Permission:Customers"));
        customers.AddChild(CustomerServicePermissions.Customers.Create, L("Permission:Create"));
        customers.AddChild(CustomerServicePermissions.Customers.Update, L("Permission:Update"));
        customers.AddChild(CustomerServicePermissions.Customers.Delete, L("Permission:Delete"));

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CustomerServiceResource>(name);
    }
}
