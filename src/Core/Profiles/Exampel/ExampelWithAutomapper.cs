using AutoMapper;

public class CarProfile : Profile
{
    public CarProfile()
    {
        /*        
        CreateMap<CarDto, Car>()
            .ForMember(_ => _.ModelYear, opt => opt.MapFrom(_ => _.ModelYear.ToUniversalTime()))
            .ForMember(_ => _.NextServingDate, opt => opt.MapFrom(_ => _.NextServingDate.ToUniversalTime()))
            .ForMember(_ => _.LastServingDate, opt => opt.MapFrom(_ => _.LastServingDate.ToUniversalTime()));

        CreateMap<Car, CarDto>();

        CreateMap<CreateOrUpdateCarRequest, CarDto>();

        CreateMap<CarDto, CarResponse>(); 
        */
    }
}
