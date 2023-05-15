﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Microsoft.AspNetCore.Components;
using Blazorise.Extensions;
using Tasky.Microservice.Shared.Dtos;
using Tasky.RemittanceService.Permissions;

namespace Tasky.Blazor.Pages.RemittancesStatus
{
    public partial class RemittancesStatus
    {

        [Inject]
        private IReadOnlyList<RemittanceDto> RemittanceList { get; set; }

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        [Parameter]
        public EventCallback<string> OnSearchChanged { get; set; }
        public string SearchTerm { get; set; }
        GetRemittanceListPagedAndSortedResultRequestDto getRemittanceListPagedAndSortedResultRequestDto
                   = new GetRemittanceListPagedAndSortedResultRequestDto();
        IReadOnlyList<CurrencyLookupDto> currencyList = Array.Empty<CurrencyLookupDto>();
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }

        private int TotalCount { get; set; }


        private bool CanCreateRemittance { get; set; }
        private bool CanEditRemittance { get; set; }
        private bool CanDeleteRemittance { get; set; }
        private bool CanApprovedRemittance { get; set; }
        private bool CanReleaseRemittance { get; set; }
        private bool CanReadyRemittance { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await GetRemittancesAsync(getRemittanceListPagedAndSortedResultRequestDto);
            currencyList = (await RemittanceAppService.GetCurrencyLookupAsync()).Items;
        }
        private async Task SetPermissionsAsync()
        {
            CanCreateRemittance = await AuthorizationService
                .IsGrantedAsync(RemittanceServicePermissions.Remittances.Create);
            CanEditRemittance = await AuthorizationService
                .IsGrantedAsync(RemittanceServicePermissions.Remittances.Update);
            CanDeleteRemittance = await AuthorizationService
                .IsGrantedAsync(RemittanceServicePermissions.Remittances.Delete);
            CanApprovedRemittance = await AuthorizationService
                .IsGrantedAsync(RemittanceServicePermissions.Remittances.Approved);
            CanReleaseRemittance = await AuthorizationService
                .IsGrantedAsync(RemittanceServicePermissions.Remittances.Released);
            CanReadyRemittance = await AuthorizationService
             .IsGrantedAsync(RemittanceServicePermissions.Remittances.Ready);

        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<RemittanceDto> e)
        {
            GetRemittanceListPagedAndSortedResultRequestDto getRemittanceListPagedAndSortedResultRequestDto
                = new GetRemittanceListPagedAndSortedResultRequestDto();
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page - 1;



            var receiverFullName = e.Columns.FirstOrDefault(c => c.SearchValue != null && c.Field == "ReceiverFullName");
            if (receiverFullName != null)
                getRemittanceListPagedAndSortedResultRequestDto.ReceiverFullName = receiverFullName.SearchValue.ToString();

            var currencyName = e.Columns.FirstOrDefault(c => c.SearchValue != null && c.Field == "CurrencyName");
            if (currencyName != null)
                getRemittanceListPagedAndSortedResultRequestDto.CurrencyName = currencyName.SearchValue.ToString();

            var senderName = e.Columns.FirstOrDefault(c => c.SearchValue != null && c.Field == "SenderName");
            if (senderName != null)
                getRemittanceListPagedAndSortedResultRequestDto.SenderName = senderName.SearchValue.ToString();


            var amount = e.Columns.FirstOrDefault(c => c.SearchValue != null && c.Field == "Amount");
            if (amount != null)
            {
                if (amount.SearchValue.ToString() != "")
                    getRemittanceListPagedAndSortedResultRequestDto.Amount = double.Parse((string)amount.SearchValue);
            }
            var totalAmount = e.Columns.FirstOrDefault(c => c.SearchValue != null && c.Field == "TotalAmount");
            if (totalAmount != null)
            {
                if (totalAmount.SearchValue.ToString() != "")
                    getRemittanceListPagedAndSortedResultRequestDto.TotalAmount = double.Parse((string)totalAmount.SearchValue);
            }

            var serialNumber = e.Columns.FirstOrDefault(c => c.SearchValue != null && c.Field == "SerialNumber");
            if (serialNumber != null)
                getRemittanceListPagedAndSortedResultRequestDto.SerialNumber = serialNumber.SearchValue.ToString();

            await GetRemittancesAsync(getRemittanceListPagedAndSortedResultRequestDto);
            await InvokeAsync(StateHasChanged);
        }
        private async Task GetRemittancesAsync(GetRemittanceListPagedAndSortedResultRequestDto getRemittanceListPagedAndSortedResultRequestDto)
        {

            PagedResultDto<RemittanceDto> result = new PagedResultDto<RemittanceDto>();

            result = await RemittanceAppService.GetListRemittancesStatusAsync(
               new GetRemittanceListPagedAndSortedResultRequestDto
               {
                   ReceiverFullName = getRemittanceListPagedAndSortedResultRequestDto.ReceiverFullName,
                   SenderName = getRemittanceListPagedAndSortedResultRequestDto.SenderName,
                   CurrencyName = getRemittanceListPagedAndSortedResultRequestDto.CurrencyName,
                   Amount = getRemittanceListPagedAndSortedResultRequestDto.Amount,
                   TotalAmount = getRemittanceListPagedAndSortedResultRequestDto.TotalAmount,
                   SerialNumber = getRemittanceListPagedAndSortedResultRequestDto.SerialNumber,

                   MaxResultCount = PageSize,
                   SkipCount = CurrentPage * PageSize,
                   Sorting = CurrentSorting
               }
           );
            RemittanceList = result.Items;
            TotalCount = (int)result.TotalCount;
        }
    }
}