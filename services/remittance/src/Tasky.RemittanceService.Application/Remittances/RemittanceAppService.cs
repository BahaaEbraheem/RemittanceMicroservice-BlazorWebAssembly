﻿using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;
using Volo.Abp.Validation;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Users;
using Nito.Disposables.Internals;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Concurrent;
using Volo.Abp.Authorization.Permissions;
using System.Data;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Tasky.RemittanceService.Permissions;
using Tasky.Microservice.Shared.Dtos;
using static Tasky.Microservice.Shared.Enums.Enums;
using Tasky.Microservice.Shared.Etos;
using Tasky.RemittanceService.Status;
using Tasky.CurrencyService.Currencies;
using Tasky.CustomerService.Customers;
using Tasky.CustomerService.Customers.Dtos;
//using AmlManagement.Permissions;

namespace Tasky.RemittanceService.Remittances;


public class RemittanceAppService : RemittanceServiceAppService, IRemittanceAppService, ITransientDependency
{
    private readonly IRemittanceRepository _remittanceRepository;
    private readonly RemitanceStatusManager _remittanceStatusManager;
    private readonly IRemittanceStatusAppService _remittanceStatusAppService;
    private readonly RemittanceManager _remittanceManager;
    private readonly IPermissionChecker _permissionChecker;
    private readonly ICurrencyAppService _currencyAppService;
    private readonly ICustomerAppService _customerAppService;
    private readonly IRemittanceStatusRepository _remittanceStatusRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IDistributedEventBus _distributedEventBus;
    public RemittanceAppService(
        IDistributedEventBus distributedEventBus,
        ICurrentUser currentUser,
        IPermissionChecker permissionChecker,
        ICustomerAppService customerAppService,
        IRemittanceStatusRepository remittanceStatusRepository,
        IRemittanceRepository remittanceRepository,
        IRemittanceStatusAppService remittanceStatusAppService,
        RemittanceManager remittanceManager,
        ICurrencyAppService currencyAppService,
        RemitanceStatusManager remittanceStatusManager)
    {

        _distributedEventBus = distributedEventBus;
        _currentUser = currentUser;
        _permissionChecker = permissionChecker;
        _remittanceRepository = remittanceRepository;
        _customerAppService = customerAppService;
        _remittanceManager = remittanceManager;
        _currencyAppService = currencyAppService;
        _remittanceStatusRepository = remittanceStatusRepository;
        _remittanceStatusAppService = remittanceStatusAppService;
        _remittanceStatusManager = remittanceStatusManager;


    }
    [Authorize(RemittanceServicePermissions.Remittances.Create)]

    public async Task<RemittanceDto> CreateAsync(CreateRemittanceDto input)
    {
        try
        {
            if (input != null)
            {
                //check if Sende Age Greater than 18
                if (!input.SenderBy.Equals(null))
                {
                    var customer = await _customerAppService.GetAsync(input.SenderBy);
                    if (customer != null)
                    {
                        var checkAge = DateTime.Now.Year - customer.BirthDate.Year;
                        if ((customer.BirthDate > DateTime.Now) || (checkAge < 18))
                        {
                            throw new UserFriendlyException("Sender Age Smaller Than 18");
                        }
                    }
                }
                //Check Type And Currency 

                if (!input.CurrencyId.Equals(null) && input.Type == RemittanceType.Internal)
                {
                    var currency = await _currencyAppService.GetAsync(input.CurrencyId);
                    if (currency == null || currency.Name != "Syrian Pound")
                    {
                        throw new UserFriendlyException("The Currency Must Be Syrian Pound");
                    }
                }
                else if (!input.CurrencyId.Equals(null) && input.Type == RemittanceType.External)
                {
                    var currency = await _currencyAppService.GetAsync(input.CurrencyId);
                    if (currency == null || currency.Name == "Syrian Pound")
                    {
                        throw new UserFriendlyException("The Currency Should Not Be Syrian Pound");
                    }
                }
                //check if Remittance contain Receiver Customer
                if (input.ReceiverBy != null)
                {
                    throw new UserFriendlyException("The Receiver Customer Should be passed on Release Remittance no in Created Remittance");
                }
                if (input.Amount == 0 || input.Amount < 0)
                {
                    throw new UserFriendlyException("The Amount Should Not Be Zero Or Smaller");
                }
                var remittance = await _remittanceManager.CreateAsync(
                 input.Amount, input.Type,
                 input.ReceiverFullName,
                 input.CreationTime,
                 input.CurrencyId,
                 input.SenderBy
                  );
                await _remittanceRepository.InsertAsync(remittance);

                var remittanceStatus = await _remittanceStatusManager.CreateAsync(remittance.Id, Remittance_Status.Draft);
                await _remittanceStatusRepository.InsertAsync(remittanceStatus);
                return ObjectMapper.Map<Remittance, RemittanceDto>(remittance);
            }
            else
            {
                throw new InvalidOperationException();
            }

        }
        catch (Exception)
        {

            throw;
        }

    }

