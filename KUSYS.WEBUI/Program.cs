using KUSYS.Business.ObjectMappers;
using KUSYS.Business.UnitOfWorks;
using KUSYS.Database.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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
//Console.WriteLine(configuration.GetSection($"ConnectionStrings:{Database}:{Env}").Value);

#region DbContext Injection
builder.Services.AddDbContext<KUSYSDbContext>(options =>
{
    options.UseSqlServer(configuration.GetSection($"ConnectionStrings:{Database}:{Env}").Value);
    options.EnableSensitiveDataLogging();
});
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

app.UseRouting();

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