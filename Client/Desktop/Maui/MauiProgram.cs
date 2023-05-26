using Bell.Reconciliation.Frontend.Desktop.Maui.Models;
using Bell.Reconciliation.Frontend.Desktop.Maui.Utilities;
using Bell.Reconciliation.Frontend.Desktop.Services.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Bell.Reconciliation.Frontend.Desktop.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        var executingAssembly = Assembly.GetExecutingAssembly();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        //var a = Assembly.GetExecutingAssembly();
        //using var stream = a.GetManifestResourceStream($"appsettings.json");
        //var config = new ConfigurationBuilder()
        //            .AddJsonStream(stream)
        //            .Build();
        //builder.Configuration.AddConfiguration(config);

        var config = new ConfigurationBuilder()
                    .SetBasePath(Path.GetDirectoryName(executingAssembly.Location))
                    .AddJsonFile($"appsettings.json")
                    .Build();
        builder.Configuration.AddConfiguration(config);
        var settings = builder.Configuration.GetRequiredSection("Settings").Get<Settings>(); ;
        //builder.Services.AddHttpClient();
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(settings.BaseAddress) });
        //builder.Services.AddHttpClient<ISyncData, SyncData>(client =>
        //{
        //    client.BaseAddress = new Uri( settings.BaseAddress);
        //});

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddDbContext<StapleSourceContext>(a => a.UseSqlite(ProjectConfig.DatabasePath));

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        builder.Services.AddTransient<IFilterService, FilterService>();
        builder.Services.AddTransient<ILocalDbRepository, LocalDbRepository>();
        builder.Services.AddTransient<ISyncData, SyncData>();

        // builder.Services.AddTransient<IInjectBellSource, InjectBellSource>();

        var app = builder.Build();
        app.SeedDatabase();
        return app;
    }
}