    [Authorize(RemittanceServicePermissions.Remittances.Delete)]

    public async Task DeleteAsync(Guid id)
    {
        try
        {
            var remittanceStatus = await _remittanceStatusManager.UpdateAsync(id);
            if (remittanceStatus != null && remittanceStatus.State == Remittance_Status.Draft)
            {
                await _remittanceRepository.DeleteAsync(id);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<RemittanceDto> GetAsync(Guid id)
    {
        try
        {
            //Get the IQueryable<Remittance> from the repository
            var queryable = _remittanceRepository.GetQueryableAsync().Result.Where(a => a.Id == id);

            //Execute the query and get the remittance with currency
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(queryable);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Remittance), id);
            }

            var remittanceDto = ObjectMapper.Map<Remittance, RemittanceDto>(queryResult);
            return remittanceDto;

        }
        catch (Exception)
        {

            throw;
        }

    }

    [Authorize(RemittanceServicePermissions.Remittances.Update)]
    public async Task UpdateAsync(Guid id, UpdateRemittanceDto input)
    {
        try
        {
            if (!id.Equals(null) && input != null)
            {
                if (input.Amount == 0 || input.Amount < 0)
                {
                    throw new UserFriendlyException("The Amount Should Not Be Zero Or Smaller");
                }
                var remittanceStatus = await _remittanceStatusManager.UpdateAsync(id);
                if (remittanceStatus != null && remittanceStatus.State == Remittance_Status.Draft)
                {
                    //check if Sende Age Greater than 18
                    if (!input.SenderBy.Equals(null))
                    {
                        var customer = await _customerAppService.GetAsync(input.SenderBy);
                        if (customer != null)
                        {
                            var checkAge = DateTime.Now.Year - customer.BirthDate.Year;
                            if ((customer.BirthDate > DateTime.Now) || (checkAge < 18))
                            {
                                throw new UserFriendlyException("Sender Age Smaller Than 18");
                            }
                        }

                    }
                    //Check Type And Currency 
                    if (!input.CurrencyId.Equals(null) && input.Type == RemittanceType.Internal)
                    {
                        var currency = await _currencyAppService.GetAsync(input.CurrencyId);
                        if (currency == null || currency.Name != "Syrian Pound")
                        {
                            throw new UserFriendlyException("The Currency Must Be Syrian Pound Exeption");
                        }
                    }
                    else if (!input.CurrencyId.Equals(null) && input.Type == RemittanceType.External)
                    {
                        var currency = await _currencyAppService.GetAsync(input.CurrencyId);
                        if (currency == null || currency.Name == "Syrian Pound")
                        {
                            throw new UserFriendlyException("The Currency Should Not Be Syrian Pound");
                        }
                    }
                    //check if Remittance contain Receiver Customer
                    if (input.ReceiverBy != null)
                    {
                        throw new UserFriendlyException("The Receiver Customer Should be passed on Release Remittance no in Created Remittance");
                    }
                    var remittance = await _remittanceRepository.GetAsync(id);
                    var CheckRemittanceIfApproved = await _remittanceManager.UpdateAsync(remittance,
                        input.Amount, input.Type,
                        input.ReceiverFullName, input.CurrencyId);

                    remittance.Amount = input.Amount;
                    remittance.Type = input.Type;
                    remittance.ReceiverFullName = input.ReceiverFullName;
                    remittance.CurrencyId = input.CurrencyId;
                    remittance.SenderBy = input.SenderBy;

                    await _remittanceRepository.UpdateAsync(remittance);
                }
            }
        }
        catch (Exception)
        {

            throw;
        }


    }


    [Authorize(RemittanceServicePermissions.Remittances.Ready)]

