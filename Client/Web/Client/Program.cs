using Bell.Reconciliation.Frontend.Web.Services.Services;
using Radzen;
using SqliteWasmHelper;

namespace Bell.Reconciliation.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped<DialogService>();
        var subFolder = builder.Configuration["baseaddress"];

        //var baseAddress = $"{builder.HostEnvironment.BaseAddress}/{subFolder}";
        //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });

        builder.Services.AddHttpClient();

        builder.Services.AddSqliteWasmDbContextFactory<StapleSourceContext>(opts => opts.UseSqlite("Data Source=StapleSource.sqlite3"));

        builder.Services.AddTransient<IFilterService, FilterService>();
        builder.Services.AddTransient<ILocalDbRepository, LocalDbRepository>();
        builder.Services.AddTransient<IFetchData, FetchData>();
        builder.Services.AddTransient<ISyncData, SyncData>();

        await builder.Build().RunAsync();
    }
}