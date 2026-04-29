using MediatR;
using Microsoft.EntityFrameworkCore;
using UdemyNewMicroservice.Catalog.API.Features.Categories.Dto;
using UdemyNewMicroservice.Catalog.API.Repositories;
using UdemyNewMicroservice.Shared;
using UdemyNewMicroservice.Shared.Extensions;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories.GetAll;

public class GetAllCategoryHandler(AppDbContext context) : IRequestHandler<GetAllCategoryQuery, ServiceResult<List<CategoryDto>>>
{
    public async Task<ServiceResult<List<CategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await context.Categories.ToListAsync();
        var categoriesAsDto = categories.Select(x=> new CategoryDto(x.Id, x.Name)).ToList();
        return ServiceResult<List<CategoryDto>>.SuccessAsOk(categoriesAsDto);
    }


}
