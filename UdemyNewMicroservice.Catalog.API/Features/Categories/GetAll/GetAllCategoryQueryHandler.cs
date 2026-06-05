using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UdemyNewMicroservice.Catalog.API.Features.Categories.Dto;
using UdemyNewMicroservice.Catalog.API.Repositories;
using UdemyNewMicroservice.Shared;
using UdemyNewMicroservice.Shared.Extensions;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories.GetAll;

public class GetAllCategoryHandler(AppDbContext context,IMapper mapper) : IRequestHandler<GetAllCategoryQuery, ServiceResult<List<CategoryDto>>>
{
    public async Task<ServiceResult<List<CategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await context.Categories.ToListAsync(cancellationToken: cancellationToken);
        var categoriesAsDto = mapper.Map<List<CategoryDto>>(categories);
        return ServiceResult<List<CategoryDto>>.SuccessAsOk(categoriesAsDto);
    }


}
