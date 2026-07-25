using MediatR;
using UdemyNewMicroservice.Catalog.API.Features.Categories.Dto;
using UdemyNewMicroservice.Shared;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories.Delete;

public record DeleteCategoryQuery(Guid Id) : IRequest<ServiceResult>;