﻿using Bell.Reconciliation.Frontend.Desktop.Services;
using Bell.Reconciliation.Frontend.Shared.ServiceInterfaces;
using Microsoft.Extensions.Logging;

namespace Bell.Reconciliation.Frontend.Desktop.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            builder.Services.AddTransient<IFilterService, FilterService>();
            builder.Services.AddTransient<IInjectBellSource, InjectBellSource>();

            return builder.Build();
        }
    }
}