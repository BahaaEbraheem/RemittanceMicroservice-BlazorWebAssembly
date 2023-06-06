using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Authorization;
using Tasky.AmlService.Aml_Person;
using Tasky.AmlService.Aml_Remittance;
using Tasky.AmlService.Permissions;
using Tasky.Microservice.Shared.Dtos;
using Tasky.Microservice.Shared.Etos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Users;
using static Tasky.Microservice.Shared.Enums.Enums;

namespace Tasky.AmlService.Samples;


[Authorize(AmlServicePermissions.AmlRemittances.Default)]

public class SampleAppService : AmlServiceAppService, ISampleAppService, ITransientDependency
{

    private readonly AmlRemittanceManager _amlRemittanceManager;
    private readonly IAmlPersonRepository _amlPersonRepository;
    private readonly IAmlRemittanceRepository _amlRemittanceRepository;
    private readonly IDistributedEventBus _distributedEventBus;

    public SampleAppService(
        AmlRemittanceManager amlRemittanceManager,
        IAmlRemittanceRepository amlRemittanceRepository,
        IDistributedEventBus distributedEventBus)
    {

        _amlRemittanceManager = amlRemittanceManager;
        _amlRemittanceRepository = amlRemittanceRepository;
        _distributedEventBus = distributedEventBus;

    }


    public async  Task<PagedResultDto<RemittanceEto>> GetListRemittancesForAmlCheckerAsync(GetRemittanceListPagedAndSortedResultRequestDto input)
    {
        try
        {
            bool CanCheckAmlRemittances = await AuthorizationService
                 .IsGrantedAsync(AmlServicePermissions.AmlRemittances.Check);
            //Get the IQueryable<remittance> from the repository
            var remittancequeryable = _amlRemittanceRepository.GetQueryableAsync().Result
             .WhereIf(!input.Amount.Equals(0), x => x.Amount.ToString().Contains(input.Amount.ToString()))
             .WhereIf(!input.SerialNumber.IsNullOrWhiteSpace(), x => x.SerialNumber.Contains(input.SerialNumber))
           .ToList();

            var query = from remittance in remittancequeryable
                        where (remittance.State == Remittance_Status.Ready && CanCheckAmlRemittances)
                        select new { remittance };

            //Paging
            query = query.AsQueryable()
         .Skip(input.SkipCount)
         .Take(input.MaxResultCount);

            //Convert the query result to a list of RemittanceDto objects
            var remittanceEtos = query.Select(x =>
            {
                var remittanceEto = ObjectMapper.Map<AmlRemittance, RemittanceEto>(x.remittance);
                remittanceEto.State = x.remittance.State;
                remittanceEto.CreationTime = x.remittance.CreationTime;
                remittanceEto.CreatorId = x.remittance.CreatorId;
                return remittanceEto;
            }).ToList();

            //Get the total count with another query
            var totalCount = await _amlRemittanceRepository.GetCountAsync();

            return new PagedResultDto<RemittanceEto>(
                totalCount,
                remittanceEtos
            );
        }
        catch (Exception)
        {

            throw;
        }


    }

    [Authorize(AmlServicePermissions.AmlRemittances.Check)]

    public async  Task CheckAML(Guid id)
    {
        try
        {
            if (id != null)
            {
                var remittance = await _amlRemittanceManager.GetAsync(id);

                if (remittance != null && remittance.State == Remittance_Status.Ready)
                {
                    var amlPerson = _amlRemittanceManager.GetAmlPersonByFirstAndFatherAndLastName(
                        remittance.FirstName, remittance.FatherName, remittance.LastName).Result;
                    if (amlPerson != null)
                    {
                        throw new UserFriendlyException("this Customer is Aml");
                    }

                    remittance.State = Remittance_Status.CheckedAML;

                    await _distributedEventBus.PublishAsync<RemittanceAfterCheckedAmlEto>(eventData: new RemittanceAfterCheckedAmlEto
                    {
                        RemittanceId = remittance.RemittanceId,
                    });
                    await _amlRemittanceManager.UpdateAsync(remittance);

                }
            }
        }

        catch (Exception)
        {

            throw;
        }
    }

    public  Task<SampleDto> GetAsync()
    {
        return Task.FromResult(
            new SampleDto
            {
                Value = 42
            }
        );
    }

    [Authorize]
    public  Task<SampleDto> GetAuthorizedAsync()
    {
        return Task.FromResult(
            new SampleDto
            {
                Value = 42
            }
        );
    }

}
