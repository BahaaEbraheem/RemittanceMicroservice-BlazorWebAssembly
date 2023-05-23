using AutoMapper;
using Tasky.AmlService.Aml_Remittance;
using Tasky.Microservice.Shared.Etos;

namespace Tasky.AmlService;

public class AmlServiceApplicationAutoMapperProfile : Profile
{
    public AmlServiceApplicationAutoMapperProfile()
    {
        CreateMap<RemittanceEto, AmlRemittance>()
                .ForMember(dest => dest.RemittanceId, opt => opt.MapFrom(src => src.RemittanceId))
                .ForMember(model => model.IsDeleted, option => option.Ignore())
                .ForMember(model => model.DeleterId, option => option.Ignore())
                .ForMember(model => model.DeletionTime, option => option.Ignore())
                .ForMember(model => model.ExtraProperties, option => option.Ignore())
                .ForMember(model => model.ConcurrencyStamp, option => option.Ignore());

        CreateMap<AmlRemittance, RemittanceEto>();
    }
}
