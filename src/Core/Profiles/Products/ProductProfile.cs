using AutoMapper;
using Core.Entities.Products;
using Core.Responses.Products;
using Core.Utilities;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductResponse>();
    }
}
