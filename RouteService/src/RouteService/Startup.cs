using RouteService.Services;
using RouteService.Utils;

namespace RouteService;

public class Startup
{

    public IConfiguration Configuration { get; }

    public Startup(IWebHostEnvironment env)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
    
        // Load User Secrets only in Development
        if (env.IsDevelopment())
        {
            builder.AddUserSecrets<Startup>();
        }
    
        // Load Environment Variables (useful for AWS)
        builder.AddEnvironmentVariables();
    
        Configuration = builder.Build();
    }
    
    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        
        services.AddSingleton(Configuration);
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        services.AddScoped<IRouteMapper, RouteMapper>();
        services.AddScoped<IWaypointFactory, WaypointFactory>();
        services.AddScoped<IGoogleMapsService, GoogleMapsService>();
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();
        
        app.UseExceptionHandler();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/",
                async context =>
                {
                    await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
                });
        });
    }
}