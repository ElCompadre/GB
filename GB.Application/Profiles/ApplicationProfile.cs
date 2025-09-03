using AutoMapper;
using GB.Domain.Entities;
using GB.Domain.Models;

namespace GB.Application.Profiles;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<Brasserie, BrasserieDTO>().ReverseMap();
        CreateMap<CreateBrasserieModel, BrasserieDTO>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Bieres, opt => opt.Ignore())
            .ForMember(dest => dest.GrossisteBrasseries, opt => opt.Ignore());
        CreateMap<Biere, BiereDTO>().ReverseMap();
        CreateMap<CreateBiereModel, BiereDTO>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.GrossisteBieres, opt => opt.Ignore())
            .ForMember(dest => dest.Brasserie, opt => opt.Ignore());
        CreateMap<Grossiste, GrossisteDTO>().ReverseMap();
        CreateMap<CreateGrossisteModel, GrossisteDTO>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.GrossisteBieres, opt => opt.Ignore());
        CreateMap<GrossisteBiere, GrossisteBiereDTO>().ReverseMap();

        CreateMap<BrasserieDTO, BrasserieModel>().ReverseMap();
        CreateMap<BrasserieDTO, GetBrasserieByIdResponseModel>()
            .ForMember(dest => dest.Bieres, opt => opt.MapFrom(src => src.Bieres));
        CreateMap<BiereDTO, BiereModel>().ReverseMap();
        CreateMap<BiereDTO, GetBrasserieByIdBiereResponseModel>()
            .ForMember(dest => dest.Grossistes, 
                opt => opt.MapFrom(src => src.GrossisteBieres.Select(gb => gb.Grossiste)));
        CreateMap<GrossisteDTO, GrossisteModel>().ReverseMap();
        CreateMap<GrossisteDTO, GetBrasserieByIdGrossisteResponseModel>();
        CreateMap<GrossisteBiereDTO, GrossisteBiereModel>().ReverseMap();
    }
}