using Bell.Reconciliation.Client;
using Bell.Reconciliation.Frontend.Shared.Pages;
using Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;
using Bell.Reconciliation.Frontend.Web.Database;
using Bell.Reconciliation.Frontend.Web.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using SqliteWasmHelper;
using Microsoft.Extensions.DependencyInjection;

namespace Bell.Reconciliation.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddTransient<IFilterService, FilterService>();
            builder.Services.AddTransient<IInjectBellSource, FetchBellFromDb>();
            // builder.Services.AddTransient<IInjectBellSource, InjectBellSource>();
            builder.Services.AddSqliteWasmDbContextFactory<StapleSourceContext>(opts => opts.UseSqlite("Data Source=StapleSource.sqlite3"));

            await builder.Build().RunAsync();
        }
    }
}