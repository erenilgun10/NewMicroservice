using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using UdemyNewMicroservice.Catalog.API.Features.Categories.Dto;
using UdemyNewMicroservice.Catalog.API.Repositories;
using UdemyNewMicroservice.Shared;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories.GetById;

public class GetByIdCategoryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetByIdCategoryQuery, ServiceResult<CategoryDto>>
{
    public async Task<ServiceResult<CategoryDto>> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken = default)
    {

        Category? hasCategory = await context.Categories.FindAsync(request.Id, cancellationToken);
        if (hasCategory == null)
        {
            return ServiceResult<CategoryDto>.Error("Category not found", $"The Category with ID {request.Id} was not found.", HttpStatusCode.NotFound);
        }
        var categoryDto = mapper.Map<CategoryDto>(hasCategory);


        return ServiceResult<CategoryDto>.SuccessAsOk(categoryDto);
    }


}
