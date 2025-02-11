﻿using Fintrack.Services.Interface;
using Microsoft.Extensions.Logging;
using Fintrack.Services;
using MudBlazor.Services;

namespace Fintrack
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
            builder.Services.AddScoped<IUser, UserService>();
            builder.Services.AddMudServices();
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Services.AddScoped<ITransaction, TransactionService>();
            builder.Logging.AddDebug();
            builder.Services.AddMudServices();

#endif

            return builder.Build();
        }
    }
}
