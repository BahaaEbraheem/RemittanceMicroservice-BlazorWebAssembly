using AutoMapper;
using Tasky.CurrencyService.Currencies;
using Tasky.CustomerService.Customers.Dtos;
using Tasky.Microservice.Shared.Dtos;
using Tasky.RemittanceService.Remittances;
using Tasky.RemittanceService.Status;
using Tasky.RemittanceService.Status.Dtos;

namespace Tasky.RemittanceService;
public class RemittanceServiceApplicationAutoMapperProfile : Profile
{
    public RemittanceServiceApplicationAutoMapperProfile()
    {
        CreateMap<Remittance, RemittanceDto>()
              .ForMember(model => model.SenderName, option => option.Ignore())
             .ForMember(model => model.ReceiverName, option => option.Ignore())
             .ForMember(model => model.CurrencyName, option => option.Ignore())
             .ForMember(model => model.State, option => option.Ignore())
             .ForMember(model => model.StatusDate, option => option.Ignore());





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
              .ForMember(model => model.Id, option => option.Ignore())
              .ForMember(model => model.ExtraProperties, option => option.Ignore())
               .ForMember(model => model.ConcurrencyStamp, option => option.Ignore())
               .ForMember(model => model.Status, option => option.Ignore());

        CreateMap<RemittanceStatus, RemittanceStatusDto>();

        CreateMap<CurrencyDto, CurrencyLookupDto>();
        CreateMap<CustomerDto, CustomerLookupDto>();




        CreateMap<GetRemittanceListPagedAndSortedResultRequestDto, RemittanceDto>()
          .ForMember(model => model.Id, option => option.Ignore())
        .ForMember(model => model.ReceiverName, option => option.Ignore());
        CreateMap<RemittanceDto,GetRemittanceListPagedAndSortedResultRequestDto>()
               .ForMember(model => model.IsDeleted, option => option.Ignore())
            .ForMember(model => model.DeleterId, option => option.Ignore())
            .ForMember(model => model.DeletionTime, option => option.Ignore())
            .ForMember(model => model.Sorting, option => option.Ignore())
            .ForMember(model => model.SkipCount, option => option.Ignore())
            .ForMember(model => model.MaxResultCount, option => option.Ignore())
            ;





        CreateMap<Remittance, RemittanceDto>()
             .ForMember(model => model.SenderName, option => option.Ignore())
            .ForMember(model => model.ReceiverName, option => option.Ignore())
            .ForMember(model => model.CurrencyName, option => option.Ignore())
            .ForMember(model => model.State, option => option.Ignore())
            .ForMember(model => model.StatusDate, option => option.Ignore());





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



        CreateMap<RemittanceStatus, RemittanceStatusDto>();

        CreateMap<CurrencyDto, CurrencyLookupDto>();
        CreateMap<CustomerDto, CustomerLookupDto>();
        //CreateMap<ICollection <CurrencyDto>, ICollection<CurrencyLookupDto> >();
    }
}
