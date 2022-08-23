using BuberBreadkfast.WebApi.Common;
using BuberBreadkfast.WebApi.Providers;
using BuberBreadkfast.WebApi.Services;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddScoped<IBreakfastService, BreakfastService>();
    builder.Services.AddScoped<IDateTimeProvider, DateTimeProvider>();
    //builder.Services.AddScoped<IValidator<CreateBreakfastRequest>, CreateBreakfastRequestValidator>();
    builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
    builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}