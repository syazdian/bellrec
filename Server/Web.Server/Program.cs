using Bell.Reconciliation.Web.Server.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Bell.Reconciliation.Web.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        var filterItems = GetFilterItems(builder);
        builder.Services.AddSingleton(filterItems);

        builder.Services.AddTransient<ServerDbRepository>();

        var executingAssembly = Assembly.GetExecutingAssembly();
        var config = new ConfigurationBuilder()
                   .SetBasePath(Path.GetDirectoryName(executingAssembly.Location))
                   .AddJsonFile($"appsettings.json")
                   .Build();

        var constring = builder.Configuration.GetConnectionString("SqlServer");
        builder.Services.AddDbContext<Data.Sqlserver.BellRecContext>(options =>
             options.UseSqlServer(constring));

        builder.Configuration.AddConfiguration(config);
        // builder.Services.AddTransient<DatabaseGenerator>();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseBlazorFrameworkFiles();
        //app.UseBlazorFrameworkFiles("/BellServices/Reconciliation");

        app.UseStaticFiles();
        //app.UseStaticFiles("/BellServices/Reconciliation");

        app.UseRouting();

        app.MapRazorPages();
        app.MapControllers();
        app.MapFallbackToFile("index.html");

        app.Run();
    }

    private static FilterItems GetFilterItems(WebApplicationBuilder builder)
    {
        FilterItems filterItems = new FilterItems();

        LoB loBWireless = new LoB()
        {
            Name = "Wireless",
            SubLoBs = builder.Configuration.GetSection("FilterItems:LoB:Wireless").Get<List<string>>()
        };

        filterItems.LoBs.Add(loBWireless);
        LoB loBWireline = new LoB()
        {
            Name = "Wireline",
            SubLoBs = builder.Configuration.GetSection("FilterItems:LoB:Wireline").Get<List<string>>()
        };
        filterItems.LoBs.Add(loBWireline);
        filterItems.Brands = builder.Configuration.GetSection("FilterItems:Brand").Get<List<string>>();
        filterItems.RebateTypes = builder.Configuration.GetSection("FilterItems:RebateType").Get<List<string>>();
        filterItems.Locations = builder.Configuration.GetSection("FilterItems:Location").Get<List<string>>();

        return filterItems;
    }
}