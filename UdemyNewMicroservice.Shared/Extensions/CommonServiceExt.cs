using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
namespace UdemyNewMicroservice.Shared.Extensions;

public static class CommonServiceExt
{

    public static IServiceCollection AddCommonServiceExt(this IServiceCollection services, Type assembly)
    {
        services.AddHttpContextAccessor();
        services.AddMediatR(x=>x.RegisterServicesFromAssemblyContaining(assembly));
        services.AddValidatorsFromAssemblyContaining(assembly);


        return services;
    }

}
