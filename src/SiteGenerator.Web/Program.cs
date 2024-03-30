using SiteGenerator.Web.DI;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddDependencies(builder.Configuration);

builder
    .Build()
    .ConfigureApp()
    .Run();