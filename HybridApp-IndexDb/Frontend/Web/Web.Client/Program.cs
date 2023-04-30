using Bell.Reconciliation.Frontend.Shared.Pages;
using Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;
using Bell.Reconciliation.Frontend.Web.Database;
using Bell.Reconciliation.Frontend.Web.Services;
using Microsoft.EntityFrameworkCore;
using SqliteWasmHelper;

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
        // builder.Services.AddTransient<IInjectBellSource, InjectBellSource>();

        builder.Services.AddSqliteWasmDbContextFactory<StapleSourceContext>(opts => opts.UseSqlite("Data Source=StapleSource.sqlite3"));

        await builder.Build().RunAsync();
    }
}