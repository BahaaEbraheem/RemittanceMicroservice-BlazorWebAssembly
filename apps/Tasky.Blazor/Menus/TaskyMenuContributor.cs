using System;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tasky.AmlService.Localization;
using Tasky.AmlService.Permissions;
using Tasky.CurrencyService.Localization;
using Tasky.CustomerService.Localization;
using Tasky.RemittanceService.Localization;
using Tasky.RemittanceService.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Blazor;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.TenantManagement.Blazor.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;

namespace Tasky.Blazor.Menus;

public class TaskyMenuContributor : IMenuContributor
{
    private readonly IConfiguration _configuration;

    public TaskyMenuContributor(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
        else if (context.Menu.Name == StandardMenus.User)
        {
            await ConfigureUserMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<CurrencyServiceResource>();
        var l1 = context.GetLocalizer<CustomerServiceResource>();
        var l2 = context.GetLocalizer<RemittanceServiceResource>();
        var l3 = context.GetLocalizer<AmlServiceResource>();

        Console.WriteLine(administration);
        administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);
         



        context.Menu.Items.Insert(0, new ApplicationMenuItem(TaskyMenus.Home,"Home","/",icon: "fas fa-home",order: 0));

        var TaskyMenu = new ApplicationMenuItem("MicroseviceStatics","MicroseviceStatics",icon: "fa fa-globe");
        if (context.IsGrantedAsync(RemittanceServicePermissions.Remittances.Default).Result)
        {
            TaskyMenu.AddItem(new ApplicationMenuItem(TaskyMenus.Customers, "Customers", url: "/customers", icon: "fa fa-users"))

                           .AddItem(new ApplicationMenuItem(TaskyMenus.Currencies, "Currencies", url: "/currencies", icon: "fa fa-money"));
        }

        context.Menu.AddItem(TaskyMenu);

        //CHECK the PERMISSION
      
   

        if (await context.IsGrantedAsync(RemittanceServicePermissions.Remittances.Default))
        {
            var rootMenuItem = new ApplicationMenuItem("RemittanceService", "RemittanceService");
            if (await context.IsGrantedAsync(RemittanceServicePermissions.Remittances.Create))
            {
                rootMenuItem.AddItem(new ApplicationMenuItem("Remittances", "Remittances", "/remittances"));
            }
            if (await context.IsGrantedAsync(RemittanceServicePermissions.Remittances.Approved))
            {
                rootMenuItem.AddItem(new ApplicationMenuItem("Remittances", "RemittanceForSupervisor", "/readyremittances"));
            }
            if (await context.IsGrantedAsync(RemittanceServicePermissions.Remittances.Released))
            {
                rootMenuItem.AddItem(new ApplicationMenuItem("Remittances", "RemittanceForReleaser", "/approvedremittances"));
            }

            rootMenuItem.AddItem(new ApplicationMenuItem("Remittances", "RemittanceStatusForAll", "/remittancesstatus"));
            context.Menu.AddItem(rootMenuItem);




            var rootMenuItemAml = new ApplicationMenuItem("AmlService", l["Menu:AmlService"]);
            if (await context.IsGrantedAsync(AmlServicePermissions.AmlRemittances.Check))
            {
                rootMenuItem.AddItem(new ApplicationMenuItem("AmlRemittances", "AmlRemittances", "/amlremittances"));
            }
            context.Menu.AddItem(rootMenuItemAml);
        }

    }
    private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
    {

        var authServerUrl = _configuration["AuthServer:Authority"] ?? "";

        context.Menu.AddItem(new ApplicationMenuItem(
            "Account.Manage",
            "Manage Your Profile",
            $"{authServerUrl.EnsureEndsWith('/')}Account/Manage?returnUrl={_configuration["App:SelfUrl"]}",
            icon: "fa fa-cog",
            order: 1000,
            null).RequireAuthenticated());

        return Task.CompletedTask;
    }

}
