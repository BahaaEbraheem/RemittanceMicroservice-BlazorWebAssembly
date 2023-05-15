using System;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

    //private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    //{
    //    //var l = context.GetLocalizer<CurrencyServiceResource>();
    //    context.Menu.Items.Insert(
    //        0,
    //        new ApplicationMenuItem(
    //            TaskyMenus.Home,
    //            "Home",
    //            "/",
    //            icon: "fas fa-home"
    //        )
    //    );
    //    context.Menu.AddItem(new ApplicationMenuItem(TaskyMenus.Currencies, displayName: "Currencies",url: "/currencies"));
    //    context.Menu.AddItem(new ApplicationMenuItem(TaskyMenus.Customers, displayName: "Customers", url: "/customers"));

    //    var administration = context.Menu.GetAdministration();
    //    Console.WriteLine(administration);
    //    administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);

    //    administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
    //    administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);

    //    return Task.CompletedTask;
    //}
    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<CurrencyServiceResource>();
        var l1 = context.GetLocalizer<CustomerServiceResource>();
        var l2 = context.GetLocalizer<RemittanceServiceResource>();

        Console.WriteLine(administration);
        administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);



        var TaskyMenu = new ApplicationMenuItem(
       TaskyMenus.Home,
        "Home",
        icon: "fa fa-book"
         );

        context.Menu.AddItem(TaskyMenu);

        //CHECK the PERMISSION
        if (context.IsGrantedAsync(RemittanceServicePermissions.Remittances.Default).Result)
        {
            TaskyMenu.AddItem(
            new ApplicationMenuItem(
                TaskyMenus.Customers,
              "Customers",
                url: "/customers"
            )
        ).AddItem(
            new ApplicationMenuItem(
                TaskyMenus.Currencies,
                "Currencies",
                url: "/currencies"
            )
        );


            context.Menu.Items.Insert(
               0,
               new ApplicationMenuItem(
                   TaskyMenus.RemittancesStatus,
                   "RemittancesStatus",
                   url: "/remittancesstatus",
                   icon: "fas fa-home",
                   order: 0
               )
           );
        }
        context.Menu.Items.Insert(
          0,
          new ApplicationMenuItem(
              TaskyMenus.Home,
              "Home",
              "/",
              icon: "fas fa-home",
              order: 0
          )
      );
        if (context.IsGrantedAsync(RemittanceServicePermissions.Remittances.Create).Result)
        {
            context.Menu.Items.Insert(
   0,
   new ApplicationMenuItem(
                TaskyMenus.Remittances,
                "Remittances",
                url: "/remittances",
       icon: "fas fa-home",
       order: 0
                  )
                 );
        }
        if (context.IsGrantedAsync(RemittanceServicePermissions.Remittances.Approved).Result)
        {
            context.Menu.Items.Insert(
   0,
   new ApplicationMenuItem(
       TaskyMenus.ReadyRemittances,
       "ReadyRemittances",
       url: "/readyremittances",
       icon: "fas fa-home",
       order: 0
                  )
                 );
        }
        if (context.IsGrantedAsync(RemittanceServicePermissions.Remittances.Released).Result)
        {
            context.Menu.Items.Insert(
   0,
   new ApplicationMenuItem(
       TaskyMenus.ApprovedRemittances,
       "ApprovedRemittances",
       url: "/approvedremittances",
       icon: "fas fa-home",
       order: 0
                  )
                 );
        }
        return Task.CompletedTask;
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
