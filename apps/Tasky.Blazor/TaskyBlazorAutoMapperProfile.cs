using AutoMapper;
using Tasky.CurrencyService.Currencies;
using Tasky.CustomerService.Customers;
using Tasky.CustomerService.Customers.Dtos;
using Tasky.Microservice.Shared.Dtos;
using Tasky.RemittanceService.Remittances;
using Tasky.RemittanceService.Status;
using Tasky.RemittanceService.Status.Dtos;
using static Tasky.Microservice.Shared.Enums.Enums;

namespace Tasky.Blazor;

public class TaskyBlazorAutoMapperProfile : Profile
{
    public TaskyBlazorAutoMapperProfile()
    {
        CreateMap<CurrencyDto, Currency>();
        CreateMap<Currency, CurrencyDto>();
        CreateMap<CurrencyDto, CreateUpdateCurrencyDto>();


        //CreateMap<CustomerDto, CreateCustomerDto>();
        CreateMap<CustomerDto, Customer>();
        CreateMap<Customer, CustomerDto>();
        //CreateMap<CustomerDto, UpdateCustomerDto>();
        CreateMap<CustomerDto, CreateUpdateCustomerDto>();
        //CreateMap<RemittanceStatusDto, CreateUpdateRemittanceStatusDto>();


        CreateMap<RemittanceDto, CreateRemittanceDto>();
        CreateMap<CreateRemittanceDto, RemittanceDto>();
        CreateMap<RemittanceDto, UpdateRemittanceDto>();
        CreateMap<UpdateRemittanceDto, RemittanceDto>();



        CreateMap<RemittanceDto, Remittance>();
        CreateMap<RemittanceStatusDto, RemittanceDto>();
        CreateMap<Remittance_Status, RemittanceDto>();
        CreateMap<Remittance, RemittanceDto>();



        CreateMap<RemittanceStatus, RemittanceStatusDto>();
        CreateMap<CreateUpdateRemittanceStatusDto, RemittanceStatus>()
            .ForMember(model => model.IsDeleted, option => option.Ignore())
            .ForMember(model => model.DeleterId, option => option.Ignore())
            .ForMember(model => model.DeletionTime, option => option.Ignore())
            .ForMember(model => model.Id, option => option.Ignore())
                .ForMember(model => model.LastModifierId, option => option.Ignore())
               .ForMember(model => model.CreatorId, option => option.Ignore())
               .ForMember(model => model.CreationTime, option => option.Ignore())
               .ForMember(model => model.LastModificationTime, option => option.Ignore());


   

        CreateMap<CreateUpdateRemittanceStatusDto, RemittanceStatusDto>()
            .ForMember(model => model.Id, option => option.Ignore())
                .ForMember(model => model.LastModifierId, option => option.Ignore())
               .ForMember(model => model.CreatorId, option => option.Ignore())
               .ForMember(model => model.CreationTime, option => option.Ignore())
               .ForMember(model => model.LastModificationTime, option => option.Ignore());

        CreateMap<GetRemittanceListPagedAndSortedResultRequestDto, Remittance>()
              .ForMember(model => model.ExtraProperties, option => option.Ignore())
               .ForMember(model => model.ConcurrencyStamp, option => option.Ignore())
               .ForMember(model => model.Status, option => option.Ignore());
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

       

    }
}
