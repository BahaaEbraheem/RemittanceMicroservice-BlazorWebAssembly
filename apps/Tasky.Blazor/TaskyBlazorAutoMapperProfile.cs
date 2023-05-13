using AutoMapper;
using Tasky.CurrencyService.Currencies;
using Tasky.CustomerService.Customers;
using Tasky.CustomerService.Customers.Dtos;

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


        //CreateMap<RemittanceDto, CreateRemittanceDto>();
        //CreateMap<CreateRemittanceDto, RemittanceDto>();
        //CreateMap<RemittanceDto, UpdateRemittanceDto>();
        //CreateMap<UpdateRemittanceDto, RemittanceDto>();



        //CreateMap<RemittanceDto, Remittance>();
        //CreateMap<RemittanceStatusDto, RemittanceDto>();
        //CreateMap<Remittance_Status, RemittanceDto>();
        //CreateMap<Remittance, RemittanceDto>();
    }
}
