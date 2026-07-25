using AutoMapper;
using MediatR;
using System.Net;
using UdemyNewMicroservice.Catalog.API.Features.Categories.Dto;
using UdemyNewMicroservice.Catalog.API.Repositories;
using UdemyNewMicroservice.Shared;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories.Update;

public class UpdateCategoryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<UpdateCategoryCommand, ServiceResult<UpdateCategoryResponse>>
{
    public async Task<ServiceResult<UpdateCategoryResponse>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken = default)
    {

        Category? hasCategory = await context.Categories.FindAsync(request.Id, cancellationToken);
        if (hasCategory == null)
        {
            return ServiceResult<UpdateCategoryResponse>.Error("Category not found", $"The Category with ID {request.Id} was not found.", HttpStatusCode.NotFound);
        }

        hasCategory.Name = request.Name;
        context.Update(hasCategory);
        await context.SaveChangesAsync(cancellationToken);

        var categoryDto = mapper.Map<CategoryDto>(hasCategory);

        return ServiceResult<UpdateCategoryResponse>.SuccessAsUpdated(new UpdateCategoryResponse(categoryDto.Name), "Category updated successfully.");
    }


}
