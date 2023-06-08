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
        //var baseAddress = builder.HostEnvironment.BaseAddress;// + "/BellServices/Reconciliation/";
        // builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });

        builder.Services.AddHttpClient();

        builder.Services.AddSqliteWasmDbContextFactory<StapleSourceContext>(opts => opts.UseSqlite("Data Source=StapleSource.sqlite3"));

        builder.Services.AddTransient<IFilterService, FilterService>();
        builder.Services.AddTransient<ILocalDbRepository, LocalDbRepository>();

        // builder.Services.AddTransient<IInjectBellSource, FetchBellFromDb>();
        builder.Services.AddTransient<IFetchData, FetchData>();
        builder.Services.AddTransient<ISyncData, SyncData>();
        // builder.Services.AddTransient<IInjectBellSource, InjectBellSource>();

        await builder.Build().RunAsync();
    }
}