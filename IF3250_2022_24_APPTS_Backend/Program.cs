using Microsoft.EntityFrameworkCore;
using IF3250_2022_24_APPTS_Backend.Authorization;
using IF3250_2022_24_APPTS_Backend.Helpers;
using IF3250_2022_24_APPTS_Backend.Services;
using IF3250_2022_24_APPTS_Backend.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;

    // use sql server db in production and sqlite db in development
    services.AddDbContext<DataContext>();

    services.AddCors();
    services.AddControllers().AddJsonOptions(options => 
    {
        options.JsonSerializerOptions.Converters.Add(new DateConverter());
        options.JsonSerializerOptions.Converters.Add(new TimeConverter());
    });

    services.AddSwaggerGen(options => 
    {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "Applicant Tracking System API",
            Description = "API for managing user, job opening, and job application",
            Version = "v1"
        });

        // using System.Reflection;
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

    // configure automapper with all automapper profiles from this assembly
    services.AddAutoMapper(typeof(Program));

    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    // configure DI for application services
    services.AddScoped<IJwtUtils, JwtUtils>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IJobOpeningService, JobOpeningService>();
    services.AddScoped<IJobApplicationService, JobApplicationService>();
    services.AddScoped<IGoogleDriveService, GoogleDriveService>();
}

var app = builder.Build();
string? port = Environment.GetEnvironmentVariable("PORT");

// migrate any database changes on startup (includes initial db creation)
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    //dataContext.Database.Migrate();
}

// configure HTTP request pipeline
{
    // heroku config
    if (!string.IsNullOrWhiteSpace(port))
    {
        app.Urls.Add("http://*:" + port);
    }

    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Applicant Tracking System API");
        options.RoutePrefix = string.Empty;
    });
}

//app.Run("http://localhost:4000");
app.Run();
