using MediatR;
using UdemyNewMicroservice.Shared;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories.Create;

public record CreateCategoryCommand(string Name) : IRequest<ServiceResult<CreateCategoryResponse>>;





