using MediatR;
using UdemyNewMicroservice.Shared.Extensions;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories.Update;

public static class UpdateCategoryEndpoint
{

    public static RouteGroupBuilder UpdateCategoryGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPut("Update/{Id:guid}/{name}", async (IMediator mediator, Guid Id, string name) => (await mediator.Send(new UpdateCategoryCommand(Id, name))).ToGenericResult());

        return group;
    }
}








