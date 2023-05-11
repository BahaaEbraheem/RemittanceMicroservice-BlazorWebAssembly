using AutoMapper;
using Tasky.CustomerService.Customers.Dtos;
using Tasky.CustomerService.Customers;

namespace Tasky.CustomerService;

public class CustomerServiceApplicationAutoMapperProfile : Profile
{
    public CustomerServiceApplicationAutoMapperProfile()
    {
        CreateMap<CustomerDto, Customer>()
              .ForMember(model => model.ExtraProperties, option => option.Ignore())
                .ForMember(model => model.ConcurrencyStamp, option => option.Ignore())
                .ForMember(model => model.IsDeleted, option => option.Ignore())
                .ForMember(model => model.DeleterId, option => option.Ignore())
                .ForMember(model => model.DeletionTime, option => option.Ignore());
        CreateMap<Customer, CustomerDto>();

        CreateMap<CreateUpdateCustomerDto, Customer>()
               .ForMember(model => model.ExtraProperties, option => option.Ignore())
               .ForMember(model => model.ConcurrencyStamp, option => option.Ignore())
               .ForMember(model => model.Id, option => option.Ignore())
               .ForMember(model => model.IsDeleted, option => option.Ignore())
               .ForMember(model => model.DeleterId, option => option.Ignore())
               .ForMember(model => model.DeletionTime, option => option.Ignore())
               .ForMember(model => model.LastModifierId, option => option.Ignore())
               .ForMember(model => model.CreatorId, option => option.Ignore())
               .ForMember(model => model.CreationTime, option => option.Ignore())
               .ForMember(model => model.LastModificationTime, option => option.Ignore());

        CreateMap<CustomerPagedAndSortedResultRequestDto, Customer>()
                     .ForMember(model => model.ExtraProperties, option => option.Ignore())
         .ForMember(model => model.ConcurrencyStamp, option => option.Ignore())
         .ForMember(model => model.Id, option => option.Ignore())
        .ForMember(model => model.BirthDate, option => option.Ignore())
        .ForMember(model => model.Gender, option => option.Ignore())
       .ForMember(model => model.Phone, option => option.Ignore())
       .ForMember(model => model.Address, option => option.Ignore())
               .ForMember(model => model.IsDeleted, option => option.Ignore())
             .ForMember(model => model.DeleterId, option => option.Ignore())
            .ForMember(model => model.DeletionTime, option => option.Ignore())
              .ForMember(model => model.LastModifierId, option => option.Ignore())
              .ForMember(model => model.CreatorId, option => option.Ignore())
              .ForMember(model => model.CreationTime, option => option.Ignore())
              .ForMember(model => model.LastModificationTime, option => option.Ignore());




        CreateMap<CustomerPagedAndSortedResultRequestDto, CustomerDto>()
      .ForMember(model => model.Id, option => option.Ignore());

        ;
        CreateMap<CreateUpdateCustomerDto, CustomerDto>()
            .ForMember(model => model.Id, option => option.Ignore());
        //.ForMember(model => model.LastModifierId, option => option.Ignore())
        //     .ForMember(model => model.CreatorId, option => option.Ignore())
        //     .ForMember(model => model.CreationTime, option => option.Ignore())
        //     .ForMember(model => model.LastModificationTime, option => option.Ignore());
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
    }
}
