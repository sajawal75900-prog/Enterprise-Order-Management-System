
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;
using EnterpriseOrderManagement.Domain;
using Microsoft.EntityFrameworkCore;
using EnterpriseOrderManagement.Repository.Repository;
using EnterpriseOrderManagement.Repository.IRepository;
using EnterpriseOrderManagement.Services.Service;
using EnterpriseOrderManagement.Services.IService;
using EnterpriseOrderManagement.Assembler.Assembler;
using EnterpriseOrderManagement.Assembler.IAssembler;

var builder = WebApplication.CreateBuilder(args);

// Database Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddScoped<IBrandRepository, BrandRepository>();



builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryAssembler, CategoryAssembler>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

app.UseExceptionHandler(errorApp => errorApp.Run(async context =>
{
    var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;
    var isValidationError = exception is ArgumentException;

    context.Response.StatusCode = isValidationError
        ? StatusCodes.Status400BadRequest
        : StatusCodes.Status500InternalServerError;
    context.Response.ContentType = "application/problem+json";

    var problem = new
    {
        type = "about:blank",
        title = isValidationError ? "Validation error" : "An unexpected error occurred.",
        status = context.Response.StatusCode,
        detail = isValidationError ? exception?.Message : "Please try again later."
    };

    await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
}));

// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Test API
app.MapGet("/weatherforecast", () =>
{
    return Results.Ok("API is working successfully!");
})
.WithName("GetWeatherForecast");

app.MapControllers();

app.Run();
