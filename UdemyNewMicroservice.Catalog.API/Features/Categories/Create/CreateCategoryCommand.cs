using MediatR;
using UdemyNewMicroservice.Shared;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories.Create;

public record UpdateCategoryCommand(string Name) : IRequest<ServiceResult<UpdateCategoryResponse>>;