    public async Task SetReady(RemittanceDto input)
    {
        try
        {
            if (input != null)
            {


                var remittanceStatus = await _remittanceStatusManager.UpdateAsync(input.Id);
                if (remittanceStatus != null && remittanceStatus.State == Remittance_Status.Draft)
                {
                    var remittance = await _remittanceRepository.GetAsync(input.Id);
                    remittanceStatus.State = Remittance_Status.Ready;
                    remittance.LastModifierId = CurrentUser.Id;
                    remittance.LastModificationTime = DateTime.Now;
                    var createdRemittance = await _remittanceRepository.UpdateAsync(remittance);
                    await _remittanceStatusRepository.InsertAsync(remittanceStatus);
                    var customer = await _customerAppService.GetAsync(input.SenderBy);

                    await _distributedEventBus.PublishAsync<RemittanceEto>(eventData: new RemittanceEto
                    {
                        RemittanceId = createdRemittance.Id,
                        SerialNumber = createdRemittance.SerialNumber,
                        Type = createdRemittance.Type,
                        SenderBy = createdRemittance.SenderBy,
                        Amount = createdRemittance.Amount,
                        CurrencyId = createdRemittance.CurrencyId,
                        State = remittanceStatus.State,

                        FirstName = customer.FirstName,
                        FatherName = customer.FatherName,
                        LastName = customer.LastName,
                        MotherName = customer.MotherName,
                    });
                }
            }
        }
        catch (Exception)
        {

            throw;
        }


    }

    public async Task SetAmlChecked(Guid? id)
    {
        try
        {
            if (id != null)
            {
                var remittance = await _remittanceRepository.GetAsync((Guid)id);
                var remittanceStatus = await _remittanceStatusManager.UpdateAsync((Guid)id);
                if (remittanceStatus != null && remittanceStatus.State == Remittance_Status.Ready)
                {
                    remittanceStatus.State = Remittance_Status.CheckedAML;
                    await _remittanceRepository.UpdateAsync(remittance);
                    await _remittanceStatusRepository.InsertAsync(remittanceStatus);
                }
            }
        }
        catch (Exception)
        {

            throw;
        }

    }

    [Authorize(RemittanceServicePermissions.Remittances.Approved)]

    public async Task SetApprove(RemittanceDto input)
    {
        try
        {
            if (input != null)
            {
                var remittanceStatus = await _remittanceStatusManager.UpdateAsync(input.Id);
                if (remittanceStatus != null && remittanceStatus.State == Remittance_Status.CheckedAML)
                {
                    var remittance = await _remittanceRepository.GetAsync(input.Id);
                    remittanceStatus.State = Remittance_Status.Approved;
                    remittance.ApprovedBy = CurrentUser.Id;
                    remittance.ApprovedDate = DateTime.Now;
                    await _remittanceRepository.UpdateAsync(remittance);
                    await _remittanceStatusRepository.InsertAsync(remittanceStatus);
                }
            }
        }
        catch (Exception)
        {

            throw;
        }

    }

    public async Task<List<RemittanceDto>> GetAllAsync()
    {
        return ObjectMapper.Map<List<Remittance>, List<RemittanceDto>>(await _remittanceRepository.GetAllAsync());
    }

    [Authorize(RemittanceServicePermissions.Remittances.Released)]
    public async Task SetRelease(RemittanceDto input)
    {
        try
        {
            if (input != null && !input.ReceiverBy.Equals(null))
            {
                var remittanceStatus = await _remittanceStatusManager.UpdateAsync(input.Id);
                if (remittanceStatus != null && remittanceStatus.State == Remittance_Status.Approved)
                {
                    var remittance = await _remittanceRepository.GetAsync(input.Id);
                    remittanceStatus.State = Remittance_Status.Release;
                    remittance.ReleasedBy = CurrentUser.Id;
                    remittance.ReleasedDate = DateTime.Now;
                    remittance.ReceiverBy = input.ReceiverBy;
                    var CustomerFullName = _customerAppService.GetAsync((Guid)input.ReceiverBy).Result;
                    remittance.ReceiverFullName = CustomerFullName.FirstName + " " + CustomerFullName.FatherName + " " + CustomerFullName.LastName;
                    await _remittanceRepository.UpdateAsync(remittance);
                    await _remittanceStatusRepository.InsertAsync(remittanceStatus);
                }
            }
            else
            {
                throw new InvalidOperationException("Please fill Receiver Customer");
            }
        }
        catch (Exception)
        {

            throw;
        }

    }

