using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UdemyNewMicroservice.Catalog.API.Repositories;
using UdemyNewMicroservice.Shared;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories.Create;

public class CreateCategoryCommandHandler(AppDbContext context) : IRequestHandler<UpdateCategoryCommand, ServiceResult<UpdateCategoryResponse>>
{
    public async Task<ServiceResult<UpdateCategoryResponse>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {

        var existCategory = await context.Categories.AnyAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);

        if (existCategory)
        {
            ServiceResult<UpdateCategoryResponse>.Error("Category already exist", $"A category with the name '{request.Name}' already exists.", HttpStatusCode.BadRequest);
        }
        var category = new Category
        {
            Name = request.Name,
            Id = NewId.NextSequentialGuid(),
        };

        await context.AddAsync(category, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return ServiceResult<UpdateCategoryResponse>.SuccessAsCreated(new UpdateCategoryResponse(category.Id),"<empty>");

    }
}