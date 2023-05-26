using Bell.Reconciliation.Web.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Bell.Reconciliation.Web.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        var filterItems = GetFilterItems(builder);
        builder.Services.AddSingleton(filterItems);

        builder.Services.AddTransient<ServerDbRepository>();
        builder.Services.AddDbContext<BellRecContext>(options =>
                options.UseSqlite(builder.Configuration.GetSection("constring").Get<string>()));
        builder.Services.AddTransient<DatabaseGenerator>();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

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

        return filterItems;
    }
}