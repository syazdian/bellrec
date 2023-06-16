using Bell.Reconciliation.Web.Server.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using System.Reflection;

namespace Bell.Reconciliation.Web.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        builder.Services.AddTransient<ServerDbRepository>();
        builder.Services.AddTransient<CallApi>();

        // var executingAssembly = Assembly.GetExecutingAssembly();
        //var config = new ConfigurationBuilder()
        //           .SetBasePath(Path.GetDirectoryName(executingAssembly.Location))
        //           .AddJsonFile($"appsettings.json")
        //           .Build();
        //builder.Configuration.AddConfiguration(config);

        var constring = builder.Configuration.GetConnectionString("SqlServer");
        builder.Services.AddDbContext<Data.Sqlserver.BellRecContext>(options =>
             options.UseSqlServer(constring));

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
}