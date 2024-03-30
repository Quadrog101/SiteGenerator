using System.Reflection;
using SiteGenerator.Domain.Abstractions;
using SiteGenerator.Domain.Options;
using SiteGenerator.Domain.Services;
using SiteGenerator.Infrastructure.Db;
using SiteGenerator.Web.Filters;

namespace SiteGenerator.Web.DI;

public static class Dependencies
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddControllers((options =>
        {
            options.Filters.Add(typeof(GlobalExceptionFilters));
        }));
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
        });

        services.AddAutoMapper(typeof(Dependencies));

        services.Configure<DatabaseContextConfiguration>(
            configuration.GetSection(DatabaseContextConfiguration.SectionName));
        
        services.AddSingleton<IApplicationContext, ApplicationContext>();
        services.AddScoped<IWebsiteService, WebsiteService>();

        return services;
    }

    public static WebApplication ConfigureApp(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}