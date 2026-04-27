using UdemyNewMicroservice.Catalog.API.Features.Categories.Create;

namespace UdemyNewMicroservice.Catalog.API.Features.Categories;

public static class CategoryEndpointExt
{
    public static void AddCategoryGroupEndPointExt(this WebApplication app)
    {

        app.MapGroup("api/categories").CreateCategoryGroupItemEndpoint().RequireAuthorization();
    }


}
