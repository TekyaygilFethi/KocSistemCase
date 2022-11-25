using KUSYS.Business.Caching.Base;
using KUSYS.Business.Caching.Redis.Server;
using KUSYS.Business.Caching.Redis.Service;
using KUSYS.Business.ObjectMappers;
using KUSYS.Business.UnitOfWorks;
using KUSYS.Database.DbContexts;
using KUSYS.WebUI.Middlewares;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

string Env = "Dev";
string Database = "MSSQL";

IConfiguration configuration = builder.Configuration;

#region Environment Variable Setting
var env = configuration.GetValue<string>("databaseEnvironment");
Env = (String.IsNullOrEmpty(env) || env != "Test") ? Env : env;
Console.WriteLine($"Database Environment: {Env}");
configuration["environment"] = Env;
#endregion

#region AutoMapper Injection
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services
  .AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(CustomMappingProfiles)));
#endregion

#region UnitOfWork Injection
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
#endregion

#region Redis Implementation
builder.Services.AddStackExchangeRedisCache(action =>
{
    action.Configuration = configuration.GetSection("RedisConfiguration:Uri").Value;
});
builder.Services.AddTransient<ICacheService, RedisCacheService>();

builder.Services.AddSingleton<RedisServer>();
#endregion

#region DbContext Injection
builder.Services.AddDbContext<KUSYSDbContext>(options =>
{
    options.UseSqlServer(configuration.GetSection($"ConnectionStrings:{Database}:{Env}").Value);
    options.EnableSensitiveDataLogging();
});
#endregion

#region Identity Injection
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
#endregion

#region Logging Configuration
ConfigureLogging();
builder.Host.UseSerilog();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseMiddleware<GlobalErrorHandlerMiddleware>();

app.UseRouting();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

void ConfigureLogging()
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile(
            $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
            optional: true)
        .Build();

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        .WriteTo.Debug()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
        .Enrich.WithProperty("Environment", environment)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
{
    return new ElasticsearchSinkOptions(new Uri(configuration.GetSection("ElasticConfiguration:Uri").Value))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
    };
}