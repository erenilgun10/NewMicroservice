using MediatR;
using UdemyNewMicroservice.Catalog.API.Features.Categories.GetById;
using UdemyNewMicroservice.Shared.Extensions;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories.Delete;

public static class DeleteCategoryEndpoint
{

    public static RouteGroupBuilder DeleteCategoryGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("delete/{id:guid}", async (IMediator mediator, Guid id) => (await mediator.Send(new DeleteCategoryQuery(id))).ToGenericResult());

        return group;
    }
}








