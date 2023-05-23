using System.Linq;
using System.Threading.Tasks;
using Blazorise.DataGrid;
using Volo.Abp.Application.Dtos;
using Microsoft.AspNetCore.Components;
using Blazorise;
using Tasky.Microservice.Shared.Etos;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata;
using Tasky.Microservice.Shared.Dtos;
using Tasky.CustomerService.Customers.Dtos;
using Tasky.RemittanceService.Permissions;

namespace Tasky.Blazor.Pages.Aml_Remittance
{

    public partial class Aml_Remittance
    {

        [Inject]
        private IReadOnlyList<RemittanceEto> AmlRemittanceList { get; set; }
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;

        private int TotalCount { get; set; }
        private bool CanCreateCurrency { get; set; }
        private bool CanEditCurrency { get; set; }
        private bool CanDeleteCurrency { get; set; }

        GetRemittanceListPagedAndSortedResultRequestDto getRemittanceListPagedAndSortedResultRequestDto
           = new GetRemittanceListPagedAndSortedResultRequestDto();
      
        protected override async Task OnInitializedAsync()
        {
            await GetAmlRemittancesAsync(getRemittanceListPagedAndSortedResultRequestDto);
            await base.OnInitializedAsync();
        }
        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<RemittanceEto> e)
        {
            GetRemittanceListPagedAndSortedResultRequestDto getRemittanceListPagedAndSortedResultRequestDto =
              new GetRemittanceListPagedAndSortedResultRequestDto();
            CurrentSorting = e.Columns
            .Where(c => c.SortDirection != SortDirection.Default)
            .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
            .JoinAsString(",");
            CurrentPage = e.Page - 1;

            await GetAmlRemittancesAsync(getRemittanceListPagedAndSortedResultRequestDto);
            await InvokeAsync(StateHasChanged);
        }

        private async Task GetAmlRemittancesAsync(GetRemittanceListPagedAndSortedResultRequestDto getRemittanceListPagedAndSortedResultRequestDto)
        {
            var result = await SampleAppService.GetListRemittancesForAmlCheckerAsync(
               new GetRemittanceListPagedAndSortedResultRequestDto
               {
                   MaxResultCount = PageSize,
                   SkipCount = CurrentPage * PageSize,
                   Sorting = CurrentSorting
               }
           );
            AmlRemittanceList = result.Items;
            TotalCount = (int)result.TotalCount;

        }


        private async void CheckAML(Guid Id)
        {
           await SampleAppService.CheckAML(Id);
            await OnInitializedAsync();
            await InvokeAsync(StateHasChanged);
            
        }


    }
}
