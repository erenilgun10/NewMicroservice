using MediatR;
using Microsoft.EntityFrameworkCore;
using UdemyNewMicroservice.Catalog.API.Features.Categories.Dto;
using UdemyNewMicroservice.Catalog.API.Repositories;
using UdemyNewMicroservice.Shared;
using UdemyNewMicroservice.Shared.Extensions;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories.GetAll;

public static class GetAllCategoryEndpoint
{

    public static RouteGroupBuilder GetAllCategoryGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IMediator mediator) => (await mediator.Send(new GetAllCategoryQuery())).ToGenericResult());

        return group;
    }
}








