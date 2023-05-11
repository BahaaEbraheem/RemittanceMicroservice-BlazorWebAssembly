using System.Linq;
using System.Threading.Tasks;
using Blazorise.DataGrid;
using Volo.Abp.Application.Dtos;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using System;
using Volo.Abp.BlazoriseUI;
using Microsoft.AspNetCore.Authorization;
using Tasky.CustomerService.Customers.Dtos;
using Tasky.CustomerService.Permissions;

namespace Tasky.Blazor.Pages.Customers
{
    public partial class Customers
    {

        private bool CanCreateCustomer { get; set; }
        private bool CanEditCustomer { get; set; }
        private bool CanDeleteCustomer { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await SetPermissions();
        }
        private async Task SetPermissions()
        {
            CanCreateCustomer = await AuthorizationService
                .IsGrantedAsync(CustomerServicePermissions.Customers.Create);

            CanEditCustomer = await AuthorizationService
                .IsGrantedAsync(CustomerServicePermissions.Customers.Update);

            CanDeleteCustomer = await AuthorizationService
                .IsGrantedAsync(CustomerServicePermissions.Customers.Delete);

        }
        protected override Task UpdateGetListInputAsync()
        {
            if
                (GetListInput is ISortedResultRequest sortedResultRequestInput)
            {
                sortedResultRequestInput.Sorting = CurrentSorting;
            }
            if (GetListInput is IPagedResultRequest pagedResultRequestInput)
            {
                pagedResultRequestInput.SkipCount = (CurrentPage - 1) * PageSize;
            }

            if (GetListInput is ILimitedResultRequest limitedResultRequestInput)
            {
                limitedResultRequestInput.MaxResultCount = PageSize;
            }
            return Task.CompletedTask;
        }
        protected override Task OnDataGridReadAsync(DataGridReadDataEventArgs<CustomerDto> e)
        {
            var firstName = e.Columns.FirstOrDefault(c => c.SearchValue != null && c.Field == "FirstName");
            if (firstName != null) this.GetListInput.FirstName = firstName.SearchValue.ToString();
            var lastName = e.Columns.FirstOrDefault(c => c.SearchValue != null && c.Field == "LastName");
            if (lastName != null) this.GetListInput.LastName = lastName.SearchValue.ToString();
            var fatherName = e.Columns.FirstOrDefault(c => c.SearchValue != null && c.Field == "FatherName");
            if (fatherName != null) this.GetListInput.FatherName = fatherName.SearchValue.ToString();
            var motherName = e.Columns.FirstOrDefault(c => c.SearchValue != null && c.Field == "MotherName");
            if (motherName != null) this.GetListInput.MotherName = motherName.SearchValue.ToString();
            return base.OnDataGridReadAsync(e);
        }



    }
}