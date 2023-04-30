using Bell.Reconciliation.Frontend.Shared.Pages;
using Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;
using Bell.Reconciliation.Frontend.Web.Client.Data;
using Bell.Reconciliation.Frontend.Web.Database;
using Bell.Reconciliation.Frontend.Web.Services;
using Microsoft.EntityFrameworkCore;

namespace Bell.Reconciliation.Frontend.Web.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddTransient<IFilterService, FilterService>();
        builder.Services.AddTransient<IInjectBellSource, FetchBellFromDb>();

        builder.Services.AddDbContextFactory<StapleSourceContext>(opts => opts.UseSqlite("Data Source=StapleSource.sqlite3"));
        builder.Services.AddSynchronizingDataFactory();
        //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        await builder.Build().RunAsync();
    }
}