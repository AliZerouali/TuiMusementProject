using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Weather.Forecast.Application.Common.Interfaces.DataAccess;
using Weather.Forecast.Cli.Services;
using Weather.Forecast.Infrastructure.DataAccessLayer;

var builder = new ConfigurationBuilder();
BuildConfig(builder);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Build())
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

Log.Logger.Information("Welcome to TUI Musement CLI Application");

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        //Add Services
        services.AddScoped<ICityDataAccess, CityDataAccess>();
        services.AddScoped<IWeatherDataAccess, WeatherDataAccess>();

        //Add HttpClient
        services.AddHttpClient<CityDataAccess>();
        services.AddHttpClient<WeatherDataAccess>();
    })
    .UseSerilog()
    .Build();

var svc = ActivatorUtilities.CreateInstance<WeatherForecastService>(host.Services);
await svc.GetAllForecastInfosAsync();

static void BuildConfig(IConfigurationBuilder builder)
{
    builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
}