    public async Task<ListResultDto<CurrencyLookupDto>> GetCurrencyLookupAsync()
    {
        var currencies =await _currencyAppService.GetAllAsync();

        return new ListResultDto<CurrencyLookupDto>(
            ObjectMapper.Map<List<CurrencyDto>, List<CurrencyLookupDto>>( currencies)
        );
    }

    private static string NormalizeSorting(string sorting)
    {
        if (sorting.IsNullOrEmpty())
        {
            return $"remittance.{nameof(Remittance.ReceiverFullName)}";
        }
        else if (sorting.Contains("id", StringComparison.OrdinalIgnoreCase))
        {
            return sorting.Replace(
                "Id",
                "remittance.Id",
                StringComparison.OrdinalIgnoreCase
            );
        }
        else if (sorting.Contains("serialNumber", StringComparison.OrdinalIgnoreCase))
        {
            return sorting.Replace(
                "serialNumber",
                "remittance.SerialNumber",
                StringComparison.OrdinalIgnoreCase
            );
        }
        else if (sorting.Contains("amount", StringComparison.OrdinalIgnoreCase))
        {
            return sorting.Replace(
                "amount",
                "remittance.Amount",
                StringComparison.OrdinalIgnoreCase
            );
        }
        else if (sorting.Contains("totalAmount", StringComparison.OrdinalIgnoreCase))
        {
            return sorting.Replace(
                "totalAmount",
                "remittance.TotalAmount",
                StringComparison.OrdinalIgnoreCase
            );
        }


        else if (sorting.Contains("receiverFullName", StringComparison.OrdinalIgnoreCase))
        {
            return sorting.Replace(
                "ReceiverFullName",
                "remittance.ReceiverFullName",
                StringComparison.OrdinalIgnoreCase
            );
        }
        else if (sorting.Contains("currencyName", StringComparison.OrdinalIgnoreCase))
        {
            return sorting.Replace(
                "CurrencyName",
                "currency.Name",
                StringComparison.OrdinalIgnoreCase
            );
        }


        else if (sorting.Contains("senderName", StringComparison.OrdinalIgnoreCase))
        {
            return sorting.Replace(
                "SenderName",
                "senderCustomer.FirstName",
                StringComparison.OrdinalIgnoreCase
            );
        }
        else if (sorting.Contains("creationTime", StringComparison.OrdinalIgnoreCase))
        {
            return sorting.Replace(
                "CreationTime",
                "remittance.CreationTime",
                StringComparison.OrdinalIgnoreCase
            );
        }

        else if (sorting.Contains("state", StringComparison.OrdinalIgnoreCase))
        {
            return sorting.Replace(
                "State",
                $"remittanceStatus.{nameof(RemittanceStatus.State)}",
                StringComparison.OrdinalIgnoreCase
            );
        }
        else if (sorting.Contains("statusDate", StringComparison.OrdinalIgnoreCase))
        {
            return sorting.Replace(
                "StatusDate",
                "remittanceStatus.CreationTime",
                StringComparison.OrdinalIgnoreCase
            );
        }

        return sorting.Replace(
                "CreationTime",
                "remittance.CreationTime",
                StringComparison.OrdinalIgnoreCase
            );
    }

    public async Task<ListResultDto<CustomerLookupDto>> GetCustomerLookupAsync()
    {
        var customers = await _customerAppService.GetAllAsync();

        return new ListResultDto<CustomerLookupDto>(
            ObjectMapper.Map<List<CustomerDto>, List<CustomerLookupDto>>(customers)
        );
    }

