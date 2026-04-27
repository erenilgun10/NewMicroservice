using Scalar.AspNetCore;
using UdemyNewMicroservice.Catalog.API.Features.Categories;
using UdemyNewMicroservice.Catalog.API.Options;
using UdemyNewMicroservice.Catalog.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();





var app = builder.Build();

app.AddCategoryGroupEndPointExt();









if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}


app.Run();


