using AutoMapper;
using Tasky.CurrencyService.Currencies;

namespace Tasky.CurrencyService;

public class CurrencyServiceApplicationAutoMapperProfile : Profile
{
    public CurrencyServiceApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
   * Alternatively, you can split your mapping configurations
   * into multiple profile classes for a better organization. */
        CreateMap<CurrencyDto, Currency>()
                .ForMember(model => model.ExtraProperties, option => option.Ignore())
             .ForMember(model => model.ConcurrencyStamp, option => option.Ignore())
             ;


        CreateMap<Currency, CurrencyDto>()
               .ForMember(model => model.LastModifierId, option => option.Ignore())
               .ForMember(model => model.CreatorId, option => option.Ignore())
               .ForMember(model => model.CreationTime, option => option.Ignore())
               .ForMember(model => model.LastModificationTime, option => option.Ignore())
               ;

        CreateMap<CreateUpdateCurrencyDto, Currency>()
              .ForMember(model => model.ExtraProperties, option => option.Ignore())
             .ForMember(model => model.ConcurrencyStamp, option => option.Ignore())
             .ForMember(model => model.Id, option => option.Ignore())
             ;

        CreateMap<CurrencyPagedAndSortedResultRequestDto, Currency>()
          .ForMember(model => model.ExtraProperties, option => option.Ignore())
         .ForMember(model => model.ConcurrencyStamp, option => option.Ignore())
         .ForMember(model => model.Id, option => option.Ignore())
         ;

        CreateMap<CurrencyPagedAndSortedResultRequestDto, CurrencyDto>()
       .ForMember(model => model.Id, option => option.Ignore());
        //  .ForMember(model => model.LastModifierId, option => option.Ignore())
        //       .ForMember(model => model.CreatorId, option => option.Ignore())
        //       .ForMember(model => model.CreationTime, option => option.Ignore())
        //       .ForMember(model => model.LastModificationTime, option => option.Ignore())
        ;
        CreateMap<CreateUpdateCurrencyDto, CurrencyDto>()
            .ForMember(model => model.Id, option => option.Ignore())
          .ForMember(model => model.LastModifierId, option => option.Ignore())
               .ForMember(model => model.CreatorId, option => option.Ignore())
               .ForMember(model => model.CreationTime, option => option.Ignore())
               .ForMember(model => model.LastModificationTime, option => option.Ignore())
               ;

    }
}
