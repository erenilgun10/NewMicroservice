using MediatR;
using UdemyNewMicroservice.Catalog.API.Features.Categories.Dto;
using UdemyNewMicroservice.Shared;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories.GetById;

public record GetByIdCategoryQuery(Guid Id) : IRequest<ServiceResult<CategoryDto>>;