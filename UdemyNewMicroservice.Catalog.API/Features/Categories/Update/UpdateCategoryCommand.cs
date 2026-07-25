using MediatR;
using UdemyNewMicroservice.Shared;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories.Update;

public record UpdateCategoryCommand(Guid Id, string Name) : IRequest<ServiceResult<UpdateCategoryResponse>>;