    public async Task<PagedResultDto<RemittanceDto>> GetListRemittancesForCreator(GetRemittanceListPagedAndSortedResultRequestDto input)
    {
        bool CanCreateRemittance = await AuthorizationService
                 .IsGrantedAsync(RemittanceServicePermissions.Remittances.Create);


        //Get the IQueryable<remittance> from the repository
        var remittancequeryable = _remittanceRepository.GetQueryableAsync().Result
            .WhereIf(!input.ReceiverFullName.IsNullOrWhiteSpace(), x => x.ReceiverFullName.Contains(input.ReceiverFullName))
            .WhereIf(!input.Amount.Equals(0), x => x.Amount.ToString().Contains( input.Amount.ToString()))
            .WhereIf(!input.TotalAmount.Equals(0), x => x.TotalAmount.ToString().Contains(input.TotalAmount.ToString()))
            .WhereIf(!input.SerialNumber.IsNullOrWhiteSpace(), x => x.SerialNumber.Contains(input.SerialNumber))

          .ToList();
        //var currencies = GetCurrencyLookupAsync().Result.Items.ToList();
        //var customers = GetCustomerLookupAsync().Result.Items.ToList();
        var currencyequeryable = GetCurrencyLookupAsync().Result.Items
             .WhereIf(!input.CurrencyName.IsNullOrWhiteSpace(), x => x.Name.Contains(input.CurrencyName))
            .ToList();
        var remittance_Statusqueryable = _remittanceStatusRepository.GetQueryableAsync().Result.ToList();
        var customerqueryable = GetCustomerLookupAsync().Result.Items
            .WhereIf(!input.SenderName.IsNullOrWhiteSpace(), x => x.FirstName.Contains(input.SenderName) ||
           x.FatherName.Contains(input.SenderName) || x.LastName.Contains(input.SenderName))
            .ToList();
        var remittanceStatusQyery = from remittance_Status in remittance_Statusqueryable
                                    group remittance_Status by remittance_Status.RemittanceId into remittance_Status
                                    select remittance_Status.OrderByDescending(t => t.CreationTime).FirstOrDefault();
        var query = from remittance in remittancequeryable
                    join currency in currencyequeryable
                    on remittance.CurrencyId equals currency.Id
                    join senderCustomer in customerqueryable
                    on remittance.SenderBy equals senderCustomer.Id
                    join remittanceStatus in remittanceStatusQyery
                    on remittance.Id equals remittanceStatus.RemittanceId
                    where (remittanceStatus.State == Remittance_Status.Draft && CanCreateRemittance)
                    select new { remittance, currency, remittanceStatus, senderCustomer };
        //Paging
        query = query.AsQueryable()
            .OrderBy(NormalizeSorting(input.Sorting))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);
        //Convert the query result to a list of RemittanceDto objects
        var remittanceDtos = query.Select(x =>
        {
            var remittanceDto = ObjectMapper.Map<Remittance, RemittanceDto>(x.remittance);
            remittanceDto.SerialNumber = x.remittance.SerialNumber;
            remittanceDto.CurrencyName = _currencyAppService.GetAsync((Guid)x.remittance.CurrencyId).Result.Name;
            remittanceDto.TotalAmount = x.remittance.TotalAmount;
            remittanceDto.StatusDate = x.remittanceStatus.CreationTime;
            remittanceDto.State = x.remittanceStatus.State;
            remittanceDto.SenderName = x.senderCustomer.FirstName + " " + x.senderCustomer.FatherName + " " + x.senderCustomer.LastName;
            return remittanceDto;
        }).ToList();
        //Get the total count with another query
        var totalCount = await _remittanceRepository.GetCountAsync();
        return new PagedResultDto<RemittanceDto>(
            totalCount,
            remittanceDtos
        );
    }

    public async Task<PagedResultDto<RemittanceDto>> GetListRemittancesForSupervisor(GetRemittanceListPagedAndSortedResultRequestDto input)
    {
        try
        {
            bool CanApprovedRemittance = await AuthorizationService
                 .IsGrantedAsync(RemittanceServicePermissions.Remittances.Approved);
            //Get the IQueryable<remittance> from the repository
            var remittancequeryable = _remittanceRepository.GetQueryableAsync().Result
             .WhereIf(!input.ReceiverFullName.IsNullOrWhiteSpace(), x => x.ReceiverFullName.Contains(input.ReceiverFullName))
             .WhereIf(!input.Amount.Equals(0), x => x.Amount.ToString().Contains(input.Amount.ToString()))
             .WhereIf(!input.TotalAmount.Equals(0), x => x.TotalAmount.ToString().Contains(input.TotalAmount.ToString()))
             .WhereIf(!input.SerialNumber.IsNullOrWhiteSpace(), x => x.SerialNumber.Contains(input.SerialNumber))

           .ToList();

            var currencyequeryable = GetCurrencyLookupAsync().Result.Items
                 .WhereIf(!input.CurrencyName.IsNullOrWhiteSpace(), x => x.Name.Contains(input.CurrencyName))
                .ToList();
            var remittance_Statusqueryable = _remittanceStatusRepository.GetQueryableAsync().Result.ToList();
            var customerqueryable = GetCustomerLookupAsync().Result.Items
                .WhereIf(!input.SenderName.IsNullOrWhiteSpace(), x => x.FirstName.Contains(input.SenderName) ||
               x.FatherName.Contains(input.SenderName) || x.LastName.Contains(input.SenderName))
                .ToList();

            var remittanceStatusQyery = from remittance_Status in remittance_Statusqueryable
                                        group remittance_Status by remittance_Status.RemittanceId into remittance_Status
                                        select remittance_Status.OrderByDescending(t => t.CreationTime).FirstOrDefault();
            var query = from remittance in remittancequeryable
                        join currency in currencyequeryable
                        on remittance.CurrencyId equals currency.Id
                        join senderCustomer in customerqueryable
                        on remittance.SenderBy equals senderCustomer.Id
                        join remittanceStatus in remittanceStatusQyery
                        on remittance.Id equals remittanceStatus.RemittanceId
                        where (remittanceStatus.State == Remittance_Status.CheckedAML && CanApprovedRemittance)
                        select new { remittance, currency, remittanceStatus, senderCustomer };

            //Paging
            query = query.AsQueryable()
         .OrderBy(NormalizeSorting(input.Sorting))
         .Skip(input.SkipCount)
         .Take(input.MaxResultCount);

            //Convert the query result to a list of RemittanceDto objects
            var remittanceDtos = query.Select(x =>
            {
                var remittanceDto = ObjectMapper.Map<Remittance, RemittanceDto>(x.remittance);
                remittanceDto.CurrencyName = _currencyAppService.GetAsync((Guid)x.remittance.CurrencyId).Result.Name;
                remittanceDto.SerialNumber = x.remittance.SerialNumber;
                remittanceDto.TotalAmount = x.remittance.TotalAmount;
                remittanceDto.StatusDate = x.remittanceStatus.CreationTime;
                remittanceDto.State = x.remittanceStatus.State;
                remittanceDto.SenderName = x.senderCustomer.FirstName + " " + x.senderCustomer.FatherName + " " + x.senderCustomer.LastName;
                return remittanceDto;
            }).ToList();

            //Get the total count with another query
            var totalCount = await _remittanceRepository.GetCountAsync();

            return new PagedResultDto<RemittanceDto>(
                totalCount,
                remittanceDtos
            );
        }
        catch (Exception)
        {

            throw;
        }


    }

    [Authorize(RemittanceServicePermissions.Remittances.Released)]

    public async Task<PagedResultDto<RemittanceDto>> GetListRemittancesForReleaser(GetRemittanceListPagedAndSortedResultRequestDto input)
    {
        try
        {
            bool CanReleaseRemittance = await AuthorizationService
            .IsGrantedAsync(RemittanceServicePermissions.Remittances.Released);
            //Get the IQueryable<remittance> from the repository
            var remittancequeryable = _remittanceRepository.GetQueryableAsync().Result
                  .WhereIf(!input.ReceiverFullName.IsNullOrWhiteSpace(), x => x.ReceiverFullName.Contains(input.ReceiverFullName))
                  .WhereIf(!input.Amount.Equals(0), x => x.Amount.ToString().Contains(input.Amount.ToString()))
                  .WhereIf(!input.TotalAmount.Equals(0), x => x.TotalAmount.ToString().Contains(input.TotalAmount.ToString()))
                  .WhereIf(!input.SerialNumber.IsNullOrWhiteSpace(), x => x.SerialNumber.Contains(input.SerialNumber))

                .ToList();

            var currencyequeryable = GetCurrencyLookupAsync().Result.Items
               .WhereIf(!input.CurrencyName.IsNullOrWhiteSpace(), x => x.Name.Contains(input.CurrencyName))
              .ToList();
            var remittance_Statusqueryable = _remittanceStatusRepository.GetQueryableAsync().Result.ToList();
            var customerqueryable = GetCustomerLookupAsync().Result.Items
                .WhereIf(!input.SenderName.IsNullOrWhiteSpace(), x => x.FirstName.Contains(input.SenderName) ||
               x.FatherName.Contains(input.SenderName) || x.LastName.Contains(input.SenderName))
                .ToList();
            var remittanceStatusQyery = from remittance_Status in remittance_Statusqueryable
                                        group remittance_Status by remittance_Status.RemittanceId into remittance_Status
                                        select remittance_Status.OrderByDescending(t => t.CreationTime).FirstOrDefault();
            var query = from remittance in remittancequeryable
                        join currency in currencyequeryable
                        on remittance.CurrencyId equals currency.Id
                        join senderCustomer in customerqueryable
                        on remittance.SenderBy equals senderCustomer.Id
                        join remittanceStatus in remittanceStatusQyery
                        on remittance.Id equals remittanceStatus.RemittanceId
                        where (remittanceStatus.State == Remittance_Status.Approved && CanReleaseRemittance)
                        select new { remittance, currency, remittanceStatus, senderCustomer };
            //Paging
            query = query.AsQueryable()
                .OrderBy(NormalizeSorting(input.Sorting))
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);
            //Convert the query result to a list of RemittanceDto objects
            var remittanceDtos = query.Select(x =>
            {
                var remittanceDto = ObjectMapper.Map<Remittance, RemittanceDto>(x.remittance);
                remittanceDto.CurrencyName = _currencyAppService.GetAsync((Guid)x.remittance.CurrencyId).Result.Name;
                remittanceDto.SerialNumber = x.remittance.SerialNumber;
                remittanceDto.TotalAmount = x.remittance.TotalAmount;
                remittanceDto.StatusDate = x.remittanceStatus.CreationTime;
                remittanceDto.State = x.remittanceStatus.State;
                remittanceDto.SenderName = x.senderCustomer.FirstName + " " + x.senderCustomer.FatherName + " " + x.senderCustomer.LastName;
                //remittanceDto.ReceiverName = x.receiverCustomer.FirstName + " " + x.receiverCustomer.FatherName + " " + x.receiverCustomer.LastName;
                return remittanceDto;
            }).ToList();

            //Get the total count with another query
            var totalCount = await _remittanceRepository.GetCountAsync();

            return new PagedResultDto<RemittanceDto>(
                totalCount,
                remittanceDtos
            );
        }
        catch (Exception)
        {

            throw;
        }


    }

    public async Task<PagedResultDto<RemittanceDto>> GetListRemittancesStatusAsync(GetRemittanceListPagedAndSortedResultRequestDto input)
    {

        var remittancequeryable = _remittanceRepository.GetQueryableAsync().Result
            .WhereIf(!input.ReceiverFullName.IsNullOrWhiteSpace(), x => x.ReceiverFullName.Contains(input.ReceiverFullName))
            .WhereIf(!input.Amount.Equals(0), x => x.Amount.ToString().Contains(input.Amount.ToString()))
            .WhereIf(!input.TotalAmount.Equals(0), x => x.TotalAmount.ToString().Contains(input.TotalAmount.ToString()))
            .WhereIf(!input.SerialNumber.IsNullOrWhiteSpace(), x => x.SerialNumber.Contains(input.SerialNumber))

          .ToList();


        var currencyequeryable = GetCurrencyLookupAsync().Result.Items
           .WhereIf(!input.CurrencyName.IsNullOrWhiteSpace(), x => x.Name.Contains(input.CurrencyName))
          .ToList();


        var remittance_Statusqueryable = _remittanceStatusRepository.GetQueryableAsync().Result.ToList();
        var customerqueryable = GetCustomerLookupAsync().Result.Items
               .WhereIf(!input.SenderName.IsNullOrWhiteSpace(), x => x.FirstName.Contains(input.SenderName) ||
              x.FatherName.Contains(input.SenderName) || x.LastName.Contains(input.SenderName))
               .ToList();

        var remittanceStatusQyery = from remittance_Status in remittance_Statusqueryable
                                    group remittance_Status by remittance_Status.RemittanceId into remittance_Status
                                    select remittance_Status.OrderByDescending(t => t.CreationTime).FirstOrDefault();

        var query = from remittance in remittancequeryable
                    join currency in currencyequeryable
                    on remittance.CurrencyId equals currency.Id
                    join senderCustomer in customerqueryable
                    on remittance.SenderBy equals senderCustomer.Id

                    join remittanceStatus in remittanceStatusQyery
                    on remittance.Id equals remittanceStatus.RemittanceId
                    select new { remittance, currency, remittanceStatus, senderCustomer };
        //Paging
        query = query.AsQueryable()
            //.OrderBy(x => x.remittance.ReceiverFullName).ThenBy(x => x.remittanceStatus.State)
            .OrderBy(NormalizeSorting(input.Sorting))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);
        //Convert the query result to a list of RemittanceDto objects
        var remittanceDtos = query.Select(x =>
        {
            var remittanceDto = ObjectMapper.Map<Remittance, RemittanceDto>(x.remittance);
            remittanceDto.SerialNumber = x.remittance.SerialNumber;
            remittanceDto.CurrencyName = _currencyAppService.GetAsync((Guid)x.remittance.CurrencyId).Result.Name;
            remittanceDto.TotalAmount = x.remittance.TotalAmount;
            remittanceDto.StatusDate = x.remittanceStatus.CreationTime;
            remittanceDto.State = x.remittanceStatus.State;
            remittanceDto.SenderName = x.senderCustomer.FirstName + " " + x.senderCustomer.FatherName + " " + x.senderCustomer.LastName;
            return remittanceDto;
        }).ToList();

        //Get the total count with another query
        var totalCount = await _remittanceRepository.GetCountAsync();
        return new PagedResultDto<RemittanceDto>(
            totalCount,
            remittanceDtos
        );

    }

    public async Task<PagedResultDto<RemittanceDto>> GetListAsync(GetRemittanceListDto input)
    {
        try
        {

            //var filter_ = ObjectMapper.Map<GetRemittanceListDto, Remittance>(input);
            //var remittancequeryable = _remittanceRepository.GetListRemittancesStatusAsync(input.SkipCount, input.MaxResultCount, input.Sorting, filter_).Result.ToList();
            var remittancequeryable = _remittanceRepository
                .GetQueryableAsync().Result
              .WhereIf(!input.Filter.IsNullOrWhiteSpace(), x => x.ReceiverFullName.Contains(input.Filter))
              .WhereIf(!input.Filter.IsNullOrWhiteSpace(), x => x.Amount.ToString().Contains(input.Filter.ToString()))
             .WhereIf(!input.Filter.IsNullOrWhiteSpace(), x => x.TotalAmount.ToString().Contains(input.Filter.ToString()))
             .WhereIf(!input.Filter.IsNullOrWhiteSpace(), x => x.SerialNumber.Contains(input.Filter))
             .WhereIf(!input.Filter.IsNullOrWhiteSpace(), x => Enum.GetName(x.Type).Contains(input.Filter))
             .ToList();

            //var remittancequeryable = _remittanceRepository.GetQueryableAsync().Result.ToList();

            var remittance_Statusqueryable = _remittanceStatusRepository.GetQueryableAsync().Result.ToList();


            var remittanceStatusQyery = from remittance_Status in remittance_Statusqueryable
                                        group remittance_Status by remittance_Status.RemittanceId into remittance_Status
                                        select remittance_Status.OrderByDescending(t => t.CreationTime).FirstOrDefault();
            var currencies = GetCurrencyLookupAsync().Result.Items.ToList();
            var customers = GetCustomerLookupAsync().Result.Items.ToList();
            var query = (from remittance in remittancequeryable
                         join currency in currencies
                         on remittance.CurrencyId equals currency.Id
                         join senderCustomer in customers
                         on remittance.SenderBy equals senderCustomer.Id
                         join receiverCustomer in customers
                        on remittance.SenderBy equals receiverCustomer.Id
                         join remittanceStatus in remittanceStatusQyery
                         on remittance.Id.ToString() equals remittanceStatus.RemittanceId.ToString()
                         select new { remittance, currency, remittanceStatus, senderCustomer, receiverCustomer });


            //Paging
            query = query.AsQueryable()
                .OrderBy(NormalizeSorting(input.Sorting))
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Convert the query result to a list of RemittanceDto objects
            var remittanceDtos = query.ToList().Select(x =>
            {
                var remittanceDto = ObjectMapper.Map<Remittance, RemittanceDto>(x.remittance);
                remittanceDto.SerialNumber = x.remittance.SerialNumber;
                remittanceDto.CurrencyName = _currencyAppService.GetAsync((Guid)x.remittance.CurrencyId).Result.Name;
                remittanceDto.TotalAmount = x.remittance.TotalAmount;
                remittanceDto.SenderName = x.senderCustomer.FirstName + " " + x.senderCustomer.FatherName + " " + x.senderCustomer.LastName;
                return remittanceDto;
            }).ToList();

            //Get the total count with another query
            var totalCount = await _remittanceRepository.GetCountAsync();

            return new PagedResultDto<RemittanceDto>(
                totalCount,
                remittanceDtos
            );
        }
        catch (Exception)
        {

            throw;
        }


    }



}



