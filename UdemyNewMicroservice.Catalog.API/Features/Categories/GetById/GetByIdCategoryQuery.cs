using MediatR;
using UdemyNewMicroservice.Catalog.API.Features.Categories.Dto;
using UdemyNewMicroservice.Shared;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories.GetById;

public record UpdateCategoryQuery(Guid Id) : IRequest<ServiceResult<CategoryDto>>;