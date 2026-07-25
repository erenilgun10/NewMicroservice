using AutoMapper;
using MediatR;
using System.Net;
using UdemyNewMicroservice.Catalog.API.Features.Categories.Dto;
using UdemyNewMicroservice.Catalog.API.Repositories;
using UdemyNewMicroservice.Shared;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories.Delete;

public class DeleteCategoryHandler(AppDbContext context) : IRequestHandler<DeleteCategoryQuery, ServiceResult>
{
    public async Task<ServiceResult> Handle(DeleteCategoryQuery request, CancellationToken cancellationToken = default)
    {

        Category? hasCategory = await context.Categories.FindAsync(request.Id, cancellationToken);
        if (hasCategory == null)
        {
            return ServiceResult<CategoryDto>.Error("Category not found", $"The Category with ID {request.Id} was not found.", HttpStatusCode.NotFound);
        }

        context.Categories.Remove(hasCategory);
        await context.SaveChangesAsync(cancellationToken);



        return ServiceResult.SuccessAsNoContent();
    }


}
